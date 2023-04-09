using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal class FlagsGen6XY : FlagsOrganizer
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
                    for (int i = 0x448; i <= 0x4E8; ++i)
                        flagHelper.SetEventFlag(i, value);
                    break;

                case FlagType.FieldItem:
                    for (int i = 0x518; i <= 0x5E8; ++i)
                        flagHelper.SetEventFlag(i, value);
                    break;

                case FlagType.TrainerBattle:
                    for (int i = 0x6C0; i <= 0x928; ++i)
                        //flagHelper.SetEventFlag(i, value);
                        flagHelper.SetEventFlag(i, true);
                    break;
                
            }
        }

        protected override void CheckAllMissingFlags()
        {
            // Hidden Items

            // - 0x448

            // - 0x4E8

            for (int i = 0x448; i <= 0x4E8; ++i)
            {
                CheckMissingFlag(i, FlagType.HiddenItem, "", (i - 0x448).ToString("D3") + $" (Flag 0x{i.ToString("X3")})");
            }


            // Field items

            // - 0x518

            // - 0x5E8

            for (int i = 0x518; i <= 0x5E8; ++i)
            {
                CheckMissingFlag(i, FlagType.FieldItem, "", (i - 0x518).ToString("D3") + $" (Flag 0x{i.ToString("X3")})");
            }


            // Trainers (??)

            // - 0x6C0

            // - 0x928


            for (int i = 0x6C0; i <= 0x928; ++i)
            {
                CheckMissingFlag(i, FlagType.TrainerBattle, "", (i - 0x6C0).ToString("D3") + $" (Flag 0x{i.ToString("X3")})");
            }


        }

    }

}
