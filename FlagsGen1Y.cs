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
        static Dictionary<int, string> s_flagDetailsHiddenCoins = new Dictionary<int, string>();
        static Dictionary<int, string> s_flagDetailsHiddenItems = new Dictionary<int, string>();
        static Dictionary<int, string> s_flagDetailsNormalItems = new Dictionary<int, string>();

        static bool[] s_obtainedHiddenCoinsFlags;
        static bool[] s_obtainedHiddenItemsFlags;
        static bool[] s_missableObjectFlags;

        static SaveFile s_savFile;

        enum FlagOffsets
        {
            ObtainedHiddenItems = 0x299C,
            ObtainedHiddenCoins = 0x29AA,
            MissableObjectFlags = 0x2852
        }


        public static void ExportFlags(SaveFile savFile)
        {
            s_savFile = savFile;

            InitFlagsData();
            InitFlagDetails();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s_obtainedHiddenCoinsFlags.Length; ++i)
            {
                if (!s_obtainedHiddenCoinsFlags[i] && s_flagDetailsHiddenCoins.ContainsKey(i))
                {
                    sb.AppendFormat("{0}\n", s_flagDetailsHiddenCoins[i]);
                }
            }

            for (int i = 0; i < s_obtainedHiddenItemsFlags.Length; ++i)
            {
                if (!s_obtainedHiddenItemsFlags[i] && s_flagDetailsHiddenItems.ContainsKey(i))
                {
                    sb.AppendFormat("{0}\n", s_flagDetailsHiddenItems[i]);
                }
            }

            for (int i = 0; i < s_missableObjectFlags.Length; ++i)
            {
                if (!s_missableObjectFlags[i] && s_flagDetailsNormalItems.ContainsKey(i))
                {
                    sb.AppendFormat("{0}\n", s_flagDetailsNormalItems[i]);
                }

                //sb.AppendFormat("FLAG_ITEM_0x{0:X2} {1}\n", i, s_missableObjectFlags[i]);
            }

            System.IO.File.WriteAllText(string.Format("missing_events_{0}.txt", s_savFile.Version), sb.ToString());
        }

        static void InitFlagsData()
        {
            bool[] result = new bool[16];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = s_savFile.GetFlag((int)FlagOffsets.ObtainedHiddenCoins + (i >> 3), i & 7);
            }
            s_obtainedHiddenCoinsFlags = result;

            result = new bool[112];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = s_savFile.GetFlag((int)FlagOffsets.ObtainedHiddenItems + (i >> 3), i & 7);
            }
            s_obtainedHiddenItemsFlags = result;

            result = new bool[32 * 8];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = s_savFile.GetFlag((int)FlagOffsets.MissableObjectFlags + (i >> 3), i & 7);
            }
            s_missableObjectFlags = result;
        }

        static void InitFlagDetails()
        {
            // Hidden Coins
            s_flagDetailsHiddenCoins[0] = "FLAG_HIDDEN_COIN_GAME_CORNER_0";
            s_flagDetailsHiddenCoins[1] = "FLAG_HIDDEN_COIN_GAME_CORNER_1";
            s_flagDetailsHiddenCoins[2] = "FLAG_HIDDEN_COIN_GAME_CORNER_2";
            s_flagDetailsHiddenCoins[3] = "FLAG_HIDDEN_COIN_GAME_CORNER_3";
            s_flagDetailsHiddenCoins[4] = "FLAG_HIDDEN_COIN_GAME_CORNER_4";
            s_flagDetailsHiddenCoins[5] = "FLAG_HIDDEN_COIN_GAME_CORNER_5";
            s_flagDetailsHiddenCoins[6] = "FLAG_HIDDEN_COIN_GAME_CORNER_6";
            s_flagDetailsHiddenCoins[7] = "FLAG_HIDDEN_COIN_GAME_CORNER_7";
            s_flagDetailsHiddenCoins[8] = "FLAG_HIDDEN_COIN_GAME_CORNER_8";
            s_flagDetailsHiddenCoins[9] = "FLAG_HIDDEN_COIN_GAME_CORNER_9";
            s_flagDetailsHiddenCoins[10] = "FLAG_HIDDEN_COIN_GAME_CORNER_10";
            //s_flagDetailsHiddenCoins[11] = "FLAG_HIDDEN_COIN_GAME_CORNER_11"; // inaccessible

            // Hidden Items
            s_flagDetailsHiddenItems[0x00] = "FLAG_HIDDEN_ITEM_SILPH_CO_5F_0";
            s_flagDetailsHiddenItems[0x01] = "FLAG_HIDDEN_ITEM_SILPH_CO_9F_0";
            s_flagDetailsHiddenItems[0x02] = "FLAG_HIDDEN_ITEM_POKEMON_MANSION_3F_0";
            s_flagDetailsHiddenItems[0x03] = "FLAG_HIDDEN_ITEM_POKEMON_MANSION_B1F_0";
            s_flagDetailsHiddenItems[0x04] = "FLAG_HIDDEN_ITEM_SAFARI_ZONE_WEST_0";
            s_flagDetailsHiddenItems[0x05] = "FLAG_HIDDEN_ITEM_CERULEAN_CAVE_2F_0";
            s_flagDetailsHiddenItems[0x06] = "FLAG_HIDDEN_ITEM_CERULEAN_CAVE_B1F_0";
            s_flagDetailsHiddenItems[0x07] = "FLAG_HIDDEN_ITEM_UNUSED_MAP_6F_0";
            s_flagDetailsHiddenItems[0x08] = "FLAG_HIDDEN_ITEM_SEAFOAM_ISLANDS_B2F_0";
            s_flagDetailsHiddenItems[0x09] = "FLAG_HIDDEN_ITEM_SEAFOAM_ISLANDS_B3F_0";
            s_flagDetailsHiddenItems[0x0A] = "FLAG_HIDDEN_ITEM_SEAFOAM_ISLANDS_B4F_0";
            s_flagDetailsHiddenItems[0x0B] = "FLAG_HIDDEN_ITEM_VIRIDIAN_FOREST_0";
            s_flagDetailsHiddenItems[0x0C] = "FLAG_HIDDEN_ITEM_VIRIDIAN_FOREST_1";
            s_flagDetailsHiddenItems[0x0D] = "FLAG_HIDDEN_ITEM_MT_MOON_B2F_0";
            s_flagDetailsHiddenItems[0x0E] = "FLAG_HIDDEN_ITEM_MT_MOON_B2F_1";
            s_flagDetailsHiddenItems[0x0F] = "FLAG_HIDDEN_ITEM_SS_ANNE_B1F_ROOMS_0";
            s_flagDetailsHiddenItems[0x10] = "FLAG_HIDDEN_ITEM_SS_ANNE_KITCHEN_0";
            s_flagDetailsHiddenItems[0x11] = "FLAG_HIDDEN_ITEM_UNDERGROUND_PATH_NORTH_SOUTH_0";
            s_flagDetailsHiddenItems[0x12] = "FLAG_HIDDEN_ITEM_UNDERGROUND_PATH_NORTH_SOUTH_1";
            s_flagDetailsHiddenItems[0x13] = "FLAG_HIDDEN_ITEM_UNDERGROUND_PATH_WEST_EAST_0";
            s_flagDetailsHiddenItems[0x14] = "FLAG_HIDDEN_ITEM_UNDERGROUND_PATH_WEST_EAST_1";
            s_flagDetailsHiddenItems[0x15] = "FLAG_HIDDEN_ITEM_ROCKET_HIDEOUT_B1F_0";
            s_flagDetailsHiddenItems[0x16] = "FLAG_HIDDEN_ITEM_ROCKET_HIDEOUT_B3F_0";
            s_flagDetailsHiddenItems[0x17] = "FLAG_HIDDEN_ITEM_ROCKET_HIDEOUT_B4F_0";
            s_flagDetailsHiddenItems[0x18] = "FLAG_HIDDEN_ITEM_ROUTE_10_0";
            s_flagDetailsHiddenItems[0x19] = "FLAG_HIDDEN_ITEM_ROUTE_10_1";
            s_flagDetailsHiddenItems[0x1A] = "FLAG_HIDDEN_ITEM_POWER_PLANT_0";
            s_flagDetailsHiddenItems[0x1B] = "FLAG_HIDDEN_ITEM_POWER_PLANT_1";
            s_flagDetailsHiddenItems[0x1C] = "FLAG_HIDDEN_ITEM_ROUTE_11_0";
            s_flagDetailsHiddenItems[0x1D] = "FLAG_HIDDEN_ITEM_ROUTE_12_0";
            s_flagDetailsHiddenItems[0x1E] = "FLAG_HIDDEN_ITEM_ROUTE_13_0";
            s_flagDetailsHiddenItems[0x1F] = "FLAG_HIDDEN_ITEM_ROUTE_13_1";
            s_flagDetailsHiddenItems[0x20] = "FLAG_HIDDEN_ITEM_ROUTE_17_0";
            s_flagDetailsHiddenItems[0x21] = "FLAG_HIDDEN_ITEM_ROUTE_17_1";
            s_flagDetailsHiddenItems[0x22] = "FLAG_HIDDEN_ITEM_ROUTE_17_2";
            s_flagDetailsHiddenItems[0x23] = "FLAG_HIDDEN_ITEM_ROUTE_17_3";
            s_flagDetailsHiddenItems[0x24] = "FLAG_HIDDEN_ITEM_ROUTE_17_4";
            s_flagDetailsHiddenItems[0x25] = "FLAG_HIDDEN_ITEM_ROUTE_23_0";
            s_flagDetailsHiddenItems[0x26] = "FLAG_HIDDEN_ITEM_ROUTE_23_1";
            s_flagDetailsHiddenItems[0x27] = "FLAG_HIDDEN_ITEM_ROUTE_23_2";
            s_flagDetailsHiddenItems[0x28] = "FLAG_HIDDEN_ITEM_VICTORY_ROAD_2F_0";
            s_flagDetailsHiddenItems[0x29] = "FLAG_HIDDEN_ITEM_VICTORY_ROAD_2F_1";
            s_flagDetailsHiddenItems[0x2A] = "FLAG_HIDDEN_ITEM_ROUTE_25_0";
            s_flagDetailsHiddenItems[0x2B] = "FLAG_HIDDEN_ITEM_ROUTE_25_1";
            s_flagDetailsHiddenItems[0x2C] = "FLAG_HIDDEN_ITEM_ROUTE_4_0";
            s_flagDetailsHiddenItems[0x2D] = "FLAG_HIDDEN_ITEM_ROUTE_9_0";
            s_flagDetailsHiddenItems[0x2E] = "FLAG_HIDDEN_ITEM_COPYCATS_HOUSE_2F_0";
            s_flagDetailsHiddenItems[0x2F] = "FLAG_HIDDEN_ITEM_VIRIDIAN_CITY_0";
            s_flagDetailsHiddenItems[0x30] = "FLAG_HIDDEN_ITEM_CERULEAN_CITY_0";
            s_flagDetailsHiddenItems[0x31] = "FLAG_HIDDEN_ITEM_CERULEAN_CAVE_1F_0";
            s_flagDetailsHiddenItems[0x32] = "FLAG_HIDDEN_ITEM_POKEMON_TOWER_5F_0";
            s_flagDetailsHiddenItems[0x33] = "FLAG_HIDDEN_ITEM_VERMILION_CITY_0";
            s_flagDetailsHiddenItems[0x34] = "FLAG_HIDDEN_ITEM_CELADON_CITY_0";
            //s_flagDetailsHiddenItems[0x35] = "FLAG_HIDDEN_ITEM_SAFARI_ZONE_GATE_0"; // inaccessible
            s_flagDetailsHiddenItems[0x36] = "FLAG_HIDDEN_ITEM_POKEMON_MANSION_1F_0";

            // Normal Items
            s_flagDetailsNormalItems[0x1A] = "FLAG_ITEM_ROUTE_2_ITEM_1";
            s_flagDetailsNormalItems[0x1B] = "FLAG_ITEM_ROUTE_2_ITEM_2";
            s_flagDetailsNormalItems[0x1C] = "FLAG_ITEM_ROUTE_4_ITEM";
            s_flagDetailsNormalItems[0x1D] = "FLAG_ITEM_ROUTE_9_ITEM";
            s_flagDetailsNormalItems[0x1F] = "FLAG_ITEM_ROUTE_12_ITEM_1";
            s_flagDetailsNormalItems[0x20] = "FLAG_ITEM_ROUTE_12_ITEM_2";
            s_flagDetailsNormalItems[0x21] = "FLAG_ITEM_ROUTE_15_ITEM";
            s_flagDetailsNormalItems[0x26] = "FLAG_ITEM_ROUTE_24_ITEM";
            s_flagDetailsNormalItems[0x27] = "FLAG_ITEM_ROUTE_25_ITEM";
            s_flagDetailsNormalItems[0x32] = "FLAG_ITEM_VIRIDIAN_GYM_ITEM";
            s_flagDetailsNormalItems[0x35] = "FLAG_ITEM_CERULEAN_CAVE_1F_ITEM_1";
            s_flagDetailsNormalItems[0x36] = "FLAG_ITEM_CERULEAN_CAVE_1F_ITEM_2";
            s_flagDetailsNormalItems[0x37] = "FLAG_ITEM_CERULEAN_CAVE_1F_ITEM_3";
            s_flagDetailsNormalItems[0x38] = "FLAG_ITEM_CERULEAN_CAVE_1F_ITEM_4";
            s_flagDetailsNormalItems[0x3A] = "FLAG_ITEM_POKEMON_TOWER_3F_ITEM";
            s_flagDetailsNormalItems[0x3B] = "FLAG_ITEM_POKEMON_TOWER_4F_ITEM_1";
            s_flagDetailsNormalItems[0x3C] = "FLAG_ITEM_POKEMON_TOWER_4F_ITEM_2";
            s_flagDetailsNormalItems[0x3D] = "FLAG_ITEM_POKEMON_TOWER_4F_ITEM_3";
            s_flagDetailsNormalItems[0x3E] = "FLAG_ITEM_POKEMON_TOWER_5F_ITEM";
            s_flagDetailsNormalItems[0x3F] = "FLAG_ITEM_POKEMON_TOWER_6F_ITEM_1";
            s_flagDetailsNormalItems[0x40] = "FLAG_ITEM_POKEMON_TOWER_6F_ITEM_2";
            s_flagDetailsNormalItems[0x47] = "FLAG_ITEM_WARDENS_HOUSE_ITEM";
            s_flagDetailsNormalItems[0x48] = "FLAG_ITEM_POKEMON_MANSION_1F_ITEM_1";
            s_flagDetailsNormalItems[0x49] = "FLAG_ITEM_POKEMON_MANSION_1F_ITEM_2";
            s_flagDetailsNormalItems[0x56] = "FLAG_ITEM_POWER_PLANT_ITEM_1";
            s_flagDetailsNormalItems[0x57] = "FLAG_ITEM_POWER_PLANT_ITEM_2";
            s_flagDetailsNormalItems[0x58] = "FLAG_ITEM_POWER_PLANT_ITEM_3";
            s_flagDetailsNormalItems[0x59] = "FLAG_ITEM_POWER_PLANT_ITEM_4";
            s_flagDetailsNormalItems[0x5A] = "FLAG_ITEM_POWER_PLANT_ITEM_5";
            s_flagDetailsNormalItems[0x5C] = "FLAG_ITEM_VICTORY_ROAD_2F_ITEM_1";
            s_flagDetailsNormalItems[0x5D] = "FLAG_ITEM_VICTORY_ROAD_2F_ITEM_2";
            s_flagDetailsNormalItems[0x5E] = "FLAG_ITEM_VICTORY_ROAD_2F_ITEM_3";
            s_flagDetailsNormalItems[0x5F] = "FLAG_ITEM_VICTORY_ROAD_2F_ITEM_4";
            s_flagDetailsNormalItems[0x64] = "FLAG_ITEM_VIRIDIAN_FOREST_ITEM_1";
            s_flagDetailsNormalItems[0x65] = "FLAG_ITEM_VIRIDIAN_FOREST_ITEM_2";
            s_flagDetailsNormalItems[0x66] = "FLAG_ITEM_VIRIDIAN_FOREST_ITEM_3";
            s_flagDetailsNormalItems[0x67] = "FLAG_ITEM_MT_MOON_1F_ITEM_1";
            s_flagDetailsNormalItems[0x68] = "FLAG_ITEM_MT_MOON_1F_ITEM_2";
            s_flagDetailsNormalItems[0x69] = "FLAG_ITEM_MT_MOON_1F_ITEM_3";
            s_flagDetailsNormalItems[0x6A] = "FLAG_ITEM_MT_MOON_1F_ITEM_4";
            s_flagDetailsNormalItems[0x6B] = "FLAG_ITEM_MT_MOON_1F_ITEM_5";
            s_flagDetailsNormalItems[0x6C] = "FLAG_ITEM_MT_MOON_1F_ITEM_6";
            s_flagDetailsNormalItems[0x71] = "FLAG_ITEM_MT_MOON_B2F_ITEM_1";
            s_flagDetailsNormalItems[0x72] = "FLAG_ITEM_MT_MOON_B2F_ITEM_2";
            s_flagDetailsNormalItems[0x74] = "FLAG_ITEM_SS_ANNE_1F_ROOMS_ITEM";
            s_flagDetailsNormalItems[0x75] = "FLAG_ITEM_SS_ANNE_2F_ROOMS_ITEM_1";
            s_flagDetailsNormalItems[0x76] = "FLAG_ITEM_SS_ANNE_2F_ROOMS_ITEM_2";
            s_flagDetailsNormalItems[0x77] = "FLAG_ITEM_SS_ANNE_B1F_ROOMS_ITEM_1";
            s_flagDetailsNormalItems[0x78] = "FLAG_ITEM_SS_ANNE_B1F_ROOMS_ITEM_2";
            s_flagDetailsNormalItems[0x79] = "FLAG_ITEM_SS_ANNE_B1F_ROOMS_ITEM_3";
            s_flagDetailsNormalItems[0x7A] = "FLAG_ITEM_VICTORY_ROAD_3F_ITEM_1";
            s_flagDetailsNormalItems[0x7B] = "FLAG_ITEM_VICTORY_ROAD_3F_ITEM_2";
            s_flagDetailsNormalItems[0x7D] = "FLAG_ITEM_ROCKET_HIDEOUT_B1F_ITEM_1";
            s_flagDetailsNormalItems[0x7E] = "FLAG_ITEM_ROCKET_HIDEOUT_B1F_ITEM_2";
            s_flagDetailsNormalItems[0x7F] = "FLAG_ITEM_ROCKET_HIDEOUT_B2F_ITEM_1";
            s_flagDetailsNormalItems[0x80] = "FLAG_ITEM_ROCKET_HIDEOUT_B2F_ITEM_2";
            s_flagDetailsNormalItems[0x81] = "FLAG_ITEM_ROCKET_HIDEOUT_B2F_ITEM_3";
            s_flagDetailsNormalItems[0x82] = "FLAG_ITEM_ROCKET_HIDEOUT_B2F_ITEM_4";
            s_flagDetailsNormalItems[0x83] = "FLAG_ITEM_ROCKET_HIDEOUT_B3F_ITEM_1";
            s_flagDetailsNormalItems[0x84] = "FLAG_ITEM_ROCKET_HIDEOUT_B3F_ITEM_2";
            s_flagDetailsNormalItems[0x88] = "FLAG_ITEM_ROCKET_HIDEOUT_B4F_ITEM_1";
            s_flagDetailsNormalItems[0x89] = "FLAG_ITEM_ROCKET_HIDEOUT_B4F_ITEM_2";
            s_flagDetailsNormalItems[0x8A] = "FLAG_ITEM_ROCKET_HIDEOUT_B4F_ITEM_3";
            s_flagDetailsNormalItems[0x8B] = "FLAG_ITEM_ROCKET_HIDEOUT_B4F_ITEM_4";
            s_flagDetailsNormalItems[0x8C] = "FLAG_ITEM_ROCKET_HIDEOUT_B4F_ITEM_5";
            s_flagDetailsNormalItems[0x94] = "FLAG_ITEM_SILPH_CO_3F_ITEM";
            s_flagDetailsNormalItems[0x98] = "FLAG_ITEM_SILPH_CO_4F_ITEM_1";
            s_flagDetailsNormalItems[0x99] = "FLAG_ITEM_SILPH_CO_4F_ITEM_2";
            s_flagDetailsNormalItems[0x9A] = "FLAG_ITEM_SILPH_CO_4F_ITEM_3";
            s_flagDetailsNormalItems[0x9F] = "FLAG_ITEM_SILPH_CO_5F_ITEM_1";
            s_flagDetailsNormalItems[0xA0] = "FLAG_ITEM_SILPH_CO_5F_ITEM_2";
            s_flagDetailsNormalItems[0xA1] = "FLAG_ITEM_SILPH_CO_5F_ITEM_3";
            s_flagDetailsNormalItems[0xA5] = "FLAG_ITEM_SILPH_CO_6F_ITEM_1";
            s_flagDetailsNormalItems[0xA6] = "FLAG_ITEM_SILPH_CO_6F_ITEM_2";
            s_flagDetailsNormalItems[0xAC] = "FLAG_ITEM_SILPH_CO_7F_ITEM_1";
            s_flagDetailsNormalItems[0xAD] = "FLAG_ITEM_SILPH_CO_7F_ITEM_2";
            s_flagDetailsNormalItems[0xB8] = "FLAG_ITEM_SILPH_CO_10F_ITEM_1";
            s_flagDetailsNormalItems[0xB9] = "FLAG_ITEM_SILPH_CO_10F_ITEM_2";
            s_flagDetailsNormalItems[0xBA] = "FLAG_ITEM_SILPH_CO_10F_ITEM_3";
            s_flagDetailsNormalItems[0xC0] = "FLAG_ITEM_POKEMON_MANSION_2F_ITEM";
            s_flagDetailsNormalItems[0xC1] = "FLAG_ITEM_POKEMON_MANSION_3F_ITEM_1";
            s_flagDetailsNormalItems[0xC2] = "FLAG_ITEM_POKEMON_MANSION_3F_ITEM_2";
            s_flagDetailsNormalItems[0xC3] = "FLAG_ITEM_POKEMON_MANSION_B1F_ITEM_1";
            s_flagDetailsNormalItems[0xC4] = "FLAG_ITEM_POKEMON_MANSION_B1F_ITEM_2";
            s_flagDetailsNormalItems[0xC5] = "FLAG_ITEM_POKEMON_MANSION_B1F_ITEM_3";
            s_flagDetailsNormalItems[0xC6] = "FLAG_ITEM_POKEMON_MANSION_B1F_ITEM_4";
            s_flagDetailsNormalItems[0xC7] = "FLAG_ITEM_POKEMON_MANSION_B1F_ITEM_5";
            s_flagDetailsNormalItems[0xC8] = "FLAG_ITEM_SAFARI_ZONE_EAST_ITEM_1";
            s_flagDetailsNormalItems[0xC9] = "FLAG_ITEM_SAFARI_ZONE_EAST_ITEM_2";
            s_flagDetailsNormalItems[0xCA] = "FLAG_ITEM_SAFARI_ZONE_EAST_ITEM_3";
            s_flagDetailsNormalItems[0xCB] = "FLAG_ITEM_SAFARI_ZONE_EAST_ITEM_4";
            s_flagDetailsNormalItems[0xCC] = "FLAG_ITEM_SAFARI_ZONE_NORTH_ITEM_1";
            s_flagDetailsNormalItems[0xCD] = "FLAG_ITEM_SAFARI_ZONE_NORTH_ITEM_2";
            s_flagDetailsNormalItems[0xCE] = "FLAG_ITEM_SAFARI_ZONE_WEST_ITEM_1";
            s_flagDetailsNormalItems[0xCF] = "FLAG_ITEM_SAFARI_ZONE_WEST_ITEM_2";
            s_flagDetailsNormalItems[0xD0] = "FLAG_ITEM_SAFARI_ZONE_WEST_ITEM_3";
            s_flagDetailsNormalItems[0xD1] = "FLAG_ITEM_SAFARI_ZONE_WEST_ITEM_4";
            s_flagDetailsNormalItems[0xD2] = "FLAG_ITEM_SAFARI_ZONE_CENTER_ITEM";
            s_flagDetailsNormalItems[0xD3] = "FLAG_ITEM_CERULEAN_CAVE_2F_ITEM_1";
            s_flagDetailsNormalItems[0xD4] = "FLAG_ITEM_CERULEAN_CAVE_2F_ITEM_2";
            s_flagDetailsNormalItems[0xD5] = "FLAG_ITEM_CERULEAN_CAVE_2F_ITEM_3";
            s_flagDetailsNormalItems[0xD6] = "FLAG_ITEM_CERULEAN_CAVE_2F_ITEM_4";
            s_flagDetailsNormalItems[0xD8] = "FLAG_ITEM_CERULEAN_CAVE_B1F_ITEM_1";
            s_flagDetailsNormalItems[0xD9] = "FLAG_ITEM_CERULEAN_CAVE_B1F_ITEM_2";
            s_flagDetailsHiddenItems[0xDA] = "FLAG_ITEM_CERULEAN_CAVE_B1F_ITEM_3";
            s_flagDetailsHiddenItems[0xDB] = "FLAG_ITEM_CERULEAN_CAVE_B1F_ITEM_4";
            s_flagDetailsHiddenItems[0xDC] = "FLAG_ITEM_VICTORY_ROAD_1F_ITEM_1";
            s_flagDetailsHiddenItems[0xDD] = "FLAG_ITEM_VICTORY_ROAD_1F_ITEM_2";
        }

    }

}

