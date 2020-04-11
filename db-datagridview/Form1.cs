using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace db_datagridview
{
    public partial class client_coach : Form
    {
        public client_coach()
        {
            InitializeComponent();
            database_funcs.InitializeDGVClients(dgv_clients);
            database_funcs.InitializeDGVCoaches(dgv_coaches);
        }
        private void dgv_clients_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            long temp = 0;
            var row = dgv_clients.Rows[e.RowIndex];
            if (!dgv_clients.IsCurrentRowDirty)
                return;
            row.ErrorText = "";
                
            var cellsWithPotentialErrors = new[] {row.Cells["client_name"],
                                                   row.Cells["client_phone"],
                                                   row.Cells["client_email"]};
            foreach (var cell in cellsWithPotentialErrors)
            {
                cell.ErrorText = "";
                if (string.IsNullOrWhiteSpace((string)cell.Value))
                {
                    cell.ErrorText = "Значение не может быть пустым";
                    e.Cancel = true;
                }
            }
            row.Cells["client_passport"].ErrorText = "";
            if (row.Cells["client_passport"] == null || !Int64.TryParse(Convert.ToString(row.Cells["client_passport"].Value), out temp))
            {
                row.Cells["client_passport"].ErrorText = "Введите число";
                e.Cancel = true;
            }

            if (!e.Cancel)
            {
                var client_id = (int?)row.Cells["client_id"].Value;
                if (client_id.HasValue)
                    database_funcs.UpdateClient(Convert.ToInt32(row.Cells["client_id"].Value),
                                               (string)row.Cells["client_name"].Value,
                                               (DateTime)row.Cells["client_birthday"].Value,
                                               Convert.ToInt64(row.Cells["client_passport"].Value),
                                               (string)row.Cells["client_phone"].Value,
                                               (string)row.Cells["client_email"].Value);
                else
                {
                    row.Cells["client_id"].Value = database_funcs.InsertClient((string)row.Cells["client_name"].Value,
                                               (DateTime)row.Cells["client_birthday"].Value,
                                               Convert.ToInt64(row.Cells["client_passport"].Value),
                                               (string)row.Cells["client_phone"].Value,
                                               (string)row.Cells["client_email"].Value);
                    var client_data = new Dictionary<string, object>();
                    foreach (var columnsName in new[] { "client_name",
                                                        "client_birthday",
                                                        "client_passport",
                                                        "client_phone",
                                                        "client_email"})
                    {
                        client_data[columnsName] = row.Cells[columnsName].Value;
                    }
                    row.Tag = client_data;
                }
            }
        }
        private void dgv_clients_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var client_id = (int?)e.Row.Cells["client_id"].Value;
            if (client_id.HasValue)
                database_funcs.DeleteClient(client_id.Value);
        }
        private void dgv_clients_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && dgv_clients.IsCurrentRowDirty)
            {
                dgv_clients.CancelEdit();
                if (dgv_clients.CurrentRow.Cells["client_id"].Value != null)
                {
                    dgv_clients.CurrentRow.ErrorText = "";
                    foreach (var kvp in (Dictionary<string, object>)dgv_clients.CurrentRow.Tag)
                    {
                        dgv_clients.CurrentRow.Cells[kvp.Key].Value = kvp.Value;
                        dgv_clients.CurrentRow.Cells[kvp.Key].ErrorText = "";
                    }
                }
                else
                {
                    dgv_clients.Rows.Remove(dgv_clients.CurrentRow);
                }
            }
        }
        private void dgv_coaches_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && dgv_coaches.IsCurrentRowDirty)
            {
                dgv_coaches.CancelEdit();
                if (dgv_coaches.CurrentRow.Cells["coach_id"].Value != null)
                {
                    dgv_coaches.CurrentRow.ErrorText = "";
                    foreach (var kvp in (Dictionary<string, object>)dgv_coaches.CurrentRow.Tag)
                    {
                        if (kvp.Key == "coach_coach_type_id") continue;
                        dgv_coaches.CurrentRow.Cells[kvp.Key].Value = kvp.Value;
                        dgv_coaches.CurrentRow.Cells[kvp.Key].ErrorText = "";
                    }
                    ((DataGridViewComboBoxCell)dgv_coaches.CurrentRow.Cells["coach_coach_type_id"]).Value = ((Dictionary<string, object>)dgv_coaches.CurrentRow.Tag)["coach_coach_type_id"];
                }
                else
                {
                    dgv_coaches.Rows.Remove(dgv_coaches.CurrentRow);
                }
            }
        }
        private void dgv_coaches_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var coach_id = (int?)e.Row.Cells["coach_id"].Value;
            if (coach_id.HasValue)
                database_funcs.DeleteCoach(coach_id.Value);
        }
        private void dgv_coaches_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            long temp = 0;
            int temp2 = 0;
            var row = dgv_coaches.Rows[e.RowIndex];
            if (!dgv_coaches.IsCurrentRowDirty)
                return;
            row.ErrorText = "";

            var cellsWithPotentialErrors = new[] {row.Cells["coach_name"],
                                                   row.Cells["coach_phone"]};
            foreach (var cell in cellsWithPotentialErrors)
            {
                cell.ErrorText = "";
                if (string.IsNullOrWhiteSpace((string)cell.Value))
                {
                    cell.ErrorText = "Значение не может быть пустым";
                    e.Cancel = true;
                }
            }
            row.Cells["coach_passport"].ErrorText = "";
            if (row.Cells["coach_passport"] == null || !Int64.TryParse(Convert.ToString(row.Cells["coach_passport"].Value), out temp))
            {
                row.Cells["coach_passport"].ErrorText = "Введите число";
                e.Cancel = true;
            }

            row.Cells["coach_tin"].ErrorText = "";
            if (row.Cells["coach_tin"] == null || !Int64.TryParse(Convert.ToString(row.Cells["coach_tin"].Value), out temp))
            {
                row.Cells["coach_tin"].ErrorText = "Введите число";
                e.Cancel = true;
            }

            row.Cells["coach_salary"].ErrorText = "";
            if (row.Cells["coach_salary"] == null || !Int32.TryParse(Convert.ToString(row.Cells["coach_salary"].Value), out temp2))
            {
                row.Cells["coach_salary"].ErrorText = "Введите число";
                e.Cancel = true;
            }

            if (!e.Cancel)
            {
                var coach_id = (int?)row.Cells["coach_id"].Value;
                if (coach_id.HasValue)
                    database_funcs.UpdateCoach(Convert.ToInt32(row.Cells["coach_id"].Value),
                                               (string)row.Cells["coach_name"].Value,
                                               (DateTime)row.Cells["coach_birthday"].Value,
                                               Convert.ToInt64(row.Cells["coach_passport"].Value),
                                               Convert.ToInt64(row.Cells["coach_tin"].Value),
                                               (string)row.Cells["coach_phone"].Value,
                                               Convert.ToInt32(row.Cells["coach_salary"].Value),
                                               Convert.ToInt32(row.Cells["coach_coach_type_id"].Value));
                else
                {
                    row.Cells["coach_id"].Value = database_funcs.InsertCoach((string)row.Cells["coach_name"].Value,
                                               (DateTime)row.Cells["coach_birthday"].Value,
                                               Convert.ToInt64(row.Cells["coach_passport"].Value),
                                               Convert.ToInt64(row.Cells["coach_tin"].Value),
                                               (string)row.Cells["coach_phone"].Value,
                                               Convert.ToInt32(row.Cells["coach_salary"].Value),
                                               Convert.ToInt32(row.Cells["coach_coach_type_id"].Value));
                    var coach_data = new Dictionary<string, object>();
                    foreach (var columnsName in new[] { "coach_name",
                                                        "coach_birthday",
                                                        "coach_passport",
                                                        "coach_tin",
                                                        "coach_phone",
                                                        "coach_salary",
                                                        "coach_coach_type_id"})
                    {
                        coach_data[columnsName] = row.Cells[columnsName].Value;
                    }
                    row.Tag = coach_data;
                }
            }
        }
    }
}
