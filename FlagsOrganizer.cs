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
            TrainerBattle,
            StaticBattle,
            InGameTrade,
            Gift,
            GeneralEvent,
            SideEvent,
            StoryEvent,
            BerryTree,

            _Unused,
        }


        protected class FlagDetail
        {
            public int OrderKey { get; set; }
            public int FlagIdx { get; private set; }
            public FlagType FlagTypeVal { get; private set; }
            public string FlagTypeTxt => FlagTypeVal.AsText();
            public string LocationName { get; private set; }
            public string DetailMsg { get; private set; }
            public bool IsSet { get; set; }


            public FlagDetail(string detailEntry)
            {
                string[] info = detailEntry.Split('\t');

                if (info.Length < 7)
                {
                    throw new ArgumentException("Argument detailEntry format is not valid");
                }

                FlagIdx = Convert.ToInt32(info[1], 16);
                FlagTypeVal = FlagTypeVal.Parse(info[2]);
                LocationName = info[3];
                if (!string.IsNullOrWhiteSpace(info[4]))
                {
                    LocationName += " " + info[4];
                }
                DetailMsg = !string.IsNullOrWhiteSpace(info[5]) ? info[5] : info[6];
                IsSet = false;
                OrderKey = string.IsNullOrWhiteSpace(info[0]) ? (FlagIdx + 100000) : Convert.ToInt32(info[0]);
            }

            public FlagDetail(int flagIdx, FlagType flagType, string detailMsg) : this(flagIdx, flagType, "", detailMsg)
            {
            }

            public FlagDetail(int flagIdx, FlagType flagType, string locationName, string detailMsg)
            {
                OrderKey = (flagIdx + 100000);
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


        protected SaveFile m_savFile;

        protected List<FlagDetail> m_eventFlagsList = new List<FlagDetail>(4096);

        protected virtual void InitFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;
            m_eventFlagsList.Clear();
        }

        protected virtual void AssembleList(string flagsList_res)
        {
            var savEventFlags = (m_savFile as IEventFlagArray).GetEventFlags();
            m_eventFlagsList.Clear();
            using (System.IO.StringReader reader = new System.IO.StringReader(flagsList_res))
            {
                string s = reader.ReadLine();
                do
                {
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        var flagDetail = new FlagDetail(s);
                        flagDetail.IsSet = savEventFlags[flagDetail.FlagIdx];
                        m_eventFlagsList.Add(flagDetail);
                    }

                    s = reader.ReadLine();

                } while (s != null);
            }
        }


        #region Actions

        public virtual void ExportMissingFlags()
        {
            m_eventFlagsList.Sort((x, y) => x.OrderKey - y.OrderKey);

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
            m_eventFlagsList.Sort((x, y) => x.OrderKey - y.OrderKey);

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
#if DEBUG
                sb.AppendFormat("FLAG_0x{0:X4} {1}\t{2}\r\n", i, m_eventFlagsList[i].IsSet,
                    m_eventFlagsList[i].FlagTypeVal == FlagType._Unused ? "UNUSED" : m_eventFlagsList[i].ToString());
#else
                sb.AppendFormat("FLAG_0x{0:X4} {1}\r\n", i, m_eventFlagsList[i].IsSet);
#endif
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

            resName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(resName));

            using (var stream = assembly.GetManifestResourceStream(resName))
            {
                using (var reader = new System.IO.StreamReader(stream))
                {
                    contentTxt = reader.ReadToEnd();
                }
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
                case GameVersion.PLA:
                case GameVersion.SL:
                case GameVersion.VL:
                case GameVersion.SV:
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
