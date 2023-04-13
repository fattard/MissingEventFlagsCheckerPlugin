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

        protected override void InitFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;
            m_battleTrainerStatus = (m_savFile as SAV8BS).BattleTrainer;

            if (s_flagsList_res == null)
            {
                s_flagsList_res = ReadFlagsListRes("flags_gen8bsbdsp.txt");
            }

            AssembleList(s_flagsList_res);
        }

        protected override void AssembleList(string flagsList_res)
        {
            var savEventFlags = (m_savFile as IEventFlagArray).GetEventFlags();
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

                        else if (flagDetail.FlagTypeVal == FlagType.TrainerBattle)
                        {
                            flagDetail.IsSet = m_battleTrainerStatus.GetIsWin(flagDetail.FlagIdx - savEventFlags.Length);
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
                            m_battleTrainerStatus.SetIsWin(f.FlagIdx, value);
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
                            flagHelper.SetEventFlag(f.FlagIdx, value);
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
