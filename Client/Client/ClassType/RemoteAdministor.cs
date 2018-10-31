/*
 * Created by SharpDevelop.
 * User: shiy
 * Date: 2018/5/17
 * Time: 14:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Client
{
    /// <summary>
    /// Description of RemoteAdministor.
    /// </summary>
    [Serializable]
    public class RemoteAdministor
    {
        public string MessageType { get; set; }
        public string Ip { get; set; }
        //public string password { get; set; }
        public string userName { get; set; }

        public string roomName { get; set; }
        public string roomPeople { get; set; }
        public string roomCustomer { get; set; }
        public string roomBusiness { get; set; }
        public string roomDatetime { get; set; }
        public string roomTips { get; set; }
        public string roomIndex { get; set; }

        public string endSend { get; set; }
    }
}
