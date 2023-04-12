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

                case "STATIONARY BATTLE":
                    flagType = FlagsOrganizer.FlagType.StationaryBattle;
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

                case FlagsOrganizer.FlagType.StationaryBattle:
                    flagTypeTxt = "STATIONARY BATTLE";
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
        }

        public override void ExportMissingFlags() { }

        public override void ExportChecklist() { }

        public override bool SupportsEditingFlag(FlagType flagType) { return false; }
    }


    class DummyOrgBlockFlags : FlagsOrganizer
    {
        Dictionary<uint, bool> m_blockEventFlags;

        protected override void InitFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;

            m_blockEventFlags = new Dictionary<uint, bool>();
            foreach (var b in (m_savFile as ISCBlockArray).AllBlocks)
            {
                if (b.Type == SCTypeCode.Bool1 || b.Type == SCTypeCode.Bool2)
                {
                    m_blockEventFlags.Add(b.Key, (b.Type == SCTypeCode.Bool2));
                }
            }

            m_eventFlagsList.Clear();
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

        public override bool SupportsEditingFlag(FlagType flagType) { return false; }
    }

}
