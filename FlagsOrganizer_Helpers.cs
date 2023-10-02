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
        public static EventFlagsOrganizer.EventFlagType Parse(this EventFlagsOrganizer.EventFlagType flagType, string txt)
        {
            switch (txt)
            {
                case "FIELD ITEM":
                    flagType = EventFlagsOrganizer.EventFlagType.FieldItem;
                    break;

                case "HIDDEN ITEM":
                    flagType = EventFlagsOrganizer.EventFlagType.HiddenItem;
                    break;

                case "SPECIAL ITEM":
                    flagType = EventFlagsOrganizer.EventFlagType.SpecialItem;
                    break;

                case "TRAINER BATTLE":
                    flagType = EventFlagsOrganizer.EventFlagType.TrainerBattle;
                    break;

                case "STATIC BATTLE":
                case "STATIONARY_BATTLE":
                    flagType = EventFlagsOrganizer.EventFlagType.StaticBattle;
                    break;

                case "IN-GAME TRADE":
                    flagType = EventFlagsOrganizer.EventFlagType.InGameTrade;
                    break;

                case "GIFT":
                case "ITEM GIFT":
                    flagType = EventFlagsOrganizer.EventFlagType.ItemGift;
                    break;

                case "PKMN GIFT":
                    flagType = EventFlagsOrganizer.EventFlagType.PkmnGift;
                    break;

                case "EVENT":
                    flagType = EventFlagsOrganizer.EventFlagType.GeneralEvent;
                    break;

                case "SIDE EVENT":
                    flagType = EventFlagsOrganizer.EventFlagType.SideEvent;
                    break;

                case "STORY EVENT":
                    flagType = EventFlagsOrganizer.EventFlagType.StoryEvent;
                    break;

                case "BERRY TREE":
                    flagType = EventFlagsOrganizer.EventFlagType.BerryTree;
                    break;

                case "COLLECTABLE":
                    flagType = EventFlagsOrganizer.EventFlagType.Collectable;
                    break;

                case "_UNUSED":
                    flagType = EventFlagsOrganizer.EventFlagType._Unused;
                    break;

                case "_SEPARATOR":
                    flagType = EventFlagsOrganizer.EventFlagType._Separator;
                    break;

                default:
                    flagType = EventFlagsOrganizer.EventFlagType._Unknown;
                    break;
            }

            return flagType;
        }

        public static string AsText(this EventFlagsOrganizer.EventFlagType flagType)
        {
            string flagTypeTxt = "";

            switch (flagType)
            {
                case EventFlagsOrganizer.EventFlagType.FieldItem:
                    flagTypeTxt = "FIELD ITEM";
                    break;

                case EventFlagsOrganizer.EventFlagType.HiddenItem:
                    flagTypeTxt = "HIDDEN ITEM";
                    break;

                case EventFlagsOrganizer.EventFlagType.SpecialItem:
                    flagTypeTxt = "SPECIAL ITEM";
                    break;

                case EventFlagsOrganizer.EventFlagType.TrainerBattle:
                    flagTypeTxt = "TRAINER BATTLE";
                    break;

                case EventFlagsOrganizer.EventFlagType.StaticBattle:
                    flagTypeTxt = "STATIC BATTLE";
                    break;

                case EventFlagsOrganizer.EventFlagType.InGameTrade:
                    flagTypeTxt = "IN-GAME TRADE";
                    break;

                case EventFlagsOrganizer.EventFlagType.ItemGift:
                    flagTypeTxt = "ITEM GIFT";
                    break;

                case EventFlagsOrganizer.EventFlagType.PkmnGift:
                    flagTypeTxt = "PKMN GIFT";
                    break;

                case EventFlagsOrganizer.EventFlagType.GeneralEvent:
                    flagTypeTxt = "EVENT";
                    break;

                case EventFlagsOrganizer.EventFlagType.SideEvent:
                    flagTypeTxt = "SIDE EVENT";
                    break;

                case EventFlagsOrganizer.EventFlagType.StoryEvent:
                    flagTypeTxt = "STORY EVENT";
                    break;

                case EventFlagsOrganizer.EventFlagType.BerryTree:
                    flagTypeTxt = "BERRY TREE";
                    break;

                case EventFlagsOrganizer.EventFlagType.Collectable:
                    flagTypeTxt = "COLLECTABLE";
                    break;

                case EventFlagsOrganizer.EventFlagType._Unused:
                    flagTypeTxt = "_UNUSED";
                    break;

                default:
                    flagTypeTxt = "";
                    break;
            }

            return flagTypeTxt;
        }
    }



    class DummyOrg : EventFlagsOrganizer
    {
        protected override void InitEventFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;
            bool[] savEventFlags = (m_savFile as IEventFlagArray).GetEventFlags();
            m_eventFlagsList.Clear();

            for (int i = 0; i < savEventFlags.Length; ++i)
            {
                m_eventFlagsList.Add(new FlagsOrganizer.FlagDetail((uint)i, source: 0, EventFlagType._Unknown, "", "") { IsSet = savEventFlags[i] });
            }
        }

        public override void ExportMissingEvents() { }

        public override void ExportChecklist() { }

        public override void MarkFlags(EventFlagType flagType) { }

        public override void UnmarkFlags(EventFlagType flagType) { }

        public override bool SupportsEditingFlag(EventFlagType flagType) { return false; }
    }


    class DummyOrgBlockFlags : EventFlagsOrganizer
    {
        List<SCBlock> m_blockEventFlags;

        protected override void InitEventFlagsData(SaveFile savFile)
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
                m_eventFlagsList.Add(new FlagsOrganizer.FlagDetail(b.Key, source: 0, EventFlagType._Unknown, "", "") { IsSet = b.Type == SCTypeCode.Bool2 });
            }
        }

        public override void ExportMissingEvents() { }

        public override void ExportChecklist() { }

        public override void DumpAllFlags()
        {
            StringBuilder sb = new StringBuilder(512 * 1024);
            
            for (int i = 0; i < m_eventFlagsList.Count; ++i)
            {
                sb.AppendFormat("FLAG_0x{0:X8} {1}\t{2}\r\n", m_eventFlagsList[i].FlagIdx, m_eventFlagsList[i].IsSet,
                    m_eventFlagsList[i].FlagTypeVal == EventFlagType._Unused ? "UNUSED" : m_eventFlagsList[i].ToString());
            }

            System.IO.File.WriteAllText(string.Format("flags_dump_{0}.txt", m_savFile.Version), sb.ToString());
        }

        public override void MarkFlags(EventFlagType flagType) { }

        public override void UnmarkFlags(EventFlagType flagType) { }

        public override bool SupportsEditingFlag(EventFlagType flagType) { return false; }
    }

}
