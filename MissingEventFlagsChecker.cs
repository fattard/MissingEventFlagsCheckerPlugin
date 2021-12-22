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

        bool[] m_flags;
        GameVersion m_gameVersion;
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
            switch (m_gameVersion)
            {
                case GameVersion.R:
                case GameVersion.S:
                case GameVersion.RS:
                    FlagsGen3RS.ExportFlags(m_flags, m_gameVersion);
                    break;

                case GameVersion.FR:
                case GameVersion.LG:
                case GameVersion.FRLG:
                    FlagsGen3FRLG.ExportFlags(m_flags, m_gameVersion);
                    break;

                case GameVersion.E:
                    FlagsGen3E.ExportFlags(m_flags, m_gameVersion);
                    break;

                default:
                    break;
            }
            
            DumpAllFlags();
        }

        public void NotifySaveLoaded()
        {
            var savData = SaveFileEditor.SAV;
            m_flags = savData.GetEventFlags();
            m_gameVersion = savData.Version;

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
            StringBuilder sb = new StringBuilder(m_flags.Length);

            for (int i = 0; i < m_flags.Length; ++i)
            {
                sb.AppendFormat("FLAG_0x{0:X4} {1}\n", i, m_flags[i]);
            }

            System.IO.File.WriteAllText(string.Format("flags_dump_{0}.txt", m_gameVersion), sb.ToString());
        }
    }

}
