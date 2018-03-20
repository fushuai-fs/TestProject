using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.IO;

namespace Common
{
    public class POP3Mail
    {
        private TcpClient Server;
        private NetworkStream NetStrm;
        private StreamReader RdStrm;
        private string Data;
        private byte[] szData;
        private string CRLF = "\r\n";// "/r/n";


        private string PopServer = "pop3.yeah.net";
        private int PopPort = 110;
        private string PopUserName = "";//@yeah.net
        private string PopPassword = "";

        private int emailCount = 0;

        public POP3Mail() { }

        public POP3Mail(string server, int port, string userName, string password)
        {
            this.PopServer = server;
            this.PopPort = port;
            this.PopUserName = userName;
            this.PopPassword = password;
        }
        public void Receive()
        {
            Connect();
            Retrieve();
            Disconnect();
        }
        /// <summary>
        /// 登陆服务器
        /// </summary>
        private Boolean Connect()
        {
            Boolean res = true;
            try
            {
                //用110端口新建POP3服务器连接 
                Server = new TcpClient(PopServer, PopPort);
                //初始化 
                NetStrm = Server.GetStream();
                //RdStrm = new StreamReader(Server.GetStream());
                RdStrm = new StreamReader(NetStrm);

                CheckCorrect(RdStrm.ReadLine(), "OK");

                //Console.WriteLine(RdStrm.ReadLine());

                //登录服务器过程 
                Data = "user " + PopUserName + CRLF;
                szData = System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
                NetStrm.Write(szData, 0, szData.Length);

                RdStrm = new StreamReader(NetStrm);
                CheckCorrect(RdStrm.ReadLine(), "OK");
                //Console.WriteLine(RdStrm.ReadLine());

                Data = "pass " + PopPassword + CRLF;
                szData = System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
                NetStrm.Write(szData, 0, szData.Length);

                CheckCorrect(RdStrm.ReadLine(), "OK");
                //Console.WriteLine(RdStrm.ReadLine());

                //向服务器发送STAT命令，从而取得邮箱的相关信息：邮件数量和大小 
                Data = "stat" + CRLF;
                szData = System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
                NetStrm.Write(szData, 0, szData.Length);

               string result =  CheckCorrectStr(RdStrm.ReadLine(), "OK");
                emailCount = Convert.ToInt32(result);
                //Console.WriteLine(RdStrm.ReadLine());
            }
            catch (InvalidOperationException e)
            {
                res = false;
            }
            return res;
        }


        private void Disconnect()
        {
            try
            {
                //向服务器发送QUIT命令从而结束和POP3服务器的会话 
                Data = "QUIT" + CRLF;
                szData = System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
                NetStrm.Write(szData, 0, szData.Length);

                //断开连接 
                NetStrm.Close();
                RdStrm.Close();
            }
            catch (Exception e)
            {
            }
        }

        private void Retrieve()
        {
            string szTemp;
            try
            {
                //根据邮件编号从服务器获得相应邮件 
                Data = "RETR " + emailCount + CRLF;
                szData = System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
                NetStrm.Write(szData, 0, szData.Length);

                szTemp = RdStrm.ReadLine();
                WriteLog(szTemp);
                if (szTemp[0] != '-')
                {
                    //不断地读取邮件内容，只到结束标志：英文句号 
                    while (szTemp != ".")
                    {
                        szTemp = RdStrm.ReadLine();
                        WriteLog(szTemp);
                    }



                    ////若BackupChBox未选中，则收取邮件后，删除保留在服务器上的邮件 
                    //if (BackupChBox.Checked == false)
                    //{
                    //    Data = "DELE" + MailNum.Text + CRLF;
                    //    szData = System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
                    //    NetStrm.Write(szData, 0, szData.Length); 
                    //}
                }

            }
            catch (InvalidOperationException err)
            {
                WriteLog(err.ToString());
            }
        }

        private bool CheckCorrect(string message, string check)
        {
            WriteLog(message);
            if (message.IndexOf(check) == -1) return false; else return true;
        }
        private string CheckCorrectStr(string message, string check)
        {
            WriteLog(message);
            if (message.IndexOf(check) == -1) return "";
            else
            {
                string[] strs = message.Split(' ');
                if (strs.Length > 1)
                    return strs[1].ToString();
                else
                    return "";
            }
        }


        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="error"></param>
        /// <param name="filePath"></param>
        protected void WriteLog(string error, string filePath = "")
        {
            if (string.IsNullOrEmpty(filePath)) { filePath = @"E:\Manage\Log\" + DateTime.Today.ToString("yyyyMMdd") + @"\" + DateTime.Now.ToString("HHmm") + ".txt"; }
            if (!Directory.Exists(System.IO.Path.GetDirectoryName(filePath))) { Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath)); }
            using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter sw = new System.IO.StreamWriter(fs, Encoding.UTF8))
                {
                    sw.Write(DateTime.Now.ToString("HH:mm:ss") + "\t");
                    sw.Write(error);
                    sw.WriteLine();
                    sw.Flush();
                }
            }
        }



    }
}
