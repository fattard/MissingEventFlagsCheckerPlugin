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
        static string s_flagsList_res = null;

        enum FlagOffsets_INTL
        {
            BadgeFlags = 0x2602,
            MissableObjectFlags = 0x2852,
            ObtainedHiddenItems = 0x299C,
            ObtainedHiddenCoins = 0x29AA,
            RodFlags = 0x29D4,
            LaprasFlag = 0x29DA,
            CompletedInGameTradeFlags = 0x29E3,
            EventFlags = 0x29F3,
        }

        enum FlagOffsets_JAP
        {
            BadgeFlags = 0x25F8,
            MissableObjectFlags = 0x2848,
            ObtainedHiddenItems = 0x2992,
            ObtainedHiddenCoins = 0x29A0,
            RodFlags = 0x29CA,
            LaprasFlag = 0x29D0,
            CompletedInGameTradeFlags = 0x29D9,
            EventFlags = 0x29E9,
        }

        int BadgeFlagsOffset;
        int MissableObjectFlagsOffset;
        int ObtainedHiddenItemsOffset;
        int ObtainedHiddenCoinsOffset;
        int RodFlagsOffset;
        int LaprasFlagOffset;
        int CompletedInGameTradeFlagsOffset;

        const int Src_EventFlags = 0;
        const int Src_HideShowFlags = 1;
        const int Src_HiddenItemFlags = 2;
        const int Src_HiddenCoinsFlags = 3;
        const int Src_TradeFlags = 4;
        const int Src_BadgesFlags = 5;
        const int Src_Misc_wd728 = 6;
        const int Src_Misc_wd72e = 7;


        protected override void InitEventFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;

            var savFile_SAV1 = (m_savFile as SAV1);

            if (savFile_SAV1.Japanese)
            {
                BadgeFlagsOffset = (int)FlagOffsets_JAP.BadgeFlags;
                MissableObjectFlagsOffset = (int)FlagOffsets_JAP.MissableObjectFlags;
                ObtainedHiddenItemsOffset = (int)FlagOffsets_JAP.ObtainedHiddenItems;
                ObtainedHiddenCoinsOffset = (int)FlagOffsets_JAP.ObtainedHiddenCoins;
                RodFlagsOffset = (int)FlagOffsets_JAP.RodFlags;
                LaprasFlagOffset = (int)FlagOffsets_JAP.LaprasFlag;
                CompletedInGameTradeFlagsOffset = (int)FlagOffsets_JAP.CompletedInGameTradeFlags;
            }
            else
            {
                BadgeFlagsOffset = (int)FlagOffsets_INTL.BadgeFlags;
                MissableObjectFlagsOffset = (int)FlagOffsets_INTL.MissableObjectFlags;
                ObtainedHiddenItemsOffset = (int)FlagOffsets_INTL.ObtainedHiddenItems;
                ObtainedHiddenCoinsOffset = (int)FlagOffsets_INTL.ObtainedHiddenCoins;
                RodFlagsOffset = (int)FlagOffsets_INTL.RodFlags;
                LaprasFlagOffset = (int)FlagOffsets_INTL.LaprasFlag;
                CompletedInGameTradeFlagsOffset = (int)FlagOffsets_INTL.CompletedInGameTradeFlags;
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
            result = new bool[8];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_savFile.GetFlag(RodFlagsOffset + (i >> 3), i & 7);
            }
            bool[] miscFlags_wd728 = result;

            // wd72e
            result = new bool[8];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_savFile.GetFlag(LaprasFlagOffset + (i >> 3), i & 7);
            }
            bool[] miscFlags_wd72e = result;

            // wEventFlags
            bool[] eventFlags = (m_savFile as IEventFlagArray).GetEventFlags();

            // Check Pokedex
            bool completedPokedex = true;
            bool isMewRegistered = false;
            for (ushort i = 001; i <= 151; i++)
            {
                switch (i)
                {
                    case 151: // Mew
                        isMewRegistered = savFile_SAV1.GetCaught(i);
                        break;

                    default:
                        if (!savFile_SAV1.GetCaught(i))
                        {
                            completedPokedex = false;
                        }
                        break;

                }
            }

            // Check if has PikaBeach hi-score
            bool hasPikaBeachHiScore = (savFile_SAV1.PikaBeachScore > 0);

