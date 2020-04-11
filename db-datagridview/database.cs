using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows.Forms;
using System.Data;

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
        private static DataTable GetCoachTypes()
        {
            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                using (var sCommand = new NpgsqlCommand())
                {
                    sCommand.Connection = sConn;
                    sCommand.CommandText = "SELECT * FROM coach_type";
                    var table = new DataTable();
                    table.Load(sCommand.ExecuteReader());
                    return table;
                }
            }
        }
        public static void InitializeDGVCoaches(DataGridView dgv_coaches)
        {
            dgv_coaches.Rows.Clear();
            dgv_coaches.Columns.Clear();
            dgv_coaches.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "coach_id",
                Visible = false
            });
            dgv_coaches.Columns.Add("coach_name", "ФИО клиента");
            dgv_coaches.Columns.Add(new CalendarColumn
            {
                Name = "coach_birthday",
                HeaderText = "Дата рождения"
            });
            dgv_coaches.Columns.Add("coach_passport", "Номер паспорта");
            dgv_coaches.Columns.Add("coach_tin", "ИНН");
            dgv_coaches.Columns.Add("coach_phone", "Номер телефона");
            dgv_coaches.Columns.Add("coach_salary", "Оклад");
            dgv_coaches.Columns.Add(new DataGridViewComboBoxColumn
            {
                HeaderText = "Тип тренера",
                DisplayMember = "coach_type_name",
                ValueMember = "coach_type_id",
                DataSource = GetCoachTypes()
            });

            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = "SELECT * FROM coach"
                };
                var reader = sCommand.ExecuteReader();
                while (reader.Read())
                {
                    var coach_data = new Dictionary<string, object>();
                    foreach (var columnsName in new[] { "coach_name",
                                                        "coach_birthday",
                                                        "coach_passport",
                                                        "coach_tin",
                                                        "coach_phone",
                                                        "coach_salary",
                                                        "coach_coach_type_id"})
                    {
                        coach_data[columnsName] = reader[columnsName];
                    }

                    var row_idx = dgv_coaches.Rows.Add(reader["coach_id"],
                                                        reader["coach_name"],
                                                        reader["coach_birthday"],
                                                        reader["coach_passport"],
                                                        reader["coach_tin"],
                                                        reader["coach_phone"],
                                                        reader["coach_salary"],
                                                        reader["coach_coach_type_id"]);
                    dgv_coaches.Rows[row_idx].Tag = coach_data;
                }
            }
        }
        public static int InsertClient(string client_name, DateTime client_birthday,
                                       long client_passport, string client_phone,
                                       string client_email)
        {
            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = $@"INSERT INTO client (client_name,
                                                        client_birthday,
                                                        client_passport,
                                                        client_phone,
                                                        client_email)
                                    VALUES (@client_name,
                                            @client_birthday,
                                            @client_passport,
                                            @client_phone,
                                            @client_email)
                                    RETURNING client_id"
                };
                sCommand.Parameters.AddWithValue("@client_name", client_name);
                sCommand.Parameters.AddWithValue("@client_birthday", client_birthday);
                sCommand.Parameters.AddWithValue("@client_passport", client_passport);
                sCommand.Parameters.AddWithValue("@client_phone", client_phone);
                sCommand.Parameters.AddWithValue("@client_email", client_email);
                return (int)sCommand.ExecuteScalar();
            }
        }
        public static void UpdateClient(int client_id, string client_name,
                                        DateTime client_birthday, long client_passport,
                                        string client_phone, string client_email)
        {
            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = $@"UPDATE client
                                    SET client_name = @client_name,
                                        client_birthday = @client_birthday,
                                        client_passport = @client_passport, 
                                        client_phone = @client_phone,
                                        client_email = @client_email
                                    WHERE client_id = @client_id"
                };
                sCommand.Parameters.AddWithValue("@client_id", client_id);
                sCommand.Parameters.AddWithValue("@client_name", client_name);
                sCommand.Parameters.AddWithValue("@client_birthday", client_birthday);
                sCommand.Parameters.AddWithValue("@client_passport", client_passport);
                sCommand.Parameters.AddWithValue("@client_phone", client_phone);
                sCommand.Parameters.AddWithValue("@client_email", client_email);
                sCommand.ExecuteNonQuery();
            }
        }
        public static void DeleteClient(int[] client_ids)
        {
            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = @"DELETE FROM client WHERE client_id = ANY(@client_id)"
                };
                sCommand.Parameters.AddWithValue("@client_id", client_ids);
                sCommand.ExecuteNonQuery();
            }
        }
        public static int InsertCoach(string coach_name, DateTime coach_birthday, 
                                      long coach_passport, long coach_tin, 
                                      string coach_phone, int coach_salary, 
                                      int coach_coach_type_id)
        {
            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = $@"INSERT INTO coach (coach_name,
                                                        coach_birthday,
                                                        coach_passport,
                                                        coach_tin,
                                                        coach_phone,
                                                        coach_salary,
                                                        coach_coach_type_id)
                                    VALUES (@coach_name,
                                            @coach_birthday,
                                            @coach_passport,
                                            @coach_tin,
                                            @coach_phone,
                                            @coach_salary,
                                            @coach_coach_type_id)
                                    RETURNING coach_id"
                };
                sCommand.Parameters.AddWithValue("@coach_name", coach_name);
                sCommand.Parameters.AddWithValue("@coach_birthday", coach_birthday);
                sCommand.Parameters.AddWithValue("@coach_passport", coach_passport);
                sCommand.Parameters.AddWithValue("@coach_tin", coach_tin);
                sCommand.Parameters.AddWithValue("@coach_phone", coach_phone);
                sCommand.Parameters.AddWithValue("@coach_salary", coach_salary);
                sCommand.Parameters.AddWithValue("@coach_coach_type_id", coach_coach_type_id);
                return (int)sCommand.ExecuteScalar();
            }
        }
        public static int UpdateCoach(int coach_id, string coach_name,
                                      DateTime coach_birthday, long coach_passport, 
                                      long coach_tin, string coach_phone, 
                                      int coach_salary, int coach_coach_type_id)
        {
            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = $@"UPDATE coach
                                    SET coach_name = @coach_name,
                                        coach_birthday = @coach_birthday,
                                        coach_passport = @coach_passport, 
                                        coach_tin = @coach_tin,
                                        coach_phone = @coach_phone,
                                        coach_salary = @coach_salary,
                                        coach_coach_type_id = @coach_coach_type_id,
                                    WHERE coach_id = @coach_id"
                };
                sCommand.Parameters.AddWithValue("@coach_id", coach_id);
                sCommand.Parameters.AddWithValue("@coach_name", coach_name);
                sCommand.Parameters.AddWithValue("@coach_birthday", coach_birthday);
                sCommand.Parameters.AddWithValue("@coach_passport", coach_passport);
                sCommand.Parameters.AddWithValue("@coach_tin", coach_tin);
                sCommand.Parameters.AddWithValue("@coach_phone", coach_phone);
                sCommand.Parameters.AddWithValue("@coach_salary", coach_salary);
                sCommand.Parameters.AddWithValue("@coach_coach_type_id", coach_coach_type_id);
                return (int)sCommand.ExecuteScalar();
            }
        }
        public static void DeleteCoach(int[] coach_ids)
        {
            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = @"DELETE FROM coach WHERE coach_id = ANY(@coach_id)"
                };
                sCommand.Parameters.AddWithValue("@coach_id", coach_ids);
                sCommand.ExecuteNonQuery();
            }
        }
    }
}
