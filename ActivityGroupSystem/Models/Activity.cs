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
        private string _numberOfPeople;
        private string _activityNote;
        private string _activityDate;
        private Chatroom _chatroom;

        /*Willie Start*/      

        public Activity()
        {
            _participantList = new List<string>();
            _chatroom = new Chatroom();
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

        public int KickOutPariticipant(string memberId)
        {
            foreach (string participantId in _participantList)
            {
                if (participantId == memberId)
                {
                    _participantList.Remove(participantId);
                    return 0;
                }
            }
            return 1;
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

        public string NumberOfPeople
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
            set
            {
                _participantList = value;
            }
        }
        /*Willie End*/

        /* Ting Start */
        public Activity(Activity activityInfo)
        {
            _participantList = new List<string>();
            _activityId = activityInfo.ActivityId;
            _activityName = activityInfo.ActivityName;
            _homeownerId = activityInfo.HomeOwnerId;
            _activityDate = activityInfo.ActivityDate;
            _activityNote = activityInfo.ActivityNote;
            _numberOfPeople = activityInfo.NumberOfPeople;
            _chatroom = new Chatroom();
        }

        public void TransferHomeowner(string newOwnerId)
        {
            _homeownerId = newOwnerId;
        }

        public void Leave(string memberId)
        {
            _participantList.Remove(memberId);
        }

        public bool IsParticipant(string memberId)
        {
            return _participantList.Contains(memberId);
        }

        public void SendMessage(string memberId, string memberName, string messageContent)
        {
            _chatroom.SendMessage(memberId, memberName, messageContent);
        }

        public List<Message> GetChatData()
        {
            return _chatroom.MessageList;
        }

        public void InitializeChatroom(List<Message> messages)
        {
            _chatroom.Initialize(messages);
        }
        /* Ting End */

        /*Hsu start*/

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
                _activityName = newData["name"];
                _homeownerId = newData["ownerId"];
                _numberOfPeople = newData["people"];
                _activityNote = newData["note"];
                _activityDate = newData["date"];
                
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