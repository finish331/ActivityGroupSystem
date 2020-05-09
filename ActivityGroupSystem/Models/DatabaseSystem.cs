using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Firebase.Database;
using Firebase.Database.Query;

namespace ActivityGroupSystem.Models
{
    public class DatabaseSystem
    {
        FirebaseClient _firebaseClient;
        /*Willie Start*/
        public DatabaseSystem()
        {
            _firebaseClient = new FirebaseClient("https://activitygroup-74f7f.firebaseio.com/");
        }
        public async Task<List<Member>> InitializationMemberData()
        {
            var memberData = await  _firebaseClient.Child("Member").OnceAsync<Member>();
            List<Member> memberList = new List<Member>();

            foreach (var tempData in memberData)
            {
                memberList.Add(tempData.Object);
            }

            return memberList;
        }

        public async Task<List<Activity>> InitializationActivityData()
        {
            var activityData = await _firebaseClient.Child("Activity").OnceAsync<Activity>();
            List<Activity> activityList = new List<Activity>();

            foreach (var tempData in activityData)
            {
                activityList.Add(tempData.Object);
            }

            return activityList;
        }

        /*Willie End*/
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