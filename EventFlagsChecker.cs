using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    public abstract class EventFlagsChecker
    {
        public enum EventFlagType
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
            FlySpot,
            BerryTree,
            Collectable,

            _Unused,
            _Separator,
        }

        /// <summary>
        /// Wraps the details of an Event that is sourced from Event Flags.
        /// </summary>
        public class EventDetail
        {
            /// <summary>
            /// The Id of the event source, which can be sourced from other specific flags
            /// </summary>
            public int EvtSource { get; private set; }

            /// <summary>
            /// The Id/Idx of this event flag in the source flag array
            /// </summary>
            public ulong EvtId { get; private set; }

            /// <summary>
            /// Category of this event
            /// </summary>
            public EventFlagType EvtTypeVal { get; private set; }

            /// <summary>
            /// The in-game location where this event happens
            /// </summary>
            public string Location { get; private set; }

            /// <summary>
            /// A description of what the action needed to trigger this event
            /// </summary>
            public string DescTxt { get; private set; }

            /// <summary>
            /// Informs if this event is done
            /// </summary>
            public bool IsDone { get; set; }

            /// <summary>
            /// Create a new EventDetail by parsing a checklist tsv line and a list of flags sources
            /// </summary>
            /// <param name="detailEntry">A single line from the checklist tsv file</param>
            /// <param name="sources">A dictionary with name/id of the sources</param>
            public EventDetail(string detailEntry, Dictionary<string, int> sources)
            {
                string[] info = detailEntry.Split('\t');

                if (info.Length < 6)
                {
                    throw new ArgumentException("Argument detailEntry format is not valid");
                }

                EvtSource = sources[info[0]];
                EvtId = ParseDecOrHex(info[1]);
                EvtTypeVal = EvtTypeVal.Parse(info[2]);

                Location = info[3];
                if (!string.IsNullOrWhiteSpace(info[4]))
                {
                    Location += " " + info[4];
                }
                DescTxt = info[5];
                IsDone = false;
            }


            public override string ToString()
            {
                if (string.IsNullOrEmpty(Location))
                {
                    return string.Format("{0} - {1}", EvtTypeVal.AsText(), DescTxt);
                }

                else
                {
                    return string.Format("{0} - {1} - {2}", EvtTypeVal.AsText(), Location, DescTxt);
                }
            }
        }


        protected SaveFile m_savFile;

        protected List<EventDetail> m_eventsChecklist = new List<EventDetail>(4096);
        protected Dictionary<string, int> m_flagsSourceInfo = new Dictionary<string, int>(10);

        /// <summary>
        /// Parsed list of event flags to be used for displaying data
        /// </summary>
        public List<EventDetail> EventsChecklist => m_eventsChecklist;

        protected abstract bool IsEvtSet(EventDetail evtDetail);
        protected abstract void InitData(SaveFile savFile);

        protected void ParseChecklist(string chkList_res)
        {
            m_eventsChecklist.Clear();

            using (System.IO.StringReader reader = new System.IO.StringReader(chkList_res))
            {
                string s = reader.ReadLine();

                do
                {
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        var evtDetail = new EventDetail(s, m_flagsSourceInfo);
                        evtDetail.IsDone = IsEvtSet(evtDetail);
                        m_eventsChecklist.Add(evtDetail);
                    }

                    s = reader.ReadLine();

                } while (s != null);
            }
        }


        #region Actions

        public virtual void ExportChecklist()
        {
            StringBuilder sb = new StringBuilder(512 * 1024);

            foreach (var evt in m_eventsChecklist)
            {
                if (evt.EvtTypeVal != EventFlagType._Separator)
                {
                    sb.AppendFormat("[{0}] {1}\r\n", evt.IsDone ? "x" : " ", evt.ToString());
                }
                else
                {
                    sb.Append("\r\n");
                }
            }

            System.IO.File.WriteAllText(string.Format("checklist_{0}.txt", m_savFile.Version), sb.ToString());
        }

        public virtual void ExportMissingEvents()
        {
            StringBuilder sb = new StringBuilder(512 * 1024);

            foreach (var evt in m_eventsChecklist)
            {
                if (evt.EvtTypeVal != EventFlagType._Separator && !evt.IsDone)
                {
                    sb.Append($"{evt}\r\n");
                }
            }

            System.IO.File.WriteAllText(string.Format("missing_events_{0}.txt", m_savFile.Version), sb.ToString());
        }

        #endregion Actions


        public static ulong ParseDecOrHex(string str)
        {
            if (str.StartsWith("0x"))
                return Convert.ToUInt64(str, 16);

            return Convert.ToUInt64(str);
        }

        protected static string ReadResFile(string resName)
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

        public static EventFlagsChecker CreateEventFlagsChecker(SaveFile savFile)
        {
            EventFlagsChecker eventsOrganizer = null;

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
                    eventsOrganizer = new CheckerGen1RB();
                    break;

                case GameVersion.YW:
                    eventsOrganizer = new CheckerGen1Y();
                    break;

                case GameVersion.GD:
                case GameVersion.SI:
                case GameVersion.GS:
                    eventsOrganizer = new CheckerGen2GS();
                    break;

                case GameVersion.C:
                    eventsOrganizer = new CheckerGen2C();
                    break;

                case GameVersion.R:
                case GameVersion.S:
                case GameVersion.RS:
                    eventsOrganizer = new CheckerGen3RS();
                    break;

                case GameVersion.FR:
                case GameVersion.LG:
                case GameVersion.FRLG:
                    eventsOrganizer = new CheckerGen3FRLG();
                    break;

                case GameVersion.E:
                    eventsOrganizer = new CheckerGen3E();
                    break;

                case GameVersion.D:
                case GameVersion.P:
                case GameVersion.DP:
                    eventsOrganizer = new CheckerGen4DP();
                    break;

                case GameVersion.Pt:
                    eventsOrganizer = new CheckerGen4Pt();
                    break;

                case GameVersion.HG:
                case GameVersion.SS:
                case GameVersion.HGSS:
                    eventsOrganizer = new CheckerGen4HGSS();
                    break;

                case GameVersion.B:
                case GameVersion.W:
                case GameVersion.BW:
                    eventsOrganizer = new CheckerGen5BW();
                    break;

                case GameVersion.B2:
                case GameVersion.W2:
                case GameVersion.B2W2:
                    eventsOrganizer = new CheckerGen5B2W2();
                    break;

                case GameVersion.X:
                case GameVersion.Y:
                case GameVersion.XY:
                    eventsOrganizer = new CheckerGen6XY();
                    break;

                case GameVersion.OR:
                case GameVersion.AS:
                case GameVersion.ORAS:
                    eventsOrganizer = new CheckerGen6ORAS();
                    break;

                case GameVersion.SN:
                case GameVersion.MN:
                case GameVersion.SM:
                    eventsOrganizer = new CheckerGen7SM();
                    break;

                case GameVersion.US:
                case GameVersion.UM:
                case GameVersion.USUM:
                    eventsOrganizer = new CheckerGen7USUM();
                    break;

                case GameVersion.GP:
                case GameVersion.GE:
                case GameVersion.GG:
                    eventsOrganizer = new CheckerGen7bGPGE();
                    break;

                case GameVersion.BD:
                case GameVersion.SP:
                case GameVersion.BDSP:
                    eventsOrganizer = new CheckerGen8bsBDSP();
                    break;

                case GameVersion.SW:
                case GameVersion.SH:
                case GameVersion.SWSH:
                    eventsOrganizer = new CheckerGen8SWSH();
                    break;

                case GameVersion.SL:
                case GameVersion.VL:
                case GameVersion.SV:
                    eventsOrganizer = new CheckerGen9SV();
                    break;


                case GameVersion.PLA:
                    //eventsOrganizer = new DummyOrgBlockFlags();
                    break;

                default:
                    break;
            }

            if (eventsOrganizer != null)
            {
                eventsOrganizer.InitData(savFile);
            }

            return eventsOrganizer;
        }
    }
}
