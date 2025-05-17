using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class letter
    {
        public string content { get; set; }
        public bool harassment { get; set; } = false;//騷擾
        public bool pornography { get; set; } = false;//色情內容
        public bool threaten { get; set; } = false;//威脅內容
        public bool Hatred { get; set; } = false;//仇恨或歧視內容
        public string detail { get; set; } = "無描述";

    }
    public class messages
    {
        private int message_ID = 0;
        public int messageID{
            get{
                return message_ID;
            }
            set{
                message_ID = value;
            }
        }
        private int reply_ID = 0;
        public int replyID{
            get{
                return reply_ID;
            }
            set{
                reply_ID = value;
            }
        }
        private string user_Name = "0202";
        public string userName{
            get{
                return user_Name;
            }
            set{
                user_Name = value;
            }
        }
        private string main_ = "無";
        public string main{
            get{
                return main_;
            }
            set{
                main_ = value;
            }
        }
        private DateTime _date = DateTime.Now;
        public DateTime date{
            get{
                return _date;
            }
            set{
                _date = value;
            }
        }
    }




    public class account
    {
        public int ID { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
    }
    static public class temp
    {
        static public int ID { get; set; }
        static public string userName { get; set; }
    }

    public class replys
    {
        public int replyID { get; set; }
        public int messageID { get; set; }
        public string userName { get; set; }
        public string main { get; set; }
        public DateTime date { get; set; }
    }
}
