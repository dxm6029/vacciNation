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

    public class EligibilityManagement {

        VacciNation.Connect connection = new VacciNation.Connect();

        public bool AddEligibilityCategory(Eligibility eligibility){
            MySqlConnection conn = new MySqlConnection();
            try{ 

                conn = connection.OpenConnection();

                string query = "INSERT INTO eligibility(vaccine_id, dependency) VALUES (@vaccine_id, @dependency)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@vaccine_id", eligibility.vaccine_id);
                cmd.Parameters.AddWithValue("@dependency", eligibility.dependency == 0 ? null : eligibility.dependency);
            

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

        public bool AddEligibilityQnA(EligibilityText eligibility, int eligibility_id){
            MySqlConnection conn = new MySqlConnection();
            try{ 

                conn = connection.OpenConnection();

                string query = "INSERT INTO eligibility_text(text_id, language, eligibility_id, type, text) VALUES (@text_id, @lang, @eligibility_id, @type, @text)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@text_id", eligibility.text_id);
                cmd.Parameters.AddWithValue("@lang", eligibility.language);
                cmd.Parameters.AddWithValue("@eligibility_id", eligibility.eligibility_id);
                cmd.Parameters.AddWithValue("@type", eligibility.type);
                cmd.Parameters.AddWithValue("@text", eligibility.text);

                int rowsQ = cmd.ExecuteNonQuery();


                if(rowsQ > 0){ 
                    return true;
                }

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
                connection.CloseConnection(conn);
            }

            return false;
        }

        public bool putEligibilityCategory(Eligibility eligibility){
            bool status = false;
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();

                string query = "UPDATE eligibility SET";

                if(eligibility.vaccine_id != -1){
                    query += " vaccine_id = @vaccine_id,";
                }

                if(eligibility.dependency != -1){
                    query += " dependency = @dependency,";
                }

                query = query.TrimEnd(',');

                query += " WHERE eligibility_id = @id";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", eligibility.eligibility_id);

                if(eligibility.vaccine_id != -1){
                    cmd.Parameters.AddWithValue("@vaccine_id", eligibility.vaccine_id);
                }

                if(eligibility.dependency != -1){
                    cmd.Parameters.AddWithValue("@dependency", eligibility.dependency);
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

          public bool putEligibilityText(EligibilityText eligibility){
            bool status = false;
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();

                string query = "UPDATE eligibility_text SET";

                if(eligibility.eligibility_id != -1){
                    query += " eligibility_id = @eligibility_id,";
                }

                if(eligibility.type != null){
                    query += " type = @type,";
                }

                if(eligibility.text != null){
                    query += " text = @text,";
                }

                query = query.TrimEnd(',');

                query += " WHERE text_id = @id AND language = @lang";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", eligibility.text_id);
                cmd.Parameters.AddWithValue("@lang", eligibility.language);


                if(eligibility.eligibility_id != -1){
                    cmd.Parameters.AddWithValue("@eligibility_id", eligibility.eligibility_id);
                }

                if(eligibility.type != null){
                    cmd.Parameters.AddWithValue("@type", eligibility.type);
                }

                if(eligibility.text != null){
                    cmd.Parameters.AddWithValue("@text", eligibility.text);
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

         public Dictionary<int, MultEligibility> getEligibilityInfo(){
            MySqlConnection conn = new MySqlConnection();
            Dictionary<int, MultEligibility> eligibilityInfo = null;
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT eligibility.eligibility_id, vaccine_id, dependency, text_id, type, text, language FROM eligibility JOIN eligibility_text USING(eligibility_id) ";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                eligibilityInfo = new Dictionary<int, MultEligibility>();
                while (rdr.Read())
                {   
                    int el_id = rdr.GetInt32(0);
                    if(eligibilityInfo.ContainsKey(el_id)){
                        eligibilityInfo[el_id].QnA.Add(new EligibilityText(rdr.GetInt32(3), rdr.GetString(6), el_id, rdr.GetString(4), rdr.GetString(5)));
                    } else {
                        List<EligibilityText> newText = new List<EligibilityText>();
                        newText.Add(new EligibilityText(rdr.GetInt32(3), rdr.GetString(6), el_id, rdr.GetString(4), rdr.GetString(5)));
                        eligibilityInfo.Add(el_id, new MultEligibility(new Eligibility(el_id, rdr.GetInt32(1), rdr.GetInt32(2)), newText));
                    }
                    
                }
                Console.WriteLine( JsonConvert.SerializeObject( eligibilityInfo ));
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return eligibilityInfo;
        }


        public bool removeEligibilityCategory(int eligibility_id){
            bool status = false;
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();

                string query = "DELETE FROM eligibility WHERE eligibility_id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", eligibility_id);

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

         public bool removeEligibility(int text_id){
            bool status = false;
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();


                string query = "UPDATE eligibility SET dependency = @dep WHERE dependency=@removed";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@dep", null);
                cmd.Parameters.AddWithValue("@removed", text_id);

                int rowsUpdate = cmd.ExecuteNonQuery();

                query = "DELETE FROM eligibility_text WHERE text_id = @id";
                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", text_id);

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

    } 

}//namespace