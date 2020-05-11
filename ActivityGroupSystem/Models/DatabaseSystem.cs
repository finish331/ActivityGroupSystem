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
        public async Task<bool> InsertActivity(Activity activityInfo)
        {
            await _firebaseClient.Child("Activity").Child(activityInfo.ActivityId).PatchAsync<Activity>(activityInfo);
            //string participantList = "";
            //if (activityInfo.ParticipantList.Count > 0)
            //{
            //    foreach (string temp in activityInfo.ParticipantList)
            //    {
            //        participantList += temp + ",";
            //    }
            //}
            //char[] charsToTrim = {','};
            //participantList = participantList.TrimEnd(charsToTrim);
            //Dictionary<string, string> tempDictionary = new Dictionary<string, string>()
            //{
            //    { "ParticipantList", participantList }
            //};
            //await _firebaseClient.Child("Activity").Child(activityInfo.ActivityId).PatchAsync(tempDictionary);
            //await _firebaseClient.Child("Activity").Child(activityInfo.ActivityId).PatchAsync("Chatroom");
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

        public async Task<List<Message>> GetChatData(string activityId)
        {
            var messageData = await _firebaseClient.Child("Activity").Child(activityId).Child("Chatroom").OnceAsync<Message>();
            List<Message> messages = new List<Message>();

            foreach (var tempData in messageData)
            {
                messages.Add(tempData.Object);
            }

            return messages;
        }

        public async Task<bool> SendMessage(string activityId, Message message)
        {
            await _firebaseClient.Child("Activity").Child(activityId).Child("Chatroom").Child(message.Time).PatchAsync<Message>(message);
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

        public async Task<bool> UpdateMemberInfo(string memberId, Dictionary<string, string> newData)
        {
            await _firebaseClient.Child("Member").Child(memberId).PatchAsync(newData);
            return true;
        }

        public async Task<bool> UpdateMemberInfoList(string memberId, string listName, List<string> newData)
        {
            await _firebaseClient.Child("Users").Child("test").Child(listName).PatchAsync(newData);
            return true;
        }

        public async Task<bool> UpdateMember(Member member)
        {
            await _firebaseClient.Child("Member").Child(member.MemberId).PatchAsync(member);
            return true;
        }
        /*Hsu end*/
    }
}