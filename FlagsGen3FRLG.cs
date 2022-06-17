using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal class FlagsGen3FRLG : FlagsOrganizer
    {

        protected override void InitFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;
            m_eventFlags = (m_savFile as IEventFlagArray).GetEventFlags();
            m_missingEventFlagsList.Clear();
        }


        public override void MarkFlags(FlagType flagType)
        {
            ChangeFlagsVal(flagType, value: true);
        }

        public override void UnmarkFlags(FlagType flagType)
        {
            ChangeFlagsVal(flagType, value: false);
        }


        void ChangeFlagsVal(FlagType flagType, bool value)
        {
            var flagHelper = (m_savFile as IEventFlagArray);

            switch (flagType)
            {
                case FlagType.HiddenItem:
                    for (int i = 0x3E8; i <= 0x4A6; ++i)
                        flagHelper.SetEventFlag(i, value);
                    break;

                case FlagType.FieldItem:
                    for (int i = 0x154; i <= 0x1FE; ++i)
                        flagHelper.SetEventFlag(i, value);
                    break;

                    /*case FlagType.TrainerBattle:
                        for (int i = 0; i < 1024; ++i)
                            flagHelper.SetEventFlag(0x550 + i, value);
                        break;*/
            }
        }

        protected override void CheckAllMissingFlags()
        {
            // Hidden Items
            CheckMissingFlag(0x3E8, FlagType.HiddenItem, "", "VIRIDIAN_FOREST_POTION");
            CheckMissingFlag(0x3E9, FlagType.HiddenItem, "", "VIRIDIAN_FOREST_ANTIDOTE");
            CheckMissingFlag(0x3EA, FlagType.HiddenItem, "", "MT_MOON_B2F_MOON_STONE");
            CheckMissingFlag(0x3EB, FlagType.HiddenItem, "", "MT_MOON_B2F_ETHER");
            CheckMissingFlag(0x3EC, FlagType.HiddenItem, "", "ROUTE25_ELIXIR");
            CheckMissingFlag(0x3ED, FlagType.HiddenItem, "", "ROUTE25_ETHER");
            CheckMissingFlag(0x3EE, FlagType.HiddenItem, "", "ROUTE9_ETHER");
            CheckMissingFlag(0x3EF, FlagType.HiddenItem, "", "UNUSED_0x07");
            CheckMissingFlag(0x3F0, FlagType.HiddenItem, "", "SSANNE_B1F_CORRIDOR_HYPER_POTION");
            CheckMissingFlag(0x3F1, FlagType.HiddenItem, "", "ROUTE10_SUPER_POTION");
            CheckMissingFlag(0x3F2, FlagType.HiddenItem, "", "ROUTE10_MAX_ETHER");
            CheckMissingFlag(0x3F3, FlagType.HiddenItem, "", "ROCKET_HIDEOUT_B1F_PP_UP");
            CheckMissingFlag(0x3F4, FlagType.HiddenItem, "", "ROCKET_HIDEOUT_B3F_NUGGET");
            CheckMissingFlag(0x3F5, FlagType.HiddenItem, "", "ROCKET_HIDEOUT_B4F_NEST_BALL");
            CheckMissingFlag(0x3F6, FlagType.HiddenItem, "", "POKEMON_TOWER_5F_BIG_MUSHROOM");
            CheckMissingFlag(0x3F7, FlagType.HiddenItem, "", "ROUTE13_PP_UP");
            CheckMissingFlag(0x3F8, FlagType.HiddenItem, "", "UNUSED_0x10");
            CheckMissingFlag(0x3F9, FlagType.HiddenItem, "", "ROUTE17_RARE_CANDY");
            CheckMissingFlag(0x3FA, FlagType.HiddenItem, "", "ROUTE17_FULL_RESTORE");
            CheckMissingFlag(0x3FB, FlagType.HiddenItem, "", "ROUTE17_PP_UP");
            CheckMissingFlag(0x3FC, FlagType.HiddenItem, "", "ROUTE17_MAX_REVIVE");
            CheckMissingFlag(0x3FD, FlagType.HiddenItem, "", "ROUTE17_MAX_ELIXIR");
            CheckMissingFlag(0x3FE, FlagType.HiddenItem, "", "SAFARI_ZONE_CENTER_LEAF_STONE");
            CheckMissingFlag(0x3FF, FlagType.HiddenItem, "", "SAFARI_ZONE_WEST_REVIVE");
            CheckMissingFlag(0x400, FlagType.HiddenItem, "", "SILPH_CO_5F_ELIXIR");
            CheckMissingFlag(0x401, FlagType.HiddenItem, "", "SILPH_CO_9F_MAX_POTION");
            CheckMissingFlag(0x402, FlagType.HiddenItem, "", "SAFFRON_CITY_COPYCATS_HOUSE_2F_NUGGET");
            CheckMissingFlag(0x403, FlagType.HiddenItem, "", "POWER_PLANT_MAX_ELIXIR");
            CheckMissingFlag(0x404, FlagType.HiddenItem, "", "POWER_PLANT_THUNDER_STONE");
            CheckMissingFlag(0x405, FlagType.HiddenItem, "", "SEAFOAM_ISLANDS_B3F_NUGGET");
            CheckMissingFlag(0x406, FlagType.HiddenItem, "", "SEAFOAM_ISLANDS_B4F_WATER_STONE");
            CheckMissingFlag(0x407, FlagType.HiddenItem, "", "POKEMON_MANSION_1F_MOON_STONE");
            CheckMissingFlag(0x408, FlagType.HiddenItem, "", "POKEMON_MANSION_3F_RARE_CANDY");
            CheckMissingFlag(0x409, FlagType.HiddenItem, "", "POKEMON_MANSION_B1F_ELIXIR");
            CheckMissingFlag(0x40A, FlagType.HiddenItem, "", "ROUTE23_FULL_RESTORE");
            CheckMissingFlag(0x40B, FlagType.HiddenItem, "", "ROUTE23_ULTRA_BALL");
            CheckMissingFlag(0x40C, FlagType.HiddenItem, "", "ROUTE23_MAX_ETHER");
            CheckMissingFlag(0x40D, FlagType.HiddenItem, "", "VICTORY_ROAD_1F_ULTRA_BALL");
            CheckMissingFlag(0x40E, FlagType.HiddenItem, "", "VICTORY_ROAD_1F_FULL_RESTORE");
            CheckMissingFlag(0x40F, FlagType.HiddenItem, "", "CERULEAN_CAVE_1F_ULTRA_BALL");
            CheckMissingFlag(0x410, FlagType.HiddenItem, "", "UNUSED_0x28");
            CheckMissingFlag(0x411, FlagType.HiddenItem, "", "ROUTE11_ESCAPE_ROPE");
            CheckMissingFlag(0x412, FlagType.HiddenItem, "", "ROUTE12_HYPER_POTION");
            CheckMissingFlag(0x413, FlagType.HiddenItem, "", "UNUSED_0x2B");
            CheckMissingFlag(0x414, FlagType.HiddenItem, "", "UNUSED_0x2C");
            CheckMissingFlag(0x415, FlagType.HiddenItem, "", "UNUSED_0x2D");
            CheckMissingFlag(0x416, FlagType.HiddenItem, "", "UNUSED_0x2E");
            CheckMissingFlag(0x417, FlagType.HiddenItem, "", "CELADON_CITY_PP_UP");
            CheckMissingFlag(0x418, FlagType.HiddenItem, "", "VERMILION_CITY_MAX_ETHER");
            CheckMissingFlag(0x419, FlagType.HiddenItem, "", "CERULEAN_CITY_RARE_CANDY");
            CheckMissingFlag(0x41A, FlagType.HiddenItem, "", "ROUTE4_GREAT_BALL");
            CheckMissingFlag(0x41B, FlagType.HiddenItem, "", "CELADON_CITY_GAME_CORNER_COINS");
            CheckMissingFlag(0x41C, FlagType.HiddenItem, "", "CELADON_CITY_GAME_CORNER_COINS_2");
            CheckMissingFlag(0x41D, FlagType.HiddenItem, "", "CELADON_CITY_GAME_CORNER_COINS_3");
            CheckMissingFlag(0x41E, FlagType.HiddenItem, "", "CELADON_CITY_GAME_CORNER_COINS_4");
            CheckMissingFlag(0x41F, FlagType.HiddenItem, "", "CELADON_CITY_GAME_CORNER_COINS_5");
            CheckMissingFlag(0x420, FlagType.HiddenItem, "", "CELADON_CITY_GAME_CORNER_COINS_6");
            CheckMissingFlag(0x421, FlagType.HiddenItem, "", "CELADON_CITY_GAME_CORNER_COINS_7");
            CheckMissingFlag(0x422, FlagType.HiddenItem, "", "CELADON_CITY_GAME_CORNER_COINS_8");
            CheckMissingFlag(0x423, FlagType.HiddenItem, "", "CELADON_CITY_GAME_CORNER_COINS_9");
            CheckMissingFlag(0x424, FlagType.HiddenItem, "", "CELADON_CITY_GAME_CORNER_COINS_10");
            CheckMissingFlag(0x425, FlagType.HiddenItem, "", "CELADON_CITY_GAME_CORNER_COINS_11");
            CheckMissingFlag(0x426, FlagType.HiddenItem, "", "CELADON_CITY_GAME_CORNER_COINS_12");
            CheckMissingFlag(0x427, FlagType.HiddenItem, "", "SEVEN_ISLAND_SEVAULT_CANYON_CHERI_BERRY");
            CheckMissingFlag(0x428, FlagType.HiddenItem, "", "SEVEN_ISLAND_TANOBY_RUINS_HEART_SCALE_4");
            CheckMissingFlag(0x429, FlagType.HiddenItem, "", "SEVEN_ISLAND_TANOBY_RUINS_HEART_SCALE");
            CheckMissingFlag(0x42A, FlagType.HiddenItem, "", "SEVEN_ISLAND_TANOBY_RUINS_HEART_SCALE_2");
            CheckMissingFlag(0x42B, FlagType.HiddenItem, "", "SEVEN_ISLAND_TANOBY_RUINS_HEART_SCALE_3");
            CheckMissingFlag(0x42C, FlagType.HiddenItem, "", "FIVE_ISLAND_ROCKET_WAREHOUSE_NEST_BALL");
            CheckMissingFlag(0x42D, FlagType.HiddenItem, "", "FIVE_ISLAND_ROCKET_WAREHOUSE_NET_BALL");
            CheckMissingFlag(0x42E, FlagType.HiddenItem, "", "UNDERGROUND_PATH_NORTH_SOUTH_TUNNEL_POTION");
            CheckMissingFlag(0x42F, FlagType.HiddenItem, "", "UNDERGROUND_PATH_NORTH_SOUTH_TUNNEL_ANTIDOTE");
            CheckMissingFlag(0x430, FlagType.HiddenItem, "", "UNDERGROUND_PATH_NORTH_SOUTH_TUNNEL_PARALYZE_HEAL");
            CheckMissingFlag(0x431, FlagType.HiddenItem, "", "UNDERGROUND_PATH_NORTH_SOUTH_TUNNEL_AWAKENING");
            CheckMissingFlag(0x432, FlagType.HiddenItem, "", "UNDERGROUND_PATH_NORTH_SOUTH_TUNNEL_BURN_HEAL");
            CheckMissingFlag(0x433, FlagType.HiddenItem, "", "UNDERGROUND_PATH_NORTH_SOUTH_TUNNEL_ICE_HEAL");
            CheckMissingFlag(0x434, FlagType.HiddenItem, "", "UNDERGROUND_PATH_NORTH_SOUTH_TUNNEL_ETHER");
            CheckMissingFlag(0x435, FlagType.HiddenItem, "", "UNDERGROUND_PATH_EAST_WEST_TUNNEL_POTION");
            CheckMissingFlag(0x436, FlagType.HiddenItem, "", "UNDERGROUND_PATH_EAST_WEST_TUNNEL_ANTIDOTE");
            CheckMissingFlag(0x437, FlagType.HiddenItem, "", "UNDERGROUND_PATH_EAST_WEST_TUNNEL_PARALYZE_HEAL");
            CheckMissingFlag(0x438, FlagType.HiddenItem, "", "UNDERGROUND_PATH_EAST_WEST_TUNNEL_AWAKENING");
            CheckMissingFlag(0x439, FlagType.HiddenItem, "", "UNDERGROUND_PATH_EAST_WEST_TUNNEL_BURN_HEAL");
            CheckMissingFlag(0x43A, FlagType.HiddenItem, "", "UNDERGROUND_PATH_EAST_WEST_TUNNEL_ICE_HEAL");
            CheckMissingFlag(0x43B, FlagType.HiddenItem, "", "UNDERGROUND_PATH_EAST_WEST_TUNNEL_ETHER");
            CheckMissingFlag(0x43C, FlagType.HiddenItem, "", "MT_MOON_B1F_TINY_MUSHROOM");
            CheckMissingFlag(0x43D, FlagType.HiddenItem, "", "MT_MOON_B1F_TINY_MUSHROOM_2");
            CheckMissingFlag(0x43E, FlagType.HiddenItem, "", "MT_MOON_B1F_TINY_MUSHROOM_3");
            CheckMissingFlag(0x43F, FlagType.HiddenItem, "", "MT_MOON_B1F_BIG_MUSHROOM");
            CheckMissingFlag(0x440, FlagType.HiddenItem, "", "MT_MOON_B1F_BIG_MUSHROOM_2");
            CheckMissingFlag(0x441, FlagType.HiddenItem, "", "MT_MOON_B1F_BIG_MUSHROOM_3");
            CheckMissingFlag(0x442, FlagType.HiddenItem, "", "THREE_ISLAND_BERRY_FOREST_RAZZ_BERRY");
            CheckMissingFlag(0x443, FlagType.HiddenItem, "", "THREE_ISLAND_BERRY_FOREST_BLUK_BERRY");
            CheckMissingFlag(0x444, FlagType.HiddenItem, "", "THREE_ISLAND_BERRY_FOREST_NANAB_BERRY");
            CheckMissingFlag(0x445, FlagType.HiddenItem, "", "THREE_ISLAND_BERRY_FOREST_WEPEAR_BERRY");
            CheckMissingFlag(0x446, FlagType.HiddenItem, "", "THREE_ISLAND_BERRY_FOREST_ORAN_BERRY");
            CheckMissingFlag(0x447, FlagType.HiddenItem, "", "THREE_ISLAND_BERRY_FOREST_CHERI_BERRY");
            CheckMissingFlag(0x448, FlagType.HiddenItem, "", "THREE_ISLAND_BERRY_FOREST_CHESTO_BERRY");
            CheckMissingFlag(0x449, FlagType.HiddenItem, "", "THREE_ISLAND_BERRY_FOREST_PECHA_BERRY");
            CheckMissingFlag(0x44A, FlagType.HiddenItem, "", "THREE_ISLAND_BERRY_FOREST_RAWST_BERRY");
            CheckMissingFlag(0x44B, FlagType.HiddenItem, "", "THREE_ISLAND_BERRY_FOREST_ASPEAR_BERRY");
            CheckMissingFlag(0x44C, FlagType.HiddenItem, "", "THREE_ISLAND_BERRY_FOREST_PERSIM_BERRY");
            CheckMissingFlag(0x44D, FlagType.HiddenItem, "", "THREE_ISLAND_BERRY_FOREST_PINAP_BERRY");
            CheckMissingFlag(0x44E, FlagType.HiddenItem, "", "THREE_ISLAND_BERRY_FOREST_LUM_BERRY");
            CheckMissingFlag(0x44F, FlagType.HiddenItem, "", "ONE_ISLAND_TREASURE_BEACH_STARDUST");
            CheckMissingFlag(0x450, FlagType.HiddenItem, "", "ONE_ISLAND_TREASURE_BEACH_STARDUST_2");
            CheckMissingFlag(0x451, FlagType.HiddenItem, "", "ONE_ISLAND_TREASURE_BEACH_PEARL");
            CheckMissingFlag(0x452, FlagType.HiddenItem, "", "ONE_ISLAND_TREASURE_BEACH_PEARL_2");
            CheckMissingFlag(0x453, FlagType.HiddenItem, "", "ONE_ISLAND_TREASURE_BEACH_ULTRA_BALL");
            CheckMissingFlag(0x454, FlagType.HiddenItem, "", "ONE_ISLAND_TREASURE_BEACH_ULTRA_BALL_2");
            CheckMissingFlag(0x455, FlagType.HiddenItem, "", "ONE_ISLAND_TREASURE_BEACH_STAR_PIECE");
            CheckMissingFlag(0x456, FlagType.HiddenItem, "", "ONE_ISLAND_TREASURE_BEACH_BIG_PEARL");
            CheckMissingFlag(0x457, FlagType.HiddenItem, "", "TWO_ISLAND_CAPE_BRINK_RARE_CANDY");
            CheckMissingFlag(0x458, FlagType.HiddenItem, "", "PEWTER_CITY_POKE_BALL");
            CheckMissingFlag(0x459, FlagType.HiddenItem, "", "ROUTE3_ORAN_BERRY");
            CheckMissingFlag(0x45A, FlagType.HiddenItem, "", "ROUTE4_PERSIM_BERRY");
            CheckMissingFlag(0x45B, FlagType.HiddenItem, "", "ROUTE24_PECHA_BERRY");
            CheckMissingFlag(0x45C, FlagType.HiddenItem, "", "ROUTE25_ORAN_BERRY");
            CheckMissingFlag(0x45D, FlagType.HiddenItem, "", "ROUTE25_BLUK_BERRY");
            CheckMissingFlag(0x45E, FlagType.HiddenItem, "", "ROUTE6_SITRUS_BERRY");
            CheckMissingFlag(0x45F, FlagType.HiddenItem, "", "ROUTE6_RARE_CANDY");
            CheckMissingFlag(0x460, FlagType.HiddenItem, "", "SSANNE_KITCHEN_PECHA_BERRY");
            CheckMissingFlag(0x461, FlagType.HiddenItem, "", "SSANNE_KITCHEN_CHERI_BERRY");
            CheckMissingFlag(0x462, FlagType.HiddenItem, "", "SSANNE_KITCHEN_CHESTO_BERRY");
            CheckMissingFlag(0x463, FlagType.HiddenItem, "", "ROUTE9_RARE_CANDY");
            CheckMissingFlag(0x464, FlagType.HiddenItem, "", "UNUSED_0x7C");
            CheckMissingFlag(0x465, FlagType.HiddenItem, "", "ROUTE10_PERSIM_BERRY");
            CheckMissingFlag(0x466, FlagType.HiddenItem, "", "ROUTE10_CHERI_BERRY");
            CheckMissingFlag(0x467, FlagType.HiddenItem, "", "ROUTE8_RAWST_BERRY");
            CheckMissingFlag(0x468, FlagType.HiddenItem, "", "ROUTE8_LUM_BERRY");
            CheckMissingFlag(0x469, FlagType.HiddenItem, "", "ROUTE8_LEPPA_BERRY");
            CheckMissingFlag(0x46A, FlagType.HiddenItem, "", "ROUTE12_RARE_CANDY");
            CheckMissingFlag(0x46B, FlagType.HiddenItem, "", "ROUTE12_LEFTOVERS");
            CheckMissingFlag(0x46C, FlagType.HiddenItem, "", "ROUTE16_LEFTOVERS");
            CheckMissingFlag(0x46D, FlagType.HiddenItem, "", "FUCHSIA_CITY_MAX_REVIVE");
            CheckMissingFlag(0x46E, FlagType.HiddenItem, "", "ROCKET_HIDEOUT_B4F_NET_BALL");
            CheckMissingFlag(0x46F, FlagType.HiddenItem, "", "SILPH_CO_2F_ULTRA_BALL");
            CheckMissingFlag(0x470, FlagType.HiddenItem, "", "SILPH_CO_3F_PROTEIN");
            CheckMissingFlag(0x471, FlagType.HiddenItem, "", "SILPH_CO_4F_IRON");
            CheckMissingFlag(0x472, FlagType.HiddenItem, "", "SILPH_CO_5F_PP_UP");
            CheckMissingFlag(0x473, FlagType.HiddenItem, "", "SILPH_CO_6F_CARBOS");
            CheckMissingFlag(0x474, FlagType.HiddenItem, "", "SILPH_CO_7F_ZINC");
            CheckMissingFlag(0x475, FlagType.HiddenItem, "", "SILPH_CO_8F_NUGGET");
            CheckMissingFlag(0x476, FlagType.HiddenItem, "", "SILPH_CO_9F_CALCIUM");
            CheckMissingFlag(0x477, FlagType.HiddenItem, "", "SILPH_CO_10F_HP_UP");
            CheckMissingFlag(0x478, FlagType.HiddenItem, "", "SILPH_CO_11F_REVIVE");
            CheckMissingFlag(0x479, FlagType.HiddenItem, "", "ROUTE23_LUM_BERRY");
            CheckMissingFlag(0x47A, FlagType.HiddenItem, "", "ROUTE23_SITRUS_BERRY");
            CheckMissingFlag(0x47B, FlagType.HiddenItem, "", "ROUTE23_ASPEAR_BERRY");
            CheckMissingFlag(0x47C, FlagType.HiddenItem, "", "ROUTE23_LEPPA_BERRY");
            CheckMissingFlag(0x47D, FlagType.HiddenItem, "", "ROUTE14_ZINC");
            CheckMissingFlag(0x47E, FlagType.HiddenItem, "", "ROUTE9_CHESTO_BERRY");
            CheckMissingFlag(0x47F, FlagType.HiddenItem, "", "ROUTE10_NANAB_BERRY");
            CheckMissingFlag(0x480, FlagType.HiddenItem, "", "ROUTE7_WEPEAR_BERRY");
            CheckMissingFlag(0x481, FlagType.HiddenItem, "", "ROUTE20_STARDUST");
            CheckMissingFlag(0x482, FlagType.HiddenItem, "", "ROUTE21_NORTH_PEARL");
            CheckMissingFlag(0x483, FlagType.HiddenItem, "", "ROUTE23_MAX_ELIXIR");
            CheckMissingFlag(0x484, FlagType.HiddenItem, "", "ROUTE4_RAZZ_BERRY");
            CheckMissingFlag(0x485, FlagType.HiddenItem, "", "ROUTE14_PINAP_BERRY");
            CheckMissingFlag(0x486, FlagType.HiddenItem, "", "MT_EMBER_EXTERIOR_FIRE_STONE");
            CheckMissingFlag(0x487, FlagType.HiddenItem, "", "POKEMON_TOWER_7F_SOOTHE_BELL");
            CheckMissingFlag(0x488, FlagType.HiddenItem, "", "NAVEL_ROCK_SUMMIT_SACRED_ASH");
            CheckMissingFlag(0x489, FlagType.HiddenItem, "", "TWO_ISLAND_CAPE_BRINK_PP_MAX");
            CheckMissingFlag(0x48A, FlagType.HiddenItem, "", "MT_EMBER_EXTERIOR_ULTRA_BALL");
            CheckMissingFlag(0x48B, FlagType.HiddenItem, "", "THREE_ISLAND_DUNSPARCE_TUNNEL_NUGGET");
            CheckMissingFlag(0x48C, FlagType.HiddenItem, "", "THREE_ISLAND_PP_UP");
            CheckMissingFlag(0x48D, FlagType.HiddenItem, "", "THREE_ISLAND_BOND_BRIDGE_MAX_REPEL");
            CheckMissingFlag(0x48E, FlagType.HiddenItem, "", "THREE_ISLAND_BOND_BRIDGE_PEARL");
            CheckMissingFlag(0x48F, FlagType.HiddenItem, "", "THREE_ISLAND_BOND_BRIDGE_STARDUST");
            CheckMissingFlag(0x490, FlagType.HiddenItem, "", "FOUR_ISLAND_PEARL");
            CheckMissingFlag(0x491, FlagType.HiddenItem, "", "FOUR_ISLAND_ULTRA_BALL");
            CheckMissingFlag(0x492, FlagType.HiddenItem, "", "FIVE_ISLAND_MEMORIAL_PILLAR_BIG_PEARL");
            CheckMissingFlag(0x493, FlagType.HiddenItem, "", "FIVE_ISLAND_MEMORIAL_PILLAR_RAZZ_BERRY");
            CheckMissingFlag(0x494, FlagType.HiddenItem, "", "FIVE_ISLAND_MEMORIAL_PILLAR_SITRUS_BERRY");
            CheckMissingFlag(0x495, FlagType.HiddenItem, "", "FIVE_ISLAND_MEMORIAL_PILLAR_BLUK_BERRY");
            CheckMissingFlag(0x496, FlagType.HiddenItem, "", "FIVE_ISLAND_RESORT_GORGEOUS_NEST_BALL");
            CheckMissingFlag(0x497, FlagType.HiddenItem, "", "FIVE_ISLAND_RESORT_GORGEOUS_STARDUST");
            CheckMissingFlag(0x498, FlagType.HiddenItem, "", "FIVE_ISLAND_RESORT_GORGEOUS_STAR_PIECE");
            CheckMissingFlag(0x499, FlagType.HiddenItem, "", "FIVE_ISLAND_RESORT_GORGEOUS_STARDUST_2");
            CheckMissingFlag(0x49A, FlagType.HiddenItem, "", "SIX_ISLAND_OUTCAST_ISLAND_STAR_PIECE");
            CheckMissingFlag(0x49B, FlagType.HiddenItem, "", "SIX_ISLAND_OUTCAST_ISLAND_NET_BALL");
            CheckMissingFlag(0x49C, FlagType.HiddenItem, "", "SIX_ISLAND_GREEN_PATH_ULTRA_BALL");
            CheckMissingFlag(0x49D, FlagType.HiddenItem, "", "SIX_ISLAND_WATER_PATH_ASPEAR_BERRY");
            CheckMissingFlag(0x49E, FlagType.HiddenItem, "", "SIX_ISLAND_WATER_PATH_ORAN_BERRY");
            CheckMissingFlag(0x49F, FlagType.HiddenItem, "", "SIX_ISLAND_WATER_PATH_PINAP_BERRY");
            CheckMissingFlag(0x4A0, FlagType.HiddenItem, "", "SIX_ISLAND_LEPPA_BERRY");
            CheckMissingFlag(0x4A1, FlagType.HiddenItem, "", "SEVEN_ISLAND_TRAINER_TOWER_BIG_PEARL");
            CheckMissingFlag(0x4A2, FlagType.HiddenItem, "", "SEVEN_ISLAND_TRAINER_TOWER_PEARL");
            CheckMissingFlag(0x4A3, FlagType.HiddenItem, "", "SEVEN_ISLAND_TRAINER_TOWER_NANAB_BERRY");
            CheckMissingFlag(0x4A4, FlagType.HiddenItem, "", "SEVEN_ISLAND_SEVAULT_CANYON_ENTRANCE_RAWST_BERRY");
            CheckMissingFlag(0x4A5, FlagType.HiddenItem, "", "VIRIDIAN_CITY_GYM_MACHO_BRACE");
            CheckMissingFlag(0x4A6, FlagType.HiddenItem, "", "SSANNE_EXTERIOR_LAVA_COOKIE");

            // Field items
            CheckMissingFlag(0x154, FlagType.FieldItem, "", "ROUTE2_ETHER");
            CheckMissingFlag(0x155, FlagType.FieldItem, "", "ROUTE2_PARALYZE_HEAL");
            CheckMissingFlag(0x156, FlagType.FieldItem, "", "VIRIDIAN_FOREST_POKE_BALL");
            CheckMissingFlag(0x157, FlagType.FieldItem, "", "VIRIDIAN_FOREST_ANTIDOTE");
            CheckMissingFlag(0x158, FlagType.FieldItem, "", "VIRIDIAN_FOREST_POTION");
            CheckMissingFlag(0x159, FlagType.FieldItem, "", "MT_MOON_1F_PARALYZE_HEAL");
            CheckMissingFlag(0x15A, FlagType.FieldItem, "", "MT_MOON_1F_TM09");
            CheckMissingFlag(0x15B, FlagType.FieldItem, "", "MT_MOON_1F_POTION");
            CheckMissingFlag(0x15C, FlagType.FieldItem, "", "MT_MOON_1F_RARE_CANDY");
            CheckMissingFlag(0x15D, FlagType.FieldItem, "", "MT_MOON_1F_ESCAPE_ROPE");
            CheckMissingFlag(0x15E, FlagType.FieldItem, "", "MT_MOON_1F_MOON_STONE");
            CheckMissingFlag(0x15F, FlagType.FieldItem, "", "MT_MOON_B2F_STAR_PIECE");
            CheckMissingFlag(0x160, FlagType.FieldItem, "", "MT_MOON_B2F_TM46");
            CheckMissingFlag(0x161, FlagType.FieldItem, "", "ROUTE4_TM05");
            CheckMissingFlag(0x162, FlagType.FieldItem, "", "ROUTE24_TM45");
            CheckMissingFlag(0x163, FlagType.FieldItem, "", "ROUTE25_TM43");
            CheckMissingFlag(0x164, FlagType.FieldItem, "", "SSANNE_1F_ROOM2_TM31");
            CheckMissingFlag(0x165, FlagType.FieldItem, "", "SSANNE_2F_ROOM2_STARDUST");
            CheckMissingFlag(0x166, FlagType.FieldItem, "", "SSANNE_2F_ROOM4_X_ATTACK");
            CheckMissingFlag(0x167, FlagType.FieldItem, "", "SSANNE_B1F_ROOM2_TM44");
            CheckMissingFlag(0x168, FlagType.FieldItem, "", "SSANNE_B1F_ROOM3_ETHER");
            CheckMissingFlag(0x169, FlagType.FieldItem, "", "SSANNE_B1F_ROOM5_SUPER_POTION");
            CheckMissingFlag(0x16A, FlagType.FieldItem, "", "SSANNE_KITCHEN_GREAT_BALL");
            CheckMissingFlag(0x16B, FlagType.FieldItem, "", "ROUTE9_TM40");
            CheckMissingFlag(0x16C, FlagType.FieldItem, "", "ROCKET_HIDEOUT_B1F_ESCAPE_ROPE");
            CheckMissingFlag(0x16D, FlagType.FieldItem, "", "ROCKET_HIDEOUT_B1F_HYPER_POTION");
            CheckMissingFlag(0x16E, FlagType.FieldItem, "", "ROCKET_HIDEOUT_B2F_X_SPEED");
            CheckMissingFlag(0x16F, FlagType.FieldItem, "", "ROCKET_HIDEOUT_B2F_MOON_STONE");
            CheckMissingFlag(0x170, FlagType.FieldItem, "", "ROCKET_HIDEOUT_B2F_TM12");
            CheckMissingFlag(0x171, FlagType.FieldItem, "", "ROCKET_HIDEOUT_B2F_SUPER_POTION");
            CheckMissingFlag(0x172, FlagType.FieldItem, "", "ROCKET_HIDEOUT_B3F_RARE_CANDY");
            CheckMissingFlag(0x173, FlagType.FieldItem, "", "ROCKET_HIDEOUT_B3F_TM21");
            CheckMissingFlag(0x174, FlagType.FieldItem, "", "ROCKET_HIDEOUT_B4F_TM49");
            CheckMissingFlag(0x175, FlagType.FieldItem, "", "ROCKET_HIDEOUT_B4F_MAX_ETHER");
            CheckMissingFlag(0x176, FlagType.FieldItem, "", "ROCKET_HIDEOUT_B4F_CALCIUM");
            CheckMissingFlag(0x177, FlagType.FieldItem, "", "POKEMON_TOWER_3F_ESCAPE_ROPE");
            CheckMissingFlag(0x178, FlagType.FieldItem, "", "POKEMON_TOWER_4F_ELIXIR");
            CheckMissingFlag(0x179, FlagType.FieldItem, "", "POKEMON_TOWER_4F_AWAKENING");
            CheckMissingFlag(0x17A, FlagType.FieldItem, "", "POKEMON_TOWER_4F_GREAT_BALL");
            CheckMissingFlag(0x17B, FlagType.FieldItem, "", "POKEMON_TOWER_5F_NUGGET");
            CheckMissingFlag(0x17C, FlagType.FieldItem, "", "POKEMON_TOWER_6F_RARE_CANDY");
            CheckMissingFlag(0x17D, FlagType.FieldItem, "", "POKEMON_TOWER_6F_X_ACCURACY");
            CheckMissingFlag(0x17E, FlagType.FieldItem, "", "ROUTE12_TM48");
            CheckMissingFlag(0x17F, FlagType.FieldItem, "", "ROUTE12_IRON");
            CheckMissingFlag(0x180, FlagType.FieldItem, "", "ROUTE15_TM18");
            CheckMissingFlag(0x181, FlagType.FieldItem, "", "SAFARI_ZONE_CENTER_NUGGET");
            CheckMissingFlag(0x182, FlagType.FieldItem, "", "SAFARI_ZONE_EAST_MAX_POTION");
            CheckMissingFlag(0x183, FlagType.FieldItem, "", "SAFARI_ZONE_EAST_FULL_RESTORE");
            CheckMissingFlag(0x184, FlagType.FieldItem, "", "SAFARI_ZONE_EAST_TM11");
            CheckMissingFlag(0x185, FlagType.FieldItem, "", "SAFARI_ZONE_EAST_LEAF_STONE");
            CheckMissingFlag(0x186, FlagType.FieldItem, "", "SAFARI_ZONE_NORTH_PROTEIN");
            CheckMissingFlag(0x187, FlagType.FieldItem, "", "SAFARI_ZONE_NORTH_TM47");
            CheckMissingFlag(0x188, FlagType.FieldItem, "", "SAFARI_ZONE_WEST_TM32");
            CheckMissingFlag(0x189, FlagType.FieldItem, "", "SAFARI_ZONE_WEST_GOLD_TEETH");
            CheckMissingFlag(0x18A, FlagType.FieldItem, "", "SAFARI_ZONE_WEST_MAX_POTION");
            CheckMissingFlag(0x18B, FlagType.FieldItem, "", "SAFARI_ZONE_WEST_MAX_REVIVE");
            CheckMissingFlag(0x18C, FlagType.FieldItem, "", "SILPH_CO_3F_HYPER_POTION");
            CheckMissingFlag(0x18D, FlagType.FieldItem, "", "SILPH_CO_4F_MAX_REVIVE");
            CheckMissingFlag(0x18E, FlagType.FieldItem, "", "SILPH_CO_4F_ESCAPE_ROPE");
            CheckMissingFlag(0x18F, FlagType.FieldItem, "", "SILPH_CO_4F_FULL_HEAL");
            CheckMissingFlag(0x190, FlagType.FieldItem, "", "SILPH_CO_5F_PROTEIN");
            CheckMissingFlag(0x191, FlagType.FieldItem, "", "SILPH_CO_5F_TM01");
            CheckMissingFlag(0x192, FlagType.FieldItem, "", "SILPH_CO_5F_CARD_KEY");
            CheckMissingFlag(0x193, FlagType.FieldItem, "", "SILPH_CO_6F_HP_UP");
            CheckMissingFlag(0x194, FlagType.FieldItem, "", "SILPH_CO_6F_X_SPECIAL");
            CheckMissingFlag(0x195, FlagType.FieldItem, "", "SILPH_CO_7F_CALCIUM");
            CheckMissingFlag(0x196, FlagType.FieldItem, "", "SILPH_CO_7F_TM08");
            CheckMissingFlag(0x197, FlagType.FieldItem, "", "SILPH_CO_10F_CARBOS");
            CheckMissingFlag(0x198, FlagType.FieldItem, "", "SILPH_CO_10F_ULTRA_BALL");
            CheckMissingFlag(0x199, FlagType.FieldItem, "", "SILPH_CO_10F_RARE_CANDY");
            CheckMissingFlag(0x19A, FlagType.FieldItem, "", "POWER_PLANT_MAX_POTION");
            CheckMissingFlag(0x19B, FlagType.FieldItem, "", "POWER_PLANT_TM17");
            CheckMissingFlag(0x19C, FlagType.FieldItem, "", "POWER_PLANT_TM25");
            CheckMissingFlag(0x19D, FlagType.FieldItem, "", "POWER_PLANT_THUNDER_STONE");
            CheckMissingFlag(0x19E, FlagType.FieldItem, "", "POWER_PLANT_ELIXIR");
            CheckMissingFlag(0x19F, FlagType.FieldItem, "", "POKEMON_MANSION_1F_CARBOS");
            CheckMissingFlag(0x1A0, FlagType.FieldItem, "", "POKEMON_MANSION_1F_ESCAPE_ROPE");
            CheckMissingFlag(0x1A1, FlagType.FieldItem, "", "POKEMON_MANSION_2F_CALCIUM");
            CheckMissingFlag(0x1A2, FlagType.FieldItem, "", "POKEMON_MANSION_3F_MAX_POTION");
            CheckMissingFlag(0x1A3, FlagType.FieldItem, "", "POKEMON_MANSION_3F_IRON");
            CheckMissingFlag(0x1A4, FlagType.FieldItem, "", "POKEMON_MANSION_B1F_TM14");
            CheckMissingFlag(0x1A5, FlagType.FieldItem, "", "POKEMON_MANSION_B1F_FULL_RESTORE");
            CheckMissingFlag(0x1A6, FlagType.FieldItem, "", "0x1A6");
            CheckMissingFlag(0x1A7, FlagType.FieldItem, "", "POKEMON_MANSION_B1F_TM22");
            CheckMissingFlag(0x1A8, FlagType.FieldItem, "", "POKEMON_MANSION_B1F_SECRET_KEY");
            CheckMissingFlag(0x1A9, FlagType.FieldItem, "", "VICTORY_ROAD_1F_RARE_CANDY");
            CheckMissingFlag(0x1AA, FlagType.FieldItem, "", "VICTORY_ROAD_1F_TM02");
            CheckMissingFlag(0x1AB, FlagType.FieldItem, "", "VICTORY_ROAD_2F_GUARD_SPEC");
            CheckMissingFlag(0x1AC, FlagType.FieldItem, "", "VICTORY_ROAD_2F_TM07");
            CheckMissingFlag(0x1AD, FlagType.FieldItem, "", "VICTORY_ROAD_2F_FULL_HEAL");
            CheckMissingFlag(0x1AE, FlagType.FieldItem, "", "VICTORY_ROAD_2F_TM37");
            CheckMissingFlag(0x1AF, FlagType.FieldItem, "", "VICTORY_ROAD_3F_MAX_REVIVE");
            CheckMissingFlag(0x1B0, FlagType.FieldItem, "", "VICTORY_ROAD_3F_TM50");
            CheckMissingFlag(0x1B1, FlagType.FieldItem, "", "CERULEAN_CAVE_1F_MAX_ELIXIR");
            CheckMissingFlag(0x1B2, FlagType.FieldItem, "", "CERULEAN_CAVE_1F_NUGGET");
            CheckMissingFlag(0x1B3, FlagType.FieldItem, "", "CERULEAN_CAVE_1F_FULL_RESTORE");
            CheckMissingFlag(0x1B4, FlagType.FieldItem, "", "CERULEAN_CAVE_2F_FULL_RESTORE");
            CheckMissingFlag(0x1B5, FlagType.FieldItem, "", "CERULEAN_CAVE_2F_PP_UP");
            CheckMissingFlag(0x1B6, FlagType.FieldItem, "", "CERULEAN_CAVE_2F_ULTRA_BALL");
            CheckMissingFlag(0x1B7, FlagType.FieldItem, "", "CERULEAN_CAVE_B1F_MAX_REVIVE");
            CheckMissingFlag(0x1B8, FlagType.FieldItem, "", "CERULEAN_CAVE_B1F_ULTRA_BALL");
            CheckMissingFlag(0x1B9, FlagType.FieldItem, "", "FUCHSIA_CITY_WARDENS_HOUSE_RARE_CANDY");
            CheckMissingFlag(0x1BA, FlagType.FieldItem, "", "TWO_ISLAND_REVIVE");
            CheckMissingFlag(0x1BB, FlagType.FieldItem, "", "THREE_ISLAND_ZINC");
            CheckMissingFlag(0x1BC, FlagType.FieldItem, "", "0x1BC");
            CheckMissingFlag(0x1BD, FlagType.FieldItem, "", "0x1BD");
            CheckMissingFlag(0x1BE, FlagType.FieldItem, "", "VIRIDIAN_FOREST_POTION_2");
            CheckMissingFlag(0x1BF, FlagType.FieldItem, "", "MT_MOON_B2F_REVIVE");
            CheckMissingFlag(0x1C0, FlagType.FieldItem, "", "MT_MOON_B2F_ANTIDOTE");
            CheckMissingFlag(0x1C1, FlagType.FieldItem, "", "ROUTE11_X_DEFEND");
            CheckMissingFlag(0x1C2, FlagType.FieldItem, "", "ROUTE9_BURN_HEAL");
            CheckMissingFlag(0x1C3, FlagType.FieldItem, "", "ROCK_TUNNEL_1F_REPEL");
            CheckMissingFlag(0x1C4, FlagType.FieldItem, "", "ROCK_TUNNEL_1F_PEARL");
            CheckMissingFlag(0x1C5, FlagType.FieldItem, "", "ROCK_TUNNEL_1F_ESCAPE_ROPE");
            CheckMissingFlag(0x1C6, FlagType.FieldItem, "", "ROCK_TUNNEL_B1F_REVIVE");
            CheckMissingFlag(0x1C7, FlagType.FieldItem, "", "ROCK_TUNNEL_B1F_MAX_ETHER");
            CheckMissingFlag(0x1C8, FlagType.FieldItem, "", "SILPH_CO_8F_IRON");
            CheckMissingFlag(0x1C9, FlagType.FieldItem, "", "SILPH_CO_11F_ZINC");
            CheckMissingFlag(0x1CA, FlagType.FieldItem, "", "POKEMON_MANSION_1F_PROTEIN");
            CheckMissingFlag(0x1CB, FlagType.FieldItem, "", "POKEMON_MANSION_2F_ZINC");
            CheckMissingFlag(0x1CC, FlagType.FieldItem, "", "POKEMON_MANSION_2F_HP_UP");
            CheckMissingFlag(0x1CD, FlagType.FieldItem, "", "VIRIDIAN_CITY_POTION");
            CheckMissingFlag(0x1CE, FlagType.FieldItem, "", "ROUTE11_GREAT_BALL");
            CheckMissingFlag(0x1CF, FlagType.FieldItem, "", "ROUTE11_AWAKENING");
            CheckMissingFlag(0x1D0, FlagType.FieldItem, "", "POKEMON_TOWER_5F_CLEANSE_TAG");
            CheckMissingFlag(0x1D1, FlagType.FieldItem, "", "CELADON_CITY_ETHER");
            CheckMissingFlag(0x1D2, FlagType.FieldItem, "", "ROCKET_HIDEOUT_B3F_BLACK_GLASSES");
            CheckMissingFlag(0x1D3, FlagType.FieldItem, "", "SAFARI_ZONE_NORTH_QUICK_CLAW");
            CheckMissingFlag(0x1D4, FlagType.FieldItem, "", "SEAFOAM_ISLANDS_1F_ICE_HEAL");
            CheckMissingFlag(0x1D5, FlagType.FieldItem, "", "SEAFOAM_ISLANDS_B1F_WATER_STONE");
            CheckMissingFlag(0x1D6, FlagType.FieldItem, "", "SEAFOAM_ISLANDS_B1F_REVIVE");
            CheckMissingFlag(0x1D7, FlagType.FieldItem, "", "SEAFOAM_ISLANDS_B2F_BIG_PEARL");
            CheckMissingFlag(0x1D8, FlagType.FieldItem, "", "SEAFOAM_ISLANDS_B4F_ULTRA_BALL");
            CheckMissingFlag(0x1D9, FlagType.FieldItem, "", "FOUR_ISLAND_STAR_PIECE");
            CheckMissingFlag(0x1DA, FlagType.FieldItem, "", "FOUR_ISLAND_STARDUST");
            CheckMissingFlag(0x1DB, FlagType.FieldItem, "", "ONE_ISLAND_KINDLE_ROAD_ETHER");
            CheckMissingFlag(0x1DC, FlagType.FieldItem, "", "ONE_ISLAND_KINDLE_ROAD_MAX_REPEL");
            CheckMissingFlag(0x1DD, FlagType.FieldItem, "", "ONE_ISLAND_KINDLE_ROAD_CARBOS");
            CheckMissingFlag(0x1DE, FlagType.FieldItem, "", "FIVE_ISLAND_MEADOW_MAX_POTION");
            CheckMissingFlag(0x1DF, FlagType.FieldItem, "", "FIVE_ISLAND_MEADOW_PP_UP");
            CheckMissingFlag(0x1E0, FlagType.FieldItem, "", "FIVE_ISLAND_MEMORIAL_PILLAR_METAL_COAT");
            CheckMissingFlag(0x1E1, FlagType.FieldItem, "", "SIX_ISLAND_OUTCAST_ISLAND_PP_UP");
            CheckMissingFlag(0x1E2, FlagType.FieldItem, "", "SIX_ISLAND_WATER_PATH_ELIXIR");
            CheckMissingFlag(0x1E3, FlagType.FieldItem, "", "SIX_ISLAND_WATER_PATH_DRAGON_SCALE");
            CheckMissingFlag(0x1E4, FlagType.FieldItem, "", "SIX_ISLAND_RUIN_VALLEY_FULL_RESTORE");
            CheckMissingFlag(0x1E5, FlagType.FieldItem, "", "SIX_ISLAND_RUIN_VALLEY_HP_UP");
            CheckMissingFlag(0x1E6, FlagType.FieldItem, "", "SIX_ISLAND_RUIN_VALLEY_SUN_STONE");
            CheckMissingFlag(0x1E7, FlagType.FieldItem, "", "SEVEN_ISLAND_SEVAULT_CANYON_KINGS_ROCK");
            CheckMissingFlag(0x1E8, FlagType.FieldItem, "", "SEVEN_ISLAND_SEVAULT_CANYON_MAX_ELIXIR");
            CheckMissingFlag(0x1E9, FlagType.FieldItem, "", "SEVEN_ISLAND_SEVAULT_CANYON_NUGGET");
            CheckMissingFlag(0x1EA, FlagType.FieldItem, "", "THREE_ISLAND_BERRY_FOREST_MAX_ETHER");
            CheckMissingFlag(0x1EB, FlagType.FieldItem, "", "THREE_ISLAND_BERRY_FOREST_FULL_HEAL");
            CheckMissingFlag(0x1EC, FlagType.FieldItem, "", "THREE_ISLAND_BERRY_FOREST_MAX_ELIXIR");
            CheckMissingFlag(0x1ED, FlagType.FieldItem, "", "MT_EMBER_EXTERIOR_ULTRA_BALL");
            CheckMissingFlag(0x1EE, FlagType.FieldItem, "", "MT_EMBER_EXTERIOR_FIRE_STONE");
            CheckMissingFlag(0x1EF, FlagType.FieldItem, "", "MT_EMBER_EXTERIOR_DIRE_HIT");
            CheckMissingFlag(0x1F0, FlagType.FieldItem, "", "FOUR_ISLAND_ICEFALL_CAVE_1F_ULTRA_BALL");
            CheckMissingFlag(0x1F1, FlagType.FieldItem, "", "FOUR_ISLAND_ICEFALL_CAVE_1F_HM07");
            CheckMissingFlag(0x1F2, FlagType.FieldItem, "", "FOUR_ISLAND_ICEFALL_CAVE_B1F_FULL_RESTORE");
            CheckMissingFlag(0x1F3, FlagType.FieldItem, "", "FOUR_ISLAND_ICEFALL_CAVE_B1F_NEVER_MELT_ICE");
            CheckMissingFlag(0x1F4, FlagType.FieldItem, "", "FIVE_ISLAND_ROCKET_WAREHOUSE_BIG_PEARL");
            CheckMissingFlag(0x1F5, FlagType.FieldItem, "", "FIVE_ISLAND_ROCKET_WAREHOUSE_TM36");
            CheckMissingFlag(0x1F6, FlagType.FieldItem, "", "FIVE_ISLAND_ROCKET_WAREHOUSE_PEARL");
            CheckMissingFlag(0x1F7, FlagType.FieldItem, "", "FIVE_ISLAND_ROCKET_WAREHOUSE_UP_GRADE");
            CheckMissingFlag(0x1F8, FlagType.FieldItem, "", "FIVE_ISLAND_LOST_CAVE_ROOM10_SILK_SCARF");
            CheckMissingFlag(0x1F9, FlagType.FieldItem, "", "FIVE_ISLAND_LOST_CAVE_ROOM11_LAX_INCENSE");
            CheckMissingFlag(0x1FA, FlagType.FieldItem, "", "FIVE_ISLAND_LOST_CAVE_ROOM12_SEA_INCENSE");
            CheckMissingFlag(0x1FB, FlagType.FieldItem, "", "FIVE_ISLAND_LOST_CAVE_ROOM13_MAX_REVIVE");
            CheckMissingFlag(0x1FC, FlagType.FieldItem, "", "FIVE_ISLAND_LOST_CAVE_ROOM14_RARE_CANDY");
            CheckMissingFlag(0x1FD, FlagType.FieldItem, "", "SEVEN_ISLAND_SEVAULT_CANYON_HOUSE_LUCKY_PUNCH");
            CheckMissingFlag(0x1FE, FlagType.FieldItem, "", "SILPH_CO_4F_TM41");
        }

    }

}
