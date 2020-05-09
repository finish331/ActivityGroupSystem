using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ActivityGroupSystem.Models
{
    public class DatabaseSystem
    {
        private FirebaseClient firebaseClient = new FirebaseClient("https://activitygroup-74f7f.firebaseio.com/");

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
            var initializationListData = await firebaseClient.Child(dataType).OnceAsync<Dictionary<string, string>>();
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
            var memberPassword = await firebaseClient.Child("Member").Child(id).Child("Password").OnceSingleAsync<string>();
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
            await firebaseClient.Child("Member").Child(newData["MemberId"]).PatchAsync(newData);
            return true;
        }

        public async Task<bool> InsertList(string searchId, string targetId, string searchType, string targetType)
        {
            var listData = await firebaseClient.Child(searchType).Child(searchId).Child(targetType).OnceSingleAsync<string>();

            listData += "," + targetId;
            await firebaseClient.Child(searchType).Child(searchId).Child(targetType).PatchAsync(listData);
            return true;
        }


        /*Hsu end*/
    }
}