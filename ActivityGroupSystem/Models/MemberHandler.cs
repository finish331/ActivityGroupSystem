using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActivityGroupSystem.Models
{
    public class MemberHandler
    {
        private List<Member> _memberList;

        /*Willie Start*/

        public MemberHandler(List<Member> memberList)
        {
            if (memberList != null)
            {
                _memberList = memberList;
            }
            else
            {
                _memberList = new List<Member>();
            }
        }

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
            List<Member> relatedMember = new List<Member>();
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
            if (memberId != blackMemberId)
            {
                RejectInvitation(blackMemberId, memberId);
                foreach (Member member in _memberList)
                {
                    if (member.MemberId == memberId)
                    {
                        return member.BlackMember(blackMemberId);
                    }
                }
            }
            return false;
        }

        public bool AddFriendInvitation(string myMemberId, string friendId)
        {
            if(myMemberId != friendId)
            {
                DeleteBlack(myMemberId, friendId);
                foreach (Member member in _memberList)
                {
                    if (member.MemberId == friendId)
                    {
                        return member.AddFriendInvitation(myMemberId);
                    }
                }
            }
            return false;
        }
        /*Willie End*/

        /* Ting Start */
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
                return member.FriendList;
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
            Member target = GetMemberById(targetId);

            if (member != null && target != null)
            {
                return member.DeleteFriend(targetId) && target.DeleteFriend(memberId);
            }
            else
                return false;
        }

        public Member GetMemberById(string id)
        {
            if (_memberList.Exists(member => member.MemberId == id))
                return _memberList.Find(member => member.MemberId == id);
            else
                return null;
        }

        public bool IsBlack(string memberId, string targetId)
        {
            Member member = GetMemberById(memberId);
            return member.IsBlack(targetId);
        }
        /* Ting End */

        /*Hsu start*/
        public bool CreateNewMember(Dictionary<string, string> memberInfo)
        {
            foreach (Member member in _memberList)
            {
                if (member.IsExist(memberInfo["MemberId"]))
                {
                    return false;
                }
            }
            _memberList.Add(new Member(memberInfo));
            return true;
        }

        public bool AgreeInvitation(string memberId, string inviterId)
        {
            foreach (Member member in _memberList)
            {
                if (member.IsExist(memberId))
                {
                    member.InsertFriendList(inviterId);
                    member.DeleteFriendInvitation(inviterId);
                }
                if (member.IsExist(inviterId))
                {
                    member.InsertFriendList(memberId);
                }
            }
            return true;
        }

        public bool RejectInvitation(string memberId, string inviterId)
        {
            foreach (Member member in _memberList)
            {
                if (member.IsExist(memberId))
                {
                    member.DeleteFriendInvitation(inviterId);
                    return true;
                }
            }
            return false;
        }

        public List<string> GetInvitationList(string memberId)
        {
            Member member = GetMemberById(memberId);
            if (member != null)
            {
                return member.FriendInvitation;
            }
            else
                return null;
        }

        public bool DeleteBlack(string memberId, string targetId)
        {
            Member member = GetMemberById(memberId);
            if (member != null)
            {
                member.DeleteBlack(targetId);
                return true;
            }
            else
                return false;
        }
        /*Hsu end*/
    }
}