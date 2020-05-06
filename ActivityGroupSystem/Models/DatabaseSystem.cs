using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActivityGroupSystem.Models
{
    public class DatabaseSystem
    {
        /* Ting Start */
        public bool InsertActivity(Dictionary<string, string> activityInfo)
        {
            return true;
        }

        public bool UpdateMember(string memberId, Dictionary<string, string> newData)
        {
            return true;
        }

        public bool UpdateActivity(string activityId, Dictionary<string, string> newData)
        {
            return true;
        }
        /* Ting End */

        /*Hsu start*/
        public bool CheckAccount(string id, string passwd)
        {

            return true;
        }

        public bool InsertMember(Dictionary<string, string> newData)
        {

            return true;
        }
        /*Hsu end*/
    }
}