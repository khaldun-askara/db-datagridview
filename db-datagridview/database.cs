using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows.Forms;

namespace db_datagridview
{
    class database_funcs
    {
        private static readonly string _sConnStr = new NpgsqlConnectionStringBuilder
        {
            Host = database.Default.Host,
            Port = database.Default.Port,
            Database = database.Default.Name,
            Username = Environment.GetEnvironmentVariable("POSTGRESQL_USERNAME"),
            Password = Environment.GetEnvironmentVariable("POSTGRESQL_PASSWORD"),
            AutoPrepareMinUsages = 2,
            MaxAutoPrepare = 10
        }.ConnectionString;

        public static void InitializeDGVClients(DataGridView dgv_clients)
        {
            dgv_clients.Rows.Clear();
            dgv_clients.Columns.Clear();
            dgv_clients.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "client_id",
                Visible = false
            });
            dgv_clients.Columns.Add("client_name", "ФИО клиента");
            dgv_clients.Columns.Add(new CalendarColumn
            {
                Name = "client_birthday",
                HeaderText = "Дата рождения"
            });
            dgv_clients.Columns.Add("client_passport", "Номер паспорта");
            dgv_clients.Columns.Add("client_phone", "Номер телефона");
            dgv_clients.Columns.Add("client_email", "E-mail");
            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = "SELECT * FROM client"
                };
                var reader = sCommand.ExecuteReader();
                while (reader.Read())
                {
                    var client_data = new Dictionary<string, object>();
                    foreach (var columnsName in new[] { "client_name",
                                                        "client_birthday",
                                                        "client_passport",
                                                        "client_phone",
                                                        "client_email"})
                    {
                        client_data[columnsName] = reader[columnsName];
                    }

                    var row_idx = dgv_clients.Rows.Add(reader["client_id"],
                                                        reader["client_name"],
                                                        reader["client_birthday"],
                                                        reader["client_passport"],
                                                        reader["client_phone"],
                                                        reader["client_email"]);
                    dgv_clients.Rows[row_idx].Tag = client_data;
                }
            }
        }
    }
}
