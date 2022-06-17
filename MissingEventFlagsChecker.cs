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
        public string NameRunChecker => "Run Checker";
        public string NameMarkFlags => "Mark Flags";
        public string NameUnMarkFlags => "Un-Mark Flags";
        public string NameDumpAllFlags => "Dump all Flags";
        public int Priority => 1; // Loading order, lowest is first.
        public ISaveFileProvider SaveFileEditor { get; private set; } = null;

        private ToolStripMenuItem ctrl;

        private ToolStripMenuItem menuEntry_Checker;
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

            menuEntry_Checker = new ToolStripMenuItem(NameRunChecker);
            menuEntry_Checker.Enabled = false;
            menuEntry_Checker.Click += new EventHandler(RunChecker);
            ctrl.DropDownItems.Add(menuEntry_Checker);

            menuEntry_MarkFlags = new ToolStripMenuItem(NameMarkFlags);
            menuEntry_MarkFlags.Enabled = false;
            menuEntry_MarkFlags.Click += new EventHandler(MarkFlags);
            ctrl.DropDownItems.Add(menuEntry_MarkFlags);

            menuEntry_UnMarkFlags = new ToolStripMenuItem(NameUnMarkFlags);
            menuEntry_UnMarkFlags.Enabled = false;
            menuEntry_UnMarkFlags.Click += new EventHandler(UnMarkFlags);
            ctrl.DropDownItems.Add(menuEntry_UnMarkFlags);

            menuEntry_DumpAllFlags = new ToolStripMenuItem(NameDumpAllFlags);
            menuEntry_DumpAllFlags.Enabled = false;
            menuEntry_DumpAllFlags.Click += new EventHandler(DumpAllFlags);
            ctrl.DropDownItems.Add(menuEntry_DumpAllFlags);
        }

        private void RunChecker(object sender, EventArgs e)
        {
            var flagsOrganizer = FlagsOrganizer.OrganizeFlags(SaveFileEditor.SAV);

            if (flagsOrganizer == null)
            {
                throw new FormatException("Unsupported SAV format: " + SaveFileEditor.SAV.Version);
            }

            flagsOrganizer.ExportMissingFlags();
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
                menuEntry_Checker.Enabled = true;
                menuEntry_MarkFlags.Enabled = true;
                menuEntry_UnMarkFlags.Enabled = true;
                menuEntry_DumpAllFlags.Enabled = true;
            }
                
        }

        public bool TryLoadFile(string filePath)
        {
            return false; // no action taken
        }

    }

}
