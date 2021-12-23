using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    static class FlagsGen2C
    {
        static Dictionary<int, string> s_flagDetails = new Dictionary<int, string>();


        public static void ExportFlags(SaveFile savFile)
        {
            InitFlagDetails();

            var flags = savFile.GetEventFlags();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < flags.Length; ++i)
            {
                if (!flags[i] && s_flagDetails.ContainsKey(i))
                {
                    sb.AppendFormat("{0}\n", s_flagDetails[i]);
                }
            }

            System.IO.File.WriteAllText(string.Format("missing_events_{0}.txt", savFile.Version), sb.ToString());
        }

        static void InitFlagDetails()
        {
            // Hidden Items
            s_flagDetails[0x084] = "FLAGS_HIDDEN_ITEM_NATIONAL_PARK_HIDDEN_FULL_HEAL";
            s_flagDetails[0x085] = "FLAGS_HIDDEN_ITEM_OLIVINE_LIGHTHOUSE_5F_HIDDEN_HYPER_POTION";
            s_flagDetails[0x086] = "FLAGS_HIDDEN_ITEM_TEAM_ROCKET_BASE_B1F_HIDDEN_REVIVE";
            s_flagDetails[0x087] = "FLAGS_HIDDEN_ITEM_TEAM_ROCKET_BASE_B2F_HIDDEN_FULL_HEAL";
            s_flagDetails[0x088] = "FLAGS_HIDDEN_ITEM_ILEX_FOREST_HIDDEN_ETHER";
            s_flagDetails[0x089] = "FLAGS_HIDDEN_ITEM_ILEX_FOREST_HIDDEN_SUPER_POTION";
            s_flagDetails[0x08A] = "FLAGS_HIDDEN_ITEM_ILEX_FOREST_HIDDEN_FULL_HEAL";
            s_flagDetails[0x08B] = "FLAGS_HIDDEN_ITEM_GOLDENROD_UNDERGROUND_HIDDEN_PARLYZ_HEAL";
            s_flagDetails[0x08C] = "FLAGS_HIDDEN_ITEM_GOLDENROD_UNDERGROUND_HIDDEN_SUPER_POTION";
            s_flagDetails[0x08D] = "FLAGS_HIDDEN_ITEM_GOLDENROD_UNDERGROUND_HIDDEN_ANTIDOTE";
            s_flagDetails[0x08E] = "FLAGS_HIDDEN_ITEM_GOLDENROD_UNDERGROUND_SWITCH_ROOM_ENTRANCES_HIDDEN_MAX_POTION";
            s_flagDetails[0x08F] = "FLAGS_HIDDEN_ITEM_GOLDENROD_UNDERGROUND_SWITCH_ROOM_ENTRANCES_HIDDEN_REVIVE";
            s_flagDetails[0x090] = "FLAGS_HIDDEN_ITEM_MOUNT_MORTAR_1F_OUTSIDE_HIDDEN_HYPER_POTION";
            s_flagDetails[0x091] = "FLAGS_HIDDEN_ITEM_MOUNT_MORTAR_1F_INSIDE_HIDDEN_MAX_REPEL";
            s_flagDetails[0x092] = "FLAGS_HIDDEN_ITEM_MOUNT_MORTAR_2F_INSIDE_HIDDEN_FULL_RESTORE";
            s_flagDetails[0x093] = "FLAGS_HIDDEN_ITEM_MOUNT_MORTAR_B1F_HIDDEN_MAX_REVIVE";
            s_flagDetails[0x094] = "FLAGS_HIDDEN_ITEM_ICE_PATH_B1F_HIDDEN_MAX_POTION";
            s_flagDetails[0x095] = "FLAGS_HIDDEN_ITEM_ICE_PATH_B2F_MAHOGANY_SIDE_HIDDEN_CARBOS";
            s_flagDetails[0x096] = "FLAGS_HIDDEN_ITEM_ICE_PATH_B2F_BLACKTHORN_SIDE_HIDDEN_ICE_HEAL";
            s_flagDetails[0x097] = "FLAGS_HIDDEN_ITEM_WHIRL_ISLAND_B1F_HIDDEN_RARE_CANDY";
            s_flagDetails[0x098] = "FLAGS_HIDDEN_ITEM_WHIRL_ISLAND_B1F_HIDDEN_ULTRA_BALL";
            s_flagDetails[0x099] = "FLAGS_HIDDEN_ITEM_WHIRL_ISLAND_B1F_HIDDEN_FULL_RESTORE";
            s_flagDetails[0x09A] = "FLAGS_HIDDEN_ITEM_SILVER_CAVE_ROOM_1_HIDDEN_DIRE_HIT";
            s_flagDetails[0x09B] = "FLAGS_HIDDEN_ITEM_SILVER_CAVE_ROOM_1_HIDDEN_ULTRA_BALL";
            s_flagDetails[0x09C] = "FLAGS_HIDDEN_ITEM_SILVER_CAVE_ROOM_2_HIDDEN_MAX_POTION";
            s_flagDetails[0x09D] = "FLAGS_HIDDEN_ITEM_DARK_CAVE_VIOLET_ENTRANCE_HIDDEN_ELIXER";
            s_flagDetails[0x09E] = "FLAGS_HIDDEN_ITEM_VICTORY_ROAD_HIDDEN_MAX_POTION";
            s_flagDetails[0x09F] = "FLAGS_HIDDEN_ITEM_VICTORY_ROAD_HIDDEN_FULL_HEAL";
            s_flagDetails[0x0A0] = "FLAGS_HIDDEN_ITEM_DRAGONS_DEN_B1F_HIDDEN_REVIVE";
            s_flagDetails[0x0A1] = "FLAGS_HIDDEN_ITEM_DRAGONS_DEN_B1F_HIDDEN_MAX_POTION";
            s_flagDetails[0x0A2] = "FLAGS_HIDDEN_ITEM_DRAGONS_DEN_B1F_HIDDEN_MAX_ELIXER";
            s_flagDetails[0x0A3] = "FLAGS_HIDDEN_ITEM_ROUTE_28_HIDDEN_RARE_CANDY";
            s_flagDetails[0x0A4] = "FLAGS_HIDDEN_ITEM_ROUTE_30_HIDDEN_POTION";
            s_flagDetails[0x0A5] = "FLAGS_HIDDEN_ITEM_ROUTE_32_HIDDEN_GREAT_BALL";
            s_flagDetails[0x0A6] = "FLAGS_HIDDEN_ITEM_ROUTE_32_HIDDEN_SUPER_POTION";
            s_flagDetails[0x0A7] = "FLAGS_HIDDEN_ITEM_ROUTE_34_HIDDEN_RARE_CANDY";
            s_flagDetails[0x0A8] = "FLAGS_HIDDEN_ITEM_ROUTE_34_HIDDEN_SUPER_POTION";
            s_flagDetails[0x0A9] = "FLAGS_HIDDEN_ITEM_ROUTE_37_HIDDEN_ETHER";
            s_flagDetails[0x0AA] = "FLAGS_HIDDEN_ITEM_ROUTE_39_HIDDEN_NUGGET";
            s_flagDetails[0x0AB] = "FLAGS_HIDDEN_ITEM_ROUTE_40_HIDDEN_HYPER_POTION";
            s_flagDetails[0x0AC] = "FLAGS_HIDDEN_ITEM_ROUTE_41_HIDDEN_MAX_ETHER";
            s_flagDetails[0x0AD] = "FLAGS_HIDDEN_ITEM_ROUTE_42_HIDDEN_MAX_POTION";
            s_flagDetails[0x0AE] = "FLAGS_HIDDEN_ITEM_ROUTE_44_HIDDEN_ELIXER";
            s_flagDetails[0x0AF] = "FLAGS_HIDDEN_ITEM_ROUTE_45_HIDDEN_PP_UP";
            s_flagDetails[0x0B0] = "FLAGS_HIDDEN_ITEM_VIOLET_CITY_HIDDEN_HYPER_POTION";
            s_flagDetails[0x0B1] = "FLAGS_HIDDEN_ITEM_AZALEA_TOWN_HIDDEN_FULL_HEAL";
            s_flagDetails[0x0B2] = "FLAGS_HIDDEN_ITEM_CIANWOOD_CITY_HIDDEN_REVIVE";
            s_flagDetails[0x0B3] = "FLAGS_HIDDEN_ITEM_CIANWOOD_CITY_HIDDEN_MAX_ETHER";
            s_flagDetails[0x0B4] = "FLAGS_HIDDEN_ITEM_ECRUTEAK_CITY_HIDDEN_HYPER_POTION";
            s_flagDetails[0x0B5] = "FLAGS_HIDDEN_ITEM_LAKE_OF_RAGE_HIDDEN_FULL_RESTORE";
            s_flagDetails[0x0B6] = "FLAGS_HIDDEN_ITEM_LAKE_OF_RAGE_HIDDEN_RARE_CANDY";
            s_flagDetails[0x0B7] = "FLAGS_HIDDEN_ITEM_LAKE_OF_RAGE_HIDDEN_MAX_POTION";
            s_flagDetails[0x0B8] = "FLAGS_HIDDEN_ITEM_SILVER_CAVE_OUTSIDE_HIDDEN_FULL_RESTORE";

            s_flagDetails[0x0E4] = "FLAGS_HIDDEN_ITEM_DIGLETTS_CAVE_HIDDEN_MAX_REVIVE";
            s_flagDetails[0x0E5] = "FLAGS_HIDDEN_ITEM_UNDERGROUND_PATH_HIDDEN_FULL_RESTORE";
            s_flagDetails[0x0E6] = "FLAGS_HIDDEN_ITEM_UNDERGROUND_PATH_HIDDEN_X_SPECIAL";
            s_flagDetails[0x0E7] = "FLAGS_HIDDEN_ITEM_ROCK_TUNNEL_1F_HIDDEN_X_ACCURACY";
            s_flagDetails[0x0E8] = "FLAGS_HIDDEN_ITEM_ROCK_TUNNEL_1F_HIDDEN_X_DEFEND";
            s_flagDetails[0x0E9] = "FLAGS_HIDDEN_ITEM_ROCK_TUNNEL_B1F_HIDDEN_MAX_POTION";
            s_flagDetails[0x0EA] = "FLAGS_HIDDEN_ITEM_OLIVINE_PORT_HIDDEN_PROTEIN";
            s_flagDetails[0x0EB] = "FLAGS_HIDDEN_ITEM_VERMILION_PORT_HIDDEN_IRON";
            s_flagDetails[0x0EC] = "FLAGS_HIDDEN_ITEM_MOUNT_MOON_SQUARE_HIDDEN_MOON_STONE";
            s_flagDetails[0x0ED] = "FLAGS_HIDDEN_ITEM_ROUTE_2_HIDDEN_MAX_ETHER";
            s_flagDetails[0x0EE] = "FLAGS_HIDDEN_ITEM_ROUTE_2_HIDDEN_FULL_HEAL";
            s_flagDetails[0x0EF] = "FLAGS_HIDDEN_ITEM_ROUTE_2_HIDDEN_FULL_RESTORE";
            s_flagDetails[0x0F0] = "FLAGS_HIDDEN_ITEM_ROUTE_2_HIDDEN_REVIVE";
            s_flagDetails[0x0F1] = "FLAGS_HIDDEN_ITEM_ROUTE_4_HIDDEN_ULTRA_BALL";
            s_flagDetails[0x0F2] = "FLAGS_HIDDEN_ITEM_ROUTE_9_HIDDEN_ETHER";
            s_flagDetails[0x0F3] = "FLAGS_HIDDEN_ITEM_ROUTE_12_HIDDEN_ELIXER";
            s_flagDetails[0x0F4] = "FLAGS_HIDDEN_ITEM_ROUTE_13_HIDDEN_CALCIUM";
            s_flagDetails[0x0F5] = "FLAGS_HIDDEN_ITEM_ROUTE_11_HIDDEN_REVIVE";
            s_flagDetails[0x0F6] = "FLAGS_HIDDEN_ITEM_ROUTE_17_HIDDEN_MAX_ETHER";
            s_flagDetails[0x0F7] = "FLAGS_HIDDEN_ITEM_ROUTE_17_HIDDEN_MAX_ELIXER";
            s_flagDetails[0x0F8] = "FLAGS_HIDDEN_ITEM_ROUTE_25_HIDDEN_POTION";
            s_flagDetails[0x0F9] = "FLAGS_HIDDEN_ITEM_FOUND_LEFTOVERS_IN_CELADON_CAFE";
            s_flagDetails[0x0FA] = "FLAGS_HIDDEN_ITEM_FOUND_BERSERK_GENE_IN_CERULEAN_CITY";
            s_flagDetails[0x0FB] = "FLAGS_HIDDEN_ITEM_FOUND_MACHINE_PART_IN_CERULEAN_GYM";
            s_flagDetails[0x0FC] = "FLAGS_HIDDEN_ITEM_VERMILION_CITY_HIDDEN_FULL_HEAL";
            s_flagDetails[0x0FD] = "FLAGS_HIDDEN_ITEM_CELADON_CITY_HIDDEN_PP_UP";
            s_flagDetails[0x0FE] = "FLAGS_HIDDEN_ITEM_CINNABAR_ISLAND_HIDDEN_RARE_CANDY";
            s_flagDetails[0x0FF] = "FLAGS_HIDDEN_ITEM_BURNED_TOWER_1F_HIDDEN_ULTRA_BALL";

            // Normal items
            s_flagDetails[0x643] = "FLAGS_ITEM_VIOLET_CITY_PP_UP";
            s_flagDetails[0x644] = "FLAGS_ITEM_VIOLET_CITY_RARE_CANDY";
            s_flagDetails[0x645] = "FLAGS_ITEM_LAKE_OF_RAGE_ELIXER";
            s_flagDetails[0x646] = "FLAGS_ITEM_LAKE_OF_RAGE_TM_DETECT";
            s_flagDetails[0x647] = "FLAGS_ITEM_SPROUT_TOWER_1F_PARLYZ_HEAL";
            s_flagDetails[0x648] = "FLAGS_ITEM_SPROUT_TOWER_2F_X_ACCURACY";
            s_flagDetails[0x649] = "FLAGS_ITEM_SPROUT_TOWER_3F_POTION";
            s_flagDetails[0x64A] = "FLAGS_ITEM_SPROUT_TOWER_3F_ESCAPE_ROPE";
            s_flagDetails[0x64B] = "FLAGS_ITEM_TIN_TOWER_3F_FULL_HEAL";
            s_flagDetails[0x64C] = "FLAGS_ITEM_TIN_TOWER_4F_ULTRA_BALL";
            s_flagDetails[0x64D] = "FLAGS_ITEM_TIN_TOWER_4F_PP_UP";
            s_flagDetails[0x64E] = "FLAGS_ITEM_TIN_TOWER_4F_ESCAPE_ROPE";
            s_flagDetails[0x64F] = "FLAGS_ITEM_TIN_TOWER_5F_RARE_CANDY";
            s_flagDetails[0x650] = "FLAGS_ITEM_TIN_TOWER_7F_MAX_REVIVE";
            s_flagDetails[0x651] = "FLAGS_ITEM_TIN_TOWER_8F_NUGGET";
            s_flagDetails[0x652] = "FLAGS_ITEM_TIN_TOWER_8F_MAX_ELIXER";
            s_flagDetails[0x653] = "FLAGS_ITEM_TIN_TOWER_8F_FULL_RESTORE";
            s_flagDetails[0x654] = "FLAGS_ITEM_TEAM_ROCKET_BASE_B3F_ULTRA_BALL";
            s_flagDetails[0x655] = "FLAGS_ITEM_GOLDENROD_UNDERGROUND_WAREHOUSE_ULTRA_BALL";
            s_flagDetails[0x656] = "FLAGS_ITEM_BURNED_TOWER_1F_HP_UP";
            s_flagDetails[0x657] = "FLAGS_ITEM_BURNED_TOWER_B1F_TM_ENDURE";
            s_flagDetails[0x658] = "FLAGS_ITEM_NATIONAL_PARK_PARLYZ_HEAL";
            s_flagDetails[0x659] = "FLAGS_ITEM_NATIONAL_PARK_TM_DIG";
            s_flagDetails[0x65A] = "FLAGS_ITEM_UNION_CAVE_1F_GREAT_BALL";
            s_flagDetails[0x65B] = "FLAGS_ITEM_UNION_CAVE_1F_X_ATTACK";
            s_flagDetails[0x65C] = "FLAGS_ITEM_UNION_CAVE_1F_POTION";
            s_flagDetails[0x65D] = "FLAGS_ITEM_UNION_CAVE_1F_AWAKENING";
            s_flagDetails[0x65E] = "FLAGS_ITEM_UNION_CAVE_B1F_TM_SWIFT";
            s_flagDetails[0x65F] = "FLAGS_ITEM_UNION_CAVE_B1F_X_DEFEND";
            s_flagDetails[0x660] = "FLAGS_ITEM_UNION_CAVE_B2F_ELIXER";
            s_flagDetails[0x661] = "FLAGS_ITEM_UNION_CAVE_B2F_HYPER_POTION";
            s_flagDetails[0x662] = "FLAGS_ITEM_SLOWPOKE_WELL_B1F_SUPER_POTION";
            s_flagDetails[0x663] = "FLAGS_ITEM_SLOWPOKE_WELL_B2F_TM_RAIN_DANCE";
            s_flagDetails[0x664] = "FLAGS_ITEM_OLIVINE_LIGHTHOUSE_3F_ETHER";
            s_flagDetails[0x665] = "FLAGS_ITEM_OLIVINE_LIGHTHOUSE_5F_RARE_CANDY";
            s_flagDetails[0x666] = "FLAGS_ITEM_OLIVINE_LIGHTHOUSE_5F_SUPER_REPEL";
            s_flagDetails[0x667] = "FLAGS_ITEM_OLIVINE_LIGHTHOUSE_5F_TM_SWAGGER";
            s_flagDetails[0x668] = "FLAGS_ITEM_OLIVINE_LIGHTHOUSE_6F_SUPER_POTION";
            s_flagDetails[0x669] = "FLAGS_ITEM_TEAM_ROCKET_BASE_B1F_HYPER_POTION";
            s_flagDetails[0x66A] = "FLAGS_ITEM_TEAM_ROCKET_BASE_B1F_NUGGET";
            s_flagDetails[0x66B] = "FLAGS_ITEM_TEAM_ROCKET_BASE_B1F_GUARD_SPEC";
            s_flagDetails[0x66C] = "FLAGS_ITEM_TEAM_ROCKET_BASE_B2F_TM_THIEF";
            s_flagDetails[0x66D] = "FLAGS_ITEM_TEAM_ROCKET_BASE_B3F_PROTEIN";
            s_flagDetails[0x66E] = "FLAGS_ITEM_TEAM_ROCKET_BASE_B3F_X_SPECIAL";
            s_flagDetails[0x66F] = "FLAGS_ITEM_TEAM_ROCKET_BASE_B3F_FULL_HEAL";
            s_flagDetails[0x670] = "FLAGS_ITEM_TEAM_ROCKET_BASE_B3F_ICE_HEAL";
            s_flagDetails[0x671] = "FLAGS_ITEM_ILEX_FOREST_REVIVE";
            s_flagDetails[0x672] = "FLAGS_ITEM_GOLDENROD_UNDERGROUND_COIN_CASE";
            s_flagDetails[0x673] = "FLAGS_ITEM_GOLDENROD_UNDERGROUND_SWITCH_ROOM_ENTRANCES_SMOKE_BALL";
            s_flagDetails[0x674] = "FLAGS_ITEM_GOLDENROD_UNDERGROUND_SWITCH_ROOM_ENTRANCES_FULL_HEAL";
            s_flagDetails[0x675] = "FLAGS_ITEM_GOLDENROD_DEPT_STORE_B1F_ETHER";
            s_flagDetails[0x676] = "FLAGS_ITEM_GOLDENROD_DEPT_STORE_B1F_AMULET_COIN";
            s_flagDetails[0x677] = "FLAGS_ITEM_GOLDENROD_DEPT_STORE_B1F_BURN_HEAL";
            s_flagDetails[0x678] = "FLAGS_ITEM_GOLDENROD_DEPT_STORE_B1F_ULTRA_BALL";
            s_flagDetails[0x679] = "FLAGS_ITEM_GOLDENROD_UNDERGROUND_WAREHOUSE_MAX_ETHER";
            s_flagDetails[0x67A] = "FLAGS_ITEM_GOLDENROD_UNDERGROUND_WAREHOUSE_TM_SLEEP_TALK";
            s_flagDetails[0x67B] = "FLAGS_ITEM_MOUNT_MORTAR_1F_OUTSIDE_ETHER";
            s_flagDetails[0x67C] = "FLAGS_ITEM_MOUNT_MORTAR_1F_OUTSIDE_REVIVE";
            s_flagDetails[0x67D] = "FLAGS_ITEM_MOUNT_MORTAR_1F_INSIDE_ESCAPE_ROPE";
            s_flagDetails[0x67E] = "FLAGS_ITEM_MOUNT_MORTAR_1F_INSIDE_MAX_REVIVE";
            s_flagDetails[0x67F] = "FLAGS_ITEM_MOUNT_MORTAR_1F_INSIDE_HYPER_POTION";
            s_flagDetails[0x680] = "FLAGS_ITEM_MOUNT_MORTAR_2F_INSIDE_MAX_POTION";
            s_flagDetails[0x681] = "FLAGS_ITEM_MOUNT_MORTAR_2F_INSIDE_RARE_CANDY";
            s_flagDetails[0x682] = "FLAGS_ITEM_MOUNT_MORTAR_2F_INSIDE_TM_DEFENSE_CURL";
            s_flagDetails[0x683] = "FLAGS_ITEM_MOUNT_MORTAR_2F_INSIDE_DRAGON_SCALE";
            s_flagDetails[0x684] = "FLAGS_ITEM_MOUNT_MORTAR_2F_INSIDE_ELIXER";
            s_flagDetails[0x685] = "FLAGS_ITEM_MOUNT_MORTAR_2F_INSIDE_ESCAPE_ROPE";
            s_flagDetails[0x686] = "FLAGS_ITEM_MOUNT_MORTAR_B1F_HYPER_POTION";
            s_flagDetails[0x687] = "FLAGS_ITEM_MOUNT_MORTAR_B1F_CARBOS";
            s_flagDetails[0x688] = "FLAGS_ITEM_GOT_HM07_WATERFALL";
            s_flagDetails[0x689] = "FLAGS_ITEM_ICE_PATH_1F_PP_UP";
            s_flagDetails[0x68A] = "FLAGS_ITEM_ICE_PATH_B1F_IRON";
            s_flagDetails[0x68B] = "FLAGS_ITEM_ICE_PATH_B2F_MAHOGANY_SIDE_FULL_HEAL";
            s_flagDetails[0x68C] = "FLAGS_ITEM_ICE_PATH_B2F_MAHOGANY_SIDE_MAX_POTION";
            s_flagDetails[0x68D] = "FLAGS_ITEM_ICE_PATH_B2F_BLACKTHORN_SIDE_TM_REST";
            s_flagDetails[0x68E] = "FLAGS_ITEM_ICE_PATH_B3F_NEVERMELTICE";
            s_flagDetails[0x68F] = "FLAGS_ITEM_WHIRL_ISLAND_NE_ULTRA_BALL";
            s_flagDetails[0x690] = "FLAGS_ITEM_WHIRL_ISLAND_SW_ULTRA_BALL";
            s_flagDetails[0x691] = "FLAGS_ITEM_WHIRL_ISLAND_B1F_FULL_RESTORE";
            s_flagDetails[0x692] = "FLAGS_ITEM_WHIRL_ISLAND_B1F_CARBOS";
            s_flagDetails[0x693] = "FLAGS_ITEM_WHIRL_ISLAND_B1F_CALCIUM";
            s_flagDetails[0x694] = "FLAGS_ITEM_WHIRL_ISLAND_B1F_NUGGET";
            s_flagDetails[0x695] = "FLAGS_ITEM_WHIRL_ISLAND_B1F_ESCAPE_ROPE";
            s_flagDetails[0x696] = "FLAGS_ITEM_WHIRL_ISLAND_B2F_FULL_RESTORE";
            s_flagDetails[0x697] = "FLAGS_ITEM_WHIRL_ISLAND_B2F_MAX_REVIVE";
            s_flagDetails[0x698] = "FLAGS_ITEM_WHIRL_ISLAND_B2F_MAX_ELIXER";
            s_flagDetails[0x699] = "FLAGS_ITEM_SILVER_CAVE_ROOM_1_MAX_ELIXER";
            s_flagDetails[0x69A] = "FLAGS_ITEM_SILVER_CAVE_ROOM_1_PROTEIN";
            s_flagDetails[0x69B] = "FLAGS_ITEM_SILVER_CAVE_ROOM_1_ESCAPE_ROPE";
            s_flagDetails[0x69C] = "FLAGS_ITEM_SILVER_CAVE_ITEM_ROOMS_MAX_REVIVE";
            s_flagDetails[0x69D] = "FLAGS_ITEM_SILVER_CAVE_ITEM_ROOMS_FULL_RESTORE";
            s_flagDetails[0x69E] = "FLAGS_ITEM_DARK_CAVE_VIOLET_ENTRANCE_POTION";
            s_flagDetails[0x69F] = "FLAGS_ITEM_DARK_CAVE_VIOLET_ENTRANCE_FULL_HEAL";
            s_flagDetails[0x6A0] = "FLAGS_ITEM_DARK_CAVE_VIOLET_ENTRANCE_HYPER_POTION";
            s_flagDetails[0x6A1] = "FLAGS_ITEM_DARK_CAVE_BLACKTHORN_ENTRANCE_REVIVE";
            s_flagDetails[0x6A2] = "FLAGS_ITEM_DARK_CAVE_BLACKTHORN_ENTRANCE_TM_SNORE";
            s_flagDetails[0x6A3] = "FLAGS_ITEM_VICTORY_ROAD_TM_EARTHQUAKE";
            s_flagDetails[0x6A4] = "FLAGS_ITEM_VICTORY_ROAD_MAX_REVIVE";
            s_flagDetails[0x6A5] = "FLAGS_ITEM_VICTORY_ROAD_FULL_RESTORE";
            s_flagDetails[0x6A6] = "FLAGS_ITEM_VICTORY_ROAD_FULL_HEAL";
            s_flagDetails[0x6A7] = "FLAGS_ITEM_VICTORY_ROAD_HP_UP";
            s_flagDetails[0x6A8] = "FLAGS_ITEM_DRAGONS_DEN_B1F_DRAGON_FANG";
            s_flagDetails[0x6A9] = "FLAGS_ITEM_TOHJO_FALLS_MOON_STONE";
            s_flagDetails[0x6AA] = "FLAGS_ITEM_ROUTE_26_MAX_ELIXER";
            s_flagDetails[0x6AB] = "FLAGS_ITEM_ROUTE_27_TM_SOLARBEAM";
            s_flagDetails[0x6AC] = "FLAGS_ITEM_ROUTE_27_RARE_CANDY";
            s_flagDetails[0x6AD] = "FLAGS_ITEM_ROUTE_29_POTION";
            s_flagDetails[0x6AE] = "FLAGS_ITEM_ROUTE_31_POTION";
            s_flagDetails[0x6AF] = "FLAGS_ITEM_ROUTE_31_POKE_BALL";
            s_flagDetails[0x6B0] = "FLAGS_ITEM_ROUTE_32_GREAT_BALL";
            s_flagDetails[0x6B1] = "FLAGS_ITEM_ROUTE_32_REPEL";
            s_flagDetails[0x6B2] = "FLAGS_ITEM_ROUTE_35_TM_ROLLOUT";
            s_flagDetails[0x6B3] = "FLAGS_ITEM_ROUTE_42_ULTRA_BALL";
            s_flagDetails[0x6B4] = "FLAGS_ITEM_ROUTE_42_SUPER_POTION";
            s_flagDetails[0x6B5] = "FLAGS_ITEM_ROUTE_43_MAX_ETHER";
            s_flagDetails[0x6B6] = "FLAGS_ITEM_ROUTE_44_MAX_REVIVE";
            s_flagDetails[0x6B7] = "FLAGS_ITEM_ROUTE_44_ULTRA_BALL";
            s_flagDetails[0x6B8] = "FLAGS_ITEM_ROUTE_45_NUGGET";
            s_flagDetails[0x6B9] = "FLAGS_ITEM_ROUTE_45_REVIVE";
            s_flagDetails[0x6BA] = "FLAGS_ITEM_ROUTE_45_ELIXER";
            s_flagDetails[0x6BB] = "FLAGS_ITEM_ROUTE_45_MAX_POTION";
            s_flagDetails[0x6BC] = "FLAGS_ITEM_ROUTE_46_X_SPEED";

            s_flagDetails[0x77D] = "FLAGS_ITEM_PICKED_UP_FOCUS_BAND";
            s_flagDetails[0x77E] = "FLAGS_ITEM_ROCK_TUNNEL_1F_ELIXER";
            s_flagDetails[0x77F] = "FLAGS_ITEM_ROCK_TUNNEL_1F_TM_STEEL_WING";
            s_flagDetails[0x780] = "FLAGS_ITEM_ROCK_TUNNEL_B1F_IRON";
            s_flagDetails[0x781] = "FLAGS_ITEM_ROCK_TUNNEL_B1F_PP_UP";
            s_flagDetails[0x782] = "FLAGS_ITEM_ROCK_TUNNEL_B1F_REVIVE";
            s_flagDetails[0x783] = "FLAGS_ITEM_ROUTE_2_DIRE_HIT";
            s_flagDetails[0x784] = "FLAGS_ITEM_ROUTE_2_MAX_POTION";
            s_flagDetails[0x785] = "FLAGS_ITEM_ROUTE_2_CARBOS";
            s_flagDetails[0x786] = "FLAGS_ITEM_ROUTE_2_ELIXER";
            s_flagDetails[0x787] = "FLAGS_ITEM_ROUTE_4_HP_UP";
            s_flagDetails[0x788] = "FLAGS_ITEM_ROUTE_12_CALCIUM";
            s_flagDetails[0x789] = "FLAGS_ITEM_ROUTE_12_NUGGET";
            s_flagDetails[0x78A] = "FLAGS_ITEM_ROUTE_15_PP_UP";
            s_flagDetails[0x78B] = "FLAGS_ITEM_ROUTE_25_PROTEIN";

            s_flagDetails[0x7A6] = "FLAGS_ITEM_MOUNT_MORTAR_1F_INSIDE_MAX_POTION";
            s_flagDetails[0x7A7] = "FLAGS_ITEM_MOUNT_MORTAR_1F_INSIDE_NUGGET";

            s_flagDetails[0x7B8] = "FLAGS_ITEM_ROUTE_30_ANTIDOTE";
            s_flagDetails[0x7B9] = "FLAGS_ITEM_ILEX_FOREST_X_ATTACK";
            s_flagDetails[0x7BA] = "FLAGS_ITEM_ILEX_FOREST_ANTIDOTE";
            s_flagDetails[0x7BB] = "FLAGS_ITEM_ILEX_FOREST_ETHER";
            s_flagDetails[0x7BC] = "FLAGS_ITEM_ROUTE_34_NUGGET";
            s_flagDetails[0x7BD] = "FLAGS_ITEM_ROUTE_44_MAX_REPEL";
            s_flagDetails[0x7BE] = "FLAGS_ITEM_ICE_PATH_1F_PROTEIN";
            s_flagDetails[0x7BF] = "FLAGS_ITEM_DRAGONS_DEN_B1F_CALCIUM";
            s_flagDetails[0x7C0] = "FLAGS_ITEM_DRAGONS_DEN_B1F_MAX_ELIXER";
            s_flagDetails[0x7C1] = "FLAGS_ITEM_SILVER_CAVE_ROOM_1_ULTRA_BALL";
            s_flagDetails[0x7C2] = "FLAGS_ITEM_SILVER_CAVE_ROOM_2_CALCIUM";
            s_flagDetails[0x7C3] = "FLAGS_ITEM_SILVER_CAVE_ROOM_2_ULTRA_BALL";
            s_flagDetails[0x7C4] = "FLAGS_ITEM_SILVER_CAVE_ROOM_2_PP_UP";
            s_flagDetails[0x7C5] = "FLAGS_ITEM_TIN_TOWER_1F_WISE_TRIO_2";
            s_flagDetails[0x7C6] = "FLAGS_ITEM_TIN_TOWER_6F_MAX_POTION";
            s_flagDetails[0x7C7] = "FLAGS_ITEM_TIN_TOWER_9F_HP_UP";
            s_flagDetails[0x7C8] = "FLAGS_ITEM_MOUNT_MORTAR_1F_INSIDE_IRON";
            s_flagDetails[0x7C9] = "FLAGS_ITEM_MOUNT_MORTAR_1F_INSIDE_ULTRA_BALL";
            s_flagDetails[0x7CA] = "FLAGS_ITEM_MOUNT_MORTAR_B1F_FULL_RESTORE";
            s_flagDetails[0x7CB] = "FLAGS_ITEM_MOUNT_MORTAR_B1F_MAX_ETHER";
            s_flagDetails[0x7CC] = "FLAGS_ITEM_MOUNT_MORTAR_B1F_PP_UP";
            s_flagDetails[0x7CD] = "FLAGS_ITEM_RADIO_TOWER_5F_ULTRA_BALL";
            s_flagDetails[0x7CE] = "FLAGS_ITEM_DARK_CAVE_VIOLET_ENTRANCE_DIRE_HIT";
        }

    }

}
