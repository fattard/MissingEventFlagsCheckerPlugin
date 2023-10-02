using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal class FlagsGen8SWSH : FlagsOrganizer
    {
        static string s_flagsList_res = null;

        protected override void InitEventFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;

#if DEBUG
            // Force refresh
            s_flagsList_res = null;
#endif

            if (s_flagsList_res == null)
            {
                s_flagsList_res = ReadResFile("flags_gen8swsh.txt");
            }

            AssembleList(s_flagsList_res);

            //TEMP
            m_eventsChecklist.Clear();
            foreach (var flagDetail in m_eventFlagsList)
            {
                if (ShouldExportEvent(flagDetail))
                {
                    var evtDetail = new EventDetail(flagDetail);
                    evtDetail.IsDone = IsEvtSet(evtDetail);
                    m_eventsChecklist.Add(evtDetail);
                }
            }
        }

        protected override void AssembleList(string flagsList_res, bool[] customFlagValues = null)
        {
            var savEventBlocks = (m_savFile as ISCBlockArray).Accessor;
            m_eventFlagsList.Clear();
            using (System.IO.StringReader reader = new System.IO.StringReader(flagsList_res))
            {
                string s = reader.ReadLine();
                do
                {
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        var flagDetail = new FlagDetail(s);
                        flagDetail.IsSet = (savEventBlocks.GetBlockSafe((uint)flagDetail.FlagIdx).Type == SCTypeCode.Bool2);
                        m_eventFlagsList.Add(flagDetail);
                    }

                    s = reader.ReadLine();

                } while (s != null);
            }
        }

        public override bool SupportsEditingFlag(EventFlagType flagType)
        {
            switch (flagType)
            {
                case EventFlagType.FieldItem:
                case EventFlagType.HiddenItem:
                case EventFlagType.TrainerBattle:
                    return true;

                default:
                    return false;
            }
        }

        public override void MarkFlags(EventFlagType flagType)
        {
            ChangeFlagsVal(flagType, value: true);
        }

        public override void UnmarkFlags(EventFlagType flagType)
        {
            ChangeFlagsVal(flagType, value: false);
        }

        void ChangeFlagsVal(EventFlagType flagType, bool value)
        {
            if (SupportsEditingFlag(flagType))
            {
                var blocks = (m_savFile as ISCBlockArray).Accessor;

                foreach (var f in m_eventFlagsList)
                {
                    if (f.FlagTypeVal == flagType)
                    {
                        f.IsSet = value;
                        blocks.GetBlockSafe((uint)f.FlagIdx).ChangeBooleanType(value ? SCTypeCode.Bool2 : SCTypeCode.Bool1);
                    }
                }
            }
        }

        public override void DumpAllFlags()
        {
            StringBuilder sb = new StringBuilder(100 * 1024);

            for (int i = 0; i < m_eventFlagsList.Count; ++i)
            {
                sb.AppendFormat("FLAG_0x{0:X8} {1}\t{2}\r\n", m_eventFlagsList[i].FlagIdx, m_eventFlagsList[i].IsSet,
                    m_eventFlagsList[i].FlagTypeVal == EventFlagType._Unused ? "UNUSED" : m_eventFlagsList[i].ToString());
            }

            System.IO.File.WriteAllText(string.Format("flags_dump_{0}.txt", m_savFile.Version), sb.ToString());
        }

        protected override bool ShouldExportEvent(FlagDetail eventDetail)
        {
            if (eventDetail.FlagTypeVal == EventFlagType.GeneralEvent)
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

        protected override bool IsEvtSet(EventDetail evtDetail)
        {
            bool isEvtSet = false;
            ulong idx = (uint)evtDetail.EvtId;
            var savEventBlocks = (m_savFile as ISCBlockArray).Accessor;

            switch (evtDetail.EvtSource)
            {
                case 0: // Bool blocks
                    isEvtSet = (savEventBlocks.GetBlockSafe((uint)idx).Type == SCTypeCode.Bool2);
                    break;

                default:
                    isEvtSet = false;
                    break;
            }

            return isEvtSet;
        }

    }

}
