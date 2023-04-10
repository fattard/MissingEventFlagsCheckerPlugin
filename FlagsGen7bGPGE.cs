using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal class FlagsGen7bGPGE : FlagsOrganizer
    {

        protected override void InitFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;
            m_eventFlags = (m_savFile as IEventFlagArray).GetEventFlags();
            m_missingEventFlagsList.Clear();
        }

        public override bool SupportsEditingFlag(FlagType flagType)
        {
            switch (flagType)
            {
                //case FlagType.FieldItem:
                //case FlagType.HiddenItem:
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
            var flagHelper = (m_savFile as IEventFlagArray);

            switch (flagType)
            {

                /*case FlagType.FieldItem:
                    for (int i = 0x2AA; i <= 0x3FE; ++i)
                        flagHelper.SetEventFlag(i, value);
                    break;*/

                case FlagType.TrainerBattle:
                    for (int i = 0xAB0; i <= 0xCD4; ++i)
                        flagHelper.SetEventFlag(i, value);
                    break;
                
            }
        }

        protected override void CheckAllMissingFlags()
        {
            // Events

            // - 0x286

            // - 0x3FE

            // ? 0x409

            /*for (int i = 0x286; i <= 0x5E8; ++i)
            {
                CheckMissingFlag(i, FlagType.GeneralEvent, "", (i - 0x518).ToString("D3") + $" (Flag 0x{i.ToString("X3")})");
            }*/


            // Trainers (??)

            // - 0xAB0

            // - 0xC43

            // Master Trainers
            // - 0xC44

            // - 0xCD4


            for (int i = 0xAB0; i <= 0xCD4; ++i)
            {
                CheckMissingFlag(i, FlagType.TrainerBattle, "", (i - 0xAB0).ToString("D3") + $" (Flag 0x{i.ToString("X3")})");
            }


        }

    }

}
