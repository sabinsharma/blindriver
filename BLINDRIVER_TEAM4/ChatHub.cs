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
            LiveChat admin = db.LiveChats.OrderByDescending(l=>l.Id).FirstOrDefault();
            string adminId = (admin != null) ? admin.ContextId : "";
            Clients.Caller.receiveMessage("ChatHub", clientId, adminId);
            return base.OnConnected();
        }

        [HubMethodName("hubconnect")]
        public void Get_Connect(String username, String userid, String connectionid)
        {
            string msg = "";
            var id = Context.ConnectionId;
            // check if user is admin
            Member admin = db.Members.Where(m => m.Username == username && m.RoleId == 3).FirstOrDefault();
            if (admin != null)
            {
                msg = "Admin " + username + " is online";

                LiveChat livechat = new LiveChat();
                livechat.AdminId = admin.Id;
                livechat.ContextId = userid;

                db.LiveChats.Add(livechat);
                db.SaveChanges();

                string[] Exceptional = new string[1];
                Exceptional[0] = id;
                Clients.Caller.receiveMessage("RU", msg, "");
                Clients.AllExcept(Exceptional).receiveMessage("AdminOnline", username, id);

            }
            else
            {
                msg = "Welcome to Waterway Hospital Live Chat!";

                string[] Exceptional = new string[1];
                Exceptional[0] = id;
                //Clients.Caller.receiveMessage("RU", msg, "");
                Clients.AllExcept(Exceptional).receiveMessage("NewConnection", username, id);
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
            string clientId = Context.ConnectionId;

            LiveChat livechat = db.LiveChats.Where(l => l.ContextId == clientId).FirstOrDefault();
            if (livechat != null)
            {
                db.LiveChats.Remove(livechat);
                db.SaveChanges();
            }          

            string[] Exceptional = new string[1];
            Exceptional[0] = clientId;
            Clients.AllExcept(Exceptional).receiveMessage("ConnectionLost", clientId + " leave", clientId);
            return base.OnDisconnected(stopCalled);
        }
    }
}