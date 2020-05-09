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

        public bool AddFriend(string myMemberId, string friendId)
        {
            foreach (Member member in _memberList)
            {
                if (member.MemberId == friendId)
                {
                    return member.AddFriend(myMemberId);
                }
            }
            return false;
        }
        /*Willie End*/

        /* Ting Start */
        public Dictionary<string, string> LoadUserData(string memberId)
        {
            Member member = GetMemberById(memberId);
            if (member != null)
            {
                return member.GetData();
            }
            else
                return null;
        }

        public bool UpdateUserData(string memberId, Dictionary<string, string> newData)
        {
            Member member = GetMemberById(memberId);
            if (member != null)
            {
                member.UpdataData(newData);
                return true;
            }
            else
                return false;
        }

        public List<string> GetFriendsList(string memberId)
        {
            Member member = GetMemberById(memberId);
            if (member != null)
            {
                return member.FriendsList;
            }
            else
                return null;
        }

        public bool InviteMember(string inviterName, string memberId, string activityId)
        {
            Member member = GetMemberById(memberId);
            if (member != null)
            {
                member.Invited(inviterName, activityId);
                return true;
            }
            else
                return false;
        }

        public bool DeleteFriend(string memberId, string targetId)
        {
            Member member = GetMemberById(memberId);
            if (member != null)
            {
                member.DeleteFriend(targetId);
                return true;
            }
            else
                return false;
        }

        private Member GetMemberById(string id)
        {
            if (_memberList.Exists(member => member.MemberId == id))
                return _memberList.Find(member => member.MemberId == id);
            else
                return null;
        }
        /* Ting End */

        /*Hsu start*/
        public bool CreateNewMember(Dictionary<string, string> memberInfo)
        {
            foreach (Member member in _memberList)
            {
                if (member.IsExist(memberInfo["id"]))
                {
                    return false;
                }
            }
            _memberList.Add(new Member(memberInfo));
            return true;
        }

        public List<string> LoadAllFriendInvitation(string memberId)
        {
            foreach (Member member in _memberList)
            {
                if (member.IsExist(memberId))
                {
                    return member.LoadAllFriendInvitation();
                }
            }
            return null;
        }

        public void AgreeInvitation(string memberId, string inviterId)
        {
            foreach (Member member in _memberList)
            {
                if (member.IsExist(memberId) && member.IsExist(inviterId))
                {
                    member.InsertFriendList(inviterId);
                    member.InsertFriendList(memberId);
                    member.DeleteFriendInvitation(inviterId);
                }
            }
        }

        public void RejectInvitation(string memberId, string inviterId)
        {
            foreach (Member member in _memberList)
            {
                if (member.IsExist(memberId))
                {
                    member.DeleteFriendInvitation(inviterId);
                }
            }
        }
        /*Hsu end*/
    }
}