using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading;

namespace NetTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            readxml();
            //startVnc();
            //startPing("10.2.76.103");
            //MessageBox.Show(startPing("10.2.76.103"));

            //写入vnc默认端口 和 默认密码
            IniFiles ini = new IniFiles();
            string vnc_port = ini.IniReadValue("VNC", "port");
            string vnc_pass = ini.IniReadValue("VNC", "password");
            if (vnc_port == "") ini.IniWriteValue("VNC", "port", "-62"); 
            if (vnc_pass == "") ini.IniWriteValue("VNC", "password", "M032j04");//Pizzay04u8

            

        }

        /// <summary>
        /// 读取xml文件
        /// </summary>
        public void readxml()
        {
            Store store = null;
            ListViewItem lv = null;
            XmlDocument doc = new XmlDocument();
            doc.Load("StoreInfo.xml");
            XmlNode xn = doc.SelectSingleNode("Stores");
            XmlNodeList xnl = xn.ChildNodes;
            foreach(XmlNode xm in xnl)
            {
                lv = new ListViewItem();
                XmlElement xe = (XmlElement)xm;
                store = new Store();
                store.Id = xe.GetAttribute("id").ToString();
                store.Name = xe.GetAttribute("name").ToString();
                store.Ip = xe.GetAttribute("ip").ToString();
                lv.Text = store.Ip;
                lv.SubItems.Add(store.Id);
                lv.SubItems.Add(store.Name);
                lv.SubItems.Add(store.Server);
                lv.SubItems.Add("");
                lv.SubItems.Add(store.Server2);


                // 添加数据
                listView1.Items.Add(lv);
            }
        }

        /// <summary>
        /// 根据传入的字段进行筛选
        /// </summary>
        /// <param name="str">条件</param>
        public void checkXmlByStr(string str)
        {
            // 清除所有项
            this.listView1.Items.Clear();
            Store store = null;
            ListViewItem lv = null;
            List<ListViewItem> ls = new List<ListViewItem>();
            XmlDocument doc = new XmlDocument();
            doc.Load("StoreInfo.xml");
            XmlNode xn = doc.SelectSingleNode("Stores");
            XmlNodeList xnl = xn.ChildNodes;
            foreach (XmlNode xm in xnl)
            {
                lv = new ListViewItem();
                 
                XmlElement xe = (XmlElement)xm;
                store = new Store();
                store.Id = xe.GetAttribute("id").ToString();
                store.Name = xe.GetAttribute("name").ToString();
                store.Ip = xe.GetAttribute("ip").ToString();

                if (store.Id.Equals(str))
                {
                    lv.Text = store.Ip;
                    lv.SubItems.Add(store.Id);
                    lv.SubItems.Add(store.Name);
                    lv.SubItems.Add(store.Server);
                    lv.SubItems.Add("");
                    lv.SubItems.Add(store.Server2);
                    ls.Add(lv);
                }
                else if (store.Name.IndexOf(str) != -1)
                {
                    lv.Text = store.Ip;
                    lv.SubItems.Add(store.Id);
                    lv.SubItems.Add(store.Name);
                    lv.SubItems.Add(store.Server);
                    lv.SubItems.Add("");
                    lv.SubItems.Add(store.Server2);
                    ls.Add(lv);
                }
                else if(str == "")
                {
                    this.readxml();
                    break;
                } 
            }

            foreach(ListViewItem i in ls)
            {
                listView1.Items.Add(i);
            }

            


        }


        public void checkSuffic(string str)
        {

        }

        public void startMstsc(string ip)
        {
            Process CmdProcess = new Process();
            CmdProcess.StartInfo.FileName = "cmd.exe";
            CmdProcess.StartInfo.CreateNoWindow = true;         // 不创建新窗口    
            CmdProcess.StartInfo.UseShellExecute = false;       //不启用shell启动进程  
            CmdProcess.StartInfo.RedirectStandardInput = true;  // 重定向输入    
            CmdProcess.StartInfo.RedirectStandardOutput = true; // 重定向标准输出    
            CmdProcess.StartInfo.RedirectStandardError = true;  // 重定向错误输出  
            CmdProcess.StartInfo.Arguments = "/c " + String.Format("mstsc -v:{0}",ip);//“/C”表示执行完命令后马上退出  
            CmdProcess.Start();//执行  
            CmdProcess.StandardOutput.ReadToEnd();//获取返回值  
            CmdProcess.WaitForExit();//等待程序执行完退出进程
            CmdProcess.Close();//结束 
        }

        /// <summary>
        /// ping 检测传入的IP
        /// </summary>
        /// <param name="host">IP</param>
        /// <returns></returns>
        public string startPing(string host)
        {

            Ping p1 = new Ping();
            PingReply reply = p1.Send(host); //发送主机名或Ip地址
            if (reply.Status == IPStatus.Success)
            {
                return "OK";
            }
            else
            {
                return "Fail";
            }
        }

        /// <summary>
        /// ping 检测传入的IP（显示控制台）2
        /// </summary>
        /// <param name="host"></param>
        public void pingByIp2(string host)
        {
            Process CmdProcess = new Process();
            CmdProcess.StartInfo.FileName = "ping.exe";
            //CmdProcess.StartInfo.CreateNoWindow = false;         // 不创建新窗口    
            CmdProcess.StartInfo.UseShellExecute = false;       //不启用shell启动进程  
            CmdProcess.StartInfo.RedirectStandardInput = true;  // 重定向输入   

            //CmdProcess.StartInfo.RedirectStandardOutput = true; // 重定向标准输出    
            //CmdProcess.StartInfo.RedirectStandardError = true;  // 重定向错误输出  
            CmdProcess.StartInfo.Arguments = String.Format(" {0} /t", host);

            CmdProcess.Start();//执行  
            //CmdProcess.StandardInput.WriteLine(String.Format("-t", ip))
            CmdProcess.Close();//结束 
        }

        /// <summary>
        /// ping 检测传入的IP（显示控制台)
        /// </summary>
        /// <param name="host"></param>
        public void pingByIp(string host)
        {
           
            Process.Start("cmd.exe", "/k ping " + host + " /t");
             
        }


        public void startVnc(string ip,string password)
        {
            IniFiles ini = new IniFiles();
            string vncUrl = ini.IniReadValue("VNC", "path");
            string vnc_port = ini.IniReadValue("VNC", "port");
            string vnc_pass = ini.IniReadValue("VNC", "password");

            if(vncUrl == "")
            {
                DialogResult dr =  MessageBox.Show(@"请先在设置里面设置VNC的路径(例如：E:\UltraVNC\vncviewer.exe)","错误提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
                if (dr == DialogResult.Cancel) return;
                else
                {
                    new Form2().ShowDialog();
                }
            }
            else
            {
                //string vncUrl = @"E:\UltraVNC\vncviewer.exe";
                Process CmdProcess = new Process();
                CmdProcess.StartInfo.FileName = vncUrl;
                CmdProcess.StartInfo.CreateNoWindow = true;         // 不创建新窗口    
                CmdProcess.StartInfo.UseShellExecute = false;       //不启用shell启动进程  
                CmdProcess.StartInfo.RedirectStandardInput = true;  // 重定向输入    
                CmdProcess.StartInfo.RedirectStandardOutput = true; // 重定向标准输出    
                CmdProcess.StartInfo.RedirectStandardError = true;  // 重定向错误输出  
                CmdProcess.StartInfo.Arguments = "/c " + String.Format("vncviewer {0}:{1} -password {2}", ip, vnc_port,vnc_pass);//“/C”表示执行完命令后马上退出  
                CmdProcess.Start();//执行  
                //CmdProcess.WaitForExit();//等待程序执行完退出进程
                CmdProcess.Close();//结束 
            }
            
        }

        /// <summary>
        /// 拷贝主机IP地址到剪贴板
        /// </summary>
        /// <param name="host">主机IP地址</param>
        public void copyStrByHost(string host)
        {
            Clipboard.SetDataObject(host);
        }

        private void ultraVNCViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 fm2 = new Form2();
            fm2.ShowDialog();
        }

        private void 服务器主机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                //string type = listView1.SelectedItems[0].SubItems[1].Text;
                Store store = new Store(ip);
                startMstsc(store.Server);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string str = toolStripTextBox1.Text;
            
            // 筛选数据
            checkXmlByStr(str);
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键
            {
                this.toolStripButton2_Click(sender, e);
            }
        }

        private void pOS收银机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string pass = "Pizzay04u8";
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                startVnc(store.PosClient1,pass);
            }
        }

        private void pOS一体机2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string pass = "Pizzay04u8";
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                startVnc(store.PosClient2, pass);
            }
        }

        private void pOS一体机3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string pass = "Pizzay04u8";
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                startVnc(store.PosClient3, pass);
            }
        }

        private void 派送台主机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string pass = "Pizzay04u8";
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                startVnc(store.SendHost, pass);
            }
        }

        private void 跟踪台主机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string pass = "Pizzay04u8";
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                startVnc(store.TrackHost, pass);
            }
        }

        private void 经理台主机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string pass = "Pizzay04u8";
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                startVnc(store.ManagerHost, pass);
            }
        }

        private void 监控台主机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string pass = "Pizzay04u8";
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                startVnc(store.MonitorHost, pass);
            }
        }

        private void 虚拟POSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string pass = "Pizzay04u8";
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                startVnc(store.VirtualPos, pass);
            }
        }

        public delegate void AddListViewItems(ListViewItem item);
    
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
            Thread th = new Thread(new ThreadStart(delegate
            {
                foreach (ListViewItem item in listView1.Items)
                {
                    this.Invoke(new AddListViewItems(InvokeAdd),item);
                    Thread.Sleep(10);
                }
            }));

            th.Start();
            
        }

        public void InvokeAdd(ListViewItem item)
        {
            // 获取Store对象
            Store store = new Store(item.SubItems[0].Text);
            listView1.Items[item.Index].SubItems[4].Text = startPing(store.Server);
            listView1.Items[item.Index].SubItems.Add(startPing(store.Server2));
            listView1.Items[item.Index].SubItems.Add(DateTime.Now.ToString());
            Console.WriteLine(store.Ip);
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void 服务器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                pingByIp(store.Server);
            }

        }

        private void 制作台主机ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string pass = "Pizzay04u8";
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                startVnc(store.MakeHost, pass);
            }
        }

        private void pOS一体机1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                pingByIp(store.PosClient1);
            }
        }

        private void pOS一体机2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                pingByIp(store.PosClient2);
            }
        }

        private void pOS一体机3ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                pingByIp(store.PosClient3);
            }
        }

        private void 虚拟POSToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                pingByIp(store.VirtualPos);
            }
        }

        private void 派送台主机ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                pingByIp(store.SendHost);
            }
        }

        private void 跟踪台主机ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                pingByIp(store.TrackHost);
            }
        }

        private void 经理台主机ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                pingByIp(store.ManagerHost);
            }
        }

        private void 监控台主机ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                pingByIp(store.MonitorHost);
            }
        }

        private void 制作台主机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                pingByIp(store.MakeHost);
            }
        }

        private void 服务器ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                copyStrByHost(store.Server);
            }
        }

        private void pOS一体机1ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                copyStrByHost(store.PosClient1);
            }
        }

        private void pOS一体机2ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                copyStrByHost(store.PosClient2);
            }
        }

        private void pOS一体机3ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                copyStrByHost(store.PosClient3);
            }
        }

        private void 虚拟POSToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                copyStrByHost(store.VirtualPos);
            }
        }

        private void 制作台主机ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                copyStrByHost(store.MakeHost);
            }
        }

        private void 派单台主机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                copyStrByHost(store.SendHost);
            }
        }

        private void 跟踪台主机ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                copyStrByHost(store.TrackHost);
            }
        }

        private void 经理台主机ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                copyStrByHost(store.ManagerHost);
            }
        }

        private void 监控台主机ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                copyStrByHost(store.MonitorHost);
            }
        }

        private void 备用服务器主机ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                pingByIp(store.Server2);
            }
        }

        private void 备用服务器主机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                //string type = listView1.SelectedItems[0].SubItems[1].Text;
                Store store = new Store(ip);
                startMstsc(store.Server2);
            }
        }

        private void 备用服务器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                copyStrByHost(store.Server2);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            new VNCObject("0", "Pizzay04u8");

            if (toolStripMenuItem3.Checked)
            {
                toolStripMenuItem3.Checked = false;
            }
            else if (海灏ToolStripMenuItem.Checked)
            {
                海灏ToolStripMenuItem.Checked = false;
            }
            else
            {
                toolStripMenuItem2.Checked = true;
            }


        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            new VNCObject("-62", "M032j04");

            if (toolStripMenuItem2.Checked)
            {
                toolStripMenuItem2.Checked = false;
            }
            else if (海灏ToolStripMenuItem.Checked)
            {
                海灏ToolStripMenuItem.Checked = false;
            }
            else
            {
                toolStripMenuItem3.Checked = true;
            }
        }

        private void 海灏ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            new VNCObject("5900", "dml2019");

            if (toolStripMenuItem2.Checked)
            {
                toolStripMenuItem2.Checked = false;
            }
            else if (toolStripMenuItem3.Checked)
            {
                toolStripMenuItem3.Checked = false;
            }
            else
            {
                海灏ToolStripMenuItem.Checked = true;
            }
        }

        private void 贴条打印机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                pingByIp(store.LablePrinterHost);
            }
        }

        private void 贴条打印机ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                copyStrByHost(store.LablePrinterHost);
            }
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            /**
            this.toolTip1.IsBalloon = true;
            
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                string id = listView1.SelectedItems[0].SubItems[1].Text;
                string name = listView1.SelectedItems[0].SubItems[2].Text;
                Store store = new Store(id,name,ip);
                this.toolTip1.ToolTipTitle = string.Format("{0} {1}",name,id);
                this.toolTip1.SetToolTip(this.listView1, "Hello");
                
            }

            Thread th = new Thread(new ThreadStart(delegate
            {
                foreach (ListViewItem item in listView1.Items)
                {
                    this.Invoke(new AddListViewItems(InvokeAdd), item);
                    Thread.Sleep(10);
                }
            }));

            th.Start();


            this.toolTip1.IsBalloon = false;
            **/
        }

        private void 菜单屏主机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                pingByIp(store.MenuPCHost);
            }
        }

        private void 菜单屏主机ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                copyStrByHost(store.MenuPCHost);
            }
        }

        private void 主路由器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                pingByIp(store.RouterHost);
            }
        }

        private void g路由器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                pingByIp(store.Routing4GHost);
            }
        }

        private void 主路由器ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                copyStrByHost(store.RouterHost);
            }
        }

        private void g路由器ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                copyStrByHost(store.Routing4GHost);
            }
        }

        private void a4一体打印机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                pingByIp(store.A4PrinterHost);
            }
        }

        private void aP无线路由器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                pingByIp(store.APHost);
            }
        }

        private void a4一体打印机ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                copyStrByHost(store.A4PrinterHost);
            }
        }

        private void aP无线路由器ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                copyStrByHost(store.APHost);
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                pingByIp(store.Ticket1Host);
            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                pingByIp(store.Ticket2Host);
            }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                pingByIp(store.Ticket3Host);
            }
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                copyStrByHost(store.Ticket1Host);
            }
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                copyStrByHost(store.Ticket2Host);
            }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string ip = listView1.SelectedItems[0].Text;
                Store store = new Store(ip);
                copyStrByHost(store.Ticket3Host);
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 fm3 = new Form3();
            fm3.ShowDialog();
        }

        private void 更新餐厅信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form3().updateLocalXML();
        }

        
    }
}
