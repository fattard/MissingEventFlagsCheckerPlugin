namespace MissingEventFlagsCheckerPlugin
{
    public class MissingEventFlagsChecker : IPlugin
    {
        public string Name => "Missing Event Flags Checker";
        public int Priority => 100; // Loading order, lowest is first.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ISaveFileProvider SaveFileEditor { get; private set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private ToolStripMenuItem? ctrl;

        public void Initialize(params object[] args)
        {
            //LocalizedStrings.Initialize("br");
            LocalizedStrings.Initialize(GameInfo.CurrentLanguage);
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
            ctrl = new ToolStripMenuItem(Name)
            {
                Enabled = false
            };
            ctrl.Click += ChecklistViewer_UIEvt;
            tools.DropDownItems.Add(ctrl);
        }

        private void ChecklistViewer_UIEvt(object? sender, EventArgs e)
        {
            var form = new Forms.ChecklistViewer(SaveFileEditor.SAV);
            form.ShowDialog();
        }

        public void NotifySaveLoaded()
        {
            ctrl!.Enabled = true;

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

        }

        public bool TryLoadFile(string filePath)
        {
            return false; // no action taken
        }

    }

}
