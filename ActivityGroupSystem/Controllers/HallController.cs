using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using ActivityGroupSystem.Models;

namespace ActivityGroupSystem.Controllers
{
    public class HallController : Controller
    {
        private ActivityHandler _activityHandler;
        private MemberHandler _memberHandler;
        public HallController()
        {
            _activityHandler = new ActivityHandler();
            _memberHandler = new MemberHandler();
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Your application description page.";

            List<string> a = new List<string>();
            List<string> b = null;
            a.Add("1");
            a.Add("2");
            a.Add("3");
            a.Add("4");
            b = a.ToList();
            b.Remove("4");

            for (int i = 0; i < a.Count; i++)
            {
                if (a[i] == b[i])
                {
                    a.RemoveAt(i);
                }
            }

            //Simulate test user data and login timestamp
            var userId = "12345";
            var currentLoginTime = DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss");

            //Save non identifying data to Firebase
            var currentUserLogin = new LoginData() { TimestampUtc = currentLoginTime };
            var firebaseClient = new FirebaseClient("https://activitygroup-74f7f.firebaseio.com/");
            var result = await firebaseClient
              .Child("Users/" + userId + "/Logins")
              .PostAsync(currentUserLogin);

            //Retrieve data from Firebase
            var dbLogins = await firebaseClient
              .Child("Users")
              .Child(userId)
              .Child("Logins")
              .OnceAsync<LoginData>();

            var timestampList = new List<DateTime>();

            //Convert JSON data to original datatype
            foreach (var login in dbLogins)
            {
                timestampList.Add(Convert.ToDateTime(login.Object.TimestampUtc).ToLocalTime());
            }

            //Pass data to the view
            ViewBag.CurrentUser = userId;
            //ViewBag.Test = timestampList.OrderByDescending(x => x);
            ViewBag.Test = timestampList;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /*Willie Start*/
        public void EnterJoinedActivity(string memberId, string activityId)
        {
            Activity activity = _activityHandler.enterJoinedActivity(memberId, activityId);
            // return activity;

        }

        public void EnterChatroom(string memberId, string activityId)
        {
            List<string> blackList = _memberHandler.GetBlackList(memberId);
            if (blackList != null)
            {
                List<Message> chatroomMessage = _activityHandler.EnterChatroom(activityId, blackList);
            }
            //return chatroomMessage;
        }

        public void sendMessage(string memberId, string activityId, string message)
        {
            _activityHandler.SendMessage(memberId, activityId, message);
        }
        /*Willie End*/
    }
}