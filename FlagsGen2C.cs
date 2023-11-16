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
        static string s_chkdb_res = null;

        enum FlagOffsets
        {
            CompletedInGameTradeFlags = 0x24EE
        }

        protected override void InitEventFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;

#if DEBUG
            // Force refresh
            s_chkdb_res = null;
#endif

            if (s_chkdb_res == null)
            {
                s_chkdb_res = ReadResFile("chkdb_gen2c.txt");
            }

            m_flagsSourceInfo["0"] = 0;
            m_flagsSourceInfo["1"] = 1; // Trade flags
            m_flagsSourceInfo["-"] = -1;

            ParseChecklist(s_chkdb_res);
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

                foreach (var evt in m_eventsChecklist)
                {
                    if (evt.EvtTypeVal == flagType)
                    {
                        int fIdx = (int)evt.EvtId;

                        evt.IsDone = value;

                        switch (evt.EvtSource)
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
