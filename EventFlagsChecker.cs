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
            StaticEncounter,
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


        protected SaveFile? m_savFile;

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
                string? s = reader.ReadLine();

                if (s is null)
                {
                    return;
                }

                do
                {
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        var evtDetail = new EventDetail(s, m_flagsSourceInfo);
                        evtDetail.IsDone = IsEvtSet(evtDetail);
                        m_eventsChecklist.Add(evtDetail);
                    }

                    s = reader.ReadLine();

                } while (s is not null);
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

            System.IO.File.WriteAllText(string.Format("checklist_{0}.txt", m_savFile!.Version), sb.ToString());
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

            System.IO.File.WriteAllText(string.Format("missing_events_{0}.txt", m_savFile!.Version), sb.ToString());
        }

        #endregion Actions

        public static bool HasItemInBag(IReadOnlyList<InventoryPouch> bag, int itemID)
        {
            foreach (var pouch in bag)
            {
                for (int i = 0; i < pouch.Items.Length; i++)
                {
                    if (pouch.Items[i].Index == itemID)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public static ulong ParseDecOrHex(string str)
        {
            if (str.StartsWith("0x"))
                return Convert.ToUInt64(str, 16);

            return Convert.ToUInt64(str);
        }

        public static long ParseDecOrHexSigned(string str)
        {
            if (str.StartsWith("0x"))
                return Convert.ToInt64(str, 16);

            return Convert.ToInt64(str);
        }

        protected static string ReadResFile(string resName, string? langCode = null)
        {
            string? contentTxt = null;

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            // Try outside file first
            var offResPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(assembly.Location)!, resName);
            if (!System.IO.File.Exists(offResPath))
            {
                resName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(resName));

                try
                {
                    resName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(resName));
                }
                catch (InvalidOperationException)
                {
                    // Load default language
                    return ReadResFile(resName, "en");
                }

                using (var stream = assembly.GetManifestResourceStream(resName))
                {
                    using (var reader = new System.IO.StreamReader(stream!))
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
            EventFlagsChecker? eventsOrganizer = savFile.Version switch
            {
                GameVersion.Any or
                GameVersion.RBY or
                GameVersion.StadiumJ or
                GameVersion.Stadium or
                GameVersion.Stadium2 or
                GameVersion.RSBOX or
                GameVersion.COLO or
                GameVersion.XD or
                GameVersion.CXD or
                GameVersion.BATREV or
                GameVersion.ORASDEMO or
                GameVersion.GO or
                GameVersion.Unknown or
                GameVersion.Invalid
                    // unsupported format
                    => null,

                GameVersion.RD or
                GameVersion.GN or
                GameVersion.BU or
                GameVersion.RB
                    => new CheckerGen1RB(),

                GameVersion.YW
                    => new CheckerGen1Y(),

                GameVersion.GD or
                GameVersion.SI or
                GameVersion.GS
                    => new CheckerGen2GS(),

                GameVersion.C
                    => new CheckerGen2C(),

                GameVersion.R or
                GameVersion.S or
                GameVersion.RS
                    => new CheckerGen3RS(),

                GameVersion.FR or
                GameVersion.LG or
                GameVersion.FRLG
                    => new CheckerGen3FRLG(),

                GameVersion.E
                    => new CheckerGen3E(),

                GameVersion.D or
                GameVersion.P or
                GameVersion.DP
                    => new CheckerGen4DP(),

                GameVersion.Pt
                    => new CheckerGen4Pt(),

                GameVersion.HG or
                GameVersion.SS or
                GameVersion.HGSS
                    => new CheckerGen4HGSS(),

                GameVersion.B or
                GameVersion.W or
                GameVersion.BW
                    => new CheckerGen5BW(),

                GameVersion.B2 or
                GameVersion.W2 or
                GameVersion.B2W2
                    => new CheckerGen5B2W2(),

                GameVersion.X or
                GameVersion.Y or
                GameVersion.XY
                    => new CheckerGen6XY(),

                GameVersion.OR or
                GameVersion.AS or
                GameVersion.ORAS
                    => new CheckerGen6ORAS(),

                GameVersion.SN or
                GameVersion.MN or
                GameVersion.SM
                    => new CheckerGen7SM(),

                GameVersion.US or
                GameVersion.UM or
                GameVersion.USUM
                    => new CheckerGen7USUM(),

                GameVersion.GP or
                GameVersion.GE or
                GameVersion.GG
                    => new CheckerGen7bGPGE(),

                GameVersion.SW or
                GameVersion.SH or
                GameVersion.SWSH
                    => new CheckerGen8SWSH(),

                GameVersion.BD or
                GameVersion.SP or
                GameVersion.BDSP
                    => new CheckerGen8BDSP(),

                GameVersion.PLA
                    => new CheckerGen8LA(),

                GameVersion.SL or
                GameVersion.VL or
                GameVersion.SV
                    => new CheckerGen9SV(),

                _ => null
            };

            if (eventsOrganizer is null)
            {
                throw new FormatException($"Unsupported SAV format: {savFile.Version}");
            }

            eventsOrganizer.InitData(savFile);

            return eventsOrganizer;
        }
    }
}
