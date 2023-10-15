using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    public class MissingEventFlagsChecker : IPlugin
    {
        public string Name => "Missing Event Flags Checker";
        public string NameExportMissingFlags => "Export only the missing flags";
        public string NameExportChecklistFlags => "Export full Checklist";
        public string NameEditFlags => "Edit flags...";
        public string NameDumpAllFlags => "Dump all Flags";
        public int Priority => 1; // Loading order, lowest is first.
        public ISaveFileProvider SaveFileEditor { get; private set; } = null;

        private ToolStripMenuItem ctrl;

        private ToolStripMenuItem menuEntry_ExporMissingFlags;
        private ToolStripMenuItem menuEntry_ExporChecklist;
        private ToolStripMenuItem menuEntry_EditFlags;
        private ToolStripMenuItem menuEntry_DumpAllFlags;

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

            menuEntry_ExporChecklist = new ToolStripMenuItem(NameExportChecklistFlags);
            menuEntry_ExporChecklist.Enabled = false;
            menuEntry_ExporChecklist.Click += new EventHandler(ExportChecklist);
            ctrl.DropDownItems.Add(menuEntry_ExporChecklist);

            menuEntry_ExporMissingFlags = new ToolStripMenuItem(NameExportMissingFlags);
            menuEntry_ExporMissingFlags.Enabled = false;
            menuEntry_ExporMissingFlags.Click += new EventHandler(ExportMissingFlags);
            ctrl.DropDownItems.Add(menuEntry_ExporMissingFlags);

            menuEntry_DumpAllFlags = new ToolStripMenuItem(NameDumpAllFlags);
            menuEntry_DumpAllFlags.Enabled = false;
            menuEntry_DumpAllFlags.Click += new EventHandler(DumpAllFlags);
            ctrl.DropDownItems.Add(menuEntry_DumpAllFlags);

            menuEntry_EditFlags = new ToolStripMenuItem(NameEditFlags);
            menuEntry_EditFlags.Enabled = false;
            menuEntry_EditFlags.Click += new EventHandler(EditFlags);
#if DEBUG
            ctrl.DropDownItems.Add(menuEntry_EditFlags);
#endif
        }

        private void ExportMissingFlags(object sender, EventArgs e)
        {
            var flagsOrganizer = FlagsOrganizer.OrganizeFlags(SaveFileEditor.SAV);

            if (flagsOrganizer == null)
            {
                throw new FormatException("Unsupported SAV format: " + SaveFileEditor.SAV.Version);
            }

            flagsOrganizer.ExportMissingFlags();
        }


        private void ExportChecklist(object sender, EventArgs e)
        {
            var flagsOrganizer = FlagsOrganizer.OrganizeFlags(SaveFileEditor.SAV);

            if (flagsOrganizer == null)
            {
                throw new FormatException("Unsupported SAV format: " + SaveFileEditor.SAV.Version);
            }

            flagsOrganizer.ExportChecklist();
        }


        private void DumpAllFlags(object sender, EventArgs e)
        {
            var flagsOrganizer = FlagsOrganizer.OrganizeFlags(SaveFileEditor.SAV);

            if (flagsOrganizer == null)
            {
                throw new FormatException("Unsupported SAV format: " + SaveFileEditor.SAV.Version);
            }

            flagsOrganizer.DumpAllFlags();
        }


        private void EditFlags(object sender, EventArgs e)
        {
            var flagsOrganizer = FlagsOrganizer.OrganizeFlags(SaveFileEditor.SAV);

            if (flagsOrganizer == null)
            {
                throw new FormatException("Unsupported SAV format: " + SaveFileEditor.SAV.Version);
            }

            var form = new SelectedFlagsEditor(flagsOrganizer);
            form.ShowDialog();
        }


        public void NotifySaveLoaded()
        {
            if (ctrl != null)
            {
                ctrl.Enabled = true;
                menuEntry_ExporMissingFlags.Enabled = true;
                menuEntry_ExporChecklist.Enabled = true;
                menuEntry_DumpAllFlags.Enabled = true;
                menuEntry_EditFlags.Enabled = true;

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


                    //TEMP: dump flags only
                    case GameVersion.PLA:
                        menuEntry_ExporMissingFlags.Enabled = false;
                        menuEntry_ExporChecklist.Enabled = false;
                        menuEntry_EditFlags.Enabled = false;
                        break;
                }

#if DEBUG
                // Quick dump all flags on load during DEBUG
                if (ctrl.Enabled)
                {
                    DumpAllFlags(null, null);
                }
#endif

            }
                
        }

        public bool TryLoadFile(string filePath)
        {
            return false; // no action taken
        }

    }

}
