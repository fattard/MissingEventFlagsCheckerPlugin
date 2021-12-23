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
        public string Name => "Run Missing Event Flags Checker";
        public int Priority => 1; // Loading order, lowest is first.
        public ISaveFileProvider SaveFileEditor { get; private set; } = null;

        private ToolStripMenuItem ctrl;

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
            tools.DropDownItems.Add(ctrl);
            //ctrl.Image = Properties.Resources.icon;
            ctrl.Click += new EventHandler(RunChecker);
            ctrl.Enabled = false;
        }

        private void RunChecker(object sender, EventArgs e)
        {
            switch (SaveFileEditor.SAV.Version)
            {
                case GameVersion.RD:
                case GameVersion.GN:
                case GameVersion.RB:
                    FlagsGen1RB.ExportFlags(SaveFileEditor.SAV);
                    break;

                case GameVersion.YW:
                    FlagsGen1Y.ExportFlags(SaveFileEditor.SAV);
                    break;

                case GameVersion.R:
                case GameVersion.S:
                case GameVersion.RS:
                    FlagsGen3RS.ExportFlags(SaveFileEditor.SAV);
                    break;

                case GameVersion.FR:
                case GameVersion.LG:
                case GameVersion.FRLG:
                    FlagsGen3FRLG.ExportFlags(SaveFileEditor.SAV);
                    break;

                case GameVersion.E:
                    FlagsGen3E.ExportFlags(SaveFileEditor.SAV);
                    break;

                default:
                    break;
            }
            
            //DumpAllFlags();
        }

        public void NotifySaveLoaded()
        {
            var savData = SaveFileEditor.SAV;

            if (ctrl != null)
            {
                ctrl.Enabled = true;
            }
                
        }

        public bool TryLoadFile(string filePath)
        {
            return false; // no action taken
        }


        void DumpAllFlags()
        {
            var flags = SaveFileEditor.SAV.GetEventFlags();

            StringBuilder sb = new StringBuilder(flags.Length);

            for (int i = 0; i < flags.Length; ++i)
            {
                sb.AppendFormat("FLAG_0x{0:X4} {1}\n", i, flags[i]);
            }

            System.IO.File.WriteAllText(string.Format("flags_dump_{0}.txt", SaveFileEditor.SAV.Version), sb.ToString());
        }
    }

}
