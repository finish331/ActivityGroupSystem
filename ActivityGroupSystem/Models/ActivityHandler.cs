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
            _activityCount = _activityList.Count;
        }

        public ActivityHandler(List<Activity> activity)
        {
            if(activity != null)
            {
                _activityList = activity;
                _activityCount = _activityList.Count;
            }
            else
            {
                _activityList = new List<Activity>();
                _activityCount = _activityList.Count;
            }
        }

        public List<Activity> ActivityList
        {
            get
            {
                return _activityList;
            }
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

        public Activity EnterJoinedActivity(string memberId, string activityId)
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

        public List<Activity> GetManageActivity(string memberId)
        {
            List<Activity> result = new List<Activity>();
            foreach (Activity activity in _activityList)
            {
                if (activity.HomeOwnerId == memberId)
                {
                    result.Add(activity);
                }
            }
            return result;
        }

        public List<Activity> GetUnJoinActivity(string memberId)
        {
            List<Activity> result = new List<Activity>();
            foreach (Activity activity in _activityList)
            {
                if (!activity.IsMemberInActivity(memberId))
                {
                    result.Add(activity);
                }
            }
            return result;
        }

        public List<Activity> GetJoinActivity(string memberId)
        {
            List<Activity> result = new List<Activity>();
            foreach (Activity activity in _activityList)
            {
                if (activity.IsMemberInActivity(memberId))
                {
                    result.Add(activity);
                }
            }
            return result;
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

        public int KickOutPariticipant(string memberId, string activityId)
        {
            Activity activity = FindActivity(activityId);
            if(activity != null)
            {
                return activity.KickOutPariticipant(memberId);
            }
            return 2;
        }
        /*Willie End*/

        /* Ting Start */
        public void CreateActivity(Activity activityInfo)
        {
            activityInfo.ActivityId = (_activityList.Count + 1).ToString();
            Activity newActivity = new Activity(activityInfo);
            _activityList.Add(newActivity);
            _activityCount++;
        }

        public List<string> GetAllParticipants(string activityId)
        {
            Activity activity = FindActivity(activityId);
            if (activity != null)
            {
                return activity.ParticipantList;
            }
            else
            {
                return null;
            }
        }

        public bool TransferHomeowner(string activityId, string newOwnerId)
        {
            Activity activity = FindActivity(activityId);
            if (activity != null)
            {
                activity.TransferHomeowner(newOwnerId);
                return true;
            }
            else
                return false;
        }

        public bool IsParticipant(string activityId, string memberId)
        {
            Activity activity = FindActivity(activityId);
            if (activity != null)
                return activity.IsParticipant(memberId);
            else
                return false;
        }

        public void SendMessage(string activityId, string memberId, string memberName, string messageContent)
        {
            Activity activity = FindActivity(activityId);
            if (activity != null)
                activity.SendMessage(memberId, memberName, messageContent);
        }

        public List<Message> GetChatData(string activityId)
        {
            Activity activity = FindActivity(activityId);
            if (activity != null)
                return activity.GetChatData();
            else
                return null;
        }

        public void InitializeChatroom(string activityId, List<Message> messages)
        {
            Activity activity = FindActivity(activityId);
            if (activity != null)
                activity.InitializeChatroom(messages);
        }
        /* Ting End */

        /*Hsu start*/
        public bool JoinActivity(string memberId, string activityId)
        {
            foreach (Activity activity in _activityList)
            {
                if (activity.IsExist(activityId))
                {
                    return activity.AddNewParticipant(memberId);
                }
            }
            return false;
        }

        public bool SaveActivity(string activityId, Dictionary<string, string> newData)
        {
            foreach (Activity activity in _activityList)
            {
                if (activity.IsExist(activityId))
                {
                    return activity.UpdateActivity(newData);
                }
            }
            return false;
        }
        /*Hsu end*/
    }
}