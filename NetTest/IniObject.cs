using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetTest
{

    class VNCObject
    {
        private string path;
        private string port;
        private string password;
        private string section = "VNC";
        private IniFiles ini = new IniFiles();

        public VNCObject()
        {
            this.path = ini.IniReadValue(section, "path");
            this.port = ini.IniReadValue(section, "port");
            this.password = ini.IniReadValue(section, "password");
        }

        public VNCObject(string path,string port,string password)
        {
            ini.IniWriteValue(section, "path", path);
            ini.IniWriteValue(section, "port", port);
            ini.IniWriteValue(section, "password", password);
        }

        public VNCObject(string port, string password)
        {
            ini.IniWriteValue(section, "port", port);
            ini.IniWriteValue(section, "password", password);
        }

        public string Path { get => path; set => ini.IniWriteValue(section, "path", path); }
        public string Port { get => port; set => ini.IniWriteValue(section, "port", port); }
        public string Password { get => password; set => ini.IniWriteValue(section, "password", password); }
    }

    class FTPObject
    {
        private string ftpHost;
        private string username;
        private string password;
        private string section = "FTP";
        private IniFiles ini = new IniFiles();

        /// <summary>
        /// 无参构造
        /// </summary>
        public FTPObject()
        {
            this.ftpHost = ini.IniReadValue(section, "host");
            this.username = ini.IniReadValue(section, "username");
            this.password = ini.IniReadValue(section, "password");
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ftpHost">地址</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public FTPObject(string ftpHost,string username,string password)
        {
            ini.IniWriteValue(section, "host", ftpHost);
            ini.IniWriteValue(section, "username", username);
            ini.IniWriteValue(section, "password", password);
        }
        /// <summary>
        /// 封装字段
        /// </summary>
        public string FtpHost { get => ftpHost; set => ini.IniWriteValue(section, "host", ftpHost); }
        public string Username { get => username; set => ini.IniWriteValue(section, "username", username); }
        public string Password { get => password; set => ini.IniWriteValue(section, "password", password); }
    }
}
