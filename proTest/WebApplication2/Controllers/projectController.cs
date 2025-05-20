using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Cryptography;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class projectController : Controller
    {
        //讀取資料庫中的留言
        public IActionResult Index(account a)
        {
            DBmanager db = new DBmanager();
            List<messages> m = new List<messages>(db.getMessages());
            m.Sort((m1, m2) =>
            {
                TimeSpan sp = m1.date - m2.date;
                if (sp.TotalSeconds > 0)
                    return -1;
                else if (sp.TotalSeconds < 0)
                    return 1;
                else
                    return 0;
            });
            m.Sort((m1, m2) =>
            {
                if (m1.messageID==m2.replyID)
                    return -1;
                else
                    return 0;
                
            });
            

            ViewBag.ID = a.ID;
            ViewBag.name = a.userName;
            ViewBag.messages = m;
            return View();
        }

        public List<messages> reList(messages m) {
            List<messages> temp = new List<messages>();
            if (0 == m.replyID)
                temp.Add(m);


            return temp;
        }

        //寄檢舉留言給管理員
        public void complainSend(letter l)
        {
            string body = $"檢舉內容:{l.content}\n騷擾:{l.harassment}\n色情:{l.pornography}\n威脅:{l.threaten}\n仇恨:{l.Hatred}\n詳細描述:{l.detail}"; // 信件內容

            MimeMessage message = new();
            message.From.Add(new MailboxAddress("Server", "testproject9487@gmail.com"));//(寄信者名稱, 寄送者信箱)
            message.To.Add(MailboxAddress.Parse("testproject9487@gmail.com"));// 目標信箱
            message.Subject = "測試信件";// 信件主旨
            message.Body = new TextPart("html")
            {
                Text = body
            };

            using SmtpClient client = new();
            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);//(送信郵件主機, 送信郵件主機連接埠, )
            client.Authenticate("testproject9487@gmail.com", "kphp eeng ektm czxu");//(帳號, 密碼)
            client.Send(message);
            client.Disconnect(true);
        }

        //刪除資料庫中的留言
        public void delMessage(messages m)
        {
            DBmanager dBmanager = new DBmanager();
            dBmanager.deleteMessage(m);
        }

        //修改資料庫中的留言
        public void modMessage(messages m)
        {
            DBmanager dBmanager = new DBmanager();
            dBmanager.fixMessage(m);
        }

        //存入留言
        public int setMessage(messages m)
        {
            DBmanager dBmanager = new DBmanager();
            dBmanager.keyinMessage(m);
            return dBmanager.getNewMessageID(m);
        }




        public IActionResult singin(account u)
        {
            if (Request.Method == "GET")
            {
                return View();
            }
            else
            {
                DBmanager dbmanager = new DBmanager();
                try
                {
                    dbmanager.login(u);
                    account accounts = dbmanager.login(u);
                    return RedirectToAction("Index", "project", new account { ID = accounts.ID, userName = accounts.userName });
                }
                catch
                {
                    Console.WriteLine("error");
                    return RedirectToAction("singin","project");
                }
            }
        }
    }
}
