using VacciNationAPI.Models;
using System;
using MySql.Data.MySqlClient; 
using System.Collections.Generic;

namespace VacciNationAPI.DataLayer
{
    public class VaccineManagement {
        
        VacciNation.Connect connection = new VacciNation.Connect();
        
        public List<Dictionary<string, string>> GetAllVaccines(){
            MySqlConnection conn = new MySqlConnection();
            List<Dictionary<string, string>> vaccines = new List<Dictionary<string, string>>();
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT v.vaccine_id, c.name as category, d.name as disease, v.description FROM vaccine as v LEFT JOIN vaccine_category as c ON v.category = c.category_id LEFT JOIN vaccine_disease as d ON v.disease = d.disease_id; ";
                
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    Dictionary<string, string> vaccine = new Dictionary<string, string>();
                    vaccine.Add("vaccine_id", "" + (rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0)) );
                    vaccine.Add("category", (rdr.IsDBNull(1) ? "" : rdr.GetString(1)) );
                    vaccine.Add("disease", (rdr.IsDBNull(2) ? "" : rdr.GetString(2)) );
                    vaccine.Add("description", (rdr.IsDBNull(3) ? "" : rdr.GetString(3)) );

                    vaccines.Add(vaccine);
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return vaccines;
        }
        
        public bool InsertVaccine(Vaccine vaccine){
            bool result = false;
            MySqlConnection conn = connection.OpenConnection();

            try{
                string query = "INSERT INTO vaccine (category, disease, description) VALUES(@category, @disease, @description)";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@category", vaccine.category_id);
                cmd.Parameters.AddWithValue("@disease", vaccine.disease_id);
                cmd.Parameters.AddWithValue("@description", vaccine.description);
                
                int numAffected = cmd.ExecuteNonQuery();

                if(numAffected > 0){
                    result = true;
                }

            } catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);} // probably should log something here eventually
            finally{
                connection.CloseConnection(conn);
            }
            return result;

        }
        
        public bool updateVaccine(Vaccine vaccine)
        {
            bool result = false;
            MySqlConnection conn = connection.OpenConnection();
            
            try{
                //build update string with whatever is set
                string query="Update vaccine set  ";

                if (vaccine.category_id != 0) {
                    query += "category=@category_id, ";
                }

                if (vaccine.disease_id != 0) {
                    query += "disease=@disease_id, ";
                }

                if (!string.IsNullOrEmpty(vaccine.description)) {
                    query += "description=@description, ";
                }

                // remove trailing ,
                if (query[query.Length - 2] == ',')
                {
                    query = query.Remove(query.Length - 2);
                }
                else
                {
                    //none of the values are set, so nothing should happen
                    return false;
                }

                query += " WHERE vaccine_id = @vaccine_id;";
    
                
                MySqlCommand cd = new MySqlCommand(query, conn);
                if (vaccine.category_id != 0) {
                    cd.Parameters.AddWithValue("@category_id", vaccine.category_id);
                }

                if (vaccine.disease_id != 0) {
                    cd.Parameters.AddWithValue("@disease_id", vaccine.disease_id);
                }

                if (!string.IsNullOrEmpty(vaccine.description)) {
                    cd.Parameters.AddWithValue("@description", vaccine.description);
                }
                cd.Parameters.AddWithValue("@vaccine_id", vaccine.vaccine_id);

                int rows = cd.ExecuteNonQuery();

                if(rows > 0){
                    result = true;
                }

            } catch (Exception e){ 
                Console.WriteLine(e.Message); 
                Console.WriteLine(e.StackTrace);} // probably should log something here eventually
            finally{
                connection.CloseConnection(conn);
            }

            return result;
        }

        public List<Dictionary<string, string>> getAllVaccinesByType(int categoryID, int diseaseID){
            MySqlConnection conn = new MySqlConnection();
            List<Dictionary<string, string>> vaccines = new List<Dictionary<string, string>>();
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT v.vaccine_id, c.name as category, d.name as disease, v.description FROM vaccine as v LEFT JOIN vaccine_category as c ON v.category = c.category_id LEFT JOIN vaccine_disease as d ON v.disease = d.disease_id WHERE v.category=@category OR v.disease=@disease;";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@category", categoryID);
                cmd.Parameters.AddWithValue("@disease", diseaseID);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    Dictionary<string, string> vaccine = new Dictionary<string, string>();
                    vaccine.Add("vaccine_id", "" + (rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0)) );
                    vaccine.Add("category", (rdr.IsDBNull(1) ? "" : rdr.GetString(1)) );
                    vaccine.Add("disease", (rdr.IsDBNull(2) ? "" : rdr.GetString(2)) );
                    vaccine.Add("description", (rdr.IsDBNull(3) ? "" : rdr.GetString(3)) );

                    vaccines.Add(vaccine);
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return vaccines;
        }

        public List<Dictionary<string, string>> GetAllCategories(){
            MySqlConnection conn = new MySqlConnection();
            List<Dictionary<string, string>> categories = new List<Dictionary<string, string>>();
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT category_id, name FROM vaccine_category; ";
                
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    Dictionary<string, string> category = new Dictionary<string, string>();
                    category.Add("category_id", "" + (rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0)) );
                    category.Add("name", (rdr.IsDBNull(1) ? "" : rdr.GetString(1)) );

                    categories.Add(category);
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return categories;
        }
        
        public bool InsertCategory(Category category){
            bool result = false;
            MySqlConnection conn = connection.OpenConnection();

            try{
                string query = "INSERT INTO vaccine_category (category_id, name) VALUES(@category_id, @name)";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@category_id", category.category_id);
                cmd.Parameters.AddWithValue("@name", category.name);
                
                int numAffected = cmd.ExecuteNonQuery();

                if(numAffected > 0){
                    result = true;
                }

            } catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);} // probably should log something here eventually
            finally{
                connection.CloseConnection(conn);
            }
            return result;

        }
        
        public bool UpdateCategory(Category category)
        {
            bool result = false;
            MySqlConnection conn = connection.OpenConnection();
            
            try{
                string query="Update vaccine_category set name=@name WHERE category_id=@category_id";

                MySqlCommand cd = new MySqlCommand(query, conn);

                cd.Parameters.AddWithValue("@name", category.name);
                cd.Parameters.AddWithValue("@category_id", category.category_id);

                int rows = cd.ExecuteNonQuery();

                if(rows > 0){
                    result = true;
                }

            } catch (Exception e){ 
                Console.WriteLine(e.Message); 
                Console.WriteLine(e.StackTrace);} // probably should log something here eventually
            finally{
                connection.CloseConnection(conn);
            }

            return result;
        }

        public Dictionary<string, string> GetCategoryById(int category_id){
            MySqlConnection conn = new MySqlConnection();
            Dictionary<string, string> category = new Dictionary<string, string>();
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT category_id, name FROM vaccine_category WHERE category_id=@category_id;";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@category_id", category_id);

                MySqlDataReader rdr = cmd.ExecuteReader();

                rdr.Read();
                category.Add("category_id", "" + (rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0)) );
                category.Add("name", (rdr.IsDBNull(1) ? "" : rdr.GetString(1)) );
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return category;
        }

        public List<Dictionary<string, string>> GetAllDiseases(){
            MySqlConnection conn = new MySqlConnection();
            List<Dictionary<string, string>> diseases = new List<Dictionary<string, string>>();
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT disease_id, name FROM vaccine_disease; ";
                
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    Dictionary<string, string> disease = new Dictionary<string, string>();
                    disease.Add("disease_id", "" + (rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0)) );
                    disease.Add("name", (rdr.IsDBNull(1) ? "" : rdr.GetString(1)) );

                    diseases.Add(disease);
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return diseases;
        }
        
        public bool InsertDisease(Disease disease){
            bool result = false;
            MySqlConnection conn = connection.OpenConnection();

            try{
                string query = "INSERT INTO vaccine_disease (disease_id, name) VALUES(@disease_id, @name)";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@disease_id", disease.disease_id);
                cmd.Parameters.AddWithValue("@name", disease.name);
                
                int numAffected = cmd.ExecuteNonQuery();

                if(numAffected > 0){
                    result = true;
                }

            } catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);} // probably should log something here eventually
            finally{
                connection.CloseConnection(conn);
            }
            return result;

        }
        
        public bool UpdateDisease(Disease disease)
        {
            bool result = false;
            MySqlConnection conn = connection.OpenConnection();
            
            try{
                string query="Update vaccine_disease set name=@name WHERE disease_id=@disease_id";

                MySqlCommand cd = new MySqlCommand(query, conn);

                cd.Parameters.AddWithValue("@name", disease.name);
                cd.Parameters.AddWithValue("@disease_id", disease.disease_id);

                int rows = cd.ExecuteNonQuery();

                if(rows > 0){
                    result = true;
                }

            } catch (Exception e){ 
                Console.WriteLine(e.Message); 
                Console.WriteLine(e.StackTrace);} // probably should log something here eventually
            finally{
                connection.CloseConnection(conn);
            }

            return result;
        }

        public Dictionary<string, string> GetDiseaseById(int disease_id){
            MySqlConnection conn = new MySqlConnection();
            Dictionary<string, string> disease = new Dictionary<string, string>();
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT disease_id, name FROM vaccine_disease WHERE disease_id=@disease_id;";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@disease_id", disease_id);

                MySqlDataReader rdr = cmd.ExecuteReader();

                rdr.Read();
                disease.Add("disease_id", "" + (rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0)) );
                disease.Add("name", (rdr.IsDBNull(1) ? "" : rdr.GetString(1)) );
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return disease;
        }

    }
}