namespace MissingEventFlagsCheckerPlugin
{
    internal class CheckerGen2C : EventFlagsChecker
    {
        static string? s_chkdb_res = null;

        enum FlagOffsets_INTL
        {
            StatusFlags = 0x23DA,
            StatusFlags2 = 0x23DB,
            MomSavingMoney = 0x23E2,
            JohtoBadges = 0x23E5,
            KantoBadges = 0x23E6,
            PokegearFlags = 0x24E5,
            TradeFlags = 0x24EE,
            CelebiEvent = 0x2781,
            BikeFlags = 0x2783,
            DailyFlags1 = 0x27AC,
            DailyFlags2 = 0x27AD,
            SwarmFlags = 0x27AE,
            FruitTreeFlags = 0x27B5,
            UnusedTwoDayTimerOn = 0x27C7,
            DailyRematchFlags = 0x27DA,
            DailyPhoneItemFlags = 0x27DE,
            DailyPhoneTimeOfDayFlags = 0x27E2,
            LuckyNumberShowFlag = 0x282B,
            VisitedSpawns = 0x2833,
            UnlockedUnowns = 0x2A81,
            DayCareMan = 0x2A83,
            DayCareLady = 0x2ABA,
            BestMagikarpLengthFeet = 0x2B76,
            PlayerGender = 0x3E3D,
        }

        enum FlagOffsets_JAP
        {
            StatusFlags = 0x23BC,
            StatusFlags2 = 0x23BD,
            MomSavingMoney = 0x23C4,
            JohtoBadges = 0x23C7,
            KantoBadges = 0x23C8,
            PokegearFlags = 0x24C7,
            TradeFlags = 0x24D0,
            FarfetchdPosition = 0x24D4,
            CelebiEvent = 0x2736,
            BikeFlags = 0x2738,
            DailyFlags1 = 0x2761,
            DailyFlags2 = 0x2762,
            SwarmFlags = 0x2763,
            FruitTreeFlags = 0x276A,
            UnusedTwoDayTimerOn = 0x277C, // ?
            DailyRematchFlags = 0x278F,
            DailyPhoneItemFlags = 0x2793,
            DailyPhoneTimeOfDayFlags = 0x2797,
            LuckyNumberShowFlag = 0x27E0,
            VisitedSpawns = 0x27E8,
            UnlockedUnowns = 0x2A04,
            DayCareMan = 0x2A06,
            DayCareLady = 0x2A33,
            BestMagikarpLengthFeet = 0x2ADB,
            PlayerGender = 0x8000,
        }

        int StatusFlagsOffset;
        int StatusFlags2Offset;
        int MomSavingMoneyOffset;
        int JohtoBadgesOffset;
        int KantoBadgesOffset;
        int PokegearFlagsOffset;
        int TradeFlagsOffset;
        int CelebiEventOffset;
        int BikeFlagsOffset;
        int DailyFlags1Offset;
        int DailyFlags2Offset;
        int SwarmFlagsOffset;
        int BerryTreeFlagsOffset;
        int UnusedTwoDayTimerOnOffset;
        int DailyRematchFlagsOffset;
        int DailyPhoneItemFlagsOffset;
        int DailyPhoneTimeOfDayFlagsOffset;
        int LuckyNumberShowFlagOffset;
        int VisitedSpawnsOffset;
        int UnlockedUnownsOffset;
        int DayCareManOffset;
        int DayCareLadyOffset;
        int BestMagikarpLengthFeet;
        int PlayerGenderOffset;

        bool m_Dex_completedRegional;
        bool m_Dex_completedUnownDex;
        bool m_Dex_isMythicalRegistered_Mew;
        bool m_Dex_isMythicalRegistered_Celebi;
        bool m_Dex_fullyCompleted;

        const int Src_EventFlags = 0;
        const int Src_SysFlags = 1;
        const int Src_TradeFlags = 2;
        const int Src_BerryTreeFlags = 3;
        const int Src_WorkArea = 4;

        const int Src_EvtEX = 5;
        const int Src_TrainerEX = 6;
        const int Src_Dex = 7;

        Dictionary<int, (int offset, int flagIdx)> m_sysFlagsTbl = [];

