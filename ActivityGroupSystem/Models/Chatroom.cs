using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActivityGroupSystem.Models
{
    public class Chatroom
    {
        private List<Message> _messageList = new List<Message>();

        /*Willie Start*/
        public void SendMessage(string memberId, string messageContent)
        {
            Message message = new Message(memberId, messageContent);
            _messageList.Add(message);
        }
        public List<Message> MessageList
        {
            get
            {
                return _messageList;
            }
        }
        /*Willie End*/
    }
}