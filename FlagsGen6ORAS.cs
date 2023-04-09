using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal class FlagsGen6ORAS : FlagsOrganizer
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

                case FlagType.HiddenItem:
                    for (int i = 0x448; i <= 0x4F8; ++i)
                        flagHelper.SetEventFlag(i, value);
                    break;

                case FlagType.FieldItem:
                    for (int i = 0x518; i <= 0x60F; ++i)
                        flagHelper.SetEventFlag(i, value);
                    break;

                case FlagType.TrainerBattle:
                    for (int i = 0x6D0; i <= 0xA0F; ++i)
                        //flagHelper.SetEventFlag(i, value);
                        flagHelper.SetEventFlag(i, true);
                    break;
                
            }
        }

        protected override void CheckAllMissingFlags()
        {
            // Hidden Items

            // - 0x448

            // - 0x4F8

            for (int i = 0x448; i <= 0x4F8; ++i)
            {
                CheckMissingFlag(i, FlagType.HiddenItem, "", (i - 0x448).ToString("D3"));
            }


            // Field items

            // - 0x518

            // - 0x60F

            for (int i = 0x518; i <= 0x60F; ++i)
            {
                CheckMissingFlag(i, FlagType.FieldItem, "", (i - 0x518).ToString("D3"));
            }


            // Trainers (??)

            // - 0x6D0

            // - 0xA0F


            for (int i = 0x6D0; i <= 0xA0F; ++i)
            {
                CheckMissingFlag(i, FlagType.TrainerBattle, "", (i - 0x6D0).ToString("D3"));
            }


        }

    }

}
