using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal class FlagsGen1Y : FlagsOrganizer
    {

        bool[] m_obtainedHiddenCoinsFlags;
        bool[] m_obtainedHiddenItemsFlags;
        bool[] m_missableObjectFlags;
        bool[] m_completedInGameTradeFlags;
        bool m_gotLaprasFlag;


        enum FlagOffsets
        {
            MissableObjectFlags = 0x2852,
            ObtainedHiddenItems = 0x299C,
            ObtainedHiddenCoins = 0x29AA,
            LaprasFlag = 0x29DA,
            CompletedInGameTradeFlags = 0x29E3,
            EventFlags = 0x29F3
        }


        protected override void InitFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;

            // wMissableObjectIndex
            bool[] result = new bool[32 * 8];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_savFile.GetFlag((int)FlagOffsets.MissableObjectFlags + (i >> 3), i & 7);
            }
            m_missableObjectFlags = result;

            // wObtainedHiddenItemsFlags
            result = new bool[112];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_savFile.GetFlag((int)FlagOffsets.ObtainedHiddenItems + (i >> 3), i & 7);
            }
            m_obtainedHiddenItemsFlags = result;

            // wObtainedHiddenCoinsFlags
            result = new bool[16];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_savFile.GetFlag((int)FlagOffsets.ObtainedHiddenCoins + (i >> 3), i & 7);
            }
            m_obtainedHiddenCoinsFlags = result;

            // wCompletedInGameTradeFlags
            result = new bool[16];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_savFile.GetFlag((int)FlagOffsets.CompletedInGameTradeFlags + (i >> 3), i & 7);
            }
            m_completedInGameTradeFlags = result;

            // wd72e
            m_gotLaprasFlag = m_savFile.GetFlag((int)FlagOffsets.LaprasFlag, 0);

            // wEventFlags
            m_eventFlags = (m_savFile as IEventFlagArray).GetEventFlags();
            m_missingEventFlagsList.Clear();
        }


        public override bool SupportsEditingFlag(FlagType flagType)
        {
            switch (flagType)
            {
                case FlagType.FieldItem:
                case FlagType.HiddenItem:
                    //case FlagType.TrainerBattle:
                    return true;

                default:
                    return false;
            }
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
                case FlagType.FieldItem:
                    for (int i = 0; i < m_missableObjectFlags.Length; i++)
                    {
                        m_missableObjectFlags[i] = value;
                        m_savFile.SetFlag((int)FlagOffsets.MissableObjectFlags + (i >> 3), i & 7, value);
                    }
                    break;

                case FlagType.HiddenItem:
                    // Hidden Coins
                    for (int i = 0; i < m_obtainedHiddenCoinsFlags.Length; i++)
                    {
                        m_obtainedHiddenCoinsFlags[i] = value;
                        m_savFile.SetFlag((int)FlagOffsets.ObtainedHiddenCoins + (i >> 3), i & 7, value);
                    }
                    // Hidden items
                    for (int i = 0; i < m_obtainedHiddenItemsFlags.Length; i++)
                    {
                        m_obtainedHiddenItemsFlags[i] = value;
                        m_savFile.SetFlag((int)FlagOffsets.ObtainedHiddenItems + (i >> 3), i & 7, value);
                    }
                    break;

                    /*case FlagType.TrainerBattle:
                        // Trainers
                        for (int i = 0x3E8; i <= 0x5BC; ++i)
                            flagHelper.SetEventFlag(i, value);
                        break;*/
            }
        }

        protected void CheckMissingFlag(bool flagVal, FlagType flagType, string mapLocation, string flagDetail)
        {
            if (isAssembleChecklist)
            {
                m_missingEventFlagsList.Add(new FlagDetail(flagIdx: 0, flagType, mapLocation, flagDetail) { IsSet = flagVal });
            }

            else if (!flagVal)
            {
                m_missingEventFlagsList.Add(new FlagDetail(flagIdx: 0, flagType, mapLocation, flagDetail));
            }
        }


        protected override void CheckAllMissingFlags()
        {
            // Hidden Coins
            CheckMissingFlag(m_obtainedHiddenCoinsFlags[0], FlagType.HiddenItem, "", "COINS_GAME_CORNER_0");
            CheckMissingFlag(m_obtainedHiddenCoinsFlags[1], FlagType.HiddenItem, "", "COINS_GAME_CORNER_1");
            CheckMissingFlag(m_obtainedHiddenCoinsFlags[2], FlagType.HiddenItem, "", "COINS_GAME_CORNER_2");
            CheckMissingFlag(m_obtainedHiddenCoinsFlags[3], FlagType.HiddenItem, "", "COINS_GAME_CORNER_3");
            CheckMissingFlag(m_obtainedHiddenCoinsFlags[4], FlagType.HiddenItem, "", "COINS_GAME_CORNER_4");
            CheckMissingFlag(m_obtainedHiddenCoinsFlags[5], FlagType.HiddenItem, "", "COINS_GAME_CORNER_5");
            CheckMissingFlag(m_obtainedHiddenCoinsFlags[6], FlagType.HiddenItem, "", "COINS_GAME_CORNER_6");
            CheckMissingFlag(m_obtainedHiddenCoinsFlags[7], FlagType.HiddenItem, "", "COINS_GAME_CORNER_7");
            CheckMissingFlag(m_obtainedHiddenCoinsFlags[8], FlagType.HiddenItem, "", "COINS_GAME_CORNER_8");
            CheckMissingFlag(m_obtainedHiddenCoinsFlags[9], FlagType.HiddenItem, "", "COINS_GAME_CORNER_9");
            CheckMissingFlag(m_obtainedHiddenCoinsFlags[10], FlagType.HiddenItem, "", "COINS_GAME_CORNER_10");
            //CheckMissingFlag(m_obtainedHiddenCoinsFlags[11], FlagType.HiddenItem, "", "COINS_GAME_CORNER_11"); // inaccessible

            // Hidden Items
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x00], FlagType.HiddenItem, "", "SILPH_CO_5F_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x01], FlagType.HiddenItem, "", "SILPH_CO_9F_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x02], FlagType.HiddenItem, "", "POKEMON_MANSION_3F_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x03], FlagType.HiddenItem, "", "POKEMON_MANSION_B1F_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x04], FlagType.HiddenItem, "", "SAFARI_ZONE_WEST_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x05], FlagType.HiddenItem, "", "CERULEAN_CAVE_2F_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x06], FlagType.HiddenItem, "", "CERULEAN_CAVE_B1F_0");
            //CheckMissingFlag(m_obtainedHiddenItemsFlags[0x07], FlagType.HiddenItem, "", "UNUSED_MAP_6F_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x08], FlagType.HiddenItem, "", "SEAFOAM_ISLANDS_B2F_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x09], FlagType.HiddenItem, "", "SEAFOAM_ISLANDS_B3F_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x0A], FlagType.HiddenItem, "", "SEAFOAM_ISLANDS_B4F_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x0B], FlagType.HiddenItem, "", "VIRIDIAN_FOREST_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x0C], FlagType.HiddenItem, "", "VIRIDIAN_FOREST_1");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x0D], FlagType.HiddenItem, "", "MT_MOON_B2F_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x0E], FlagType.HiddenItem, "", "MT_MOON_B2F_1");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x0F], FlagType.HiddenItem, "", "SS_ANNE_B1F_ROOMS_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x10], FlagType.HiddenItem, "", "SS_ANNE_KITCHEN_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x11], FlagType.HiddenItem, "", "UNDERGROUND_PATH_NORTH_SOUTH_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x12], FlagType.HiddenItem, "", "UNDERGROUND_PATH_NORTH_SOUTH_1");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x13], FlagType.HiddenItem, "", "UNDERGROUND_PATH_WEST_EAST_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x14], FlagType.HiddenItem, "", "UNDERGROUND_PATH_WEST_EAST_1");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x15], FlagType.HiddenItem, "", "ROCKET_HIDEOUT_B1F_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x16], FlagType.HiddenItem, "", "ROCKET_HIDEOUT_B3F_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x17], FlagType.HiddenItem, "", "ROCKET_HIDEOUT_B4F_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x18], FlagType.HiddenItem, "", "ROUTE_10_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x19], FlagType.HiddenItem, "", "ROUTE_10_1");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x1A], FlagType.HiddenItem, "", "POWER_PLANT_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x1B], FlagType.HiddenItem, "", "POWER_PLANT_1");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x1C], FlagType.HiddenItem, "", "ROUTE_11_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x1D], FlagType.HiddenItem, "", "ROUTE_12_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x1E], FlagType.HiddenItem, "", "ROUTE_13_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x1F], FlagType.HiddenItem, "", "ROUTE_13_1");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x20], FlagType.HiddenItem, "", "ROUTE_17_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x21], FlagType.HiddenItem, "", "ROUTE_17_1");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x22], FlagType.HiddenItem, "", "ROUTE_17_2");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x23], FlagType.HiddenItem, "", "ROUTE_17_3");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x24], FlagType.HiddenItem, "", "ROUTE_17_4");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x25], FlagType.HiddenItem, "", "ROUTE_23_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x26], FlagType.HiddenItem, "", "ROUTE_23_1");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x27], FlagType.HiddenItem, "", "ROUTE_23_2");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x28], FlagType.HiddenItem, "", "VICTORY_ROAD_2F_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x29], FlagType.HiddenItem, "", "VICTORY_ROAD_2F_1");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x2A], FlagType.HiddenItem, "", "ROUTE_25_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x2B], FlagType.HiddenItem, "", "ROUTE_25_1");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x2C], FlagType.HiddenItem, "", "ROUTE_4_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x2D], FlagType.HiddenItem, "", "ROUTE_9_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x2E], FlagType.HiddenItem, "", "COPYCATS_HOUSE_2F_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x2F], FlagType.HiddenItem, "", "VIRIDIAN_CITY_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x30], FlagType.HiddenItem, "", "CERULEAN_CITY_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x31], FlagType.HiddenItem, "", "CERULEAN_CAVE_1F_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x32], FlagType.HiddenItem, "", "POKEMON_TOWER_5F_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x33], FlagType.HiddenItem, "", "VERMILION_CITY_0");
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x34], FlagType.HiddenItem, "", "CELADON_CITY_0");
            //CheckMissingFlag(m_obtainedHiddenItemsFlags[0x35], FlagType.HiddenItem, "", "SAFARI_ZONE_GATE_0"); // inaccessible
            CheckMissingFlag(m_obtainedHiddenItemsFlags[0x36], FlagType.HiddenItem, "", "POKEMON_MANSION_1F_0");

            // Field Items
            CheckMissingFlag(m_missableObjectFlags[0x1A], FlagType.FieldItem, "", "ROUTE_2_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0x1B], FlagType.FieldItem, "", "ROUTE_2_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0x1C], FlagType.FieldItem, "", "ROUTE_4_ITEM");
            CheckMissingFlag(m_missableObjectFlags[0x1D], FlagType.FieldItem, "", "ROUTE_9_ITEM");
            CheckMissingFlag(m_missableObjectFlags[0x1F], FlagType.FieldItem, "", "ROUTE_12_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0x20], FlagType.FieldItem, "", "ROUTE_12_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0x21], FlagType.FieldItem, "", "ROUTE_15_ITEM");
            CheckMissingFlag(m_missableObjectFlags[0x26], FlagType.FieldItem, "", "ROUTE_24_ITEM");
            CheckMissingFlag(m_missableObjectFlags[0x27], FlagType.FieldItem, "", "ROUTE_25_ITEM");
            CheckMissingFlag(m_missableObjectFlags[0x32], FlagType.FieldItem, "", "VIRIDIAN_GYM_ITEM");
            CheckMissingFlag(m_missableObjectFlags[0x35], FlagType.FieldItem, "", "CERULEAN_CAVE_1F_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0x36], FlagType.FieldItem, "", "CERULEAN_CAVE_1F_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0x37], FlagType.FieldItem, "", "CERULEAN_CAVE_1F_ITEM_3");
            CheckMissingFlag(m_missableObjectFlags[0x38], FlagType.FieldItem, "", "CERULEAN_CAVE_1F_ITEM_4");
            CheckMissingFlag(m_missableObjectFlags[0x3A], FlagType.FieldItem, "", "POKEMON_TOWER_3F_ITEM");
            CheckMissingFlag(m_missableObjectFlags[0x3B], FlagType.FieldItem, "", "POKEMON_TOWER_4F_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0x3C], FlagType.FieldItem, "", "POKEMON_TOWER_4F_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0x3D], FlagType.FieldItem, "", "POKEMON_TOWER_4F_ITEM_3");
            CheckMissingFlag(m_missableObjectFlags[0x3E], FlagType.FieldItem, "", "POKEMON_TOWER_5F_ITEM");
            CheckMissingFlag(m_missableObjectFlags[0x3F], FlagType.FieldItem, "", "POKEMON_TOWER_6F_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0x40], FlagType.FieldItem, "", "POKEMON_TOWER_6F_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0x47], FlagType.FieldItem, "", "WARDENS_HOUSE_ITEM");
            CheckMissingFlag(m_missableObjectFlags[0x48], FlagType.FieldItem, "", "POKEMON_MANSION_1F_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0x49], FlagType.FieldItem, "", "POKEMON_MANSION_1F_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0x56], FlagType.FieldItem, "", "POWER_PLANT_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0x57], FlagType.FieldItem, "", "POWER_PLANT_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0x58], FlagType.FieldItem, "", "POWER_PLANT_ITEM_3");
            CheckMissingFlag(m_missableObjectFlags[0x59], FlagType.FieldItem, "", "POWER_PLANT_ITEM_4");
            CheckMissingFlag(m_missableObjectFlags[0x5A], FlagType.FieldItem, "", "POWER_PLANT_ITEM_5");
            CheckMissingFlag(m_missableObjectFlags[0x5C], FlagType.FieldItem, "", "VICTORY_ROAD_2F_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0x5D], FlagType.FieldItem, "", "VICTORY_ROAD_2F_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0x5E], FlagType.FieldItem, "", "VICTORY_ROAD_2F_ITEM_3");
            CheckMissingFlag(m_missableObjectFlags[0x5F], FlagType.FieldItem, "", "VICTORY_ROAD_2F_ITEM_4");
            CheckMissingFlag(m_missableObjectFlags[0x64], FlagType.FieldItem, "", "VIRIDIAN_FOREST_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0x65], FlagType.FieldItem, "", "VIRIDIAN_FOREST_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0x66], FlagType.FieldItem, "", "VIRIDIAN_FOREST_ITEM_3");
            CheckMissingFlag(m_missableObjectFlags[0x67], FlagType.FieldItem, "", "MT_MOON_1F_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0x68], FlagType.FieldItem, "", "MT_MOON_1F_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0x69], FlagType.FieldItem, "", "MT_MOON_1F_ITEM_3");
            CheckMissingFlag(m_missableObjectFlags[0x6A], FlagType.FieldItem, "", "MT_MOON_1F_ITEM_4");
            CheckMissingFlag(m_missableObjectFlags[0x6B], FlagType.FieldItem, "", "MT_MOON_1F_ITEM_5");
            CheckMissingFlag(m_missableObjectFlags[0x6C], FlagType.FieldItem, "", "MT_MOON_1F_ITEM_6");
            CheckMissingFlag(m_missableObjectFlags[0x71], FlagType.FieldItem, "", "MT_MOON_B2F_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0x72], FlagType.FieldItem, "", "MT_MOON_B2F_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0x74], FlagType.FieldItem, "", "SS_ANNE_1F_ROOMS_ITEM");
            CheckMissingFlag(m_missableObjectFlags[0x75], FlagType.FieldItem, "", "SS_ANNE_2F_ROOMS_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0x76], FlagType.FieldItem, "", "SS_ANNE_2F_ROOMS_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0x77], FlagType.FieldItem, "", "SS_ANNE_B1F_ROOMS_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0x78], FlagType.FieldItem, "", "SS_ANNE_B1F_ROOMS_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0x79], FlagType.FieldItem, "", "SS_ANNE_B1F_ROOMS_ITEM_3");
            CheckMissingFlag(m_missableObjectFlags[0x7A], FlagType.FieldItem, "", "VICTORY_ROAD_3F_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0x7B], FlagType.FieldItem, "", "VICTORY_ROAD_3F_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0x7D], FlagType.FieldItem, "", "ROCKET_HIDEOUT_B1F_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0x7E], FlagType.FieldItem, "", "ROCKET_HIDEOUT_B1F_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0x7F], FlagType.FieldItem, "", "ROCKET_HIDEOUT_B2F_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0x80], FlagType.FieldItem, "", "ROCKET_HIDEOUT_B2F_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0x81], FlagType.FieldItem, "", "ROCKET_HIDEOUT_B2F_ITEM_3");
            CheckMissingFlag(m_missableObjectFlags[0x82], FlagType.FieldItem, "", "ROCKET_HIDEOUT_B2F_ITEM_4");
            CheckMissingFlag(m_missableObjectFlags[0x83], FlagType.FieldItem, "", "ROCKET_HIDEOUT_B3F_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0x84], FlagType.FieldItem, "", "ROCKET_HIDEOUT_B3F_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0x88], FlagType.FieldItem, "", "ROCKET_HIDEOUT_B4F_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0x89], FlagType.FieldItem, "", "ROCKET_HIDEOUT_B4F_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0x8A], FlagType.FieldItem, "", "ROCKET_HIDEOUT_B4F_ITEM_3");
            CheckMissingFlag(m_missableObjectFlags[0x8B], FlagType.FieldItem, "", "ROCKET_HIDEOUT_B4F_ITEM_4");
            CheckMissingFlag(m_missableObjectFlags[0x8C], FlagType.FieldItem, "", "ROCKET_HIDEOUT_B4F_ITEM_5");
            CheckMissingFlag(m_missableObjectFlags[0x94], FlagType.FieldItem, "", "SILPH_CO_3F_ITEM");
            CheckMissingFlag(m_missableObjectFlags[0x98], FlagType.FieldItem, "", "SILPH_CO_4F_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0x99], FlagType.FieldItem, "", "SILPH_CO_4F_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0x9A], FlagType.FieldItem, "", "SILPH_CO_4F_ITEM_3");
            CheckMissingFlag(m_missableObjectFlags[0x9F], FlagType.FieldItem, "", "SILPH_CO_5F_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0xA0], FlagType.FieldItem, "", "SILPH_CO_5F_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0xA1], FlagType.FieldItem, "", "SILPH_CO_5F_ITEM_3");
            CheckMissingFlag(m_missableObjectFlags[0xA5], FlagType.FieldItem, "", "SILPH_CO_6F_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0xA6], FlagType.FieldItem, "", "SILPH_CO_6F_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0xAC], FlagType.FieldItem, "", "SILPH_CO_7F_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0xAD], FlagType.FieldItem, "", "SILPH_CO_7F_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0xB8], FlagType.FieldItem, "", "SILPH_CO_10F_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0xB9], FlagType.FieldItem, "", "SILPH_CO_10F_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0xBA], FlagType.FieldItem, "", "SILPH_CO_10F_ITEM_3");
            CheckMissingFlag(m_missableObjectFlags[0xC0], FlagType.FieldItem, "", "POKEMON_MANSION_2F_ITEM");
            CheckMissingFlag(m_missableObjectFlags[0xC1], FlagType.FieldItem, "", "POKEMON_MANSION_3F_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0xC2], FlagType.FieldItem, "", "POKEMON_MANSION_3F_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0xC3], FlagType.FieldItem, "", "POKEMON_MANSION_B1F_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0xC4], FlagType.FieldItem, "", "POKEMON_MANSION_B1F_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0xC5], FlagType.FieldItem, "", "POKEMON_MANSION_B1F_ITEM_3");
            CheckMissingFlag(m_missableObjectFlags[0xC6], FlagType.FieldItem, "", "POKEMON_MANSION_B1F_ITEM_4");
            CheckMissingFlag(m_missableObjectFlags[0xC7], FlagType.FieldItem, "", "POKEMON_MANSION_B1F_ITEM_5");
            CheckMissingFlag(m_missableObjectFlags[0xC8], FlagType.FieldItem, "", "SAFARI_ZONE_EAST_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0xC9], FlagType.FieldItem, "", "SAFARI_ZONE_EAST_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0xCA], FlagType.FieldItem, "", "SAFARI_ZONE_EAST_ITEM_3");
            CheckMissingFlag(m_missableObjectFlags[0xCB], FlagType.FieldItem, "", "SAFARI_ZONE_EAST_ITEM_4");
            CheckMissingFlag(m_missableObjectFlags[0xCC], FlagType.FieldItem, "", "SAFARI_ZONE_NORTH_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0xCD], FlagType.FieldItem, "", "SAFARI_ZONE_NORTH_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0xCE], FlagType.FieldItem, "", "SAFARI_ZONE_WEST_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0xCF], FlagType.FieldItem, "", "SAFARI_ZONE_WEST_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0xD0], FlagType.FieldItem, "", "SAFARI_ZONE_WEST_ITEM_3");
            CheckMissingFlag(m_missableObjectFlags[0xD1], FlagType.FieldItem, "", "SAFARI_ZONE_WEST_ITEM_4");
            CheckMissingFlag(m_missableObjectFlags[0xD2], FlagType.FieldItem, "", "SAFARI_ZONE_CENTER_ITEM");
            CheckMissingFlag(m_missableObjectFlags[0xD3], FlagType.FieldItem, "", "CERULEAN_CAVE_2F_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0xD4], FlagType.FieldItem, "", "CERULEAN_CAVE_2F_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0xD5], FlagType.FieldItem, "", "CERULEAN_CAVE_2F_ITEM_3");
            CheckMissingFlag(m_missableObjectFlags[0xD6], FlagType.FieldItem, "", "CERULEAN_CAVE_2F_ITEM_4");
            CheckMissingFlag(m_missableObjectFlags[0xD8], FlagType.FieldItem, "", "CERULEAN_CAVE_B1F_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0xD9], FlagType.FieldItem, "", "CERULEAN_CAVE_B1F_ITEM_2");
            CheckMissingFlag(m_missableObjectFlags[0xDA], FlagType.FieldItem, "", "CERULEAN_CAVE_B1F_ITEM_3");
            CheckMissingFlag(m_missableObjectFlags[0xDB], FlagType.FieldItem, "", "CERULEAN_CAVE_B1F_ITEM_4");
            CheckMissingFlag(m_missableObjectFlags[0xDC], FlagType.FieldItem, "", "VICTORY_ROAD_1F_ITEM_1");
            CheckMissingFlag(m_missableObjectFlags[0xDD], FlagType.FieldItem, "", "VICTORY_ROAD_1F_ITEM_2");

            // In Game Trades
            CheckMissingFlag(m_completedInGameTradeFlags[0x00], FlagType.InGameTrade, "", "TRADE_FOR_GURIO");
            CheckMissingFlag(m_completedInGameTradeFlags[0x01], FlagType.InGameTrade, "", "TRADE_FOR_MILES");
            //CheckMissingFlag(m_completedInGameTradeFlags[0x02], FlagType.InGameTrade, "", "TRADE_FOR_STINGER"); // unused
            CheckMissingFlag(m_completedInGameTradeFlags[0x03], FlagType.InGameTrade, "", "TRADE_FOR_STICKY");
            //CheckMissingFlag(m_completedInGameTradeFlags[0x04], FlagType.InGameTrade, "", "TRADE_FOR_BART"); // unused
            CheckMissingFlag(m_completedInGameTradeFlags[0x05], FlagType.InGameTrade, "", "TRADE_FOR_SPIKE");
            CheckMissingFlag(m_completedInGameTradeFlags[0x06], FlagType.InGameTrade, "", "TRADE_FOR_MARTY");
            CheckMissingFlag(m_completedInGameTradeFlags[0x07], FlagType.InGameTrade, "", "TRADE_FOR_BUFFY");
            CheckMissingFlag(m_completedInGameTradeFlags[0x08], FlagType.InGameTrade, "", "TRADE_FOR_CEZANNE");
            CheckMissingFlag(m_completedInGameTradeFlags[0x09], FlagType.InGameTrade, "", "TRADE_FOR_RICKY");

            // Other events
            CheckMissingFlag(m_missableObjectFlags[0x45], FlagType.GeneralEvent, "", "CELADON_MANSION_EEVEE_GIFT");
            CheckMissingFlag(m_gotLaprasFlag, FlagType.GeneralEvent, "", "SILPH_CO_7F_LAPRAS_GIFT");

            CheckMissingFlag(m_eventFlags[0x018], FlagType.GeneralEvent, "", "GOT_TOWN_MAP");
            CheckMissingFlag(m_eventFlags[0x022], FlagType.GeneralEvent, "", "GOT_STARTER");
            CheckMissingFlag(m_eventFlags[0x023], FlagType.GeneralEvent, "", "BATTLED_RIVAL_IN_OAKS_LAB");
            CheckMissingFlag(m_eventFlags[0x025], FlagType.GeneralEvent, "", "GOT_POKEDEX");

            CheckMissingFlag(m_eventFlags[0x029], FlagType.GeneralEvent, "", "GOT_TM42");
            CheckMissingFlag(m_eventFlags[0x038], FlagType.GeneralEvent, "", "GOT_POKEBALLS_FROM_OAK"); // EVENT_OAK_GOT_PARCEL
            CheckMissingFlag(m_eventFlags[0x039], FlagType.GeneralEvent, "", "GOT_OAKS_PARCEL");
            CheckMissingFlag(m_eventFlags[0x050], FlagType.GeneralEvent, "", "GOT_TM27");
            CheckMissingFlag(m_eventFlags[0x051], FlagType.TrainerBattle, "", "BEAT_VIRIDIAN_GYM_GIOVANNI");
            CheckMissingFlag(m_eventFlags[0x052], FlagType.TrainerBattle, "", "BEAT_VIRIDIAN_GYM_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x053], FlagType.TrainerBattle, "", "BEAT_VIRIDIAN_GYM_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x054], FlagType.TrainerBattle, "", "BEAT_VIRIDIAN_GYM_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x055], FlagType.TrainerBattle, "", "BEAT_VIRIDIAN_GYM_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x056], FlagType.TrainerBattle, "", "BEAT_VIRIDIAN_GYM_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x057], FlagType.TrainerBattle, "", "BEAT_VIRIDIAN_GYM_TRAINER_5");
            CheckMissingFlag(m_eventFlags[0x058], FlagType.TrainerBattle, "", "BEAT_VIRIDIAN_GYM_TRAINER_6");
            CheckMissingFlag(m_eventFlags[0x059], FlagType.TrainerBattle, "", "BEAT_VIRIDIAN_GYM_TRAINER_7");

            CheckMissingFlag(m_eventFlags[0x069], FlagType.GeneralEvent, "", "GOT_OLD_AMBER");
            CheckMissingFlag(m_eventFlags[0x072], FlagType.TrainerBattle, "", "BEAT_PEWTER_GYM_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x076], FlagType.GeneralEvent, "", "GOT_TM34");
            CheckMissingFlag(m_eventFlags[0x077], FlagType.TrainerBattle, "", "BEAT_BROCK");

            CheckMissingFlag(m_eventFlags[0x098], FlagType.TrainerBattle, "", "BEAT_CERULEAN_RIVAL");
            CheckMissingFlag(m_eventFlags[0x0A7], FlagType.TrainerBattle, "", "BEAT_CERULEAN_ROCKET_THIEF");
            CheckMissingFlag(m_eventFlags[0x0A8], FlagType.GeneralEvent, "", "GOT_BULBASAUR_IN_CERULEAN");
            CheckMissingFlag(m_eventFlags[0x0BA], FlagType.TrainerBattle, "", "BEAT_CERULEAN_GYM_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x0BB], FlagType.TrainerBattle, "", "BEAT_CERULEAN_GYM_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x0BE], FlagType.GeneralEvent, "", "GOT_TM11");
            CheckMissingFlag(m_eventFlags[0x0BF], FlagType.TrainerBattle, "", "BEAT_MISTY");
            CheckMissingFlag(m_eventFlags[0x0C0], FlagType.GeneralEvent, "", "GOT_BICYCLE");

            CheckMissingFlag(m_eventFlags[0x0EF], FlagType.TrainerBattle, "", "BEAT_POKEMON_TOWER_RIVAL");
            CheckMissingFlag(m_eventFlags[0x0F1], FlagType.TrainerBattle, "", "BEAT_POKEMONTOWER_3_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x0F2], FlagType.TrainerBattle, "", "BEAT_POKEMONTOWER_3_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x0F3], FlagType.TrainerBattle, "", "BEAT_POKEMONTOWER_3_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x0F9], FlagType.TrainerBattle, "", "BEAT_POKEMONTOWER_4_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x0FA], FlagType.TrainerBattle, "", "BEAT_POKEMONTOWER_4_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x0FB], FlagType.TrainerBattle, "", "BEAT_POKEMONTOWER_4_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x102], FlagType.TrainerBattle, "", "BEAT_POKEMONTOWER_5_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x103], FlagType.TrainerBattle, "", "BEAT_POKEMONTOWER_5_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x104], FlagType.TrainerBattle, "", "BEAT_POKEMONTOWER_5_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x105], FlagType.TrainerBattle, "", "BEAT_POKEMONTOWER_5_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x109], FlagType.TrainerBattle, "", "BEAT_POKEMONTOWER_6_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x10A], FlagType.TrainerBattle, "", "BEAT_POKEMONTOWER_6_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x10B], FlagType.TrainerBattle, "", "BEAT_POKEMONTOWER_6_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x10F], FlagType.GeneralEvent, "", "BEAT_GHOST_MAROWAK");
            CheckMissingFlag(m_eventFlags[0x111], FlagType.TrainerBattle, "", "BEAT_POKEMONTOWER_7_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x112], FlagType.TrainerBattle, "", "BEAT_POKEMONTOWER_7_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x113], FlagType.TrainerBattle, "", "BEAT_POKEMONTOWER_7_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x128], FlagType.GeneralEvent, "", "GOT_POKE_FLUTE");
            CheckMissingFlag(m_eventFlags[0x147], FlagType.GeneralEvent, "", "GOT_SQUIRTLE_FROM_OFFICER_JENNY");

            CheckMissingFlag(m_eventFlags[0x151], FlagType.GeneralEvent, "", "GOT_BIKE_VOUCHER");
            CheckMissingFlag(m_eventFlags[0x162], FlagType.TrainerBattle, "", "BEAT_VERMILION_GYM_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x163], FlagType.TrainerBattle, "", "BEAT_VERMILION_GYM_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x164], FlagType.TrainerBattle, "", "BEAT_VERMILION_GYM_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x166], FlagType.GeneralEvent, "", "GOT_TM24");
            CheckMissingFlag(m_eventFlags[0x167], FlagType.TrainerBattle, "", "BEAT_LT_SURGE");

            CheckMissingFlag(m_eventFlags[0x180], FlagType.GeneralEvent, "", "GOT_TM41");
            CheckMissingFlag(m_eventFlags[0x18C], FlagType.GeneralEvent, "", "GOT_TM13");
            CheckMissingFlag(m_eventFlags[0x18D], FlagType.GeneralEvent, "", "GOT_TM48");
            CheckMissingFlag(m_eventFlags[0x18E], FlagType.GeneralEvent, "", "GOT_TM49");
            CheckMissingFlag(m_eventFlags[0x18F], FlagType.GeneralEvent, "", "GOT_TM18");
            CheckMissingFlag(m_eventFlags[0x1A8], FlagType.GeneralEvent, "", "GOT_TM21");
            CheckMissingFlag(m_eventFlags[0x1A9], FlagType.TrainerBattle, "", "BEAT_ERIKA");
            CheckMissingFlag(m_eventFlags[0x1AA], FlagType.TrainerBattle, "", "BEAT_CELADON_GYM_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x1AB], FlagType.TrainerBattle, "", "BEAT_CELADON_GYM_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x1AC], FlagType.TrainerBattle, "", "BEAT_CELADON_GYM_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x1AD], FlagType.TrainerBattle, "", "BEAT_CELADON_GYM_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x1AE], FlagType.TrainerBattle, "", "BEAT_CELADON_GYM_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x1AF], FlagType.TrainerBattle, "", "BEAT_CELADON_GYM_TRAINER_5");
            CheckMissingFlag(m_eventFlags[0x1B0], FlagType.TrainerBattle, "", "BEAT_CELADON_GYM_TRAINER_6");
            CheckMissingFlag(m_eventFlags[0x1BA], FlagType.GeneralEvent, "", "GOT_10_COINS");
            CheckMissingFlag(m_eventFlags[0x1BB], FlagType.GeneralEvent, "", "GOT_20_COINS");
            CheckMissingFlag(m_eventFlags[0x1BC], FlagType.GeneralEvent, "", "GOT_20_COINS_2");
            CheckMissingFlag(m_eventFlags[0x1E0], FlagType.GeneralEvent, "", "GOT_COIN_CASE");

            CheckMissingFlag(m_eventFlags[0x238], FlagType.GeneralEvent, "", "GOT_HM04");
            CheckMissingFlag(m_eventFlags[0x258], FlagType.GeneralEvent, "", "GOT_TM06");
            CheckMissingFlag(m_eventFlags[0x259], FlagType.TrainerBattle, "", "BEAT_KOGA");
            CheckMissingFlag(m_eventFlags[0x25A], FlagType.TrainerBattle, "", "BEAT_FUCHSIA_GYM_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x25B], FlagType.TrainerBattle, "", "BEAT_FUCHSIA_GYM_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x25C], FlagType.TrainerBattle, "", "BEAT_FUCHSIA_GYM_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x25D], FlagType.TrainerBattle, "", "BEAT_FUCHSIA_GYM_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x25E], FlagType.TrainerBattle, "", "BEAT_FUCHSIA_GYM_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x25F], FlagType.TrainerBattle, "", "BEAT_FUCHSIA_GYM_TRAINER_5");

            CheckMissingFlag(m_eventFlags[0x289], FlagType.TrainerBattle, "", "BEAT_MANSION_1_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x298], FlagType.GeneralEvent, "", "GOT_TM38");
            CheckMissingFlag(m_eventFlags[0x299], FlagType.TrainerBattle, "", "BEAT_BLAINE");
            CheckMissingFlag(m_eventFlags[0x29A], FlagType.TrainerBattle, "", "BEAT_CINNABAR_GYM_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x29B], FlagType.TrainerBattle, "", "BEAT_CINNABAR_GYM_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x29C], FlagType.TrainerBattle, "", "BEAT_CINNABAR_GYM_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x29D], FlagType.TrainerBattle, "", "BEAT_CINNABAR_GYM_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x29E], FlagType.TrainerBattle, "", "BEAT_CINNABAR_GYM_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x29F], FlagType.TrainerBattle, "", "BEAT_CINNABAR_GYM_TRAINER_5");
            CheckMissingFlag(m_eventFlags[0x2A0], FlagType.TrainerBattle, "", "BEAT_CINNABAR_GYM_TRAINER_6");
            CheckMissingFlag(m_eventFlags[0x2D7], FlagType.GeneralEvent, "", "GOT_TM35");

            CheckMissingFlag(m_eventFlags[0x340], FlagType.GeneralEvent, "", "GOT_TM31");
            CheckMissingFlag(m_eventFlags[0x351], FlagType.TrainerBattle, "", "BEAT_KARATE_MASTER");
            CheckMissingFlag(m_eventFlags[0x352], FlagType.TrainerBattle, "", "BEAT_FIGHTING_DOJO_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x353], FlagType.TrainerBattle, "", "BEAT_FIGHTING_DOJO_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x354], FlagType.TrainerBattle, "", "BEAT_FIGHTING_DOJO_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x355], FlagType.TrainerBattle, "", "BEAT_FIGHTING_DOJO_TRAINER_3");

            if (!m_eventFlags[0x356] && !m_eventFlags[0x357])
            {
                //CheckMissingFlag(m_eventFlags[0x356], FlagType.GeneralEvent, "", "GOT_HITMONLEE");
                //CheckMissingFlag(m_eventFlags[0x357], FlagType.GeneralEvent, "", "GOT_HITMONCHAN");

                CheckMissingFlag(m_eventFlags[0x356], FlagType.GeneralEvent, "", "GOT_HITMONLEE/HITMONCHAN");
            }


            CheckMissingFlag(m_eventFlags[0x360], FlagType.GeneralEvent, "", "GOT_TM46");
            CheckMissingFlag(m_eventFlags[0x361], FlagType.TrainerBattle, "", "BEAT_SABRINA");
            CheckMissingFlag(m_eventFlags[0x362], FlagType.TrainerBattle, "", "BEAT_SAFFRON_GYM_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x363], FlagType.TrainerBattle, "", "BEAT_SAFFRON_GYM_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x364], FlagType.TrainerBattle, "", "BEAT_SAFFRON_GYM_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x365], FlagType.TrainerBattle, "", "BEAT_SAFFRON_GYM_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x366], FlagType.TrainerBattle, "", "BEAT_SAFFRON_GYM_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x367], FlagType.TrainerBattle, "", "BEAT_SAFFRON_GYM_TRAINER_5");
            CheckMissingFlag(m_eventFlags[0x368], FlagType.TrainerBattle, "", "BEAT_SAFFRON_GYM_TRAINER_6");
            CheckMissingFlag(m_eventFlags[0x3B0], FlagType.GeneralEvent, "", "GOT_TM29");

            CheckMissingFlag(m_eventFlags[0x3C0], FlagType.GeneralEvent, "", "GOT_POTION_SAMPLE");

            CheckMissingFlag(m_eventFlags[0x3D8], FlagType.GeneralEvent, "", "GOT_HM05");

            CheckMissingFlag(m_eventFlags[0x3E2], FlagType.TrainerBattle, "", "BEAT_ROUTE_3_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x3E3], FlagType.TrainerBattle, "", "BEAT_ROUTE_3_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x3E4], FlagType.TrainerBattle, "", "BEAT_ROUTE_3_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x3E5], FlagType.TrainerBattle, "", "BEAT_ROUTE_3_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x3E6], FlagType.TrainerBattle, "", "BEAT_ROUTE_3_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x3E7], FlagType.TrainerBattle, "", "BEAT_ROUTE_3_TRAINER_5");
            CheckMissingFlag(m_eventFlags[0x3E8], FlagType.TrainerBattle, "", "BEAT_ROUTE_3_TRAINER_6");
            CheckMissingFlag(m_eventFlags[0x3E9], FlagType.TrainerBattle, "", "BEAT_ROUTE_3_TRAINER_7");

            CheckMissingFlag(m_eventFlags[0x3F2], FlagType.TrainerBattle, "", "BEAT_ROUTE_4_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x3FF], FlagType.GeneralEvent, "", "BOUGHT_MAGIKARP");

            CheckMissingFlag(m_eventFlags[0x411], FlagType.TrainerBattle, "", "BEAT_ROUTE_6_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x412], FlagType.TrainerBattle, "", "BEAT_ROUTE_6_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x413], FlagType.TrainerBattle, "", "BEAT_ROUTE_6_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x414], FlagType.TrainerBattle, "", "BEAT_ROUTE_6_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x415], FlagType.TrainerBattle, "", "BEAT_ROUTE_6_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x416], FlagType.TrainerBattle, "", "BEAT_ROUTE_6_TRAINER_5");

            CheckMissingFlag(m_eventFlags[0x431], FlagType.TrainerBattle, "", "BEAT_ROUTE_8_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x432], FlagType.TrainerBattle, "", "BEAT_ROUTE_8_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x433], FlagType.TrainerBattle, "", "BEAT_ROUTE_8_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x434], FlagType.TrainerBattle, "", "BEAT_ROUTE_8_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x435], FlagType.TrainerBattle, "", "BEAT_ROUTE_8_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x436], FlagType.TrainerBattle, "", "BEAT_ROUTE_8_TRAINER_5");
            CheckMissingFlag(m_eventFlags[0x437], FlagType.TrainerBattle, "", "BEAT_ROUTE_8_TRAINER_6");
            CheckMissingFlag(m_eventFlags[0x438], FlagType.TrainerBattle, "", "BEAT_ROUTE_8_TRAINER_7");
            CheckMissingFlag(m_eventFlags[0x439], FlagType.TrainerBattle, "", "BEAT_ROUTE_8_TRAINER_8");

            CheckMissingFlag(m_eventFlags[0x441], FlagType.TrainerBattle, "", "BEAT_ROUTE_9_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x442], FlagType.TrainerBattle, "", "BEAT_ROUTE_9_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x443], FlagType.TrainerBattle, "", "BEAT_ROUTE_9_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x444], FlagType.TrainerBattle, "", "BEAT_ROUTE_9_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x445], FlagType.TrainerBattle, "", "BEAT_ROUTE_9_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x446], FlagType.TrainerBattle, "", "BEAT_ROUTE_9_TRAINER_5");
            CheckMissingFlag(m_eventFlags[0x447], FlagType.TrainerBattle, "", "BEAT_ROUTE_9_TRAINER_6");
            CheckMissingFlag(m_eventFlags[0x448], FlagType.TrainerBattle, "", "BEAT_ROUTE_9_TRAINER_7");
            CheckMissingFlag(m_eventFlags[0x449], FlagType.TrainerBattle, "", "BEAT_ROUTE_9_TRAINER_8");

            CheckMissingFlag(m_eventFlags[0x451], FlagType.TrainerBattle, "", "BEAT_ROUTE_10_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x452], FlagType.TrainerBattle, "", "BEAT_ROUTE_10_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x453], FlagType.TrainerBattle, "", "BEAT_ROUTE_10_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x454], FlagType.TrainerBattle, "", "BEAT_ROUTE_10_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x455], FlagType.TrainerBattle, "", "BEAT_ROUTE_10_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x456], FlagType.TrainerBattle, "", "BEAT_ROUTE_10_TRAINER_5");
            CheckMissingFlag(m_eventFlags[0x459], FlagType.TrainerBattle, "", "BEAT_ROCK_TUNNEL_1_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x45A], FlagType.TrainerBattle, "", "BEAT_ROCK_TUNNEL_1_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x45B], FlagType.TrainerBattle, "", "BEAT_ROCK_TUNNEL_1_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x45C], FlagType.TrainerBattle, "", "BEAT_ROCK_TUNNEL_1_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x45D], FlagType.TrainerBattle, "", "BEAT_ROCK_TUNNEL_1_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x45E], FlagType.TrainerBattle, "", "BEAT_ROCK_TUNNEL_1_TRAINER_5");
            CheckMissingFlag(m_eventFlags[0x45F], FlagType.TrainerBattle, "", "BEAT_ROCK_TUNNEL_1_TRAINER_6");
            CheckMissingFlag(m_eventFlags[0x461], FlagType.TrainerBattle, "", "BEAT_POWER_PLANT_VOLTORB_0");
            CheckMissingFlag(m_eventFlags[0x462], FlagType.TrainerBattle, "", "BEAT_POWER_PLANT_VOLTORB_1");
            CheckMissingFlag(m_eventFlags[0x463], FlagType.TrainerBattle, "", "BEAT_POWER_PLANT_VOLTORB_2");
            CheckMissingFlag(m_eventFlags[0x464], FlagType.TrainerBattle, "", "BEAT_POWER_PLANT_VOLTORB_3");
            CheckMissingFlag(m_eventFlags[0x465], FlagType.TrainerBattle, "", "BEAT_POWER_PLANT_VOLTORB_4");
            CheckMissingFlag(m_eventFlags[0x466], FlagType.TrainerBattle, "", "BEAT_POWER_PLANT_VOLTORB_5");
            CheckMissingFlag(m_eventFlags[0x467], FlagType.TrainerBattle, "", "BEAT_POWER_PLANT_VOLTORB_6");
            CheckMissingFlag(m_eventFlags[0x468], FlagType.TrainerBattle, "", "BEAT_POWER_PLANT_VOLTORB_7");
            CheckMissingFlag(m_eventFlags[0x469], FlagType.GeneralEvent, "", "BEAT_ZAPDOS");

            CheckMissingFlag(m_eventFlags[0x471], FlagType.TrainerBattle, "", "BEAT_ROUTE_11_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x472], FlagType.TrainerBattle, "", "BEAT_ROUTE_11_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x473], FlagType.TrainerBattle, "", "BEAT_ROUTE_11_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x474], FlagType.TrainerBattle, "", "BEAT_ROUTE_11_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x475], FlagType.TrainerBattle, "", "BEAT_ROUTE_11_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x476], FlagType.TrainerBattle, "", "BEAT_ROUTE_11_TRAINER_5");
            CheckMissingFlag(m_eventFlags[0x477], FlagType.TrainerBattle, "", "BEAT_ROUTE_11_TRAINER_6");
            CheckMissingFlag(m_eventFlags[0x478], FlagType.TrainerBattle, "", "BEAT_ROUTE_11_TRAINER_7");
            CheckMissingFlag(m_eventFlags[0x479], FlagType.TrainerBattle, "", "BEAT_ROUTE_11_TRAINER_8");
            CheckMissingFlag(m_eventFlags[0x47A], FlagType.TrainerBattle, "", "BEAT_ROUTE_11_TRAINER_9");
            CheckMissingFlag(m_eventFlags[0x47F], FlagType.GeneralEvent, "", "GOT_ITEMFINDER");

            CheckMissingFlag(m_eventFlags[0x480], FlagType.GeneralEvent, "", "GOT_TM39");
            CheckMissingFlag(m_eventFlags[0x482], FlagType.TrainerBattle, "", "BEAT_ROUTE_12_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x483], FlagType.TrainerBattle, "", "BEAT_ROUTE_12_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x484], FlagType.TrainerBattle, "", "BEAT_ROUTE_12_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x485], FlagType.TrainerBattle, "", "BEAT_ROUTE_12_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x486], FlagType.TrainerBattle, "", "BEAT_ROUTE_12_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x487], FlagType.TrainerBattle, "", "BEAT_ROUTE_12_TRAINER_5");
            CheckMissingFlag(m_eventFlags[0x488], FlagType.TrainerBattle, "", "BEAT_ROUTE_12_TRAINER_6");
            CheckMissingFlag(m_eventFlags[0x48F], FlagType.TrainerBattle, "", "BEAT_ROUTE12_SNORLAX");

            CheckMissingFlag(m_eventFlags[0x491], FlagType.TrainerBattle, "", "BEAT_ROUTE_13_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x492], FlagType.TrainerBattle, "", "BEAT_ROUTE_13_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x493], FlagType.TrainerBattle, "", "BEAT_ROUTE_13_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x494], FlagType.TrainerBattle, "", "BEAT_ROUTE_13_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x495], FlagType.TrainerBattle, "", "BEAT_ROUTE_13_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x496], FlagType.TrainerBattle, "", "BEAT_ROUTE_13_TRAINER_5");
            CheckMissingFlag(m_eventFlags[0x497], FlagType.TrainerBattle, "", "BEAT_ROUTE_13_TRAINER_6");
            CheckMissingFlag(m_eventFlags[0x498], FlagType.TrainerBattle, "", "BEAT_ROUTE_13_TRAINER_7");
            CheckMissingFlag(m_eventFlags[0x499], FlagType.TrainerBattle, "", "BEAT_ROUTE_13_TRAINER_8");
            CheckMissingFlag(m_eventFlags[0x49A], FlagType.TrainerBattle, "", "BEAT_ROUTE_13_TRAINER_9");

            CheckMissingFlag(m_eventFlags[0x4A1], FlagType.TrainerBattle, "", "BEAT_ROUTE_14_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x4A2], FlagType.TrainerBattle, "", "BEAT_ROUTE_14_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x4A3], FlagType.TrainerBattle, "", "BEAT_ROUTE_14_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x4A4], FlagType.TrainerBattle, "", "BEAT_ROUTE_14_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x4A5], FlagType.TrainerBattle, "", "BEAT_ROUTE_14_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x4A6], FlagType.TrainerBattle, "", "BEAT_ROUTE_14_TRAINER_5");
            CheckMissingFlag(m_eventFlags[0x4A7], FlagType.TrainerBattle, "", "BEAT_ROUTE_14_TRAINER_6");
            CheckMissingFlag(m_eventFlags[0x4A8], FlagType.TrainerBattle, "", "BEAT_ROUTE_14_TRAINER_7");
            CheckMissingFlag(m_eventFlags[0x4A9], FlagType.TrainerBattle, "", "BEAT_ROUTE_14_TRAINER_8");
            CheckMissingFlag(m_eventFlags[0x4AA], FlagType.TrainerBattle, "", "BEAT_ROUTE_14_TRAINER_9");

            CheckMissingFlag(m_eventFlags[0x4B0], FlagType.GeneralEvent, "", "GOT_EXP_ALL");
            CheckMissingFlag(m_eventFlags[0x4B1], FlagType.TrainerBattle, "", "BEAT_ROUTE_15_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x4B2], FlagType.TrainerBattle, "", "BEAT_ROUTE_15_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x4B3], FlagType.TrainerBattle, "", "BEAT_ROUTE_15_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x4B4], FlagType.TrainerBattle, "", "BEAT_ROUTE_15_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x4B5], FlagType.TrainerBattle, "", "BEAT_ROUTE_15_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x4B6], FlagType.TrainerBattle, "", "BEAT_ROUTE_15_TRAINER_5");
            CheckMissingFlag(m_eventFlags[0x4B7], FlagType.TrainerBattle, "", "BEAT_ROUTE_15_TRAINER_6");
            CheckMissingFlag(m_eventFlags[0x4B8], FlagType.TrainerBattle, "", "BEAT_ROUTE_15_TRAINER_7");
            CheckMissingFlag(m_eventFlags[0x4B9], FlagType.TrainerBattle, "", "BEAT_ROUTE_15_TRAINER_8");
            CheckMissingFlag(m_eventFlags[0x4BA], FlagType.TrainerBattle, "", "BEAT_ROUTE_15_TRAINER_9");

            CheckMissingFlag(m_eventFlags[0x4C1], FlagType.TrainerBattle, "", "BEAT_ROUTE_16_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x4C2], FlagType.TrainerBattle, "", "BEAT_ROUTE_16_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x4C3], FlagType.TrainerBattle, "", "BEAT_ROUTE_16_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x4C4], FlagType.TrainerBattle, "", "BEAT_ROUTE_16_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x4C5], FlagType.TrainerBattle, "", "BEAT_ROUTE_16_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x4C6], FlagType.TrainerBattle, "", "BEAT_ROUTE_16_TRAINER_5");
            CheckMissingFlag(m_eventFlags[0x4C9], FlagType.TrainerBattle, "", "BEAT_ROUTE16_SNORLAX");
            CheckMissingFlag(m_eventFlags[0x4CE], FlagType.GeneralEvent, "", "GOT_HM02");

            CheckMissingFlag(m_eventFlags[0x4D1], FlagType.TrainerBattle, "", "BEAT_ROUTE_17_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x4D2], FlagType.TrainerBattle, "", "BEAT_ROUTE_17_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x4D3], FlagType.TrainerBattle, "", "BEAT_ROUTE_17_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x4D4], FlagType.TrainerBattle, "", "BEAT_ROUTE_17_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x4D5], FlagType.TrainerBattle, "", "BEAT_ROUTE_17_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x4D6], FlagType.TrainerBattle, "", "BEAT_ROUTE_17_TRAINER_5");
            CheckMissingFlag(m_eventFlags[0x4D7], FlagType.TrainerBattle, "", "BEAT_ROUTE_17_TRAINER_6");
            CheckMissingFlag(m_eventFlags[0x4D8], FlagType.TrainerBattle, "", "BEAT_ROUTE_17_TRAINER_7");
            CheckMissingFlag(m_eventFlags[0x4D9], FlagType.TrainerBattle, "", "BEAT_ROUTE_17_TRAINER_8");
            CheckMissingFlag(m_eventFlags[0x4DA], FlagType.TrainerBattle, "", "BEAT_ROUTE_17_TRAINER_9");

            CheckMissingFlag(m_eventFlags[0x4E1], FlagType.TrainerBattle, "", "BEAT_ROUTE_18_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x4E2], FlagType.TrainerBattle, "", "BEAT_ROUTE_18_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x4E3], FlagType.TrainerBattle, "", "BEAT_ROUTE_18_TRAINER_2");

            CheckMissingFlag(m_eventFlags[0x4F1], FlagType.TrainerBattle, "", "BEAT_ROUTE_19_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x4F2], FlagType.TrainerBattle, "", "BEAT_ROUTE_19_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x4F3], FlagType.TrainerBattle, "", "BEAT_ROUTE_19_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x4F4], FlagType.TrainerBattle, "", "BEAT_ROUTE_19_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x4F5], FlagType.TrainerBattle, "", "BEAT_ROUTE_19_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x4F6], FlagType.TrainerBattle, "", "BEAT_ROUTE_19_TRAINER_5");
            CheckMissingFlag(m_eventFlags[0x4F7], FlagType.TrainerBattle, "", "BEAT_ROUTE_19_TRAINER_6");
            CheckMissingFlag(m_eventFlags[0x4F8], FlagType.TrainerBattle, "", "BEAT_ROUTE_19_TRAINER_7");
            CheckMissingFlag(m_eventFlags[0x4F9], FlagType.TrainerBattle, "", "BEAT_ROUTE_19_TRAINER_8");
            CheckMissingFlag(m_eventFlags[0x4FA], FlagType.TrainerBattle, "", "BEAT_ROUTE_19_TRAINER_9");

            CheckMissingFlag(m_eventFlags[0x501], FlagType.TrainerBattle, "", "BEAT_ROUTE_20_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x502], FlagType.TrainerBattle, "", "BEAT_ROUTE_20_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x503], FlagType.TrainerBattle, "", "BEAT_ROUTE_20_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x504], FlagType.TrainerBattle, "", "BEAT_ROUTE_20_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x505], FlagType.TrainerBattle, "", "BEAT_ROUTE_20_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x506], FlagType.TrainerBattle, "", "BEAT_ROUTE_20_TRAINER_5");
            CheckMissingFlag(m_eventFlags[0x507], FlagType.TrainerBattle, "", "BEAT_ROUTE_20_TRAINER_6");
            CheckMissingFlag(m_eventFlags[0x508], FlagType.TrainerBattle, "", "BEAT_ROUTE_20_TRAINER_7");
            CheckMissingFlag(m_eventFlags[0x509], FlagType.TrainerBattle, "", "BEAT_ROUTE_20_TRAINER_8");
            CheckMissingFlag(m_eventFlags[0x50A], FlagType.TrainerBattle, "", "BEAT_ROUTE_20_TRAINER_9");

            CheckMissingFlag(m_eventFlags[0x511], FlagType.TrainerBattle, "", "BEAT_ROUTE_21_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x512], FlagType.TrainerBattle, "", "BEAT_ROUTE_21_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x513], FlagType.TrainerBattle, "", "BEAT_ROUTE_21_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x514], FlagType.TrainerBattle, "", "BEAT_ROUTE_21_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x515], FlagType.TrainerBattle, "", "BEAT_ROUTE_21_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x516], FlagType.TrainerBattle, "", "BEAT_ROUTE_21_TRAINER_5");
            CheckMissingFlag(m_eventFlags[0x517], FlagType.TrainerBattle, "", "BEAT_ROUTE_21_TRAINER_6");
            CheckMissingFlag(m_eventFlags[0x518], FlagType.TrainerBattle, "", "BEAT_ROUTE_21_TRAINER_7");
            CheckMissingFlag(m_eventFlags[0x519], FlagType.TrainerBattle, "", "BEAT_ROUTE_21_TRAINER_8");

            CheckMissingFlag(m_eventFlags[0x525], FlagType.TrainerBattle, "", "BEAT_ROUTE22_RIVAL_1ST_BATTLE");
            CheckMissingFlag(m_eventFlags[0x526], FlagType.TrainerBattle, "", "BEAT_ROUTE22_RIVAL_2ND_BATTLE");

            CheckMissingFlag(m_eventFlags[0x539], FlagType.TrainerBattle, "", "BEAT_VICTORY_ROAD_2_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x53A], FlagType.TrainerBattle, "", "BEAT_VICTORY_ROAD_2_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x53B], FlagType.TrainerBattle, "", "BEAT_VICTORY_ROAD_2_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x53C], FlagType.TrainerBattle, "", "BEAT_VICTORY_ROAD_2_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x53D], FlagType.TrainerBattle, "", "BEAT_VICTORY_ROAD_2_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x53E], FlagType.GeneralEvent, "", "BEAT_MOLTRES");

            CheckMissingFlag(m_eventFlags[0x540], FlagType.GeneralEvent, "", "GOT_NUGGET");
            CheckMissingFlag(m_eventFlags[0x541], FlagType.TrainerBattle, "", "BEAT_ROUTE24_ROCKET");
            CheckMissingFlag(m_eventFlags[0x542], FlagType.TrainerBattle, "", "BEAT_ROUTE_24_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x543], FlagType.TrainerBattle, "", "BEAT_ROUTE_24_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x544], FlagType.TrainerBattle, "", "BEAT_ROUTE_24_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x545], FlagType.TrainerBattle, "", "BEAT_ROUTE_24_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x546], FlagType.TrainerBattle, "", "BEAT_ROUTE_24_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x547], FlagType.TrainerBattle, "", "BEAT_ROUTE_24_TRAINER_5");

            CheckMissingFlag(m_eventFlags[0x54F], FlagType.GeneralEvent, "", "GOT_CHARMANDER_FROM_DAMIAN");
            CheckMissingFlag(m_eventFlags[0x551], FlagType.TrainerBattle, "", "BEAT_ROUTE_25_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x552], FlagType.TrainerBattle, "", "BEAT_ROUTE_25_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x553], FlagType.TrainerBattle, "", "BEAT_ROUTE_25_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x554], FlagType.TrainerBattle, "", "BEAT_ROUTE_25_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x555], FlagType.TrainerBattle, "", "BEAT_ROUTE_25_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x556], FlagType.TrainerBattle, "", "BEAT_ROUTE_25_TRAINER_5");
            CheckMissingFlag(m_eventFlags[0x557], FlagType.TrainerBattle, "", "BEAT_ROUTE_25_TRAINER_6");
            CheckMissingFlag(m_eventFlags[0x558], FlagType.TrainerBattle, "", "BEAT_ROUTE_25_TRAINER_7");
            CheckMissingFlag(m_eventFlags[0x559], FlagType.TrainerBattle, "", "BEAT_ROUTE_25_TRAINER_8");
            CheckMissingFlag(m_eventFlags[0x55C], FlagType.GeneralEvent, "", "GOT_SS_TICKET");

            CheckMissingFlag(m_eventFlags[0x562], FlagType.TrainerBattle, "", "BEAT_VIRIDIAN_FOREST_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x563], FlagType.TrainerBattle, "", "BEAT_VIRIDIAN_FOREST_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x564], FlagType.TrainerBattle, "", "BEAT_VIRIDIAN_FOREST_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x565], FlagType.TrainerBattle, "", "BEAT_VIRIDIAN_FOREST_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x566], FlagType.TrainerBattle, "", "BEAT_VIRIDIAN_FOREST_TRAINER_4");

            CheckMissingFlag(m_eventFlags[0x571], FlagType.TrainerBattle, "", "BEAT_MT_MOON_1_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x572], FlagType.TrainerBattle, "", "BEAT_MT_MOON_1_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x573], FlagType.TrainerBattle, "", "BEAT_MT_MOON_1_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x574], FlagType.TrainerBattle, "", "BEAT_MT_MOON_1_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x575], FlagType.TrainerBattle, "", "BEAT_MT_MOON_1_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x576], FlagType.TrainerBattle, "", "BEAT_MT_MOON_1_TRAINER_5");
            CheckMissingFlag(m_eventFlags[0x577], FlagType.TrainerBattle, "", "BEAT_MT_MOON_1_TRAINER_6");
            CheckMissingFlag(m_eventFlags[0x579], FlagType.TrainerBattle, "", "BEAT_MT_MOON_EXIT_SUPER_NERD");
            CheckMissingFlag(m_eventFlags[0x57A], FlagType.TrainerBattle, "", "BEAT_MT_MOON_3_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x57B], FlagType.TrainerBattle, "", "BEAT_MT_MOON_3_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x57C], FlagType.TrainerBattle, "", "BEAT_MT_MOON_3_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x57D], FlagType.TrainerBattle, "", "BEAT_MT_MOON_3_TRAINER_3");

            if (!m_eventFlags[0x578] && !m_eventFlags[0x57F])
            {
                //CheckMissingFlag(m_eventFlags[0x578], FlagType.GeneralEvent, "", "GOT_DOME_FOSSIL");
                //CheckMissingFlag(m_eventFlags[0x57F], FlagType.GeneralEvent, "", "GOT_HELIX_FOSSIL");

                CheckMissingFlag(m_eventFlags[0x578], FlagType.GeneralEvent, "", "GOT_DOME/HELIX_FOSSIL");
            }

            CheckMissingFlag(m_eventFlags[0x5C4], FlagType.TrainerBattle, "", "BEAT_SS_ANNE_5_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x5C5], FlagType.TrainerBattle, "", "BEAT_SS_ANNE_5_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x5E0], FlagType.GeneralEvent, "", "GOT_HM01");
            CheckMissingFlag(m_eventFlags[0x5F1], FlagType.TrainerBattle, "", "BEAT_SS_ANNE_8_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x5F2], FlagType.TrainerBattle, "", "BEAT_SS_ANNE_8_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x5F3], FlagType.TrainerBattle, "", "BEAT_SS_ANNE_8_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x5F4], FlagType.TrainerBattle, "", "BEAT_SS_ANNE_8_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x601], FlagType.TrainerBattle, "", "BEAT_SS_ANNE_9_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x602], FlagType.TrainerBattle, "", "BEAT_SS_ANNE_9_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x603], FlagType.TrainerBattle, "", "BEAT_SS_ANNE_9_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x604], FlagType.TrainerBattle, "", "BEAT_SS_ANNE_9_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x611], FlagType.TrainerBattle, "", "BEAT_SS_ANNE_10_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x612], FlagType.TrainerBattle, "", "BEAT_SS_ANNE_10_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x613], FlagType.TrainerBattle, "", "BEAT_SS_ANNE_10_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x614], FlagType.TrainerBattle, "", "BEAT_SS_ANNE_10_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x615], FlagType.TrainerBattle, "", "BEAT_SS_ANNE_10_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x616], FlagType.TrainerBattle, "", "BEAT_SS_ANNE_10_TRAINER_5");

            CheckMissingFlag(m_eventFlags[0x661], FlagType.TrainerBattle, "", "BEAT_VICTORY_ROAD_3_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x662], FlagType.TrainerBattle, "", "BEAT_VICTORY_ROAD_3_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x663], FlagType.TrainerBattle, "", "BEAT_VICTORY_ROAD_3_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x664], FlagType.TrainerBattle, "", "BEAT_VICTORY_ROAD_3_TRAINER_3");

            CheckMissingFlag(m_eventFlags[0x671], FlagType.TrainerBattle, "", "BEAT_ROCKET_HIDEOUT_1_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x672], FlagType.TrainerBattle, "", "BEAT_ROCKET_HIDEOUT_1_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x673], FlagType.TrainerBattle, "", "BEAT_ROCKET_HIDEOUT_1_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x674], FlagType.TrainerBattle, "", "BEAT_ROCKET_HIDEOUT_1_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x675], FlagType.TrainerBattle, "", "BEAT_ROCKET_HIDEOUT_1_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x681], FlagType.TrainerBattle, "", "BEAT_ROCKET_HIDEOUT_2_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x691], FlagType.TrainerBattle, "", "BEAT_ROCKET_HIDEOUT_3_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x692], FlagType.TrainerBattle, "", "BEAT_ROCKET_HIDEOUT_3_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x6A2], FlagType.TrainerBattle, "", "BEAT_ROCKET_HIDEOUT_4_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x6A3], FlagType.TrainerBattle, "", "BEAT_ROCKET_HIDEOUT_4_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x6A4], FlagType.TrainerBattle, "", "BEAT_ROCKET_HIDEOUT_4_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x6A7], FlagType.TrainerBattle, "", "BEAT_ROCKET_HIDEOUT_GIOVANNI");

            CheckMissingFlag(m_eventFlags[0x6F2], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_2F_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x6F3], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_2F_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x6F4], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_2F_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x6F5], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_2F_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x6FF], FlagType.GeneralEvent, "", "GOT_TM36");
            CheckMissingFlag(m_eventFlags[0x702], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_3F_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x703], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_3F_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x712], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_4F_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x713], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_4F_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x714], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_4F_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x722], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_5F_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x723], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_5F_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x724], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_5F_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x725], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_5F_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x736], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_6F_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x737], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_6F_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x738], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_6F_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x740], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_RIVAL");
            CheckMissingFlag(m_eventFlags[0x745], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_7F_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x746], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_7F_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x747], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_7F_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x748], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_7F_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x752], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_8F_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x753], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_8F_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x754], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_8F_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x762], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_9F_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x763], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_9F_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x764], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_9F_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x771], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_10F_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x772], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_10F_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x784], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_11F_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x785], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_11F_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x78D], FlagType.GeneralEvent, "", "GOT_MASTER_BALL");
            CheckMissingFlag(m_eventFlags[0x78F], FlagType.TrainerBattle, "", "BEAT_SILPH_CO_GIOVANNI");

            CheckMissingFlag(m_eventFlags[0x801], FlagType.TrainerBattle, "", "BEAT_MANSION_2_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x811], FlagType.TrainerBattle, "", "BEAT_MANSION_3_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x812], FlagType.TrainerBattle, "", "BEAT_MANSION_3_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x821], FlagType.TrainerBattle, "", "BEAT_MANSION_4_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x822], FlagType.TrainerBattle, "", "BEAT_MANSION_4_TRAINER_1");

            CheckMissingFlag(m_eventFlags[0x880], FlagType.GeneralEvent, "", "GOT_HM03");

            CheckMissingFlag(m_eventFlags[0x8C1], FlagType.GeneralEvent, "", "BEAT_MEWTWO");

            CheckMissingFlag(m_eventFlags[0x911], FlagType.TrainerBattle, "", "BEAT_VICTORY_ROAD_1_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x912], FlagType.TrainerBattle, "", "BEAT_VICTORY_ROAD_1_TRAINER_1");

            CheckMissingFlag(m_eventFlags[0x9B1], FlagType.TrainerBattle, "", "BEAT_ROCK_TUNNEL_2_TRAINER_0");
            CheckMissingFlag(m_eventFlags[0x9B2], FlagType.TrainerBattle, "", "BEAT_ROCK_TUNNEL_2_TRAINER_1");
            CheckMissingFlag(m_eventFlags[0x9B3], FlagType.TrainerBattle, "", "BEAT_ROCK_TUNNEL_2_TRAINER_2");
            CheckMissingFlag(m_eventFlags[0x9B4], FlagType.TrainerBattle, "", "BEAT_ROCK_TUNNEL_2_TRAINER_3");
            CheckMissingFlag(m_eventFlags[0x9B5], FlagType.TrainerBattle, "", "BEAT_ROCK_TUNNEL_2_TRAINER_4");
            CheckMissingFlag(m_eventFlags[0x9B6], FlagType.TrainerBattle, "", "BEAT_ROCK_TUNNEL_2_TRAINER_5");
            CheckMissingFlag(m_eventFlags[0x9B7], FlagType.TrainerBattle, "", "BEAT_ROCK_TUNNEL_2_TRAINER_6");
            CheckMissingFlag(m_eventFlags[0x9B8], FlagType.TrainerBattle, "", "BEAT_ROCK_TUNNEL_2_TRAINER_7");

            CheckMissingFlag(m_eventFlags[0x9DA], FlagType.GeneralEvent, "", "BEAT_ARTICUNO");
        }
    }

}
