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
        EventFlagsOrganizer m_eventFlagsOrganizer;

        public SelectedFlagsEditor(EventFlagsOrganizer eventFlagsOrganizer)
        {
            m_eventFlagsOrganizer = eventFlagsOrganizer;

            InitializeComponent();

            fieldItemsChk.Enabled = m_eventFlagsOrganizer.SupportsEditingFlag(EventFlagsOrganizer.EventFlagType.FieldItem);
            hiddenItemsChk.Enabled = m_eventFlagsOrganizer.SupportsEditingFlag(EventFlagsOrganizer.EventFlagType.HiddenItem);
            itemGiftsChk.Enabled = m_eventFlagsOrganizer.SupportsEditingFlag(EventFlagsOrganizer.EventFlagType.ItemGift);
            pkmnGiftsChk.Enabled = m_eventFlagsOrganizer.SupportsEditingFlag(EventFlagsOrganizer.EventFlagType.PkmnGift);
            trainerBattlesChk.Enabled = m_eventFlagsOrganizer.SupportsEditingFlag(EventFlagsOrganizer.EventFlagType.TrainerBattle);
            staticEncounterChk.Enabled = m_eventFlagsOrganizer.SupportsEditingFlag(EventFlagsOrganizer.EventFlagType.StaticBattle);
            inGameTradesChk.Enabled = m_eventFlagsOrganizer.SupportsEditingFlag(EventFlagsOrganizer.EventFlagType.InGameTrade);
            sideEventsChk.Enabled = m_eventFlagsOrganizer.SupportsEditingFlag(EventFlagsOrganizer.EventFlagType.SideEvent);
            miscEventsChk.Enabled = m_eventFlagsOrganizer.SupportsEditingFlag(EventFlagsOrganizer.EventFlagType.GeneralEvent);
            berryTreesChk.Enabled = m_eventFlagsOrganizer.SupportsEditingFlag(EventFlagsOrganizer.EventFlagType.BerryTree);
            collectablesChk.Enabled = m_eventFlagsOrganizer.SupportsEditingFlag(EventFlagsOrganizer.EventFlagType.Collectable);
        }

        private void markFlagsBtn_Click(object sender, EventArgs e)
        {
            if (fieldItemsChk.Checked)
                m_eventFlagsOrganizer.MarkFlags(EventFlagsOrganizer.EventFlagType.FieldItem);

            if (hiddenItemsChk.Checked)
                m_eventFlagsOrganizer.MarkFlags(EventFlagsOrganizer.EventFlagType.HiddenItem);

            if (itemGiftsChk.Checked)
                m_eventFlagsOrganizer.MarkFlags(EventFlagsOrganizer.EventFlagType.ItemGift);

            if (pkmnGiftsChk.Checked)
                m_eventFlagsOrganizer.MarkFlags(EventFlagsOrganizer.EventFlagType.PkmnGift);
            
            if (trainerBattlesChk.Checked)
                m_eventFlagsOrganizer.MarkFlags(EventFlagsOrganizer.EventFlagType.TrainerBattle);

            if (staticEncounterChk.Checked)
                m_eventFlagsOrganizer.MarkFlags(EventFlagsOrganizer.EventFlagType.StaticBattle);

            if (inGameTradesChk.Checked)
                m_eventFlagsOrganizer.MarkFlags(EventFlagsOrganizer.EventFlagType.InGameTrade);

            if (sideEventsChk.Checked)
                m_eventFlagsOrganizer.MarkFlags(EventFlagsOrganizer.EventFlagType.SideEvent);

            if (miscEventsChk.Checked)
                m_eventFlagsOrganizer.MarkFlags(EventFlagsOrganizer.EventFlagType.GeneralEvent);

            if (berryTreesChk.Checked)
                m_eventFlagsOrganizer.MarkFlags(EventFlagsOrganizer.EventFlagType.BerryTree);

            if (collectablesChk.Checked)
                m_eventFlagsOrganizer.MarkFlags(EventFlagsOrganizer.EventFlagType.Collectable);

            Close();
        }

        private void unmarkFlagsBtn_Click(object sender, EventArgs e)
        {
            if (fieldItemsChk.Checked)
                m_eventFlagsOrganizer.UnmarkFlags(EventFlagsOrganizer.EventFlagType.FieldItem);

            if (hiddenItemsChk.Checked)
                m_eventFlagsOrganizer.UnmarkFlags(EventFlagsOrganizer.EventFlagType.HiddenItem);

            if (itemGiftsChk.Checked)
                m_eventFlagsOrganizer.UnmarkFlags(EventFlagsOrganizer.EventFlagType.ItemGift);

            if (pkmnGiftsChk.Checked)
                m_eventFlagsOrganizer.UnmarkFlags(EventFlagsOrganizer.EventFlagType.PkmnGift);

            if (trainerBattlesChk.Checked)
                m_eventFlagsOrganizer.UnmarkFlags(EventFlagsOrganizer.EventFlagType.TrainerBattle);

            if (staticEncounterChk.Checked)
                m_eventFlagsOrganizer.UnmarkFlags(EventFlagsOrganizer.EventFlagType.StaticBattle);

            if (inGameTradesChk.Checked)
                m_eventFlagsOrganizer.UnmarkFlags(EventFlagsOrganizer.EventFlagType.InGameTrade);

            if (sideEventsChk.Checked)
                m_eventFlagsOrganizer.UnmarkFlags(EventFlagsOrganizer.EventFlagType.SideEvent);

            if (miscEventsChk.Checked)
                m_eventFlagsOrganizer.UnmarkFlags(EventFlagsOrganizer.EventFlagType.GeneralEvent);

            if (berryTreesChk.Checked)
                m_eventFlagsOrganizer.UnmarkFlags(EventFlagsOrganizer.EventFlagType.BerryTree);

            if (collectablesChk.Checked)
                m_eventFlagsOrganizer.UnmarkFlags(EventFlagsOrganizer.EventFlagType.Collectable);

            Close();
        }
    }
}
