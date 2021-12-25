using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    static class FlagsGen3E
    {
        static List<string> s_missingEventFlagsList = new List<string>(4096);

        static bool[] s_eventFlags;

        static void CheckFlag(int flagIdx, string aFlagDetail)
        {
            if (!s_eventFlags[flagIdx])
            {
                s_missingEventFlagsList.Add(aFlagDetail);
            }
        }

        public static void ExportFlags(SaveFile savFile)
        {
            s_eventFlags = savFile.GetEventFlags();
            s_missingEventFlagsList.Clear();

            CheckFlags();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s_missingEventFlagsList.Count; ++i)
            {
                sb.AppendFormat("{0}\n", s_missingEventFlagsList[i]);
            }

            System.IO.File.WriteAllText(string.Format("missing_events_{0}.txt", savFile.Version), sb.ToString());
        }

        static void CheckFlags()
        {
            // Hidden Items
            CheckFlag(0x1F4, "FLAG_HIDDEN_ITEM_LAVARIDGE_TOWN_ICE_HEAL");
            CheckFlag(0x1F5, "FLAG_HIDDEN_ITEM_TRICK_HOUSE_NUGGET");
            CheckFlag(0x1F6, "FLAG_HIDDEN_ITEM_ROUTE_111_STARDUST");
            CheckFlag(0x1F7, "FLAG_HIDDEN_ITEM_ROUTE_113_ETHER");
            CheckFlag(0x1F8, "FLAG_HIDDEN_ITEM_ROUTE_114_CARBOS");
            CheckFlag(0x1F9, "FLAG_HIDDEN_ITEM_ROUTE_119_CALCIUM");
            CheckFlag(0x1FA, "FLAG_HIDDEN_ITEM_ROUTE_119_ULTRA_BALL");
            CheckFlag(0x1FB, "FLAG_HIDDEN_ITEM_ROUTE_123_SUPER_REPEL");
            CheckFlag(0x1FC, "FLAG_HIDDEN_ITEM_UNDERWATER_124_CARBOS");
            CheckFlag(0x1FD, "FLAG_HIDDEN_ITEM_UNDERWATER_124_GREEN_SHARD");
            CheckFlag(0x1FE, "FLAG_HIDDEN_ITEM_UNDERWATER_124_PEARL");
            CheckFlag(0x1FF, "FLAG_HIDDEN_ITEM_UNDERWATER_124_BIG_PEARL");
            CheckFlag(0x200, "FLAG_HIDDEN_ITEM_UNDERWATER_126_BLUE_SHARD");
            CheckFlag(0x201, "FLAG_HIDDEN_ITEM_UNDERWATER_124_HEART_SCALE_1");
            CheckFlag(0x202, "FLAG_HIDDEN_ITEM_UNDERWATER_126_HEART_SCALE");
            CheckFlag(0x203, "FLAG_HIDDEN_ITEM_UNDERWATER_126_ULTRA_BALL");
            CheckFlag(0x204, "FLAG_HIDDEN_ITEM_UNDERWATER_126_STARDUST");
            CheckFlag(0x205, "FLAG_HIDDEN_ITEM_UNDERWATER_126_PEARL");
            CheckFlag(0x206, "FLAG_HIDDEN_ITEM_UNDERWATER_126_YELLOW_SHARD");
            CheckFlag(0x207, "FLAG_HIDDEN_ITEM_UNDERWATER_126_IRON");
            CheckFlag(0x208, "FLAG_HIDDEN_ITEM_UNDERWATER_126_BIG_PEARL");
            CheckFlag(0x209, "FLAG_HIDDEN_ITEM_UNDERWATER_127_STAR_PIECE");
            CheckFlag(0x20A, "FLAG_HIDDEN_ITEM_UNDERWATER_127_HP_UP");
            CheckFlag(0x20B, "FLAG_HIDDEN_ITEM_UNDERWATER_127_HEART_SCALE");
            CheckFlag(0x20C, "FLAG_HIDDEN_ITEM_UNDERWATER_127_RED_SHARD");
            CheckFlag(0x20D, "FLAG_HIDDEN_ITEM_UNDERWATER_128_PROTEIN");
            CheckFlag(0x20E, "FLAG_HIDDEN_ITEM_UNDERWATER_128_PEARL");
            CheckFlag(0x20F, "FLAG_HIDDEN_ITEM_LILYCOVE_CITY_HEART_SCALE");
            CheckFlag(0x210, "FLAG_HIDDEN_ITEM_FALLARBOR_TOWN_NUGGET");
            CheckFlag(0x211, "FLAG_HIDDEN_ITEM_MT_PYRE_EXTERIOR_ULTRA_BALL");
            CheckFlag(0x212, "FLAG_HIDDEN_ITEM_ROUTE_113_TM_32");
            CheckFlag(0x213, "FLAG_HIDDEN_ITEM_ABANDONED_SHIP_RM_1_KEY");
            CheckFlag(0x214, "FLAG_HIDDEN_ITEM_ABANDONED_SHIP_RM_2_KEY");
            CheckFlag(0x215, "FLAG_HIDDEN_ITEM_ABANDONED_SHIP_RM_4_KEY");
            CheckFlag(0x216, "FLAG_HIDDEN_ITEM_ABANDONED_SHIP_RM_6_KEY");
            CheckFlag(0x217, "FLAG_HIDDEN_ITEM_SS_TIDAL_LOWER_DECK_LEFTOVERS");
            CheckFlag(0x218, "FLAG_HIDDEN_ITEM_UNDERWATER_124_CALCIUM");
            CheckFlag(0x219, "FLAG_HIDDEN_ITEM_ROUTE_104_POTION");
            CheckFlag(0x21A, "FLAG_HIDDEN_ITEM_UNDERWATER_124_HEART_SCALE_2");
            CheckFlag(0x21B, "FLAG_HIDDEN_ITEM_ROUTE_121_HP_UP");
            CheckFlag(0x21C, "FLAG_HIDDEN_ITEM_ROUTE_121_NUGGET");
            CheckFlag(0x21D, "FLAG_HIDDEN_ITEM_ROUTE_123_REVIVE");
            CheckFlag(0x21E, "FLAG_HIDDEN_ITEM_ROUTE_113_REVIVE");
            CheckFlag(0x21F, "FLAG_HIDDEN_ITEM_LILYCOVE_CITY_PP_UP");
            CheckFlag(0x220, "FLAG_HIDDEN_ITEM_ROUTE_104_SUPER_POTION");
            CheckFlag(0x221, "FLAG_HIDDEN_ITEM_ROUTE_116_SUPER_POTION");
            CheckFlag(0x222, "FLAG_HIDDEN_ITEM_ROUTE_106_STARDUST");
            CheckFlag(0x223, "FLAG_HIDDEN_ITEM_ROUTE_106_HEART_SCALE");
            CheckFlag(0x224, "FLAG_HIDDEN_ITEM_GRANITE_CAVE_B2F_EVERSTONE_1");
            CheckFlag(0x225, "FLAG_HIDDEN_ITEM_GRANITE_CAVE_B2F_EVERSTONE_2");
            CheckFlag(0x226, "FLAG_HIDDEN_ITEM_ROUTE_109_REVIVE");
            CheckFlag(0x227, "FLAG_HIDDEN_ITEM_ROUTE_109_GREAT_BALL");
            CheckFlag(0x228, "FLAG_HIDDEN_ITEM_ROUTE_109_HEART_SCALE_1");
            CheckFlag(0x229, "FLAG_HIDDEN_ITEM_ROUTE_110_GREAT_BALL");
            CheckFlag(0x22A, "FLAG_HIDDEN_ITEM_ROUTE_110_REVIVE");
            CheckFlag(0x22B, "FLAG_HIDDEN_ITEM_ROUTE_110_FULL_HEAL");
            CheckFlag(0x22C, "FLAG_HIDDEN_ITEM_ROUTE_111_PROTEIN");
            CheckFlag(0x22D, "FLAG_HIDDEN_ITEM_ROUTE_111_RARE_CANDY");
            CheckFlag(0x22E, "FLAG_HIDDEN_ITEM_PETALBURG_WOODS_POTION");
            CheckFlag(0x22F, "FLAG_HIDDEN_ITEM_PETALBURG_WOODS_TINY_MUSHROOM_1");
            CheckFlag(0x230, "FLAG_HIDDEN_ITEM_PETALBURG_WOODS_TINY_MUSHROOM_2");
            CheckFlag(0x231, "FLAG_HIDDEN_ITEM_PETALBURG_WOODS_POKE_BALL");
            CheckFlag(0x232, "FLAG_HIDDEN_ITEM_ROUTE_104_POKE_BALL");
            CheckFlag(0x233, "FLAG_HIDDEN_ITEM_ROUTE_106_POKE_BALL");
            CheckFlag(0x234, "FLAG_HIDDEN_ITEM_ROUTE_109_ETHER");
            CheckFlag(0x235, "FLAG_HIDDEN_ITEM_ROUTE_110_POKE_BALL");
            CheckFlag(0x236, "FLAG_HIDDEN_ITEM_ROUTE_118_HEART_SCALE");
            CheckFlag(0x237, "FLAG_HIDDEN_ITEM_ROUTE_118_IRON");
            CheckFlag(0x238, "FLAG_HIDDEN_ITEM_ROUTE_119_FULL_HEAL");
            CheckFlag(0x239, "FLAG_HIDDEN_ITEM_ROUTE_120_RARE_CANDY_2");
            CheckFlag(0x23A, "FLAG_HIDDEN_ITEM_ROUTE_120_ZINC");
            CheckFlag(0x23B, "FLAG_HIDDEN_ITEM_ROUTE_120_RARE_CANDY_1");
            CheckFlag(0x23C, "FLAG_HIDDEN_ITEM_ROUTE_117_REPEL");
            CheckFlag(0x23D, "FLAG_HIDDEN_ITEM_ROUTE_121_FULL_HEAL");
            CheckFlag(0x23E, "FLAG_HIDDEN_ITEM_ROUTE_123_HYPER_POTION");
            CheckFlag(0x23F, "FLAG_HIDDEN_ITEM_LILYCOVE_CITY_POKE_BALL");
            CheckFlag(0x240, "FLAG_HIDDEN_ITEM_JAGGED_PASS_GREAT_BALL");
            CheckFlag(0x241, "FLAG_HIDDEN_ITEM_JAGGED_PASS_FULL_HEAL");
            CheckFlag(0x242, "FLAG_HIDDEN_ITEM_MT_PYRE_EXTERIOR_MAX_ETHER");
            CheckFlag(0x243, "FLAG_HIDDEN_ITEM_MT_PYRE_SUMMIT_ZINC");
            CheckFlag(0x244, "FLAG_HIDDEN_ITEM_MT_PYRE_SUMMIT_RARE_CANDY");
            CheckFlag(0x245, "FLAG_HIDDEN_ITEM_VICTORY_ROAD_1F_ULTRA_BALL");
            CheckFlag(0x246, "FLAG_HIDDEN_ITEM_VICTORY_ROAD_B2F_ELIXIR");
            CheckFlag(0x247, "FLAG_HIDDEN_ITEM_VICTORY_ROAD_B2F_MAX_REPEL");
            CheckFlag(0x248, "FLAG_HIDDEN_ITEM_ROUTE_120_REVIVE");
            CheckFlag(0x249, "FLAG_HIDDEN_ITEM_ROUTE_104_ANTIDOTE");
            CheckFlag(0x24A, "FLAG_HIDDEN_ITEM_ROUTE_108_RARE_CANDY");
            CheckFlag(0x24B, "FLAG_HIDDEN_ITEM_ROUTE_119_MAX_ETHER");
            CheckFlag(0x24C, "FLAG_HIDDEN_ITEM_ROUTE_104_HEART_SCALE");
            CheckFlag(0x24D, "FLAG_HIDDEN_ITEM_ROUTE_105_HEART_SCALE");
            CheckFlag(0x24E, "FLAG_HIDDEN_ITEM_ROUTE_109_HEART_SCALE_2");
            CheckFlag(0x24F, "FLAG_HIDDEN_ITEM_ROUTE_109_HEART_SCALE_3");
            CheckFlag(0x250, "FLAG_HIDDEN_ITEM_ROUTE_128_HEART_SCALE_1");
            CheckFlag(0x251, "FLAG_HIDDEN_ITEM_ROUTE_128_HEART_SCALE_2");
            CheckFlag(0x252, "FLAG_HIDDEN_ITEM_ROUTE_128_HEART_SCALE_3");
            CheckFlag(0x253, "FLAG_HIDDEN_ITEM_PETALBURG_CITY_RARE_CANDY");
            CheckFlag(0x254, "FLAG_HIDDEN_ITEM_ROUTE_116_BLACK_GLASSES");
            CheckFlag(0x255, "FLAG_HIDDEN_ITEM_ROUTE_115_HEART_SCALE");
            CheckFlag(0x256, "FLAG_HIDDEN_ITEM_ROUTE_113_NUGGET");
            CheckFlag(0x257, "FLAG_HIDDEN_ITEM_ROUTE_123_PP_UP");
            CheckFlag(0x258, "FLAG_HIDDEN_ITEM_ROUTE_121_MAX_REVIVE");
            CheckFlag(0x259, "FLAG_HIDDEN_ITEM_ARTISAN_CAVE_B1F_CALCIUM");
            CheckFlag(0x25A, "FLAG_HIDDEN_ITEM_ARTISAN_CAVE_B1F_ZINC");
            CheckFlag(0x25B, "FLAG_HIDDEN_ITEM_ARTISAN_CAVE_B1F_PROTEIN");
            CheckFlag(0x25C, "FLAG_HIDDEN_ITEM_ARTISAN_CAVE_B1F_IRON");
            CheckFlag(0x25D, "FLAG_HIDDEN_ITEM_SAFARI_ZONE_SOUTH_EAST_FULL_RESTORE");
            CheckFlag(0x25E, "FLAG_HIDDEN_ITEM_SAFARI_ZONE_NORTH_EAST_RARE_CANDY");
            CheckFlag(0x25F, "FLAG_HIDDEN_ITEM_SAFARI_ZONE_NORTH_EAST_ZINC");
            CheckFlag(0x260, "FLAG_HIDDEN_ITEM_SAFARI_ZONE_SOUTH_EAST_PP_UP");
            CheckFlag(0x261, "FLAG_HIDDEN_ITEM_NAVEL_ROCK_TOP_SACRED_ASH");
            CheckFlag(0x262, "FLAG_HIDDEN_ITEM_ROUTE_123_RARE_CANDY");
            CheckFlag(0x263, "FLAG_HIDDEN_ITEM_ROUTE_105_BIG_PEARL");

            // Normal items
            CheckFlag(0x3E8, "FLAG_ITEM_ROUTE_102_POTION");
            CheckFlag(0x3E9, "FLAG_ITEM_ROUTE_116_X_SPECIAL");
            CheckFlag(0x3EA, "FLAG_ITEM_ROUTE_104_PP_UP");
            CheckFlag(0x3EB, "FLAG_ITEM_ROUTE_105_IRON");
            CheckFlag(0x3EC, "FLAG_ITEM_ROUTE_106_PROTEIN");
            CheckFlag(0x3ED, "FLAG_ITEM_ROUTE_109_PP_UP");
            CheckFlag(0x3EE, "FLAG_ITEM_ROUTE_109_RARE_CANDY");
            CheckFlag(0x3EF, "FLAG_ITEM_ROUTE_110_DIRE_HIT");
            CheckFlag(0x3F0, "FLAG_ITEM_ROUTE_111_TM_37");
            CheckFlag(0x3F1, "FLAG_ITEM_ROUTE_111_STARDUST");
            CheckFlag(0x3F2, "FLAG_ITEM_ROUTE_111_HP_UP");
            CheckFlag(0x3F3, "FLAG_ITEM_ROUTE_112_NUGGET");
            CheckFlag(0x3F4, "FLAG_ITEM_ROUTE_113_MAX_ETHER");
            CheckFlag(0x3F5, "FLAG_ITEM_ROUTE_113_SUPER_REPEL");
            CheckFlag(0x3F6, "FLAG_ITEM_ROUTE_114_RARE_CANDY");
            CheckFlag(0x3F7, "FLAG_ITEM_ROUTE_114_PROTEIN");
            CheckFlag(0x3F8, "FLAG_ITEM_ROUTE_115_SUPER_POTION");
            CheckFlag(0x3F9, "FLAG_ITEM_ROUTE_115_TM_01");
            CheckFlag(0x3FA, "FLAG_ITEM_ROUTE_115_IRON");
            CheckFlag(0x3FB, "FLAG_ITEM_ROUTE_116_ETHER");
            CheckFlag(0x3FC, "FLAG_ITEM_ROUTE_116_REPEL");
            CheckFlag(0x3FD, "FLAG_ITEM_ROUTE_116_HP_UP");
            CheckFlag(0x3FE, "FLAG_ITEM_ROUTE_117_GREAT_BALL");
            CheckFlag(0x3FF, "FLAG_ITEM_ROUTE_117_REVIVE");
            CheckFlag(0x400, "FLAG_ITEM_ROUTE_119_SUPER_REPEL");
            CheckFlag(0x401, "FLAG_ITEM_ROUTE_119_ZINC");
            CheckFlag(0x402, "FLAG_ITEM_ROUTE_119_ELIXIR_1");
            CheckFlag(0x403, "FLAG_ITEM_ROUTE_119_LEAF_STONE");
            CheckFlag(0x404, "FLAG_ITEM_ROUTE_119_RARE_CANDY");
            CheckFlag(0x405, "FLAG_ITEM_ROUTE_119_HYPER_POTION_1");
            CheckFlag(0x406, "FLAG_ITEM_ROUTE_120_NUGGET");
            CheckFlag(0x407, "FLAG_ITEM_ROUTE_120_FULL_HEAL");
            CheckFlag(0x408, "FLAG_ITEM_ROUTE_123_CALCIUM");
            CheckFlag(0x409, "FLAG_ITEM_ROUTE_123_RARE_CANDY");
            CheckFlag(0x40A, "FLAG_ITEM_ROUTE_127_ZINC");
            CheckFlag(0x40B, "FLAG_ITEM_ROUTE_127_CARBOS");
            CheckFlag(0x40C, "FLAG_ITEM_ROUTE_132_RARE_CANDY");
            CheckFlag(0x40D, "FLAG_ITEM_ROUTE_133_BIG_PEARL");
            CheckFlag(0x40E, "FLAG_ITEM_ROUTE_133_STAR_PIECE");
            CheckFlag(0x40F, "FLAG_ITEM_PETALBURG_CITY_MAX_REVIVE");
            CheckFlag(0x410, "FLAG_ITEM_PETALBURG_CITY_ETHER");
            CheckFlag(0x411, "FLAG_ITEM_RUSTBORO_CITY_X_DEFEND");
            CheckFlag(0x412, "FLAG_ITEM_LILYCOVE_CITY_MAX_REPEL");
            CheckFlag(0x413, "FLAG_ITEM_MOSSDEEP_CITY_NET_BALL");
            CheckFlag(0x414, "FLAG_ITEM_METEOR_FALLS_1F_1R_TM_23");
            CheckFlag(0x415, "FLAG_ITEM_METEOR_FALLS_1F_1R_FULL_HEAL");
            CheckFlag(0x416, "FLAG_ITEM_METEOR_FALLS_1F_1R_MOON_STONE");
            CheckFlag(0x417, "FLAG_ITEM_METEOR_FALLS_1F_1R_PP_UP");
            CheckFlag(0x418, "FLAG_ITEM_RUSTURF_TUNNEL_POKE_BALL");
            CheckFlag(0x419, "FLAG_ITEM_RUSTURF_TUNNEL_MAX_ETHER");
            CheckFlag(0x41A, "FLAG_ITEM_GRANITE_CAVE_1F_ESCAPE_ROPE");
            CheckFlag(0x41B, "FLAG_ITEM_GRANITE_CAVE_B1F_POKE_BALL");
            CheckFlag(0x41C, "FLAG_ITEM_MT_PYRE_5F_LAX_INCENSE");
            CheckFlag(0x41D, "FLAG_ITEM_GRANITE_CAVE_B2F_REPEL");
            CheckFlag(0x41E, "FLAG_ITEM_GRANITE_CAVE_B2F_RARE_CANDY");
            CheckFlag(0x41F, "FLAG_ITEM_PETALBURG_WOODS_X_ATTACK");
            CheckFlag(0x420, "FLAG_ITEM_PETALBURG_WOODS_GREAT_BALL");
            CheckFlag(0x421, "FLAG_ITEM_ROUTE_104_POKE_BALL");
            CheckFlag(0x422, "FLAG_ITEM_PETALBURG_WOODS_ETHER");
            CheckFlag(0x423, "FLAG_ITEM_MAGMA_HIDEOUT_3F_3R_ECAPE_ROPE");
            CheckFlag(0x424, "FLAG_ITEM_TRICK_HOUSE_PUZZLE_1_ORANGE_MAIL");
            CheckFlag(0x425, "FLAG_ITEM_TRICK_HOUSE_PUZZLE_2_HARBOR_MAIL");
            CheckFlag(0x426, "FLAG_ITEM_TRICK_HOUSE_PUZZLE_2_WAVE_MAIL");
            CheckFlag(0x427, "FLAG_ITEM_TRICK_HOUSE_PUZZLE_3_SHADOW_MAIL");
            CheckFlag(0x428, "FLAG_ITEM_TRICK_HOUSE_PUZZLE_3_WOOD_MAIL");
            CheckFlag(0x429, "FLAG_ITEM_TRICK_HOUSE_PUZZLE_4_MECH_MAIL");
            CheckFlag(0x42A, "FLAG_ITEM_ROUTE_124_YELLOW_SHARD");
            CheckFlag(0x42B, "FLAG_ITEM_TRICK_HOUSE_PUZZLE_6_GLITTER_MAIL");
            CheckFlag(0x42C, "FLAG_ITEM_TRICK_HOUSE_PUZZLE_7_TROPIC_MAIL");
            CheckFlag(0x42D, "FLAG_ITEM_TRICK_HOUSE_PUZZLE_8_BEAD_MAIL");
            CheckFlag(0x42E, "FLAG_ITEM_JAGGED_PASS_BURN_HEAL");
            CheckFlag(0x42F, "FLAG_ITEM_AQUA_HIDEOUT_B1F_MAX_ELIXIR");
            CheckFlag(0x430, "FLAG_ITEM_AQUA_HIDEOUT_B2F_NEST_BALL");
            CheckFlag(0x431, "FLAG_ITEM_MT_PYRE_EXTERIOR_MAX_POTION");
            CheckFlag(0x432, "FLAG_ITEM_MT_PYRE_EXTERIOR_TM_48");
            CheckFlag(0x433, "FLAG_ITEM_NEW_MAUVILLE_ULTRA_BALL");
            CheckFlag(0x434, "FLAG_ITEM_NEW_MAUVILLE_ESCAPE_ROPE");
            CheckFlag(0x435, "FLAG_ITEM_ABANDONED_SHIP_HIDDEN_FLOOR_ROOM_6_LUXURY_BALL");
            CheckFlag(0x436, "FLAG_ITEM_ABANDONED_SHIP_HIDDEN_FLOOR_ROOM_4_SCANNER");
            CheckFlag(0x437, "FLAG_ITEM_SCORCHED_SLAB_TM_11");
            CheckFlag(0x438, "FLAG_ITEM_METEOR_FALLS_B1F_2R_TM_02");
            CheckFlag(0x439, "FLAG_ITEM_SHOAL_CAVE_ENTRANCE_BIG_PEARL");
            CheckFlag(0x43A, "FLAG_ITEM_SHOAL_CAVE_INNER_ROOM_RARE_CANDY");
            CheckFlag(0x43B, "FLAG_ITEM_SHOAL_CAVE_STAIRS_ROOM_ICE_HEAL");
            CheckFlag(0x43C, "FLAG_ITEM_VICTORY_ROAD_1F_MAX_ELIXIR");
            CheckFlag(0x43D, "FLAG_ITEM_VICTORY_ROAD_1F_PP_UP");
            CheckFlag(0x43E, "FLAG_ITEM_VICTORY_ROAD_B1F_TM_29");
            CheckFlag(0x43F, "FLAG_ITEM_VICTORY_ROAD_B1F_FULL_RESTORE");
            CheckFlag(0x440, "FLAG_ITEM_VICTORY_ROAD_B2F_FULL_HEAL");
            CheckFlag(0x441, "FLAG_ITEM_MT_PYRE_6F_TM_30");
            CheckFlag(0x442, "FLAG_ITEM_SEAFLOOR_CAVERN_ROOM_9_TM_26");
            CheckFlag(0x443, "FLAG_ITEM_FIERY_PATH_TM06");
            CheckFlag(0x444, "FLAG_ITEM_ROUTE_124_RED_SHARD");
            CheckFlag(0x445, "FLAG_ITEM_ROUTE_124_BLUE_SHARD");
            CheckFlag(0x446, "FLAG_ITEM_SAFARI_ZONE_NORTH_WEST_TM_22");
            CheckFlag(0x447, "FLAG_ITEM_ABANDONED_SHIP_ROOMS_1F_HARBOR_MAIL");
            CheckFlag(0x448, "FLAG_ITEM_ABANDONED_SHIP_ROOMS_B1F_ESCAPE_ROPE");
            CheckFlag(0x449, "FLAG_ITEM_ABANDONED_SHIP_ROOMS_2_B1F_DIVE_BALL");
            CheckFlag(0x44A, "FLAG_ITEM_ABANDONED_SHIP_ROOMS_B1F_TM_13");
            CheckFlag(0x44B, "FLAG_ITEM_ABANDONED_SHIP_ROOMS_2_1F_REVIVE");
            CheckFlag(0x44C, "FLAG_ITEM_ABANDONED_SHIP_CAPTAINS_OFFICE_STORAGE_KEY");
            CheckFlag(0x44D, "FLAG_ITEM_ABANDONED_SHIP_HIDDEN_FLOOR_ROOM_3_WATER_STONE");
            CheckFlag(0x44E, "FLAG_ITEM_ABANDONED_SHIP_HIDDEN_FLOOR_ROOM_1_TM_18");
            CheckFlag(0x44F, "FLAG_ITEM_ROUTE_121_CARBOS");
            CheckFlag(0x450, "FLAG_ITEM_ROUTE_123_ULTRA_BALL");
            CheckFlag(0x451, "FLAG_ITEM_ROUTE_126_GREEN_SHARD");
            CheckFlag(0x452, "FLAG_ITEM_ROUTE_119_HYPER_POTION_2");
            CheckFlag(0x453, "FLAG_ITEM_ROUTE_120_HYPER_POTION");
            CheckFlag(0x454, "FLAG_ITEM_ROUTE_120_NEST_BALL");
            CheckFlag(0x455, "FLAG_ITEM_ROUTE_123_ELIXIR");
            CheckFlag(0x456, "FLAG_ITEM_NEW_MAUVILLE_THUNDER_STONE");
            CheckFlag(0x457, "FLAG_ITEM_FIERY_PATH_FIRE_STONE");
            CheckFlag(0x458, "FLAG_ITEM_SHOAL_CAVE_ICE_ROOM_TM_07");
            CheckFlag(0x459, "FLAG_ITEM_SHOAL_CAVE_ICE_ROOM_NEVER_MELT_ICE");
            CheckFlag(0x45A, "FLAG_ITEM_ROUTE_103_GUARD_SPEC");
            CheckFlag(0x45B, "FLAG_ITEM_ROUTE_104_X_ACCURACY");
            CheckFlag(0x45C, "FLAG_ITEM_MAUVILLE_CITY_X_SPEED");
            CheckFlag(0x45D, "FLAG_ITEM_PETALBURD_WOODS_PARALYZE_HEAL");
            CheckFlag(0x45E, "FLAG_ITEM_ROUTE_115_GREAT_BALL");
            CheckFlag(0x45F, "FLAG_ITEM_SAFARI_ZONE_NORTH_CALCIUM");
            CheckFlag(0x460, "FLAG_ITEM_MT_PYRE_3F_SUPER_REPEL");
            CheckFlag(0x461, "FLAG_ITEM_ROUTE_118_HYPER_POTION");
            CheckFlag(0x462, "FLAG_ITEM_NEW_MAUVILLE_FULL_HEAL");
            CheckFlag(0x463, "FLAG_ITEM_NEW_MAUVILLE_PARALYZE_HEAL");
            CheckFlag(0x464, "FLAG_ITEM_AQUA_HIDEOUT_B1F_MASTER_BALL");
            CheckFlag(0x465, "FLAG_ITEM_OLD_MAGMA_HIDEOUT_B1F_MASTER_BALL");
            CheckFlag(0x466, "FLAG_ITEM_OLD_MAGMA_HIDEOUT_B1F_MAX_ELIXIR");
            CheckFlag(0x467, "FLAG_ITEM_OLD_MAGMA_HIDEOUT_B2F_NEST_BALL");
            //CheckFlag(0x468, "FLAG_UNUSED_0x468");
            CheckFlag(0x469, "FLAG_ITEM_MT_PYRE_2F_ULTRA_BALL");
            CheckFlag(0x46A, "FLAG_ITEM_MT_PYRE_4F_SEA_INCENSE");
            CheckFlag(0x46B, "FLAG_ITEM_SAFARI_ZONE_SOUTH_WEST_MAX_REVIVE");
            CheckFlag(0x46C, "FLAG_ITEM_AQUA_HIDEOUT_B1F_NUGGET");
            CheckFlag(0x46D, "FLAG_ITEM_MOSSDEEP_STEVENS_HOUSE_HM08");
            CheckFlag(0x46E, "FLAG_ITEM_ROUTE_119_NUGGET");
            CheckFlag(0x46F, "FLAG_ITEM_ROUTE_104_POTION");
            //CheckFlag(0x470, "FLAG_UNUSED_0x470");
            CheckFlag(0x471, "FLAG_ITEM_ROUTE_103_PP_UP");
            //CheckFlag(0x472, "FLAG_UNUSED_0x472");
            CheckFlag(0x473, "FLAG_ITEM_ROUTE_108_STAR_PIECE");
            CheckFlag(0x474, "FLAG_ITEM_ROUTE_109_POTION");
            CheckFlag(0x475, "FLAG_ITEM_ROUTE_110_ELIXIR");
            CheckFlag(0x476, "FLAG_ITEM_ROUTE_111_ELIXIR");
            CheckFlag(0x477, "FLAG_ITEM_ROUTE_113_HYPER_POTION");
            CheckFlag(0x478, "FLAG_ITEM_ROUTE_115_HEAL_POWDER");
            //CheckFlag(0x479, "FLAG_UNUSED_0x479");
            CheckFlag(0x47A, "FLAG_ITEM_ROUTE_116_POTION");
            CheckFlag(0x47B, "FLAG_ITEM_ROUTE_119_ELIXIR_2");
            CheckFlag(0x47C, "FLAG_ITEM_ROUTE_120_REVIVE");
            CheckFlag(0x47D, "FLAG_ITEM_ROUTE_121_REVIVE");
            CheckFlag(0x47E, "FLAG_ITEM_ROUTE_121_ZINC");
            CheckFlag(0x47F, "FLAG_ITEM_MAGMA_HIDEOUT_1F_RARE_CANDY");
            CheckFlag(0x480, "FLAG_ITEM_ROUTE_123_PP_UP");
            CheckFlag(0x481, "FLAG_ITEM_ROUTE_123_REVIVAL_HERB");
            CheckFlag(0x482, "FLAG_ITEM_ROUTE_125_BIG_PEARL");
            CheckFlag(0x483, "FLAG_ITEM_ROUTE_127_RARE_CANDY");
            CheckFlag(0x484, "FLAG_ITEM_ROUTE_132_PROTEIN");
            CheckFlag(0x485, "FLAG_ITEM_ROUTE_133_MAX_REVIVE");
            CheckFlag(0x486, "FLAG_ITEM_ROUTE_134_CARBOS");
            CheckFlag(0x487, "FLAG_ITEM_ROUTE_134_STAR_PIECE");
            CheckFlag(0x488, "FLAG_ITEM_ROUTE_114_ENERGY_POWDER");
            CheckFlag(0x489, "FLAG_ITEM_ROUTE_115_PP_UP");
            CheckFlag(0x48A, "FLAG_ITEM_ARTISAN_CAVE_B1F_HP_UP");
            CheckFlag(0x48B, "FLAG_ITEM_ARTISAN_CAVE_1F_CARBOS");
            CheckFlag(0x48C, "FLAG_ITEM_MAGMA_HIDEOUT_2F_2R_MAX_ELIXIR");
            CheckFlag(0x48D, "FLAG_ITEM_MAGMA_HIDEOUT_2F_2R_FULL_RESTORE");
            CheckFlag(0x48E, "FLAG_ITEM_MAGMA_HIDEOUT_3F_1R_NUGGET");
            CheckFlag(0x48F, "FLAG_ITEM_MAGMA_HIDEOUT_3F_2R_PP_MAX");
            CheckFlag(0x490, "FLAG_ITEM_MAGMA_HIDEOUT_4F_MAX_REVIVE");
            CheckFlag(0x491, "FLAG_ITEM_SAFARI_ZONE_NORTH_EAST_NUGGET");
            CheckFlag(0x492, "FLAG_ITEM_SAFARI_ZONE_SOUTH_EAST_BIG_PEARL");
        }

    }

}
