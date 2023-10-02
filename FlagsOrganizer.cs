using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{

    public abstract class FlagsOrganizer : EventFlagsOrganizer
    {

        public class FlagDetail
        {
            public long OrderKey { get; set; }
            public int SourceIdx { get; set; }
            public uint FlagIdx { get; private set; }
            public EventFlagType FlagTypeVal { get; private set; }
            public string FlagTypeTxt => FlagTypeVal.AsText();
            public string LocationName { get; private set; }
            public string DetailMsg { get; private set; }
            public bool IsSet { get; set; }
            public ulong AHTB { get; set; }


            public FlagDetail(string detailEntry)
            {
                string[] info = detailEntry.Split('\t');

                if (info.Length < 7)
                {
                    throw new ArgumentException("Argument detailEntry format is not valid");
                }
                AHTB = Convert.ToUInt64(info[0], 16);
                FlagIdx = (uint)(AHTB & 0xFFFFFFFF);
                FlagTypeVal = FlagTypeVal.Parse(info[1]);
                LocationName = info[2];
                if (!string.IsNullOrWhiteSpace(info[3]))
                {
                    LocationName += " " + info[3];
                }
                DetailMsg = !string.IsNullOrWhiteSpace(info[4]) ? info[4] : info[6];
                IsSet = false;
                //OrderKey = string.IsNullOrWhiteSpace(info[0]) ? (FlagIdx + 100000) : Convert.ToInt64(info[0]);
                OrderKey = (FlagIdx + 100000);
                SourceIdx = 0;
            }

            public FlagDetail(uint flagIdx, int source, EventFlagType flagType, string locationName, string detailMsg)
            {
                OrderKey = (flagIdx + 100000);
                AHTB = (ulong)flagIdx;
                FlagIdx = flagIdx;
                FlagTypeVal = flagType;
                LocationName = locationName;
                DetailMsg = detailMsg;
                IsSet = false;
                SourceIdx = source;
            }

            public override string ToString()
            {
                if (string.IsNullOrEmpty(LocationName))
                {
                    return string.Format("{0} - {1}", FlagTypeTxt, DetailMsg);
                }

                else
                {
                    return string.Format("{0} - {1} - {2}", FlagTypeTxt, LocationName, DetailMsg);
                }
            }
        }


        public class WorkDetail
        {
            public long OrderKey { get; set; }
            public uint WorkIdx { get; private set; }
            public EventFlagType FlagTypeVal { get; private set; }
            public string FlagTypeTxt => FlagTypeVal.AsText();
            public string LocationName { get; private set; }
            public string DetailMsg { get; private set; }
            public Dictionary<long, string> ValidValues { get; private set; }
            public long Value { get; set; }
            public ulong AHTB { get; set; }


            public WorkDetail(string detailEntry)
            {
                string[] info = detailEntry.Split('\t');

                if (info.Length < 7)
                {
                    throw new ArgumentException("Argument detailEntry format is not valid");
                }
                AHTB = Convert.ToUInt64(info[0], 16);
                WorkIdx = (uint)(AHTB & 0xFFFFFFFF);
                FlagTypeVal = FlagTypeVal.Parse(info[1]);
                LocationName = info[2];
                if (!string.IsNullOrWhiteSpace(info[3]))
                {
                    LocationName += " " + info[3];
                }
                DetailMsg = !string.IsNullOrWhiteSpace(info[4]) ? info[4] : info[6];
                Value = 0;

                ValidValues = new Dictionary<long, string>(4);
                if (!string.IsNullOrWhiteSpace(info[5]))
                {
                    // x:y tuples separated by ,
                    var possibleTuples = info[5].Split(',');
                    foreach (var t in possibleTuples)
                    {
                        int sep = t.IndexOf(':');
                        if (sep > 0)
                        {
                            ValidValues.Add(Convert.ToInt64(t.Substring(0, sep)), t.Substring(sep + 1));
                        }
                    }
                }

                //OrderKey = string.IsNullOrWhiteSpace(info[0]) ? (WorkIdx + 100000) : Convert.ToInt64(info[0]);
                OrderKey = (WorkIdx + 100000);
            }

            public WorkDetail(uint workIdx, EventFlagType flagType, string detailMsg) : this(workIdx, flagType, "", detailMsg)
            {
            }

            public WorkDetail(uint workIdx, EventFlagType flagType, string locationName, string detailMsg)
            {
                OrderKey = (workIdx + 100000);
                AHTB = (ulong)workIdx;
                WorkIdx = workIdx;
                FlagTypeVal = flagType;
                LocationName = locationName;
                DetailMsg = detailMsg;
                ValidValues = new Dictionary<long, string>(4);
                Value = 0;
            }

            public override string ToString()
            {
                if (string.IsNullOrEmpty(LocationName))
                {
                    return string.Format("{0} - {1}{2}", FlagTypeTxt, DetailMsg, ((ValidValues.Count > 0 && ValidValues.ContainsKey(Value)) ? " => " + ValidValues[Value] : ""));
                }

                else
                {
                    return string.Format("{0} - {1} - {2}{3}", FlagTypeTxt, LocationName, DetailMsg, ((ValidValues.Count > 0 && ValidValues.ContainsKey(Value)) ? " => " + ValidValues[Value] : ""));
                }
            }
        }


        protected virtual void AssembleList(string flagsList_res, bool[] customFlagValues = null)
        {
            var savEventFlags = customFlagValues ?? (m_savFile as IEventFlagArray).GetEventFlags();
            
            //TODO: remove the clear from this place, each InitFlags should clear it
            if (customFlagValues == null)
                m_eventFlagsList.Clear();

            using (System.IO.StringReader reader = new System.IO.StringReader(flagsList_res))
            {
                string s = reader.ReadLine();
                
                // Skip header
                if (s.StartsWith("//"))
                {
                    s = reader.ReadLine();
                }

                do
                {
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        // End of section
                        if (s.StartsWith("//"))
                        {
                            break;
                        }

                        var flagDetail = new FlagDetail(s);
                        flagDetail.IsSet = savEventFlags[flagDetail.FlagIdx];
                        m_eventFlagsList.Add(flagDetail);
                    }

                    s = reader.ReadLine();

                } while (s != null);
            }
        }

        protected virtual void AssembleList(string flagsList_res, int sourceIdx, bool[] flagValues)
        {
            using (System.IO.StringReader reader = new System.IO.StringReader(flagsList_res))
            {
                string s = reader.ReadLine();

                // Skip header
                if (s.StartsWith("//"))
                {
                    s = reader.ReadLine();
                }

                do
                {
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        // End of section
                        if (s.StartsWith("//"))
                        {
                            break;
                        }

                        var flagDetail = new FlagDetail(s);
                        flagDetail.IsSet = flagValues[flagDetail.FlagIdx];
                        flagDetail.SourceIdx = sourceIdx;
                        m_eventFlagsList.Add(flagDetail);
                    }

                    s = reader.ReadLine();

                } while (s != null);
            }
        }


        protected virtual void AssembleWorkList<T>(string workList_res) where T: unmanaged
        {
            var savEventWork = (m_savFile as IEventWorkArray<T>).GetAllEventWork();
            m_eventWorkList.Clear();

            //TODO: temp for those that still have no resources file
            if (workList_res == null)
            {
                for (uint i = 0; i < savEventWork.Length; i++)
                {
                    var workDetail = new WorkDetail(i, EventFlagType._Unknown, "");
                    workDetail.Value = Convert.ToInt64(savEventWork[workDetail.WorkIdx]);
                    m_eventWorkList.Add(workDetail);
                }
            }
            else
            {
                using (System.IO.StringReader reader = new System.IO.StringReader(workList_res))
                {
                    string s = reader.ReadLine();

                    // Skip header
                    if (s.StartsWith("//"))
                    {
                        s = reader.ReadLine();
                    }

                    do
                    {
                        if (!string.IsNullOrWhiteSpace(s))
                        {
                            // End of section
                            if (s.StartsWith("//"))
                            {
                                break;
                            }

                            var workDetail = new WorkDetail(s);
                            workDetail.Value = Convert.ToInt64(savEventWork[workDetail.WorkIdx]);
                            m_eventWorkList.Add(workDetail);
                        }

                        s = reader.ReadLine();

                    } while (s != null);
                }
            }
        }



        #region Actions

        public override void ExportMissingEvents()
        {
            if (m_eventsChecklist.Count == 0)
            {
                StringBuilder sb = new StringBuilder(512 * 1024);
                m_eventFlagsList.Sort((x, y) => (int)(x.OrderKey - y.OrderKey));

                for (int i = 0; i < m_eventFlagsList.Count; ++i)
                {
                    if (!m_eventFlagsList[i].IsSet && ShouldExportEvent(m_eventFlagsList[i]))
                    {
                        sb.Append($"{m_eventFlagsList[i]}\r\n");
                    }
                }
                
                System.IO.File.WriteAllText(string.Format("missing_events_{0}.txt", m_savFile.Version), sb.ToString());
            }

            else
            {
                base.ExportMissingEvents();
            }

        }

        public override void ExportChecklist()
        {

            if (m_eventsChecklist.Count == 0)
            {
                StringBuilder sb = new StringBuilder(512 * 1024);

                m_eventFlagsList.Sort((x, y) => (int)(x.OrderKey - y.OrderKey));

                for (int i = 0; i < m_eventFlagsList.Count; ++i)
                {
                    if (ShouldExportEvent(m_eventFlagsList[i]))
                    {
                        sb.AppendFormat("[{0}] {1}\r\n", m_eventFlagsList[i].IsSet ? "x" : " ", m_eventFlagsList[i]);
                    }
                }

                System.IO.File.WriteAllText(string.Format("checklist_{0}.txt", m_savFile.Version), sb.ToString());
            }

            else
            {
                base.ExportChecklist();
            }
        }

        


        #endregion

        protected virtual bool ShouldExportEvent(FlagDetail eventDetail)
        {
            switch (eventDetail.FlagTypeVal)
            {
                case EventFlagType.GeneralEvent:
                case EventFlagType._Unused:
                case EventFlagType._Unknown:
                    return false;

                default:
                    return true;
            }
        }

    }

}
