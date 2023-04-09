using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal class FlagsGen7SM : FlagsOrganizer
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
                    for (int i = 0x618; i <= 0x759; ++i)
                        flagHelper.SetEventFlag(i, value);
                    break;

                case FlagType.TrainerBattle:
                    for (int i = 0x818; i <= 0x9FF; ++i)
                        //flagHelper.SetEventFlag(i, value);
                        flagHelper.SetEventFlag(i, true);
                    break;
                
            }
        }

        protected override void CheckAllMissingFlags()
        {
            // items

            // - 0x618

            // - 0x759

            // ? 0x76D

            for (int i = 0x618; i <= 0x759; ++i)
            {
                CheckMissingFlag(i, FlagType.FieldItem, "", (i - 0x618).ToString("D3") + $" (Flag 0x{i.ToString("X3")})");
            }


            // Trainers (??)

            // - 0x818

            // - 0x9FF


            for (int i = 0x818; i <= 0x9FF; ++i)
            {
                CheckMissingFlag(i, FlagType.TrainerBattle, "", (i - 0x818).ToString("D3") + $" (Flag 0x{i.ToString("X3")})");
            }


        }

    }

}
