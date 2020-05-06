﻿using System;
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
        private Chatroom _chatroom;

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
        /* Ting End */
    }
}