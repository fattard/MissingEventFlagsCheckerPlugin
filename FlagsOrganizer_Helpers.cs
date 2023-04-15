using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{

    static class FlagTypeExtensions
    {
        public static FlagsOrganizer.FlagType Parse(this FlagsOrganizer.FlagType flagType, string txt)
        {
            switch (txt)
            {
                case "FIELD ITEM":
                    flagType = FlagsOrganizer.FlagType.FieldItem;
                    break;

                case "HIDDEN ITEM":
                    flagType = FlagsOrganizer.FlagType.HiddenItem;
                    break;

                case "TRAINER BATTLE":
                    flagType = FlagsOrganizer.FlagType.TrainerBattle;
                    break;

                case "STATIC BATTLE":
                case "STATIONARY_BATTLE":
                    flagType = FlagsOrganizer.FlagType.StaticBattle;
                    break;

                case "IN-GAME TRADE":
                    flagType = FlagsOrganizer.FlagType.InGameTrade;
                    break;

                case "GIFT":
                    flagType = FlagsOrganizer.FlagType.Gift;
                    break;

                case "EVENT":
                    flagType = FlagsOrganizer.FlagType.GeneralEvent;
                    break;

                case "SIDE EVENT":
                    flagType = FlagsOrganizer.FlagType.SideEvent;
                    break;

                case "STORY EVENT":
                    flagType = FlagsOrganizer.FlagType.StoryEvent;
                    break;

                case "BERRY TREE":
                    flagType = FlagsOrganizer.FlagType.BerryTree;
                    break;

                case "_UNUSED":
                    flagType = FlagsOrganizer.FlagType._Unused;
                    break;

                default:
                    flagType = FlagsOrganizer.FlagType._Unknown;
                    break;
            }

            return flagType;
        }

        public static string AsText(this FlagsOrganizer.FlagType flagType)
        {
            string flagTypeTxt = "";

            switch (flagType)
            {
                case FlagsOrganizer.FlagType.FieldItem:
                    flagTypeTxt = "FIELD ITEM";
                    break;

                case FlagsOrganizer.FlagType.HiddenItem:
                    flagTypeTxt = "HIDDEN ITEM";
                    break;

                case FlagsOrganizer.FlagType.TrainerBattle:
                    flagTypeTxt = "TRAINER BATTLE";
                    break;

                case FlagsOrganizer.FlagType.StaticBattle:
                    flagTypeTxt = "STATIC BATTLE";
                    break;

                case FlagsOrganizer.FlagType.InGameTrade:
                    flagTypeTxt = "IN-GAME TRADE";
                    break;

                case FlagsOrganizer.FlagType.Gift:
                    flagTypeTxt = "GIFT";
                    break;

                case FlagsOrganizer.FlagType.GeneralEvent:
                    flagTypeTxt = "EVENT";
                    break;

                case FlagsOrganizer.FlagType.SideEvent:
                    flagTypeTxt = "SIDE EVENT";
                    break;

                case FlagsOrganizer.FlagType.StoryEvent:
                    flagTypeTxt = "STORY EVENT";
                    break;

                case FlagsOrganizer.FlagType.BerryTree:
                    flagTypeTxt = "BERRY TREE";
                    break;

                case FlagsOrganizer.FlagType._Unused:
                    flagTypeTxt = "_UNUSED";
                    break;

                default:
                    flagTypeTxt = "";
                    break;
            }

            return flagTypeTxt;
        }
    }



    class DummyOrgFlags : FlagsOrganizer
    {
        protected override void InitFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;
            bool[] savEventFlags = (m_savFile as IEventFlagArray).GetEventFlags();
            m_eventFlagsList.Clear();

            for (int i = 0; i < savEventFlags.Length; ++i)
            {
                m_eventFlagsList.Add(new FlagDetail(i, FlagType._Unknown, "", "") { IsSet = savEventFlags[i] });
            }
        }

        public override void ExportMissingFlags() { }

        public override void ExportChecklist() { }

        public override void MarkFlags(FlagType flagType) { }

        public override void UnmarkFlags(FlagType flagType) { }

        public override bool SupportsEditingFlag(FlagType flagType) { return false; }
    }


    class DummyOrgBlockFlags : FlagsOrganizer
    {
        List<SCBlock> m_blockEventFlags;

        protected override void InitFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;

            m_blockEventFlags = new List<SCBlock>(5000);
            foreach (var b in (m_savFile as ISCBlockArray).AllBlocks)
            {
                // Filter only bool blocks
                if (b.Type == SCTypeCode.Bool1 || b.Type == SCTypeCode.Bool2)
                {
                    m_blockEventFlags.Add(b);
                }
            }

            m_eventFlagsList.Clear();

            for (int i = 0; i < m_blockEventFlags.Count; ++i)
            {
                var b = m_blockEventFlags[i];
                m_eventFlagsList.Add(new FlagDetail((int)b.Key, FlagType._Unknown, "", "") { IsSet = b.Type == SCTypeCode.Bool2 });
            }
        }

        public override void ExportMissingFlags() { }

        public override void ExportChecklist() { }

        public override void DumpAllFlags()
        {
            StringBuilder sb = new StringBuilder(100 * 1024);
            
            for (int i = 0; i < m_eventFlagsList.Count; ++i)
            {
#if DEBUG
                sb.AppendFormat("FLAG_0x{0:X8} {1}\t{2}\r\n", m_eventFlagsList[i].FlagIdx, m_eventFlagsList[i].IsSet,
                    m_eventFlagsList[i].FlagTypeVal == FlagType._Unused ? "UNUSED" : m_eventFlagsList[i].ToString());
#else
                sb.AppendFormat("FLAG_0x{0:X8} {1}\r\n", m_eventFlagsList[i].FlagIdx, m_eventFlagsList[i].IsSet);
#endif
            }

            System.IO.File.WriteAllText(string.Format("flags_dump_{0}.txt", m_savFile.Version), sb.ToString());
        }

        public override void MarkFlags(FlagType flagType) { }

        public override void UnmarkFlags(FlagType flagType) { }

        public override bool SupportsEditingFlag(FlagType flagType) { return false; }
    }

}
