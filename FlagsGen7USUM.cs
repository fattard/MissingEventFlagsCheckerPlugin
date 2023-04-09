using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal class FlagsGen7USUM : FlagsOrganizer
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
                    for (int i = 0x9D8; i <= 0xBAF; ++i)
                        flagHelper.SetEventFlag(i, value);
                    break;

                case FlagType.TrainerBattle:
                    for (int i = 0xC00; i <= 0xE6F; ++i)
                        //flagHelper.SetEventFlag(i, value);
                        flagHelper.SetEventFlag(i, true);
                    break;
                
            }
        }

        protected override void CheckAllMissingFlags()
        {
            // items

            // - 0x9D8

            // - 0xBAF

            // ? 0xBDD
            // ? 0xBFF

            for (int i = 0x9D8; i <= 0xBAF; ++i)
            {
                CheckMissingFlag(i, FlagType.FieldItem, "", (i - 0x9D8).ToString("D3"));
            }


            // Trainers (??)

            // - 0xC00

            // - 0xE6F


            for (int i = 0xC00; i <= 0xE6F; ++i)
            {
                CheckMissingFlag(i, FlagType.TrainerBattle, "", (i - 0xC00).ToString("D3"));
            }


        }

    }

}
