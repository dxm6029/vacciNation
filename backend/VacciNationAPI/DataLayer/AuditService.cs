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
    }
}