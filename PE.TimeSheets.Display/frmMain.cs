using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace PE.TimeSheets.Display
{
    public partial class frmMain : Form
    {
        BindingList<TimeSheet> lstTimeSheets;
        private bool blAddNewRow = false;
        private TimeSheet BckTimeSheet;

        public frmMain()
        {
            InitializeComponent();

            lnkNew.LinkBehavior = LinkBehavior.NeverUnderline;
            lnkSubmit.LinkBehavior = LinkBehavior.NeverUnderline;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();
            InitializelstTimeSheets();
            dgvTimeSheets.DataSource = lstTimeSheets;
        }

        private void InitializelstTimeSheets()
        {
            lstTimeSheets = new BindingList<TimeSheet>
            {
                new TimeSheet(1,State.Active, "Task 1", Type.TelephoneCall, new TimeSpan(0, 2, 10, 0), 250.50m),
                new TimeSheet(2,State.Submitted, "Task 2", Type.Research, new TimeSpan(0, 3, 05, 0), 120.00m)
            };
        }

        private void InitializeDataGridView()
        {
            dgvTimeSheets.AutoGenerateColumns = false;
            dgvTimeSheets.AutoSize = true;
            dgvTimeSheets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTimeSheets.EditMode = DataGridViewEditMode.EditOnEnter;

            DataGridViewColumn column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ID",
                Name = "ID",
                Visible = false
            };
            dgvTimeSheets.Columns.Add(column);

            column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "State",
                Name = "State"
            };
            dgvTimeSheets.Columns.Add(column);

            column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Title",
                Name = "Title"
            };
            dgvTimeSheets.Columns.Add(column);

            dgvTimeSheets.Columns.Add(CreateComboBoxWithEnums());

            column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Duration",
                Name = "Duration"
            };
            dgvTimeSheets.Columns.Add(column);

            column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HourlyRate",
                Name = "HourlyRate"
            };
            dgvTimeSheets.Columns.Add(column);

            column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Total",
                Name = "Total"
            };
            dgvTimeSheets.Columns.Add(column);

            column = new DataGridViewLinkColumn
            {
                DataPropertyName = "Action1",
                Name = "Action1",
                Text = "Text Action1",
                UseColumnTextForLinkValue = false
            };
            dgvTimeSheets.Columns.Add(column);

            column = new DataGridViewLinkColumn
            {
                DataPropertyName = "Action2",
                Name = "Action2",
                Text = "Text Action2",
                UseColumnTextForLinkValue = false
            };
            dgvTimeSheets.Columns.Add(column);
        }

        private DataGridViewComboBoxColumn CreateComboBoxWithEnums()
        {
            DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn
            {
                DataSource = Enum.GetValues(typeof(Type)),
                DataPropertyName = "Type",
                Name = "Type"
            };
            return combo;
        }

        private void DgvTimeSheets_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow r in dgvTimeSheets.Rows)
            {
                r.ReadOnly = true;
                if ((State)r.Cells["State"].Value == State.Active)
                {
                    if (r.IsNewRow)
                    {
                        r.Cells["Action1"].Value = "[ Save ]";
                        r.Cells["Action2"].Value = "[ Cancel ]";
                    }
                    else
                    {
                        r.Cells["Action1"].Value = "[ Edit ]";
                        r.Cells["Action2"].Value = "[ Delete ]";
                    }
                }
                else if ((State)r.Cells["State"].Value == State.Submitted)
                {
                    r.Cells["Action1"].Value = string.Empty;
                    r.Cells["Action2"].Value = string.Empty;
                }
            }
        }

        private void DgvTimeSheets_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTimeSheets.Columns[e.ColumnIndex] is DataGridViewLinkColumn && e.RowIndex != -1)
            {
                switch (dgvTimeSheets.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    case "[ Delete ]":
                        DeleteRecord((int)dgvTimeSheets.Rows[e.RowIndex].Cells["ID"].Value);
                        break;
                    case "[ Cancel ]":
                        CancelRecord((int)dgvTimeSheets.Rows[e.RowIndex].Cells["ID"].Value);
                        break;
                    case "[ Edit ]":
                        dgvTimeSheets.Rows[e.RowIndex].Cells["Title"].ReadOnly = false;
                        dgvTimeSheets.Rows[e.RowIndex].Cells["Type"].ReadOnly = false;
                        dgvTimeSheets.Rows[e.RowIndex].Cells["Duration"].ReadOnly = false;
                        dgvTimeSheets.Rows[e.RowIndex].Cells["HourlyRate"].ReadOnly = false;
                        dgvTimeSheets.Rows[e.RowIndex].Cells["Action1"].Value = "[ Save ]";
                        dgvTimeSheets.Rows[e.RowIndex].Cells["Action2"].Value = "[ Cancel ]";

                        SetFocusCell(e.RowIndex);

                        EditRecord((int)dgvTimeSheets.Rows[e.RowIndex].Cells["ID"].Value);
                        break;
                    case "[ Save ]":
                        SaveRecord((int)dgvTimeSheets.Rows[e.RowIndex].Cells["ID"].Value);
                        break;
                    default:
                        break;
                }
            }
        }

        private void DgvTimeSheets_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 5)//HourlyRate column
            {
                if (!decimal.TryParse(e.FormattedValue.ToString(), out decimal value))
                {
                    e.Cancel = true;
                    MessageBox.Show("The hourly rate field must be a number ", "Hourly Rate Format", MessageBoxButtons.OK);
                }
            }

            if (e.ColumnIndex == 4)//Duration column
            {
                if (!TimeSpan.TryParse(e.FormattedValue.ToString(), out TimeSpan time))
                {
                    e.Cancel = true;
                    MessageBox.Show("Enter time in the format h:mm", "Duration Format", MessageBoxButtons.OK);
                }
            }
        }

        void EditRecord(int id)
        {
            var item = lstTimeSheets.Select((Item, Index) => new { Item, Index })
                      .LastOrDefault(x => x.Item.ID == id);
            if (item != null)
            {
                BckTimeSheet = new TimeSheet(item.Item.ID, item.Item.State, item.Item.Title, item.Item.Type, TimeSpan.Parse(item.Item.Duration), item.Item.HourlyRate);
            }
        }

        void DeleteRecord(int id)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this timesheet entry?", "Delete Time Sheet Entry", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                var item = lstTimeSheets.SingleOrDefault(x => x.ID == id);
                if (item != null)
                    lstTimeSheets.Remove(item);
            }
        }

        void CancelRecord(int id)
        {
            if (blAddNewRow)
            {
                var item = lstTimeSheets.SingleOrDefault(x => x.ID == id);
                if (item != null)
                    lstTimeSheets.Remove(item);

                blAddNewRow = false;
            }
            else
            {
                var item = lstTimeSheets.Select((Item, Index) => new { Item, Index })
                      .LastOrDefault(x => x.Item.ID == id);
                if (item != null)
                {
                    lstTimeSheets[item.Index] = BckTimeSheet;
                }
            }

        }

        void SaveRecord(int id)
        {
            var item = lstTimeSheets.Select((Item, Index) => new { Item, Index })
                      .LastOrDefault(x => x.Item.ID == id);

            if (item != null)
            {
                lstTimeSheets[item.Index] = new TimeSheet(item.Item.ID, item.Item.State, item.Item.Title, item.Item.Type, TimeSpan.Parse(item.Item.Duration), item.Item.HourlyRate);
            }

            blAddNewRow = false;
        }

        private void LnkNew_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (blAddNewRow) return;

            int maxId = lstTimeSheets.Max(t => t.ID);

            lstTimeSheets.Insert(0, new TimeSheet(maxId + 1, Convert.ToDecimal(txtDefaultHourlyRate.Text)));

            dgvTimeSheets.Rows[0].Cells["Title"].ReadOnly = false;
            dgvTimeSheets.Rows[0].Cells["Type"].ReadOnly = false;
            dgvTimeSheets.Rows[0].Cells["Duration"].ReadOnly = false;
            dgvTimeSheets.Rows[0].Cells["HourlyRate"].ReadOnly = false;
            dgvTimeSheets.Rows[0].Cells["Action1"].Value = "[ Save ]";
            dgvTimeSheets.Rows[0].Cells["Action2"].Value = "[ Cancel ]";

            SetFocusCell(0);

            blAddNewRow = true;
        }

        private void LnkSubmit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (blAddNewRow) return;

            if (dgvTimeSheets.SelectedRows.Count > 0)
            {
                if ((State)dgvTimeSheets.SelectedRows[0].Cells[1].Value == State.Active)
                {
                    var item = lstTimeSheets.Select((Item, Index) => new { Item, Index })
                                      .LastOrDefault(x => x.Item.ID == (int)dgvTimeSheets.SelectedRows[0].Cells["ID"].Value);
                    if (item != null)
                    {
                        lstTimeSheets[item.Index] = new TimeSheet(item.Item.ID, State.Submitted, item.Item.Title, item.Item.Type, TimeSpan.Parse(item.Item.Duration), item.Item.HourlyRate);
                    }
                }
            }
        }

        private void TxtDefaultHourlyRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void SetFocusCell(int rowIndex)
        {
            dgvTimeSheets.CurrentCell = dgvTimeSheets.Rows[rowIndex].Cells["Title"];
            dgvTimeSheets.BeginEdit(true);
        }
    }
}
