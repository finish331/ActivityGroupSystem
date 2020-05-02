using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActivityGroupSystem.Models
{
    public class AvtivityHandler
    {
        private List<Activity> _activityList = new List<Activity>();
        /*Willie Start*/
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
        /*Willie End*/
    }
}