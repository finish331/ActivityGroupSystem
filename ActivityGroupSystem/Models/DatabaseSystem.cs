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
            return true;
        }

        public async Task<bool> UpdateActivity(string activityId, Dictionary<string, string> newData)
        {
            await _firebaseClient.Child("Activity").Child(activityId).PatchAsync(newData);         
            return true;
        }

        public async Task<bool> UpdateParticipantList(string activityId, List<string> newList)
        {
            await _firebaseClient.Child("Activity").Child(activityId).Child("ParticipantList").PutAsync(newList);
            return true;
        }

        public async Task<bool> UpdateInvitedList(string memberId, Dictionary<string, string> newList)
        {
            await _firebaseClient.Child("Member").Child(memberId).Child("InvitedList").PutAsync(newList);
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
        public async Task<bool> UpdateMemberInfo(Member member)
        {
            await _firebaseClient.Child("Member").Child(member.MemberId).PatchAsync(member);
            return true;
        }

        public async Task<bool> Delete(string activityId, string dataType)
        {
            await _firebaseClient.Child(dataType).Child(activityId).DeleteAsync();
            return true;
        }
        /*Hsu end*/
    }
}