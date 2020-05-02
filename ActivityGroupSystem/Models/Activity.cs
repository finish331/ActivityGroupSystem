using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActivityGroupSystem.Models
{
    public class Activity
    {
        private List<string> _participantList = new List<string>();
        private string _activityId;
        private string _activityName;
        private string _homeownerId;
        private string _chatroom;

        /*Willie Start*/
        
        public bool IsMemberInActivity(string memberId)
        {
            for (int i = 0; i < _participantList.Count; i++)
            {
                if (_participantList[i] == memberId)
                {
                    return true;
                }
            }
            return false;
        }


        public string ActivityId
        {
            get
            {
                return _activityId;
            }
        }

        public string ActivityName
        {
            get
            {
                return _activityName;
            }
        }

        public List<string> ParticipantList
        {
            get
            {
                return _participantList;
            }
        }
        /*Willie End*/
    }
}