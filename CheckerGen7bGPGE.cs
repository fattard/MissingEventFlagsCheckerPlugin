namespace MissingEventFlagsCheckerPlugin
{
    internal class CheckerGen7bGPGE : EventFlagsChecker
    {
        static string? s_chkdb_res = null;

        EventWork7b? m_eventWorkData;

        protected override void InitData(SaveFile savFile)
        {
            m_savFile = savFile;
            m_eventWorkData = ((SAV7b)m_savFile).EventWork;

#if DEBUG
            // Force refresh
            s_chkdb_res = null;
#endif

            s_chkdb_res ??= ReadResFile("chkdb_gen7blgpe");

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
                    isEvtSet = m_eventWorkData!.GetFlag(idx);
                    break;

                default:
                    isEvtSet = false;
                    break;
            }

            return isEvtSet;
        }

    }

}
