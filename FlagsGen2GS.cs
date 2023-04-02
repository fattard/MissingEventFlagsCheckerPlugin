using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal class FlagsGen2GS : FlagsOrganizer
    {
        static string s_flagsList_res = null;

        protected override void InitFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;
            m_eventFlags = (m_savFile as IEventFlagArray).GetEventFlags();

            if (s_flagsList_res == null)
            {
                s_flagsList_res = ReadFlagsListRes("flags_gen2gs.txt");
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


        public override void MarkFlags(FlagType flagType)
        {
            ChangeFlagsVal(flagType, value:true);
        }

        public override void UnmarkFlags(FlagType flagType)
        {
            ChangeFlagsVal(flagType, value:false);
        }


        void ChangeFlagsVal(FlagType flagType, bool value)
        {
            var flagHelper = (m_savFile as IEventFlagArray);

            switch (flagType)
            {
                case FlagType.FieldItem:
                    // Johto items
                    for (int i = 0x643; i <= 0x6BC; ++i)
                        flagHelper.SetEventFlag(i, value);
                    // Kanto items
                    for (int i = 0x77D; i <= 0x78B; ++i)
                        flagHelper.SetEventFlag(i, value);
                    break;

                case FlagType.HiddenItem:
                    // Johto hidden items
                    for (int i = 0x07D; i <= 0x0B8; ++i)
                        flagHelper.SetEventFlag(i, value);
                    // Kanto hidden items
                    for (int i = 0x0E4; i <= 0x0FE; ++i)
                        flagHelper.SetEventFlag(i, value);
                    break;

                case FlagType.TrainerBattle:
                    // Trainers
                    for (int i = 0x3E8; i <= 0x5BC; ++i)
                        flagHelper.SetEventFlag(i, value);
                    break;
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