        protected override void InitData(SaveFile savFile)
        {
            m_savFile = savFile;

            var savFile_SAV2 = (SAV2)m_savFile;

            if (savFile_SAV2.Japanese)
            {
                StatusFlagsOffset = (int)FlagOffsets_JAP.StatusFlags;
                StatusFlags2Offset = (int)FlagOffsets_JAP.StatusFlags2;
                MomSavingMoneyOffset = (int)FlagOffsets_JAP.MomSavingMoney;
                JohtoBadgesOffset = (int)FlagOffsets_JAP.JohtoBadges;
                KantoBadgesOffset = (int)FlagOffsets_JAP.KantoBadges;
                PokegearFlagsOffset = (int)FlagOffsets_JAP.PokegearFlags;
                TradeFlagsOffset = (int)FlagOffsets_JAP.TradeFlags;
                CelebiEventOffset = (int)FlagOffsets_JAP.CelebiEvent;
                BikeFlagsOffset = (int)FlagOffsets_JAP.BikeFlags;
                DailyFlags1Offset = (int)FlagOffsets_JAP.DailyFlags1;
                DailyFlags2Offset = (int)FlagOffsets_JAP.DailyFlags2;
                SwarmFlagsOffset = (int)FlagOffsets_JAP.SwarmFlags;
                BerryTreeFlagsOffset = (int)FlagOffsets_JAP.FruitTreeFlags;
                UnusedTwoDayTimerOnOffset = (int)FlagOffsets_JAP.UnusedTwoDayTimerOn;
                DailyRematchFlagsOffset = (int)FlagOffsets_JAP.DailyRematchFlags;
                DailyPhoneItemFlagsOffset = (int)FlagOffsets_JAP.DailyPhoneItemFlags;
                DailyPhoneTimeOfDayFlagsOffset = (int)FlagOffsets_JAP.DailyPhoneTimeOfDayFlags;
                LuckyNumberShowFlagOffset = (int)FlagOffsets_JAP.LuckyNumberShowFlag;
                VisitedSpawnsOffset = (int)FlagOffsets_JAP.VisitedSpawns;
                UnlockedUnownsOffset = (int)FlagOffsets_JAP.UnlockedUnowns;
                DayCareManOffset = (int)FlagOffsets_JAP.DayCareMan;
                DayCareLadyOffset = (int)FlagOffsets_JAP.DayCareLady;
                BestMagikarpLengthFeet = (int)FlagOffsets_JAP.BestMagikarpLengthFeet;
                PlayerGenderOffset = (int)FlagOffsets_JAP.PlayerGender;
            }
            else
            {
                StatusFlagsOffset = (int)FlagOffsets_INTL.StatusFlags;
                StatusFlags2Offset = (int)FlagOffsets_INTL.StatusFlags2;
                MomSavingMoneyOffset = (int)FlagOffsets_INTL.MomSavingMoney;
                JohtoBadgesOffset = (int)FlagOffsets_INTL.JohtoBadges;
                KantoBadgesOffset = (int)FlagOffsets_INTL.KantoBadges;
                PokegearFlagsOffset = (int)FlagOffsets_INTL.PokegearFlags;
                TradeFlagsOffset = (int)FlagOffsets_INTL.TradeFlags;
                CelebiEventOffset = (int)FlagOffsets_INTL.CelebiEvent;
                BikeFlagsOffset = (int)FlagOffsets_INTL.BikeFlags;
                DailyFlags1Offset = (int)FlagOffsets_INTL.DailyFlags1;
                DailyFlags2Offset = (int)FlagOffsets_INTL.DailyFlags2;
                SwarmFlagsOffset = (int)FlagOffsets_INTL.SwarmFlags;
                BerryTreeFlagsOffset = (int)FlagOffsets_INTL.FruitTreeFlags;
                UnusedTwoDayTimerOnOffset = (int)FlagOffsets_INTL.UnusedTwoDayTimerOn;
                DailyRematchFlagsOffset = (int)FlagOffsets_INTL.DailyRematchFlags;
                DailyPhoneItemFlagsOffset = (int)FlagOffsets_INTL.DailyPhoneItemFlags;
                DailyPhoneTimeOfDayFlagsOffset = (int)FlagOffsets_INTL.DailyPhoneTimeOfDayFlags;
                LuckyNumberShowFlagOffset = (int)FlagOffsets_INTL.LuckyNumberShowFlag;
                VisitedSpawnsOffset = (int)FlagOffsets_INTL.VisitedSpawns;
                UnlockedUnownsOffset = (int)FlagOffsets_INTL.UnlockedUnowns;
                DayCareManOffset = (int)FlagOffsets_INTL.DayCareMan;
                DayCareLadyOffset = (int)FlagOffsets_INTL.DayCareLady;
                BestMagikarpLengthFeet = (int)FlagOffsets_INTL.BestMagikarpLengthFeet;
                PlayerGenderOffset = (int)FlagOffsets_INTL.PlayerGender;
            }

            CreateSysFlagsTbl();

            // Check Pokedex
            m_Dex_completedRegional = true;
            m_Dex_isMythicalRegistered_Mew = false;
            m_Dex_isMythicalRegistered_Celebi = false;
            for (ushort i = 001; i <= 251; i++)
            {
                switch (i)
                {
                    case 151: // Mew
                        m_Dex_isMythicalRegistered_Mew = savFile_SAV2.GetCaught(i);
                        break;

                    case 251: // Celebi
                        m_Dex_isMythicalRegistered_Celebi = savFile_SAV2.GetCaught(i);
                        break;

                    default:
                        if (!savFile_SAV2.GetCaught(i))
                        {
                            m_Dex_completedRegional = false;
                        }
                        break;
                }
            }

            m_Dex_completedUnownDex = true;
            for (int i = 0; i < 26; i++)
            {
                if (m_savFile.Data[UnlockedUnownsOffset - (26 + i)] == 0)
                {
                    m_Dex_completedUnownDex = false;
                    break;
                }
            }

            m_Dex_fullyCompleted = m_Dex_completedRegional
                && m_Dex_completedUnownDex
                && m_Dex_isMythicalRegistered_Mew
                && m_Dex_isMythicalRegistered_Celebi
                ;

            m_flagsSourceInfo["EvtFlags"] = Src_EventFlags;
            m_flagsSourceInfo["SysFlags"] = Src_SysFlags;
            m_flagsSourceInfo["TradeFlags"] = Src_TradeFlags;
            m_flagsSourceInfo["BerryTrees"] = Src_BerryTreeFlags;
            m_flagsSourceInfo["WorkArea"] = Src_WorkArea;
            m_flagsSourceInfo["EvtEX"] = Src_EvtEX;
            m_flagsSourceInfo["TrainerEX"] = Src_TrainerEX;
            m_flagsSourceInfo["Dex"] = Src_Dex;
            m_flagsSourceInfo["-"] = -1;

#if DEBUG
            // Force refresh
            s_chkdb_res = null;
#endif

            s_chkdb_res ??= ReadResFile("chkdb_gen2c");

            ParseChecklist(s_chkdb_res);
        }

