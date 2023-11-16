using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal class FlagsGen2C : EventFlagsOrganizer
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
