using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal class FlagsGen5BW : FlagsOrganizer
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
                    for (int i = 0x380; i <= 0x437; ++i)
                        flagHelper.SetEventFlag(i, value);
                    break;

                case FlagType.FieldItem:
                    for (int i = 0x450; i <= 0x587; ++i)
                        flagHelper.SetEventFlag(i, value);
                    break;

                case FlagType.TrainerBattle:
                    for (int i = 0x588; i <= 0x7D7; ++i)
                        //flagHelper.SetEventFlag(i, value);
                        flagHelper.SetEventFlag(i, true);
                    break;
            }
        }

        protected override void CheckAllMissingFlags()
        {
            // Hidden Items

            // ? 0x350

            // - 0x380

            // - 0x437

            for (int i = 0; i < 256; ++i)
            {
                CheckMissingFlag(0x350 + i, FlagType.HiddenItem, "", i.ToString("D3"));
            }


            // Field items

            // - 0x450

            // - 0x587

            for (int i = 0; i < 256; ++i)
            {
                CheckMissingFlag(0x0450 + i, FlagType.FieldItem, "", i.ToString("D3"));
            }


            // Trainers (?? - 0x7D7)

            // - 0x588

            // - 0x7D7

            // ? 0x7F0

            for (int i = 0; i < 1000; ++i)
            {
                CheckMissingFlag(0x550 + i, FlagType.TrainerBattle, "", i.ToString("D3"));
            }
        }

    }

}
