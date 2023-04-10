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

        protected override void InitFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;
            m_eventFlags = (m_savFile as IEventFlagArray).GetEventFlags();

            if (s_flagsList_res == null)
            {
                s_flagsList_res = ReadFlagsListRes("flags_gen7blgpe.txt");
            }

            m_missingEventFlagsList.Clear();
            using (System.IO.StringReader reader = new System.IO.StringReader(s_flagsList_res))
            {
                string s = reader.ReadLine();
                do
                {
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        var flagDetail = new FlagDetail(s);
                        flagDetail.IsSet = m_eventFlags[flagDetail.FlagIdx];
                        if (ShouldIncludeEvent(flagDetail))
                        {
                            m_missingEventFlagsList.Add(flagDetail);
                        }
                    }

                    s = reader.ReadLine();

                } while (s != null);
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
            var flagHelper = (m_savFile as IEventFlagArray);

            string typeTxt = GetFlagTypeText(flagType);

            foreach (var f in m_missingEventFlagsList)
            {
                if (f.FlagTypeTxt == typeTxt)
                {
                    flagHelper.SetEventFlag(f.FlagIdx, value);
                }
            }

        }

        protected bool ShouldIncludeEvent(FlagDetail flagDetail)
        {
            switch (flagDetail.FlagTypeTxt)
            {
                case "":
                case "_UNUSED":
                    return false;


                case "EVENT":
                    switch (flagDetail.FlagIdx)
                    {
                        default:
                            return false;
                    }


                default:
                    return true;
            }
        }

        protected override void CheckAllMissingFlags()
        {
            // Do nothing
        }

    }

}
