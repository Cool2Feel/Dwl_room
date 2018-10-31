using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    /// <summary>
    /// 会议室信息
    /// </summary>
    [Serializable]
    public class Class_Room
    {
        #region Properties
        //一级ID，代表惠南、坂田等厂区编号
        public int topID { get; set; }
        //二级ID，代表楼层
        public int middleID { get; set; }
        //三级ID，房间号码
        public int lastID { get; set; }
        //房间状态编码，0代表空，1代表使用中
        public int status { get; set; }

        public int mininumber { get; set; }

        public int maxnumber { get; set; }

        public int cunpinCoffee { get; set; }
        public int oneaddtwoCoffee { get; set; }

        private bool cunpinCoffeeAddMilk;

        public bool CunpinCoffeeAddMilk
        {
            get { return cunpinCoffeeAddMilk; }
            set { cunpinCoffeeAddMilk = value; }
        }
        private bool cunpinCoffeeAddSugar;

        public bool CunpinCoffeeAddSugar
        {
            get { return cunpinCoffeeAddSugar; }
            set { cunpinCoffeeAddSugar = value; }
        }
        private bool oneaddtwoCoffeeAddMilk;

        public bool OneaddtwoCoffeeAddMilk
        {
            get { return oneaddtwoCoffeeAddMilk; }
            set { oneaddtwoCoffeeAddMilk = value; }
        }
        private bool oneaddtwoCoffeeAddSugar;

        public bool OneaddtwoCoffeeAddSugar
        {
            get { return oneaddtwoCoffeeAddSugar; }
            set { oneaddtwoCoffeeAddSugar = value; }
        }

        private int blacktea;

        public int Blacktea
        {
            get { return blacktea; }
            set { blacktea = value; }
        }
        private int greentea;

        public int Greentea
        {
            get { return greentea; }
            set { greentea = value; }
        }

        //是否有会议电话机
        public bool hasConferenceTEL { get; set; }
        //是否能抽烟
        public bool isSmoking { get; set; }
        //是否有wifi
        public bool isHaveWifi { get; set; }

        public List<scheduleRoom> roomScheduleList = new List<scheduleRoom>();

        #endregion

        #region roominfo
        /// <summary>
        /// 开会信息
        /// </summary>
        [Serializable]
        public class scheduleRoom
        {
            //安排编号
            public int id { get; set; }
            //开始时间
            public int startTime { get; set; }
            //结束时间
            public int endTime { get; set; }
            //客户
            public string Customer { get; set; }
            //业务员
            public string Clerk { get; set; }
            //预定人
            public string OrderPerson { get; set; }

            public string peopleNumber { get; set; }
            //到访目的
            public string Aim { get; set; }

        }
        #endregion
    }
    [Serializable]
    public class IpRoom
    {
        public List<RoomId> roomIDList = new List<RoomId>();
        [Serializable]
        public class RoomId
        {
            //一级ID，代表惠南、坂田等厂区编号
            public int topID { get; set; }
            //二级ID，代表楼层
            public int middleID { get; set; }
            //三级ID，房间号码
            public int lastID { get; set; }
        }
    }

    [Serializable]
    public class OrderCoffee
    {
        private int cunpinNumber { get; set; }
        private int oneaddtwoNumber { get; set; }

    }



}
