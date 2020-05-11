using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActivityGroupSystem.Models
{
    public class Message
    {
        private string _memberId;
        private string _memberName;
        private string _messageContent;
        private string _time;

        public Message(string memberId, string memberName, string messageContent)
        {
            _memberId = memberId;
            _memberName = memberName;
            _messageContent = messageContent;

            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _time = now;
        }

        public string MemberId
        {
            get
            {
                return _memberId;
            }
        }

        public string MemberName
        {
            get
            {
                return _memberName;
            }
        }

        public string MessageContent
        {
            get
            {
                return _messageContent;
            }
        }

        public string Time
        {
            get
            {
                return _time;
            }
        }
    }
}