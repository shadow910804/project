using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication2.Models
{
    public class DBmanager
    {
        private readonly string connStr = "Data Source=(localdb)\\MSSQLLocalDB;Database=test;User ID=shadow;Password=shadow9487;Trusted_Connection=True";


        //取得留言、回覆↓
        public List<messages> getMessages()
        {
            List<messages> m = new List<messages>();

            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM message");
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    messages account = new messages
                    {
                        messageID = reader.GetInt32(reader.GetOrdinal("messageID")),
                        replyID = reader.GetInt32(reader.GetOrdinal("replyID")),
                        userName = reader.GetString(reader.GetOrdinal("userName")),
                        main = reader.GetString(reader.GetOrdinal("main")),
                        date = reader.GetDateTime(reader.GetOrdinal("date"))
                    };
                    m.Add(account);
                }
            }
            else
            {
                Console.WriteLine("資料庫為空！");
            }
            sqlConnection.Close();
            return m;
        }
        //取得留言、回覆↑


        //存入留言↓
        public void keyinMessage(messages m)
        {
            SqlConnection sqlconnection = new SqlConnection(connStr);
            SqlCommand sqlcommand = new SqlCommand(@"INSERT INTO message(replyID,userID,userName,main) VALUES(@replyID,@userID,@userName,@main)");
            sqlcommand.Connection = sqlconnection;

            sqlcommand.Parameters.Add(new SqlParameter("@replyID", m.replyID));
            sqlcommand.Parameters.Add(new SqlParameter("@userID", m.userID));
            sqlcommand.Parameters.Add(new SqlParameter("@userName", m.userName));
            sqlcommand.Parameters.Add(new SqlParameter("@main", m.main));

            sqlconnection.Open();
            sqlcommand.ExecuteNonQuery();
            sqlconnection.Close();
        }
        //存入留言↑


        //修改留言↓
        public void fixMessage(messages m)
        {
            SqlConnection sqlconnection = new SqlConnection(connStr);
            SqlCommand sqlcommand = new SqlCommand(@"UPDATE message SET main = @main,date = @date WHERE userID = @i");
            sqlcommand.Connection = sqlconnection;

            sqlcommand.Parameters.Add(new SqlParameter("@i", m.userID));
            sqlcommand.Parameters.Add(new SqlParameter("@main", m.main));
            sqlcommand.Parameters.Add(new SqlParameter("@date", m.date));

            sqlconnection.Open();
            sqlcommand.ExecuteNonQuery();
            sqlconnection.Close();
        }
        //修改留言↑


        //取得回覆↓(舊版
        public List<replys> getreplys()
        {
            List<replys> r = new List<replys>();

            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM reply");
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    replys account = new replys
                    {
                        replyID = reader.GetInt32(reader.GetOrdinal("messageID")),
                        messageID = reader.GetInt32(reader.GetOrdinal("messageID")),
                        userName = reader.GetString(reader.GetOrdinal("userName")),
                        main = reader.GetString(reader.GetOrdinal("main")),
                        date = reader.GetDateTime(reader.GetOrdinal("date"))
                    };
                    r.Add(account);
                }
            }
            else
            {
                Console.WriteLine("資料庫為空！");
            }
            sqlConnection.Close();
            return r;
        }
        //取得回覆↑(舊版


        public account login(account u)
        {
            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM users WHERE userName=@u AND password=@p");
            sqlCommand.Parameters.Add(new SqlParameter("@u", u.userName));
            sqlCommand.Parameters.Add(new SqlParameter("@p", u.password));
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    account account = new account
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("id")),
                        userName = reader.GetString(reader.GetOrdinal("userName"))
                    };
                    sqlConnection.Close();
                    return account;
                }
            }
            else
            {
                Console.WriteLine("資料庫為空！");
            }
            sqlConnection.Close();
            throw new Exception();
        }



        public void newAccount(account user)
        {
            SqlConnection sqlconnection = new SqlConnection(connStr);
            SqlCommand sqlcommand = new SqlCommand(@"INSERT INTO users(userName,password) VALUES(@username,@password)");
            sqlcommand.Connection = sqlconnection;

            sqlcommand.Parameters.Add(new SqlParameter("@username", user.userName));
            sqlcommand.Parameters.Add(new SqlParameter("@password", user.password));

            sqlconnection.Open();
            sqlcommand.ExecuteNonQuery();
            sqlconnection.Close();
        }
    }
}