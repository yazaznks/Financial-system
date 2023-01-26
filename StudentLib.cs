using System;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;
using System.Data.SqlClient;
namespace nedaasqlite
{
    class StudentLib
    {
        public StudentLib()
        {
        
        }


        // adding student 
        public static string connectionString = @"Data Source = NEDA\SQLEXPRESS;Initial Catalog=Neda;User ID=Nedaa;Password=nedaa";
        public static void addStudent(string Name, string DOB, string id, string mom, string dad, string condition, string nationality, int fee,  int b, string note)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand CMD_Insert = new SqlCommand();
                CMD_Insert.Connection = con;
                CMD_Insert.CommandText = "INSERT INTO student(name, dob, ID, momNumber, dadNumber,condition, nationality, fees, busF, notes) VALUES(@Name, @DOB, @ID, @mom, @dad, @cond, @nationality, @fees,@busF, @notes);";
                CMD_Insert.Parameters.AddWithValue("@Name", Name);
                CMD_Insert.Parameters.AddWithValue("@DOB", DOB);
                CMD_Insert.Parameters.AddWithValue("@ID", id);
                CMD_Insert.Parameters.AddWithValue("@mom", mom);
                CMD_Insert.Parameters.AddWithValue("@dad", dad);
                CMD_Insert.Parameters.AddWithValue("@cond", condition);
                CMD_Insert.Parameters.AddWithValue("@nationality", nationality);
                CMD_Insert.Parameters.AddWithValue("@fees", fee);
                CMD_Insert.Parameters.AddWithValue("@busF", b);
                CMD_Insert.Parameters.AddWithValue("@notes", note);
                CMD_Insert.ExecuteReader();
                con.Close();
            }

        }

        // get students

        public static List<studentDetail> GetStudent()
        {
            List<studentDetail> userList = new List<studentDetail>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string selectCMD = "SELECT * FROM student ORDER BY name";
                SqlCommand cmd_getRec = new SqlCommand(selectCMD, con);


                SqlDataReader reader = cmd_getRec.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new studentDetail(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                    reader.GetString(3), reader.GetString(4), reader.GetString(5),
                    reader.GetString(6), reader.GetString(7), reader.GetInt32(8),
                     reader.GetInt32(9), reader.GetString(10)));
                }
                con.Close();
            }
            return userList;
        }

        public static List<studentDetail> GetStudentInfo(int N)
        {
            List<studentDetail> userList = new List<studentDetail>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand CMD_Select = new SqlCommand("SELECT * FROM student WHERE N = @search", con);
                CMD_Select.Parameters.Add(new SqlParameter("@search", N ));
                SqlDataReader reader = CMD_Select.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new studentDetail(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), reader.GetString(4), reader.GetString(5),
                        reader.GetString(6), reader.GetString(7), reader.GetInt32(8),
                        reader.GetInt32(9), reader.GetString(10)));
                }
                con.Close();

            }

            return userList;
        }
 

        // Search Student
        public static List<studentDetail> searchStudent(string search)
        {
            List<studentDetail> userList = new List<studentDetail>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand CMD_Select = new SqlCommand("SELECT * FROM student WHERE name LIKE @search OR dob LIKE @search OR momNumber LIKE @search OR dadNumber LIKE @search ORDER BY name", con);
                CMD_Select.Parameters.Add(new SqlParameter("@search", '%'+search+'%'));
                SqlDataReader reader = CMD_Select.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new studentDetail(reader.GetInt32(0),reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), reader.GetString(4), reader.GetString(5),
                        reader.GetString(6), reader.GetString(7), reader.GetInt32(8),
                        reader.GetInt32(9), reader.GetString(10)));
                }
                con.Close();

            }

            return userList;
        }


        //Edit Student 

        public static void UpdateStudent(int N, string name, string id, string dob, string mom, string dad, string cond, string nat,
            int fees, int busF, string notes )
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand CMD_Select = new SqlCommand("UPDATE student SET name = @Name, ID = @iD," +
                    " DOB = @DOb, momNumber = @mom, dadNumber = @dad, condition = @con, nationality = @natio, " +
                    "fees = @fee, busF = @busF, notes = @notes WHERE N = @search", con);
                CMD_Select.Parameters.Add(new SqlParameter("@Name", name));
                CMD_Select.Parameters.Add(new SqlParameter("@iD", id));
                CMD_Select.Parameters.Add(new SqlParameter("@DOb", dob));
                CMD_Select.Parameters.Add(new SqlParameter("@mom", mom));
                CMD_Select.Parameters.Add(new SqlParameter("@dad", dad));
                CMD_Select.Parameters.Add(new SqlParameter("@con", cond));
                CMD_Select.Parameters.Add(new SqlParameter("@natio", nat));
                CMD_Select.Parameters.Add(new SqlParameter("@fee", fees));
                CMD_Select.Parameters.Add(new SqlParameter("@busF", busF));
                CMD_Select.Parameters.Add(new SqlParameter("@notes", notes));
                CMD_Select.Parameters.Add(new SqlParameter("@search", N));
                SqlDataReader reader = CMD_Select.ExecuteReader();
               
                con.Close();
            }

        }

        // delete student
        public static void DeleteStudent(int N)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand CMD_Select = new SqlCommand("Delete FROM student WHERE N = @search", con);
                CMD_Select.Parameters.Add(new SqlParameter("@search", N));
                SqlDataReader reader = CMD_Select.ExecuteReader();
               
                con.Close();
            }

        }

        public static int Getfees(string name)
        {
            int fee;
            List<studentDetail> userList = new List<studentDetail>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                
                SqlCommand CMD_select = new SqlCommand();
                CMD_select.Connection = con;
                CMD_select.CommandText = "SELECT fees FROM student WHERE name like @search";
                CMD_select.Parameters.Add(new SqlParameter("@search", '%' + name + '%'));
                SqlDataReader reader = CMD_select.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new studentDetail(reader.GetInt32(0)));
                    fee = reader.GetInt32(0);
                    return fee;
                }
                con.Close();
                
            }
            return 0; 
        }

        // get student bus fees
        public static int GetBfees(string name)
        {
            int fee;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand CMD_select = new SqlCommand();
                CMD_select.Connection = con;
                CMD_select.CommandText = "SELECT busF FROM student WHERE name like @search";
                CMD_select.Parameters.Add(new SqlParameter("@search", '%' + name + '%'));
                SqlDataReader reader = CMD_select.ExecuteReader();
                while (reader.Read())
                {
                  
                    fee = reader.GetInt32(0);
                    return fee;
                }
                con.Close();

            }
            return 0;
        }

        //Get Student 


        public class studentDetail
        {
            public int N { get; set; }
            public String name { get; set; }
            public String dob { get; set; }
            public String ID { get; set; }
            public String momNumber { get; set; }
            public String dadNumber { get; set; }
            public String condition { get; set; }
            public String nationality { get; set; }
            public int fees { get; set; }
            public int BusF { get; set; }
            public String notes { get; set; }
            
            

            public studentDetail(int n,string Name, string DOB2, string id, string mom,
                string dad, string condition1, string nationality2, int fee, int Busf, string note)
            {
                N = n;
                name = Name;
                dob = DOB2;
                ID = id;
                momNumber = mom;
                dadNumber = dad;
                condition = condition1;
                nationality = nationality2;
                fees = fee;
                
                BusF = Busf;
                notes = note;

            }
            public studentDetail(int fee)
            {
                fees = fee;
               
            }
            public studentDetail(int n, string g, string dob,string idd, string momnum)
            {
                N = n;
                name = g;
                this.dob = dob;
                ID = idd;
                momNumber = momnum;

            }
        }
        public class studentName
        {
            public string name { get; set; }
            public studentName(string name)
            {
                this.name = name;
            }
        }


        
    }
}
