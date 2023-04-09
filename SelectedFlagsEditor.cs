using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    public partial class SelectedFlagsEditor : Form
    {
        FlagsOrganizer m_flagsOrganizer;

        public SelectedFlagsEditor(FlagsOrganizer flagsOrganizer)
        {
            m_flagsOrganizer = flagsOrganizer;

            InitializeComponent();
        }

        private void markFlagsBtn_Click(object sender, EventArgs e)
        {
            if (fieldItemsChk.Checked)
                m_flagsOrganizer.MarkFlags(FlagsOrganizer.FlagType.FieldItem);

            if (hiddenItemsChk.Checked)
                m_flagsOrganizer.MarkFlags(FlagsOrganizer.FlagType.HiddenItem);

            if (trainerBattlesChk.Checked)
                m_flagsOrganizer.MarkFlags(FlagsOrganizer.FlagType.TrainerBattle);

            Close();
        }

        private void unmarkFlagsBtn_Click(object sender, EventArgs e)
        {
            if (fieldItemsChk.Checked)
                m_flagsOrganizer.UnmarkFlags(FlagsOrganizer.FlagType.FieldItem);

            if (hiddenItemsChk.Checked)
                m_flagsOrganizer.UnmarkFlags(FlagsOrganizer.FlagType.HiddenItem);

            if (trainerBattlesChk.Checked)
                m_flagsOrganizer.UnmarkFlags(FlagsOrganizer.FlagType.TrainerBattle);

            Close();
        }
    }
}
