using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    static class FlagsGen1Y
    {
        static List<string> s_missingEventFlagsList = new List<string>(4096);

        static bool[] s_obtainedHiddenCoinsFlags;
        static bool[] s_obtainedHiddenItemsFlags;
        static bool[] s_missableObjectFlags;
        static bool[] s_completedInGameTradeFlags;
        static bool[] s_eventFlags;
        static bool s_gotLaprasFlag;

        static SaveFile s_savFile;

        enum FlagOffsets
        {
            MissableObjectFlags = 0x2852,
            ObtainedHiddenItems = 0x299C,
            ObtainedHiddenCoins = 0x29AA,
            LaprasFlag = 0x29DA,
            CompletedInGameTradeFlags = 0x29E3,
            EventFlags = 0x29F3
        }


        static void CheckFlag(bool flagState, string aFlagDetail)
        {
            if (!flagState)
            {
                s_missingEventFlagsList.Add(aFlagDetail);
            }
        }


        public static void ExportFlags(SaveFile savFile)
        {
            s_savFile = savFile;
            s_missingEventFlagsList.Clear();

            InitFlagsData();
            CheckFlags();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s_missingEventFlagsList.Count; ++i)
            {
                sb.AppendFormat("{0}\n", s_missingEventFlagsList[i]);
            }

            System.IO.File.WriteAllText(string.Format("missing_events_{0}.txt", s_savFile.Version), sb.ToString());
        }

        static void InitFlagsData()
        {
            // wMissableObjectIndex
            bool[] result = new bool[32 * 8];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = s_savFile.GetFlag((int)FlagOffsets.MissableObjectFlags + (i >> 3), i & 7);
            }
            s_missableObjectFlags = result;

            // wObtainedHiddenItemsFlags
            result = new bool[112];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = s_savFile.GetFlag((int)FlagOffsets.ObtainedHiddenItems + (i >> 3), i & 7);
            }
            s_obtainedHiddenItemsFlags = result;

            // wObtainedHiddenCoinsFlags
            result = new bool[16];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = s_savFile.GetFlag((int)FlagOffsets.ObtainedHiddenCoins + (i >> 3), i & 7);
            }
            s_obtainedHiddenCoinsFlags = result;

            // wCompletedInGameTradeFlags
            result = new bool[16];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = s_savFile.GetFlag((int)FlagOffsets.CompletedInGameTradeFlags + (i >> 3), i & 7);
            }
            s_completedInGameTradeFlags = result;

            // wd72e
            s_gotLaprasFlag = s_savFile.GetFlag((int)FlagOffsets.LaprasFlag, 0);

            // wEventFlags
            s_eventFlags = (s_savFile as SAV1).GetEventFlags();
        }

        static void CheckFlags()
        {
            // Hidden Coins
            CheckFlag(s_obtainedHiddenCoinsFlags[0], "FLAG_HIDDEN_COINS_GAME_CORNER_0");
            CheckFlag(s_obtainedHiddenCoinsFlags[1], "FLAG_HIDDEN_COINS_GAME_CORNER_1");
            CheckFlag(s_obtainedHiddenCoinsFlags[2], "FLAG_HIDDEN_COINS_GAME_CORNER_2");
            CheckFlag(s_obtainedHiddenCoinsFlags[3], "FLAG_HIDDEN_COINS_GAME_CORNER_3");
            CheckFlag(s_obtainedHiddenCoinsFlags[4], "FLAG_HIDDEN_COINS_GAME_CORNER_4");
            CheckFlag(s_obtainedHiddenCoinsFlags[5], "FLAG_HIDDEN_COINS_GAME_CORNER_5");
            CheckFlag(s_obtainedHiddenCoinsFlags[6], "FLAG_HIDDEN_COINS_GAME_CORNER_6");
            CheckFlag(s_obtainedHiddenCoinsFlags[7], "FLAG_HIDDEN_COINS_GAME_CORNER_7");
            CheckFlag(s_obtainedHiddenCoinsFlags[8], "FLAG_HIDDEN_COINS_GAME_CORNER_8");
            CheckFlag(s_obtainedHiddenCoinsFlags[9], "FLAG_HIDDEN_COINS_GAME_CORNER_9");
            CheckFlag(s_obtainedHiddenCoinsFlags[10], "FLAG_HIDDEN_COINS_GAME_CORNER_10");
            //CheckFlag(s_obtainedHiddenCoinsFlags[11], "FLAG_HIDDEN_COINS_GAME_CORNER_11"); // inaccessible

            // Hidden Items
            CheckFlag(s_obtainedHiddenItemsFlags[0x00], "FLAG_HIDDEN_ITEM_SILPH_CO_5F_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x01], "FLAG_HIDDEN_ITEM_SILPH_CO_9F_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x02], "FLAG_HIDDEN_ITEM_POKEMON_MANSION_3F_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x03], "FLAG_HIDDEN_ITEM_POKEMON_MANSION_B1F_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x04], "FLAG_HIDDEN_ITEM_SAFARI_ZONE_WEST_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x05], "FLAG_HIDDEN_ITEM_CERULEAN_CAVE_2F_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x06], "FLAG_HIDDEN_ITEM_CERULEAN_CAVE_B1F_0");
            //CheckFlag(s_obtainedHiddenItemsFlags[0x07], "FLAG_HIDDEN_ITEM_UNUSED_MAP_6F_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x08], "FLAG_HIDDEN_ITEM_SEAFOAM_ISLANDS_B2F_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x09], "FLAG_HIDDEN_ITEM_SEAFOAM_ISLANDS_B3F_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x0A], "FLAG_HIDDEN_ITEM_SEAFOAM_ISLANDS_B4F_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x0B], "FLAG_HIDDEN_ITEM_VIRIDIAN_FOREST_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x0C], "FLAG_HIDDEN_ITEM_VIRIDIAN_FOREST_1");
            CheckFlag(s_obtainedHiddenItemsFlags[0x0D], "FLAG_HIDDEN_ITEM_MT_MOON_B2F_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x0E], "FLAG_HIDDEN_ITEM_MT_MOON_B2F_1");
            CheckFlag(s_obtainedHiddenItemsFlags[0x0F], "FLAG_HIDDEN_ITEM_SS_ANNE_B1F_ROOMS_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x10], "FLAG_HIDDEN_ITEM_SS_ANNE_KITCHEN_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x11], "FLAG_HIDDEN_ITEM_UNDERGROUND_PATH_NORTH_SOUTH_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x12], "FLAG_HIDDEN_ITEM_UNDERGROUND_PATH_NORTH_SOUTH_1");
            CheckFlag(s_obtainedHiddenItemsFlags[0x13], "FLAG_HIDDEN_ITEM_UNDERGROUND_PATH_WEST_EAST_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x14], "FLAG_HIDDEN_ITEM_UNDERGROUND_PATH_WEST_EAST_1");
            CheckFlag(s_obtainedHiddenItemsFlags[0x15], "FLAG_HIDDEN_ITEM_ROCKET_HIDEOUT_B1F_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x16], "FLAG_HIDDEN_ITEM_ROCKET_HIDEOUT_B3F_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x17], "FLAG_HIDDEN_ITEM_ROCKET_HIDEOUT_B4F_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x18], "FLAG_HIDDEN_ITEM_ROUTE_10_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x19], "FLAG_HIDDEN_ITEM_ROUTE_10_1");
            CheckFlag(s_obtainedHiddenItemsFlags[0x1A], "FLAG_HIDDEN_ITEM_POWER_PLANT_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x1B], "FLAG_HIDDEN_ITEM_POWER_PLANT_1");
            CheckFlag(s_obtainedHiddenItemsFlags[0x1C], "FLAG_HIDDEN_ITEM_ROUTE_11_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x1D], "FLAG_HIDDEN_ITEM_ROUTE_12_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x1E], "FLAG_HIDDEN_ITEM_ROUTE_13_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x1F], "FLAG_HIDDEN_ITEM_ROUTE_13_1");
            CheckFlag(s_obtainedHiddenItemsFlags[0x20], "FLAG_HIDDEN_ITEM_ROUTE_17_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x21], "FLAG_HIDDEN_ITEM_ROUTE_17_1");
            CheckFlag(s_obtainedHiddenItemsFlags[0x22], "FLAG_HIDDEN_ITEM_ROUTE_17_2");
            CheckFlag(s_obtainedHiddenItemsFlags[0x23], "FLAG_HIDDEN_ITEM_ROUTE_17_3");
            CheckFlag(s_obtainedHiddenItemsFlags[0x24], "FLAG_HIDDEN_ITEM_ROUTE_17_4");
            CheckFlag(s_obtainedHiddenItemsFlags[0x25], "FLAG_HIDDEN_ITEM_ROUTE_23_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x26], "FLAG_HIDDEN_ITEM_ROUTE_23_1");
            CheckFlag(s_obtainedHiddenItemsFlags[0x27], "FLAG_HIDDEN_ITEM_ROUTE_23_2");
            CheckFlag(s_obtainedHiddenItemsFlags[0x28], "FLAG_HIDDEN_ITEM_VICTORY_ROAD_2F_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x29], "FLAG_HIDDEN_ITEM_VICTORY_ROAD_2F_1");
            CheckFlag(s_obtainedHiddenItemsFlags[0x2A], "FLAG_HIDDEN_ITEM_ROUTE_25_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x2B], "FLAG_HIDDEN_ITEM_ROUTE_25_1");
            CheckFlag(s_obtainedHiddenItemsFlags[0x2C], "FLAG_HIDDEN_ITEM_ROUTE_4_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x2D], "FLAG_HIDDEN_ITEM_ROUTE_9_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x2E], "FLAG_HIDDEN_ITEM_COPYCATS_HOUSE_2F_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x2F], "FLAG_HIDDEN_ITEM_VIRIDIAN_CITY_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x30], "FLAG_HIDDEN_ITEM_CERULEAN_CITY_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x31], "FLAG_HIDDEN_ITEM_CERULEAN_CAVE_1F_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x32], "FLAG_HIDDEN_ITEM_POKEMON_TOWER_5F_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x33], "FLAG_HIDDEN_ITEM_VERMILION_CITY_0");
            CheckFlag(s_obtainedHiddenItemsFlags[0x34], "FLAG_HIDDEN_ITEM_CELADON_CITY_0");
            //CheckFlag(s_obtainedHiddenItemsFlags[0x35], "FLAG_HIDDEN_ITEM_SAFARI_ZONE_GATE_0"); // inaccessible
            CheckFlag(s_obtainedHiddenItemsFlags[0x36], "FLAG_HIDDEN_ITEM_POKEMON_MANSION_1F_0");

            // Normal Items
            CheckFlag(s_missableObjectFlags[0x1A], "FLAG_ITEM_ROUTE_2_ITEM_1");
            CheckFlag(s_missableObjectFlags[0x1B], "FLAG_ITEM_ROUTE_2_ITEM_2");
            CheckFlag(s_missableObjectFlags[0x1C], "FLAG_ITEM_ROUTE_4_ITEM");
            CheckFlag(s_missableObjectFlags[0x1D], "FLAG_ITEM_ROUTE_9_ITEM");
            CheckFlag(s_missableObjectFlags[0x1F], "FLAG_ITEM_ROUTE_12_ITEM_1");
            CheckFlag(s_missableObjectFlags[0x20], "FLAG_ITEM_ROUTE_12_ITEM_2");
            CheckFlag(s_missableObjectFlags[0x21], "FLAG_ITEM_ROUTE_15_ITEM");
            CheckFlag(s_missableObjectFlags[0x26], "FLAG_ITEM_ROUTE_24_ITEM");
            CheckFlag(s_missableObjectFlags[0x27], "FLAG_ITEM_ROUTE_25_ITEM");
            CheckFlag(s_missableObjectFlags[0x32], "FLAG_ITEM_VIRIDIAN_GYM_ITEM");
            CheckFlag(s_missableObjectFlags[0x35], "FLAG_ITEM_CERULEAN_CAVE_1F_ITEM_1");
            CheckFlag(s_missableObjectFlags[0x36], "FLAG_ITEM_CERULEAN_CAVE_1F_ITEM_2");
            CheckFlag(s_missableObjectFlags[0x37], "FLAG_ITEM_CERULEAN_CAVE_1F_ITEM_3");
            CheckFlag(s_missableObjectFlags[0x38], "FLAG_ITEM_CERULEAN_CAVE_1F_ITEM_4");
            CheckFlag(s_missableObjectFlags[0x3A], "FLAG_ITEM_POKEMON_TOWER_3F_ITEM");
            CheckFlag(s_missableObjectFlags[0x3B], "FLAG_ITEM_POKEMON_TOWER_4F_ITEM_1");
            CheckFlag(s_missableObjectFlags[0x3C], "FLAG_ITEM_POKEMON_TOWER_4F_ITEM_2");
            CheckFlag(s_missableObjectFlags[0x3D], "FLAG_ITEM_POKEMON_TOWER_4F_ITEM_3");
            CheckFlag(s_missableObjectFlags[0x3E], "FLAG_ITEM_POKEMON_TOWER_5F_ITEM");
            CheckFlag(s_missableObjectFlags[0x3F], "FLAG_ITEM_POKEMON_TOWER_6F_ITEM_1");
            CheckFlag(s_missableObjectFlags[0x40], "FLAG_ITEM_POKEMON_TOWER_6F_ITEM_2");
            CheckFlag(s_missableObjectFlags[0x47], "FLAG_ITEM_WARDENS_HOUSE_ITEM");
            CheckFlag(s_missableObjectFlags[0x48], "FLAG_ITEM_POKEMON_MANSION_1F_ITEM_1");
            CheckFlag(s_missableObjectFlags[0x49], "FLAG_ITEM_POKEMON_MANSION_1F_ITEM_2");
            CheckFlag(s_missableObjectFlags[0x56], "FLAG_ITEM_POWER_PLANT_ITEM_1");
            CheckFlag(s_missableObjectFlags[0x57], "FLAG_ITEM_POWER_PLANT_ITEM_2");
            CheckFlag(s_missableObjectFlags[0x58], "FLAG_ITEM_POWER_PLANT_ITEM_3");
            CheckFlag(s_missableObjectFlags[0x59], "FLAG_ITEM_POWER_PLANT_ITEM_4");
            CheckFlag(s_missableObjectFlags[0x5A], "FLAG_ITEM_POWER_PLANT_ITEM_5");
            CheckFlag(s_missableObjectFlags[0x5C], "FLAG_ITEM_VICTORY_ROAD_2F_ITEM_1");
            CheckFlag(s_missableObjectFlags[0x5D], "FLAG_ITEM_VICTORY_ROAD_2F_ITEM_2");
            CheckFlag(s_missableObjectFlags[0x5E], "FLAG_ITEM_VICTORY_ROAD_2F_ITEM_3");
            CheckFlag(s_missableObjectFlags[0x5F], "FLAG_ITEM_VICTORY_ROAD_2F_ITEM_4");
            CheckFlag(s_missableObjectFlags[0x64], "FLAG_ITEM_VIRIDIAN_FOREST_ITEM_1");
            CheckFlag(s_missableObjectFlags[0x65], "FLAG_ITEM_VIRIDIAN_FOREST_ITEM_2");
            CheckFlag(s_missableObjectFlags[0x66], "FLAG_ITEM_VIRIDIAN_FOREST_ITEM_3");
            CheckFlag(s_missableObjectFlags[0x67], "FLAG_ITEM_MT_MOON_1F_ITEM_1");
            CheckFlag(s_missableObjectFlags[0x68], "FLAG_ITEM_MT_MOON_1F_ITEM_2");
            CheckFlag(s_missableObjectFlags[0x69], "FLAG_ITEM_MT_MOON_1F_ITEM_3");
            CheckFlag(s_missableObjectFlags[0x6A], "FLAG_ITEM_MT_MOON_1F_ITEM_4");
            CheckFlag(s_missableObjectFlags[0x6B], "FLAG_ITEM_MT_MOON_1F_ITEM_5");
            CheckFlag(s_missableObjectFlags[0x6C], "FLAG_ITEM_MT_MOON_1F_ITEM_6");
            CheckFlag(s_missableObjectFlags[0x71], "FLAG_ITEM_MT_MOON_B2F_ITEM_1");
            CheckFlag(s_missableObjectFlags[0x72], "FLAG_ITEM_MT_MOON_B2F_ITEM_2");
            CheckFlag(s_missableObjectFlags[0x74], "FLAG_ITEM_SS_ANNE_1F_ROOMS_ITEM");
            CheckFlag(s_missableObjectFlags[0x75], "FLAG_ITEM_SS_ANNE_2F_ROOMS_ITEM_1");
            CheckFlag(s_missableObjectFlags[0x76], "FLAG_ITEM_SS_ANNE_2F_ROOMS_ITEM_2");
            CheckFlag(s_missableObjectFlags[0x77], "FLAG_ITEM_SS_ANNE_B1F_ROOMS_ITEM_1");
            CheckFlag(s_missableObjectFlags[0x78], "FLAG_ITEM_SS_ANNE_B1F_ROOMS_ITEM_2");
            CheckFlag(s_missableObjectFlags[0x79], "FLAG_ITEM_SS_ANNE_B1F_ROOMS_ITEM_3");
            CheckFlag(s_missableObjectFlags[0x7A], "FLAG_ITEM_VICTORY_ROAD_3F_ITEM_1");
            CheckFlag(s_missableObjectFlags[0x7B], "FLAG_ITEM_VICTORY_ROAD_3F_ITEM_2");
            CheckFlag(s_missableObjectFlags[0x7D], "FLAG_ITEM_ROCKET_HIDEOUT_B1F_ITEM_1");
            CheckFlag(s_missableObjectFlags[0x7E], "FLAG_ITEM_ROCKET_HIDEOUT_B1F_ITEM_2");
            CheckFlag(s_missableObjectFlags[0x7F], "FLAG_ITEM_ROCKET_HIDEOUT_B2F_ITEM_1");
            CheckFlag(s_missableObjectFlags[0x80], "FLAG_ITEM_ROCKET_HIDEOUT_B2F_ITEM_2");
            CheckFlag(s_missableObjectFlags[0x81], "FLAG_ITEM_ROCKET_HIDEOUT_B2F_ITEM_3");
            CheckFlag(s_missableObjectFlags[0x82], "FLAG_ITEM_ROCKET_HIDEOUT_B2F_ITEM_4");
            CheckFlag(s_missableObjectFlags[0x83], "FLAG_ITEM_ROCKET_HIDEOUT_B3F_ITEM_1");
            CheckFlag(s_missableObjectFlags[0x84], "FLAG_ITEM_ROCKET_HIDEOUT_B3F_ITEM_2");
            CheckFlag(s_missableObjectFlags[0x88], "FLAG_ITEM_ROCKET_HIDEOUT_B4F_ITEM_1");
            CheckFlag(s_missableObjectFlags[0x89], "FLAG_ITEM_ROCKET_HIDEOUT_B4F_ITEM_2");
            CheckFlag(s_missableObjectFlags[0x8A], "FLAG_ITEM_ROCKET_HIDEOUT_B4F_ITEM_3");
            CheckFlag(s_missableObjectFlags[0x8B], "FLAG_ITEM_ROCKET_HIDEOUT_B4F_ITEM_4");
            CheckFlag(s_missableObjectFlags[0x8C], "FLAG_ITEM_ROCKET_HIDEOUT_B4F_ITEM_5");
            CheckFlag(s_missableObjectFlags[0x94], "FLAG_ITEM_SILPH_CO_3F_ITEM");
            CheckFlag(s_missableObjectFlags[0x98], "FLAG_ITEM_SILPH_CO_4F_ITEM_1");
            CheckFlag(s_missableObjectFlags[0x99], "FLAG_ITEM_SILPH_CO_4F_ITEM_2");
            CheckFlag(s_missableObjectFlags[0x9A], "FLAG_ITEM_SILPH_CO_4F_ITEM_3");
            CheckFlag(s_missableObjectFlags[0x9F], "FLAG_ITEM_SILPH_CO_5F_ITEM_1");
            CheckFlag(s_missableObjectFlags[0xA0], "FLAG_ITEM_SILPH_CO_5F_ITEM_2");
            CheckFlag(s_missableObjectFlags[0xA1], "FLAG_ITEM_SILPH_CO_5F_ITEM_3");
            CheckFlag(s_missableObjectFlags[0xA5], "FLAG_ITEM_SILPH_CO_6F_ITEM_1");
            CheckFlag(s_missableObjectFlags[0xA6], "FLAG_ITEM_SILPH_CO_6F_ITEM_2");
            CheckFlag(s_missableObjectFlags[0xAC], "FLAG_ITEM_SILPH_CO_7F_ITEM_1");
            CheckFlag(s_missableObjectFlags[0xAD], "FLAG_ITEM_SILPH_CO_7F_ITEM_2");
            CheckFlag(s_missableObjectFlags[0xB8], "FLAG_ITEM_SILPH_CO_10F_ITEM_1");
            CheckFlag(s_missableObjectFlags[0xB9], "FLAG_ITEM_SILPH_CO_10F_ITEM_2");
            CheckFlag(s_missableObjectFlags[0xBA], "FLAG_ITEM_SILPH_CO_10F_ITEM_3");
            CheckFlag(s_missableObjectFlags[0xC0], "FLAG_ITEM_POKEMON_MANSION_2F_ITEM");
            CheckFlag(s_missableObjectFlags[0xC1], "FLAG_ITEM_POKEMON_MANSION_3F_ITEM_1");
            CheckFlag(s_missableObjectFlags[0xC2], "FLAG_ITEM_POKEMON_MANSION_3F_ITEM_2");
            CheckFlag(s_missableObjectFlags[0xC3], "FLAG_ITEM_POKEMON_MANSION_B1F_ITEM_1");
            CheckFlag(s_missableObjectFlags[0xC4], "FLAG_ITEM_POKEMON_MANSION_B1F_ITEM_2");
            CheckFlag(s_missableObjectFlags[0xC5], "FLAG_ITEM_POKEMON_MANSION_B1F_ITEM_3");
            CheckFlag(s_missableObjectFlags[0xC6], "FLAG_ITEM_POKEMON_MANSION_B1F_ITEM_4");
            CheckFlag(s_missableObjectFlags[0xC7], "FLAG_ITEM_POKEMON_MANSION_B1F_ITEM_5");
            CheckFlag(s_missableObjectFlags[0xC8], "FLAG_ITEM_SAFARI_ZONE_EAST_ITEM_1");
            CheckFlag(s_missableObjectFlags[0xC9], "FLAG_ITEM_SAFARI_ZONE_EAST_ITEM_2");
            CheckFlag(s_missableObjectFlags[0xCA], "FLAG_ITEM_SAFARI_ZONE_EAST_ITEM_3");
            CheckFlag(s_missableObjectFlags[0xCB], "FLAG_ITEM_SAFARI_ZONE_EAST_ITEM_4");
            CheckFlag(s_missableObjectFlags[0xCC], "FLAG_ITEM_SAFARI_ZONE_NORTH_ITEM_1");
            CheckFlag(s_missableObjectFlags[0xCD], "FLAG_ITEM_SAFARI_ZONE_NORTH_ITEM_2");
            CheckFlag(s_missableObjectFlags[0xCE], "FLAG_ITEM_SAFARI_ZONE_WEST_ITEM_1");
            CheckFlag(s_missableObjectFlags[0xCF], "FLAG_ITEM_SAFARI_ZONE_WEST_ITEM_2");
            CheckFlag(s_missableObjectFlags[0xD0], "FLAG_ITEM_SAFARI_ZONE_WEST_ITEM_3");
            CheckFlag(s_missableObjectFlags[0xD1], "FLAG_ITEM_SAFARI_ZONE_WEST_ITEM_4");
            CheckFlag(s_missableObjectFlags[0xD2], "FLAG_ITEM_SAFARI_ZONE_CENTER_ITEM");
            CheckFlag(s_missableObjectFlags[0xD3], "FLAG_ITEM_CERULEAN_CAVE_2F_ITEM_1");
            CheckFlag(s_missableObjectFlags[0xD4], "FLAG_ITEM_CERULEAN_CAVE_2F_ITEM_2");
            CheckFlag(s_missableObjectFlags[0xD5], "FLAG_ITEM_CERULEAN_CAVE_2F_ITEM_3");
            CheckFlag(s_missableObjectFlags[0xD6], "FLAG_ITEM_CERULEAN_CAVE_2F_ITEM_4");
            CheckFlag(s_missableObjectFlags[0xD8], "FLAG_ITEM_CERULEAN_CAVE_B1F_ITEM_1");
            CheckFlag(s_missableObjectFlags[0xD9], "FLAG_ITEM_CERULEAN_CAVE_B1F_ITEM_2");
            CheckFlag(s_missableObjectFlags[0xDA], "FLAG_ITEM_CERULEAN_CAVE_B1F_ITEM_3");
            CheckFlag(s_missableObjectFlags[0xDB], "FLAG_ITEM_CERULEAN_CAVE_B1F_ITEM_4");
            CheckFlag(s_missableObjectFlags[0xDC], "FLAG_ITEM_VICTORY_ROAD_1F_ITEM_1");
            CheckFlag(s_missableObjectFlags[0xDD], "FLAG_ITEM_VICTORY_ROAD_1F_ITEM_2");

            // In Game Trades
            CheckFlag(s_completedInGameTradeFlags[0x00], "TRADE_FOR_GURIO");
            CheckFlag(s_completedInGameTradeFlags[0x01], "TRADE_FOR_MILES");
            //CheckFlag(s_completedInGameTradeFlags[0x02], "TRADE_FOR_STINGER"); // unused
            CheckFlag(s_completedInGameTradeFlags[0x03], "TRADE_FOR_STICKY");
            //CheckFlag(s_completedInGameTradeFlags[0x04], "TRADE_FOR_BART"); // unused
            CheckFlag(s_completedInGameTradeFlags[0x05], "TRADE_FOR_SPIKE");
            CheckFlag(s_completedInGameTradeFlags[0x06], "TRADE_FOR_MARTY");
            CheckFlag(s_completedInGameTradeFlags[0x07], "TRADE_FOR_BUFFY");
            CheckFlag(s_completedInGameTradeFlags[0x08], "TRADE_FOR_CEZANNE");
            CheckFlag(s_completedInGameTradeFlags[0x09], "TRADE_FOR_RICKY");

            // Other events
            CheckFlag(s_missableObjectFlags[0x45], "EVENT_CELADON_MANSION_EEVEE_GIFT");
            CheckFlag(s_gotLaprasFlag, "EVENT_SILPH_CO_7F_LAPRAS_GIFT");

            CheckFlag(s_eventFlags[0x018], "EVENT_GOT_TOWN_MAP");
            CheckFlag(s_eventFlags[0x022], "EVENT_GOT_STARTER");
            CheckFlag(s_eventFlags[0x023], "EVENT_BATTLED_RIVAL_IN_OAKS_LAB");
            CheckFlag(s_eventFlags[0x025], "EVENT_GOT_POKEDEX");

            CheckFlag(s_eventFlags[0x029], "EVENT_GOT_TM42");
            CheckFlag(s_eventFlags[0x038], "EVENT_GOT_POKEBALLS_FROM_OAK"); // EVENT_OAK_GOT_PARCEL
            CheckFlag(s_eventFlags[0x039], "EVENT_GOT_OAKS_PARCEL");
            CheckFlag(s_eventFlags[0x050], "EVENT_GOT_TM27");
            CheckFlag(s_eventFlags[0x051], "EVENT_BEAT_VIRIDIAN_GYM_GIOVANNI");
            CheckFlag(s_eventFlags[0x052], "EVENT_BEAT_VIRIDIAN_GYM_TRAINER_0");
            CheckFlag(s_eventFlags[0x053], "EVENT_BEAT_VIRIDIAN_GYM_TRAINER_1");
            CheckFlag(s_eventFlags[0x054], "EVENT_BEAT_VIRIDIAN_GYM_TRAINER_2");
            CheckFlag(s_eventFlags[0x055], "EVENT_BEAT_VIRIDIAN_GYM_TRAINER_3");
            CheckFlag(s_eventFlags[0x056], "EVENT_BEAT_VIRIDIAN_GYM_TRAINER_4");
            CheckFlag(s_eventFlags[0x057], "EVENT_BEAT_VIRIDIAN_GYM_TRAINER_5");
            CheckFlag(s_eventFlags[0x058], "EVENT_BEAT_VIRIDIAN_GYM_TRAINER_6");
            CheckFlag(s_eventFlags[0x059], "EVENT_BEAT_VIRIDIAN_GYM_TRAINER_7");

            CheckFlag(s_eventFlags[0x069], "EVENT_GOT_OLD_AMBER");
            CheckFlag(s_eventFlags[0x072], "EVENT_BEAT_PEWTER_GYM_TRAINER_0");
            CheckFlag(s_eventFlags[0x076], "EVENT_GOT_TM34");
            CheckFlag(s_eventFlags[0x077], "EVENT_BEAT_BROCK");

            CheckFlag(s_eventFlags[0x098], "EVENT_BEAT_CERULEAN_RIVAL");
            CheckFlag(s_eventFlags[0x0A7], "EVENT_BEAT_CERULEAN_ROCKET_THIEF");
            CheckFlag(s_eventFlags[0x0A8], "EVENT_GOT_BULBASAUR_IN_CERULEAN");
            CheckFlag(s_eventFlags[0x0BA], "EVENT_BEAT_CERULEAN_GYM_TRAINER_0");
            CheckFlag(s_eventFlags[0x0BB], "EVENT_BEAT_CERULEAN_GYM_TRAINER_1");
            CheckFlag(s_eventFlags[0x0BE], "EVENT_GOT_TM11");
            CheckFlag(s_eventFlags[0x0BF], "EVENT_BEAT_MISTY");
            CheckFlag(s_eventFlags[0x0C0], "EVENT_GOT_BICYCLE");

            CheckFlag(s_eventFlags[0x0EF], "EVENT_BEAT_POKEMON_TOWER_RIVAL");
            CheckFlag(s_eventFlags[0x0F1], "EVENT_BEAT_POKEMONTOWER_3_TRAINER_0");
            CheckFlag(s_eventFlags[0x0F2], "EVENT_BEAT_POKEMONTOWER_3_TRAINER_1");
            CheckFlag(s_eventFlags[0x0F3], "EVENT_BEAT_POKEMONTOWER_3_TRAINER_2");
            CheckFlag(s_eventFlags[0x0F9], "EVENT_BEAT_POKEMONTOWER_4_TRAINER_0");
            CheckFlag(s_eventFlags[0x0FA], "EVENT_BEAT_POKEMONTOWER_4_TRAINER_1");
            CheckFlag(s_eventFlags[0x0FB], "EVENT_BEAT_POKEMONTOWER_4_TRAINER_2");
            CheckFlag(s_eventFlags[0x102], "EVENT_BEAT_POKEMONTOWER_5_TRAINER_0");
            CheckFlag(s_eventFlags[0x103], "EVENT_BEAT_POKEMONTOWER_5_TRAINER_1");
            CheckFlag(s_eventFlags[0x104], "EVENT_BEAT_POKEMONTOWER_5_TRAINER_2");
            CheckFlag(s_eventFlags[0x105], "EVENT_BEAT_POKEMONTOWER_5_TRAINER_3");
            CheckFlag(s_eventFlags[0x109], "EVENT_BEAT_POKEMONTOWER_6_TRAINER_0");
            CheckFlag(s_eventFlags[0x10A], "EVENT_BEAT_POKEMONTOWER_6_TRAINER_1");
            CheckFlag(s_eventFlags[0x10B], "EVENT_BEAT_POKEMONTOWER_6_TRAINER_2");
            CheckFlag(s_eventFlags[0x10F], "EVENT_BEAT_GHOST_MAROWAK");
            CheckFlag(s_eventFlags[0x111], "EVENT_BEAT_POKEMONTOWER_7_TRAINER_0");
            CheckFlag(s_eventFlags[0x112], "EVENT_BEAT_POKEMONTOWER_7_TRAINER_1");
            CheckFlag(s_eventFlags[0x113], "EVENT_BEAT_POKEMONTOWER_7_TRAINER_2");
            CheckFlag(s_eventFlags[0x128], "EVENT_GOT_POKE_FLUTE");
            CheckFlag(s_eventFlags[0x147], "EVENT_GOT_SQUIRTLE_FROM_OFFICER_JENNY");

            CheckFlag(s_eventFlags[0x151], "EVENT_GOT_BIKE_VOUCHER");
            CheckFlag(s_eventFlags[0x162], "EVENT_BEAT_VERMILION_GYM_TRAINER_0");
            CheckFlag(s_eventFlags[0x163], "EVENT_BEAT_VERMILION_GYM_TRAINER_1");
            CheckFlag(s_eventFlags[0x164], "EVENT_BEAT_VERMILION_GYM_TRAINER_2");
            CheckFlag(s_eventFlags[0x166], "EVENT_GOT_TM24");
            CheckFlag(s_eventFlags[0x167], "EVENT_BEAT_LT_SURGE");

            CheckFlag(s_eventFlags[0x180], "EVENT_GOT_TM41");
            CheckFlag(s_eventFlags[0x18C], "EVENT_GOT_TM13");
            CheckFlag(s_eventFlags[0x18D], "EVENT_GOT_TM48");
            CheckFlag(s_eventFlags[0x18E], "EVENT_GOT_TM49");
            CheckFlag(s_eventFlags[0x18F], "EVENT_GOT_TM18");
            CheckFlag(s_eventFlags[0x1A8], "EVENT_GOT_TM21");
            CheckFlag(s_eventFlags[0x1A9], "EVENT_BEAT_ERIKA");
            CheckFlag(s_eventFlags[0x1AA], "EVENT_BEAT_CELADON_GYM_TRAINER_0");
            CheckFlag(s_eventFlags[0x1AB], "EVENT_BEAT_CELADON_GYM_TRAINER_1");
            CheckFlag(s_eventFlags[0x1AC], "EVENT_BEAT_CELADON_GYM_TRAINER_2");
            CheckFlag(s_eventFlags[0x1AD], "EVENT_BEAT_CELADON_GYM_TRAINER_3");
            CheckFlag(s_eventFlags[0x1AE], "EVENT_BEAT_CELADON_GYM_TRAINER_4");
            CheckFlag(s_eventFlags[0x1AF], "EVENT_BEAT_CELADON_GYM_TRAINER_5");
            CheckFlag(s_eventFlags[0x1B0], "EVENT_BEAT_CELADON_GYM_TRAINER_6");
            CheckFlag(s_eventFlags[0x1BA], "EVENT_GOT_10_COINS");
            CheckFlag(s_eventFlags[0x1BB], "EVENT_GOT_20_COINS");
            CheckFlag(s_eventFlags[0x1BC], "EVENT_GOT_20_COINS_2");
            CheckFlag(s_eventFlags[0x1E0], "EVENT_GOT_COIN_CASE");

            CheckFlag(s_eventFlags[0x238], "EVENT_GOT_HM04");
            CheckFlag(s_eventFlags[0x258], "EVENT_GOT_TM06");
            CheckFlag(s_eventFlags[0x259], "EVENT_BEAT_KOGA");
            CheckFlag(s_eventFlags[0x25A], "EVENT_BEAT_FUCHSIA_GYM_TRAINER_0");
            CheckFlag(s_eventFlags[0x25B], "EVENT_BEAT_FUCHSIA_GYM_TRAINER_1");
            CheckFlag(s_eventFlags[0x25C], "EVENT_BEAT_FUCHSIA_GYM_TRAINER_2");
            CheckFlag(s_eventFlags[0x25D], "EVENT_BEAT_FUCHSIA_GYM_TRAINER_3");
            CheckFlag(s_eventFlags[0x25E], "EVENT_BEAT_FUCHSIA_GYM_TRAINER_4");
            CheckFlag(s_eventFlags[0x25F], "EVENT_BEAT_FUCHSIA_GYM_TRAINER_5");

            CheckFlag(s_eventFlags[0x289], "EVENT_BEAT_MANSION_1_TRAINER_0");
            CheckFlag(s_eventFlags[0x298], "EVENT_GOT_TM38");
            CheckFlag(s_eventFlags[0x299], "EVENT_BEAT_BLAINE");
            CheckFlag(s_eventFlags[0x29A], "EVENT_BEAT_CINNABAR_GYM_TRAINER_0");
            CheckFlag(s_eventFlags[0x29B], "EVENT_BEAT_CINNABAR_GYM_TRAINER_1");
            CheckFlag(s_eventFlags[0x29C], "EVENT_BEAT_CINNABAR_GYM_TRAINER_2");
            CheckFlag(s_eventFlags[0x29D], "EVENT_BEAT_CINNABAR_GYM_TRAINER_3");
            CheckFlag(s_eventFlags[0x29E], "EVENT_BEAT_CINNABAR_GYM_TRAINER_4");
            CheckFlag(s_eventFlags[0x29F], "EVENT_BEAT_CINNABAR_GYM_TRAINER_5");
            CheckFlag(s_eventFlags[0x2A0], "EVENT_BEAT_CINNABAR_GYM_TRAINER_6");
            CheckFlag(s_eventFlags[0x2D7], "EVENT_GOT_TM35");

            CheckFlag(s_eventFlags[0x340], "EVENT_GOT_TM31");
            CheckFlag(s_eventFlags[0x351], "EVENT_BEAT_KARATE_MASTER");
            CheckFlag(s_eventFlags[0x352], "EVENT_BEAT_FIGHTING_DOJO_TRAINER_0");
            CheckFlag(s_eventFlags[0x353], "EVENT_BEAT_FIGHTING_DOJO_TRAINER_1");
            CheckFlag(s_eventFlags[0x354], "EVENT_BEAT_FIGHTING_DOJO_TRAINER_2");
            CheckFlag(s_eventFlags[0x355], "EVENT_BEAT_FIGHTING_DOJO_TRAINER_3");

            if (!s_eventFlags[0x356] && !s_eventFlags[0x357])
            {
                //CheckFlag(s_eventFlags[0x356], "EVENT_GOT_HITMONLEE");
                //CheckFlag(s_eventFlags[0x357], "EVENT_GOT_HITMONCHAN");

                CheckFlag(s_eventFlags[0x356], "EVENT_GOT_HITMONLEE/HITMONCHAN");
            }

            
            CheckFlag(s_eventFlags[0x360], "EVENT_GOT_TM46");
            CheckFlag(s_eventFlags[0x361], "EVENT_BEAT_SABRINA");
            CheckFlag(s_eventFlags[0x362], "EVENT_BEAT_SAFFRON_GYM_TRAINER_0");
            CheckFlag(s_eventFlags[0x363], "EVENT_BEAT_SAFFRON_GYM_TRAINER_1");
            CheckFlag(s_eventFlags[0x364], "EVENT_BEAT_SAFFRON_GYM_TRAINER_2");
            CheckFlag(s_eventFlags[0x365], "EVENT_BEAT_SAFFRON_GYM_TRAINER_3");
            CheckFlag(s_eventFlags[0x366], "EVENT_BEAT_SAFFRON_GYM_TRAINER_4");
            CheckFlag(s_eventFlags[0x367], "EVENT_BEAT_SAFFRON_GYM_TRAINER_5");
            CheckFlag(s_eventFlags[0x368], "EVENT_BEAT_SAFFRON_GYM_TRAINER_6");
            CheckFlag(s_eventFlags[0x3B0], "EVENT_GOT_TM29");

            CheckFlag(s_eventFlags[0x3C0], "EVENT_GOT_POTION_SAMPLE");

            CheckFlag(s_eventFlags[0x3D8], "EVENT_GOT_HM05");

            CheckFlag(s_eventFlags[0x3E2], "EVENT_BEAT_ROUTE_3_TRAINER_0");
            CheckFlag(s_eventFlags[0x3E3], "EVENT_BEAT_ROUTE_3_TRAINER_1");
            CheckFlag(s_eventFlags[0x3E4], "EVENT_BEAT_ROUTE_3_TRAINER_2");
            CheckFlag(s_eventFlags[0x3E5], "EVENT_BEAT_ROUTE_3_TRAINER_3");
            CheckFlag(s_eventFlags[0x3E6], "EVENT_BEAT_ROUTE_3_TRAINER_4");
            CheckFlag(s_eventFlags[0x3E7], "EVENT_BEAT_ROUTE_3_TRAINER_5");
            CheckFlag(s_eventFlags[0x3E8], "EVENT_BEAT_ROUTE_3_TRAINER_6");
            CheckFlag(s_eventFlags[0x3E9], "EVENT_BEAT_ROUTE_3_TRAINER_7");

            CheckFlag(s_eventFlags[0x3F2], "EVENT_BEAT_ROUTE_4_TRAINER_0");
            CheckFlag(s_eventFlags[0x3FF], "EVENT_BOUGHT_MAGIKARP");

            CheckFlag(s_eventFlags[0x411], "EVENT_BEAT_ROUTE_6_TRAINER_0");
            CheckFlag(s_eventFlags[0x412], "EVENT_BEAT_ROUTE_6_TRAINER_1");
            CheckFlag(s_eventFlags[0x413], "EVENT_BEAT_ROUTE_6_TRAINER_2");
            CheckFlag(s_eventFlags[0x414], "EVENT_BEAT_ROUTE_6_TRAINER_3");
            CheckFlag(s_eventFlags[0x415], "EVENT_BEAT_ROUTE_6_TRAINER_4");
            CheckFlag(s_eventFlags[0x416], "EVENT_BEAT_ROUTE_6_TRAINER_5");

            CheckFlag(s_eventFlags[0x431], "EVENT_BEAT_ROUTE_8_TRAINER_0");
            CheckFlag(s_eventFlags[0x432], "EVENT_BEAT_ROUTE_8_TRAINER_1");
            CheckFlag(s_eventFlags[0x433], "EVENT_BEAT_ROUTE_8_TRAINER_2");
            CheckFlag(s_eventFlags[0x434], "EVENT_BEAT_ROUTE_8_TRAINER_3");
            CheckFlag(s_eventFlags[0x435], "EVENT_BEAT_ROUTE_8_TRAINER_4");
            CheckFlag(s_eventFlags[0x436], "EVENT_BEAT_ROUTE_8_TRAINER_5");
            CheckFlag(s_eventFlags[0x437], "EVENT_BEAT_ROUTE_8_TRAINER_6");
            CheckFlag(s_eventFlags[0x438], "EVENT_BEAT_ROUTE_8_TRAINER_7");
            CheckFlag(s_eventFlags[0x439], "EVENT_BEAT_ROUTE_8_TRAINER_8");

            CheckFlag(s_eventFlags[0x441], "EVENT_BEAT_ROUTE_9_TRAINER_0");
            CheckFlag(s_eventFlags[0x442], "EVENT_BEAT_ROUTE_9_TRAINER_1");
            CheckFlag(s_eventFlags[0x443], "EVENT_BEAT_ROUTE_9_TRAINER_2");
            CheckFlag(s_eventFlags[0x444], "EVENT_BEAT_ROUTE_9_TRAINER_3");
            CheckFlag(s_eventFlags[0x445], "EVENT_BEAT_ROUTE_9_TRAINER_4");
            CheckFlag(s_eventFlags[0x446], "EVENT_BEAT_ROUTE_9_TRAINER_5");
            CheckFlag(s_eventFlags[0x447], "EVENT_BEAT_ROUTE_9_TRAINER_6");
            CheckFlag(s_eventFlags[0x448], "EVENT_BEAT_ROUTE_9_TRAINER_7");
            CheckFlag(s_eventFlags[0x449], "EVENT_BEAT_ROUTE_9_TRAINER_8");

            CheckFlag(s_eventFlags[0x451], "EVENT_BEAT_ROUTE_10_TRAINER_0");
            CheckFlag(s_eventFlags[0x452], "EVENT_BEAT_ROUTE_10_TRAINER_1");
            CheckFlag(s_eventFlags[0x453], "EVENT_BEAT_ROUTE_10_TRAINER_2");
            CheckFlag(s_eventFlags[0x454], "EVENT_BEAT_ROUTE_10_TRAINER_3");
            CheckFlag(s_eventFlags[0x455], "EVENT_BEAT_ROUTE_10_TRAINER_4");
            CheckFlag(s_eventFlags[0x456], "EVENT_BEAT_ROUTE_10_TRAINER_5");
            CheckFlag(s_eventFlags[0x459], "EVENT_BEAT_ROCK_TUNNEL_1_TRAINER_0");
            CheckFlag(s_eventFlags[0x45A], "EVENT_BEAT_ROCK_TUNNEL_1_TRAINER_1");
            CheckFlag(s_eventFlags[0x45B], "EVENT_BEAT_ROCK_TUNNEL_1_TRAINER_2");
            CheckFlag(s_eventFlags[0x45C], "EVENT_BEAT_ROCK_TUNNEL_1_TRAINER_3");
            CheckFlag(s_eventFlags[0x45D], "EVENT_BEAT_ROCK_TUNNEL_1_TRAINER_4");
            CheckFlag(s_eventFlags[0x45E], "EVENT_BEAT_ROCK_TUNNEL_1_TRAINER_5");
            CheckFlag(s_eventFlags[0x45F], "EVENT_BEAT_ROCK_TUNNEL_1_TRAINER_6");
            CheckFlag(s_eventFlags[0x461], "EVENT_BEAT_POWER_PLANT_VOLTORB_0");
            CheckFlag(s_eventFlags[0x462], "EVENT_BEAT_POWER_PLANT_VOLTORB_1");
            CheckFlag(s_eventFlags[0x463], "EVENT_BEAT_POWER_PLANT_VOLTORB_2");
            CheckFlag(s_eventFlags[0x464], "EVENT_BEAT_POWER_PLANT_VOLTORB_3");
            CheckFlag(s_eventFlags[0x465], "EVENT_BEAT_POWER_PLANT_VOLTORB_4");
            CheckFlag(s_eventFlags[0x466], "EVENT_BEAT_POWER_PLANT_VOLTORB_5");
            CheckFlag(s_eventFlags[0x467], "EVENT_BEAT_POWER_PLANT_VOLTORB_6");
            CheckFlag(s_eventFlags[0x468], "EVENT_BEAT_POWER_PLANT_VOLTORB_7");
            CheckFlag(s_eventFlags[0x469], "EVENT_BEAT_ZAPDOS");

            CheckFlag(s_eventFlags[0x471], "EVENT_BEAT_ROUTE_11_TRAINER_0");
            CheckFlag(s_eventFlags[0x472], "EVENT_BEAT_ROUTE_11_TRAINER_1");
            CheckFlag(s_eventFlags[0x473], "EVENT_BEAT_ROUTE_11_TRAINER_2");
            CheckFlag(s_eventFlags[0x474], "EVENT_BEAT_ROUTE_11_TRAINER_3");
            CheckFlag(s_eventFlags[0x475], "EVENT_BEAT_ROUTE_11_TRAINER_4");
            CheckFlag(s_eventFlags[0x476], "EVENT_BEAT_ROUTE_11_TRAINER_5");
            CheckFlag(s_eventFlags[0x477], "EVENT_BEAT_ROUTE_11_TRAINER_6");
            CheckFlag(s_eventFlags[0x478], "EVENT_BEAT_ROUTE_11_TRAINER_7");
            CheckFlag(s_eventFlags[0x479], "EVENT_BEAT_ROUTE_11_TRAINER_8");
            CheckFlag(s_eventFlags[0x47A], "EVENT_BEAT_ROUTE_11_TRAINER_9");
            CheckFlag(s_eventFlags[0x47F], "EVENT_GOT_ITEMFINDER");

            CheckFlag(s_eventFlags[0x480], "EVENT_GOT_TM39");
            CheckFlag(s_eventFlags[0x482], "EVENT_BEAT_ROUTE_12_TRAINER_0");
            CheckFlag(s_eventFlags[0x483], "EVENT_BEAT_ROUTE_12_TRAINER_1");
            CheckFlag(s_eventFlags[0x484], "EVENT_BEAT_ROUTE_12_TRAINER_2");
            CheckFlag(s_eventFlags[0x485], "EVENT_BEAT_ROUTE_12_TRAINER_3");
            CheckFlag(s_eventFlags[0x486], "EVENT_BEAT_ROUTE_12_TRAINER_4");
            CheckFlag(s_eventFlags[0x487], "EVENT_BEAT_ROUTE_12_TRAINER_5");
            CheckFlag(s_eventFlags[0x488], "EVENT_BEAT_ROUTE_12_TRAINER_6");
            CheckFlag(s_eventFlags[0x48F], "EVENT_BEAT_ROUTE12_SNORLAX");

            CheckFlag(s_eventFlags[0x491], "EVENT_BEAT_ROUTE_13_TRAINER_0");
            CheckFlag(s_eventFlags[0x492], "EVENT_BEAT_ROUTE_13_TRAINER_1");
            CheckFlag(s_eventFlags[0x493], "EVENT_BEAT_ROUTE_13_TRAINER_2");
            CheckFlag(s_eventFlags[0x494], "EVENT_BEAT_ROUTE_13_TRAINER_3");
            CheckFlag(s_eventFlags[0x495], "EVENT_BEAT_ROUTE_13_TRAINER_4");
            CheckFlag(s_eventFlags[0x496], "EVENT_BEAT_ROUTE_13_TRAINER_5");
            CheckFlag(s_eventFlags[0x497], "EVENT_BEAT_ROUTE_13_TRAINER_6");
            CheckFlag(s_eventFlags[0x498], "EVENT_BEAT_ROUTE_13_TRAINER_7");
            CheckFlag(s_eventFlags[0x499], "EVENT_BEAT_ROUTE_13_TRAINER_8");
            CheckFlag(s_eventFlags[0x49A], "EVENT_BEAT_ROUTE_13_TRAINER_9");

            CheckFlag(s_eventFlags[0x4A1], "EVENT_BEAT_ROUTE_14_TRAINER_0");
            CheckFlag(s_eventFlags[0x4A2], "EVENT_BEAT_ROUTE_14_TRAINER_1");
            CheckFlag(s_eventFlags[0x4A3], "EVENT_BEAT_ROUTE_14_TRAINER_2");
            CheckFlag(s_eventFlags[0x4A4], "EVENT_BEAT_ROUTE_14_TRAINER_3");
            CheckFlag(s_eventFlags[0x4A5], "EVENT_BEAT_ROUTE_14_TRAINER_4");
            CheckFlag(s_eventFlags[0x4A6], "EVENT_BEAT_ROUTE_14_TRAINER_5");
            CheckFlag(s_eventFlags[0x4A7], "EVENT_BEAT_ROUTE_14_TRAINER_6");
            CheckFlag(s_eventFlags[0x4A8], "EVENT_BEAT_ROUTE_14_TRAINER_7");
            CheckFlag(s_eventFlags[0x4A9], "EVENT_BEAT_ROUTE_14_TRAINER_8");
            CheckFlag(s_eventFlags[0x4AA], "EVENT_BEAT_ROUTE_14_TRAINER_9");

            CheckFlag(s_eventFlags[0x4B0], "EVENT_GOT_EXP_ALL");
            CheckFlag(s_eventFlags[0x4B1], "EVENT_BEAT_ROUTE_15_TRAINER_0");
            CheckFlag(s_eventFlags[0x4B2], "EVENT_BEAT_ROUTE_15_TRAINER_1");
            CheckFlag(s_eventFlags[0x4B3], "EVENT_BEAT_ROUTE_15_TRAINER_2");
            CheckFlag(s_eventFlags[0x4B4], "EVENT_BEAT_ROUTE_15_TRAINER_3");
            CheckFlag(s_eventFlags[0x4B5], "EVENT_BEAT_ROUTE_15_TRAINER_4");
            CheckFlag(s_eventFlags[0x4B6], "EVENT_BEAT_ROUTE_15_TRAINER_5");
            CheckFlag(s_eventFlags[0x4B7], "EVENT_BEAT_ROUTE_15_TRAINER_6");
            CheckFlag(s_eventFlags[0x4B8], "EVENT_BEAT_ROUTE_15_TRAINER_7");
            CheckFlag(s_eventFlags[0x4B9], "EVENT_BEAT_ROUTE_15_TRAINER_8");
            CheckFlag(s_eventFlags[0x4BA], "EVENT_BEAT_ROUTE_15_TRAINER_9");

            CheckFlag(s_eventFlags[0x4C1], "EVENT_BEAT_ROUTE_16_TRAINER_0");
            CheckFlag(s_eventFlags[0x4C2], "EVENT_BEAT_ROUTE_16_TRAINER_1");
            CheckFlag(s_eventFlags[0x4C3], "EVENT_BEAT_ROUTE_16_TRAINER_2");
            CheckFlag(s_eventFlags[0x4C4], "EVENT_BEAT_ROUTE_16_TRAINER_3");
            CheckFlag(s_eventFlags[0x4C5], "EVENT_BEAT_ROUTE_16_TRAINER_4");
            CheckFlag(s_eventFlags[0x4C6], "EVENT_BEAT_ROUTE_16_TRAINER_5");
            CheckFlag(s_eventFlags[0x4C9], "EVENT_BEAT_ROUTE16_SNORLAX");
            CheckFlag(s_eventFlags[0x4CE], "EVENT_GOT_HM02");

            CheckFlag(s_eventFlags[0x4D1], "EVENT_BEAT_ROUTE_17_TRAINER_0");
            CheckFlag(s_eventFlags[0x4D2], "EVENT_BEAT_ROUTE_17_TRAINER_1");
            CheckFlag(s_eventFlags[0x4D3], "EVENT_BEAT_ROUTE_17_TRAINER_2");
            CheckFlag(s_eventFlags[0x4D4], "EVENT_BEAT_ROUTE_17_TRAINER_3");
            CheckFlag(s_eventFlags[0x4D5], "EVENT_BEAT_ROUTE_17_TRAINER_4");
            CheckFlag(s_eventFlags[0x4D6], "EVENT_BEAT_ROUTE_17_TRAINER_5");
            CheckFlag(s_eventFlags[0x4D7], "EVENT_BEAT_ROUTE_17_TRAINER_6");
            CheckFlag(s_eventFlags[0x4D8], "EVENT_BEAT_ROUTE_17_TRAINER_7");
            CheckFlag(s_eventFlags[0x4D9], "EVENT_BEAT_ROUTE_17_TRAINER_8");
            CheckFlag(s_eventFlags[0x4DA], "EVENT_BEAT_ROUTE_17_TRAINER_9");

            CheckFlag(s_eventFlags[0x4E1], "EVENT_BEAT_ROUTE_18_TRAINER_0");
            CheckFlag(s_eventFlags[0x4E2], "EVENT_BEAT_ROUTE_18_TRAINER_1");
            CheckFlag(s_eventFlags[0x4E3], "EVENT_BEAT_ROUTE_18_TRAINER_2");

            CheckFlag(s_eventFlags[0x4F1], "EVENT_BEAT_ROUTE_19_TRAINER_0");
            CheckFlag(s_eventFlags[0x4F2], "EVENT_BEAT_ROUTE_19_TRAINER_1");
            CheckFlag(s_eventFlags[0x4F3], "EVENT_BEAT_ROUTE_19_TRAINER_2");
            CheckFlag(s_eventFlags[0x4F4], "EVENT_BEAT_ROUTE_19_TRAINER_3");
            CheckFlag(s_eventFlags[0x4F5], "EVENT_BEAT_ROUTE_19_TRAINER_4");
            CheckFlag(s_eventFlags[0x4F6], "EVENT_BEAT_ROUTE_19_TRAINER_5");
            CheckFlag(s_eventFlags[0x4F7], "EVENT_BEAT_ROUTE_19_TRAINER_6");
            CheckFlag(s_eventFlags[0x4F8], "EVENT_BEAT_ROUTE_19_TRAINER_7");
            CheckFlag(s_eventFlags[0x4F9], "EVENT_BEAT_ROUTE_19_TRAINER_8");
            CheckFlag(s_eventFlags[0x4FA], "EVENT_BEAT_ROUTE_19_TRAINER_9");

            CheckFlag(s_eventFlags[0x501], "EVENT_BEAT_ROUTE_20_TRAINER_0");
            CheckFlag(s_eventFlags[0x502], "EVENT_BEAT_ROUTE_20_TRAINER_1");
            CheckFlag(s_eventFlags[0x503], "EVENT_BEAT_ROUTE_20_TRAINER_2");
            CheckFlag(s_eventFlags[0x504], "EVENT_BEAT_ROUTE_20_TRAINER_3");
            CheckFlag(s_eventFlags[0x505], "EVENT_BEAT_ROUTE_20_TRAINER_4");
            CheckFlag(s_eventFlags[0x506], "EVENT_BEAT_ROUTE_20_TRAINER_5");
            CheckFlag(s_eventFlags[0x507], "EVENT_BEAT_ROUTE_20_TRAINER_6");
            CheckFlag(s_eventFlags[0x508], "EVENT_BEAT_ROUTE_20_TRAINER_7");
            CheckFlag(s_eventFlags[0x509], "EVENT_BEAT_ROUTE_20_TRAINER_8");
            CheckFlag(s_eventFlags[0x50A], "EVENT_BEAT_ROUTE_20_TRAINER_9");

            CheckFlag(s_eventFlags[0x511], "EVENT_BEAT_ROUTE_21_TRAINER_0");
            CheckFlag(s_eventFlags[0x512], "EVENT_BEAT_ROUTE_21_TRAINER_1");
            CheckFlag(s_eventFlags[0x513], "EVENT_BEAT_ROUTE_21_TRAINER_2");
            CheckFlag(s_eventFlags[0x514], "EVENT_BEAT_ROUTE_21_TRAINER_3");
            CheckFlag(s_eventFlags[0x515], "EVENT_BEAT_ROUTE_21_TRAINER_4");
            CheckFlag(s_eventFlags[0x516], "EVENT_BEAT_ROUTE_21_TRAINER_5");
            CheckFlag(s_eventFlags[0x517], "EVENT_BEAT_ROUTE_21_TRAINER_6");
            CheckFlag(s_eventFlags[0x518], "EVENT_BEAT_ROUTE_21_TRAINER_7");
            CheckFlag(s_eventFlags[0x519], "EVENT_BEAT_ROUTE_21_TRAINER_8");

            CheckFlag(s_eventFlags[0x525], "EVENT_BEAT_ROUTE22_RIVAL_1ST_BATTLE");
            CheckFlag(s_eventFlags[0x526], "EVENT_BEAT_ROUTE22_RIVAL_2ND_BATTLE");

            CheckFlag(s_eventFlags[0x539], "EVENT_BEAT_VICTORY_ROAD_2_TRAINER_0");
            CheckFlag(s_eventFlags[0x53A], "EVENT_BEAT_VICTORY_ROAD_2_TRAINER_1");
            CheckFlag(s_eventFlags[0x53B], "EVENT_BEAT_VICTORY_ROAD_2_TRAINER_2");
            CheckFlag(s_eventFlags[0x53C], "EVENT_BEAT_VICTORY_ROAD_2_TRAINER_3");
            CheckFlag(s_eventFlags[0x53D], "EVENT_BEAT_VICTORY_ROAD_2_TRAINER_4");
            CheckFlag(s_eventFlags[0x53E], "EVENT_BEAT_MOLTRES");

            CheckFlag(s_eventFlags[0x540], "EVENT_GOT_NUGGET");
            CheckFlag(s_eventFlags[0x541], "EVENT_BEAT_ROUTE24_ROCKET");
            CheckFlag(s_eventFlags[0x542], "EVENT_BEAT_ROUTE_24_TRAINER_0");
            CheckFlag(s_eventFlags[0x543], "EVENT_BEAT_ROUTE_24_TRAINER_1");
            CheckFlag(s_eventFlags[0x544], "EVENT_BEAT_ROUTE_24_TRAINER_2");
            CheckFlag(s_eventFlags[0x545], "EVENT_BEAT_ROUTE_24_TRAINER_3");
            CheckFlag(s_eventFlags[0x546], "EVENT_BEAT_ROUTE_24_TRAINER_4");
            CheckFlag(s_eventFlags[0x547], "EVENT_BEAT_ROUTE_24_TRAINER_5");

            CheckFlag(s_eventFlags[0x54F], "EVENT_GOT_CHARMANDER_FROM_DAMIAN");
            CheckFlag(s_eventFlags[0x551], "EVENT_BEAT_ROUTE_25_TRAINER_0");
            CheckFlag(s_eventFlags[0x552], "EVENT_BEAT_ROUTE_25_TRAINER_1");
            CheckFlag(s_eventFlags[0x553], "EVENT_BEAT_ROUTE_25_TRAINER_2");
            CheckFlag(s_eventFlags[0x554], "EVENT_BEAT_ROUTE_25_TRAINER_3");
            CheckFlag(s_eventFlags[0x555], "EVENT_BEAT_ROUTE_25_TRAINER_4");
            CheckFlag(s_eventFlags[0x556], "EVENT_BEAT_ROUTE_25_TRAINER_5");
            CheckFlag(s_eventFlags[0x557], "EVENT_BEAT_ROUTE_25_TRAINER_6");
            CheckFlag(s_eventFlags[0x558], "EVENT_BEAT_ROUTE_25_TRAINER_7");
            CheckFlag(s_eventFlags[0x559], "EVENT_BEAT_ROUTE_25_TRAINER_8");
            CheckFlag(s_eventFlags[0x55C], "EVENT_GOT_SS_TICKET");

            CheckFlag(s_eventFlags[0x562], "EVENT_BEAT_VIRIDIAN_FOREST_TRAINER_0");
            CheckFlag(s_eventFlags[0x563], "EVENT_BEAT_VIRIDIAN_FOREST_TRAINER_1");
            CheckFlag(s_eventFlags[0x564], "EVENT_BEAT_VIRIDIAN_FOREST_TRAINER_2");
            CheckFlag(s_eventFlags[0x565], "EVENT_BEAT_VIRIDIAN_FOREST_TRAINER_3");
            CheckFlag(s_eventFlags[0x566], "EVENT_BEAT_VIRIDIAN_FOREST_TRAINER_4");

            CheckFlag(s_eventFlags[0x571], "EVENT_BEAT_MT_MOON_1_TRAINER_0");
            CheckFlag(s_eventFlags[0x572], "EVENT_BEAT_MT_MOON_1_TRAINER_1");
            CheckFlag(s_eventFlags[0x573], "EVENT_BEAT_MT_MOON_1_TRAINER_2");
            CheckFlag(s_eventFlags[0x574], "EVENT_BEAT_MT_MOON_1_TRAINER_3");
            CheckFlag(s_eventFlags[0x575], "EVENT_BEAT_MT_MOON_1_TRAINER_4");
            CheckFlag(s_eventFlags[0x576], "EVENT_BEAT_MT_MOON_1_TRAINER_5");
            CheckFlag(s_eventFlags[0x577], "EVENT_BEAT_MT_MOON_1_TRAINER_6");
            CheckFlag(s_eventFlags[0x579], "EVENT_BEAT_MT_MOON_EXIT_SUPER_NERD");
            CheckFlag(s_eventFlags[0x57A], "EVENT_BEAT_MT_MOON_3_TRAINER_0");
            CheckFlag(s_eventFlags[0x57B], "EVENT_BEAT_MT_MOON_3_TRAINER_1");
            CheckFlag(s_eventFlags[0x57C], "EVENT_BEAT_MT_MOON_3_TRAINER_2");
            CheckFlag(s_eventFlags[0x57D], "EVENT_BEAT_MT_MOON_3_TRAINER_3");

            if (!s_eventFlags[0x578] && !s_eventFlags[0x57F])
            {
                //CheckFlag(s_eventFlags[0x578], "EVENT_GOT_DOME_FOSSIL");
                //CheckFlag(s_eventFlags[0x57F], "EVENT_GOT_HELIX_FOSSIL");

                CheckFlag(s_eventFlags[0x578], "EVENT_GOT_DOME/HELIX_FOSSIL");
            }

            CheckFlag(s_eventFlags[0x5C4], "EVENT_BEAT_SS_ANNE_5_TRAINER_0");
            CheckFlag(s_eventFlags[0x5C5], "EVENT_BEAT_SS_ANNE_5_TRAINER_1");
            CheckFlag(s_eventFlags[0x5E0], "EVENT_GOT_HM01");
            CheckFlag(s_eventFlags[0x5F1], "EVENT_BEAT_SS_ANNE_8_TRAINER_0");
            CheckFlag(s_eventFlags[0x5F2], "EVENT_BEAT_SS_ANNE_8_TRAINER_1");
            CheckFlag(s_eventFlags[0x5F3], "EVENT_BEAT_SS_ANNE_8_TRAINER_2");
            CheckFlag(s_eventFlags[0x5F4], "EVENT_BEAT_SS_ANNE_8_TRAINER_3");
            CheckFlag(s_eventFlags[0x601], "EVENT_BEAT_SS_ANNE_9_TRAINER_0");
            CheckFlag(s_eventFlags[0x602], "EVENT_BEAT_SS_ANNE_9_TRAINER_1");
            CheckFlag(s_eventFlags[0x603], "EVENT_BEAT_SS_ANNE_9_TRAINER_2");
            CheckFlag(s_eventFlags[0x604], "EVENT_BEAT_SS_ANNE_9_TRAINER_3");
            CheckFlag(s_eventFlags[0x611], "EVENT_BEAT_SS_ANNE_10_TRAINER_0");
            CheckFlag(s_eventFlags[0x612], "EVENT_BEAT_SS_ANNE_10_TRAINER_1");
            CheckFlag(s_eventFlags[0x613], "EVENT_BEAT_SS_ANNE_10_TRAINER_2");
            CheckFlag(s_eventFlags[0x614], "EVENT_BEAT_SS_ANNE_10_TRAINER_3");
            CheckFlag(s_eventFlags[0x615], "EVENT_BEAT_SS_ANNE_10_TRAINER_4");
            CheckFlag(s_eventFlags[0x616], "EVENT_BEAT_SS_ANNE_10_TRAINER_5");

            CheckFlag(s_eventFlags[0x661], "EVENT_BEAT_VICTORY_ROAD_3_TRAINER_0");
            CheckFlag(s_eventFlags[0x662], "EVENT_BEAT_VICTORY_ROAD_3_TRAINER_1");
            CheckFlag(s_eventFlags[0x663], "EVENT_BEAT_VICTORY_ROAD_3_TRAINER_2");
            CheckFlag(s_eventFlags[0x664], "EVENT_BEAT_VICTORY_ROAD_3_TRAINER_3");

            CheckFlag(s_eventFlags[0x671], "EVENT_BEAT_ROCKET_HIDEOUT_1_TRAINER_0");
            CheckFlag(s_eventFlags[0x672], "EVENT_BEAT_ROCKET_HIDEOUT_1_TRAINER_1");
            CheckFlag(s_eventFlags[0x673], "EVENT_BEAT_ROCKET_HIDEOUT_1_TRAINER_2");
            CheckFlag(s_eventFlags[0x674], "EVENT_BEAT_ROCKET_HIDEOUT_1_TRAINER_3");
            CheckFlag(s_eventFlags[0x675], "EVENT_BEAT_ROCKET_HIDEOUT_1_TRAINER_4");
            CheckFlag(s_eventFlags[0x681], "EVENT_BEAT_ROCKET_HIDEOUT_2_TRAINER_0");
            CheckFlag(s_eventFlags[0x691], "EVENT_BEAT_ROCKET_HIDEOUT_3_TRAINER_0");
            CheckFlag(s_eventFlags[0x692], "EVENT_BEAT_ROCKET_HIDEOUT_3_TRAINER_1");
            CheckFlag(s_eventFlags[0x6A2], "EVENT_BEAT_ROCKET_HIDEOUT_4_TRAINER_0");
            CheckFlag(s_eventFlags[0x6A3], "EVENT_BEAT_ROCKET_HIDEOUT_4_TRAINER_1");
            CheckFlag(s_eventFlags[0x6A4], "EVENT_BEAT_ROCKET_HIDEOUT_4_TRAINER_2");
            CheckFlag(s_eventFlags[0x6A7], "EVENT_BEAT_ROCKET_HIDEOUT_GIOVANNI");

            CheckFlag(s_eventFlags[0x6F2], "EVENT_BEAT_SILPH_CO_2F_TRAINER_0");
            CheckFlag(s_eventFlags[0x6F3], "EVENT_BEAT_SILPH_CO_2F_TRAINER_1");
            CheckFlag(s_eventFlags[0x6F4], "EVENT_BEAT_SILPH_CO_2F_TRAINER_2");
            CheckFlag(s_eventFlags[0x6F5], "EVENT_BEAT_SILPH_CO_2F_TRAINER_3");
            CheckFlag(s_eventFlags[0x6FF], "EVENT_GOT_TM36");
            CheckFlag(s_eventFlags[0x702], "EVENT_BEAT_SILPH_CO_3F_TRAINER_0");
            CheckFlag(s_eventFlags[0x703], "EVENT_BEAT_SILPH_CO_3F_TRAINER_1");
            CheckFlag(s_eventFlags[0x712], "EVENT_BEAT_SILPH_CO_4F_TRAINER_0");
            CheckFlag(s_eventFlags[0x713], "EVENT_BEAT_SILPH_CO_4F_TRAINER_1");
            CheckFlag(s_eventFlags[0x714], "EVENT_BEAT_SILPH_CO_4F_TRAINER_2");
            CheckFlag(s_eventFlags[0x722], "EVENT_BEAT_SILPH_CO_5F_TRAINER_0");
            CheckFlag(s_eventFlags[0x723], "EVENT_BEAT_SILPH_CO_5F_TRAINER_1");
            CheckFlag(s_eventFlags[0x724], "EVENT_BEAT_SILPH_CO_5F_TRAINER_2");
            CheckFlag(s_eventFlags[0x725], "EVENT_BEAT_SILPH_CO_5F_TRAINER_3");
            CheckFlag(s_eventFlags[0x736], "EVENT_BEAT_SILPH_CO_6F_TRAINER_0");
            CheckFlag(s_eventFlags[0x737], "EVENT_BEAT_SILPH_CO_6F_TRAINER_1");
            CheckFlag(s_eventFlags[0x738], "EVENT_BEAT_SILPH_CO_6F_TRAINER_2");
            CheckFlag(s_eventFlags[0x740], "EVENT_BEAT_SILPH_CO_RIVAL");
            CheckFlag(s_eventFlags[0x745], "EVENT_BEAT_SILPH_CO_7F_TRAINER_0");
            CheckFlag(s_eventFlags[0x746], "EVENT_BEAT_SILPH_CO_7F_TRAINER_1");
            CheckFlag(s_eventFlags[0x747], "EVENT_BEAT_SILPH_CO_7F_TRAINER_2");
            CheckFlag(s_eventFlags[0x748], "EVENT_BEAT_SILPH_CO_7F_TRAINER_3");
            CheckFlag(s_eventFlags[0x752], "EVENT_BEAT_SILPH_CO_8F_TRAINER_0");
            CheckFlag(s_eventFlags[0x753], "EVENT_BEAT_SILPH_CO_8F_TRAINER_1");
            CheckFlag(s_eventFlags[0x754], "EVENT_BEAT_SILPH_CO_8F_TRAINER_2");
            CheckFlag(s_eventFlags[0x762], "EVENT_BEAT_SILPH_CO_9F_TRAINER_0");
            CheckFlag(s_eventFlags[0x763], "EVENT_BEAT_SILPH_CO_9F_TRAINER_1");
            CheckFlag(s_eventFlags[0x764], "EVENT_BEAT_SILPH_CO_9F_TRAINER_2");
            CheckFlag(s_eventFlags[0x771], "EVENT_BEAT_SILPH_CO_10F_TRAINER_0");
            CheckFlag(s_eventFlags[0x772], "EVENT_BEAT_SILPH_CO_10F_TRAINER_1");
            CheckFlag(s_eventFlags[0x784], "EVENT_BEAT_SILPH_CO_11F_TRAINER_0");
            CheckFlag(s_eventFlags[0x785], "EVENT_BEAT_SILPH_CO_11F_TRAINER_1");
            CheckFlag(s_eventFlags[0x78D], "EVENT_GOT_MASTER_BALL");
            CheckFlag(s_eventFlags[0x78F], "EVENT_BEAT_SILPH_CO_GIOVANNI");

            CheckFlag(s_eventFlags[0x801], "EVENT_BEAT_MANSION_2_TRAINER_0");
            CheckFlag(s_eventFlags[0x811], "EVENT_BEAT_MANSION_3_TRAINER_0");
            CheckFlag(s_eventFlags[0x812], "EVENT_BEAT_MANSION_3_TRAINER_1");
            CheckFlag(s_eventFlags[0x821], "EVENT_BEAT_MANSION_4_TRAINER_0");
            CheckFlag(s_eventFlags[0x822], "EVENT_BEAT_MANSION_4_TRAINER_1");

            CheckFlag(s_eventFlags[0x880], "EVENT_GOT_HM03");

            CheckFlag(s_eventFlags[0x8C1], "EVENT_BEAT_MEWTWO");

            CheckFlag(s_eventFlags[0x911], "EVENT_BEAT_VICTORY_ROAD_1_TRAINER_0");
            CheckFlag(s_eventFlags[0x912], "EVENT_BEAT_VICTORY_ROAD_1_TRAINER_1");

            CheckFlag(s_eventFlags[0x9B1], "EVENT_BEAT_ROCK_TUNNEL_2_TRAINER_0");
            CheckFlag(s_eventFlags[0x9B2], "EVENT_BEAT_ROCK_TUNNEL_2_TRAINER_1");
            CheckFlag(s_eventFlags[0x9B3], "EVENT_BEAT_ROCK_TUNNEL_2_TRAINER_2");
            CheckFlag(s_eventFlags[0x9B4], "EVENT_BEAT_ROCK_TUNNEL_2_TRAINER_3");
            CheckFlag(s_eventFlags[0x9B5], "EVENT_BEAT_ROCK_TUNNEL_2_TRAINER_4");
            CheckFlag(s_eventFlags[0x9B6], "EVENT_BEAT_ROCK_TUNNEL_2_TRAINER_5");
            CheckFlag(s_eventFlags[0x9B7], "EVENT_BEAT_ROCK_TUNNEL_2_TRAINER_6");
            CheckFlag(s_eventFlags[0x9B8], "EVENT_BEAT_ROCK_TUNNEL_2_TRAINER_7");

            CheckFlag(s_eventFlags[0x9DA], "EVENT_BEAT_ARTICUNO");
        }
    }

}
