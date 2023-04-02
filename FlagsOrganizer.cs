using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal abstract class FlagsOrganizer
    {
        public enum FlagType
        {
            FieldItem,
            HiddenItem,
            TrainerBattle,
            StationaryBattle,
            InGameTrade,
            Gift,
            GeneralEvent,
            SideEvent,
            StoryEvent,
        }

        protected class FlagDetail
        {
            public int FlagIdx { get; private set; }
            public bool IsSet { get; private set; }
            public string FlagTypeTxt { get; private set; }
            public string LocationName { get; private set; }
            public string DetailMsg { get; private set; }

            public FlagDetail(int flagIdx, FlagType flagType, string detailMsg) : this(flagIdx, flagType, "", detailMsg)
            {
            }

            public FlagDetail(int flagIdx, FlagType flagType, string locationName, string detailMsg)
            {
                FlagIdx = flagIdx;

                switch (flagType)
                {
                    case FlagType.FieldItem:
                        FlagTypeTxt = "FIELD ITEM";
                        break;

                    case FlagType.HiddenItem:
                        FlagTypeTxt = "HIDDEN ITEM";
                        break;

                    case FlagType.TrainerBattle:
                        FlagTypeTxt = "TRAINER BATTLE";
                        break;

                    case FlagType.StationaryBattle:
                        FlagTypeTxt = "STATIONARY BATTLE";
                        break;

                    case FlagType.InGameTrade:
                        FlagTypeTxt = "IN-GAME TRADE";
                        break;

                    case FlagType.Gift:
                        FlagTypeTxt = "GIFT";
                        break;

                    case FlagType.GeneralEvent:
                        FlagTypeTxt = "EVENT";
                        break;

                    case FlagType.SideEvent:
                        FlagTypeTxt = "SIDE EVENT";
                        break;

                    case FlagType.StoryEvent:
                        FlagTypeTxt = "STORY EVENT";
                        break;
                }

                LocationName = locationName;
                DetailMsg = detailMsg;
            }

            public override string ToString()
            {
                if (string.IsNullOrEmpty(LocationName))
                {
                    return string.Format("{0} - {1}\r\n", FlagTypeTxt, DetailMsg);
                }

                else
                {
                    return string.Format("{0} - {1} - {2}\r\n", FlagTypeTxt, LocationName, DetailMsg);
                }
            }
        }


        protected SaveFile m_savFile;
        protected bool[] m_eventFlags;

        protected List<FlagDetail> m_missingEventFlagsList = new List<FlagDetail>(4096);

        protected abstract void InitFlagsData(SaveFile savFile);

        protected abstract void CheckAllMissingFlags();

        protected virtual void AssembleChecklist() { }

        public virtual void ExportMissingFlags()
        {
            CheckAllMissingFlags();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < m_missingEventFlagsList.Count; ++i)
            {
                sb.Append(m_missingEventFlagsList[i]);
            }

            System.IO.File.WriteAllText(string.Format("missing_events_{0}.txt", m_savFile.Version), sb.ToString());
        }

        public virtual void ExportChecklist()
        {
            AssembleChecklist();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < m_missingEventFlagsList.Count; ++i)
            {
                sb.AppendFormat("[{0}] {1}", m_missingEventFlagsList[i].IsSet ? "x" : " ", m_missingEventFlagsList[i]);
            }

            System.IO.File.WriteAllText(string.Format("checklist_{0}.txt", m_savFile.Version), sb.ToString());
        }

        public virtual void DumpAllFlags()
        {
            StringBuilder sb = new StringBuilder(m_eventFlags.Length);

            for (int i = 0; i < m_eventFlags.Length; ++i)
            {
                sb.AppendFormat("FLAG_0x{0:X4} {1}\r\n", i, m_eventFlags[i]);
            }

            System.IO.File.WriteAllText(string.Format("flags_dump_{0}.txt", m_savFile.Version), sb.ToString());
        }

        public virtual void MarkFlags(FlagType flagType) { }
        public virtual void UnmarkFlags(FlagType flagType) { }

        protected void CheckMissingFlag(int flagIdx, FlagType flagType, string mapLocation, string flagDetail)
        {
            if (!IsFlagSet(flagIdx))
            {
                m_missingEventFlagsList.Add(new FlagDetail(flagIdx, flagType, mapLocation, flagDetail));
            }
        }

        protected bool IsFlagSet(int flagIdx) => m_eventFlags[flagIdx];



        public static FlagsOrganizer OrganizeFlags(SaveFile savFile)
        {
            FlagsOrganizer flagsOrganizer = null;

            switch (savFile.Version)
            {
                case GameVersion.RD:
                case GameVersion.GN:
                case GameVersion.RB:
                    flagsOrganizer = new FlagsGen1RB();
                    break;

                case GameVersion.YW:
                    flagsOrganizer = new FlagsGen1Y();
                    break;

                case GameVersion.GD:
                case GameVersion.SI:
                case GameVersion.GS:
                    flagsOrganizer = new FlagsGen2GS();
                    break;

                case GameVersion.C:
                    flagsOrganizer = new FlagsGen2C();
                    break;

                case GameVersion.R:
                case GameVersion.S:
                case GameVersion.RS:
                    flagsOrganizer = new FlagsGen3RS();
                    break;

                case GameVersion.FR:
                case GameVersion.LG:
                case GameVersion.FRLG:
                    flagsOrganizer = new FlagsGen3FRLG();
                    break;

                case GameVersion.E:
                    flagsOrganizer = new FlagsGen3E();
                    break;

                case GameVersion.D:
                case GameVersion.P:
                case GameVersion.DP:
                    flagsOrganizer = new FlagsGen4DP();
                    break;

                case GameVersion.Pt:
                    flagsOrganizer = new FlagsGen4Pt();
                    break;

                case GameVersion.HG:
                case GameVersion.SS:
                case GameVersion.HGSS:
                    flagsOrganizer = new FlagsGen4HGSS();
                    break;

                case GameVersion.B:
                case GameVersion.W:
                case GameVersion.BW:
                    flagsOrganizer = new FlagsGen5BW();
                    break;

                case GameVersion.B2:
                case GameVersion.W2:
                case GameVersion.B2W2:
                    flagsOrganizer = new FlagsGen5B2W2();
                    break;

                case GameVersion.X:
                case GameVersion.Y:
                case GameVersion.XY:
                case GameVersion.OR:
                case GameVersion.AS:
                case GameVersion.ORAS:
                case GameVersion.SN:
                case GameVersion.MN:
                case GameVersion.SM:
                case GameVersion.US:
                case GameVersion.UM:
                case GameVersion.USUM:
                case GameVersion.GP:
                case GameVersion.GE:
                case GameVersion.BD:
                case GameVersion.SP:
                case GameVersion.BDSP:
                    flagsOrganizer = new DummyOrgFlags();
                    break;

                case GameVersion.SW:
                case GameVersion.SH:
                case GameVersion.SWSH:
                case GameVersion.PLA:
                case GameVersion.SL:
                case GameVersion.VL:
                case GameVersion.SV:
                    flagsOrganizer = new DummyOrgBlockFlags();
                    break;

                default:
                    break;
            }

            if (flagsOrganizer != null)
            {
                flagsOrganizer.InitFlagsData(savFile);
            }

            return flagsOrganizer;
        }

    }



    //TEMP
    class DummyOrgFlags : FlagsOrganizer
    {
        protected override void CheckAllMissingFlags() { }
        protected override void AssembleChecklist() { }
        protected override void InitFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;
            m_eventFlags = (m_savFile as IEventFlagArray).GetEventFlags();
            m_missingEventFlagsList.Clear();
        }

        public override void ExportMissingFlags() { }
        public override void ExportChecklist() { }
    }

    class DummyOrgBlockFlags : FlagsOrganizer
    {
        Dictionary<uint, bool> m_blockEventFlags;

        protected override void CheckAllMissingFlags() { }
        protected override void AssembleChecklist() { }
        protected override void InitFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;
            m_eventFlags = new bool[0]; // dummy

            m_blockEventFlags = new Dictionary<uint, bool>();
            foreach (var b in (m_savFile as ISCBlockArray).AllBlocks)
            {
                if (b.Type == SCTypeCode.Bool1 || b.Type == SCTypeCode.Bool2)
                {
                    m_blockEventFlags.Add(b.Key, (b.Type == SCTypeCode.Bool2));
                }
            }

            m_missingEventFlagsList.Clear();
        }

        public override void ExportMissingFlags() { }

        public override void ExportChecklist() { }

        public override void DumpAllFlags()
        {
            StringBuilder sb = new StringBuilder(m_blockEventFlags.Count);

            var keys = new List<uint>(m_blockEventFlags.Keys);
            for (int i = 0; i < keys.Count; ++i)
            {
                sb.AppendFormat("FLAG_0x{0:X8} {1}\r\n", keys[i], m_blockEventFlags[keys[i]]);
            }

            System.IO.File.WriteAllText(string.Format("flags_dump_{0}.txt", m_savFile.Version), sb.ToString());
        }
    }
}
