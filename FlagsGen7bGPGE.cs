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
        static string s_flagsList_res = null;

        EventWork7b m_eventWorkData;

        protected override void InitFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;
            m_eventWorkData = (m_savFile as SAV7b).EventWork;

#if DEBUG
            // Force refresh
            s_flagsList_res = null;
#endif

            if (s_flagsList_res == null)
            {
                s_flagsList_res = ReadFlagsListRes("flags_gen7blgpe.txt");
            }

            AssembleList(s_flagsList_res);

            // AssembleWorkList<int>
            m_eventWorkList.Clear();
            for (uint i = 0; i < m_eventWorkData.CountWork; i++)
            {
                var workDetail = new WorkDetail(i, FlagType._Unknown, "");
                workDetail.Value = Convert.ToInt64(m_eventWorkData.GetWork((int)workDetail.WorkIdx));
                m_eventWorkList.Add(workDetail);
            }
        }

        public override bool SupportsEditingFlag(FlagType flagType)
        {
            switch (flagType)
            {
                case FlagType.FieldItem:
                case FlagType.HiddenItem:
                case FlagType.TrainerBattle:
                case FlagType.InGameTrade:
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
                        flagHelper.SetEventFlag((int)f.FlagIdx, value);
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
