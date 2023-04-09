using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal class FlagsGen5B2W2 : FlagsOrganizer
    {

        protected override void InitFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;
            m_eventFlags = (m_savFile as IEventFlagArray).GetEventFlags();
            m_missingEventFlagsList.Clear();
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

                case FlagType.FieldItem:
                    for (int i = 0x448; i <= 0x5CF; ++i)
                        flagHelper.SetEventFlag(i, value);
                    break;

                case FlagType.TrainerBattle:
                    for (int i = 0x618; i <= 0x90F; ++i)
                        //flagHelper.SetEventFlag(i, value);
                        flagHelper.SetEventFlag(i, true);
                    break;
                
                case FlagType.HiddenItem:
                    for (int i = 0xB00; i <= 0xBDF; ++i)
                        flagHelper.SetEventFlag(i, value);
                    break;
            }
        }

        protected override void CheckAllMissingFlags()
        {
            // Field items

            // ? 0x34D
            // ? 0x3AF

            // - 0x448

            // - 0x5CF

            for (int i = 0x448; i <= 0x5CF; ++i)
            {
                CheckMissingFlag(i, FlagType.FieldItem, "", (i - 0x448).ToString("D3"));
            }


            // Trainers (??)

            // - 0x618

            // - 0x90F

            // ~ 0x963

            for (int i = 0x618; i <= 0x90F; ++i)
            {
                CheckMissingFlag(i, FlagType.TrainerBattle, "", (i - 0x618).ToString("D3"));
            }


            // Hidden Items

            // - 0xB00

            // - 0xBDF

            // 0xBF7

            for (int i = 0xB00; i <= 0xBDF; ++i)
            {
                CheckMissingFlag(i, FlagType.HiddenItem, "", (i - 0xB00).ToString("D3"));
            }
        }

    }

}
