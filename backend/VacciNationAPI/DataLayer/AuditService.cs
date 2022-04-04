using System;
using MySql.Data.MySqlClient; 
using System.Collections.Generic;
using System.Text.Json;

namespace VacciNationAPI.DataLayer
{
    public class AuditService
    {

        public static bool Save(String table, String row, String idFieldName, int rowId, String newValue, int userId, MySqlConnection conn)
        {
            
            try{ 
                string query = "INSERT INTO change_history (change_table, change_row, row_id, old_val, new_val, change_date, changed_by) VALUES (@table, @row, @rowId, (SELECT " + row + " FROM " + table + " WHERE " + idFieldName + "=@rowId ), @newValue, CURRENT_TIMESTAMP(), @userId)";
                
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@table", table);
                cmd.Parameters.AddWithValue("@row", row);
                cmd.Parameters.AddWithValue("@rowId", rowId);
                cmd.Parameters.AddWithValue("@newValue", newValue);
                cmd.Parameters.AddWithValue("@userId", userId);
                int numAffected = cmd.ExecuteNonQuery();
                               
                //insert failed
                if (numAffected < 1)
                {
                    Console.WriteLine("Error recording change history");
                    return false;
                }

                return true;

            }
            catch (Exception e) {
                Console.WriteLine(e.Message); 
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
        
        public static List<Dictionary<string, string>> GetAllChanges(){
            VacciNation.Connect connection = new VacciNation.Connect();
            MySqlConnection conn = connection.OpenConnection();
            List<Dictionary<string, string>> changes = new List<Dictionary<string, string>>();
            try{ 
                string query = "SELECT h.change_table, h.change_row, h.row_id, h.old_val, h.new_val, h.change_date, s.staff_id as changedbyid, CONCAT(s.first_name, ' ', s.last_name) as changeby FROM change_history as h JOIN staff as s ON h.changed_by = s.staff_id;";
                
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    Dictionary<string, string> change = new Dictionary<string, string>();
                    change.Add("table", (rdr.IsDBNull(0) ? "" : rdr.GetString(0)) );
                    change.Add("row", (rdr.IsDBNull(1) ? "" : rdr.GetString(1)) );
                    change.Add("row_id", "" + (rdr.IsDBNull(2) ? -1 : rdr.GetInt32(2)) );
                    change.Add("old_val", (rdr.IsDBNull(3) ? "" : rdr.GetString(3)) );
                    change.Add("new_val", (rdr.IsDBNull(4) ? "" : rdr.GetString(4)) );
                    change.Add("date", (rdr.IsDBNull(5) ? "" : rdr.GetString(5)) );
                    change.Add("changedbyid", "" + (rdr.IsDBNull(6) ? -1 : rdr.GetInt32(6)) );
                    change.Add("changedbyname", (rdr.IsDBNull(7) ? "" : rdr.GetString(7)) );
                    
                    changes.Add(change);
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return changes;
        }

    }
}