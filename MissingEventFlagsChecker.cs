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
        public string NameExportMissingFlags => "Export Missing flags";
        public string NameExportChecklistFlags => "Export Checklist";
        public string NameMarkFlags => "Mark Flags";
        public string NameUnMarkFlags => "Un-Mark Flags";
        public string NameDumpAllFlags => "Dump all Flags";
        public int Priority => 1; // Loading order, lowest is first.
        public ISaveFileProvider SaveFileEditor { get; private set; } = null;

        private ToolStripMenuItem ctrl;

        private ToolStripMenuItem menuEntry_ExporMissingFlags;
        private ToolStripMenuItem menuEntry_ExporChecklist;
        private ToolStripMenuItem menuEntry_MarkFlags;
        private ToolStripMenuItem menuEntry_UnMarkFlags;
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

            menuEntry_ExporMissingFlags = new ToolStripMenuItem(NameExportMissingFlags);
            menuEntry_ExporMissingFlags.Enabled = false;
            menuEntry_ExporMissingFlags.Click += new EventHandler(ExportMissingFlags);
            ctrl.DropDownItems.Add(menuEntry_ExporMissingFlags);

            menuEntry_ExporChecklist = new ToolStripMenuItem(NameExportChecklistFlags);
            menuEntry_ExporChecklist.Enabled = false;
            menuEntry_ExporChecklist.Click += new EventHandler(ExportChecklist);
            ctrl.DropDownItems.Add(menuEntry_ExporChecklist);

            menuEntry_DumpAllFlags = new ToolStripMenuItem(NameDumpAllFlags);
            menuEntry_DumpAllFlags.Enabled = false;
            menuEntry_DumpAllFlags.Click += new EventHandler(DumpAllFlags);
            ctrl.DropDownItems.Add(menuEntry_DumpAllFlags);

#if DEBUG
            menuEntry_MarkFlags = new ToolStripMenuItem(NameMarkFlags);
            menuEntry_MarkFlags.Enabled = false;
            menuEntry_MarkFlags.Click += new EventHandler(MarkFlags);
            ctrl.DropDownItems.Add(menuEntry_MarkFlags);

            menuEntry_UnMarkFlags = new ToolStripMenuItem(NameUnMarkFlags);
            menuEntry_UnMarkFlags.Enabled = false;
            menuEntry_UnMarkFlags.Click += new EventHandler(UnMarkFlags);
            ctrl.DropDownItems.Add(menuEntry_UnMarkFlags);
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


        private void MarkFlags(object sender, EventArgs e)
        {
            var flagsOrganizer = FlagsOrganizer.OrganizeFlags(SaveFileEditor.SAV);

            flagsOrganizer.MarkFlags(FlagsOrganizer.FlagType.FieldItem);
            flagsOrganizer.MarkFlags(FlagsOrganizer.FlagType.HiddenItem);
            flagsOrganizer.MarkFlags(FlagsOrganizer.FlagType.TrainerBattle);
        }


        private void UnMarkFlags(object sender, EventArgs e)
        {
            var flagsOrganizer = FlagsOrganizer.OrganizeFlags(SaveFileEditor.SAV);

            flagsOrganizer.UnmarkFlags(FlagsOrganizer.FlagType.FieldItem);
            flagsOrganizer.UnmarkFlags(FlagsOrganizer.FlagType.HiddenItem);
            flagsOrganizer.UnmarkFlags(FlagsOrganizer.FlagType.TrainerBattle);
        }

        private void DumpAllFlags(object sender, EventArgs e)
        {
            var flagsOrganizer = FlagsOrganizer.OrganizeFlags(SaveFileEditor.SAV);

            flagsOrganizer.DumpAllFlags();
        }



        public void NotifySaveLoaded()
        {
            var savData = SaveFileEditor.SAV;

            if (ctrl != null)
            {
                ctrl.Enabled = true;
                menuEntry_ExporMissingFlags.Enabled = true;
                menuEntry_ExporChecklist.Enabled = true;
                menuEntry_DumpAllFlags.Enabled = true;
#if DEBUG
                menuEntry_MarkFlags.Enabled = true;
                menuEntry_UnMarkFlags.Enabled = true;
#endif

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

                    // Check for SN Demo
                    case GameVersion.SN:
                        {
                            var sav7 = (savData as SAV7SM);
                            if (sav7.BoxLayout.BoxesUnlocked == 8 && string.IsNullOrWhiteSpace(sav7.BoxLayout.GetBoxName(10)))
                            {
                                // Can't rename a locked box - must be Demo
                                ctrl.Enabled = false;
                            }
                            else
                            {
                                menuEntry_ExporMissingFlags.Enabled = false;
                                menuEntry_ExporChecklist.Enabled = false;
                            }
                        }
                        break;


                    //TEMP: dump flags only
                    case GameVersion.GP:
                    case GameVersion.GE:
                    case GameVersion.BD:
                    case GameVersion.SP:
                    case GameVersion.BDSP:
                    case GameVersion.SW:
                    case GameVersion.SH:
                    case GameVersion.SWSH:
                    case GameVersion.PLA:
                    case GameVersion.SL:
                    case GameVersion.VL:
                    case GameVersion.SV:
                        menuEntry_ExporMissingFlags.Enabled = false;
                        menuEntry_ExporChecklist.Enabled = false;
                        break;
                }

#if DEBUG
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
