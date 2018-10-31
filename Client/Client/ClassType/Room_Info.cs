using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    public class Room_Info
    {
        private string RoomName;//会议室名字
        private string Nember_People;//人数
        private string Customer;//客户
        private string Business;//业务
        private string Datetime;//时间
        private string Endtime;//时间
        private string Tips;//备注
        private bool State;

        public Room_Info(string name, string nember, string customer, string business, string datetime,string endtime, string tips,bool state)
        {
            RoomName = name;
            Nember_People = nember;
            Customer = customer;
            Business = business;
            Datetime = datetime;
            Endtime = endtime;
            Tips = tips;
            State = state;
        }

        public string GetName()
        {
            return RoomName;
        }

        public string GetNember()
        {
            return Nember_People;
        }

        public string GetCustomer()
        {
            return Customer;
        }

        public string GetBusiness()
        {
            return Business;
        }

        public string GetDatetime()
        {
            return Datetime;
        }

        public string GetEndtime()
        {
            return Endtime;
        }

        public string GetTips()
        {
            return Tips;
        }

        public bool Getstate()
        {
            return State;
        }

        public void Setstate(bool state)
        {
            State = state;
        }
    }
}
