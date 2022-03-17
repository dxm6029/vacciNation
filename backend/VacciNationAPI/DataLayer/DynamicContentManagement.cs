using VacciNationAPI.Models;
using System;
using MySql.Data.MySqlClient; 
using System.Collections.Generic;
using System.Security.Cryptography;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using  Newtonsoft.Json;

namespace VacciNationAPI.DataLayer
{

    public class DynamicContentManagement {

        VacciNation.Connect connection = new VacciNation.Connect();

        public bool AddPageContent(PageContent pageContent){
            MySqlConnection conn = new MySqlConnection();
            try{ 

                conn = connection.OpenConnection();

                string query = "INSERT INTO content(label, text_en, text_es) VALUES (@label, @text_en, @text_es)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@label", pageContent.label);
                cmd.Parameters.AddWithValue("@text_en", pageContent.text_en);
                cmd.Parameters.AddWithValue("@text_es", pageContent.text_es);

                int rows = cmd.ExecuteNonQuery();

                if(rows > 0){ 
                    return true;
                }

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
                connection.CloseConnection(conn);
            }

            return false;
        }

        public bool removePageContent(int content_id){
            bool status = false;
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();

                string query = "DELETE FROM content WHERE content_id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", content_id);

                int rows = cmd.ExecuteNonQuery();
                
                if(rows > 0){
                    status = true;
                }
            } catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally {
                connection.CloseConnection(conn);
            }

            return status;
        }

        public bool putPageContentId(PageContent pageContent){
            bool status = false;
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();

                string query = "UPDATE content SET";

                if(pageContent.label != null){
                    query += " label = @label,";
                }

                if(pageContent.text_en != null){
                    query += " text_en = @text_en,";
                }

                if(pageContent.text_es != null){
                    query += " text_es = @text_es,";
                }

                query = query.TrimEnd(',');

                query += " WHERE content_id = @id";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", pageContent.content_id);

                if(pageContent.label != null){
                    cmd.Parameters.AddWithValue("@label", pageContent.label);
                }

                if(pageContent.text_en != null){
                    cmd.Parameters.AddWithValue("@text_en", pageContent.text_en);
                }

                if(pageContent.text_es != null){
                    cmd.Parameters.AddWithValue("@text_es", pageContent.text_es);
                }

                int rows = cmd.ExecuteNonQuery();

                if(rows > 0){
                    status = true;
                }

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return status;
        }


         public bool putPageContentLabel(PageContent pageContent){
            bool status = false;
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();

                string query = "UPDATE content SET";

                if(pageContent.text_en != null){
                    query += " text_en = @text_en,";
                }

                if(pageContent.text_es != null){
                    query += " text_es = @text_es,";
                }

                query = query.TrimEnd(',');

                query += " WHERE label = @label";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@label", pageContent.label);

                if(pageContent.text_en != null){
                    cmd.Parameters.AddWithValue("@text_en", pageContent.text_en);
                }

                if(pageContent.text_es != null){
                    cmd.Parameters.AddWithValue("@text_es", pageContent.text_es);
                }

                int rows = cmd.ExecuteNonQuery();

                if(rows > 0){
                    status = true;
                }

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return status;
        }

        public PageContent getPageContentWithID(int id){
            MySqlConnection conn = new MySqlConnection();
            PageContent pageContent = null;
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT content_id, label, text_en, text_es FROM content WHERE content_id=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    pageContent = new PageContent(rdr.IsDBNull(0) ? -1: rdr.GetInt32(0), rdr.IsDBNull(1) ? "": rdr.GetString(1), rdr.IsDBNull(2) ? "": rdr.GetString(2), rdr.IsDBNull(3) ? "": rdr.GetString(3));
                }
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return pageContent;
        }


        public PageContent getPageContentWithLabel(string label){
            MySqlConnection conn = new MySqlConnection();
            PageContent pageContent = null;
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT content_id, label, text_en, text_es FROM content WHERE label=@label";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@label", label);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    pageContent = new PageContent(rdr.IsDBNull(0) ? -1: rdr.GetInt32(0), rdr.IsDBNull(1) ? "": rdr.GetString(1), rdr.IsDBNull(2) ? "": rdr.GetString(2), rdr.IsDBNull(3) ? "": rdr.GetString(3));
                }
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return pageContent;
        }

        public List<PageContent> getAllPageContent(){
            MySqlConnection conn = new MySqlConnection();
            List<PageContent> pageContent = new List<PageContent>();
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT content_id, label, text_en, text_es FROM content";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    pageContent.Add(new PageContent(rdr.IsDBNull(0) ? -1: rdr.GetInt32(0), rdr.IsDBNull(1) ? "": rdr.GetString(1), rdr.IsDBNull(2) ? "": rdr.GetString(2), rdr.IsDBNull(3) ? "": rdr.GetString(3)));
                }
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return pageContent;
        }

    } 

}//namespace