namespace MissingEventFlagsCheckerPlugin
{

    static class EventFlagTypeExtensions
    {
        public static EventFlagsChecker.EventFlagType Parse(this EventFlagsChecker.EventFlagType _, string txt) => txt switch
        {
            "FIELD ITEM" => EventFlagsChecker.EventFlagType.FieldItem,
            "HIDDEN ITEM" => EventFlagsChecker.EventFlagType.HiddenItem,
            "SPECIAL ITEM" => EventFlagsChecker.EventFlagType.SpecialItem,
            "TRAINER BATTLE" => EventFlagsChecker.EventFlagType.TrainerBattle,
            "STATIC ENCOUNTER" => EventFlagsChecker.EventFlagType.StaticEncounter,
            "IN-GAME TRADE" => EventFlagsChecker.EventFlagType.InGameTrade,
            "ITEM GIFT" => EventFlagsChecker.EventFlagType.ItemGift,
            "PKMN GIFT" => EventFlagsChecker.EventFlagType.PkmnGift,
            "EVENT" => EventFlagsChecker.EventFlagType.GeneralEvent,
            "SIDE EVENT" => EventFlagsChecker.EventFlagType.SideEvent,
            "STORY EVENT" => EventFlagsChecker.EventFlagType.StoryEvent,
            "BERRY TREE" => EventFlagsChecker.EventFlagType.BerryTree,
            "FLY SPOT" => EventFlagsChecker.EventFlagType.FlySpot,
            "COLLECTABLE" => EventFlagsChecker.EventFlagType.Collectable,
            "_UNUSED" => EventFlagsChecker.EventFlagType._Unused,
            "_SEPARATOR" => EventFlagsChecker.EventFlagType._Separator,
            _ => EventFlagsChecker.EventFlagType._Unknown,
        };

        public static string AsText(this EventFlagsChecker.EventFlagType flagType) => flagType switch
        {
            EventFlagsChecker.EventFlagType.FieldItem => "FIELD ITEM",
            EventFlagsChecker.EventFlagType.HiddenItem => "HIDDEN ITEM",
            EventFlagsChecker.EventFlagType.SpecialItem => "SPECIAL ITEM",
            EventFlagsChecker.EventFlagType.TrainerBattle => "TRAINER BATTLE",
            EventFlagsChecker.EventFlagType.StaticEncounter => "STATIC ENCOUNTER",
            EventFlagsChecker.EventFlagType.InGameTrade => "IN-GAME TRADE",
            EventFlagsChecker.EventFlagType.ItemGift => "ITEM GIFT",
            EventFlagsChecker.EventFlagType.PkmnGift => "PKMN GIFT",
            EventFlagsChecker.EventFlagType.GeneralEvent => "EVENT",
            EventFlagsChecker.EventFlagType.SideEvent => "SIDE EVENT",
            EventFlagsChecker.EventFlagType.StoryEvent => "STORY EVENT",
            EventFlagsChecker.EventFlagType.BerryTree => "BERRY TREE",
            EventFlagsChecker.EventFlagType.FlySpot => "FLY SPOT",
            EventFlagsChecker.EventFlagType.Collectable => "COLLECTABLE",
            EventFlagsChecker.EventFlagType._Unused => "_UNUSED",
            _ => "",
        };

        public static string AsLocalizedText(this EventFlagsChecker.EventFlagType flagType)
        {
            return LocalizedStrings.Find($"EventFlagType.{flagType}", flagType.ToString());
        }
    }
}
