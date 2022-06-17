using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal class FlagsGen4Pt : FlagsOrganizer
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
                    for (int i = 0; i < 256 + 28; ++i)
                        flagHelper.SetEventFlag(0x2DA + i, value);
                    break;

                case FlagType.FieldItem:
                    for (int i = 0; i < 256 + 72; ++i)
                        flagHelper.SetEventFlag(0x3F6 + i, value);
                    break;

                case FlagType.TrainerBattle:
                    for (int i = 0; i < 1024; ++i)
                        flagHelper.SetEventFlag(0x550 + i, value);
                    break;
            }
        }

        protected override void CheckAllMissingFlags()
        {
            // Hidden Items
            for (int i = 0; i < 256+28; ++i)
            {
                CheckMissingFlag(0x2DA + i, FlagType.HiddenItem, "", i.ToString("X"));
            }


            // Normal items
            for (int i = 0; i < 256+72; ++i)
            {
                CheckMissingFlag(0x3F6 + i, FlagType.FieldItem, "", i.ToString("X"));
            }


            // Trainers (?? 928)
            for (int i = 0; i < 1024; ++i)
            {
                CheckMissingFlag(0x550 + i, FlagType.TrainerBattle, "", i.ToString("X"));
            }
        }

    }

}
