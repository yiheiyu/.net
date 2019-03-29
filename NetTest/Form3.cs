using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetTest
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private FTPObject ftpobject = new FTPObject();

        private void Form3_Load(object sender, EventArgs e)
        {
            
            this.textBox1.Text = ftpobject.FtpHost;
            this.textBox2.Text = ftpobject.Username;
            this.textBox3.Text = ftpobject.Password;
        }

        /// <summary>
        /// 更新storeinfo.xml
        /// </summary>
        public void updateLocalXML()
        {
            try
            {
                string loaclPath = System.AppDomain.CurrentDomain.BaseDirectory;

                FTPHelper ftp = new FTPHelper(new Uri(string.Format("ftp://{0}", ftpobject.FtpHost)), ftpobject.Username, ftpobject.Password);

                bool ftpResult = ftp.DownloadFile("StoreInfo.xml", loaclPath);

                if (ftpResult)
                {
                    MessageBox.Show("本地数据更新成功！请退出程序重新运行。", "更新提示");
                }
                else
                {
                    MessageBox.Show("本地数据更新失败！", "更新提示");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,"错误提示");
                return;
                
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.updateLocalXML();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new FTPObject(this.textBox1.Text, this.textBox2.Text, this.textBox3.Text);
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close(); //关闭窗口
        }
    }
}
