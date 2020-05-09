using Firebase.Database;
using Firebase.Database.Query;
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
        public async Task<List<Dictionary<string, string>>> GetInitializationData(string dataType)
        {
            _firebaseClient = new FirebaseClient("https://activitygroup-74f7f.firebaseio.com/");
            var initializationListData = await _firebaseClient.Child(dataType).OnceAsync<Dictionary<string, string>>();
            if (initializationListData.Count != 0)
            {
                List<Dictionary<string, string>> memberList = new List<Dictionary<string, string>>();
                foreach (var member in initializationListData)
                {
                    memberList.Add(member.Object);
                }
                return memberList;
            }
            return null;
        }

        public async Task<bool> CheckAccount(string id, string passwd)
        {
            _firebaseClient = new FirebaseClient("https://activitygroup-74f7f.firebaseio.com/");
            var memberPassword = await _firebaseClient.Child("Member").Child(id).Child("Password").OnceSingleAsync<string>();
            if (memberPassword != null)
            {
                if(memberPassword == passwd)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> InsertMember(Dictionary<string, string> newData)
        {
            _firebaseClient = new FirebaseClient("https://activitygroup-74f7f.firebaseio.com/");
            await _firebaseClient.Child("Member").Child(newData["MemberId"]).PatchAsync(newData);
            return true;
        }

        public async Task<bool> InsertList(string searchId, string targetId, string searchType, string targetType)
        {
            _firebaseClient = new FirebaseClient("https://activitygroup-74f7f.firebaseio.com/");
            var listData = await _firebaseClient.Child(searchType).Child(searchId).Child(targetType).OnceSingleAsync<string>();

            listData += "," + targetId;
            await _firebaseClient.Child(searchType).Child(searchId).Child(targetType).PatchAsync(listData);
            return true;
        }


        /*Hsu end*/
    }
}