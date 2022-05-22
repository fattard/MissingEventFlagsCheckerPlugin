using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    static class FlagsGen3RS
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
            s_eventFlags = (savFile as SAV3).GetEventFlags();
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
            CheckFlag(0x258, "FLAG_HIDDEN_ITEM_0");
            CheckFlag(0x259, "FLAG_HIDDEN_ITEM_1");
            CheckFlag(0x25A, "FLAG_HIDDEN_ITEM_2");
            CheckFlag(0x25B, "FLAG_HIDDEN_ITEM_3");
            CheckFlag(0x25C, "FLAG_HIDDEN_ITEM_4");
            CheckFlag(0x25D, "FLAG_HIDDEN_ITEM_5");
            CheckFlag(0x25E, "FLAG_HIDDEN_ITEM_6");
            CheckFlag(0x25F, "FLAG_HIDDEN_ITEM_7");
            CheckFlag(0x260, "FLAG_HIDDEN_ITEM_8");
            CheckFlag(0x261, "FLAG_HIDDEN_ITEM_9");
            CheckFlag(0x262, "FLAG_HIDDEN_ITEM_A");
            CheckFlag(0x263, "FLAG_HIDDEN_ITEM_B");
            CheckFlag(0x264, "FLAG_HIDDEN_ITEM_C");
            CheckFlag(0x265, "FLAG_HIDDEN_ITEM_D");
            CheckFlag(0x266, "FLAG_HIDDEN_ITEM_E");
            CheckFlag(0x267, "FLAG_HIDDEN_ITEM_F");
            CheckFlag(0x268, "FLAG_HIDDEN_ITEM_10");
            CheckFlag(0x269, "FLAG_HIDDEN_ITEM_11");
            CheckFlag(0x26A, "FLAG_HIDDEN_ITEM_12");
            CheckFlag(0x26B, "FLAG_HIDDEN_ITEM_13");
            CheckFlag(0x26C, "FLAG_HIDDEN_ITEM_14");
            CheckFlag(0x26D, "FLAG_HIDDEN_ITEM_15");
            CheckFlag(0x26E, "FLAG_HIDDEN_ITEM_16");
            CheckFlag(0x26F, "FLAG_HIDDEN_ITEM_17");
            CheckFlag(0x270, "FLAG_HIDDEN_ITEM_18");
            CheckFlag(0x271, "FLAG_HIDDEN_ITEM_19");
            CheckFlag(0x272, "FLAG_HIDDEN_ITEM_1A");
            CheckFlag(0x273, "FLAG_HIDDEN_ITEM_1B");
            CheckFlag(0x274, "FLAG_HIDDEN_ITEM_1C");
            CheckFlag(0x275, "FLAG_HIDDEN_ITEM_1D");
            CheckFlag(0x276, "FLAG_HIDDEN_ITEM_1E");
            CheckFlag(0x277, "FLAG_HIDDEN_ITEM_1F");
            CheckFlag(0x278, "FLAG_HIDDEN_ITEM_20");
            CheckFlag(0x279, "FLAG_HIDDEN_ITEM_21");
            CheckFlag(0x27A, "FLAG_HIDDEN_ITEM_22");
            CheckFlag(0x27B, "FLAG_HIDDEN_ITEM_23");
            CheckFlag(0x27C, "FLAG_HIDDEN_ITEM_24");
            CheckFlag(0x27D, "FLAG_HIDDEN_ITEM_25");
            CheckFlag(0x27E, "FLAG_HIDDEN_ITEM_26");
            CheckFlag(0x27F, "FLAG_HIDDEN_ITEM_27");
            CheckFlag(0x280, "FLAG_HIDDEN_ITEM_28");
            CheckFlag(0x281, "FLAG_HIDDEN_ITEM_29");
            CheckFlag(0x282, "FLAG_HIDDEN_ITEM_2A");
            CheckFlag(0x283, "FLAG_HIDDEN_ITEM_2B");
            CheckFlag(0x284, "FLAG_HIDDEN_ITEM_2C");
            CheckFlag(0x285, "FLAG_HIDDEN_ITEM_2D");
            CheckFlag(0x286, "FLAG_HIDDEN_ITEM_2E");
            CheckFlag(0x287, "FLAG_HIDDEN_ITEM_2F");
            CheckFlag(0x288, "FLAG_HIDDEN_ITEM_30");
            CheckFlag(0x289, "FLAG_HIDDEN_ITEM_31");
            CheckFlag(0x28A, "FLAG_HIDDEN_ITEM_32");
            CheckFlag(0x28B, "FLAG_HIDDEN_ITEM_33");
            CheckFlag(0x28C, "FLAG_HIDDEN_ITEM_34");
            CheckFlag(0x28D, "FLAG_HIDDEN_ITEM_35");
            CheckFlag(0x28E, "FLAG_HIDDEN_ITEM_36");
            CheckFlag(0x28F, "FLAG_HIDDEN_ITEM_37");
            CheckFlag(0x290, "FLAG_HIDDEN_ITEM_38");
            CheckFlag(0x291, "FLAG_HIDDEN_ITEM_39");
            CheckFlag(0x292, "FLAG_HIDDEN_ITEM_3A");
            CheckFlag(0x293, "FLAG_HIDDEN_ITEM_3B");
            CheckFlag(0x294, "FLAG_HIDDEN_ITEM_3C");
            CheckFlag(0x295, "FLAG_HIDDEN_ITEM_3D");
            CheckFlag(0x296, "FLAG_HIDDEN_ITEM_3E");
            CheckFlag(0x297, "FLAG_HIDDEN_ITEM_3F");
            CheckFlag(0x298, "FLAG_HIDDEN_ITEM_40");
            CheckFlag(0x299, "FLAG_HIDDEN_ITEM_41");
            CheckFlag(0x29A, "FLAG_HIDDEN_ITEM_42");
            CheckFlag(0x29B, "FLAG_HIDDEN_ITEM_43");
            CheckFlag(0x29C, "FLAG_HIDDEN_ITEM_44");
            CheckFlag(0x29D, "FLAG_HIDDEN_ITEM_45");
            CheckFlag(0x29E, "FLAG_HIDDEN_ITEM_46");
            CheckFlag(0x29F, "FLAG_HIDDEN_ITEM_47");
            CheckFlag(0x2A0, "FLAG_HIDDEN_ITEM_48");
            CheckFlag(0x2A1, "FLAG_HIDDEN_ITEM_49");
            CheckFlag(0x2A2, "FLAG_HIDDEN_ITEM_4A");
            CheckFlag(0x2A3, "FLAG_HIDDEN_ITEM_4B");
            CheckFlag(0x2A4, "FLAG_HIDDEN_ITEM_4C");
            CheckFlag(0x2A5, "FLAG_HIDDEN_ITEM_4D");
            CheckFlag(0x2A6, "FLAG_HIDDEN_ITEM_4E");
            CheckFlag(0x2A7, "FLAG_HIDDEN_ITEM_4F");
            CheckFlag(0x2A8, "FLAG_HIDDEN_ITEM_50");
            CheckFlag(0x2A9, "FLAG_HIDDEN_ITEM_51");
            CheckFlag(0x2AA, "FLAG_HIDDEN_ITEM_52");
            CheckFlag(0x2AB, "FLAG_HIDDEN_ITEM_53");
            CheckFlag(0x2AC, "FLAG_HIDDEN_ITEM_54");
            CheckFlag(0x2AD, "FLAG_HIDDEN_ITEM_55");
            CheckFlag(0x2AE, "FLAG_HIDDEN_ITEM_56");
            CheckFlag(0x2AF, "FLAG_HIDDEN_ITEM_57");
            CheckFlag(0x2B0, "FLAG_HIDDEN_ITEM_58");
            CheckFlag(0x2B1, "FLAG_HIDDEN_ITEM_59");
            CheckFlag(0x2B2, "FLAG_HIDDEN_ITEM_5A");
            CheckFlag(0x2B3, "FLAG_HIDDEN_ITEM_5B");
            CheckFlag(0x2B4, "FLAG_HIDDEN_ITEM_5C");
            CheckFlag(0x2B5, "FLAG_HIDDEN_ITEM_5D");
            CheckFlag(0x2B6, "FLAG_HIDDEN_ITEM_5E");
            CheckFlag(0x2B7, "FLAG_HIDDEN_ITEM_5F");
            CheckFlag(0x2B8, "FLAG_HIDDEN_ITEM_BLACK_GLASSES");
            CheckFlag(0x2B9, "FLAG_HIDDEN_ITEM_61");

            // Normal items
            CheckFlag(0x3E8, "FLAG_ITEM_ROUTE102_1");
            CheckFlag(0x3E9, "FLAG_ITEM_ROUTE116_1");
            CheckFlag(0x3EA, "FLAG_ITEM_ROUTE104_1");
            CheckFlag(0x3EB, "FLAG_ITEM_ROUTE105_1");
            CheckFlag(0x3EC, "FLAG_ITEM_ROUTE106_1");
            CheckFlag(0x3ED, "FLAG_ITEM_ROUTE109_1");
            CheckFlag(0x3EE, "FLAG_ITEM_ROUTE110_1");
            CheckFlag(0x3EF, "FLAG_ITEM_ROUTE110_2");
            CheckFlag(0x3F0, "FLAG_ITEM_ROUTE111_1");
            CheckFlag(0x3F1, "FLAG_ITEM_ROUTE111_2");
            CheckFlag(0x3F2, "FLAG_ITEM_ROUTE111_3");
            CheckFlag(0x3F3, "FLAG_ITEM_ROUTE112_1");
            CheckFlag(0x3F4, "FLAG_ITEM_ROUTE113_1");
            CheckFlag(0x3F5, "FLAG_ITEM_ROUTE113_2");
            CheckFlag(0x3F6, "FLAG_ITEM_ROUTE114_1");
            CheckFlag(0x3F7, "FLAG_ITEM_ROUTE114_2");
            CheckFlag(0x3F8, "FLAG_ITEM_ROUTE115_1");
            CheckFlag(0x3F9, "FLAG_ITEM_ROUTE115_2");
            CheckFlag(0x3FA, "FLAG_ITEM_ROUTE115_3");
            CheckFlag(0x3FB, "FLAG_ITEM_ROUTE116_2");
            CheckFlag(0x3FC, "FLAG_ITEM_ROUTE116_3");
            CheckFlag(0x3FD, "FLAG_ITEM_ROUTE116_4");
            CheckFlag(0x3FE, "FLAG_ITEM_ROUTE117_1");
            CheckFlag(0x3FF, "FLAG_ITEM_ROUTE117_2");
            CheckFlag(0x400, "FLAG_ITEM_ROUTE119_1");
            CheckFlag(0x401, "FLAG_ITEM_ROUTE119_2");
            CheckFlag(0x402, "FLAG_ITEM_ROUTE119_3");
            CheckFlag(0x403, "FLAG_ITEM_ROUTE119_4");
            CheckFlag(0x404, "FLAG_ITEM_ROUTE119_5");
            CheckFlag(0x405, "FLAG_ITEM_ROUTE119_6");
            CheckFlag(0x406, "FLAG_ITEM_ROUTE120_1");
            CheckFlag(0x407, "FLAG_ITEM_ROUTE120_2");
            CheckFlag(0x408, "FLAG_ITEM_ROUTE123_1");
            CheckFlag(0x409, "FLAG_ITEM_ROUTE123_2");
            CheckFlag(0x40A, "FLAG_ITEM_ROUTE127_1");
            CheckFlag(0x40B, "FLAG_ITEM_ROUTE127_2");
            CheckFlag(0x40C, "FLAG_ITEM_ROUTE132_1");
            CheckFlag(0x40D, "FLAG_ITEM_ROUTE133_1");
            CheckFlag(0x40E, "FLAG_ITEM_ROUTE133_2");
            CheckFlag(0x40F, "FLAG_ITEM_PETALBURG_1");
            CheckFlag(0x410, "FLAG_ITEM_PETALBURG_2");
            CheckFlag(0x411, "FLAG_ITEM_RUSTBORO_1");
            CheckFlag(0x412, "FLAG_ITEM_LILYCOVE_1");
            CheckFlag(0x413, "FLAG_ITEM_MOSSDEEP_1");
            CheckFlag(0x414, "FLAG_ITEM_METEOR_FALLS_1F_1R_1");
            CheckFlag(0x415, "FLAG_ITEM_METEOR_FALLS_1F_1R_2");
            CheckFlag(0x416, "FLAG_ITEM_METEOR_FALLS_1F_1R_3");
            CheckFlag(0x417, "FLAG_ITEM_METEOR_FALLS_1F_1R_4");
            CheckFlag(0x418, "FLAG_ITEM_RUSTURF_TUNNEL_1");
            CheckFlag(0x419, "FLAG_ITEM_RUSTURF_TUNNEL_2");
            CheckFlag(0x41A, "FLAG_ITEM_GRANITE_CAVE_1F_1");
            CheckFlag(0x41B, "FLAG_ITEM_GRANITE_CAVE_B1F_1");
            CheckFlag(0x41C, "FLAG_ITEM_MT_PYRE_5F_1");
            CheckFlag(0x41D, "FLAG_ITEM_GRANITE_CAVE_B2F_1");
            CheckFlag(0x41E, "FLAG_ITEM_GRANITE_CAVE_B2F_2");
            CheckFlag(0x41F, "FLAG_ITEM_PETALBURG_WOODS_1");
            CheckFlag(0x420, "FLAG_ITEM_PETALBURG_WOODS_2");
            CheckFlag(0x421, "FLAG_ITEM_ROUTE104_2");
            CheckFlag(0x422, "FLAG_ITEM_PETALBURG_WOODS_3");
            CheckFlag(0x423, "FLAG_ITEM_CAVE_OF_ORIGIN_B3F_1");
            CheckFlag(0x424, "FLAG_ITEM_TRICK_HOUSE_PUZZLE_1_1");
            CheckFlag(0x425, "FLAG_ITEM_TRICK_HOUSE_PUZZLE_2_1");
            CheckFlag(0x426, "FLAG_ITEM_TRICK_HOUSE_PUZZLE_2_2");
            CheckFlag(0x427, "FLAG_ITEM_TRICK_HOUSE_PUZZLE_3_1");
            CheckFlag(0x428, "FLAG_ITEM_TRICK_HOUSE_PUZZLE_3_2");
            CheckFlag(0x429, "FLAG_ITEM_TRICK_HOUSE_PUZZLE_4_1");
            CheckFlag(0x42A, "FLAG_ITEM_ROUTE124_1");
            CheckFlag(0x42B, "FLAG_ITEM_TRICK_HOUSE_PUZZLE_6_1");
            CheckFlag(0x42C, "FLAG_ITEM_TRICK_HOUSE_PUZZLE_7_1");
            CheckFlag(0x42D, "FLAG_ITEM_TRICK_HOUSE_PUZZLE_8_1");
            CheckFlag(0x42E, "FLAG_ITEM_JAGGED_PASS_1");
            CheckFlag(0x42F, "FLAG_ITEM_AQUA_HIDEOUT_B1F_1");
            CheckFlag(0x430, "FLAG_ITEM_AQUA_HIDEOUT_B2F_1");
            CheckFlag(0x431, "FLAG_ITEM_MT_PYRE_EXTERIOR_1");
            CheckFlag(0x432, "FLAG_ITEM_MT_PYRE_EXTERIOR_2");
            CheckFlag(0x433, "FLAG_ITEM_NEW_MAUVILLE_INSIDE_1");
            CheckFlag(0x434, "FLAG_ITEM_NEW_MAUVILLE_INSIDE_2");
            CheckFlag(0x435, "FLAG_ITEM_ABANDONED_SHIP_HIDDEN_FLOOR_ROOMS_1");
            CheckFlag(0x436, "FLAG_ITEM_ABANDONED_SHIP_HIDDEN_FLOOR_ROOMS_2");
            CheckFlag(0x437, "FLAG_ITEM_SCORCHED_SLAB_1");
            CheckFlag(0x438, "FLAG_ITEM_METEOR_FALLS_B1F_2R_1");
            CheckFlag(0x439, "FLAG_ITEM_SHOAL_CAVE_LOW_TIDE_ENTRANCE_1");
            CheckFlag(0x43A, "FLAG_ITEM_SHOAL_CAVE_LOW_TIDE_INNER_ROOM_1");
            CheckFlag(0x43B, "FLAG_ITEM_SHOAL_CAVE_LOW_TIDE_STAIRS_ROOM_1");
            CheckFlag(0x43C, "FLAG_ITEM_VICTORY_ROAD_1F_1");
            CheckFlag(0x43D, "FLAG_ITEM_VICTORY_ROAD_1F_2");
            CheckFlag(0x43E, "FLAG_ITEM_VICTORY_ROAD_B1F_1");
            CheckFlag(0x43F, "FLAG_ITEM_VICTORY_ROAD_B1F_2");
            CheckFlag(0x440, "FLAG_ITEM_VICTORY_ROAD_B2F_1");
            CheckFlag(0x441, "FLAG_ITEM_MT_PYRE_6F_1");
            CheckFlag(0x442, "FLAG_ITEM_SEAFLOOR_CAVERN_ROOM_9_1");
            CheckFlag(0x443, "FLAG_ITEM_FIERY_PATH_1");
            CheckFlag(0x444, "FLAG_ITEM_ROUTE124_2");
            CheckFlag(0x445, "FLAG_ITEM_ROUTE124_3");
            CheckFlag(0x446, "FLAG_ITEM_SAFARI_ZONE_NORTHWEST_1");
            CheckFlag(0x447, "FLAG_ITEM_ABANDONED_SHIP_ROOMS_1F_1");
            CheckFlag(0x448, "FLAG_ITEM_ABANDONED_SHIP_ROOMS_B1F_1");
            CheckFlag(0x449, "FLAG_ITEM_ABANDONED_SHIP_ROOMS_2_B1F_1");
            CheckFlag(0x44A, "FLAG_ITEM_ABANDONED_SHIP_ROOM_B1F_1");
            CheckFlag(0x44B, "FLAG_ITEM_ABANDONED_SHIP_ROOMS_2_1F_1");
            CheckFlag(0x44C, "FLAG_ITEM_ABANDONED_SHIP_CAPTAINS_OFFICE_1");
            CheckFlag(0x44D, "FLAG_ITEM_ABANDONED_SHIP_HIDDEN_FLOOR_ROOMS_3");
            CheckFlag(0x44E, "FLAG_ITEM_ABANDONED_SHIP_HIDDEN_FLOOR_ROOMS_4");
            CheckFlag(0x44F, "FLAG_ITEM_ROUTE121_1");
            CheckFlag(0x450, "FLAG_ITEM_ROUTE123_3");
            CheckFlag(0x451, "FLAG_ITEM_ROUTE126_1");
            CheckFlag(0x452, "FLAG_ITEM_ROUTE119_7");
            CheckFlag(0x453, "FLAG_ITEM_ROUTE120_3");
            CheckFlag(0x454, "FLAG_ITEM_ROUTE120_4");
            CheckFlag(0x455, "FLAG_ITEM_ROUTE123_4");
            CheckFlag(0x456, "FLAG_ITEM_NEW_MAUVILLE_INSIDE_3");
            CheckFlag(0x457, "FLAG_ITEM_FIERY_PATH_2");
            CheckFlag(0x458, "FLAG_ITEM_SHOAL_CAVE_LOW_TIDE_ICE_ROOM_1");
            CheckFlag(0x459, "FLAG_ITEM_SHOAL_CAVE_LOW_TIDE_ICE_ROOM_2");
            CheckFlag(0x45A, "FLAG_ITEM_ROUTE103_1");
            CheckFlag(0x45B, "FLAG_ITEM_ROUTE104_3");
            CheckFlag(0x45C, "FLAG_ITEM_MAUVILLE_1");
            CheckFlag(0x45D, "FLAG_ITEM_PETALBURG_WOODS_4");
            CheckFlag(0x45E, "FLAG_ITEM_ROUTE115_4");
            CheckFlag(0x45F, "FLAG_ITEM_SAFARI_ZONE_NORTHEAST_1");
            CheckFlag(0x460, "FLAG_ITEM_MT_PYRE_3F_1");
            CheckFlag(0x461, "FLAG_ITEM_ROUTE118_1");
            CheckFlag(0x462, "FLAG_ITEM_NEW_MAUVILLE_INSIDE_4");
            CheckFlag(0x463, "FLAG_ITEM_NEW_MAUVILLE_INSIDE_5");
            CheckFlag(0x464, "FLAG_ITEM_AQUA_HIDEOUT_B1F_2");
            CheckFlag(0x465, "FLAG_ITEM_MAGMA_HIDEOUT_B1F_1");
            CheckFlag(0x466, "FLAG_ITEM_MAGMA_HIDEOUT_B1F_2");
            CheckFlag(0x467, "FLAG_ITEM_MAGMA_HIDEOUT_B2F_1");
            CheckFlag(0x469, "FLAG_ITEM_MT_PYRE_2F_1");
            CheckFlag(0x46A, "FLAG_ITEM_MT_PYRE_4F_1");
            CheckFlag(0x46B, "FLAG_ITEM_SAFARI_ZONE_SOUTHWEST");
            CheckFlag(0x46C, "FLAG_ITEM_AQUA_HIDEOUT_B1F_3 ");
            CheckFlag(0x46D, "FLAG_ITEM_MOSSDEEP_STEVENS_HOUSE_HM08");
            CheckFlag(0x46E, "FLAG_ITEM_MAGMA_HIDEOUT_B1F_3");
            CheckFlag(0x46F, "FLAG_ITEM_ROUTE104_4");
        }

    }

}
