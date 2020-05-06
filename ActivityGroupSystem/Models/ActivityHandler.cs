using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActivityGroupSystem.Models
{
    public class ActivityHandler
    {
        private List<Activity> _activityList;
        private int _activityCount;

        public ActivityHandler()
        {
            _activityList = new List<Activity>();
            _activityCount = 0;
        }

        /*Willie Start*/
        public Activity FindActivity(string activityId)
        {
            foreach (Activity activity in _activityList)
            {
                if (activity.ActivityId == activityId)
                {
                    return activity;
                }
            }
            return null;
        }

        public Activity enterJoinedActivity(string memberId, string activityId)
        {
            Activity activityResult = null;
            foreach (Activity tempActivity in _activityList)
            {
                if (tempActivity.ActivityId == activityId && tempActivity.IsMemberInActivity(memberId))
                {
                    activityResult = tempActivity;
                }
            }
            return activityResult;
        }

        public List<Message> EnterChatroom(string activityId, List<string> blackList)
        {
            List<Message> messageResult = null;
            Activity activity = FindActivity(activityId);
            messageResult = activity.GetChatroomMessage(blackList);
            return messageResult;
        }

        public void SendMessage(string activityId, string memberId, string messageContent)
        {
            Activity activity = FindActivity(activityId);
            activity.SendMessage(memberId, messageContent);
        }

        public List<Activity> InputAcivityKeyWord(string keyWord)
        {
            List<Activity> relatedActivity = new List<Activity>();
            foreach (Activity activity in _activityList)
            {
                if (activity.ActivityId.Contains(keyWord) || activity.ActivityName.Contains(keyWord))
                {
                    relatedActivity.Add(activity);
                }
            }
            return relatedActivity;
        }

        public bool KickOutPariticipant(string memberId, string activityId)
        {
            Activity activity = FindActivity(activityId);
            if(activity != null)
            {
                return activity.KickOutPariticipant(memberId);
            }
            return false;
        }
        /*Willie End*/

        /* Ting Start */
        public void CreateActivity(Dictionary<string, string> activityInfo)
        {
            activityInfo.Add("id", (_activityCount + 1).ToString());
            Activity newActivity = new Activity(activityInfo);
            _activityList.Add(newActivity);
            _activityCount++;
        }

        public List<string> GetAllParticipants(string activityId)
        {
            Activity activity = GetActivityById(activityId);
            if (activity != null)
            {
                return activity.ParticipantList;
            }
            else
                return null;
        }

        public bool transferHomeowner(string activityId, string newOwnerId)
        {
            Activity activity = GetActivityById(activityId);
            if (activity != null)
            {
                activity.transferHomeowner(newOwnerId);
                return true;
            }
            else
                return false;
        }

        private Activity GetActivityById(string id)
        {
            if (_activityList.Exists(activity => activity.ActivityId == id))
                return _activityList.Find(activity => activity.ActivityId == id);
            else
                return null;
        }
        /* Ting End */
    }
}