using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal class FlagsGen1RB : FlagsOrganizer
    {
        static string s_flagsList_res = null;

        enum FlagOffsets_INTL
        {
            BadgeFlags_INTL = 0x2602,
            MissableObjectFlags_INTL = 0x2852,
            ObtainedHiddenItems_INTL = 0x299C,
            ObtainedHiddenCoins_INTL = 0x29AA,
            RodFlags_INTL = 0x29D4,
            LaprasFlag_INTL = 0x29DA,
            CompletedInGameTradeFlags_INTL = 0x29E3,
            EventFlags_INTL = 0x29F3,
        }

        int BadgeFlagsOffset;
        int MissableObjectFlagsOffset;
        int ObtainedHiddenItemsOffset;
        int ObtainedHiddenCoinsOffset;
        int RodFlagsOffset;
        int LaprasFlagOffset;
        int CompletedInGameTradeFlagsOffset;
        int EventFlagsOffset;

        const int Src_EventFlags = 0;
        const int Src_HideShowFlags = 1;
        const int Src_HiddenItemFlags = 2;
        const int Src_HiddenCoinsFlags = 3;
        const int Src_TradeFlags = 4;
        const int Src_BadgesFlags = 5;
        const int Src_MiscFlags = 6;


        protected override void InitFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;

            if ((m_savFile as SAV1).Japanese)
            {
                //TODO:
            }
            else
            {
                BadgeFlagsOffset = (int)FlagOffsets_INTL.BadgeFlags_INTL;
                MissableObjectFlagsOffset = (int)FlagOffsets_INTL.MissableObjectFlags_INTL;
                ObtainedHiddenItemsOffset = (int)FlagOffsets_INTL.ObtainedHiddenItems_INTL;
                ObtainedHiddenCoinsOffset = (int)FlagOffsets_INTL.ObtainedHiddenCoins_INTL;
                RodFlagsOffset = (int)FlagOffsets_INTL.RodFlags_INTL;
                LaprasFlagOffset = (int)FlagOffsets_INTL.LaprasFlag_INTL;
                CompletedInGameTradeFlagsOffset = (int)FlagOffsets_INTL.CompletedInGameTradeFlags_INTL;
                EventFlagsOffset = (int)FlagOffsets_INTL.EventFlags_INTL;
            }

            // wObtainedBadges
            bool[] result = new bool[8];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_savFile.GetFlag(BadgeFlagsOffset + (i >> 3), i & 7);
            }
            bool[] badgeFlags = result;

            // wMissableObjectIndex
            result = new bool[32 * 8];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_savFile.GetFlag(MissableObjectFlagsOffset + (i >> 3), i & 7);
            }
            bool[] missableObjectFlags = result;

            // wObtainedHiddenItemsFlags
            result = new bool[112];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_savFile.GetFlag(ObtainedHiddenItemsOffset + (i >> 3), i & 7);
            }
            bool[] obtainedHiddenItemsFlags = result;

            // wObtainedHiddenCoinsFlags
            result = new bool[16];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_savFile.GetFlag(ObtainedHiddenCoinsOffset + (i >> 3), i & 7);
            }
            bool[] obtainedHiddenCoinsFlags = result;

            // wCompletedInGameTradeFlags
            result = new bool[16];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_savFile.GetFlag(CompletedInGameTradeFlagsOffset + (i >> 3), i & 7);
            }
            bool[] completedInGameTradeFlags = result;

            // wd728
            bool gotOldRod = m_savFile.GetFlag(RodFlagsOffset, 3);
            bool gotGoodRod = m_savFile.GetFlag(RodFlagsOffset, 4);
            bool gotSuperRod = m_savFile.GetFlag(RodFlagsOffset, 5);

            // wd72e
            bool gotLaprasFlag = m_savFile.GetFlag(LaprasFlagOffset, 0);

            // Unified Misc flags
            result = new bool[] { gotLaprasFlag, gotOldRod, gotGoodRod, gotSuperRod };
            bool[] miscFlags = result;

            // wEventFlags
            bool[] eventFlags = (m_savFile as IEventFlagArray).GetEventFlags();

#if DEBUG
            // Force refresh
            s_flagsList_res = null;
