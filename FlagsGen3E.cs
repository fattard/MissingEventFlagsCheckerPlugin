using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal class FlagsGen3E : FlagsOrganizer
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
                    for (int i = 0x1F4; i <= 0x263; ++i)
                        flagHelper.SetEventFlag(i, value);
                    break;

                case FlagType.FieldItem:
                    for (int i = 0x3E8; i <= 0x492; ++i)
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
            CheckMissingFlag(0x1F4, FlagType.HiddenItem, "", "LAVARIDGE_TOWN_ICE_HEAL");
            CheckMissingFlag(0x1F5, FlagType.HiddenItem, "", "TRICK_HOUSE_NUGGET");
            CheckMissingFlag(0x1F6, FlagType.HiddenItem, "", "ROUTE_111_STARDUST");
            CheckMissingFlag(0x1F7, FlagType.HiddenItem, "", "ROUTE_113_ETHER");
            CheckMissingFlag(0x1F8, FlagType.HiddenItem, "", "ROUTE_114_CARBOS");
            CheckMissingFlag(0x1F9, FlagType.HiddenItem, "", "ROUTE_119_CALCIUM");
            CheckMissingFlag(0x1FA, FlagType.HiddenItem, "", "ROUTE_119_ULTRA_BALL");
            CheckMissingFlag(0x1FB, FlagType.HiddenItem, "", "ROUTE_123_SUPER_REPEL");
            CheckMissingFlag(0x1FC, FlagType.HiddenItem, "", "UNDERWATER_124_CARBOS");
            CheckMissingFlag(0x1FD, FlagType.HiddenItem, "", "UNDERWATER_124_GREEN_SHARD");
            CheckMissingFlag(0x1FE, FlagType.HiddenItem, "", "UNDERWATER_124_PEARL");
            CheckMissingFlag(0x1FF, FlagType.HiddenItem, "", "UNDERWATER_124_BIG_PEARL");
            CheckMissingFlag(0x200, FlagType.HiddenItem, "", "UNDERWATER_126_BLUE_SHARD");
            CheckMissingFlag(0x201, FlagType.HiddenItem, "", "UNDERWATER_124_HEART_SCALE_1");
            CheckMissingFlag(0x202, FlagType.HiddenItem, "", "UNDERWATER_126_HEART_SCALE");
            CheckMissingFlag(0x203, FlagType.HiddenItem, "", "UNDERWATER_126_ULTRA_BALL");
            CheckMissingFlag(0x204, FlagType.HiddenItem, "", "UNDERWATER_126_STARDUST");
            CheckMissingFlag(0x205, FlagType.HiddenItem, "", "UNDERWATER_126_PEARL");
            CheckMissingFlag(0x206, FlagType.HiddenItem, "", "UNDERWATER_126_YELLOW_SHARD");
            CheckMissingFlag(0x207, FlagType.HiddenItem, "", "UNDERWATER_126_IRON");
            CheckMissingFlag(0x208, FlagType.HiddenItem, "", "UNDERWATER_126_BIG_PEARL");
            CheckMissingFlag(0x209, FlagType.HiddenItem, "", "UNDERWATER_127_STAR_PIECE");
            CheckMissingFlag(0x20A, FlagType.HiddenItem, "", "UNDERWATER_127_HP_UP");
            CheckMissingFlag(0x20B, FlagType.HiddenItem, "", "UNDERWATER_127_HEART_SCALE");
            CheckMissingFlag(0x20C, FlagType.HiddenItem, "", "UNDERWATER_127_RED_SHARD");
            CheckMissingFlag(0x20D, FlagType.HiddenItem, "", "UNDERWATER_128_PROTEIN");
            CheckMissingFlag(0x20E, FlagType.HiddenItem, "", "UNDERWATER_128_PEARL");
            CheckMissingFlag(0x20F, FlagType.HiddenItem, "", "LILYCOVE_CITY_HEART_SCALE");
            CheckMissingFlag(0x210, FlagType.HiddenItem, "", "FALLARBOR_TOWN_NUGGET");
            CheckMissingFlag(0x211, FlagType.HiddenItem, "", "MT_PYRE_EXTERIOR_ULTRA_BALL");
            CheckMissingFlag(0x212, FlagType.HiddenItem, "", "ROUTE_113_TM_32");
            CheckMissingFlag(0x213, FlagType.HiddenItem, "", "ABANDONED_SHIP_RM_1_KEY");
            CheckMissingFlag(0x214, FlagType.HiddenItem, "", "ABANDONED_SHIP_RM_2_KEY");
            CheckMissingFlag(0x215, FlagType.HiddenItem, "", "ABANDONED_SHIP_RM_4_KEY");
            CheckMissingFlag(0x216, FlagType.HiddenItem, "", "ABANDONED_SHIP_RM_6_KEY");
            CheckMissingFlag(0x217, FlagType.HiddenItem, "", "SS_TIDAL_LOWER_DECK_LEFTOVERS");
            CheckMissingFlag(0x218, FlagType.HiddenItem, "", "UNDERWATER_124_CALCIUM");
            CheckMissingFlag(0x219, FlagType.HiddenItem, "", "ROUTE_104_POTION");
            CheckMissingFlag(0x21A, FlagType.HiddenItem, "", "UNDERWATER_124_HEART_SCALE_2");
            CheckMissingFlag(0x21B, FlagType.HiddenItem, "", "ROUTE_121_HP_UP");
            CheckMissingFlag(0x21C, FlagType.HiddenItem, "", "ROUTE_121_NUGGET");
            CheckMissingFlag(0x21D, FlagType.HiddenItem, "", "ROUTE_123_REVIVE");
            CheckMissingFlag(0x21E, FlagType.HiddenItem, "", "ROUTE_113_REVIVE");
            CheckMissingFlag(0x21F, FlagType.HiddenItem, "", "LILYCOVE_CITY_PP_UP");
            CheckMissingFlag(0x220, FlagType.HiddenItem, "", "ROUTE_104_SUPER_POTION");
            CheckMissingFlag(0x221, FlagType.HiddenItem, "", "ROUTE_116_SUPER_POTION");
            CheckMissingFlag(0x222, FlagType.HiddenItem, "", "ROUTE_106_STARDUST");
            CheckMissingFlag(0x223, FlagType.HiddenItem, "", "ROUTE_106_HEART_SCALE");
            CheckMissingFlag(0x224, FlagType.HiddenItem, "", "GRANITE_CAVE_B2F_EVERSTONE_1");
            CheckMissingFlag(0x225, FlagType.HiddenItem, "", "GRANITE_CAVE_B2F_EVERSTONE_2");
            CheckMissingFlag(0x226, FlagType.HiddenItem, "", "ROUTE_109_REVIVE");
            CheckMissingFlag(0x227, FlagType.HiddenItem, "", "ROUTE_109_GREAT_BALL");
            CheckMissingFlag(0x228, FlagType.HiddenItem, "", "ROUTE_109_HEART_SCALE_1");
            CheckMissingFlag(0x229, FlagType.HiddenItem, "", "ROUTE_110_GREAT_BALL");
            CheckMissingFlag(0x22A, FlagType.HiddenItem, "", "ROUTE_110_REVIVE");
            CheckMissingFlag(0x22B, FlagType.HiddenItem, "", "ROUTE_110_FULL_HEAL");
            CheckMissingFlag(0x22C, FlagType.HiddenItem, "", "ROUTE_111_PROTEIN");
            CheckMissingFlag(0x22D, FlagType.HiddenItem, "", "ROUTE_111_RARE_CANDY");
            CheckMissingFlag(0x22E, FlagType.HiddenItem, "", "PETALBURG_WOODS_POTION");
            CheckMissingFlag(0x22F, FlagType.HiddenItem, "", "PETALBURG_WOODS_TINY_MUSHROOM_1");
            CheckMissingFlag(0x230, FlagType.HiddenItem, "", "PETALBURG_WOODS_TINY_MUSHROOM_2");
            CheckMissingFlag(0x231, FlagType.HiddenItem, "", "PETALBURG_WOODS_POKE_BALL");
            CheckMissingFlag(0x232, FlagType.HiddenItem, "", "ROUTE_104_POKE_BALL");
            CheckMissingFlag(0x233, FlagType.HiddenItem, "", "ROUTE_106_POKE_BALL");
            CheckMissingFlag(0x234, FlagType.HiddenItem, "", "ROUTE_109_ETHER");
            CheckMissingFlag(0x235, FlagType.HiddenItem, "", "ROUTE_110_POKE_BALL");
            CheckMissingFlag(0x236, FlagType.HiddenItem, "", "ROUTE_118_HEART_SCALE");
            CheckMissingFlag(0x237, FlagType.HiddenItem, "", "ROUTE_118_IRON");
            CheckMissingFlag(0x238, FlagType.HiddenItem, "", "ROUTE_119_FULL_HEAL");
            CheckMissingFlag(0x239, FlagType.HiddenItem, "", "ROUTE_120_RARE_CANDY_2");
            CheckMissingFlag(0x23A, FlagType.HiddenItem, "", "ROUTE_120_ZINC");
            CheckMissingFlag(0x23B, FlagType.HiddenItem, "", "ROUTE_120_RARE_CANDY_1");
            CheckMissingFlag(0x23C, FlagType.HiddenItem, "", "ROUTE_117_REPEL");
            CheckMissingFlag(0x23D, FlagType.HiddenItem, "", "ROUTE_121_FULL_HEAL");
            CheckMissingFlag(0x23E, FlagType.HiddenItem, "", "ROUTE_123_HYPER_POTION");
            CheckMissingFlag(0x23F, FlagType.HiddenItem, "", "LILYCOVE_CITY_POKE_BALL");
            CheckMissingFlag(0x240, FlagType.HiddenItem, "", "JAGGED_PASS_GREAT_BALL");
            CheckMissingFlag(0x241, FlagType.HiddenItem, "", "JAGGED_PASS_FULL_HEAL");
            CheckMissingFlag(0x242, FlagType.HiddenItem, "", "MT_PYRE_EXTERIOR_MAX_ETHER");
            CheckMissingFlag(0x243, FlagType.HiddenItem, "", "MT_PYRE_SUMMIT_ZINC");
            CheckMissingFlag(0x244, FlagType.HiddenItem, "", "MT_PYRE_SUMMIT_RARE_CANDY");
            CheckMissingFlag(0x245, FlagType.HiddenItem, "", "VICTORY_ROAD_1F_ULTRA_BALL");
            CheckMissingFlag(0x246, FlagType.HiddenItem, "", "VICTORY_ROAD_B2F_ELIXIR");
            CheckMissingFlag(0x247, FlagType.HiddenItem, "", "VICTORY_ROAD_B2F_MAX_REPEL");
            CheckMissingFlag(0x248, FlagType.HiddenItem, "", "ROUTE_120_REVIVE");
            CheckMissingFlag(0x249, FlagType.HiddenItem, "", "ROUTE_104_ANTIDOTE");
            CheckMissingFlag(0x24A, FlagType.HiddenItem, "", "ROUTE_108_RARE_CANDY");
            CheckMissingFlag(0x24B, FlagType.HiddenItem, "", "ROUTE_119_MAX_ETHER");
            CheckMissingFlag(0x24C, FlagType.HiddenItem, "", "ROUTE_104_HEART_SCALE");
            CheckMissingFlag(0x24D, FlagType.HiddenItem, "", "ROUTE_105_HEART_SCALE");
            CheckMissingFlag(0x24E, FlagType.HiddenItem, "", "ROUTE_109_HEART_SCALE_2");
            CheckMissingFlag(0x24F, FlagType.HiddenItem, "", "ROUTE_109_HEART_SCALE_3");
            CheckMissingFlag(0x250, FlagType.HiddenItem, "", "ROUTE_128_HEART_SCALE_1");
            CheckMissingFlag(0x251, FlagType.HiddenItem, "", "ROUTE_128_HEART_SCALE_2");
            CheckMissingFlag(0x252, FlagType.HiddenItem, "", "ROUTE_128_HEART_SCALE_3");
            CheckMissingFlag(0x253, FlagType.HiddenItem, "", "PETALBURG_CITY_RARE_CANDY");
            CheckMissingFlag(0x254, FlagType.HiddenItem, "", "ROUTE_116_BLACK_GLASSES");
            CheckMissingFlag(0x255, FlagType.HiddenItem, "", "ROUTE_115_HEART_SCALE");
            CheckMissingFlag(0x256, FlagType.HiddenItem, "", "ROUTE_113_NUGGET");
            CheckMissingFlag(0x257, FlagType.HiddenItem, "", "ROUTE_123_PP_UP");
            CheckMissingFlag(0x258, FlagType.HiddenItem, "", "ROUTE_121_MAX_REVIVE");
            CheckMissingFlag(0x259, FlagType.HiddenItem, "", "ARTISAN_CAVE_B1F_CALCIUM");
            CheckMissingFlag(0x25A, FlagType.HiddenItem, "", "ARTISAN_CAVE_B1F_ZINC");
            CheckMissingFlag(0x25B, FlagType.HiddenItem, "", "ARTISAN_CAVE_B1F_PROTEIN");
            CheckMissingFlag(0x25C, FlagType.HiddenItem, "", "ARTISAN_CAVE_B1F_IRON");
            CheckMissingFlag(0x25D, FlagType.HiddenItem, "", "SAFARI_ZONE_SOUTH_EAST_FULL_RESTORE");
            CheckMissingFlag(0x25E, FlagType.HiddenItem, "", "SAFARI_ZONE_NORTH_EAST_RARE_CANDY");
            CheckMissingFlag(0x25F, FlagType.HiddenItem, "", "SAFARI_ZONE_NORTH_EAST_ZINC");
            CheckMissingFlag(0x260, FlagType.HiddenItem, "", "SAFARI_ZONE_SOUTH_EAST_PP_UP");
            CheckMissingFlag(0x261, FlagType.HiddenItem, "", "NAVEL_ROCK_TOP_SACRED_ASH");
            CheckMissingFlag(0x262, FlagType.HiddenItem, "", "ROUTE_123_RARE_CANDY");
            CheckMissingFlag(0x263, FlagType.HiddenItem, "", "ROUTE_105_BIG_PEARL");

            // Field items
            CheckMissingFlag(0x3E8, FlagType.FieldItem, "", "ROUTE_102_POTION");
            CheckMissingFlag(0x3E9, FlagType.FieldItem, "", "ROUTE_116_X_SPECIAL");
            CheckMissingFlag(0x3EA, FlagType.FieldItem, "", "ROUTE_104_PP_UP");
            CheckMissingFlag(0x3EB, FlagType.FieldItem, "", "ROUTE_105_IRON");
            CheckMissingFlag(0x3EC, FlagType.FieldItem, "", "ROUTE_106_PROTEIN");
            CheckMissingFlag(0x3ED, FlagType.FieldItem, "", "ROUTE_109_PP_UP");
            CheckMissingFlag(0x3EE, FlagType.FieldItem, "", "ROUTE_109_RARE_CANDY");
            CheckMissingFlag(0x3EF, FlagType.FieldItem, "", "ROUTE_110_DIRE_HIT");
            CheckMissingFlag(0x3F0, FlagType.FieldItem, "", "ROUTE_111_TM_37");
            CheckMissingFlag(0x3F1, FlagType.FieldItem, "", "ROUTE_111_STARDUST");
            CheckMissingFlag(0x3F2, FlagType.FieldItem, "", "ROUTE_111_HP_UP");
            CheckMissingFlag(0x3F3, FlagType.FieldItem, "", "ROUTE_112_NUGGET");
            CheckMissingFlag(0x3F4, FlagType.FieldItem, "", "ROUTE_113_MAX_ETHER");
            CheckMissingFlag(0x3F5, FlagType.FieldItem, "", "ROUTE_113_SUPER_REPEL");
            CheckMissingFlag(0x3F6, FlagType.FieldItem, "", "ROUTE_114_RARE_CANDY");
            CheckMissingFlag(0x3F7, FlagType.FieldItem, "", "ROUTE_114_PROTEIN");
            CheckMissingFlag(0x3F8, FlagType.FieldItem, "", "ROUTE_115_SUPER_POTION");
            CheckMissingFlag(0x3F9, FlagType.FieldItem, "", "ROUTE_115_TM_01");
            CheckMissingFlag(0x3FA, FlagType.FieldItem, "", "ROUTE_115_IRON");
            CheckMissingFlag(0x3FB, FlagType.FieldItem, "", "ROUTE_116_ETHER");
            CheckMissingFlag(0x3FC, FlagType.FieldItem, "", "ROUTE_116_REPEL");
            CheckMissingFlag(0x3FD, FlagType.FieldItem, "", "ROUTE_116_HP_UP");
            CheckMissingFlag(0x3FE, FlagType.FieldItem, "", "ROUTE_117_GREAT_BALL");
            CheckMissingFlag(0x3FF, FlagType.FieldItem, "", "ROUTE_117_REVIVE");
            CheckMissingFlag(0x400, FlagType.FieldItem, "", "ROUTE_119_SUPER_REPEL");
            CheckMissingFlag(0x401, FlagType.FieldItem, "", "ROUTE_119_ZINC");
            CheckMissingFlag(0x402, FlagType.FieldItem, "", "ROUTE_119_ELIXIR_1");
            CheckMissingFlag(0x403, FlagType.FieldItem, "", "ROUTE_119_LEAF_STONE");
            CheckMissingFlag(0x404, FlagType.FieldItem, "", "ROUTE_119_RARE_CANDY");
            CheckMissingFlag(0x405, FlagType.FieldItem, "", "ROUTE_119_HYPER_POTION_1");
            CheckMissingFlag(0x406, FlagType.FieldItem, "", "ROUTE_120_NUGGET");
            CheckMissingFlag(0x407, FlagType.FieldItem, "", "ROUTE_120_FULL_HEAL");
            CheckMissingFlag(0x408, FlagType.FieldItem, "", "ROUTE_123_CALCIUM");
            CheckMissingFlag(0x409, FlagType.FieldItem, "", "ROUTE_123_RARE_CANDY");
            CheckMissingFlag(0x40A, FlagType.FieldItem, "", "ROUTE_127_ZINC");
            CheckMissingFlag(0x40B, FlagType.FieldItem, "", "ROUTE_127_CARBOS");
            CheckMissingFlag(0x40C, FlagType.FieldItem, "", "ROUTE_132_RARE_CANDY");
            CheckMissingFlag(0x40D, FlagType.FieldItem, "", "ROUTE_133_BIG_PEARL");
            CheckMissingFlag(0x40E, FlagType.FieldItem, "", "ROUTE_133_STAR_PIECE");
            CheckMissingFlag(0x40F, FlagType.FieldItem, "", "PETALBURG_CITY_MAX_REVIVE");
            CheckMissingFlag(0x410, FlagType.FieldItem, "", "PETALBURG_CITY_ETHER");
            CheckMissingFlag(0x411, FlagType.FieldItem, "", "RUSTBORO_CITY_X_DEFEND");
            CheckMissingFlag(0x412, FlagType.FieldItem, "", "LILYCOVE_CITY_MAX_REPEL");
            CheckMissingFlag(0x413, FlagType.FieldItem, "", "MOSSDEEP_CITY_NET_BALL");
            CheckMissingFlag(0x414, FlagType.FieldItem, "", "METEOR_FALLS_1F_1R_TM_23");
            CheckMissingFlag(0x415, FlagType.FieldItem, "", "METEOR_FALLS_1F_1R_FULL_HEAL");
            CheckMissingFlag(0x416, FlagType.FieldItem, "", "METEOR_FALLS_1F_1R_MOON_STONE");
            CheckMissingFlag(0x417, FlagType.FieldItem, "", "METEOR_FALLS_1F_1R_PP_UP");
            CheckMissingFlag(0x418, FlagType.FieldItem, "", "RUSTURF_TUNNEL_POKE_BALL");
            CheckMissingFlag(0x419, FlagType.FieldItem, "", "RUSTURF_TUNNEL_MAX_ETHER");
            CheckMissingFlag(0x41A, FlagType.FieldItem, "", "GRANITE_CAVE_1F_ESCAPE_ROPE");
            CheckMissingFlag(0x41B, FlagType.FieldItem, "", "GRANITE_CAVE_B1F_POKE_BALL");
            CheckMissingFlag(0x41C, FlagType.FieldItem, "", "MT_PYRE_5F_LAX_INCENSE");
            CheckMissingFlag(0x41D, FlagType.FieldItem, "", "GRANITE_CAVE_B2F_REPEL");
            CheckMissingFlag(0x41E, FlagType.FieldItem, "", "GRANITE_CAVE_B2F_RARE_CANDY");
            CheckMissingFlag(0x41F, FlagType.FieldItem, "", "PETALBURG_WOODS_X_ATTACK");
            CheckMissingFlag(0x420, FlagType.FieldItem, "", "PETALBURG_WOODS_GREAT_BALL");
            CheckMissingFlag(0x421, FlagType.FieldItem, "", "ROUTE_104_POKE_BALL");
            CheckMissingFlag(0x422, FlagType.FieldItem, "", "PETALBURG_WOODS_ETHER");
            CheckMissingFlag(0x423, FlagType.FieldItem, "", "MAGMA_HIDEOUT_3F_3R_ECAPE_ROPE");
            CheckMissingFlag(0x424, FlagType.FieldItem, "", "TRICK_HOUSE_PUZZLE_1_ORANGE_MAIL");
            CheckMissingFlag(0x425, FlagType.FieldItem, "", "TRICK_HOUSE_PUZZLE_2_HARBOR_MAIL");
            CheckMissingFlag(0x426, FlagType.FieldItem, "", "TRICK_HOUSE_PUZZLE_2_WAVE_MAIL");
            CheckMissingFlag(0x427, FlagType.FieldItem, "", "TRICK_HOUSE_PUZZLE_3_SHADOW_MAIL");
            CheckMissingFlag(0x428, FlagType.FieldItem, "", "TRICK_HOUSE_PUZZLE_3_WOOD_MAIL");
            CheckMissingFlag(0x429, FlagType.FieldItem, "", "TRICK_HOUSE_PUZZLE_4_MECH_MAIL");
            CheckMissingFlag(0x42A, FlagType.FieldItem, "", "ROUTE_124_YELLOW_SHARD");
            CheckMissingFlag(0x42B, FlagType.FieldItem, "", "TRICK_HOUSE_PUZZLE_6_GLITTER_MAIL");
            CheckMissingFlag(0x42C, FlagType.FieldItem, "", "TRICK_HOUSE_PUZZLE_7_TROPIC_MAIL");
            CheckMissingFlag(0x42D, FlagType.FieldItem, "", "TRICK_HOUSE_PUZZLE_8_BEAD_MAIL");
            CheckMissingFlag(0x42E, FlagType.FieldItem, "", "JAGGED_PASS_BURN_HEAL");
            CheckMissingFlag(0x42F, FlagType.FieldItem, "", "AQUA_HIDEOUT_B1F_MAX_ELIXIR");
            CheckMissingFlag(0x430, FlagType.FieldItem, "", "AQUA_HIDEOUT_B2F_NEST_BALL");
            CheckMissingFlag(0x431, FlagType.FieldItem, "", "MT_PYRE_EXTERIOR_MAX_POTION");
            CheckMissingFlag(0x432, FlagType.FieldItem, "", "MT_PYRE_EXTERIOR_TM_48");
            CheckMissingFlag(0x433, FlagType.FieldItem, "", "NEW_MAUVILLE_ULTRA_BALL");
            CheckMissingFlag(0x434, FlagType.FieldItem, "", "NEW_MAUVILLE_ESCAPE_ROPE");
            CheckMissingFlag(0x435, FlagType.FieldItem, "", "ABANDONED_SHIP_HIDDEN_FLOOR_ROOM_6_LUXURY_BALL");
            CheckMissingFlag(0x436, FlagType.FieldItem, "", "ABANDONED_SHIP_HIDDEN_FLOOR_ROOM_4_SCANNER");
            CheckMissingFlag(0x437, FlagType.FieldItem, "", "SCORCHED_SLAB_TM_11");
            CheckMissingFlag(0x438, FlagType.FieldItem, "", "METEOR_FALLS_B1F_2R_TM_02");
            CheckMissingFlag(0x439, FlagType.FieldItem, "", "SHOAL_CAVE_ENTRANCE_BIG_PEARL");
            CheckMissingFlag(0x43A, FlagType.FieldItem, "", "SHOAL_CAVE_INNER_ROOM_RARE_CANDY");
            CheckMissingFlag(0x43B, FlagType.FieldItem, "", "SHOAL_CAVE_STAIRS_ROOM_ICE_HEAL");
            CheckMissingFlag(0x43C, FlagType.FieldItem, "", "VICTORY_ROAD_1F_MAX_ELIXIR");
            CheckMissingFlag(0x43D, FlagType.FieldItem, "", "VICTORY_ROAD_1F_PP_UP");
            CheckMissingFlag(0x43E, FlagType.FieldItem, "", "VICTORY_ROAD_B1F_TM_29");
            CheckMissingFlag(0x43F, FlagType.FieldItem, "", "VICTORY_ROAD_B1F_FULL_RESTORE");
            CheckMissingFlag(0x440, FlagType.FieldItem, "", "VICTORY_ROAD_B2F_FULL_HEAL");
            CheckMissingFlag(0x441, FlagType.FieldItem, "", "MT_PYRE_6F_TM_30");
            CheckMissingFlag(0x442, FlagType.FieldItem, "", "SEAFLOOR_CAVERN_ROOM_9_TM_26");
            CheckMissingFlag(0x443, FlagType.FieldItem, "", "FIERY_PATH_TM06");
            CheckMissingFlag(0x444, FlagType.FieldItem, "", "ROUTE_124_RED_SHARD");
            CheckMissingFlag(0x445, FlagType.FieldItem, "", "ROUTE_124_BLUE_SHARD");
            CheckMissingFlag(0x446, FlagType.FieldItem, "", "SAFARI_ZONE_NORTH_WEST_TM_22");
            CheckMissingFlag(0x447, FlagType.FieldItem, "", "ABANDONED_SHIP_ROOMS_1F_HARBOR_MAIL");
            CheckMissingFlag(0x448, FlagType.FieldItem, "", "ABANDONED_SHIP_ROOMS_B1F_ESCAPE_ROPE");
            CheckMissingFlag(0x449, FlagType.FieldItem, "", "ABANDONED_SHIP_ROOMS_2_B1F_DIVE_BALL");
            CheckMissingFlag(0x44A, FlagType.FieldItem, "", "ABANDONED_SHIP_ROOMS_B1F_TM_13");
            CheckMissingFlag(0x44B, FlagType.FieldItem, "", "ABANDONED_SHIP_ROOMS_2_1F_REVIVE");
            CheckMissingFlag(0x44C, FlagType.FieldItem, "", "ABANDONED_SHIP_CAPTAINS_OFFICE_STORAGE_KEY");
            CheckMissingFlag(0x44D, FlagType.FieldItem, "", "ABANDONED_SHIP_HIDDEN_FLOOR_ROOM_3_WATER_STONE");
            CheckMissingFlag(0x44E, FlagType.FieldItem, "", "ABANDONED_SHIP_HIDDEN_FLOOR_ROOM_1_TM_18");
            CheckMissingFlag(0x44F, FlagType.FieldItem, "", "ROUTE_121_CARBOS");
            CheckMissingFlag(0x450, FlagType.FieldItem, "", "ROUTE_123_ULTRA_BALL");
            CheckMissingFlag(0x451, FlagType.FieldItem, "", "ROUTE_126_GREEN_SHARD");
            CheckMissingFlag(0x452, FlagType.FieldItem, "", "ROUTE_119_HYPER_POTION_2");
            CheckMissingFlag(0x453, FlagType.FieldItem, "", "ROUTE_120_HYPER_POTION");
            CheckMissingFlag(0x454, FlagType.FieldItem, "", "ROUTE_120_NEST_BALL");
            CheckMissingFlag(0x455, FlagType.FieldItem, "", "ROUTE_123_ELIXIR");
            CheckMissingFlag(0x456, FlagType.FieldItem, "", "NEW_MAUVILLE_THUNDER_STONE");
            CheckMissingFlag(0x457, FlagType.FieldItem, "", "FIERY_PATH_FIRE_STONE");
            CheckMissingFlag(0x458, FlagType.FieldItem, "", "SHOAL_CAVE_ICE_ROOM_TM_07");
            CheckMissingFlag(0x459, FlagType.FieldItem, "", "SHOAL_CAVE_ICE_ROOM_NEVER_MELT_ICE");
            CheckMissingFlag(0x45A, FlagType.FieldItem, "", "ROUTE_103_GUARD_SPEC");
            CheckMissingFlag(0x45B, FlagType.FieldItem, "", "ROUTE_104_X_ACCURACY");
            CheckMissingFlag(0x45C, FlagType.FieldItem, "", "MAUVILLE_CITY_X_SPEED");
            CheckMissingFlag(0x45D, FlagType.FieldItem, "", "PETALBURD_WOODS_PARALYZE_HEAL");
            CheckMissingFlag(0x45E, FlagType.FieldItem, "", "ROUTE_115_GREAT_BALL");
            CheckMissingFlag(0x45F, FlagType.FieldItem, "", "SAFARI_ZONE_NORTH_CALCIUM");
            CheckMissingFlag(0x460, FlagType.FieldItem, "", "MT_PYRE_3F_SUPER_REPEL");
            CheckMissingFlag(0x461, FlagType.FieldItem, "", "ROUTE_118_HYPER_POTION");
            CheckMissingFlag(0x462, FlagType.FieldItem, "", "NEW_MAUVILLE_FULL_HEAL");
            CheckMissingFlag(0x463, FlagType.FieldItem, "", "NEW_MAUVILLE_PARALYZE_HEAL");
            CheckMissingFlag(0x464, FlagType.FieldItem, "", "AQUA_HIDEOUT_B1F_MASTER_BALL");
            CheckMissingFlag(0x465, FlagType.FieldItem, "", "OLD_MAGMA_HIDEOUT_B1F_MASTER_BALL");
            CheckMissingFlag(0x466, FlagType.FieldItem, "", "OLD_MAGMA_HIDEOUT_B1F_MAX_ELIXIR");
            CheckMissingFlag(0x467, FlagType.FieldItem, "", "OLD_MAGMA_HIDEOUT_B2F_NEST_BALL");
            //CheckMissingFlag(0x468,FlagType.FieldItem, "", UNUSED_0x468");
            CheckMissingFlag(0x469, FlagType.FieldItem, "", "MT_PYRE_2F_ULTRA_BALL");
            CheckMissingFlag(0x46A, FlagType.FieldItem, "", "MT_PYRE_4F_SEA_INCENSE");
            CheckMissingFlag(0x46B, FlagType.FieldItem, "", "SAFARI_ZONE_SOUTH_WEST_MAX_REVIVE");
            CheckMissingFlag(0x46C, FlagType.FieldItem, "", "AQUA_HIDEOUT_B1F_NUGGET");
            CheckMissingFlag(0x46D, FlagType.FieldItem, "", "MOSSDEEP_STEVENS_HOUSE_HM08");
            CheckMissingFlag(0x46E, FlagType.FieldItem, "", "ROUTE_119_NUGGET");
            CheckMissingFlag(0x46F, FlagType.FieldItem, "", "ROUTE_104_POTION");
            //CheckMissingFlag(0x470, FlagType.FieldItem, "", "UNUSED_0x470");
            CheckMissingFlag(0x471, FlagType.FieldItem, "", "ROUTE_103_PP_UP");
            //CheckMissingFlag(0x472, FlagType.FieldItem, "", "UNUSED_0x472");
            CheckMissingFlag(0x473, FlagType.FieldItem, "", "ROUTE_108_STAR_PIECE");
            CheckMissingFlag(0x474, FlagType.FieldItem, "", "ROUTE_109_POTION");
            CheckMissingFlag(0x475, FlagType.FieldItem, "", "ROUTE_110_ELIXIR");
            CheckMissingFlag(0x476, FlagType.FieldItem, "", "ROUTE_111_ELIXIR");
            CheckMissingFlag(0x477, FlagType.FieldItem, "", "ROUTE_113_HYPER_POTION");
            CheckMissingFlag(0x478, FlagType.FieldItem, "", "ROUTE_115_HEAL_POWDER");
            //CheckMissingFlag(0x479, FlagType.FieldItem, "", "UNUSED_0x479");
            CheckMissingFlag(0x47A, FlagType.FieldItem, "", "ROUTE_116_POTION");
            CheckMissingFlag(0x47B, FlagType.FieldItem, "", "ROUTE_119_ELIXIR_2");
            CheckMissingFlag(0x47C, FlagType.FieldItem, "", "ROUTE_120_REVIVE");
            CheckMissingFlag(0x47D, FlagType.FieldItem, "", "ROUTE_121_REVIVE");
            CheckMissingFlag(0x47E, FlagType.FieldItem, "", "ROUTE_121_ZINC");
            CheckMissingFlag(0x47F, FlagType.FieldItem, "", "MAGMA_HIDEOUT_1F_RARE_CANDY");
            CheckMissingFlag(0x480, FlagType.FieldItem, "", "ROUTE_123_PP_UP");
            CheckMissingFlag(0x481, FlagType.FieldItem, "", "ROUTE_123_REVIVAL_HERB");
            CheckMissingFlag(0x482, FlagType.FieldItem, "", "ROUTE_125_BIG_PEARL");
            CheckMissingFlag(0x483, FlagType.FieldItem, "", "ROUTE_127_RARE_CANDY");
            CheckMissingFlag(0x484, FlagType.FieldItem, "", "ROUTE_132_PROTEIN");
            CheckMissingFlag(0x485, FlagType.FieldItem, "", "ROUTE_133_MAX_REVIVE");
            CheckMissingFlag(0x486, FlagType.FieldItem, "", "ROUTE_134_CARBOS");
            CheckMissingFlag(0x487, FlagType.FieldItem, "", "ROUTE_134_STAR_PIECE");
            CheckMissingFlag(0x488, FlagType.FieldItem, "", "ROUTE_114_ENERGY_POWDER");
            CheckMissingFlag(0x489, FlagType.FieldItem, "", "ROUTE_115_PP_UP");
            CheckMissingFlag(0x48A, FlagType.FieldItem, "", "ARTISAN_CAVE_B1F_HP_UP");
            CheckMissingFlag(0x48B, FlagType.FieldItem, "", "ARTISAN_CAVE_1F_CARBOS");
            CheckMissingFlag(0x48C, FlagType.FieldItem, "", "MAGMA_HIDEOUT_2F_2R_MAX_ELIXIR");
            CheckMissingFlag(0x48D, FlagType.FieldItem, "", "MAGMA_HIDEOUT_2F_2R_FULL_RESTORE");
            CheckMissingFlag(0x48E, FlagType.FieldItem, "", "MAGMA_HIDEOUT_3F_1R_NUGGET");
            CheckMissingFlag(0x48F, FlagType.FieldItem, "", "MAGMA_HIDEOUT_3F_2R_PP_MAX");
            CheckMissingFlag(0x490, FlagType.FieldItem, "", "MAGMA_HIDEOUT_4F_MAX_REVIVE");
            CheckMissingFlag(0x491, FlagType.FieldItem, "", "SAFARI_ZONE_NORTH_EAST_NUGGET");
            CheckMissingFlag(0x492, FlagType.FieldItem, "", "SAFARI_ZONE_SOUTH_EAST_BIG_PEARL");
        }

    }

}