#if DEBUG
            // Force refresh
            s_flagsList_res = null;
#endif

            if (s_flagsList_res == null)
            {
                s_flagsList_res = ReadResFile("flags_gen1y.txt");
            }

            int idxEventFlagsSection = s_flagsList_res.IndexOf("//\tEvent Flags");
            int idxHideShowSection = s_flagsList_res.IndexOf("//\tHide-Show Flags");
            int idxHiddenItemsSection = s_flagsList_res.IndexOf("//\tHidden Items Flags");
            int idxHiddenCoinsSection = s_flagsList_res.IndexOf("//\tHidden Coins Flags");
            int idxTradesSection = s_flagsList_res.IndexOf("//\tIn-Game Trades Flags");
            int idxBadgesSection = s_flagsList_res.IndexOf("//\tBadges Flags");
            int idxMisc_wd728_Section = s_flagsList_res.IndexOf("//\tMisc-wd728");
            int idxMisc_wd72e_Section = s_flagsList_res.IndexOf("//\tMisc-wd72e");

            m_eventFlagsList.Clear();

            AssembleList(s_flagsList_res.Substring(idxEventFlagsSection), Src_EventFlags, eventFlags);
            AssembleList(s_flagsList_res.Substring(idxHideShowSection), Src_HideShowFlags, missableObjectFlags);
            AssembleList(s_flagsList_res.Substring(idxHiddenItemsSection), Src_HiddenItemFlags, obtainedHiddenItemsFlags);
            AssembleList(s_flagsList_res.Substring(idxHiddenCoinsSection), Src_HiddenCoinsFlags, obtainedHiddenCoinsFlags);
            AssembleList(s_flagsList_res.Substring(idxTradesSection), Src_TradeFlags, completedInGameTradeFlags);
            AssembleList(s_flagsList_res.Substring(idxBadgesSection), Src_BadgesFlags, badgeFlags);
            AssembleList(s_flagsList_res.Substring(idxMisc_wd728_Section), Src_Misc_wd728, miscFlags_wd728);
            AssembleList(s_flagsList_res.Substring(idxMisc_wd72e_Section), Src_Misc_wd72e, miscFlags_wd72e);

            //TEMP
            m_eventsChecklist.Clear();
            foreach (var flagDetail in m_eventFlagsList)
            {
                if (ShouldExportEvent(flagDetail))
                {
                    var evtDetail = new EventDetail(flagDetail);
                    evtDetail.IsDone = IsEvtSet(evtDetail);
                    m_eventsChecklist.Add(evtDetail);
                }
            }

            var newF = new FlagDetail(0, 10, EventFlagType.SideEvent, "", "Complete the regional Kanto Pokédex");
            var newE = new EventDetail(newF);
            newE.IsDone = completedPokedex;
            m_eventsChecklist.Add(newE);

            newF = new FlagDetail(1, 10, EventFlagType.SideEvent, "", "Register the mythical Mew in the Pokédex");
            newE = new EventDetail(newF);
            newE.IsDone = isMewRegistered;
            m_eventsChecklist.Add(newE);

            newF = new FlagDetail(2, 10, EventFlagType.SideEvent, "", "Have a Hi-Score in the Pikachu's Beach minigame");
            newE = new EventDetail(newF);
            newE.IsDone = hasPikaBeachHiScore;
            m_eventsChecklist.Add(newE);

            // Reset S.S. Anne
            {
                //(m_savFile as IEventFlagArray).SetEventFlag(0x5E1, false);
                //(m_savFile as IEventFlagArray).SetEventFlag(0x5E2, false);
            }
        }

        public override bool SupportsEditingFlag(EventFlagType flagType)
        {
            switch (flagType)
            {
                case EventFlagType.FieldItem:
                case EventFlagType.HiddenItem:
                case EventFlagType.TrainerBattle:
                case EventFlagType.StaticBattle:
                case EventFlagType.InGameTrade:
                case EventFlagType.ItemGift:
                case EventFlagType.PkmnGift:
                    return true;

                default:
                    return false;
            }
        }

        public override void MarkFlags(EventFlagType flagType)
        {
            ChangeFlagsVal(flagType, value: true);
        }

        public override void UnmarkFlags(EventFlagType flagType)
        {
            ChangeFlagsVal(flagType, value: false);
        }

        void ChangeFlagsVal(EventFlagType flagType, bool value)
        {
            if (SupportsEditingFlag(flagType))
            {
                var flagHelper = (m_savFile as IEventFlagArray);

                foreach (var evt in m_eventsChecklist)
                {
                    if (evt.EvtTypeVal == flagType)
                    {
                        int idx = (int)evt.EvtId;

                        evt.IsDone = value;

                        switch (evt.EvtSource)
                        {
                            case Src_EventFlags:
                                flagHelper.SetEventFlag(idx, value);
                                break;

                            case Src_HideShowFlags:
                                m_savFile.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, value);
                                break;

                            case Src_HiddenItemFlags:
                                m_savFile.SetFlag(ObtainedHiddenItemsOffset + (idx >> 3), idx & 7, value);
                                break;

                            case Src_HiddenCoinsFlags:
                                m_savFile.SetFlag(ObtainedHiddenCoinsOffset + (idx >> 3), idx & 7, value);
                                break;

                            case Src_TradeFlags:
                                m_savFile.SetFlag(CompletedInGameTradeFlagsOffset + (idx >> 3), idx & 7, value);
                                break;

                            case Src_BadgesFlags:
                                m_savFile.SetFlag(BadgeFlagsOffset + (idx >> 3), idx & 7, value);
                                break;

                            case Src_Misc_wd728:
                                m_savFile.SetFlag(RodFlagsOffset + (idx >> 3), idx & 7, value);
                                break;

                            case Src_Misc_wd72e:
                                m_savFile.SetFlag(LaprasFlagOffset + (idx >> 3), idx & 7, value);
                                break;
                        }

                    }
                }
            }
        }

        protected override bool ShouldExportEvent(FlagDetail eventDetail)
        {
            if (eventDetail.FlagTypeVal == EventFlagType.GeneralEvent)
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

        protected override bool IsEvtSet(EventDetail evtDetail)
        {
            bool isEvtSet = false;
            int idx = (int)evtDetail.EvtId;

            switch (evtDetail.EvtSource)
            {
                case Src_EventFlags:
                    isEvtSet = (m_savFile as IEventFlagArray).GetEventFlag(idx);
                    break;

                case Src_HideShowFlags:
                    isEvtSet = m_savFile.GetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7);
                    break;

                case Src_HiddenItemFlags:
                    isEvtSet = m_savFile.GetFlag(ObtainedHiddenItemsOffset + (idx >> 3), idx & 7);
                    break;

                case Src_HiddenCoinsFlags:
                    isEvtSet = m_savFile.GetFlag(ObtainedHiddenCoinsOffset + (idx >> 3), idx & 7);
                    break;

                case Src_TradeFlags:
                    isEvtSet = m_savFile.GetFlag(CompletedInGameTradeFlagsOffset + (idx >> 3), idx & 7);
                    break;

                case Src_BadgesFlags:
                    isEvtSet = m_savFile.GetFlag(BadgeFlagsOffset + (idx >> 3), idx & 7);
                    break;

                case Src_Misc_wd728:
                    isEvtSet = m_savFile.GetFlag(RodFlagsOffset + (idx >> 3), idx & 7);
                    break;

                case Src_Misc_wd72e:
                    isEvtSet = m_savFile.GetFlag(LaprasFlagOffset + (idx >> 3), idx & 7);
                    break;

                default:
                    isEvtSet = false;
                    break;
            }

            return isEvtSet;
        }
    }

}
