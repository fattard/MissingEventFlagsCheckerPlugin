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
                case FlagType.HiddenItem:
                    for (int i = 0xB00; i <= 0xBF7; ++i)
                        flagHelper.SetEventFlag(i, value);
                    break;

                case FlagType.FieldItem:
                    for (int i = 0x44C; i <= 0x5CB; ++i)
                        flagHelper.SetEventFlag(i, value);
                    break;

                case FlagType.TrainerBattle:
                    for (int i = 0x61B; i <= 0x909; ++i)
                        //flagHelper.SetEventFlag(i, value);
                        flagHelper.SetEventFlag(i, true);
                    break;
            }
        }

        protected override void CheckAllMissingFlags()
        {
            // Field items

            // 0x34D
            // 0x3AF

            // - 0x44C

            // - 0x5CB

            for (int i = 0; i < 256; ++i)
            {
                CheckMissingFlag(0x0450 + i, FlagType.FieldItem, "", i.ToString("D3"));
            }


            // Trainers (??)

            // - 0x61A

            // - 0x909

            // ~ 0x963

            for (int i = 0; i < 1000; ++i)
            {
                CheckMissingFlag(0x550 + i, FlagType.TrainerBattle, "", i.ToString("D3"));
            }


            // Hidden Items

            // - 0xB04

            // - 0xBD8

            // 0xBF7

            for (int i = 0; i < 256; ++i)
            {
                CheckMissingFlag(0x350 + i, FlagType.HiddenItem, "", i.ToString("D3"));
            }
        }

    }

}
