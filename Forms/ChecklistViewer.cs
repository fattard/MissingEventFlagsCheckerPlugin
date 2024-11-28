using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using static MissingEventFlagsCheckerPlugin.EventFlagsChecker;

namespace MissingEventFlagsCheckerPlugin.Forms
{
    public partial class ChecklistViewer : Form
    {
        EventFlagsChecker m_checker;
        List<EventDetail> m_checkerList;
        readonly Assembly? m_flagsEditorAssembly;
        EventFlagType m_categoryFilter;

        public ChecklistViewer(SaveFile saveFile)
        {
            InitializeComponent();
            LocalizedStrings.LocalizeForm(this);

            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            filterByCategoryCombo.Items.Add(LocalizedStrings.Find("ChecklistViewer.flagsCategoryComboAll", "- All -"));
            for (EventFlagType i = (EventFlagType._Unknown) + 1; i < EventFlagType._Unused; i++)
            {
                filterByCategoryCombo.Items.Add(i.AsLocalizedText());
            }

            try
            {
                m_flagsEditorAssembly = Assembly.LoadFrom("plugins/FlagsEditorEX.dll");

                if (m_flagsEditorAssembly is not null)
                {
                    editEventsBtn.Enabled = true;
                    getFlagsEditorExLinkLabel.Visible = false;
                }
            }
            catch (Exception)
            {
            }

            ReloadAllData(saveFile);
            RefreshTotals();
            filterByCategoryCombo.SelectedIndex = 0; // will trigger RefreshDataGrid()
        }

        [MemberNotNull(nameof(m_checker), nameof(m_checkerList))]
        private void ReloadAllData(SaveFile saveFile)
        {
            m_checker = EventFlagsChecker.CreateEventFlagsChecker(saveFile);
            m_checkerList = new List<EventDetail>(m_checker.EventsChecklist.Count);

            m_checkerList.Clear();

            foreach (var evt in m_checker.EventsChecklist)
            {
                if (evt.EvtTypeVal != EventFlagType._Unused && evt.EvtTypeVal != EventFlagType._Separator && (showTimedEventsChk.Checked || !evt.IsTimedEvent))
                {
                    m_checkerList.Add(evt);
                }
            }
        }

        private void GetFlagsEditorExLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/fattard/FlagsEditorEXPlugin/releases/latest") { UseShellExecute = true });
        }

        private void EditEventsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var flagsOrganizerType = m_flagsEditorAssembly!.GetType("FlagsEditorEXPlugin.FlagsOrganizer");
                var flagsEditorMainWinType = m_flagsEditorAssembly!.GetType("FlagsEditorEXPlugin.Forms.MainWin");
                var flagsOrganizerInstance = flagsOrganizerType!.GetMethod("CreateFlagsOrganizer")!.Invoke(null, [m_checker.SaveFile, null]);
                var flagsEditorMainWinInstance = (Form)Activator.CreateInstance(flagsEditorMainWinType!, flagsOrganizerInstance)!;
                flagsEditorMainWinInstance.ShowDialog();
                flagsEditorMainWinInstance.Dispose();

