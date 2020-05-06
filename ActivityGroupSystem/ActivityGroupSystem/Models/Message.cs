using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActivityGroupSystem.Models
{
    public class Message
    {
        private string _memberId;
        private string _message;

        public Message(string memberId, string message)
        {
            _memberId = memberId;
            _message = message;
        }

        public string MemberId
        {
            get
            {
                return _memberId;
            }
        }
        public string MessageContent
        {
            get
            {
                return _message;
            }
        }
    }
}