#endif

            if (s_flagsList_res == null)
            {
                s_flagsList_res = ReadFlagsListRes("flags_gen1rb.txt");
            }

            int idxEventFlagsSection = s_flagsList_res.IndexOf("//\tEvent Flags");
            int idxHideShowSection = s_flagsList_res.IndexOf("//\tHide-Show Flags");
            int idxHiddenItemsSection = s_flagsList_res.IndexOf("//\tHidden Items Flags");
            int idxHiddenCoinsSection = s_flagsList_res.IndexOf("//\tHidden Coins Flags");
            int idxTradesSection = s_flagsList_res.IndexOf("//\tIn-Game Trades Flags");
            int idxBadgesSection = s_flagsList_res.IndexOf("//\tBadges Flags");
            int idxMiscSection = s_flagsList_res.IndexOf("//\tMisc");

            m_eventFlagsList.Clear();

            AssembleList(s_flagsList_res.Substring(idxEventFlagsSection), Src_EventFlags, eventFlags);
            AssembleList(s_flagsList_res.Substring(idxHideShowSection), Src_HideShowFlags, missableObjectFlags);
            AssembleList(s_flagsList_res.Substring(idxHiddenItemsSection), Src_HiddenItemFlags, obtainedHiddenItemsFlags);
            AssembleList(s_flagsList_res.Substring(idxHiddenCoinsSection), Src_HiddenCoinsFlags, obtainedHiddenCoinsFlags);
            AssembleList(s_flagsList_res.Substring(idxTradesSection), Src_TradeFlags, completedInGameTradeFlags);
            AssembleList(s_flagsList_res.Substring(idxBadgesSection), Src_BadgesFlags, badgeFlags);
            AssembleList(s_flagsList_res.Substring(idxMiscSection), Src_MiscFlags, miscFlags);

            // Reset S.S. Anne
            {
                //(m_savFile as IEventFlagArray).SetEventFlag(0x5E1, false);
                //(m_savFile as IEventFlagArray).SetEventFlag(0x5E2, false);
            }
        }

        public override bool SupportsEditingFlag(FlagType flagType)
        {
            switch (flagType)
            {
                case FlagType.FieldItem:
                case FlagType.HiddenItem:
                case FlagType.TrainerBattle:
                case FlagType.StaticBattle:
                case FlagType.InGameTrade:
                case FlagType.ItemGift:
                case FlagType.PkmnGift:
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

            foreach (var f in m_eventFlagsList)
            {
                if (f.FlagTypeVal == flagType)
                {
                    int fIdx = (int)f.FlagIdx;
                    
                    f.IsSet = value;

                    switch (f.SourceIdx)
                    {
                        case Src_EventFlags:
                            flagHelper.SetEventFlag(fIdx, value);
                            break;

                        case Src_HideShowFlags:
                            m_savFile.SetFlag(MissableObjectFlagsOffset + (fIdx >> 3), fIdx & 7, value);
                            break;

                        case Src_HiddenItemFlags:
                            m_savFile.SetFlag(ObtainedHiddenItemsOffset + (fIdx >> 3), fIdx & 7, value);
                            break;

                        case Src_HiddenCoinsFlags:
                            m_savFile.SetFlag(ObtainedHiddenCoinsOffset + (fIdx >> 3), fIdx & 7, value);
                            break;

                        case Src_TradeFlags:
                            m_savFile.SetFlag(CompletedInGameTradeFlagsOffset + (fIdx >> 3), fIdx & 7, value);
                            break;

                        case Src_BadgesFlags:
                            m_savFile.SetFlag(BadgeFlagsOffset + (fIdx >> 3), fIdx & 7, value);
                            break;

                        case Src_MiscFlags:
                            {
                                switch (fIdx)
                                {
                                    case 0x00: // Lapras flag
                                        m_savFile.SetFlag(LaprasFlagOffset, 0, value);
                                        break;

                                    case 0x01: // Old Rod flag
                                        m_savFile.SetFlag(RodFlagsOffset, 3, value);
                                        break;

                                    case 0x02: // Good Rod flag
                                        m_savFile.SetFlag(RodFlagsOffset, 4, value);
                                        break;

                                    case 0x03: // Super Rod flag
                                        m_savFile.SetFlag(RodFlagsOffset, 5, value);
                                        break;
                                }
                            }
                            break;
                    }

                }
            }
        }

        protected override bool ShouldExportEvent(FlagDetail eventDetail)
        {
            if (eventDetail.FlagTypeVal == FlagType.GeneralEvent)
            {
                bool shouldInclude = false;

                switch (eventDetail.FlagIdx)
                {
                    default:
                        shouldInclude = false;
                        break;
                }

                return shouldInclude;
            }
            else
            {
                return base.ShouldExportEvent(eventDetail);
            }
        }
    }

}
