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
        private string CRLF = "/r/n";


        private string PopServer = "pop.yeah.net";
        private int PopPort = 110;
        private string PopUserName = "fushuaifs@yeah.net";
        private string PopPassword = "fushuai";

        public POP3Mail() { }

        public POP3Mail(string server, int port, string userName, string password)
        {
            this.PopServer = server;
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

                //Console.WriteLine(RdStrm.ReadLine());

                //登录服务器过程 
                Data = "USER " + PopUserName + CRLF;
                szData = System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
                NetStrm.Write(szData, 0, szData.Length);

                //Console.WriteLine(RdStrm.ReadLine());

                Data = "PASS " + PopPassword + CRLF;
                szData = System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
                NetStrm.Write(szData, 0, szData.Length);

                //Console.WriteLine(RdStrm.ReadLine());

                //向服务器发送STAT命令，从而取得邮箱的相关信息：邮件数量和大小 
                Data = "STAT" + CRLF;
                szData = System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
                NetStrm.Write(szData, 0, szData.Length);

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
                Data = "RETR " + CRLF;
                szData = System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
                NetStrm.Write(szData, 0, szData.Length);
                
                szTemp = RdStrm.ReadLine();

                if (szTemp[0] != '-')
                {
                    //不断地读取邮件内容，只到结束标志：英文句号 
                    while (szTemp != ".")
                    {
                        szTemp = RdStrm.ReadLine();
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
            }
        }




    }
}
