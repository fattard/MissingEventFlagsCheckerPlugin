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
                    flagType = EventFlagsOrganizer.EventFlagType.StaticBattle;
                    break;

                case "IN-GAME TRADE":
                    flagType = EventFlagsOrganizer.EventFlagType.InGameTrade;
                    break;

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

                case "FLY SPOT":
                    flagType = EventFlagsOrganizer.EventFlagType.FlySpot;
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

                case EventFlagsOrganizer.EventFlagType.FlySpot:
                    flagTypeTxt = "FLY SPOT";
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
}
