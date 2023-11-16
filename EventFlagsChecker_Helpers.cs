using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{

    static class EventFlagTypeExtensions
    {
        public static EventFlagsChecker.EventFlagType Parse(this EventFlagsChecker.EventFlagType flagType, string txt)
        {
            switch (txt)
            {
                case "FIELD ITEM":
                    flagType = EventFlagsChecker.EventFlagType.FieldItem;
                    break;

                case "HIDDEN ITEM":
                    flagType = EventFlagsChecker.EventFlagType.HiddenItem;
                    break;

                case "SPECIAL ITEM":
                    flagType = EventFlagsChecker.EventFlagType.SpecialItem;
                    break;

                case "TRAINER BATTLE":
                    flagType = EventFlagsChecker.EventFlagType.TrainerBattle;
                    break;

                case "STATIC BATTLE":
                    flagType = EventFlagsChecker.EventFlagType.StaticBattle;
                    break;

                case "IN-GAME TRADE":
                    flagType = EventFlagsChecker.EventFlagType.InGameTrade;
                    break;

                case "ITEM GIFT":
                    flagType = EventFlagsChecker.EventFlagType.ItemGift;
                    break;

                case "PKMN GIFT":
                    flagType = EventFlagsChecker.EventFlagType.PkmnGift;
                    break;

                case "EVENT":
                    flagType = EventFlagsChecker.EventFlagType.GeneralEvent;
                    break;

                case "SIDE EVENT":
                    flagType = EventFlagsChecker.EventFlagType.SideEvent;
                    break;

                case "STORY EVENT":
                    flagType = EventFlagsChecker.EventFlagType.StoryEvent;
                    break;

                case "BERRY TREE":
                    flagType = EventFlagsChecker.EventFlagType.BerryTree;
                    break;

                case "FLY SPOT":
                    flagType = EventFlagsChecker.EventFlagType.FlySpot;
                    break;

                case "COLLECTABLE":
                    flagType = EventFlagsChecker.EventFlagType.Collectable;
                    break;

                case "_UNUSED":
                    flagType = EventFlagsChecker.EventFlagType._Unused;
                    break;

                case "_SEPARATOR":
                    flagType = EventFlagsChecker.EventFlagType._Separator;
                    break;

                default:
                    flagType = EventFlagsChecker.EventFlagType._Unknown;
                    break;
            }

            return flagType;
        }

        public static string AsText(this EventFlagsChecker.EventFlagType flagType)
        {
            string flagTypeTxt = "";

            switch (flagType)
            {
                case EventFlagsChecker.EventFlagType.FieldItem:
                    flagTypeTxt = "FIELD ITEM";
                    break;

                case EventFlagsChecker.EventFlagType.HiddenItem:
                    flagTypeTxt = "HIDDEN ITEM";
                    break;

                case EventFlagsChecker.EventFlagType.SpecialItem:
                    flagTypeTxt = "SPECIAL ITEM";
                    break;

                case EventFlagsChecker.EventFlagType.TrainerBattle:
                    flagTypeTxt = "TRAINER BATTLE";
                    break;

                case EventFlagsChecker.EventFlagType.StaticBattle:
                    flagTypeTxt = "STATIC BATTLE";
                    break;

                case EventFlagsChecker.EventFlagType.InGameTrade:
                    flagTypeTxt = "IN-GAME TRADE";
                    break;

                case EventFlagsChecker.EventFlagType.ItemGift:
                    flagTypeTxt = "ITEM GIFT";
                    break;

                case EventFlagsChecker.EventFlagType.PkmnGift:
                    flagTypeTxt = "PKMN GIFT";
                    break;

                case EventFlagsChecker.EventFlagType.GeneralEvent:
                    flagTypeTxt = "EVENT";
                    break;

                case EventFlagsChecker.EventFlagType.SideEvent:
                    flagTypeTxt = "SIDE EVENT";
                    break;

                case EventFlagsChecker.EventFlagType.StoryEvent:
                    flagTypeTxt = "STORY EVENT";
                    break;

                case EventFlagsChecker.EventFlagType.BerryTree:
                    flagTypeTxt = "BERRY TREE";
                    break;

                case EventFlagsChecker.EventFlagType.FlySpot:
                    flagTypeTxt = "FLY SPOT";
                    break;

                case EventFlagsChecker.EventFlagType.Collectable:
                    flagTypeTxt = "COLLECTABLE";
                    break;

                case EventFlagsChecker.EventFlagType._Unused:
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
