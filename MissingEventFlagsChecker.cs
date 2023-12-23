namespace MissingEventFlagsCheckerPlugin
{
    public class MissingEventFlagsChecker : IPlugin
    {
        public string Name => "Missing Event Flags Checker";
        public string NameExportMissingFlags => "Export only the missing events";
        public string NameExportChecklistFlags => "Export full Checklist";
        public string NameChecklistViewer => "Checklist Viewer";
        public int Priority => 100; // Loading order, lowest is first.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ISaveFileProvider SaveFileEditor { get; private set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private ToolStripMenuItem? ctrl;

        private ToolStripMenuItem? menuEntry_ChecklistViewer;
        private ToolStripMenuItem? menuEntry_ExportMissingEvents;
        private ToolStripMenuItem? menuEntry_ExportChecklist;

        public void Initialize(params object[] args)
        {
            SaveFileEditor = (ISaveFileProvider)Array.Find(args, z => z is ISaveFileProvider)!;
            var menu = (ToolStrip)Array.Find(args, z => z is ToolStrip)!;
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

            menuEntry_ChecklistViewer = new ToolStripMenuItem(NameChecklistViewer);
            menuEntry_ChecklistViewer.Enabled = false;
            menuEntry_ChecklistViewer.Click += ChecklistViewer_UIEvt;
            ctrl.DropDownItems.Add(menuEntry_ChecklistViewer);

            menuEntry_ExportChecklist = new ToolStripMenuItem(NameExportChecklistFlags);
            menuEntry_ExportChecklist.Enabled = false;
            menuEntry_ExportChecklist.Click += ExportChecklist_UIEvt;
            ctrl.DropDownItems.Add(menuEntry_ExportChecklist);

            menuEntry_ExportMissingEvents = new ToolStripMenuItem(NameExportMissingFlags);
            menuEntry_ExportMissingEvents.Enabled = false;
            menuEntry_ExportMissingEvents.Click += ExportMissingEvents_UIEvt;
            ctrl.DropDownItems.Add(menuEntry_ExportMissingEvents);
        }

        private void ExportMissingEvents_UIEvt(object? sender, EventArgs e)
        {
            var eventsChecker = EventFlagsChecker.CreateEventFlagsChecker(SaveFileEditor.SAV);
            var fileContent = eventsChecker.ExportMissingEvents();

            System.IO.File.WriteAllText(string.Format("missing_events_{0}.txt", SaveFileEditor.SAV.Version), fileContent);
        }

        private void ExportChecklist_UIEvt(object? sender, EventArgs e)
        {
            var eventsChecker = EventFlagsChecker.CreateEventFlagsChecker(SaveFileEditor.SAV);
            var fileContent = eventsChecker.ExportChecklist();

            System.IO.File.WriteAllText(string.Format("checklist_{0}.txt", SaveFileEditor.SAV.Version), fileContent);
        }

        private void ChecklistViewer_UIEvt(object? sender, EventArgs e)
        {
            var form = new Forms.ChecklistViewer(SaveFileEditor.SAV);
            form.ShowDialog();
        }

        public void NotifySaveLoaded()
        {
            ctrl!.Enabled = true;
            menuEntry_ChecklistViewer!.Enabled = true;
            menuEntry_ExportMissingEvents!.Enabled = true;
            menuEntry_ExportChecklist!.Enabled = true;

            var savData = SaveFileEditor.SAV;

            // Prevent usage if state is not Exportable
            if (!savData.State.Exportable)
            {
                ctrl.Enabled = false;
                return;
            }

            ctrl.Enabled = savData.Version switch
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
                    => false,

                // Check for AS Demo
                GameVersion.AS
                    => savData is not SAV6AODemo,

                // Check for SN Demo
                GameVersion.SN
                    // Can't have a renamed box which is locked in non-demo version
                    => !(((SAV7SM)savData).BoxLayout.BoxesUnlocked == 8 && string.IsNullOrWhiteSpace(((SAV7SM)savData).BoxLayout.GetBoxName(10))),

                _ => true
            };

#if DEBUG
            // Quick export full checklist on load during DEBUG
            if (ctrl.Enabled)
            {
                ExportChecklist_UIEvt(null, new EventArgs());
            }
#endif
        }

        public bool TryLoadFile(string filePath)
        {
            return false; // no action taken
        }

    }

}
