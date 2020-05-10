using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActivityGroupSystem.Models
{
    public class Activity
    {
        private List<string> _participantList;
        private string _activityId;
        private string _activityName;
        private string _homeownerId;
        private int _numberOfPeople;
        private string _activityNote;
        private string _activityDate;
        private Chatroom _chatroom;

        /*Willie Start*/      

        public Activity()
        {

        }

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

        public List<Message> GetChatroomMessage(List<string> blackList)
        {
            List<Message> messageResult = _chatroom.MessageList.ToList();
            if (blackList.Count > 0)
            {
                for (int i = messageResult.Count - 1; i > 0; i--)
                {
                    foreach (string blackedMemberId in blackList)
                    {
                        if (messageResult[i].MemberId == blackedMemberId)
                        {
                            messageResult.RemoveAt(i);
                        }
                    }
                }
            }
            return messageResult;
        }

        public void SendMessage(string memberId, string messageContent)
        {
            _chatroom.SendMessage(memberId, messageContent);
        }

        public bool KickOutPariticipant(string memberId)
        {
            foreach (string participantId in _participantList)
            {
                if (participantId == memberId)
                {
                    _participantList.Remove(participantId);
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
            set
            {
                _activityId = value;
            }
        }

        public string ActivityName
        {
            get
            {
                return _activityName;
            }
            set
            {
                _activityName = value;
            }
        }

        public string ActivityDate
        {
            get
            {
                return _activityDate;
            }
            set
            {
                _activityDate = value;
            }
        }

        public string ActivityNote
        {
            get
            {
                return _activityNote;
            }
            set
            {
                _activityNote = value;
            }
        }

        public int NumberOfPeople
        {
            get
            {
                return _numberOfPeople;
            }
            set
            {
                _numberOfPeople = value;
            }
        }

        public string HomeOwnerId
        {
            get
            {
                return _homeownerId;
            }
            set
            {
                _homeownerId = value;
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

        /* Ting Start */
        public Activity(Dictionary<string, string> activityInfo)
        {
            _participantList = new List<string>();
            _activityId = activityInfo["id"];
            _activityName = activityInfo["name"];
            _homeownerId = activityInfo["ownerId"];
            _chatroom = new Chatroom();
        }

        public void transferHomeowner(string newOwnerId)
        {
            _homeownerId = newOwnerId;
        }

        public bool Leave(string memberId)
        {
            if (_participantList.Contains(memberId))
            {
                _participantList.Remove(memberId);
                return true;
            }
            else
                return false;
        }
        /* Ting End */

        /*Hsu start*/
        /*public string ActivityName
        {
            get
            {
                return _activityData["name"];
            }
        }*/

        public bool AddNewParticipant(string memberId)
        {
            foreach (string participant in _participantList)
            {
                if (participant == memberId)
                {
                    return false;
                }
            }
            _participantList.Add(memberId);
            return true;
        }

        public bool IsExist(string activityId)
        {
            return _activityId == activityId;
        }

        public bool UpdateActivity(Dictionary<string, string> newData)
        {
            try
            {
                _activityId = newData["id"];
                _activityName = newData["name"];
                _homeownerId = newData["ownerId"];

                
                return true;
            }
            catch
            {
                return false;
            }
        }
        /*Hsu end*/
    }
}