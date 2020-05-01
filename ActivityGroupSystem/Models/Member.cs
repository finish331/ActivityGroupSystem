using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActivityGroupSystem.Models
{
    public class Member
    {
        private string memberId;
        private string memberName;
        private List<string> friendList = new List<string>();
        private List<string> blackList = new List<string>();
        private List<string> invitedList = new List<string>();
    }
}