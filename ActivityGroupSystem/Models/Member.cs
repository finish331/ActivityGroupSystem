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
        private string _memberPassword;
        private string _memberSexuality;
        private string _memberBirthday;
        private string _memberPhone;
        private List<string> _friendsList;
        private List<string> _blackList;
        private List<string> _friendInvitation;
        private Dictionary<string, string> _invitedList; // { 活動ID, 所有邀請人姓名(間隔用',') }

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

        public bool AddFriend(string memberId)
        {
            foreach (string friendId in _friendsList)
            {
                if (friendId == memberId)
                {
                    return false;
                }
            }
            _friendsList.Add(memberId);
            return true;
        }

        public string MemberId
        {
            get
            {
                return _memberId;
            }
            set
            {
                _memberId = value;
            }
        }

        public string MemberName
        {
            get
            {
                return _memberName;
            }
            set
            {
                _memberName = value;
            }
        }

        public string Password
        {
            get
            {
                return _memberPassword;
            }
            set
            {
                _memberPassword = value;
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

        /* Ting Start */
        public Dictionary<string, string> GetData()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("MemberId", _memberId);
            data.Add("MemberName", _memberName);
            data.Add("Password", _memberPassword);
            data.Add("Sexuality", _memberSexuality);
            data.Add("Birthday", _memberBirthday);
            data.Add("Phone", _memberPhone);
            
            /*_friendsList = new List<string>();
            _blackList = new List<string>();
            _invitedList = new Dictionary<string, string>();
            _friendInvitation = new List<string>();*/
            return data;
        }

        public void UpdataData(Dictionary<string, string> newData)
        {
            _memberId = newData["id"];
            _memberName = newData["name"];
        }

        public List<string> FriendsList
        {
            get
            {
                return _friendsList;
            }
        }

        public void Invited(string inviterName, string activityId)
        {
            // 判斷是否已經被邀請到此活動
            if (_invitedList.ContainsKey(activityId))
            {
                string[] inviters = _invitedList[activityId].Split(',');

                // 判斷邀請人是否重複
                if (!inviters.Contains(inviterName))
                    _invitedList[activityId] = _invitedList[activityId] + ',' + inviterName;
            }
            else
            {
                _invitedList.Add(activityId, inviterName);
            }
        }

        public bool DeleteFriend(string targetId)
        {
            if (_friendsList.Contains(targetId))
            {
                _friendsList.Remove(targetId);
                return true;
            }
            else
            {
                return false;
            }
        }
        /* Ting End */

        /*Hsu start*/
        public Member(Dictionary<string, string> memberInfo)
        {
            _memberId = memberInfo["MemberId"];
            _memberName = memberInfo["MemberName"];
            _memberPassword = memberInfo["Password"];
            _memberSexuality = memberInfo["Sexuality"];
            _memberBirthday = memberInfo["Birthday"];
            _memberPhone = memberInfo["Phone"];
            _friendsList = new List<string>();
            _blackList = new List<string>();
            _invitedList = new Dictionary<string, string>();
            _friendInvitation = new List<string>();
            /*foreach (KeyValuePair<string, string> pair in _memberData)
            {
                _memberData.Add(pair.Key, pair.Value);
            }*/
        }

        public bool IsExist(string memberId)
        {
            return _memberId == memberId;
        }

        public List<string> LoadAllFriendInvitation()
        {
            return _friendInvitation;
        }

        public void InsertFriendList(string memberId)
        {
            _friendsList.Add(memberId);
        }

        public void DeleteFriendInvitation(string memberId)
        {
            _friendInvitation.Remove(memberId);
        }
        /*Hsu end*/
    }
}