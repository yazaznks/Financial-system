
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using Windows.Storage;

namespace nedaasqlite
{
    
    class AccountingLib
    {


        public static string connectionString = @"Data Source = NEDA\SQLEXPRESS;Initial Catalog=Neda;User ID=Nedaa;Password=nedaa";
        //adding credit
        public static void addCredit(string Name, int amount, string notes, string recipient, bool petty)
        {

            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand CMD_Insert = new SqlCommand();
                CMD_Insert.Connection = con;
                CMD_Insert.CommandText = "INSERT INTO Credit (name,amount,Date,Notes, recipient, petty) VALUES(@Name, @Amount, @Date, @Notes, @recip, @petty);";
                CMD_Insert.Parameters.AddWithValue("@Name", Name);
                CMD_Insert.Parameters.AddWithValue("@Amount", amount);
                CMD_Insert.Parameters.AddWithValue("@Date", DateTime.UtcNow.ToString().Substring(0, 10));
                CMD_Insert.Parameters.AddWithValue("@Notes", notes);
                CMD_Insert.Parameters.AddWithValue("@recip", recipient);
                CMD_Insert.Parameters.AddWithValue("@petty", petty);
                CMD_Insert.ExecuteReader();
                con.Close();
            }
        }

        // adding tuition


        public static void addTuition(string Name, int assess, int academic, int bus, int books, string notes, int remain, int pa)
        {

            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                int total = assess + academic + bus + books;
                con.Open();
                SqlCommand CMD_Insert = new SqlCommand();
                CMD_Insert.Connection = con;
                CMD_Insert.CommandText = "INSERT INTO Tuition (SName,assess,academic,bus,books,Date,Total, Paid , remaining, notes) VALUES(@Name, @Assess, @Academic, @bus, @book, @Date, @Total, @pai, @remain, @notes);";
                CMD_Insert.Parameters.AddWithValue("@Name", Name);
                CMD_Insert.Parameters.AddWithValue("@Assess", assess);
                CMD_Insert.Parameters.AddWithValue("@Academic", academic);
                CMD_Insert.Parameters.AddWithValue("@bus", bus);
                CMD_Insert.Parameters.AddWithValue("@book", books);
                CMD_Insert.Parameters.AddWithValue("@Date", DateTime.UtcNow.ToString().Substring(0, 10));
                CMD_Insert.Parameters.AddWithValue("@Total", total);
                CMD_Insert.Parameters.AddWithValue("@pai", pa);
                CMD_Insert.Parameters.AddWithValue("@notes", notes);
                CMD_Insert.Parameters.AddWithValue("@remain", remain);



                CMD_Insert.ExecuteReader();

                con.Close();
            }

        }
       

        //Get Credit Total


        public static List<CreditDetail> GetCreditTotal()
        {
            List<CreditDetail> userList = new List<CreditDetail>();
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string selectCMD = "SELECT name, SUM(amount) FROM Credit GROUP BY name";
                SqlCommand cmd_getRec = new SqlCommand(selectCMD, con);


                SqlDataReader reader = cmd_getRec.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new CreditDetail(reader.GetString(0), reader.GetInt32(1), 0));
                    
                }
                con.Close();

            }

            return userList;
        }

        //get credit total of each month
        public static List<CreditDetail> GetCreditperM(string date)
        {
            List<CreditDetail> userList = new List<CreditDetail>();
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand CMD_Select = new SqlCommand("SELECT name, SUM(amount) FROM Credit WHERE (substring(Date,4,2) LIKE @search) GROUP by name ", con);
                CMD_Select.Parameters.Add(new SqlParameter("@search", '%' + date + '%')); ;
                SqlDataReader reader = CMD_Select.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new CreditDetail(reader.GetString(0), reader.GetInt32(1), 0));

                }
                con.Close();

            }

            return userList;
        }

        //get tuition paid total
        public static int GetTuitionTotalM(string x)
        {
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand CMD_select = new SqlCommand();
                SqlCommand CMD_Select = new SqlCommand("SELECT SUM(Total) FROM Tuition WHERE (substring(Date,4,2) LIKE @search) GROUP by substring(Date,4,2)", con);
                CMD_Select.Parameters.Add(new SqlParameter("@search", '%' + x + '%'));
                SqlDataReader reader = CMD_Select.ExecuteReader();
                while (reader.Read())
                {
                    int total = reader.GetInt32(0);
                    return total;
                }
                con.Close();
            }
            return 0;
        }



        //Get Credit Details   ( for chart )


        public static List<CreditDetail> GetCreditAmount()
        {
            List<CreditDetail> userList = new List<CreditDetail>();
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string selectCMD = "SELECT Date, amount FROM Credit ORDER BY Date";
                SqlCommand cmd_getRec = new SqlCommand(selectCMD, con);


                SqlDataReader reader = cmd_getRec.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new CreditDetail(reader.GetString(0), reader.GetInt32(1)));
                }
                con.Close();

            }

            return userList;
        }





        //Get tuition


        public static int GetTuitionperName(string name)
        {
            int fee;

         
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand CMD_select = new SqlCommand();
                CMD_select.Connection = con;
                CMD_select.CommandText = "SELECT fees, busF FROM student WHERE name like @search";
                CMD_select.Parameters.Add(new SqlParameter("@search", '%' + name + '%'));
                SqlDataReader reader = CMD_select.ExecuteReader();
                while (reader.Read())
                {

                    fee = reader.GetInt32(0) + reader.GetInt32(1);
                    return fee;
                }
                con.Close();

            }
            return 0;
        }

        //search tuition

        public static List<Tuition> searchTuition(string search)
        {
            List<Tuition> userList = new List<Tuition>();
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand CMD_Select = new SqlCommand("SELECT * FROM Tuition WHERE SName LIKE @search OR Date LIKE @search OR Total LIKE @search ORDER BY Date", con);
                CMD_Select.Parameters.Add(new SqlParameter("@search", '%' + search + '%'));
                SqlDataReader reader = CMD_Select.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new Tuition(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2),
                        reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5),
                        reader.GetString(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetString(10)));
                }
                con.Close();

            }

            return userList;
        }
     
        // get tuition paid per month

        public static List<Tuition> GetdebitperM(string date)
        {
            List<Tuition> userList = new List<Tuition>();
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand CMD_select = new SqlCommand();
                SqlCommand CMD_Select = new SqlCommand("SELECT * FROM Tuition WHERE (SUBSTRING(Date,4,2) LIKE @search) ORDER BY Date", con);
                CMD_Select.Parameters.Add(new SqlParameter("@search", '%' + date + '%'));
                SqlDataReader reader = CMD_Select.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new Tuition(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2),
                        reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5),
                        reader.GetString(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetString(10)));
                }
                con.Close();

            }

            return userList;
        }



        //edit credit 

        public static List<CreditDetail> GetcreditperID(int ID)
        {
            List<CreditDetail> userList = new List<CreditDetail>();
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand CMD_Select = new SqlCommand("SELECT * FROM Credit WHERE ID = @search", con);
                CMD_Select.Parameters.Add(new SqlParameter("@search", ID));
                SqlDataReader reader = CMD_Select.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new CreditDetail(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), 
                        reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetBoolean(6)));
                }
                con.Close();

            }

            return userList;
        }



        public static List<Tuition> GetDebitperID(int ID)
        {
            List<Tuition> userList = new List<Tuition>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand CMD_Select = new SqlCommand("SELECT * FROM Tuition WHERE ID = @search", con);
                CMD_Select.Parameters.Add(new SqlParameter("@search", ID));
                SqlDataReader reader = CMD_Select.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new Tuition(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2),
                        reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5),
                        reader.GetString(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetString(10)));
                }
                
                con.Close();

            }

            return userList;
        }
        public static void UpdateCredit(int ID, string Name, int amount, string notes, string recipient, bool petty, string date)
        {

         
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand CMD_Select = new SqlCommand("UPDATE Credit SET name = @Name, amount = @amount,"+
                    "Notes = @notes, recipient = @recip, petty = @petty, Date = @date Where ID = @ID " , con);
                CMD_Select.Parameters.Add(new SqlParameter("@Name", Name));
                CMD_Select.Parameters.Add(new SqlParameter("@amount", amount));
                CMD_Select.Parameters.Add(new SqlParameter("@notes", notes));
                CMD_Select.Parameters.Add(new SqlParameter("@recip", recipient));
                CMD_Select.Parameters.Add(new SqlParameter("@petty", petty));
                CMD_Select.Parameters.Add(new SqlParameter("@date", date));
                CMD_Select.Parameters.Add(new SqlParameter("@ID", ID));


                SqlDataReader reader = CMD_Select.ExecuteReader();

                con.Close();
            }

        }


        public static void UpdateDebit(int ID, string Name, int assess, int academic, int bus, int books, string date, int pa, int remain, string notes)
        {
            int total = assess + academic + bus + books;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand CMD_Select = new SqlCommand("UPDATE Tuition SET SName = @Name, assess = @assess," +
                    "academic = @academic, bus = @bus, books = @books, Date = @date, Total = @total, Paid = @pai, remaining = @remain, notes = @notes  Where ID = @ID ", con);
                CMD_Select.Parameters.Add(new SqlParameter("@Name", Name));
                CMD_Select.Parameters.Add(new SqlParameter("@assess", assess));
                CMD_Select.Parameters.Add(new SqlParameter("@academic", academic));
                CMD_Select.Parameters.Add(new SqlParameter("@bus", bus));
                CMD_Select.Parameters.Add(new SqlParameter("@books", books));
                CMD_Select.Parameters.Add(new SqlParameter("@date", date));
                CMD_Select.Parameters.Add(new SqlParameter("@total", total));
                CMD_Select.Parameters.Add(new SqlParameter("@pai", pa));
                CMD_Select.Parameters.Add(new SqlParameter("@notes", notes));
                CMD_Select.Parameters.Add(new SqlParameter("@remain", remain));
                CMD_Select.Parameters.Add(new SqlParameter("@ID", ID));


                SqlDataReader reader = CMD_Select.ExecuteReader();

                con.Close();
            }

        }

        public static void Updatenote(int ID, string notes)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand CMD_Select = new SqlCommand("UPDATE Tuition SET notes = @notes Where ID = @ID ", con);
                CMD_Select.Parameters.Add(new SqlParameter("@notes", notes));
                CMD_Select.Parameters.Add(new SqlParameter("@ID", ID));
                SqlDataReader reader = CMD_Select.ExecuteReader();
                con.Close();
            }
        }

        // credit per month 
        public static List<CreditDetail> GetCreditp(string date)
        {
            List<CreditDetail> userList = new List<CreditDetail>();
     
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand CMD_select = new SqlCommand();
                SqlCommand CMD_Select = new SqlCommand("SELECT * FROM Credit WHERE (substring(Date,4,2) LIKE @search)", con);
                CMD_Select.Parameters.Add(new SqlParameter("@search", '%' + date + '%'));
                SqlDataReader reader = CMD_Select.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new CreditDetail(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetBoolean(6)));
                }
                con.Close();

            }

            return userList;
        }

        //petty per month
        public static List<CreditDetail> Getpetty(string date)
        {
            List<CreditDetail> userList = new List<CreditDetail>();
          
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand CMD_select = new SqlCommand();
                SqlCommand CMD_Select = new SqlCommand("SELECT name, SUM(amount) FROM Credit WHERE ((substring(Date,4,2) LIKE @search) and petty = 1) GROUP BY name", con);
                CMD_Select.Parameters.Add(new SqlParameter("@search", '%' + date + '%'));
                SqlDataReader reader = CMD_Select.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new CreditDetail(reader.GetString(0), reader.GetInt32(1),(0)));
                }
                con.Close();

            }

            return userList;
        }


        // revenue
        public static int GetRevenue()
        {

            int total = 0;
       
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand CMD_select = new SqlCommand();
                CMD_select.Connection = con;
                CMD_select.CommandText = "SELECT SUM(paid) FROM Tuition";
                SqlDataReader reader = CMD_select.ExecuteReader();
                while (reader.Read())
                {
                    total += reader.GetInt32(0);
                }
                con.Close();
            }
            using (SqlConnection con2 = new SqlConnection(connectionString))
                {
                con2.Open();
                SqlCommand CMD_select2 = new SqlCommand();
                CMD_select2.Connection = con2;
                CMD_select2.CommandText = "SELECT SUM(amount) FROM Credit";
                SqlDataReader reader2 = CMD_select2.ExecuteReader();
                while (reader2.Read())
                {
                    total -= reader2.GetInt32(0);
                    return total;
                }
                con2.Close();
            }
            return 0;
        }

        // delete credit 

        public static void DeleteCredit(int N)
        {

   
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand CMD_Select = new SqlCommand("Delete FROM Credit WHERE ID = @search", con);
                CMD_Select.Parameters.Add(new SqlParameter("@search", N));
                SqlDataReader reader = CMD_Select.ExecuteReader();

                con.Close();
            }

        }

        //Delete Debit 

        public static void DeleteDebit(int N)
        {

     
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand CMD_Select = new SqlCommand("Delete FROM Tuition WHERE ID = @search", con);
                CMD_Select.Parameters.Add(new SqlParameter("@search", N));
                SqlDataReader reader = CMD_Select.ExecuteReader();

                con.Close();
            }

        }
        // Get Credit

        public class CreditDetail
        {
            public int ID { get; set; }
            public string CName { get; set; }
            public int amount { get; set; }
            public string date { get; set; }
            public string recip { get; set; }
            public string notes { get; set; }
            public bool petty { get; set; }

            public CreditDetail(int n, string name, int amount2, string date2, string notes2, string recipient, bool petty2)
            {
                ID = n;
                CName = name;
                amount = amount2;
                notes = notes2;
                date = date2;
                recip = recipient;
                petty = petty2;
            }

            public CreditDetail(string name, int amount2)
            {
                date = name;
                amount = amount2;
            }
            public CreditDetail(string name, int amount2, int z)
            {
                CName = name;
                amount = amount2;
            }


        }

        //Tuition get
        public class Tuition
        {
            public int ID { get; set; }
            public string SName { get; set; }
            public int assess { get; set; }
            public int academic { get; set; }
            public int bus { get; set; }
            public int books { get; set; }
            public string date { get; set; }
            public int total { get; set; }
            public int paid { get; set; }
            public int remaining { get; set; }
            
            public string notes { get; set; }
            
            public Tuition(int ID2, string name, int assess2, int academic2, int bus2, int book, string Date, int Total, int paid2, int remain, string note)
            {
                ID = ID2;
                SName = name;
                assess = assess2;
                academic = academic2;
                bus = bus2;
                books = book;
                total = Total;
                date = Date;
                notes = note;
                remaining = remain;
                paid = paid2;
            }

            public Tuition(string Date, int amount2)
            {
                date = Date;
                total = amount2;
            }
        }

    }
}
