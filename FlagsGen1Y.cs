using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal class FlagsGen1Y : EventFlagsOrganizer
    {
        static string s_chkdb_res = null;

        enum FlagOffsets_INTL
        {
            BadgeFlags = 0x2602,
            MissableObjectFlags = 0x2852,
            GameProgressFlags = 0x289C,
            ObtainedHiddenItems = 0x299C,
            ObtainedHiddenCoins = 0x29AA,
            FlySpotFlags = 0x29B7,
            RodFlags = 0x29D4,
            LaprasFlag = 0x29DA,
            CompletedInGameTradeFlags = 0x29E3,
            EventFlags = 0x29F3,
        }

        enum FlagOffsets_JAP
        {
            BadgeFlags = 0x25F8,
            MissableObjectFlags = 0x2848,
            GameProgressFlags = 0x2892,
            ObtainedHiddenItems = 0x2992,
            ObtainedHiddenCoins = 0x29A0,
            FlySpotFlags = 0x29AD,
            RodFlags = 0x29CA,
            LaprasFlag = 0x29D0,
            CompletedInGameTradeFlags = 0x29D9,
            EventFlags = 0x29E9,
        }

        int BadgeFlagsOffset;
        int MissableObjectFlagsOffset;
        int GameProgressWorkOffset;
        int ObtainedHiddenItemsOffset;
        int ObtainedHiddenCoinsOffset;
        int FlySpotFlagsOffset;
        int RodFlagsOffset;
        int LaprasFlagOffset;
        int CompletedInGameTradeFlagsOffset;

        bool m_Dex_completedRegional;
        bool m_Dex_isMythicalRegistered_Mew;
        bool m_Dex_fullyCompleted;

        bool m_hasPikaBeachHiScore;

        const int Src_EventFlags = 0;
        const int Src_HideShowFlags = 1;
        const int Src_HiddenItemFlags = 2;
        const int Src_HiddenCoinsFlags = 3;
        const int Src_TradeFlags = 4;
        const int Src_FlySpotFlags = 5;
        const int Src_BadgesFlags = 6;
        const int Src_Misc_wd728 = 7;
        const int Src_Misc_wd72e = 8;
        const int Src_WorkArea = 9;

        const int Src_EventFlagsEX = 10;
        const int Src_HideShowFlagsEX = 11;
        const int Src_Dex = 12;

        const int Src_HiScore = 13;


        protected override void InitEventFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;

            var savFile_SAV1 = (m_savFile as SAV1);

            if (savFile_SAV1.Japanese)
            {
                BadgeFlagsOffset = (int)FlagOffsets_JAP.BadgeFlags;
                MissableObjectFlagsOffset = (int)FlagOffsets_JAP.MissableObjectFlags;
                GameProgressWorkOffset = (int)FlagOffsets_JAP.GameProgressFlags;
                ObtainedHiddenItemsOffset = (int)FlagOffsets_JAP.ObtainedHiddenItems;
                ObtainedHiddenCoinsOffset = (int)FlagOffsets_JAP.ObtainedHiddenCoins;
                FlySpotFlagsOffset = (int)FlagOffsets_JAP.FlySpotFlags;
                RodFlagsOffset = (int)FlagOffsets_JAP.RodFlags;
                LaprasFlagOffset = (int)FlagOffsets_JAP.LaprasFlag;
                CompletedInGameTradeFlagsOffset = (int)FlagOffsets_JAP.CompletedInGameTradeFlags;
            }
            else
            {
                BadgeFlagsOffset = (int)FlagOffsets_INTL.BadgeFlags;
                MissableObjectFlagsOffset = (int)FlagOffsets_INTL.MissableObjectFlags;
                GameProgressWorkOffset = (int)FlagOffsets_INTL.GameProgressFlags;
                ObtainedHiddenItemsOffset = (int)FlagOffsets_INTL.ObtainedHiddenItems;
                ObtainedHiddenCoinsOffset = (int)FlagOffsets_INTL.ObtainedHiddenCoins;
                FlySpotFlagsOffset = (int)FlagOffsets_INTL.FlySpotFlags;
                RodFlagsOffset = (int)FlagOffsets_INTL.RodFlags;
                LaprasFlagOffset = (int)FlagOffsets_INTL.LaprasFlag;
                CompletedInGameTradeFlagsOffset = (int)FlagOffsets_INTL.CompletedInGameTradeFlags;
            }

            // Check Pokedex
            m_Dex_completedRegional = true;
            m_Dex_isMythicalRegistered_Mew = false;
            for (ushort i = 001; i <= 151; i++)
            {
                switch (i)
                {
                    case 151: // Mew
                        m_Dex_isMythicalRegistered_Mew = savFile_SAV1.GetCaught(i);
                        break;

                    default:
                        if (!savFile_SAV1.GetCaught(i))
                        {
                            m_Dex_completedRegional = false;
                        }
                        break;
                }
            }

            m_Dex_fullyCompleted = m_Dex_completedRegional && m_Dex_isMythicalRegistered_Mew;
            
            // Check if has PikaBeach hi-score
            m_hasPikaBeachHiScore = (savFile_SAV1.PikaBeachScore > 0);

            m_flagsSourceInfo["EvtFlags"] = Src_EventFlags;
            m_flagsSourceInfo["HS-Flags"] = Src_HideShowFlags;
            m_flagsSourceInfo["HI-Flags"] = Src_HiddenItemFlags;
            m_flagsSourceInfo["HC-Flags"] = Src_HiddenCoinsFlags;
            m_flagsSourceInfo["TradeFlags"] = Src_TradeFlags;
            m_flagsSourceInfo["FlyFlags"] = Src_FlySpotFlags;
            m_flagsSourceInfo["Badges"] = Src_BadgesFlags;
            m_flagsSourceInfo["wd72e"] = Src_Misc_wd72e;
            m_flagsSourceInfo["wd728"] = Src_Misc_wd728;
            m_flagsSourceInfo["WorkArea"] = Src_WorkArea;
            m_flagsSourceInfo["EvtFlagsEX"] = Src_EventFlagsEX;
            m_flagsSourceInfo["HS-FlagsEX"] = Src_HideShowFlagsEX;
            m_flagsSourceInfo["Dex"] = Src_Dex;
            m_flagsSourceInfo["Score"] = Src_HiScore;
            m_flagsSourceInfo["-"] = -1;

#if DEBUG
            // Force refresh
            s_chkdb_res = null;
#endif

            if (s_chkdb_res == null)
            {
                s_chkdb_res = ReadResFile("chkdb_gen1y.txt");
            }

            ParseChecklist(s_chkdb_res);
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

                case Src_FlySpotFlags:
                    isEvtSet = m_savFile.GetFlag(FlySpotFlagsOffset + (idx >> 3), idx & 7);
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

                case Src_EventFlagsEX:
                    {
                        switch (idx)
                        {
                            case 0x356: // Choice: Hitmonlee / Hitmonchan
                                isEvtSet = (m_savFile as IEventFlagArray).GetEventFlag(0x356) ||
                                           (m_savFile as IEventFlagArray).GetEventFlag(0x357);
                                break;

                            case 0x578: // Choice: Dome Fossil / Helix Fossil
                                isEvtSet = (m_savFile as IEventFlagArray).GetEventFlag(0x578) ||
                                           (m_savFile as IEventFlagArray).GetEventFlag(0x57F);
                                break;

                            default:
                                isEvtSet = false;
                                break;
                        }
                    }
                    break;

                case Src_HideShowFlagsEX:
                    {
                        switch (idx)
                        {
                            case 0x8B: // Silph Scope
                                isEvtSet = m_savFile.GetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7) &&
                                           m_savFile.GetFlag(MissableObjectFlagsOffset + (0x85 >> 3), 0x85 & 7); // HS_ROCKET_HIDEOUT_B4F_GIOVANNI
                                break;

                            case 0x8C: // Lift Key
                                isEvtSet = m_savFile.GetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7) &&
                                           (m_savFile as IEventFlagArray).GetEventFlag(0x6A6); // EVENT_ROCKET_DROPPED_LIFT_KEY
                                break;

                            default:
                                isEvtSet = false;
                                break;
                        }
                    }
                    break;

                case Src_WorkArea:
                    {
                        switch (idx)
                        {
                            case 0x75: // S.S. Anne 2F
                                isEvtSet = m_savFile.Data[GameProgressWorkOffset + idx] == 0x4;
                                break;

                            default:
                                isEvtSet = false;
                                break;
                        }
                    }
                    break;

                case Src_Dex:
                    {
                        switch (idx)
                        {
                            case 0: // Regional complete
                                isEvtSet = m_Dex_completedRegional;
                                break;

                            case 1: // Mew
                                isEvtSet = m_Dex_isMythicalRegistered_Mew;
                                break;

                            case 2: // Fully completed
                                isEvtSet = m_Dex_fullyCompleted;
                                break;
                        }
                    }
                    break;

                case Src_HiScore:
                    isEvtSet = m_hasPikaBeachHiScore;
                    break;

                default:
                    isEvtSet = false;
                    break;
            }

            return isEvtSet;
        }
    }

}