                ReloadAllData(m_checker.SaveFile!);
                RefreshTotals();
                RefreshDataGrid();
            }
            catch (Exception)
            {
            }

        }

        private void ExportCurrentViewBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Text File|*.txt",
                FileName = string.Format("checklist_custom_{0}.txt", m_checker.SaveFile!.Version),
            };
            var result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder(512 * 1024);

                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    var idx = ((int?)dataGridView.Rows[i].Cells[0].Value).Value - 1;

                    var evt = m_checkerList[idx];

                    if (evt.EvtTypeVal != EventFlagType._Separator)
                    {
                        sb.AppendFormat("[{0}] {1}\r\n", evt.IsDone ? "x" : " ", evt.ToString());
                    }
                    else
                    {
                        sb.Append("\r\n");
                    }
                }

                System.IO.File.WriteAllText(saveFileDialog.FileName, sb.ToString());
            }
        }

        private void ExportFullChecklistBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Text File|*.txt",
                FileName = string.Format("checklist_{0}.txt", m_checker.SaveFile!.Version),
            };
            var result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                System.IO.File.WriteAllText(saveFileDialog.FileName, m_checker.ExportChecklist(showTimedEventsChk.Checked));
            }
        }

        private void ExportMissingEventsBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Text File|*.txt",
                FileName = string.Format("missing_events_{0}.txt", m_checker.SaveFile!.Version),
            };
            var result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                System.IO.File.WriteAllText(saveFileDialog.FileName, m_checker.ExportMissingEvents(showTimedEventsChk.Checked));
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ShowTimedEventsChk_CheckedChanged(object sender, EventArgs e)
        {
            ReloadAllData(m_checker.SaveFile!);
            RefreshTotals();
            RefreshDataGrid();
        }

        private void ShowOnlyMarkedChk_CheckedChanged(object sender, EventArgs e)
        {
            if (showOnlyMarkedChk.Checked)
            {
                showOnlyUnmarkedChk.Checked = false;
            }

            RefreshDataGrid();
        }

        private void ShowOnlyUnmarkedChk_CheckedChanged(object sender, EventArgs e)
        {
            if (showOnlyUnmarkedChk.Checked)
            {
                showOnlyMarkedChk.Checked = false;
            }

            RefreshDataGrid();
        }

        private void FilterBySearchChk_CheckedChanged(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void FilterByCategoryCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_categoryFilter = (EventFlagType)filterByCategoryCombo.SelectedIndex;

            RefreshDataGrid();
        }

        private void SearchTermBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!filterBySearchChk.Checked)
                {
                    filterBySearchChk.Checked = true;
                }
                // Force event if already checked
                else
                {
                    FilterBySearchChk_CheckedChanged(sender, new EventArgs());
                }
            }
        }

        private void RefreshTotals()
        {
            int totalRows = m_checkerList.Count;
            int totalSet = 0;
            int totalUnset = 0;

            for (int i = 0; i < m_checkerList.Count; i++)
            {
                if (m_checkerList[i].IsDone)
                {
                    totalSet++;
                }
                else
                {
                    totalUnset++;
                }
            }

            totalsCountTxt.Text = string.Format("{0:D4}/{1:D4}", totalSet, totalRows);
            totalsCountTxt.ForeColor = totalSet == totalRows ? Color.Green : SystemColors.ControlText;
        }

        private void RefreshDataGrid()
        {
            this.SuspendLayout();

            bool skipSet = showOnlyUnmarkedChk.Checked;
            bool skipUnset = showOnlyMarkedChk.Checked;
            bool useCategoryFilter = (m_categoryFilter != EventFlagType._Unknown);
            bool filterBySearch = filterBySearchChk.Checked && !string.IsNullOrWhiteSpace(searchTermBox.Text);

            List<DataGridViewRow> rowsToAdd = [];

            bool isNegatedSearch = searchTermBox.Text.StartsWith("^");
            string searchTerm = searchTermBox.Text.ToUpperInvariant().Replace("^", "");

            for (int i = 0; i < m_checkerList.Count; i++)
            {
                var evt = m_checkerList[i];

                if ((skipSet && evt.IsDone) || (skipUnset && !evt.IsDone))
                {
                    continue;
                }

                if (useCategoryFilter && evt.EvtTypeVal != m_categoryFilter)
                {
                    continue;
                }

                if (filterBySearch)
                {
                    if (!isNegatedSearch && !evt.ToString().Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase))
                    {
                        continue;
                    }
                    else if (isNegatedSearch && evt.ToString().Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase))
                    {
                        continue;
                    }
                }

                var curRow = new DataGridViewRow();
                curRow.CreateCells(dataGridView);
                curRow.Cells[0].Value = i + 1;
                curRow.Cells[1].Value = evt.IsDone;
                curRow.Cells[2].Value = evt.EvtTypeVal.AsText();
                curRow.Cells[3].Value = evt.Location;
                curRow.Cells[4].Value = evt.DescTxt;

                rowsToAdd.Add(curRow);
            }

            dataGridView.Rows.Clear();
            dataGridView.Refresh();

            dataGridView.Rows.AddRange([.. rowsToAdd]);

            this.ResumeLayout(false);
        }
    }
}
