using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetTest
{
    class Store
    {
        // 餐厅id
        private string id;
        // 餐厅名称
        private string name;
        // ip段
        private string ip;

        public Store()
        {
            
        }

        public Store(string ip)
        {
            this.ip = ip;
        }

        public Store(string id,string name, string ip)
        {
            this.id = id;
            this.name = name;
            this.ip = ip;
        }

        // 餐厅服务器
        //private string server;




        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Ip { get => ip; set => ip = value; }
        /// <summary>
        /// 服务器
        /// </summary>
        public string Server { get => String.Format("{0}{1}",ip,"103");}
        /// <summary>
        /// 服务器2
        /// </summary>
        public string Server2 { get => String.Format("{0}{1}", ip, "104"); }
        /// <summary>
        /// POS
        /// </summary>
        public string PosClient1 { get => String.Format("{0}{1}", ip, "21"); }
        public string PosClient2 { get => String.Format("{0}{1}", ip, "22"); }
        public string PosClient3 { get => String.Format("{0}{1}", ip, "23"); }
        /// <summary>
        /// 制作台主机
        /// </summary>
        public string MakeHost { get => String.Format("{0}{1}", ip, "31"); }
        /// <summary>
        /// 派送台主机
        /// </summary>
        public string SendHost { get => String.Format("{0}{1}", ip, "41"); }
        /// <summary>
        /// 跟踪台主机
        /// </summary>
        public string TrackHost { get => String.Format("{0}{1}", ip, "42"); }
        /// <summary>
        /// 经理台主机
        /// </summary>
        public string ManagerHost { get => String.Format("{0}{1}", ip, "81"); }
        /// <summary>
        /// 监控台主机
        /// </summary>
        public string MonitorHost { get => String.Format("{0}{1}", ip, "193"); }
        /// <summary>
        /// 虚拟POS
        /// </summary>
        public string VirtualPos { get => String.Format("{0}{1}", ip, "102"); }
        /// <summary>
        /// 主路由器地址
        /// </summary>
        public string RouterHost { get => String.Format("{0}{1}", ip, "251"); }
        /// <summary>
        /// 副4G路由器地址
        /// </summary>
        public string Routing4GHost { get => String.Format("{0}{1}", ip, "252"); }
        /// <summary>
        /// 贴条打印机
        /// </summary>
        public string LablePrinterHost { get => String.Format("{0}{1}", ip, "161"); }
        /// <summary>
        /// 菜单屏PC地址
        /// </summary>
        public string MenuPCHost { get => string.Format("{0}{1}", ip, "194"); }
        /// <summary>
        /// A4打印机地址
        /// </summary>
        public string A4PrinterHost { get => string.Format("{0}{1}", ip, "82"); }
        /// <summary>
        /// AP无线路由器地址
        /// </summary>
        public string APHost { get => string.Format("{0}{1}", ip, "200"); }
        /// <summary>
        /// 小票打印机地址
        /// </summary>
        public string Ticket1Host { get => string.Format("{0}{1}", ip, "51"); }
        public string Ticket2Host { get => string.Format("{0}{1}", ip, "52"); }
        public string Ticket3Host { get => string.Format("{0}{1}", ip, "53"); }
    }
}
