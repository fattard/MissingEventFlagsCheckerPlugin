namespace MissingEventFlagsCheckerPlugin
{
    internal class CheckerGen8BDSP : EventFlagsChecker
    {
        static string? s_chkdb_res = null;

        BattleTrainerStatus8b? m_battleTrainerStatus;
        FlagWork8b? m_flagWork;

        const int Src_EventFlags = 0;
        const int Src_SysFlags = 1;
        const int Src_TrainerFlags = 2;

        protected override void InitData(SaveFile savFile)
        {
            m_savFile = savFile;
            m_battleTrainerStatus = ((SAV8BS)m_savFile).BattleTrainer;
            m_flagWork = ((SAV8BS)m_savFile).FlagWork;

#if DEBUG
            // Force refresh
            s_chkdb_res = null;
#endif

            s_chkdb_res ??= ReadResFile("chkdb_gen8bdsp");

            m_flagsSourceInfo["EvtFlags"] = Src_EventFlags;
            m_flagsSourceInfo["SysFlags"] = Src_SysFlags;
            m_flagsSourceInfo["TRFlags"] = Src_TrainerFlags;
            m_flagsSourceInfo["-"] = -1;

            ParseChecklist(s_chkdb_res);
        }

        protected override bool IsEvtSet(EventDetail evtDetail)
        {
            bool isEvtSet = false;
            int idx = (int)evtDetail.EvtId;

            switch (evtDetail.EvtSource)
            {
                case Src_EventFlags:
                    isEvtSet = m_flagWork!.GetFlag(idx);
                    break;

                case Src_SysFlags:
                    isEvtSet = m_flagWork!.GetSystemFlag(idx);
                    break;

                case Src_TrainerFlags:
                    isEvtSet = m_battleTrainerStatus!.GetIsWin(idx);
                    break;

                default:
                    isEvtSet = false;
                    break;
            }

            return isEvtSet;
        }

    }

}
