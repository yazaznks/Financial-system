
using System;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;
using System.Data.SqlClient;

namespace nedaasqlite
{
    class EmployeeLib
    {


        // adding Employee
        public static string connectionString = @"Data Source = NEDA\SQLEXPRESS;Initial Catalog=Neda;User ID=Nedaa;Password=nedaa";
        public static void addEmployee(string Name, string DOB, string id, string num, string nationality, string salary)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand CMD_Insert = new SqlCommand();
                CMD_Insert.Connection = con;
                CMD_Insert.CommandText = "INSERT INTO Employee(Name, ID, DOB, number, nationality, salary) VALUES(@Name, @DOB, @ID, @phone, @nationality, @salary);";
                CMD_Insert.Parameters.AddWithValue("@Name", Name);
                CMD_Insert.Parameters.AddWithValue("@ID", id);
                CMD_Insert.Parameters.AddWithValue("@DOB", DOB);
                CMD_Insert.Parameters.AddWithValue("@phone", num);
                CMD_Insert.Parameters.AddWithValue("@nationality", nationality);
                CMD_Insert.Parameters.AddWithValue("@salary", salary);
                CMD_Insert.ExecuteReader();
                con.Close();
            }
        }

        // add record of salary
        public static void addSalary(string Name, double total, string month, double absent, string notes)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand CMD_Insert = new SqlCommand();
                CMD_Insert.Connection = con;
                CMD_Insert.CommandText = "INSERT INTO Salary(Name,Total ,month, absent, notes) VALUES(@Name, @Total, @month, @absent, @notes);";
                CMD_Insert.Parameters.AddWithValue("@Name", Name);
                CMD_Insert.Parameters.AddWithValue("@Total", total);
                CMD_Insert.Parameters.AddWithValue("@month", month);
                CMD_Insert.Parameters.AddWithValue("@absent", absent);
                CMD_Insert.Parameters.AddWithValue("@notes", notes);
                CMD_Insert.ExecuteReader();
                con.Close();
            }
        }
    
        // get Employees

        public static List<employeeDetail> GetEmployees()
        {
            List<employeeDetail> userList = new List<employeeDetail>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string selectCMD = "SELECT * FROM Employee";
                SqlCommand cmd_getRec = new SqlCommand(selectCMD, con);


                SqlDataReader reader = cmd_getRec.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new employeeDetail(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetInt32(6)));

                }
                con.Close();

            }

            return userList;
        }

        // get Employee's id
        public static string GetID(string name)
        {
            string fee;
          

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand CMD_select = new SqlCommand();
                CMD_select.Connection = con;
                CMD_select.CommandText = "SELECT ID FROM Employee WHERE name like @search";
                CMD_select.Parameters.Add(new SqlParameter("@search", '%' + name + '%'));
                SqlDataReader reader = CMD_select.ExecuteReader();
                while (reader.Read())
                {

                    fee = reader.GetString(0);
                    return fee;
                }
                con.Close();

            }
            return "";
        }
        public static List<employeeDetail> searchemp(string search)
        {
            List<employeeDetail> userList = new List<employeeDetail>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand CMD_Select = new SqlCommand("SELECT * FROM Employee WHERE Name LIKE @search OR ID LIKE @search OR number LIKE @search OR nationality LIKE @search", con);
                CMD_Select.Parameters.Add(new SqlParameter("@search", '%' + search + '%'));
                SqlDataReader reader = CMD_Select.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new employeeDetail(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetInt32(6)));
                }
                con.Close();

            }

            return userList;
        }
        // update employee
        public static void UpdateEmployee(int N, string name, string id, string dob, string number, string nationality, string salary)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand CMD_Select = new SqlCommand("UPDATE Employee SET Name = @Name, ID = @iD," +
                    " DOB = @DOb, number = @number, nationality = @nationality, salary = @salary WHERE N = @search", con);
                CMD_Select.Parameters.Add(new SqlParameter("@Name", name));
                CMD_Select.Parameters.Add(new SqlParameter("@iD", id));
                CMD_Select.Parameters.Add(new SqlParameter("@DOb", dob));
                CMD_Select.Parameters.Add(new SqlParameter("@number", number));
                CMD_Select.Parameters.Add(new SqlParameter("@nationality", nationality));
                CMD_Select.Parameters.Add(new SqlParameter("@salary", salary));
                CMD_Select.Parameters.Add(new SqlParameter("@search", N));
                SqlDataReader reader = CMD_Select.ExecuteReader();

                con.Close();
            }

        }

        public static List<employeeDetail> GetEmpInfo(int N)
        {
            List<employeeDetail> userList = new List<employeeDetail>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand CMD_Select = new SqlCommand("SELECT * FROM Employee WHERE N = @search", con);
                CMD_Select.Parameters.Add(new SqlParameter("@search", N));
                SqlDataReader reader = CMD_Select.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new employeeDetail(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), reader.GetString(4),reader.GetString(5), reader.GetInt32(6)));
                }
                con.Close();

            }

            return userList;
        }

        // Get Employee's Salary
        public static int GetSalary(string name)
        {
            int fee;
            List<Salary> userList = new List<Salary>();
          
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand CMD_select = new SqlCommand();
                CMD_select.Connection = con;
                CMD_select.CommandText = "SELECT Salary FROM Employee WHERE name like @search";
                CMD_select.Parameters.Add(new SqlParameter("@search", '%' + name + '%'));
                SqlDataReader reader = CMD_select.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new Salary(reader.GetInt32(0)));
                    fee = reader.GetInt32(0);
                    return fee;
                }
                con.Close();

            }
            return 0;
        }

        //Get all salaries


        public static List<Salary> GetAllSalaries()
        {
            List<Salary> userList = new List<Salary>();
  
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string selectCMD = "SELECT * FROM Salary";
                SqlCommand cmd_getRec = new SqlCommand(selectCMD, con);


                SqlDataReader reader = cmd_getRec.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new Salary(reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), reader.GetString(5)));
                }
                con.Close();

            }

            return userList;
        }
        // search salaries
        public static List<Salary> searchSalary(string search)
        {
            List<Salary> userList = new List<Salary>();
        
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand CMD_Select = new SqlCommand("SELECT * FROM Salary WHERE Name LIKE @search OR Month LIKE @search", con);
                CMD_Select.Parameters.Add(new SqlParameter("@search", '%' + search + '%'));
                SqlDataReader reader = CMD_Select.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new Salary(reader.GetString(0), reader.GetInt32(1), reader.GetString(2), reader.GetDouble(3), reader.GetString(4)));
                }
                con.Close();

            }

            return userList;
        }

        public static List<users> GetUsers()
        {
            List<users> userList = new List<users>();
            
          
          
            using (SqlConnection  con = new SqlConnection(connectionString))
            {
                    con.Open();
                    string selectCMD = "SELECT * FROM userInfo";
                    SqlCommand cmd_getRec = new SqlCommand(selectCMD, con);


                    SqlDataReader reader = cmd_getRec.ExecuteReader();
                    while (reader.Read())
                    {
                        userList.Add(new users(reader.GetString(0), reader.GetString(1),reader.GetString(2)));

                    }
                    con.Close();

                }
            return userList;
        }

        // validate password
        public static string validate(string name, string pass)
        {
            string permission;
           
         
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand CMD_select = new SqlCommand();
                CMD_select.Connection = con;
                CMD_select.CommandText = "SELECT permission FROM userInfo WHERE name = @search AND password = @pass";
                CMD_select.Parameters.Add(new SqlParameter("@search", name ));
                CMD_select.Parameters.Add(new SqlParameter("@pass", pass ));
                SqlDataReader reader = CMD_select.ExecuteReader();
                while (reader.Read())
                {
                   
                    permission = reader.GetString(0);
                    return permission;
                }
                con.Close();

            }
            return "0";
        }

        //employee Details

        public class employeeDetail
        {
            public int N { get; set; }
            public String name { get; set; }
            public String dob { get; set; }
            public String ID { get; set; }
            public String Number { get; set; }
            public String nationality { get; set; }
            public int salary { get; set; }
   
            public employeeDetail(int n, string Name, string id, string DOB2, string num, string nationality2, int salary)
            {
                N = n;
                name = Name;
                dob = DOB2;
                ID = id;
                Number = num;
                nationality = nationality2;
                this.salary = salary;

            }
        }


        //salary record details
        public class Salary
        {
            public string SName { get; set; }
            public int total { get; set; }
            public string Month { get; set; }
            public double absent { get; set; }
            public string notes { get; set; }
            public Salary(string name, int Total, string month, double abs, string notes)
            {
                SName = name;
                total = Total;
                Month = month;
                absent = abs;
                this.notes = notes;
            }
            public Salary(int Total)
            {
                total = Total;
            }
        }


        public class users
        {
            public string Name { get; set; }
            public string password { get; set; }
            public string permission { get; set; }
            public users(string name, string password, string permission)
            {
                Name = name;
                this.password = password;
                this.permission = permission;
            }

        }







    }
}
