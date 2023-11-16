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
        static string s_chkdb_res = null;
        Dictionary<ulong, bool> m_blocksStatus;

        protected override void InitEventFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;
            m_blocksStatus = new Dictionary<ulong, bool>(4000);

#if DEBUG
            // Force refresh
            s_chkdb_res = null;
#endif

            if (s_chkdb_res == null)
            {
                s_chkdb_res = ReadResFile("chkdb_gen9sv.txt");
            }

            m_flagsSourceInfo["EvtFlags"] = 0;
            m_flagsSourceInfo["ItemFlags"] = 1;
            m_flagsSourceInfo["TRFlags"] = 2;
            m_flagsSourceInfo["-"] = -1;

            ParseChecklist(s_chkdb_res);
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
            // Trainer Status Expansion (v2.0.2+)
            FillBlockStatus(savEventBlocks.GetBlockSafe(0x28E475DE).Data, endKey: 0xCBF29CE484222645);

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
                                    case EventFlagType.StaticBattle:
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

                                    case EventFlagType.StoryEvent:
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
                m_eventFlagsList.Add(new FlagDetail((uint)pair.Key, source: 0, EventFlagType._Unknown, "", "") { IsSet = pair.Value, AHTB = pair.Key });
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
            // Ignore dummy blocks
            if (aData.Length == 0)
            {
                return;
            }

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

        public override bool SupportsEditingFlag(EventFlagType flagType)
        {
            switch (flagType)
            {
                case EventFlagType.FieldItem:
                //case FlagType.HiddenItem:
                case EventFlagType.TrainerBattle:
                case EventFlagType.SideEvent:
                case EventFlagType.InGameTrade:
                case EventFlagType.StaticBattle:
                case EventFlagType.Gift:
                    return true;

                default:
                    return false;
            }
        }

        public override void MarkFlags(EventFlagType flagType)
        {
            ChangeFlagsVal(flagType, value: true);
        }

        public override void UnmarkFlags(EventFlagType flagType)
        {
            ChangeFlagsVal(flagType, value: false);
        }

        void ChangeFlagsVal(EventFlagType flagType, bool value)
        {
            if (SupportsEditingFlag(flagType))
            {
                var savEventBlocks = (m_savFile as ISCBlockArray).Accessor;
                byte[] bdata;

                if (flagType == EventFlagType.FieldItem)
                {
                    bdata = savEventBlocks.GetBlockSafe(0x2482AD60).Data;
                    using (var ms = new System.IO.MemoryStream(bdata))
                    {
                        using (var writer = new System.IO.BinaryWriter(ms))
                        {
                            foreach (var evt in m_eventsChecklist)
                            {
                                if (ms.Position < ms.Length)
                                {
                                    if (evt.EvtTypeVal == flagType)
                                    {
                                        evt.IsDone = value;
                                        writer.Write(evt.EvtId);
                                        writer.Write(value ? (ulong)1 : (ulong)0);
                                    }
                                }
                            }
                        }
                    }
                }
                
                else if (flagType == EventFlagType.TrainerBattle)
                {
                    bdata = savEventBlocks.GetBlockSafe(0xF018C4AC).Data;
                    using (var ms = new System.IO.MemoryStream(bdata))
                    {
                        using (var writer = new System.IO.BinaryWriter(ms))
                        {
                            foreach (var evt in m_eventsChecklist)
                            {
                                if (ms.Position < ms.Length)
                                {
                                    if (evt.EvtTypeVal == flagType)
                                    {
                                        evt.IsDone = value;
                                        writer.Write(value ? evt.EvtId : 0xCBF29CE484222645);
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

                else if (flagType == EventFlagType.SideEvent || flagType == EventFlagType.InGameTrade || flagType == EventFlagType.Gift)
                {
                    foreach (var evt in m_eventsChecklist)
                    {
                        if (evt.EvtTypeVal == flagType)
                        {
                            evt.IsDone = value;
                            savEventBlocks.GetBlockSafe((uint)evt.EvtId).ChangeBooleanType(value ? SCTypeCode.Bool2 : SCTypeCode.Bool1);
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

        public override void DumpAllFlags()
        {
            StringBuilder sb = new StringBuilder(100 * 1024);

            for (int i = 0; i < m_eventFlagsList.Count; ++i)
            {
                sb.AppendFormat("FLAG_0x{0:X16} {1}\t{2}\r\n", m_eventFlagsList[i].AHTB, m_eventFlagsList[i].IsSet,
                    m_eventFlagsList[i].FlagTypeVal == EventFlagType._Unused ? "UNUSED" : m_eventFlagsList[i].ToString());
            }

            System.IO.File.WriteAllText(string.Format("flags_dump_{0}.txt", m_savFile.Version), sb.ToString());
        }

        protected override bool ShouldExportEvent(FlagDetail eventDetail)
        {
            if (eventDetail.FlagTypeVal == EventFlagType.GeneralEvent)
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

        protected override bool IsEvtSet(EventDetail evtDetail)
        {
            bool isEvtSet = false;
            ulong idx = (uint)evtDetail.EvtId;
            var savEventBlocks = (m_savFile as ISCBlockArray).Accessor;

            switch (evtDetail.EvtSource)
            {
                case 0: // EvtFlags
                    isEvtSet = (savEventBlocks.GetBlockSafe((uint)idx).Type == SCTypeCode.Bool2);
                    break;

                case 1: // ItemFlags
                    {
                        var bdata = savEventBlocks.GetBlockSafe(0x2482AD60).Data;
                        using (var ms = new System.IO.MemoryStream(bdata))
                        {
                            using (var reader = new System.IO.BinaryReader(ms))
                            {
                                while (ms.Position < ms.Length)
                                {
                                    ulong id = reader.ReadUInt64();
                                    bool isSet = (reader.ReadUInt64() == 1);

                                    if (id == evtDetail.EvtId && isSet)
                                    {
                                        isEvtSet = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;

                case 2: // TRFlags
                    {
                        var bdata1 = savEventBlocks.GetBlockSafe(0xF018C4AC).Data;
                        var bdata2 = savEventBlocks.GetBlockSafe(0x28E475DE).Data;
                        using (var ms1 = new System.IO.MemoryStream(bdata1))
                        using (var ms2 = new System.IO.MemoryStream(bdata2))
                        {
                            using (var reader = new System.IO.BinaryReader(ms1))
                            {
                                while (ms1.Position < ms1.Length)
                                {
                                    ulong id = reader.ReadUInt64();
                                    bool isSet = (reader.ReadUInt64() == 1);

                                    if (id == evtDetail.EvtId && isSet)
                                    {
                                        isEvtSet = true;
                                        break;
                                    }
                                }
                            }
                            if (!isEvtSet)
                            {
                                using (var reader = new System.IO.BinaryReader(ms2))
                                {
                                    while (ms2.Position < ms2.Length)
                                    {
                                        ulong id = reader.ReadUInt64();
                                        bool isSet = (reader.ReadUInt64() == 1);

                                        if (id == evtDetail.EvtId && isSet)
                                        {
                                            isEvtSet = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;

                default:
                    isEvtSet = false;
                    break;
            }

            return isEvtSet;
        }
    }
}
