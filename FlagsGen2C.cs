using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal class FlagsGen2C : FlagsOrganizer
    {
        static string s_flagsList_res = null;

        enum FlagOffsets
        {
            CompletedInGameTradeFlags = 0x24EE
        }

        protected override void InitEventFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;

            // wTradeFlags
            bool[] result = new bool[8];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_savFile.GetFlag((int)FlagOffsets.CompletedInGameTradeFlags + (i >> 3), i & 7);
            }
            bool[] completedInGameTradeFlags = result;

#if DEBUG
            // Force refresh
            s_flagsList_res = null;
#endif

            if (s_flagsList_res == null)
            {
                s_flagsList_res = ReadResFile("flags_gen2c.txt");
            }

            int idxEventFlagsSection = s_flagsList_res.IndexOf("//\tEvent Flags");
            int idxTradeFlagsSection = s_flagsList_res.IndexOf("//\tTrade Flags");
            int idxEventWorkSection = s_flagsList_res.IndexOf("//\tEvent Work");


            AssembleList(s_flagsList_res.Substring(idxEventFlagsSection), 0, (m_savFile as IEventFlagArray).GetEventFlags());
            AssembleList(s_flagsList_res.Substring(idxTradeFlagsSection), 1, completedInGameTradeFlags);

            AssembleWorkList<byte>(s_flagsList_res.Substring(idxEventWorkSection));

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
        }

        public override bool SupportsEditingFlag(EventFlagType flagType)
        {
            switch (flagType)
            {
                case EventFlagType.FieldItem:
                case EventFlagType.HiddenItem:
                case EventFlagType.TrainerBattle:
                case EventFlagType.InGameTrade:
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

                foreach (var f in m_eventFlagsList)
                {
                    if (f.FlagTypeVal == flagType)
                    {
                        int fIdx = (int)f.FlagIdx;

                        f.IsSet = value;

                        switch (f.SourceIdx)
                        {
                            case 0: // EventFlags
                                flagHelper.SetEventFlag(fIdx, value);
                                break;

                            case 1: // TradeFlags
                                m_savFile.SetFlag((int)FlagOffsets.CompletedInGameTradeFlags + (fIdx >> 3), fIdx & 7, value);
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
                case 0: // EventFlags
                    isEvtSet = (m_savFile as IEventFlagArray).GetEventFlag(idx);
                    break;

                case 1: // TradeFlags
                    isEvtSet = m_savFile.GetFlag((int)FlagOffsets.CompletedInGameTradeFlags + (idx >> 3), idx & 7);
                    break;

                default:
                    isEvtSet = false;
                    break;
            }

            return isEvtSet;
        }
    }

}
