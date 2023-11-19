using PKHeX.Core;
using System;
using System.Windows.Forms;

namespace MissingEventFlagsCheckerPlugin
{
    public class MissingEventFlagsChecker : IPlugin
    {
        public string Name => "Missing Event Flags Checker";
        public string NameExportMissingFlags => "Export only the missing events";
        public string NameExportChecklistFlags => "Export full Checklist";
        public int Priority => 100; // Loading order, lowest is first.
        public ISaveFileProvider SaveFileEditor { get; private set; } = null;

        private ToolStripMenuItem ctrl;

        private ToolStripMenuItem menuEntry_ExportMissingEvents;
        private ToolStripMenuItem menuEntry_ExportChecklist;

        public void Initialize(params object[] args)
        {
            SaveFileEditor = (ISaveFileProvider)Array.Find(args, z => z is ISaveFileProvider);
            var menu = (ToolStrip)Array.Find(args, z => z is ToolStrip);
            LoadMenuStrip(menu);
        }

        private void LoadMenuStrip(ToolStrip menuStrip)
        {
            var items = menuStrip.Items;
            var tools = (ToolStripDropDownItem)items.Find("Menu_Tools", false)[0];
            AddPluginControl(tools);
        }

        private void AddPluginControl(ToolStripDropDownItem tools)
        {
            ctrl = new ToolStripMenuItem(Name);
            ctrl.Enabled = false;
            tools.DropDownItems.Add(ctrl);

            menuEntry_ExportChecklist = new ToolStripMenuItem(NameExportChecklistFlags);
            menuEntry_ExportChecklist.Enabled = false;
            menuEntry_ExportChecklist.Click += new EventHandler(ExportChecklist_UIEvt);
            ctrl.DropDownItems.Add(menuEntry_ExportChecklist);

            menuEntry_ExportMissingEvents = new ToolStripMenuItem(NameExportMissingFlags);
            menuEntry_ExportMissingEvents.Enabled = false;
            menuEntry_ExportMissingEvents.Click += new EventHandler(ExportMissingEvents_UIEvt);
            ctrl.DropDownItems.Add(menuEntry_ExportMissingEvents);
        }

        private void ExportMissingEvents_UIEvt(object sender, EventArgs e)
        {
            var eventsChecker = EventFlagsChecker.CreateEventFlagsChecker(SaveFileEditor.SAV);

            if (eventsChecker == null)
            {
                throw new FormatException("Unsupported SAV format: " + SaveFileEditor.SAV.Version);
            }

            eventsChecker.ExportMissingEvents();
        }

        private void ExportChecklist_UIEvt(object sender, EventArgs e)
        {
            var eventsChecker = EventFlagsChecker.CreateEventFlagsChecker(SaveFileEditor.SAV);

            if (eventsChecker == null)
            {
                throw new FormatException("Unsupported SAV format: " + SaveFileEditor.SAV.Version);
            }

            eventsChecker.ExportChecklist();
        }

        public void NotifySaveLoaded()
        {
            if (ctrl != null)
            {
                ctrl.Enabled = true;
                menuEntry_ExportMissingEvents.Enabled = true;
                menuEntry_ExportChecklist.Enabled = true;

                var savData = SaveFileEditor.SAV;

                // Prevent usage if state is not Exportable
                if (!savData.State.Exportable)
                {
                    ctrl.Enabled = false;
                    return;
                }

                switch (savData.Version)
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
                        ctrl.Enabled = false;
                        break;


                    // Check for AS Demo
                    case GameVersion.AS:
                        {
                            if (savData is SAV6AODemo)
                            {
                                ctrl.Enabled = false;
                            }
                        }
                        break;


                    // Check for SN Demo
                    case GameVersion.SN:
                        {
                            var sav7 = (savData as SAV7SM);
                            if (sav7.BoxLayout.BoxesUnlocked == 8 && string.IsNullOrWhiteSpace(sav7.BoxLayout.GetBoxName(10)))
                            {
                                // Can't have a renamed box which is locked - must be Demo
                                ctrl.Enabled = false;
                            }
                        }
                        break;
                }
            }
        }

        public bool TryLoadFile(string filePath)
        {
            return false; // no action taken
        }
    }
}
