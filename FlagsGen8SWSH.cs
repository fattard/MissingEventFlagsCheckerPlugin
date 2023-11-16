using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal class FlagsGen8SWSH : EventFlagsOrganizer
    {
        static string s_chkdb_res = null;

        protected override void InitEventFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;

#if DEBUG
            // Force refresh
            s_chkdb_res = null;
#endif

            if (s_chkdb_res == null)
            {
                s_chkdb_res = ReadResFile("chkdb_gen8swsh.txt");
            }

            m_flagsSourceInfo["0"] = 0;
            m_flagsSourceInfo["-"] = -1;

            ParseChecklist(s_chkdb_res);
        }

        protected override bool IsEvtSet(EventDetail evtDetail)
        {
            bool isEvtSet = false;
            ulong idx = (uint)evtDetail.EvtId;
            var savEventBlocks = (m_savFile as ISCBlockArray).Accessor;

            switch (evtDetail.EvtSource)
            {
                case 0: // Bool blocks
                    isEvtSet = (savEventBlocks.GetBlockSafe((uint)idx).Type == SCTypeCode.Bool2);
                    break;

                default:
                    isEvtSet = false;
                    break;
            }

            return isEvtSet;
        }

    }

}
