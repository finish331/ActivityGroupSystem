using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActivityGroupSystem.Models
{
    public class ActivityHandler
    {
        private List<Activity> _activityList = new List<Activity>();
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
        /*Willie End*/
    }
}