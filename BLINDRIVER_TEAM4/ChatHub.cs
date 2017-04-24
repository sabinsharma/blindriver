using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using BLINDRIVER_TEAM4.Models;
using System.Web.Security;

namespace BLINDRIVER_TEAM4
{
    [HubName("chatHub")]
    public class ChatHub : Hub
    {
        private BlindRiverContext db = new BlindRiverContext();

        public override System.Threading.Tasks.Task OnConnected()
        {
            string clientId = Context.ConnectionId;
            string data = clientId;
            string count = "";
            try
            {
                //count = GetCount().ToString();
                count = "1";
            }
            catch (Exception d)
            {
                count = d.Message;
            }
            Clients.Caller.receiveMessage("ChatHub", data, count);
            return base.OnConnected();
        }

        [HubMethodName("hubconnect")]
        public void Get_Connect(String username, String userid, String connectionid)
        {
            string count = "";
            string msg = "";
            string list = "";

            // check if user is admin
            var admin = db.Members.Where(m => m.Username == username && m.RoleId == 3);
            if (admin != null)
            {
                count = "1";
                msg = "Welcome back " + username;
                list = "";

                var id = Context.ConnectionId;
                HttpCookie CookieAdminId = new HttpCookie("AdminId");
                CookieAdminId.Expires.AddDays(365);
                CookieAdminId.Value = id;


                string[] Exceptional = new string[1];
                Exceptional[0] = id;
                Clients.Caller.receiveMessage("RU", msg, list);
                Clients.AllExcept(Exceptional).receiveMessage("AdminOnline", username + " " + id, count);
            }
            else
            {
                count = "1";
                msg = "Welcome to Waterway Hospital Live Chat!";
                list = "";

                var id = Context.ConnectionId;
                string[] Exceptional = new string[1];
                Exceptional[0] = id;
                Clients.Caller.receiveMessage("RU", msg, list);
                Clients.AllExcept(Exceptional).receiveMessage("NewConnection", username + " " + id, count);
            }           
        }

        [HubMethodName("privatemessage")]
        public void Send_PrivateMessage(String msgFrom, String msg, String touserid)
        {
            var id = Context.ConnectionId;
            Clients.Caller.receiveMessage(msgFrom, msg, touserid);
            Clients.Client(touserid).receiveMessage(msgFrom, msg, id);
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            string count = "";
            string msg = "";
            string clientId = Context.ConnectionId;
            //DeleteRecord(clientId);
            try
            {
                count = "0";
            }
            catch (Exception d)
            {
                msg = "DB Error " + d.Message;
            }
            string[] Exceptional = new string[1];
            Exceptional[0] = clientId;
            Clients.AllExcept(Exceptional).receiveMessage("NewConnection", clientId + " leave", count);
            return base.OnDisconnected(stopCalled);
        }
    }
}