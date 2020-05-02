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

        /*Willie End*/
    }
}