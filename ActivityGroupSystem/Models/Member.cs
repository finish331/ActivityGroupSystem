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
        private List<string> _friendsList;
        private List<string> _blackList;
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

        /* Ting Start */
        public Member()
        {
            _friendsList = new List<string>();
            _blackList = new List<string>();
            _invitedList = new Dictionary<string, string>();
        }

        public Dictionary<string, string> GetData()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("id", _memberId);
            data.Add("name", _memberName);

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
    }
}