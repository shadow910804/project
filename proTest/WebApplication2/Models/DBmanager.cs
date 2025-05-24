using System;
using System.Collections.Generic;
using System.Data;
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
                        productID = reader.GetInt32(reader.GetOrdinal("productID")),
                        userID = reader.GetInt32(reader.GetOrdinal("userID")),
                        userName = reader.GetString(reader.GetOrdinal("userName")),
                        main = reader.GetString(reader.GetOrdinal("main")),
                        date = reader.GetDateTime(reader.GetOrdinal("date")),
                        score = reader.GetInt32(reader.GetOrdinal("score"))
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
            SqlCommand sqlcommand = new SqlCommand(@"INSERT INTO message(replyID,userID,productID,userName,main,date,score) VALUES(@replyID,@userID,@productID,@userName,@main,@date,@score)");
            sqlcommand.Connection = sqlconnection;

            sqlcommand.Parameters.Add(new SqlParameter("@replyID", m.replyID));
            sqlcommand.Parameters.Add(new SqlParameter("@userID", m.userID));
            sqlcommand.Parameters.Add(new SqlParameter("@productID", m.productID));
            sqlcommand.Parameters.Add(new SqlParameter("@userName", m.userName));
            sqlcommand.Parameters.Add(new SqlParameter("@main", m.main));
            sqlcommand.Parameters.Add(new SqlParameter("@date", m.date));
            sqlcommand.Parameters.Add(new SqlParameter("@score", m.score));

            sqlconnection.Open();
            sqlcommand.ExecuteNonQuery();
            sqlconnection.Close();
        }
        //存入留言↑

        //取得剛新增留言的ID↓
        public int getNewMessageID(messages m)
        {
            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM message WHERE userID=@u AND main=@m AND date=@date");
            sqlCommand.Parameters.Add(new SqlParameter("@u", m.userID));
            sqlCommand.Parameters.Add(new SqlParameter("@m", m.main));
            sqlCommand.Parameters.Add(new SqlParameter("@date", m.date));
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();

            try{
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int messageID = reader.GetInt32(reader.GetOrdinal("messageID"));
                        sqlConnection.Close();
                        return messageID;
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                sqlConnection.Close();
                return 0;
            }
            throw new Exception();
        }
        //取得剛新增留言的ID↑


        //修改留言↓
        public void fixMessage(messages m)
        {
            SqlConnection sqlconnection = new SqlConnection(connStr);
            SqlCommand sqlcommand = new SqlCommand(@"UPDATE message SET main = @main,date = @date,score = @score WHERE userID = @i AND messageID = @m");
            sqlcommand.Connection = sqlconnection;

            sqlcommand.Parameters.Add(new SqlParameter("@m", m.messageID));
            sqlcommand.Parameters.Add(new SqlParameter("@i", m.userID));
            sqlcommand.Parameters.Add(new SqlParameter("@main", m.main));
            sqlcommand.Parameters.Add(new SqlParameter("@date", m.date));
            sqlcommand.Parameters.Add(new SqlParameter("@score", m.score));

            sqlconnection.Open();
            sqlcommand.ExecuteNonQuery();
            sqlconnection.Close();
        }
        //修改留言↑


        //刪除留言↓
        public void deleteMessage(messages m)
        {
            SqlConnection sqlconnection = new SqlConnection(connStr);
            SqlCommand sqlcommand = new SqlCommand(@"DELETE FROM message　WHERE messageID = @m");
            sqlcommand.Connection = sqlconnection;
            try{
                sqlcommand.Parameters.Add(new SqlParameter("@m", m.messageID));

                sqlconnection.Open();
                sqlcommand.ExecuteNonQuery();
                sqlconnection.Close();
            }
            catch{
                sqlconnection.Close();
            }
        }
        //刪除留言↑




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
                        userName = reader.GetString(reader.GetOrdinal("userName")),
                        buyOrSell = reader.GetString(reader.GetOrdinal("buyOrSell"))
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
    }
}