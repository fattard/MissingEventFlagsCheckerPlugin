using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{

    public abstract class FlagsOrganizer
    {
        public enum FlagType
        {
            _Unknown,

            FieldItem,
            HiddenItem,
            SpecialItem,
            TrainerBattle,
            StaticBattle,
            InGameTrade,
            ItemGift,
            PkmnGift,
            GeneralEvent,
            SideEvent,
            StoryEvent,
            BerryTree,
            Collectable,

            _Unused,
            _Separator,

            //TODO: remove
            Gift = ItemGift,
        }


        protected class FlagDetail
        {
            public long OrderKey { get; set; }
            public uint FlagIdx { get; private set; }
            public FlagType FlagTypeVal { get; private set; }
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
            }

            public FlagDetail(uint flagIdx, FlagType flagType, string detailMsg) : this(flagIdx, flagType, "", detailMsg)
            {
            }

            public FlagDetail(uint flagIdx, FlagType flagType, string locationName, string detailMsg)
            {
                OrderKey = (flagIdx + 100000);
                AHTB = (ulong)flagIdx;
                FlagIdx = flagIdx;
                FlagTypeVal = flagType;
                LocationName = locationName;
                DetailMsg = detailMsg;
                IsSet = false;
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


        protected class WorkDetail
        {
            public long OrderKey { get; set; }
            public uint WorkIdx { get; private set; }
            public FlagType FlagTypeVal { get; private set; }
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

            public WorkDetail(uint workIdx, FlagType flagType, string detailMsg) : this(workIdx, flagType, "", detailMsg)
            {
            }

            public WorkDetail(uint workIdx, FlagType flagType, string locationName, string detailMsg)
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


        protected SaveFile m_savFile;

        protected List<FlagDetail> m_eventFlagsList = new List<FlagDetail>(4096);
        protected List<WorkDetail> m_eventWorkList = new List<WorkDetail>(4096);

        protected virtual void InitFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;
            m_eventFlagsList.Clear();
            m_eventWorkList.Clear();
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


        protected virtual void AssembleWorkList<T>(string workList_res) where T: unmanaged
        {
            var savEventWork = (m_savFile as IEventWorkArray<T>).GetAllEventWork();
            m_eventWorkList.Clear();

            //TODO: temp for those that still have no resources file
            if (workList_res == null)
            {
                for (uint i = 0; i < savEventWork.Length; i++)
                {
                    var workDetail = new WorkDetail(i, FlagType._Unknown, "");
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

        public virtual void ExportMissingFlags()
        {
            m_eventFlagsList.Sort((x, y) => (int)(x.OrderKey - y.OrderKey));

            StringBuilder sb = new StringBuilder(100 * 1024);
            for (int i = 0; i < m_eventFlagsList.Count; ++i)
            {
                if (!m_eventFlagsList[i].IsSet && ShouldExportEvent(m_eventFlagsList[i]))
                {
                    sb.Append($"{m_eventFlagsList[i]}\r\n");
                }
            }

            System.IO.File.WriteAllText(string.Format("missing_events_{0}.txt", m_savFile.Version), sb.ToString());
        }

        public virtual void ExportChecklist()
        {
            m_eventFlagsList.Sort((x, y) => (int)(x.OrderKey - y.OrderKey));

            StringBuilder sb = new StringBuilder(100 * 1024);
            for (int i = 0; i < m_eventFlagsList.Count; ++i)
            {
                if (ShouldExportEvent(m_eventFlagsList[i]))
                {
                    sb.AppendFormat("[{0}] {1}\r\n", m_eventFlagsList[i].IsSet ? "x" : " ", m_eventFlagsList[i]);
                }
            }

            System.IO.File.WriteAllText(string.Format("checklist_{0}.txt", m_savFile.Version), sb.ToString());
        }

        public virtual void DumpAllFlags()
        {
            StringBuilder sb = new StringBuilder(100 * 1024);
            for (int i = 0; i < m_eventFlagsList.Count; ++i)
            {
                sb.AppendFormat("FLAG_0x{0:X4} {1}\t{2}\r\n", i, m_eventFlagsList[i].IsSet,
                    m_eventFlagsList[i].FlagTypeVal == FlagType._Unused ? "UNUSED" : m_eventFlagsList[i].ToString());
            }

            if (m_eventWorkList.Count > 0)
            {
                sb.Append("\r\n\r\n");

                for (int i = 0; i < m_eventWorkList.Count; ++i)
                {
                    sb.AppendFormat("WORK_0x{0:X4} => {1,5}\t{2}\r\n", i, m_eventWorkList[i].Value,
                        m_eventWorkList[i].FlagTypeVal == FlagType._Unused ? "UNUSED" : m_eventWorkList[i].ToString());
                }
            }

            System.IO.File.WriteAllText(string.Format("flags_dump_{0}.txt", m_savFile.Version), sb.ToString());
        }

        public abstract void MarkFlags(FlagType flagType);
        public abstract void UnmarkFlags(FlagType flagType);
        public abstract bool SupportsEditingFlag(FlagType flagType);

        #endregion

        protected virtual bool ShouldExportEvent(FlagDetail eventDetail)
        {
            switch (eventDetail.FlagTypeVal)
            {
                case FlagType.GeneralEvent:
                case FlagType._Unused:
                case FlagType._Unknown:
                    return false;

                default:
                    return true;
            }
        }

        protected string ReadFlagsListRes(string resName)
        {
            string contentTxt = null;

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            // Try off-res first
            var offResPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(assembly.Location), resName);
            if (!System.IO.File.Exists(offResPath))
            {
                resName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(resName));

                using (var stream = assembly.GetManifestResourceStream(resName))
                {
                    using (var reader = new System.IO.StreamReader(stream))
                    {
                        contentTxt = reader.ReadToEnd();
                    }
                }
            }
            else
            {
                contentTxt = System.IO.File.ReadAllText(offResPath);
            }

            return contentTxt;
        }



        public static FlagsOrganizer OrganizeFlags(SaveFile savFile)
        {
            FlagsOrganizer flagsOrganizer = null;

            switch (savFile.Version)
            {
                case GameVersion.Any:
                case GameVersion.RBY:
                case GameVersion.StadiumJ:
                case GameVersion.Stadium:
                case GameVersion.Stadium2:
                case GameVersion.RSBOX:
                case GameVersion.COLO:
                case GameVersion.XD:
                case GameVersion.CXD:
                case GameVersion.BATREV:
                case GameVersion.ORASDEMO:
                case GameVersion.GO:
                case GameVersion.Unknown:
                case GameVersion.Invalid:
                    break; // unsupported format

                case GameVersion.RD:
                case GameVersion.GN:
                case GameVersion.RB:
                    flagsOrganizer = new FlagsGen1RB();
                    break;

                case GameVersion.YW:
                    flagsOrganizer = new FlagsGen1Y();
                    break;

                case GameVersion.GD:
                case GameVersion.SI:
                case GameVersion.GS:
                    flagsOrganizer = new FlagsGen2GS();
                    break;

                case GameVersion.C:
                    flagsOrganizer = new FlagsGen2C();
                    break;

                case GameVersion.R:
                case GameVersion.S:
                case GameVersion.RS:
                    flagsOrganizer = new FlagsGen3RS();
                    break;

                case GameVersion.FR:
                case GameVersion.LG:
                case GameVersion.FRLG:
                    flagsOrganizer = new FlagsGen3FRLG();
                    break;

                case GameVersion.E:
                    flagsOrganizer = new FlagsGen3E();
                    break;

                case GameVersion.D:
                case GameVersion.P:
                case GameVersion.DP:
                    flagsOrganizer = new FlagsGen4DP();
                    break;

                case GameVersion.Pt:
                    flagsOrganizer = new FlagsGen4Pt();
                    break;

                case GameVersion.HG:
                case GameVersion.SS:
                case GameVersion.HGSS:
                    flagsOrganizer = new FlagsGen4HGSS();
                    break;

                case GameVersion.B:
                case GameVersion.W:
                case GameVersion.BW:
                    flagsOrganizer = new FlagsGen5BW();
                    break;

                case GameVersion.B2:
                case GameVersion.W2:
                case GameVersion.B2W2:
                    flagsOrganizer = new FlagsGen5B2W2();
                    break;

                case GameVersion.X:
                case GameVersion.Y:
                case GameVersion.XY:
                    flagsOrganizer = new FlagsGen6XY();
                    break;

                case GameVersion.OR:
                case GameVersion.AS:
                case GameVersion.ORAS:
                    flagsOrganizer = new FlagsGen6ORAS();
                    break;

                case GameVersion.SN:
                case GameVersion.MN:
                case GameVersion.SM:
                    flagsOrganizer = new FlagsGen7SM();
                    break;

                case GameVersion.US:
                case GameVersion.UM:
                case GameVersion.USUM:
                    flagsOrganizer = new FlagsGen7USUM();
                    break;

                case GameVersion.GP:
                case GameVersion.GE:
                case GameVersion.GG:
                    flagsOrganizer = new FlagsGen7bGPGE();
                    break;

                case GameVersion.BD:
                case GameVersion.SP:
                case GameVersion.BDSP:
                    flagsOrganizer = new FlagsGen8bsBDSP();
                    break;

                case GameVersion.SW:
                case GameVersion.SH:
                case GameVersion.SWSH:
                    flagsOrganizer = new FlagsGen8SWSH();
                    break;

                case GameVersion.SL:
                case GameVersion.VL:
                case GameVersion.SV:
                    flagsOrganizer = new FlagsGen9SV();
                    break;


                case GameVersion.PLA:
                    flagsOrganizer = new DummyOrgBlockFlags();
                    break;

                default:
                    break;
            }

            if (flagsOrganizer != null)
            {
                flagsOrganizer.InitFlagsData(savFile);
            }

            return flagsOrganizer;
        }

    }

}
