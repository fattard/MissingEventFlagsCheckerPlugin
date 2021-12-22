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
        static Dictionary<int, string> s_flagDetails = new Dictionary<int, string>();


        public static void ExportFlags(bool[] flags, GameVersion gameVersion)
        {
            InitFlagDetails();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < flags.Length; ++i)
            {
                if (!flags[i] && s_flagDetails.ContainsKey(i))
                {
                    sb.AppendFormat("{0}\n", s_flagDetails[i]);
                }
            }

            System.IO.File.WriteAllText(string.Format("missing_events_{0}.txt", gameVersion), sb.ToString());
        }

        static void InitFlagDetails()
        {
            // Hidden Items
            s_flagDetails[0x258] = "FLAG_HIDDEN_ITEM_0";
            s_flagDetails[0x259] = "FLAG_HIDDEN_ITEM_1";
            s_flagDetails[0x25A] = "FLAG_HIDDEN_ITEM_2";
            s_flagDetails[0x25B] = "FLAG_HIDDEN_ITEM_3";
            s_flagDetails[0x25C] = "FLAG_HIDDEN_ITEM_4";
            s_flagDetails[0x25D] = "FLAG_HIDDEN_ITEM_5";
            s_flagDetails[0x25E] = "FLAG_HIDDEN_ITEM_6";
            s_flagDetails[0x25F] = "FLAG_HIDDEN_ITEM_7";
            s_flagDetails[0x260] = "FLAG_HIDDEN_ITEM_8";
            s_flagDetails[0x261] = "FLAG_HIDDEN_ITEM_9";
            s_flagDetails[0x262] = "FLAG_HIDDEN_ITEM_A";
            s_flagDetails[0x263] = "FLAG_HIDDEN_ITEM_B";
            s_flagDetails[0x264] = "FLAG_HIDDEN_ITEM_C";
            s_flagDetails[0x265] = "FLAG_HIDDEN_ITEM_D";
            s_flagDetails[0x266] = "FLAG_HIDDEN_ITEM_E";
            s_flagDetails[0x267] = "FLAG_HIDDEN_ITEM_F";
            s_flagDetails[0x268] = "FLAG_HIDDEN_ITEM_10";
            s_flagDetails[0x269] = "FLAG_HIDDEN_ITEM_11";
            s_flagDetails[0x26A] = "FLAG_HIDDEN_ITEM_12";
            s_flagDetails[0x26B] = "FLAG_HIDDEN_ITEM_13";
            s_flagDetails[0x26C] = "FLAG_HIDDEN_ITEM_14";
            s_flagDetails[0x26D] = "FLAG_HIDDEN_ITEM_15";
            s_flagDetails[0x26E] = "FLAG_HIDDEN_ITEM_16";
            s_flagDetails[0x26F] = "FLAG_HIDDEN_ITEM_17";
            s_flagDetails[0x270] = "FLAG_HIDDEN_ITEM_18";
            s_flagDetails[0x271] = "FLAG_HIDDEN_ITEM_19";
            s_flagDetails[0x272] = "FLAG_HIDDEN_ITEM_1A";
            s_flagDetails[0x273] = "FLAG_HIDDEN_ITEM_1B";
            s_flagDetails[0x274] = "FLAG_HIDDEN_ITEM_1C";
            s_flagDetails[0x275] = "FLAG_HIDDEN_ITEM_1D";
            s_flagDetails[0x276] = "FLAG_HIDDEN_ITEM_1E";
            s_flagDetails[0x277] = "FLAG_HIDDEN_ITEM_1F";
            s_flagDetails[0x278] = "FLAG_HIDDEN_ITEM_20";
            s_flagDetails[0x279] = "FLAG_HIDDEN_ITEM_21";
            s_flagDetails[0x27A] = "FLAG_HIDDEN_ITEM_22";
            s_flagDetails[0x27B] = "FLAG_HIDDEN_ITEM_23";
            s_flagDetails[0x27C] = "FLAG_HIDDEN_ITEM_24";
            s_flagDetails[0x27D] = "FLAG_HIDDEN_ITEM_25";
            s_flagDetails[0x27E] = "FLAG_HIDDEN_ITEM_26";
            s_flagDetails[0x27F] = "FLAG_HIDDEN_ITEM_27";
            s_flagDetails[0x280] = "FLAG_HIDDEN_ITEM_28";
            s_flagDetails[0x281] = "FLAG_HIDDEN_ITEM_29";
            s_flagDetails[0x282] = "FLAG_HIDDEN_ITEM_2A";
            s_flagDetails[0x283] = "FLAG_HIDDEN_ITEM_2B";
            s_flagDetails[0x284] = "FLAG_HIDDEN_ITEM_2C";
            s_flagDetails[0x285] = "FLAG_HIDDEN_ITEM_2D";
            s_flagDetails[0x286] = "FLAG_HIDDEN_ITEM_2E";
            s_flagDetails[0x287] = "FLAG_HIDDEN_ITEM_2F";
            s_flagDetails[0x288] = "FLAG_HIDDEN_ITEM_30";
            s_flagDetails[0x289] = "FLAG_HIDDEN_ITEM_31";
            s_flagDetails[0x28A] = "FLAG_HIDDEN_ITEM_32";
            s_flagDetails[0x28B] = "FLAG_HIDDEN_ITEM_33";
            s_flagDetails[0x28C] = "FLAG_HIDDEN_ITEM_34";
            s_flagDetails[0x28D] = "FLAG_HIDDEN_ITEM_35";
            s_flagDetails[0x28E] = "FLAG_HIDDEN_ITEM_36";
            s_flagDetails[0x28F] = "FLAG_HIDDEN_ITEM_37";
            s_flagDetails[0x290] = "FLAG_HIDDEN_ITEM_38";
            s_flagDetails[0x291] = "FLAG_HIDDEN_ITEM_39";
            s_flagDetails[0x292] = "FLAG_HIDDEN_ITEM_3A";
            s_flagDetails[0x293] = "FLAG_HIDDEN_ITEM_3B";
            s_flagDetails[0x294] = "FLAG_HIDDEN_ITEM_3C";
            s_flagDetails[0x295] = "FLAG_HIDDEN_ITEM_3D";
            s_flagDetails[0x296] = "FLAG_HIDDEN_ITEM_3E";
            s_flagDetails[0x297] = "FLAG_HIDDEN_ITEM_3F";
            s_flagDetails[0x298] = "FLAG_HIDDEN_ITEM_40";
            s_flagDetails[0x299] = "FLAG_HIDDEN_ITEM_41";
            s_flagDetails[0x29A] = "FLAG_HIDDEN_ITEM_42";
            s_flagDetails[0x29B] = "FLAG_HIDDEN_ITEM_43";
            s_flagDetails[0x29C] = "FLAG_HIDDEN_ITEM_44";
            s_flagDetails[0x29D] = "FLAG_HIDDEN_ITEM_45";
            s_flagDetails[0x29E] = "FLAG_HIDDEN_ITEM_46";
            s_flagDetails[0x29F] = "FLAG_HIDDEN_ITEM_47";
            s_flagDetails[0x2A0] = "FLAG_HIDDEN_ITEM_48";
            s_flagDetails[0x2A1] = "FLAG_HIDDEN_ITEM_49";
            s_flagDetails[0x2A2] = "FLAG_HIDDEN_ITEM_4A";
            s_flagDetails[0x2A3] = "FLAG_HIDDEN_ITEM_4B";
            s_flagDetails[0x2A4] = "FLAG_HIDDEN_ITEM_4C";
            s_flagDetails[0x2A5] = "FLAG_HIDDEN_ITEM_4D";
            s_flagDetails[0x2A6] = "FLAG_HIDDEN_ITEM_4E";
            s_flagDetails[0x2A7] = "FLAG_HIDDEN_ITEM_4F";
            s_flagDetails[0x2A8] = "FLAG_HIDDEN_ITEM_50";
            s_flagDetails[0x2A9] = "FLAG_HIDDEN_ITEM_51";
            s_flagDetails[0x2AA] = "FLAG_HIDDEN_ITEM_52";
            s_flagDetails[0x2AB] = "FLAG_HIDDEN_ITEM_53";
            s_flagDetails[0x2AC] = "FLAG_HIDDEN_ITEM_54";
            s_flagDetails[0x2AD] = "FLAG_HIDDEN_ITEM_55";
            s_flagDetails[0x2AE] = "FLAG_HIDDEN_ITEM_56";
            s_flagDetails[0x2AF] = "FLAG_HIDDEN_ITEM_57";
            s_flagDetails[0x2B0] = "FLAG_HIDDEN_ITEM_58";
            s_flagDetails[0x2B1] = "FLAG_HIDDEN_ITEM_59";
            s_flagDetails[0x2B2] = "FLAG_HIDDEN_ITEM_5A";
            s_flagDetails[0x2B3] = "FLAG_HIDDEN_ITEM_5B";
            s_flagDetails[0x2B4] = "FLAG_HIDDEN_ITEM_5C";
            s_flagDetails[0x2B5] = "FLAG_HIDDEN_ITEM_5D";
            s_flagDetails[0x2B6] = "FLAG_HIDDEN_ITEM_5E";
            s_flagDetails[0x2B7] = "FLAG_HIDDEN_ITEM_5F";
            s_flagDetails[0x2B8] = "FLAG_HIDDEN_ITEM_BLACK_GLASSES";
            s_flagDetails[0x2B9] = "FLAG_HIDDEN_ITEM_61";

            // Normal items
            s_flagDetails[0x3E8] = "FLAG_ITEM_ROUTE102_1";
            s_flagDetails[0x3E9] = "FLAG_ITEM_ROUTE116_1";
            s_flagDetails[0x3EA] = "FLAG_ITEM_ROUTE104_1";
            s_flagDetails[0x3EB] = "FLAG_ITEM_ROUTE105_1";
            s_flagDetails[0x3EC] = "FLAG_ITEM_ROUTE106_1";
            s_flagDetails[0x3ED] = "FLAG_ITEM_ROUTE109_1";
            s_flagDetails[0x3EE] = "FLAG_ITEM_ROUTE110_1";
            s_flagDetails[0x3EF] = "FLAG_ITEM_ROUTE110_2";
            s_flagDetails[0x3F0] = "FLAG_ITEM_ROUTE111_1";
            s_flagDetails[0x3F1] = "FLAG_ITEM_ROUTE111_2";
            s_flagDetails[0x3F2] = "FLAG_ITEM_ROUTE111_3";
            s_flagDetails[0x3F3] = "FLAG_ITEM_ROUTE112_1";
            s_flagDetails[0x3F4] = "FLAG_ITEM_ROUTE113_1";
            s_flagDetails[0x3F5] = "FLAG_ITEM_ROUTE113_2";
            s_flagDetails[0x3F6] = "FLAG_ITEM_ROUTE114_1";
            s_flagDetails[0x3F7] = "FLAG_ITEM_ROUTE114_2";
            s_flagDetails[0x3F8] = "FLAG_ITEM_ROUTE115_1";
            s_flagDetails[0x3F9] = "FLAG_ITEM_ROUTE115_2";
            s_flagDetails[0x3FA] = "FLAG_ITEM_ROUTE115_3";
            s_flagDetails[0x3FB] = "FLAG_ITEM_ROUTE116_2";
            s_flagDetails[0x3FC] = "FLAG_ITEM_ROUTE116_3";
            s_flagDetails[0x3FD] = "FLAG_ITEM_ROUTE116_4";
            s_flagDetails[0x3FE] = "FLAG_ITEM_ROUTE117_1";
            s_flagDetails[0x3FF] = "FLAG_ITEM_ROUTE117_2";
            s_flagDetails[0x400] = "FLAG_ITEM_ROUTE119_1";
            s_flagDetails[0x401] = "FLAG_ITEM_ROUTE119_2";
            s_flagDetails[0x402] = "FLAG_ITEM_ROUTE119_3";
            s_flagDetails[0x403] = "FLAG_ITEM_ROUTE119_4";
            s_flagDetails[0x404] = "FLAG_ITEM_ROUTE119_5";
            s_flagDetails[0x405] = "FLAG_ITEM_ROUTE119_6";
            s_flagDetails[0x406] = "FLAG_ITEM_ROUTE120_1";
            s_flagDetails[0x407] = "FLAG_ITEM_ROUTE120_2";
            s_flagDetails[0x408] = "FLAG_ITEM_ROUTE123_1";
            s_flagDetails[0x409] = "FLAG_ITEM_ROUTE123_2";
            s_flagDetails[0x40A] = "FLAG_ITEM_ROUTE127_1";
            s_flagDetails[0x40B] = "FLAG_ITEM_ROUTE127_2";
            s_flagDetails[0x40C] = "FLAG_ITEM_ROUTE132_1";
            s_flagDetails[0x40D] = "FLAG_ITEM_ROUTE133_1";
            s_flagDetails[0x40E] = "FLAG_ITEM_ROUTE133_2";
            s_flagDetails[0x40F] = "FLAG_ITEM_PETALBURG_1";
            s_flagDetails[0x410] = "FLAG_ITEM_PETALBURG_2";
            s_flagDetails[0x411] = "FLAG_ITEM_RUSTBORO_1";
            s_flagDetails[0x412] = "FLAG_ITEM_LILYCOVE_1";
            s_flagDetails[0x413] = "FLAG_ITEM_MOSSDEEP_1";
            s_flagDetails[0x414] = "FLAG_ITEM_METEOR_FALLS_1F_1R_1";
            s_flagDetails[0x415] = "FLAG_ITEM_METEOR_FALLS_1F_1R_2";
            s_flagDetails[0x416] = "FLAG_ITEM_METEOR_FALLS_1F_1R_3";
            s_flagDetails[0x417] = "FLAG_ITEM_METEOR_FALLS_1F_1R_4";
            s_flagDetails[0x418] = "FLAG_ITEM_RUSTURF_TUNNEL_1";
            s_flagDetails[0x419] = "FLAG_ITEM_RUSTURF_TUNNEL_2";
            s_flagDetails[0x41A] = "FLAG_ITEM_GRANITE_CAVE_1F_1";
            s_flagDetails[0x41B] = "FLAG_ITEM_GRANITE_CAVE_B1F_1";
            s_flagDetails[0x41C] = "FLAG_ITEM_MT_PYRE_5F_1";
            s_flagDetails[0x41D] = "FLAG_ITEM_GRANITE_CAVE_B2F_1";
            s_flagDetails[0x41E] = "FLAG_ITEM_GRANITE_CAVE_B2F_2";
            s_flagDetails[0x41F] = "FLAG_ITEM_PETALBURG_WOODS_1";
            s_flagDetails[0x420] = "FLAG_ITEM_PETALBURG_WOODS_2";
            s_flagDetails[0x421] = "FLAG_ITEM_ROUTE104_2";
            s_flagDetails[0x422] = "FLAG_ITEM_PETALBURG_WOODS_3";
            s_flagDetails[0x423] = "FLAG_ITEM_CAVE_OF_ORIGIN_B3F_1";
            s_flagDetails[0x424] = "FLAG_ITEM_TRICK_HOUSE_PUZZLE_1_1";
            s_flagDetails[0x425] = "FLAG_ITEM_TRICK_HOUSE_PUZZLE_2_1";
            s_flagDetails[0x426] = "FLAG_ITEM_TRICK_HOUSE_PUZZLE_2_2";
            s_flagDetails[0x427] = "FLAG_ITEM_TRICK_HOUSE_PUZZLE_3_1";
            s_flagDetails[0x428] = "FLAG_ITEM_TRICK_HOUSE_PUZZLE_3_2";
            s_flagDetails[0x429] = "FLAG_ITEM_TRICK_HOUSE_PUZZLE_4_1";
            s_flagDetails[0x42A] = "FLAG_ITEM_ROUTE124_1";
            s_flagDetails[0x42B] = "FLAG_ITEM_TRICK_HOUSE_PUZZLE_6_1";
            s_flagDetails[0x42C] = "FLAG_ITEM_TRICK_HOUSE_PUZZLE_7_1";
            s_flagDetails[0x42D] = "FLAG_ITEM_TRICK_HOUSE_PUZZLE_8_1";
            s_flagDetails[0x42E] = "FLAG_ITEM_JAGGED_PASS_1";
            s_flagDetails[0x42F] = "FLAG_ITEM_AQUA_HIDEOUT_B1F_1";
            s_flagDetails[0x430] = "FLAG_ITEM_AQUA_HIDEOUT_B2F_1";
            s_flagDetails[0x431] = "FLAG_ITEM_MT_PYRE_EXTERIOR_1";
            s_flagDetails[0x432] = "FLAG_ITEM_MT_PYRE_EXTERIOR_2";
            s_flagDetails[0x433] = "FLAG_ITEM_NEW_MAUVILLE_INSIDE_1";
            s_flagDetails[0x434] = "FLAG_ITEM_NEW_MAUVILLE_INSIDE_2";
            s_flagDetails[0x435] = "FLAG_ITEM_ABANDONED_SHIP_HIDDEN_FLOOR_ROOMS_1";
            s_flagDetails[0x436] = "FLAG_ITEM_ABANDONED_SHIP_HIDDEN_FLOOR_ROOMS_2";
            s_flagDetails[0x437] = "FLAG_ITEM_SCORCHED_SLAB_1";
            s_flagDetails[0x438] = "FLAG_ITEM_METEOR_FALLS_B1F_2R_1";
            s_flagDetails[0x439] = "FLAG_ITEM_SHOAL_CAVE_LOW_TIDE_ENTRANCE_1";
            s_flagDetails[0x43A] = "FLAG_ITEM_SHOAL_CAVE_LOW_TIDE_INNER_ROOM_1";
            s_flagDetails[0x43B] = "FLAG_ITEM_SHOAL_CAVE_LOW_TIDE_STAIRS_ROOM_1";
            s_flagDetails[0x43C] = "FLAG_ITEM_VICTORY_ROAD_1F_1";
            s_flagDetails[0x43D] = "FLAG_ITEM_VICTORY_ROAD_1F_2";
            s_flagDetails[0x43E] = "FLAG_ITEM_VICTORY_ROAD_B1F_1";
            s_flagDetails[0x43F] = "FLAG_ITEM_VICTORY_ROAD_B1F_2";
            s_flagDetails[0x440] = "FLAG_ITEM_VICTORY_ROAD_B2F_1";
            s_flagDetails[0x441] = "FLAG_ITEM_MT_PYRE_6F_1";
            s_flagDetails[0x442] = "FLAG_ITEM_SEAFLOOR_CAVERN_ROOM_9_1";
            s_flagDetails[0x443] = "FLAG_ITEM_FIERY_PATH_1";
            s_flagDetails[0x444] = "FLAG_ITEM_ROUTE124_2";
            s_flagDetails[0x445] = "FLAG_ITEM_ROUTE124_3";
            s_flagDetails[0x446] = "FLAG_ITEM_SAFARI_ZONE_NORTHWEST_1";
            s_flagDetails[0x447] = "FLAG_ITEM_ABANDONED_SHIP_ROOMS_1F_1";
            s_flagDetails[0x448] = "FLAG_ITEM_ABANDONED_SHIP_ROOMS_B1F_1";
            s_flagDetails[0x449] = "FLAG_ITEM_ABANDONED_SHIP_ROOMS_2_B1F_1";
            s_flagDetails[0x44A] = "FLAG_ITEM_ABANDONED_SHIP_ROOM_B1F_1";
            s_flagDetails[0x44B] = "FLAG_ITEM_ABANDONED_SHIP_ROOMS_2_1F_1";
            s_flagDetails[0x44C] = "FLAG_ITEM_ABANDONED_SHIP_CAPTAINS_OFFICE_1";
            s_flagDetails[0x44D] = "FLAG_ITEM_ABANDONED_SHIP_HIDDEN_FLOOR_ROOMS_3";
            s_flagDetails[0x44E] = "FLAG_ITEM_ABANDONED_SHIP_HIDDEN_FLOOR_ROOMS_4";
            s_flagDetails[0x44F] = "FLAG_ITEM_ROUTE121_1";
            s_flagDetails[0x450] = "FLAG_ITEM_ROUTE123_3";
            s_flagDetails[0x451] = "FLAG_ITEM_ROUTE126_1";
            s_flagDetails[0x452] = "FLAG_ITEM_ROUTE119_7";
            s_flagDetails[0x453] = "FLAG_ITEM_ROUTE120_3";
            s_flagDetails[0x454] = "FLAG_ITEM_ROUTE120_4";
            s_flagDetails[0x455] = "FLAG_ITEM_ROUTE123_4";
            s_flagDetails[0x456] = "FLAG_ITEM_NEW_MAUVILLE_INSIDE_3";
            s_flagDetails[0x457] = "FLAG_ITEM_FIERY_PATH_2";
            s_flagDetails[0x458] = "FLAG_ITEM_SHOAL_CAVE_LOW_TIDE_ICE_ROOM_1";
            s_flagDetails[0x459] = "FLAG_ITEM_SHOAL_CAVE_LOW_TIDE_ICE_ROOM_2";
            s_flagDetails[0x45A] = "FLAG_ITEM_ROUTE103_1";
            s_flagDetails[0x45B] = "FLAG_ITEM_ROUTE104_3";
            s_flagDetails[0x45C] = "FLAG_ITEM_MAUVILLE_1";
            s_flagDetails[0x45D] = "FLAG_ITEM_PETALBURG_WOODS_4";
            s_flagDetails[0x45E] = "FLAG_ITEM_ROUTE115_4";
            s_flagDetails[0x45F] = "FLAG_ITEM_SAFARI_ZONE_NORTHEAST_1";
            s_flagDetails[0x460] = "FLAG_ITEM_MT_PYRE_3F_1";
            s_flagDetails[0x461] = "FLAG_ITEM_ROUTE118_1";
            s_flagDetails[0x462] = "FLAG_ITEM_NEW_MAUVILLE_INSIDE_4";
            s_flagDetails[0x463] = "FLAG_ITEM_NEW_MAUVILLE_INSIDE_5";
            s_flagDetails[0x464] = "FLAG_ITEM_AQUA_HIDEOUT_B1F_2";
            s_flagDetails[0x465] = "FLAG_ITEM_MAGMA_HIDEOUT_B1F_1";
            s_flagDetails[0x466] = "FLAG_ITEM_MAGMA_HIDEOUT_B1F_2";
            s_flagDetails[0x467] = "FLAG_ITEM_MAGMA_HIDEOUT_B2F_1";
            s_flagDetails[0x469] = "FLAG_ITEM_MT_PYRE_2F_1";
            s_flagDetails[0x46A] = "FLAG_ITEM_MT_PYRE_4F_1";
            s_flagDetails[0x46B] = "FLAG_ITEM_SAFARI_ZONE_SOUTHWEST";
            s_flagDetails[0x46C] = "FLAG_ITEM_AQUA_HIDEOUT_B1F_3 ";
            s_flagDetails[0x46D] = "FLAG_ITEM_MOSSDEEP_STEVENS_HOUSE_HM08";
            s_flagDetails[0x46E] = "FLAG_ITEM_MAGMA_HIDEOUT_B1F_3";
            s_flagDetails[0x46F] = "FLAG_ITEM_ROUTE104_4";
        }

    }

}
