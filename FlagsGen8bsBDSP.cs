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
        int sysFlagStart;
        int trainerFlagStart;

        protected override void InitFlagsData(SaveFile savFile)
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
                s_flagsList_res = ReadFlagsListRes("flags_gen8bsbdsp.txt");
            }

            AssembleList(s_flagsList_res);
            AssembleWorkList<int>(null);
        }

        protected override void AssembleList(string flagsList_res, bool[] customFlagValues = null)
        {
            var savEventFlags = (m_savFile as IEventFlagArray).GetEventFlags();
            
            var sysFlags = new bool[m_flagWork.CountSystem];

            sysFlagStart = m_flagWork.CountFlag;
            trainerFlagStart = m_flagWork.CountFlag + m_flagWork.CountSystem;

            m_eventFlagsList.Clear();
            using (System.IO.StringReader reader = new System.IO.StringReader(flagsList_res))
            {
                string s = reader.ReadLine();
                do
                {
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        var flagDetail = new FlagDetail(s);

                        if (flagDetail.FlagIdx < savEventFlags.Length)
                        {
                            flagDetail.IsSet = savEventFlags[flagDetail.FlagIdx];
                        }

                        else if (flagDetail.FlagIdx < (trainerFlagStart))
                        {
                            flagDetail.IsSet = m_flagWork.GetSystemFlag((int)flagDetail.FlagIdx - sysFlagStart);
                        }

                        else if (flagDetail.FlagTypeVal == FlagType.TrainerBattle)
                        {
                            flagDetail.IsSet = m_battleTrainerStatus.GetIsWin((int)flagDetail.FlagIdx - trainerFlagStart);
                        }
                        
                        m_eventFlagsList.Add(flagDetail);
                    }

                    s = reader.ReadLine();

                } while (s != null);
            }
        }

        public override bool SupportsEditingFlag(FlagType flagType)
        {
            switch (flagType)
            {
                case FlagType.FieldItem:
                case FlagType.HiddenItem:
                case FlagType.TrainerBattle:
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
            if (SupportsEditingFlag(flagType))
            {
                // Trainer status
                if (flagType == FlagType.TrainerBattle)
                {
                    foreach (var f in m_eventFlagsList)
                    {
                        if (f.FlagTypeVal == flagType)
                        {
                            f.IsSet = value;
                            m_battleTrainerStatus.SetIsWin((int)f.FlagIdx - trainerFlagStart, value);
                        }
                    }
                }

                // Common event flags
                else
                {
                    var flagHelper = (m_savFile as IEventFlagArray);

                    foreach (var f in m_eventFlagsList)
                    {
                        if (f.FlagTypeVal == flagType)
                        {
                            f.IsSet = value;
                            if (f.FlagIdx >= flagHelper.EventFlagCount)
                            {
                                m_flagWork.SetSystemFlag((int)f.FlagIdx - m_flagWork.CountFlag, value);
                            }

                            else
                            {
                                flagHelper.SetEventFlag((int)f.FlagIdx, value);
                            }
                        }
                    }
                }
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
