using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal class FlagsGen4HGSS : FlagsOrganizer
    {

        protected override void InitFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;
            //m_eventFlags = (m_savFile as IEventFlagArray).GetEventFlags();
            m_eventFlagsList.Clear();
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
                    for (int i = 0; i < 256; ++i)
                        flagHelper.SetEventFlag(0x320 + i, value);
                    break;

                case FlagType.FieldItem:
                    for (int i = 0; i < 256; ++i)
                        flagHelper.SetEventFlag(0x420 + i, value);
                    break;

                case FlagType.TrainerBattle:
                    for (int i = 0; i < 1040; ++i)
                        flagHelper.SetEventFlag(0x550 + i, value);
                    break;
            }
        }

        protected override void CheckAllMissingFlags()
        {
            // Hidden Items
            for (int i = 0; i < 256; ++i)
            {
                CheckMissingFlag(0x320 + i, FlagType.HiddenItem, "", i.ToString("D3") + $" (Flag 0x{(i + 0x320).ToString("X3")})");
            }


            // Field items
            for (int i = 0; i < 256; ++i)
            {
                CheckMissingFlag(0x420 + i, FlagType.FieldItem, "", i.ToString("D3") + $" (Flag 0x{(i + 0x420).ToString("X3")})");
            }


            // Trainers (??)
            for (int i = 0; i < 1040; ++i)
            {
                CheckMissingFlag(0x550 + i, FlagType.TrainerBattle, "", i.ToString("D3") + $" (Flag 0x{(i + 0x550).ToString("X3")})");
            }
        }

    }

}
