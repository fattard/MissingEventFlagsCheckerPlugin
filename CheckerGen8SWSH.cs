namespace MissingEventFlagsCheckerPlugin
{
    internal class CheckerGen8SWSH : EventFlagsChecker
    {
        static string? s_chkdb_res = null;

        protected override void InitData(SaveFile savFile)
        {
            m_savFile = savFile;

#if DEBUG
            // Force refresh
            s_chkdb_res = null;
#endif

            s_chkdb_res ??= ReadResFile("chkdb_gen8swsh");

            m_flagsSourceInfo["0"] = 0;
            m_flagsSourceInfo["-"] = -1;

            ParseChecklist(s_chkdb_res);
        }

        protected override bool IsEvtSet(EventDetail evtDetail)
        {
            bool isEvtSet = false;
            ulong idx = (uint)evtDetail.EvtId;
            var savEventBlocks = ((ISCBlockArray)m_savFile!).Accessor;

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
