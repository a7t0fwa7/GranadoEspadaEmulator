using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace GranadoEspadaHeadless.Network.Database
{
    public class Database
    {
        public static string DefaultConnectionString = "Data Source=XXX.YYY.ZZZ.AAA;Initial Catalog=GE;User id=sa;Password=11aabbddee!324hG";

        private readonly string m_connectionString;
        private readonly object m_locker;

        public Database(string connectionString)
        {
            m_connectionString = connectionString;
            m_locker = new object();
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(m_connectionString);
        }

        private SqlDataReader ExecuteDataQuery(SqlConnection connection, string query, params object[] objects)
        {
            query = string.Format(query, objects);

            SqlCommand command = new SqlCommand(query);
            command.ExecuteNonQuery();

            return command.ExecuteReader();
        }

        public string[] GetPlayerNotice(out int id)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM AUTOMATION_PlayerNotice1 WHERE sent=0 ORDER BY id";

                        command.Prepare();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {              
                                string[] nameAndNotice = new string[2];
                                id = reader.GetInt32(0);
                                string name = reader.GetString(1);
                                string notice = reader.GetString(2);

                                if(name.Length > 0 && notice.Length > 0)
                                {
                                    nameAndNotice[0] = name; //test this
                                    nameAndNotice[1] = notice;
                                    return nameAndNotice;
                                }       
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while getting notice from DB: " + ex.Message);
            }
            id = -1;
            return null;
        }

        public string GetGlobalNotice(out int id)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM AUTOMATION_GlobalNotice1 WHERE sent=0 ORDER BY id";

                        command.Prepare();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                id = reader.GetInt32(0);
                                string notice = reader.GetString(1);

                                return notice;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while getting Global notice from DB: " + ex.Message);
            }
            id = -1;
            return string.Empty;
        }

        public void UpdatePlayerNotice(int id)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "UPDATE AUTOMATION_PlayerNotice1 SET sent=1, date=GETDATE() WHERE id=" + id;
                        
                        command.Prepare();

                        int numRows = command.ExecuteNonQuery();
                        Console.WriteLine("Player Whisper Notice #{0} updated", numRows);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while upadating notice in DB: " + ex.Message);
            }

        }

        public void UpdateGlobalNotice(int id)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "UPDATE AUTOMATION_GlobalNotice1 SET sent=1, date=GETDATE() WHERE id=@id";

                        command.Prepare();

                        command.Parameters.AddWithValue("@id", id);
                        int numRows = command.ExecuteNonQuery();
                        Console.WriteLine("Global Notice #{0} updated", numRows);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while upadating notice in DB: " + ex.Message);
            }

        }

        public void InsertWorldChat(string charname, string message)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    message = message.Replace('\'', '`');

                    string query = String.Format("INSERT INTO WorldChat (name, message, date) VALUES('{0}', '{1}', GETDATE() )", charname, message);
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.ExecuteReader();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while InsertWorldChat in DB: " + ex.Message);
            }
        }

        public void InsertWhisper(string charname, string message)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    string query = String.Format("INSERT INTO AUTOMATION_ReceivedWhispers1 (name, message, date) VALUES('{0}', '{1}', GETDATE())", charname, message);
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.ExecuteReader();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while InsertWhisper in DB: " + ex.Message);
            }
        }
    }
}
