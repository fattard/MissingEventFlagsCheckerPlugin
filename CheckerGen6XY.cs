namespace MissingEventFlagsCheckerPlugin
{
    internal class CheckerGen6XY : EventFlagsChecker
    {
        static string? s_chkdb_res = null;

        protected override void InitData(SaveFile savFile)
        {
            m_savFile = savFile;

#if DEBUG
            // Force refresh
            s_chkdb_res = null;
#endif

            s_chkdb_res ??= ReadResFile("chkdb_gen6xy");

            m_flagsSourceInfo["0"] = 0;
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
                    isEvtSet = ((IEventFlagProvider37)m_savFile!).EventWork.GetEventFlag(idx);
                    break;

                default:
                    isEvtSet = false;
                    break;
            }

            return isEvtSet;
        }

    }

}
