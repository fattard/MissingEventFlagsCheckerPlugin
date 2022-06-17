using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal class FlagsGen3RS : FlagsOrganizer
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
                    for (int i = 0x258; i <= 0x2B9; ++i)
                        flagHelper.SetEventFlag(i, value);
                    break;

                case FlagType.FieldItem:
                    for (int i = 0x3E8; i <= 0x46F; ++i)
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
            CheckMissingFlag(0x258, FlagType.HiddenItem, "", "0");
            CheckMissingFlag(0x259, FlagType.HiddenItem, "", "1");
            CheckMissingFlag(0x25A, FlagType.HiddenItem, "", "2");
            CheckMissingFlag(0x25B, FlagType.HiddenItem, "", "3");
            CheckMissingFlag(0x25C, FlagType.HiddenItem, "", "4");
            CheckMissingFlag(0x25D, FlagType.HiddenItem, "", "5");
            CheckMissingFlag(0x25E, FlagType.HiddenItem, "", "6");
            CheckMissingFlag(0x25F, FlagType.HiddenItem, "", "7");
            CheckMissingFlag(0x260, FlagType.HiddenItem, "", "8");
            CheckMissingFlag(0x261, FlagType.HiddenItem, "", "9");
            CheckMissingFlag(0x262, FlagType.HiddenItem, "", "A");
            CheckMissingFlag(0x263, FlagType.HiddenItem, "", "B");
            CheckMissingFlag(0x264, FlagType.HiddenItem, "", "C");
            CheckMissingFlag(0x265, FlagType.HiddenItem, "", "D");
            CheckMissingFlag(0x266, FlagType.HiddenItem, "", "E");
            CheckMissingFlag(0x267, FlagType.HiddenItem, "", "F");
            CheckMissingFlag(0x268, FlagType.HiddenItem, "", "10");
            CheckMissingFlag(0x269, FlagType.HiddenItem, "", "11");
            CheckMissingFlag(0x26A, FlagType.HiddenItem, "", "12");
            CheckMissingFlag(0x26B, FlagType.HiddenItem, "", "13");
            CheckMissingFlag(0x26C, FlagType.HiddenItem, "", "14");
            CheckMissingFlag(0x26D, FlagType.HiddenItem, "", "15");
            CheckMissingFlag(0x26E, FlagType.HiddenItem, "", "16");
            CheckMissingFlag(0x26F, FlagType.HiddenItem, "", "17");
            CheckMissingFlag(0x270, FlagType.HiddenItem, "", "18");
            CheckMissingFlag(0x271, FlagType.HiddenItem, "", "19");
            CheckMissingFlag(0x272, FlagType.HiddenItem, "", "1A");
            CheckMissingFlag(0x273, FlagType.HiddenItem, "", "1B");
            CheckMissingFlag(0x274, FlagType.HiddenItem, "", "1C");
            CheckMissingFlag(0x275, FlagType.HiddenItem, "", "1D");
            CheckMissingFlag(0x276, FlagType.HiddenItem, "", "1E");
            CheckMissingFlag(0x277, FlagType.HiddenItem, "", "1F");
            CheckMissingFlag(0x278, FlagType.HiddenItem, "", "20");
            CheckMissingFlag(0x279, FlagType.HiddenItem, "", "21");
            CheckMissingFlag(0x27A, FlagType.HiddenItem, "", "22");
            CheckMissingFlag(0x27B, FlagType.HiddenItem, "", "23");
            CheckMissingFlag(0x27C, FlagType.HiddenItem, "", "24");
            CheckMissingFlag(0x27D, FlagType.HiddenItem, "", "25");
            CheckMissingFlag(0x27E, FlagType.HiddenItem, "", "26");
            CheckMissingFlag(0x27F, FlagType.HiddenItem, "", "27");
            CheckMissingFlag(0x280, FlagType.HiddenItem, "", "28");
            CheckMissingFlag(0x281, FlagType.HiddenItem, "", "29");
            CheckMissingFlag(0x282, FlagType.HiddenItem, "", "2A");
            CheckMissingFlag(0x283, FlagType.HiddenItem, "", "2B");
            CheckMissingFlag(0x284, FlagType.HiddenItem, "", "2C");
            CheckMissingFlag(0x285, FlagType.HiddenItem, "", "2D");
            CheckMissingFlag(0x286, FlagType.HiddenItem, "", "2E");
            CheckMissingFlag(0x287, FlagType.HiddenItem, "", "2F");
            CheckMissingFlag(0x288, FlagType.HiddenItem, "", "30");
            CheckMissingFlag(0x289, FlagType.HiddenItem, "", "31");
            CheckMissingFlag(0x28A, FlagType.HiddenItem, "", "32");
            CheckMissingFlag(0x28B, FlagType.HiddenItem, "", "33");
            CheckMissingFlag(0x28C, FlagType.HiddenItem, "", "34");
            CheckMissingFlag(0x28D, FlagType.HiddenItem, "", "35");
            CheckMissingFlag(0x28E, FlagType.HiddenItem, "", "36");
            CheckMissingFlag(0x28F, FlagType.HiddenItem, "", "37");
            CheckMissingFlag(0x290, FlagType.HiddenItem, "", "38");
            CheckMissingFlag(0x291, FlagType.HiddenItem, "", "39");
            CheckMissingFlag(0x292, FlagType.HiddenItem, "", "3A");
            CheckMissingFlag(0x293, FlagType.HiddenItem, "", "3B");
            CheckMissingFlag(0x294, FlagType.HiddenItem, "", "3C");
            CheckMissingFlag(0x295, FlagType.HiddenItem, "", "3D");
            CheckMissingFlag(0x296, FlagType.HiddenItem, "", "3E");
            CheckMissingFlag(0x297, FlagType.HiddenItem, "", "3F");
            CheckMissingFlag(0x298, FlagType.HiddenItem, "", "40");
            CheckMissingFlag(0x299, FlagType.HiddenItem, "", "41");
            CheckMissingFlag(0x29A, FlagType.HiddenItem, "", "42");
            CheckMissingFlag(0x29B, FlagType.HiddenItem, "", "43");
            CheckMissingFlag(0x29C, FlagType.HiddenItem, "", "44");
            CheckMissingFlag(0x29D, FlagType.HiddenItem, "", "45");
            CheckMissingFlag(0x29E, FlagType.HiddenItem, "", "46");
            CheckMissingFlag(0x29F, FlagType.HiddenItem, "", "47");
            CheckMissingFlag(0x2A0, FlagType.HiddenItem, "", "48");
            CheckMissingFlag(0x2A1, FlagType.HiddenItem, "", "49");
            CheckMissingFlag(0x2A2, FlagType.HiddenItem, "", "4A");
            CheckMissingFlag(0x2A3, FlagType.HiddenItem, "", "4B");
            CheckMissingFlag(0x2A4, FlagType.HiddenItem, "", "4C");
            CheckMissingFlag(0x2A5, FlagType.HiddenItem, "", "4D");
            CheckMissingFlag(0x2A6, FlagType.HiddenItem, "", "4E");
            CheckMissingFlag(0x2A7, FlagType.HiddenItem, "", "4F");
            CheckMissingFlag(0x2A8, FlagType.HiddenItem, "", "50");
            CheckMissingFlag(0x2A9, FlagType.HiddenItem, "", "51");
            CheckMissingFlag(0x2AA, FlagType.HiddenItem, "", "52");
            CheckMissingFlag(0x2AB, FlagType.HiddenItem, "", "53");
            CheckMissingFlag(0x2AC, FlagType.HiddenItem, "", "54");
            CheckMissingFlag(0x2AD, FlagType.HiddenItem, "", "55");
            CheckMissingFlag(0x2AE, FlagType.HiddenItem, "", "56");
            CheckMissingFlag(0x2AF, FlagType.HiddenItem, "", "57");
            CheckMissingFlag(0x2B0, FlagType.HiddenItem, "", "58");
            CheckMissingFlag(0x2B1, FlagType.HiddenItem, "", "59");
            CheckMissingFlag(0x2B2, FlagType.HiddenItem, "", "5A");
            CheckMissingFlag(0x2B3, FlagType.HiddenItem, "", "5B");
            CheckMissingFlag(0x2B4, FlagType.HiddenItem, "", "5C");
            CheckMissingFlag(0x2B5, FlagType.HiddenItem, "", "5D");
            CheckMissingFlag(0x2B6, FlagType.HiddenItem, "", "5E");
            CheckMissingFlag(0x2B7, FlagType.HiddenItem, "", "5F");
            CheckMissingFlag(0x2B8, FlagType.HiddenItem, "", "BLACK_GLASSES");
            CheckMissingFlag(0x2B9, FlagType.HiddenItem, "", "61");

            // Field items
            CheckMissingFlag(0x3E8, FlagType.FieldItem, "", "ROUTE102_1");
            CheckMissingFlag(0x3E9, FlagType.FieldItem, "", "ROUTE116_1");
            CheckMissingFlag(0x3EA, FlagType.FieldItem, "", "ROUTE104_1");
            CheckMissingFlag(0x3EB, FlagType.FieldItem, "", "ROUTE105_1");
            CheckMissingFlag(0x3EC, FlagType.FieldItem, "", "ROUTE106_1");
            CheckMissingFlag(0x3ED, FlagType.FieldItem, "", "ROUTE109_1");
            CheckMissingFlag(0x3EE, FlagType.FieldItem, "", "ROUTE110_1");
            CheckMissingFlag(0x3EF, FlagType.FieldItem, "", "ROUTE110_2");
            CheckMissingFlag(0x3F0, FlagType.FieldItem, "", "ROUTE111_1");
            CheckMissingFlag(0x3F1, FlagType.FieldItem, "", "ROUTE111_2");
            CheckMissingFlag(0x3F2, FlagType.FieldItem, "", "ROUTE111_3");
            CheckMissingFlag(0x3F3, FlagType.FieldItem, "", "ROUTE112_1");
            CheckMissingFlag(0x3F4, FlagType.FieldItem, "", "ROUTE113_1");
            CheckMissingFlag(0x3F5, FlagType.FieldItem, "", "ROUTE113_2");
            CheckMissingFlag(0x3F6, FlagType.FieldItem, "", "ROUTE114_1");
            CheckMissingFlag(0x3F7, FlagType.FieldItem, "", "ROUTE114_2");
            CheckMissingFlag(0x3F8, FlagType.FieldItem, "", "ROUTE115_1");
            CheckMissingFlag(0x3F9, FlagType.FieldItem, "", "ROUTE115_2");
            CheckMissingFlag(0x3FA, FlagType.FieldItem, "", "ROUTE115_3");
            CheckMissingFlag(0x3FB, FlagType.FieldItem, "", "ROUTE116_2");
            CheckMissingFlag(0x3FC, FlagType.FieldItem, "", "ROUTE116_3");
            CheckMissingFlag(0x3FD, FlagType.FieldItem, "", "ROUTE116_4");
            CheckMissingFlag(0x3FE, FlagType.FieldItem, "", "ROUTE117_1");
            CheckMissingFlag(0x3FF, FlagType.FieldItem, "", "ROUTE117_2");
            CheckMissingFlag(0x400, FlagType.FieldItem, "", "ROUTE119_1");
            CheckMissingFlag(0x401, FlagType.FieldItem, "", "ROUTE119_2");
            CheckMissingFlag(0x402, FlagType.FieldItem, "", "ROUTE119_3");
            CheckMissingFlag(0x403, FlagType.FieldItem, "", "ROUTE119_4");
            CheckMissingFlag(0x404, FlagType.FieldItem, "", "ROUTE119_5");
            CheckMissingFlag(0x405, FlagType.FieldItem, "", "ROUTE119_6");
            CheckMissingFlag(0x406, FlagType.FieldItem, "", "ROUTE120_1");
            CheckMissingFlag(0x407, FlagType.FieldItem, "", "ROUTE120_2");
            CheckMissingFlag(0x408, FlagType.FieldItem, "", "ROUTE123_1");
            CheckMissingFlag(0x409, FlagType.FieldItem, "", "ROUTE123_2");
            CheckMissingFlag(0x40A, FlagType.FieldItem, "", "ROUTE127_1");
            CheckMissingFlag(0x40B, FlagType.FieldItem, "", "ROUTE127_2");
            CheckMissingFlag(0x40C, FlagType.FieldItem, "", "ROUTE132_1");
            CheckMissingFlag(0x40D, FlagType.FieldItem, "", "ROUTE133_1");
            CheckMissingFlag(0x40E, FlagType.FieldItem, "", "ROUTE133_2");
            CheckMissingFlag(0x40F, FlagType.FieldItem, "", "PETALBURG_1");
            CheckMissingFlag(0x410, FlagType.FieldItem, "", "PETALBURG_2");
            CheckMissingFlag(0x411, FlagType.FieldItem, "", "RUSTBORO_1");
            CheckMissingFlag(0x412, FlagType.FieldItem, "", "LILYCOVE_1");
            CheckMissingFlag(0x413, FlagType.FieldItem, "", "MOSSDEEP_1");
            CheckMissingFlag(0x414, FlagType.FieldItem, "", "METEOR_FALLS_1F_1R_1");
            CheckMissingFlag(0x415, FlagType.FieldItem, "", "METEOR_FALLS_1F_1R_2");
            CheckMissingFlag(0x416, FlagType.FieldItem, "", "METEOR_FALLS_1F_1R_3");
            CheckMissingFlag(0x417, FlagType.FieldItem, "", "METEOR_FALLS_1F_1R_4");
            CheckMissingFlag(0x418, FlagType.FieldItem, "", "RUSTURF_TUNNEL_1");
            CheckMissingFlag(0x419, FlagType.FieldItem, "", "RUSTURF_TUNNEL_2");
            CheckMissingFlag(0x41A, FlagType.FieldItem, "", "GRANITE_CAVE_1F_1");
            CheckMissingFlag(0x41B, FlagType.FieldItem, "", "GRANITE_CAVE_B1F_1");
            CheckMissingFlag(0x41C, FlagType.FieldItem, "", "MT_PYRE_5F_1");
            CheckMissingFlag(0x41D, FlagType.FieldItem, "", "GRANITE_CAVE_B2F_1");
            CheckMissingFlag(0x41E, FlagType.FieldItem, "", "GRANITE_CAVE_B2F_2");
            CheckMissingFlag(0x41F, FlagType.FieldItem, "", "PETALBURG_WOODS_1");
            CheckMissingFlag(0x420, FlagType.FieldItem, "", "PETALBURG_WOODS_2");
            CheckMissingFlag(0x421, FlagType.FieldItem, "", "ROUTE104_2");
            CheckMissingFlag(0x422, FlagType.FieldItem, "", "PETALBURG_WOODS_3");
            CheckMissingFlag(0x423, FlagType.FieldItem, "", "CAVE_OF_ORIGIN_B3F_1");
            CheckMissingFlag(0x424, FlagType.FieldItem, "", "TRICK_HOUSE_PUZZLE_1_1");
            CheckMissingFlag(0x425, FlagType.FieldItem, "", "TRICK_HOUSE_PUZZLE_2_1");
            CheckMissingFlag(0x426, FlagType.FieldItem, "", "TRICK_HOUSE_PUZZLE_2_2");
            CheckMissingFlag(0x427, FlagType.FieldItem, "", "TRICK_HOUSE_PUZZLE_3_1");
            CheckMissingFlag(0x428, FlagType.FieldItem, "", "TRICK_HOUSE_PUZZLE_3_2");
            CheckMissingFlag(0x429, FlagType.FieldItem, "", "TRICK_HOUSE_PUZZLE_4_1");
            CheckMissingFlag(0x42A, FlagType.FieldItem, "", "ROUTE124_1");
            CheckMissingFlag(0x42B, FlagType.FieldItem, "", "TRICK_HOUSE_PUZZLE_6_1");
            CheckMissingFlag(0x42C, FlagType.FieldItem, "", "TRICK_HOUSE_PUZZLE_7_1");
            CheckMissingFlag(0x42D, FlagType.FieldItem, "", "TRICK_HOUSE_PUZZLE_8_1");
            CheckMissingFlag(0x42E, FlagType.FieldItem, "", "JAGGED_PASS_1");
            CheckMissingFlag(0x42F, FlagType.FieldItem, "", "AQUA_HIDEOUT_B1F_1");
            CheckMissingFlag(0x430, FlagType.FieldItem, "", "AQUA_HIDEOUT_B2F_1");
            CheckMissingFlag(0x431, FlagType.FieldItem, "", "MT_PYRE_EXTERIOR_1");
            CheckMissingFlag(0x432, FlagType.FieldItem, "", "MT_PYRE_EXTERIOR_2");
            CheckMissingFlag(0x433, FlagType.FieldItem, "", "NEW_MAUVILLE_INSIDE_1");
            CheckMissingFlag(0x434, FlagType.FieldItem, "", "NEW_MAUVILLE_INSIDE_2");
            CheckMissingFlag(0x435, FlagType.FieldItem, "", "ABANDONED_SHIP_HIDDEN_FLOOR_ROOMS_1");
            CheckMissingFlag(0x436, FlagType.FieldItem, "", "ABANDONED_SHIP_HIDDEN_FLOOR_ROOMS_2");
            CheckMissingFlag(0x437, FlagType.FieldItem, "", "SCORCHED_SLAB_1");
            CheckMissingFlag(0x438, FlagType.FieldItem, "", "METEOR_FALLS_B1F_2R_1");
            CheckMissingFlag(0x439, FlagType.FieldItem, "", "SHOAL_CAVE_LOW_TIDE_ENTRANCE_1");
            CheckMissingFlag(0x43A, FlagType.FieldItem, "", "SHOAL_CAVE_LOW_TIDE_INNER_ROOM_1");
            CheckMissingFlag(0x43B, FlagType.FieldItem, "", "SHOAL_CAVE_LOW_TIDE_STAIRS_ROOM_1");
            CheckMissingFlag(0x43C, FlagType.FieldItem, "", "VICTORY_ROAD_1F_1");
            CheckMissingFlag(0x43D, FlagType.FieldItem, "", "VICTORY_ROAD_1F_2");
            CheckMissingFlag(0x43E, FlagType.FieldItem, "", "VICTORY_ROAD_B1F_1");
            CheckMissingFlag(0x43F, FlagType.FieldItem, "", "VICTORY_ROAD_B1F_2");
            CheckMissingFlag(0x440, FlagType.FieldItem, "", "VICTORY_ROAD_B2F_1");
            CheckMissingFlag(0x441, FlagType.FieldItem, "", "MT_PYRE_6F_1");
            CheckMissingFlag(0x442, FlagType.FieldItem, "", "SEAFLOOR_CAVERN_ROOM_9_1");
            CheckMissingFlag(0x443, FlagType.FieldItem, "", "FIERY_PATH_1");
            CheckMissingFlag(0x444, FlagType.FieldItem, "", "ROUTE124_2");
            CheckMissingFlag(0x445, FlagType.FieldItem, "", "ROUTE124_3");
            CheckMissingFlag(0x446, FlagType.FieldItem, "", "SAFARI_ZONE_NORTHWEST_1");
            CheckMissingFlag(0x447, FlagType.FieldItem, "", "ABANDONED_SHIP_ROOMS_1F_1");
            CheckMissingFlag(0x448, FlagType.FieldItem, "", "ABANDONED_SHIP_ROOMS_B1F_1");
            CheckMissingFlag(0x449, FlagType.FieldItem, "", "ABANDONED_SHIP_ROOMS_2_B1F_1");
            CheckMissingFlag(0x44A, FlagType.FieldItem, "", "ABANDONED_SHIP_ROOM_B1F_1");
            CheckMissingFlag(0x44B, FlagType.FieldItem, "", "ABANDONED_SHIP_ROOMS_2_1F_1");
            CheckMissingFlag(0x44C, FlagType.FieldItem, "", "ABANDONED_SHIP_CAPTAINS_OFFICE_1");
            CheckMissingFlag(0x44D, FlagType.FieldItem, "", "ABANDONED_SHIP_HIDDEN_FLOOR_ROOMS_3");
            CheckMissingFlag(0x44E, FlagType.FieldItem, "", "ABANDONED_SHIP_HIDDEN_FLOOR_ROOMS_4");
            CheckMissingFlag(0x44F, FlagType.FieldItem, "", "ROUTE121_1");
            CheckMissingFlag(0x450, FlagType.FieldItem, "", "ROUTE123_3");
            CheckMissingFlag(0x451, FlagType.FieldItem, "", "ROUTE126_1");
            CheckMissingFlag(0x452, FlagType.FieldItem, "", "ROUTE119_7");
            CheckMissingFlag(0x453, FlagType.FieldItem, "", "ROUTE120_3");
            CheckMissingFlag(0x454, FlagType.FieldItem, "", "ROUTE120_4");
            CheckMissingFlag(0x455, FlagType.FieldItem, "", "ROUTE123_4");
            CheckMissingFlag(0x456, FlagType.FieldItem, "", "NEW_MAUVILLE_INSIDE_3");
            CheckMissingFlag(0x457, FlagType.FieldItem, "", "FIERY_PATH_2");
            CheckMissingFlag(0x458, FlagType.FieldItem, "", "SHOAL_CAVE_LOW_TIDE_ICE_ROOM_1");
            CheckMissingFlag(0x459, FlagType.FieldItem, "", "SHOAL_CAVE_LOW_TIDE_ICE_ROOM_2");
            CheckMissingFlag(0x45A, FlagType.FieldItem, "", "ROUTE103_1");
            CheckMissingFlag(0x45B, FlagType.FieldItem, "", "ROUTE104_3");
            CheckMissingFlag(0x45C, FlagType.FieldItem, "", "MAUVILLE_1");
            CheckMissingFlag(0x45D, FlagType.FieldItem, "", "PETALBURG_WOODS_4");
            CheckMissingFlag(0x45E, FlagType.FieldItem, "", "ROUTE115_4");
            CheckMissingFlag(0x45F, FlagType.FieldItem, "", "SAFARI_ZONE_NORTHEAST_1");
            CheckMissingFlag(0x460, FlagType.FieldItem, "", "MT_PYRE_3F_1");
            CheckMissingFlag(0x461, FlagType.FieldItem, "", "ROUTE118_1");
            CheckMissingFlag(0x462, FlagType.FieldItem, "", "NEW_MAUVILLE_INSIDE_4");
            CheckMissingFlag(0x463, FlagType.FieldItem, "", "NEW_MAUVILLE_INSIDE_5");
            CheckMissingFlag(0x464, FlagType.FieldItem, "", "AQUA_HIDEOUT_B1F_2");
            CheckMissingFlag(0x465, FlagType.FieldItem, "", "MAGMA_HIDEOUT_B1F_1");
            CheckMissingFlag(0x466, FlagType.FieldItem, "", "MAGMA_HIDEOUT_B1F_2");
            CheckMissingFlag(0x467, FlagType.FieldItem, "", "MAGMA_HIDEOUT_B2F_1");
            CheckMissingFlag(0x469, FlagType.FieldItem, "", "MT_PYRE_2F_1");
            CheckMissingFlag(0x46A, FlagType.FieldItem, "", "MT_PYRE_4F_1");
            CheckMissingFlag(0x46B, FlagType.FieldItem, "", "SAFARI_ZONE_SOUTHWEST");
            CheckMissingFlag(0x46C, FlagType.FieldItem, "", "AQUA_HIDEOUT_B1F_3 ");
            CheckMissingFlag(0x46D, FlagType.FieldItem, "", "MOSSDEEP_STEVENS_HOUSE_HM08");
            CheckMissingFlag(0x46E, FlagType.FieldItem, "", "MAGMA_HIDEOUT_B1F_3");
            CheckMissingFlag(0x46F, FlagType.FieldItem, "", "ROUTE104_4");
        }

    }

}
