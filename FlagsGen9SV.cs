using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal class FlagsGen9SV : FlagsOrganizer
    {
        static string s_flagsList_res = null;
        Dictionary<ulong, bool> m_blocksStatus;

        protected override void InitFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;
            m_blocksStatus = new Dictionary<ulong, bool>(4000);

#if DEBUG
            // Force refresh
            s_flagsList_res = null;
#endif

            if (s_flagsList_res == null)
            {
                s_flagsList_res = ReadFlagsListRes("flags_gen9sv.txt");
            }

            AssembleList(s_flagsList_res);
        }

        protected override void AssembleList(string flagsList_res, bool[] customFlagValues = null)
        {
            var savEventBlocks = (m_savFile as ISCBlockArray).Accessor;

            m_eventFlagsList.Clear();
            m_blocksStatus.Clear();

            // Field Items
            FillBlockStatus(savEventBlocks.GetBlockSafe(0x2482AD60).Data, endKey: 0x0000000000000000);
            // Trainer Status
            FillBlockStatus(savEventBlocks.GetBlockSafe(0xF018C4AC).Data, endKey: 0xCBF29CE484222645);

            //TODO:
            // Ghimighoul chests

            using (System.IO.StringReader reader = new System.IO.StringReader(flagsList_res))
            {
                string s = reader.ReadLine();
                do
                {
                    if (!string.IsNullOrWhiteSpace(s) && !s.StartsWith("//"))
                    {
                        var flagDetail = new FlagDetail(s);
                        if (flagDetail.AHTB != 0)
                        {
                            if (m_blocksStatus.ContainsKey(flagDetail.AHTB))
                            {
                                flagDetail.IsSet = m_blocksStatus[flagDetail.AHTB];
                                m_blocksStatus.Remove(flagDetail.AHTB);
                            }
                            else
                            {
                                bool flagVal = false;
                                bool handled = false;

                                switch (flagDetail.FlagTypeVal)
                                {
                                    case FlagType.StaticBattle:
                                        {
                                            switch (flagDetail.FlagIdx)
                                            {
                                                case 0xA3B2E1E8: // Ting-Lu
                                                case 0xB6D28884: // Chien-Pao
                                                case 0x8FC1AFF5: // Wo-Chien
                                                case 0x0FD2F9E2: // Chi-Yu
                                                    flagVal = ((int)savEventBlocks.GetBlockSafe(flagDetail.FlagIdx).GetValue() == 3);
                                                    handled = true;
                                                    break;
                                            }
                                        }
                                        break;

                                    case FlagType.StoryEvent:
                                        {
                                            switch (flagDetail.FlagIdx)
                                            {
                                                case 0x6C29ACC5: // Badge Dark
                                                case 0x71DB2CEB: // Badge Poison
                                                case 0xE1271327: // Badge Fairy
                                                case 0x9C6FF7DD: // Badge Fire
                                                case 0x2A3AC89A: // Badge Fighting
                                                case 0x8205ECAD: // Badge Electric
                                                case 0x3B819021: // Badge Psychic
                                                case 0xCDA61DED: // Badge Ghost
                                                case 0x46B6CB30: // Badge Ice
                                                case 0xB4C3AFE6: // Badge Grass
                                                case 0xA803FAAD: // Badge Water
                                                case 0x89306FE6: // Badge Bug
                                                case 0xF90EFD79: // Badge Normal
                                                case 0xEC7361B7: // Badge Dragon
                                                case 0x0D0602DE: // Badge Steel
                                                case 0x9C16DA94: // Badge Flying
                                                case 0xA6CDE603: // Badge Rock
                                                case 0xBDAC74B3: // Badge Ground
                                                    flagVal = ((int)savEventBlocks.GetBlockSafe(flagDetail.FlagIdx).GetValue() != 0);
                                                    handled = true;
                                                    break;
                                            }
                                        }
                                        break;
                                }

                                // Common bool block
                                if (!handled)
                                {
                                    flagVal = (savEventBlocks.GetBlockSafe(flagDetail.FlagIdx).Type == SCTypeCode.Bool2);
                                }

                                flagDetail.IsSet = flagVal;
                            }
                            m_eventFlagsList.Add(flagDetail);
                        }
                    }

                    s = reader.ReadLine();

                } while (s != null);
            }

            // Fill missing block status
            foreach (var pair in m_blocksStatus)
            {
                m_eventFlagsList.Add(new FlagDetail((uint)pair.Key, FlagType._Unknown, "", "") { IsSet = pair.Value, AHTB = pair.Key });
            }


            /*var data = savEventBlocks.GetBlockSafe(0x2482AD60).Data;

            for (int i = 0; i < data.Length; i += 16)
            {
                data[i + 8] = 1;
            }
            System.IO.File.WriteAllBytes("2482AD60_grabbed.bin", data);*/

            //FnvHash.HashFnv1a_64(flag);
        }

        void FillBlockStatus(byte[] aData, ulong endKey)
        {
            using (var ms = new System.IO.MemoryStream(aData))
            {
                using (var reader = new System.IO.BinaryReader(ms))
                {
                    do
                    {
                        var key = reader.ReadUInt64();

                        if (key == endKey)
                        {
                            break;
                        }

                        if (!m_blocksStatus.ContainsKey(key))
                        {
                            m_blocksStatus.Add(key, reader.ReadUInt64() == 1);
                        }
                        else
                        {
                            throw new ArgumentException("AHTB collision: 0x" + key.ToString("X8"));
                        }

                    } while (ms.Position < ms.Length);
                }
            }
        }

        public override bool SupportsEditingFlag(FlagType flagType)
        {
            switch (flagType)
            {
                case FlagType.FieldItem:
                //case FlagType.HiddenItem:
                case FlagType.TrainerBattle:
                case FlagType.SideEvent:
                case FlagType.InGameTrade:
                case FlagType.StaticBattle:
                case FlagType.Gift:
                    return true;

                default:
                    return false;
            }
        }

        public override void MarkFlags(FlagType flagType)
        {
            ChangeFlagsVal(flagType, value: true);
        }

        public override void UnmarkFlags(FlagType flagType)
        {
            ChangeFlagsVal(flagType, value: false);
        }

        void ChangeFlagsVal(FlagType flagType, bool value)
        {
            if (SupportsEditingFlag(flagType))
            {
                var savEventBlocks = (m_savFile as ISCBlockArray).Accessor;
                byte[] bdata;

                if (flagType == FlagType.FieldItem)
                {
                    bdata = savEventBlocks.GetBlockSafe(0x2482AD60).Data;
                    using (var ms = new System.IO.MemoryStream(bdata))
                    {
                        using (var writer = new System.IO.BinaryWriter(ms))
                        {
                            foreach (var f in m_eventFlagsList)
                            {
                                if (ms.Position < ms.Length)
                                {
                                    if (f.FlagTypeVal == flagType)
                                    {
                                        f.IsSet = value;
                                        writer.Write(f.AHTB);
                                        writer.Write(value ? (ulong)1 : (ulong)0);
                                    }
                                }
                            }
                        }
                    }
                }
                
                else if (flagType == FlagType.TrainerBattle)
                {
                    bdata = savEventBlocks.GetBlockSafe(0xF018C4AC).Data;
                    using (var ms = new System.IO.MemoryStream(bdata))
                    {
                        using (var writer = new System.IO.BinaryWriter(ms))
                        {
                            foreach (var f in m_eventFlagsList)
                            {
                                if (ms.Position < ms.Length)
                                {
                                    if (f.FlagTypeVal == flagType)
                                    {
                                        f.IsSet = value;
                                        writer.Write(value ? f.AHTB : 0xCBF29CE484222645);
                                        writer.Write(value ? (ulong)1 : (ulong)0);
                                    }
                                }
                            }

                            // fill blanks
                            while (ms.Position < ms.Length)
                            {
                                writer.Write(0xCBF29CE484222645);
                                writer.Write((ulong)0);
                            }
                        }
                    }
                }

                else if (flagType == FlagType.SideEvent || flagType == FlagType.InGameTrade || flagType == FlagType.Gift)
                {
                    foreach (var f in m_eventFlagsList)
                    {
                        if (f.FlagTypeVal == flagType)
                        {
                            f.IsSet = value;
                            savEventBlocks.GetBlockSafe(f.FlagIdx).ChangeBooleanType(value ? SCTypeCode.Bool2 : SCTypeCode.Bool1);
                        }
                    }
                }

                /*var blocks = (m_savFile as ISCBlockArray).Accessor;

                foreach (var f in m_eventFlagsList)
                {
                    if (f.FlagTypeVal == flagType)
                    {
                        f.IsSet = value;
                        blocks.GetBlockSafe((uint)f.FlagIdx).ChangeBooleanType(value ? SCTypeCode.Bool2 : SCTypeCode.Bool1);
                    }
                }*/
            }
        }

        public override void ExportMissingFlags()
        {
            //m_eventFlagsList.Sort((x, y) => x.OrderKey - y.OrderKey);

            StringBuilder sb = new StringBuilder(100 * 1024);
            for (int i = 0; i < m_eventFlagsList.Count; ++i)
            {
                if (!m_eventFlagsList[i].IsSet && ShouldExportEvent(m_eventFlagsList[i]))
                {
                    sb.Append($"{m_eventFlagsList[i]}\r\n");
                }
            }

            System.IO.File.WriteAllText(string.Format("missing_events_{0}.txt", m_savFile.Version), sb.ToString());
        }

        public override void ExportChecklist()
        {
            //m_eventFlagsList.Sort((x, y) => x.OrderKey - y.OrderKey);

            StringBuilder sb = new StringBuilder(100 * 1024);
            for (int i = 0; i < m_eventFlagsList.Count; ++i)
            {
                if (ShouldExportEvent(m_eventFlagsList[i]))
                {
                    sb.AppendFormat("[{0}] {1}\r\n", m_eventFlagsList[i].IsSet ? "x" : " ", m_eventFlagsList[i]);
                }
            }

            System.IO.File.WriteAllText(string.Format("checklist_{0}.txt", m_savFile.Version), sb.ToString());
        }

        public override void DumpAllFlags()
        {
            StringBuilder sb = new StringBuilder(100 * 1024);

            for (int i = 0; i < m_eventFlagsList.Count; ++i)
            {
                sb.AppendFormat("FLAG_0x{0:X16} {1}\t{2}\r\n", m_eventFlagsList[i].AHTB, m_eventFlagsList[i].IsSet,
                    m_eventFlagsList[i].FlagTypeVal == FlagType._Unused ? "UNUSED" : m_eventFlagsList[i].ToString());
            }

            System.IO.File.WriteAllText(string.Format("flags_dump_{0}.txt", m_savFile.Version), sb.ToString());
        }

        protected override bool ShouldExportEvent(FlagDetail eventDetail)
        {
            if (eventDetail.FlagTypeVal == FlagType.GeneralEvent)
            {
                bool shouldInclude = false;

                switch (eventDetail.FlagIdx)
                {
                    default:
                        shouldInclude = false;
                        break;
                }

                return shouldInclude;
            }
            else
            {
                return base.ShouldExportEvent(eventDetail);
            }
        }

    }

}
