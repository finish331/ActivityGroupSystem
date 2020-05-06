using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActivityGroupSystem.Models
{
    public class MemberHandler
    {
        private List<Member> _memberList = new List<Member>();

        /*Willie Start*/

        public List<string> GetBlackList(string memberId)
        {
            List<string> result = null;
            foreach (Member tempMember in _memberList)
            {
                if (tempMember.MemberId == memberId)
                {
                    result = tempMember.BlackList.ToList();
                }
            }
            return result;
        }

        public List<Member> SearchMemberInfo(string keyWord)
        {
            List<Member> relatedMember = null;
            foreach (Member member in _memberList)
            {
                if (member.MemberId.Contains(keyWord) || member.MemberName.Contains(keyWord))
                {
                    relatedMember.Add(member);
                }
            }
            return relatedMember;
        }

        public bool BlackMember(string memberId, string blackMemberId)
        {
            foreach (Member member in _memberList)
            {
                if (member.MemberId == memberId)
                {
                    return member.BlackMember(blackMemberId);
                }
            }
            return false;
        }

        /*Willie End*/
    }
}