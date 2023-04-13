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
            var savEventFlags = (m_savFile as IEventFlagArray).GetEventFlags();
            m_eventFlagsList.Clear();

            for (int idx = 0; idx < savEventFlags.Length; ++idx)
            {
                // items:
                // - 0x9D8

                // - 0xBAF

                // ? 0xBDD
                // ? 0xBFF

                if (idx >= 0x9D8 && idx < 0xBB0)
                {
                    int i = idx - 0x9D8;
                    m_eventFlagsList.Add(new FlagDetail(idx, FlagType.FieldItem, "", i.ToString("D3") + $" (Flag 0x{(idx).ToString("X3")})") { IsSet = savEventFlags[idx] });
                }

                // FlagType.TrainerBattle:
                // - 0xC00

                // - 0xE6F

                else if (idx >= 0xC00 && idx < 0xE70)
                {
                    int i = idx - 0xC00;
                    m_eventFlagsList.Add(new FlagDetail(idx, FlagType.TrainerBattle, "", i.ToString("D3") + $" (Flag 0x{(idx).ToString("X3")})") { IsSet = savEventFlags[idx] });
                }

                // Misc
                else
                {
                    m_eventFlagsList.Add(new FlagDetail(idx, FlagType._Unknown, "", "") { IsSet = savEventFlags[idx] });
                }
            }

        }

        public override bool SupportsEditingFlag(FlagType flagType)
        {
            switch (flagType)
            {
                case FlagType.FieldItem:
                case FlagType.HiddenItem:
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
            if (SupportsEditingFlag(flagType))
            {
                var flagHelper = (m_savFile as IEventFlagArray);

                foreach (var f in m_eventFlagsList)
                {
                    if (f.FlagTypeVal == flagType)
                    {
                        f.IsSet = value;
                        flagHelper.SetEventFlag(f.FlagIdx, value);
                    }
                }
            }
        }

        protected override bool ShouldExportEvent(FlagDetail eventDetail)
        {
            if (eventDetail.FlagTypeVal == FlagType.GeneralEvent)
            {
                bool shouldInclude = false;

                switch (eventDetail.FlagIdx)
                {
                    default:
                        shouldInclude = false;
                        break;
                }

                return shouldInclude;
            }
            else
            {
                return base.ShouldExportEvent(eventDetail);
            }
        }

    }

}
