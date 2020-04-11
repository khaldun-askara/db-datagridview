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
                        client_data[columnsName] = row.Cells[columnsName];
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
                if (dgv_clients.CurrentRow.Cells["id"].Value != null)
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
    }
}
