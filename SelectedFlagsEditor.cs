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

            fieldItemsChk.Enabled = m_flagsOrganizer.SupportsEditingFlag(FlagsOrganizer.FlagType.FieldItem);
            hiddenItemsChk.Enabled = m_flagsOrganizer.SupportsEditingFlag(FlagsOrganizer.FlagType.HiddenItem);
            itemGiftsChk.Enabled = m_flagsOrganizer.SupportsEditingFlag(FlagsOrganizer.FlagType.ItemGift);
            pkmnGiftsChk.Enabled = m_flagsOrganizer.SupportsEditingFlag(FlagsOrganizer.FlagType.PkmnGift);
            trainerBattlesChk.Enabled = m_flagsOrganizer.SupportsEditingFlag(FlagsOrganizer.FlagType.TrainerBattle);
            staticEncounterChk.Enabled = m_flagsOrganizer.SupportsEditingFlag(FlagsOrganizer.FlagType.StaticBattle);
            inGameTradesChk.Enabled = m_flagsOrganizer.SupportsEditingFlag(FlagsOrganizer.FlagType.InGameTrade);
            sideEventsChk.Enabled = m_flagsOrganizer.SupportsEditingFlag(FlagsOrganizer.FlagType.SideEvent);
            miscEventsChk.Enabled = m_flagsOrganizer.SupportsEditingFlag(FlagsOrganizer.FlagType.GeneralEvent);
            berryTreesChk.Enabled = m_flagsOrganizer.SupportsEditingFlag(FlagsOrganizer.FlagType.BerryTree);
            collectablesChk.Enabled = m_flagsOrganizer.SupportsEditingFlag(FlagsOrganizer.FlagType.Collectable);
        }

        private void markFlagsBtn_Click(object sender, EventArgs e)
        {
            if (fieldItemsChk.Checked)
                m_flagsOrganizer.MarkFlags(FlagsOrganizer.FlagType.FieldItem);

            if (hiddenItemsChk.Checked)
                m_flagsOrganizer.MarkFlags(FlagsOrganizer.FlagType.HiddenItem);

            if (itemGiftsChk.Checked)
                m_flagsOrganizer.MarkFlags(FlagsOrganizer.FlagType.ItemGift);

            if (pkmnGiftsChk.Checked)
                m_flagsOrganizer.MarkFlags(FlagsOrganizer.FlagType.PkmnGift);
            
            if (trainerBattlesChk.Checked)
                m_flagsOrganizer.MarkFlags(FlagsOrganizer.FlagType.TrainerBattle);

            if (staticEncounterChk.Checked)
                m_flagsOrganizer.MarkFlags(FlagsOrganizer.FlagType.StaticBattle);

            if (inGameTradesChk.Checked)
                m_flagsOrganizer.MarkFlags(FlagsOrganizer.FlagType.InGameTrade);

            if (sideEventsChk.Checked)
                m_flagsOrganizer.MarkFlags(FlagsOrganizer.FlagType.SideEvent);

            if (miscEventsChk.Checked)
                m_flagsOrganizer.MarkFlags(FlagsOrganizer.FlagType.GeneralEvent);

            if (berryTreesChk.Checked)
                m_flagsOrganizer.MarkFlags(FlagsOrganizer.FlagType.BerryTree);

            if (collectablesChk.Checked)
                m_flagsOrganizer.MarkFlags(FlagsOrganizer.FlagType.Collectable);

            Close();
        }

        private void unmarkFlagsBtn_Click(object sender, EventArgs e)
        {
            if (fieldItemsChk.Checked)
                m_flagsOrganizer.UnmarkFlags(FlagsOrganizer.FlagType.FieldItem);

            if (hiddenItemsChk.Checked)
                m_flagsOrganizer.UnmarkFlags(FlagsOrganizer.FlagType.HiddenItem);

            if (itemGiftsChk.Checked)
                m_flagsOrganizer.UnmarkFlags(FlagsOrganizer.FlagType.ItemGift);

            if (pkmnGiftsChk.Checked)
                m_flagsOrganizer.UnmarkFlags(FlagsOrganizer.FlagType.PkmnGift);

            if (trainerBattlesChk.Checked)
                m_flagsOrganizer.UnmarkFlags(FlagsOrganizer.FlagType.TrainerBattle);

            if (staticEncounterChk.Checked)
                m_flagsOrganizer.UnmarkFlags(FlagsOrganizer.FlagType.StaticBattle);

            if (inGameTradesChk.Checked)
                m_flagsOrganizer.UnmarkFlags(FlagsOrganizer.FlagType.InGameTrade);

            if (sideEventsChk.Checked)
                m_flagsOrganizer.UnmarkFlags(FlagsOrganizer.FlagType.SideEvent);

            if (miscEventsChk.Checked)
                m_flagsOrganizer.UnmarkFlags(FlagsOrganizer.FlagType.GeneralEvent);

            if (berryTreesChk.Checked)
                m_flagsOrganizer.UnmarkFlags(FlagsOrganizer.FlagType.BerryTree);

            if (collectablesChk.Checked)
                m_flagsOrganizer.UnmarkFlags(FlagsOrganizer.FlagType.Collectable);

            Close();
        }
    }
}
