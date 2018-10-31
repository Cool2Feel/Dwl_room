/*
 * Created by SharpDevelop.
 * User: shiy
 * Date: 2018/5/17
 * Time: 14:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Server
{
    /// <summary>
    /// Description of RemoteAdministor.
    /// </summary>
    [Serializable]
    public class RemoteAdministor
    {
        //public string MessageType { get; set; }
        public string Ip { get; set; }
        public string password { get; set; }
        public string userName { get; set; }

        //public string LogState { get; set; }
    }
}
