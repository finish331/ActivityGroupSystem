using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActivityGroupSystem.Models
{
    public class Member
    {
        private string _memberId;
        private string _memberName;
        private List<string> _friendList = new List<string>();
        private List<string> _blackList = new List<string>();
        private List<string> _invitedList = new List<string>();

        /*Willie Start*/

        public bool BlackMember(string blackMemberId)
        {
            foreach (string originBlack in _blackList)
            {
                if (originBlack == blackMemberId)
                {
                    return false;
                }
            }
            _blackList.Add(blackMemberId);
            return true;
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

        public List<string> BlackList
        {
            get
            {
                return _blackList;
            }
        }

        /*Willie End*/
    }
}