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
            var savEventFlags = (m_savFile as IEventFlagArray).GetEventFlags();
            m_eventFlagsList.Clear();

            for (int idx = 0; idx < savEventFlags.Length; ++idx)
            {
                // FlagType.FieldItem:
                // ? 0x34D
                // ? 0x3AF

                // - 0x448

                // - 0x5CF

                if (idx >= 0x448 && idx < 0x5D0)
                {
                    int i = idx - 0x448;
                    m_eventFlagsList.Add(new FlagDetail(idx, FlagType.FieldItem, "", i.ToString("D3") + $" (Flag 0x{(idx).ToString("X3")})") { IsSet = savEventFlags[idx] });
                }

                // FlagType.TrainerBattle:
                // - 0x618

                // - 0x90F

                // ~ 0x963

                else if (idx >= 0x618 && idx < 0x910)
                {
                    int i = idx - 0x618;
                    m_eventFlagsList.Add(new FlagDetail(idx, FlagType.TrainerBattle, "", i.ToString("D3") + $" (Flag 0x{(idx).ToString("X3")})") { IsSet = savEventFlags[idx] });
                }

                // FlagType.HiddenItem:
                // - 0xB00

                // - 0xBDF

                // 0xBF7

                else if (idx >= 0xB00 && idx < 0xBE0)
                {
                    int i = idx - 0xB00;
                    m_eventFlagsList.Add(new FlagDetail(idx, FlagType.HiddenItem, "", i.ToString("D3") + $" (Flag 0x{(idx).ToString("X3")})") { IsSet = savEventFlags[idx] });
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