        void CreateSysFlagsTbl()
        {
            // Based on data/events/engine_flags

            int idx = 0;

            m_sysFlagsTbl = new Dictionary<int, (int offset, int flagIdx)>()
            {
                { idx++, (PokegearFlagsOffset, 1) }, // POKEGEAR_RADIO_CARD_F
                { idx++, (PokegearFlagsOffset, 0) }, // POKEGEAR_MAP_CARD_F
                { idx++, (PokegearFlagsOffset, 2) }, // POKEGEAR_PHONE_CARD_F
                { idx++, (PokegearFlagsOffset, 3) }, // POKEGEAR_EXPN_CARD_F
                { idx++, (PokegearFlagsOffset, 7) }, // POKEGEAR_OBTAINED_F

                { idx++, (DayCareManOffset, 6) }, // DAYCAREMAN_HAS_EGG_F
                { idx++, (DayCareManOffset, 0) }, // DAYCAREMAN_HAS_MON_F
                { idx++, (DayCareLadyOffset, 0) }, // DAYCARELADY_HAS_MON_F

                { idx++, (MomSavingMoneyOffset, 0) }, // MOM_SAVING_SOME_MONEY_F
                { idx++, (MomSavingMoneyOffset, 7) }, // MOM_ACTIVE_F

                { idx++, (UnusedTwoDayTimerOnOffset, 0) },

                { idx++, (StatusFlagsOffset, 0) }, // STATUSFLAGS_POKEDEX_F
                { idx++, (StatusFlagsOffset, 1) }, // STATUSFLAGS_UNOWN_DEX_F
                { idx++, (StatusFlagsOffset, 3) }, // STATUSFLAGS_CAUGHT_POKERUS_F
                { idx++, (StatusFlagsOffset, 4) }, // STATUSFLAGS_ROCKET_SIGNAL_F
                { idx++, (StatusFlagsOffset, 6) }, // STATUSFLAGS_HALL_OF_FAME_F
                { idx++, (StatusFlagsOffset, 7) }, // STATUSFLAGS_MAIN_MENU_MOBILE_CHOICES_F

                { idx++, (StatusFlags2Offset, 2) }, // STATUSFLAGS2_BUG_CONTEST_TIMER_F
                { idx++, (StatusFlags2Offset, 1) }, // STATUSFLAGS2_SAFARI_GAME_F
                { idx++, (StatusFlags2Offset, 0) }, // STATUSFLAGS2_ROCKETS_IN_RADIO_TOWER_F
                { idx++, (StatusFlags2Offset, 4) }, // STATUSFLAGS2_BIKE_SHOP_CALL_F
                { idx++, (StatusFlags2Offset, 5) }, // STATUSFLAGS2_UNUSED_5_F
                { idx++, (StatusFlags2Offset, 6) }, // STATUSFLAGS2_REACHED_GOLDENROD_F
                { idx++, (StatusFlags2Offset, 7) }, // STATUSFLAGS2_ROCKETS_IN_MAHOGANY_F

                { idx++, (BikeFlagsOffset, 0) }, // BIKEFLAGS_STRENGTH_ACTIVE_F
                { idx++, (BikeFlagsOffset, 1) }, // BIKEFLAGS_ALWAYS_ON_BIKE_F
                { idx++, (BikeFlagsOffset, 2) }, // BIKEFLAGS_DOWNHILL_F

                { idx++, (JohtoBadgesOffset, 0) }, // ZEPHYRBADGE
                { idx++, (JohtoBadgesOffset, 1) }, // HIVEBADGE
                { idx++, (JohtoBadgesOffset, 2) }, // PLAINBADGE
                { idx++, (JohtoBadgesOffset, 3) }, // FOGBADGE
                { idx++, (JohtoBadgesOffset, 4) }, // MINERALBADGE
                { idx++, (JohtoBadgesOffset, 5) }, // STORMBADGE
                { idx++, (JohtoBadgesOffset, 6) }, // GLACIERBADGE
                { idx++, (JohtoBadgesOffset, 7) }, // RISINGBADGE

                { idx++, (KantoBadgesOffset, 0) }, // BOULDERBADGE
                { idx++, (KantoBadgesOffset, 1) }, // CASCADEBADGE
                { idx++, (KantoBadgesOffset, 2) }, // THUNDERBADGE
                { idx++, (KantoBadgesOffset, 3) }, // RAINBOWBADGE
                { idx++, (KantoBadgesOffset, 4) }, // SOULBADGE
                { idx++, (KantoBadgesOffset, 5) }, // MARSHBADGE
                { idx++, (KantoBadgesOffset, 6) }, // VOLCANOBADGE
                { idx++, (KantoBadgesOffset, 7) }, // EARTHBADGE

                { idx++, (UnlockedUnownsOffset, 0) }, // UNLOCKED_UNOWNS_A_TO_K_F
                { idx++, (UnlockedUnownsOffset, 1) }, // UNLOCKED_UNOWNS_L_TO_R_F
                { idx++, (UnlockedUnownsOffset, 2) }, // UNLOCKED_UNOWNS_S_TO_W_F
                { idx++, (UnlockedUnownsOffset, 3) }, // UNLOCKED_UNOWNS_X_TO_Z_F
                { idx++, (UnlockedUnownsOffset, 4) },
                { idx++, (UnlockedUnownsOffset, 5) },
                { idx++, (UnlockedUnownsOffset, 6) },
                { idx++, (UnlockedUnownsOffset, 7) },

                { idx++, (VisitedSpawnsOffset, 0) },  // SPAWN_HOME
                { idx++, (VisitedSpawnsOffset, 1) },  // SPAWN_DEBUG
                { idx++, (VisitedSpawnsOffset, 2) },  // SPAWN_PALLET
                { idx++, (VisitedSpawnsOffset, 3) },  // SPAWN_VIRIDIAN
                { idx++, (VisitedSpawnsOffset, 4) },  // SPAWN_PEWTER
                { idx++, (VisitedSpawnsOffset, 5) },  // SPAWN_CERULEAN
                { idx++, (VisitedSpawnsOffset, 6) },  // SPAWN_ROCK_TUNNEL
                { idx++, (VisitedSpawnsOffset, 7) },  // SPAWN_VERMILION
                { idx++, (VisitedSpawnsOffset, 8) },  // SPAWN_LAVENDER
                { idx++, (VisitedSpawnsOffset, 9) },  // SPAWN_SAFFRON
                { idx++, (VisitedSpawnsOffset, 10) }, // SPAWN_CELADON
                { idx++, (VisitedSpawnsOffset, 11) }, // SPAWN_FUCHSIA
                { idx++, (VisitedSpawnsOffset, 12) }, // SPAWN_CINNABAR
                { idx++, (VisitedSpawnsOffset, 13) }, // SPAWN_INDIGO
                { idx++, (VisitedSpawnsOffset, 14) }, // SPAWN_NEW_BARK
                { idx++, (VisitedSpawnsOffset, 15) }, // SPAWN_CHERRYGROVE
                { idx++, (VisitedSpawnsOffset, 16) }, // SPAWN_VIOLET
                { idx++, (VisitedSpawnsOffset, 18) }, // SPAWN_AZALEA
                { idx++, (VisitedSpawnsOffset, 19) }, // SPAWN_CIANWOOD
                { idx++, (VisitedSpawnsOffset, 20) }, // SPAWN_GOLDENROD
                { idx++, (VisitedSpawnsOffset, 21) }, // SPAWN_OLIVINE
                { idx++, (VisitedSpawnsOffset, 22) }, // SPAWN_ECRUTEAK
                { idx++, (VisitedSpawnsOffset, 23) }, // SPAWN_MAHOGANY
                { idx++, (VisitedSpawnsOffset, 24) }, // SPAWN_LAKE_OF_RAGE
                { idx++, (VisitedSpawnsOffset, 25) }, // SPAWN_BLACKTHORN
                { idx++, (VisitedSpawnsOffset, 26) }, // SPAWN_MT_SILVER
                { idx++, (VisitedSpawnsOffset, 28) }, // NUM_SPAWNS

                { idx++, (LuckyNumberShowFlagOffset, 0) }, // LUCKYNUMBERSHOW_GAME_OVER_F

                { idx++, (StatusFlags2Offset, 3) }, // STATUSFLAGS2_UNUSED_3_F

                { idx++, (DailyFlags1Offset, 0) }, // DAILYFLAGS1_KURT_MAKING_BALLS_F
                { idx++, (DailyFlags1Offset, 1) }, // DAILYFLAGS1_BUG_CONTEST_F
                { idx++, (DailyFlags1Offset, 2) }, // DAILYFLAGS1_FISH_SWARM_F
                { idx++, (DailyFlags1Offset, 3) }, // DAILYFLAGS1_TIME_CAPSULE_F
                { idx++, (DailyFlags1Offset, 4) }, // DAILYFLAGS1_ALL_FRUIT_TREES_F
                { idx++, (DailyFlags1Offset, 5) }, // DAILYFLAGS1_GOT_SHUCKIE_TODAY_F
                { idx++, (DailyFlags1Offset, 6) }, // DAILYFLAGS1_GOLDENROD_UNDERGROUND_BARGAIN_F
                { idx++, (DailyFlags1Offset, 7) }, // DAILYFLAGS1_TRAINER_HOUSE_F

                { idx++, (DailyFlags2Offset, 0) }, // DAILYFLAGS2_MT_MOON_SQUARE_CLEFAIRY_F
                { idx++, (DailyFlags2Offset, 1) }, // DAILYFLAGS2_UNION_CAVE_LAPRAS_F
                { idx++, (DailyFlags2Offset, 2) }, // DAILYFLAGS2_GOLDENROD_UNDERGROUND_GOT_HAIRCUT_F
                { idx++, (DailyFlags2Offset, 3) }, // DAILYFLAGS2_GOLDENROD_DEPT_STORE_TM27_RETURN_F
                { idx++, (DailyFlags2Offset, 4) }, // DAILYFLAGS2_DAISYS_GROOMING_F
                { idx++, (DailyFlags2Offset, 5) }, // DAILYFLAGS2_INDIGO_PLATEAU_RIVAL_FIGHT_F
                { idx++, (DailyFlags2Offset, 6) }, // DAILYFLAGS2_MOVE_TUTOR_F
                { idx++, (DailyFlags2Offset, 7) }, // DAILYFLAGS2_BUENAS_PASSWORD_F

                { idx++, (SwarmFlagsOffset, 0) }, // SWARMFLAGS_BUENAS_PASSWORD_F
                { idx++, (SwarmFlagsOffset, 1) }, // SWARMFLAGS_GOLDENROD_DEPT_STORE_SALE_F

                { idx++, (0, 7) }, // GAME_TIMER_MOBILE_F

                { idx++, (PlayerGenderOffset, 0) }, // PLAYERGENDER_FEMALE_F

                { idx++, (CelebiEventOffset, 2) }, // CELEBIEVENT_FOREST_IS_RESTLESS_F

                { idx++, (DailyRematchFlagsOffset, 0) },  // jack
                { idx++, (DailyRematchFlagsOffset, 1) },  // huey
                { idx++, (DailyRematchFlagsOffset, 2) },  // gaven
                { idx++, (DailyRematchFlagsOffset, 3) },  // beth
                { idx++, (DailyRematchFlagsOffset, 4) },  // jose
                { idx++, (DailyRematchFlagsOffset, 5) },  // reena
                { idx++, (DailyRematchFlagsOffset, 6) },  // joey
                { idx++, (DailyRematchFlagsOffset, 7) },  // wade
                { idx++, (DailyRematchFlagsOffset, 8) },  // ralph
                { idx++, (DailyRematchFlagsOffset, 9) },  // liz
                { idx++, (DailyRematchFlagsOffset, 10) }, // anthony
                { idx++, (DailyRematchFlagsOffset, 11) }, // todd
                { idx++, (DailyRematchFlagsOffset, 12) }, // gina
                { idx++, (DailyRematchFlagsOffset, 13) }, // arnie
                { idx++, (DailyRematchFlagsOffset, 14) }, // alan
                { idx++, (DailyRematchFlagsOffset, 15) }, // dana
                { idx++, (DailyRematchFlagsOffset, 16) }, // chad
                { idx++, (DailyRematchFlagsOffset, 17) }, // tully
                { idx++, (DailyRematchFlagsOffset, 18) }, // brent
                { idx++, (DailyRematchFlagsOffset, 19) }, // tiffany
                { idx++, (DailyRematchFlagsOffset, 20) }, // vance
                { idx++, (DailyRematchFlagsOffset, 21) }, // wilton
                { idx++, (DailyRematchFlagsOffset, 22) }, // parry
                { idx++, (DailyRematchFlagsOffset, 23) }, // erin

                { idx++, (DailyPhoneItemFlagsOffset, 0) }, // beverly has nugget
                { idx++, (DailyPhoneItemFlagsOffset, 1) }, // jose has star piece
                { idx++, (DailyPhoneItemFlagsOffset, 2) }, // wade has item
                { idx++, (DailyPhoneItemFlagsOffset, 3) }, // gina has leaf stone
                { idx++, (DailyPhoneItemFlagsOffset, 4) }, // alan has fire stone
                { idx++, (DailyPhoneItemFlagsOffset, 5) }, // liz has thunderstone
                { idx++, (DailyPhoneItemFlagsOffset, 6) }, // derek has nugget
                { idx++, (DailyPhoneItemFlagsOffset, 7) }, // tully has water stone
                { idx++, (DailyPhoneItemFlagsOffset, 8) }, // tiffany has pink bow
                { idx++, (DailyPhoneItemFlagsOffset, 9) }, // wilton has item

                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 0) },  // jack
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 1) },  // huey
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 2) },  // gaven
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 3) },  // beth
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 4) },  // jose
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 5) },  // reena
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 6) },  // joey
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 7) },  // wade
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 8) },  // ralph
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 9) },  // liz
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 10) }, // anthony
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 11) }, // todd
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 12) }, // gina
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 13) }, // arnie
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 14) }, // alan
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 15) }, // dana
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 16) }, // chad
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 17) }, // tully
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 18) }, // brent
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 19) }, // tiffany
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 20) }, // vance
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 21) }, // wilton
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 22) }, // parry
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 23) }, // erin

                { idx++, (0, 2) }, // PLAYERSPRITESETUP_FEMALE_TO_MALE_F

                { idx++, (SwarmFlagsOffset, 2) }, // SWARMFLAGS_DUNSPARCE_SWARM_F
                { idx++, (SwarmFlagsOffset, 3) }, // SWARMFLAGS_YANMA_SWARM_F
            };
        }

        bool GetSysFlag(int idx)
        {
            var (offset, flagIdx) = m_sysFlagsTbl[idx];
            return m_savFile!.GetFlag(offset + (flagIdx >> 3), flagIdx & 7);
        }

        protected override bool IsEvtSet(EventDetail evtDetail)
        {
            bool isEvtSet = false;
            int idx = (int)evtDetail.EvtId;

            var eventWorkHelper = (IEventWorkArray<byte>)m_savFile!;
            var eventFlagsHelper = ((IEventFlagArray)m_savFile!);
            var sav2 = (SAV2)m_savFile!;

            switch (evtDetail.EvtSource)
            {
                case Src_EventFlags:
                    isEvtSet = eventFlagsHelper.GetEventFlag(idx);
                    break;

                case Src_SysFlags:
                    isEvtSet = GetSysFlag(idx);
                    break;

                case Src_TradeFlags:
                    isEvtSet = m_savFile!.GetFlag(TradeFlagsOffset + (idx >> 3), idx & 7);
                    break;

                case Src_BerryTreeFlags:
                    isEvtSet = m_savFile!.GetFlag(BerryTreeFlagsOffset + (idx >> 3), idx & 7);
                    break;

                case Src_WorkArea:
                    {
                        switch (idx)
                        {
                            // Rival
                            case 0x43: // wGoldenrodUndergroundSwitchRoomEntrancesSceneID
                            case 0x45: // wVictoryRoadSceneID
                            case 0x32: // wMountMoonSceneID
                                isEvtSet = eventWorkHelper.GetWork(idx) != 0;
                                break;
                            case 0x35: // wBurnedTower1FSceneID
                                isEvtSet = eventWorkHelper.GetWork(idx) == 2;
                                break;

                            default:
                                isEvtSet = false;
                                break;
                        }
                    }
                    break;

                case Src_EvtEX:
                    {
                        switch (idx)
                        {
                            case 0x00: // Elm's aide gives Potion in lab
                                {
                                    isEvtSet = eventFlagsHelper.GetEventFlag(0x1A) // EVENT_GOT_A_POKEMON_FROM_ELM
                                        && eventWorkHelper.GetWork(0x15) != 5 // wElmsLabSceneID
                                        ;
                                }
                                break;

                            case 0x01: // Rival battle at Cherrygrove
                                {
                                    isEvtSet = eventFlagsHelper.GetEventFlag(0x43) // EVENT_ELM_CALLED_ABOUT_STOLEN_POKEMON
                                        && eventWorkHelper.GetWork(0x18) == 0 // wCherrygroveCitySceneID
                                        ;
                                }
                                break;

                            case 0x02: // Elm's aide gives Poké Ball (x5)
                                {
                                    isEvtSet = eventFlagsHelper.GetEventFlag(0x1F) // EVENT_GAVE_MYSTERY_EGG_TO_ELM
                                        && eventWorkHelper.GetWork(0x15) != 6 // wElmsLabSceneID
                                        ;
                                }
                                break;

                            case 0x03: // Exchange Red Scale for Exp. Share
                                {
                                    isEvtSet = (!eventFlagsHelper.GetEventFlag(0x6D4) || eventFlagsHelper.GetEventFlag(0x60)) // !EVENT_LAKE_OF_RAGE_LANCE || EVENT_DECIDED_TO_HELP_LANCE
                                        && !HasItemInBag(m_savFile!.Inventory, 66) // Checks if Red Scale is not in Bag
                                        ;
                                }
                                break;

                            case 0x04: // Rival battle at Azalea
                                {
                                    isEvtSet = eventFlagsHelper.GetEventFlag(0x2B) // EVENT_CLEARED_SLOWPOKE_WELL
                                        && eventWorkHelper.GetWork(0x1E) == 0 // wAzaleaTownSceneID
                                        ;
                                }
                                break;

                            case 0x05: // Unlock Mystery Gift
                                {
                                    //TODO: wait PKHeX bug fix
                                    if (sav2.Japanese)
                                    {
                                        isEvtSet = m_savFile!.Data[0xB51] != 0xFF;
                                    }
                                    else
                                    {
                                        isEvtSet = m_savFile!.Data[0xBE3] != 0xFF;
                                    }

                                    //isEvtSet = sav2.MysteryGiftIsUnlocked;
                                }
                                break;

                            case 0x06: // Unlock Time Capsule
                                {
                                    isEvtSet = !eventFlagsHelper.GetEventFlag(0x712) // EVENT_MET_BILL
                                        && !GetSysFlag(0x53) // ENGINE_TIME_CAPSULE
                                        ;
                                }
                                break;

                            case 0x07: // Hidden Item - Machine Part
                                {
                                    isEvtSet = eventFlagsHelper.GetEventFlag(0xCB) // EVENT_MET_ROCKET_GRUNT_AT_CERULEAN_GYM
                                        && eventFlagsHelper.GetEventFlag(0xFB) // EVENT_FOUND_MACHINE_PART_IN_CERULEAN_GYM
                                        ;
                                }
                                break;

                            case 0x08: // Help Bike Shop owner
                                {
                                    isEvtSet = !GetSysFlag(0x14) // ENGINE_BIKE_SHOP_CALL_ENABLED
                                        && eventFlagsHelper.GetEventFlag(0x5B) // EVENT_GOT_BICYCLE
                                        ;
                                }
                                break;

                            case 0x09: // Red battle at Mt. Silver
                                {
                                    isEvtSet = eventFlagsHelper.GetEventFlag(0x74F) // EVENT_OPENED_MT_SILVER
                                        && eventFlagsHelper.GetEventFlag(0x762) // EVENT_RED_IN_MT_SILVER
                                        ;
                                }
                                break;

                            case 0x0A: // Best Magikarp size record
                                {
                                    if (sav2.Japanese)
                                    {
                                        isEvtSet = m_savFile!.Data[BestMagikarpLengthFeet] > 0x04
                                            || m_savFile!.Data[BestMagikarpLengthFeet + 1] > 0x1D
                                            ;
                                    }

                                    else
                                    {
                                        isEvtSet = m_savFile!.Data[BestMagikarpLengthFeet] > 3
                                            || m_savFile!.Data[BestMagikarpLengthFeet + 1] > 6
                                            ;
                                    }
                                }
                                break;

                            default:
                                isEvtSet = false;
                                break;
                        }
                    }
                    break;

                case Src_TrainerEX:
                    {
                        int evtWorkIdx = (idx >> 4) + 0x80;
                        int rematchesCount = idx & 0xF;

                        isEvtSet = eventWorkHelper.GetWork(evtWorkIdx) >= rematchesCount;
                    }
                    break;

                case Src_Dex:
                    {
                        switch (idx)
                        {
                            case 0: // Regional complete
                                isEvtSet = m_Dex_completedRegional;
                                break;

                            case 1: // UnownDex complete
                                isEvtSet = m_Dex_completedUnownDex;
                                break;

                            case 2: // Mew
                                isEvtSet = m_Dex_isMythicalRegistered_Mew;
                                break;

                            case 3: // Celebi
                                isEvtSet = m_Dex_isMythicalRegistered_Celebi;
                                break;

                            case 4: // Fully completed
                                isEvtSet = m_Dex_fullyCompleted;
                                break;
                        }
                    }
                    break;

                default:
                    isEvtSet = false;
                    break;
            }

            return isEvtSet;
        }
    }

}
