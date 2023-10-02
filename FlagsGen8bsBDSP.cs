using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal class FlagsGen8bsBDSP : FlagsOrganizer
    {
        static string s_flagsList_res = null;

        BattleTrainerStatus8b m_battleTrainerStatus;
        FlagWork8b m_flagWork;

        const int Src_EventFlags = 0;
        const int Src_SysFlags = 1;
        const int Src_TrainerFlags = 2;

        protected override void InitEventFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;
            m_battleTrainerStatus = (m_savFile as SAV8BS).BattleTrainer;
            m_flagWork = (m_savFile as SAV8BS).FlagWork;

#if DEBUG
            // Force refresh
            s_flagsList_res = null;
#endif

            if (s_flagsList_res == null)
            {
                s_flagsList_res = ReadResFile("flags_gen8bsbdsp.txt");
            }

            int idxEventFlagsSection = s_flagsList_res.IndexOf("//\tEvent Flags");
            int idxSysFlagsSection = s_flagsList_res.IndexOf("//\tSys Flags");
            int idxTrainerFlagsSection = s_flagsList_res.IndexOf("//\tTrainer Flags");
            int idxEventWorkSection = s_flagsList_res.IndexOf("//\tEvent Work");

            var sysFlagsVals = new bool[m_flagWork.CountSystem];
            for (int i = 0; i < sysFlagsVals.Length; i++)
            {
                sysFlagsVals[i] = m_flagWork.GetSystemFlag(i);
            }

            var battleTrainerVals = new bool[707];
            for (int i = 0; i < battleTrainerVals.Length; i++)
            {
                battleTrainerVals[i] = m_battleTrainerStatus.GetIsWin(i);
            }

            bool[] eventFlags = (m_savFile as IEventFlagArray).GetEventFlags();

            AssembleList(s_flagsList_res.Substring(idxEventFlagsSection), Src_EventFlags, eventFlags);
            AssembleList(s_flagsList_res.Substring(idxSysFlagsSection), Src_SysFlags, sysFlagsVals);
            AssembleList(s_flagsList_res.Substring(idxTrainerFlagsSection), Src_TrainerFlags, battleTrainerVals);

            AssembleWorkList<int>(s_flagsList_res.Substring(idxEventWorkSection));

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
                foreach (var evt in m_eventsChecklist)
                {
                    if (evt.EvtTypeVal == flagType)
                    {
                        int idx = (int)evt.EvtId;

                        evt.IsDone = value;

                        switch (evt.EvtSource)
                        {
                            case Src_EventFlags:
                                m_flagWork.SetFlag(idx, value);
                                break;

                            case Src_SysFlags:
                                m_flagWork.SetSystemFlag(idx, value);
                                break;

                            case Src_TrainerFlags:
                                m_battleTrainerStatus.SetIsWin(idx, value);
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
                case Src_EventFlags:
                    isEvtSet = m_flagWork.GetFlag(idx);
                    break;

                case Src_SysFlags:
                    isEvtSet = m_flagWork.GetSystemFlag(idx);
                    break;

                case Src_TrainerFlags:
                    isEvtSet = m_battleTrainerStatus.GetIsWin(idx);
                    break;

                default:
                    isEvtSet = false;
                    break;
            }

            return isEvtSet;
        }

    }

}
