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

        bool[] m_eventFlags;
        bool[] m_obtainedHiddenCoinsFlags;
        bool[] m_obtainedHiddenItemsFlags;
        bool[] m_missableObjectFlags;
        bool[] m_completedInGameTradeFlags;
        bool[] m_miscFlags;
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


            // Misc flags
            result = new bool[] { m_gotLaprasFlag };
            m_miscFlags = result;

            m_eventFlagsList.Clear();

            //AssembleFlagsList();

#if DEBUG
            // Force refresh
            s_flagsList_res = null;
#endif

            if (s_flagsList_res == null)
            {
                s_flagsList_res = ReadFlagsListRes("flags_gen1y.txt");
            }

            int idxEventFlagsSection = s_flagsList_res.IndexOf("//\tEvent Flags");
            int idxHideShowSection = s_flagsList_res.IndexOf("//\tHide-Show Flags");
            int idxHiddenItemsSection = s_flagsList_res.IndexOf("//\tHidden Items Flags");
            int idxHiddenCoinsSection = s_flagsList_res.IndexOf("//\tHidden Coins Flags");
            int idxTradesSection = s_flagsList_res.IndexOf("//\tIn-Game Trades Flags");
            int idxMiscSection = s_flagsList_res.IndexOf("//\tMisc");


            AssembleList(s_flagsList_res.Substring(idxHideShowSection), m_missableObjectFlags);
            AssembleList(s_flagsList_res.Substring(idxHiddenItemsSection), m_obtainedHiddenItemsFlags);
            AssembleList(s_flagsList_res.Substring(idxHiddenCoinsSection), m_obtainedHiddenCoinsFlags);
            AssembleList(s_flagsList_res.Substring(idxTradesSection), m_completedInGameTradeFlags);
            AssembleList(s_flagsList_res.Substring(idxEventFlagsSection), m_eventFlags);
            AssembleList(s_flagsList_res.Substring(idxMiscSection), m_miscFlags);
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
