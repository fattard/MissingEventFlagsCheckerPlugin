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
        BattleTrainerStatus8b m_battleTrainerStatus;


        protected override void InitFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;
            //m_eventFlags = (m_savFile as IEventFlagArray).GetEventFlags();
            m_eventFlagsList.Clear();
            m_battleTrainerStatus = (m_savFile as SAV8BS).BattleTrainer;
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
            var flagHelper = (m_savFile as IEventFlagArray);

            switch (flagType)
            {

                case FlagType.HiddenItem:
                    for (int i = 0x25B; i <= 0x35A; ++i)
                        flagHelper.SetEventFlag(i, value);
                    break;

                case FlagType.FieldItem:
                    for (int i = 0x35B; i <= 0x45A; ++i)
                        flagHelper.SetEventFlag(i, value);
                    break;

                case FlagType.TrainerBattle:
                    for (int i = 0; i < 707; ++i)
                        m_battleTrainerStatus.SetIsWin(i, value);
                    break;
                
            }
        }

        protected override void CheckAllMissingFlags()
        {
            // Hidden Items

            // - 0x25B

            // - 0x35A


            for (int i = 0x25B; i <= 0x35A; ++i)
            {
                CheckMissingFlag(i, FlagType.HiddenItem, "", (i - 0x25B).ToString("D3") + $" (Flag 0x{i.ToString("X3")})");
            }


            // Field items

            // - 0x35B

            // - 0x45A

            for (int i = 0x35B; i <= 0x45A; ++i)
            {
                CheckMissingFlag(i, FlagType.FieldItem, "", (i - 0x35B).ToString("D3") + $" (Flag 0x{i.ToString("X3")})");
            }


            // Trainers (??)

            for (int i = 0; i < 707; ++i)
            {
                if (isAssembleChecklist)
                {
                    m_eventFlagsList.Add(new FlagDetail(i, FlagType.TrainerBattle, "", (i).ToString("D3")) { IsSet = m_battleTrainerStatus.GetIsWin(i) });
                }

                else if (m_battleTrainerStatus.GetIsWin(i))
                {
                    m_eventFlagsList.Add(new FlagDetail(i, FlagType.TrainerBattle, "", (i).ToString("D3")));
                }
            }


        }

    }

}
