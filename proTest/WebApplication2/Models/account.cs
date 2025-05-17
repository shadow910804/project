using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public int messageID { get; set; } = 0;

        public int replyID { get; set; } = 0;

        public int userID { get; set; }

        public string userName { get; set; } = "0202";

        [Required(ErrorMessage = "訊息不能為空！")]
        public string main { get; set; }

        public DateTime date { get; set; } = DateTime.Now;
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
