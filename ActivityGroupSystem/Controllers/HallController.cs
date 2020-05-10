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
        private DatabaseSystem _databaseSystem;

        public HallController()
        {
            _databaseSystem = new DatabaseSystem();
        }

        public async Task InitializationModel()
        {
            List<Activity> activityData = await _databaseSystem.InitializationActivityData();
            _activityHandler = new ActivityHandler(activityData);
            List<Member> memberData = await _databaseSystem.InitializationMemberData();
            _memberHandler = new MemberHandler(memberData);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost()]
        public async Task<JsonResult> GetAllActivity()
        {
            await InitializationModel();
            return Json(_activityHandler.ActivityList);
        }

        [HttpPost()]
        public async Task<JsonResult> GetAllActivity2()
        {
            List<Activity> allActivity = new List<Activity>();
            Activity activity = new Activity();
            activity.ActivityId = "3";
            activity.ActivityName = "3";
            activity.HomeOwnerId = "3";
            allActivity.Add(activity);
            return Json(allActivity);
        }

        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Your application description page.";

            //List<string> test = _memberHandler.GetBlackList("1");

            List<string> a = new List<string>();
            List<string> b = null;
            string abc = "testWord";
            if (abc.Contains("tW"))
            {
                a.Add("1");
            }
            if (b != null)
            {
                a.Add("1");
            }

            if (b == null)
            {
                a.Add("1");
                a.Add("2");
                a.Add("3");
                a.Add("4");
                b = a.ToList();
                b.Remove("4");
            }


            if (b != null)
            {
                for (int i = 0; i < a.Count; i++)
                {
                    if (a[i] == b[i])
                    {
                        a.RemoveAt(i);
                    }
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

        public void SendMessage(string memberId, string activityId, string message)
        {
            _activityHandler.SendMessage(memberId, activityId, message);
        }

        public void SearchMemberInfo(string keyWord)
        {
            List<Member> searchResult = _memberHandler.SearchMemberInfo(keyWord);
        }

        public void BlackMember(string memberId, string blackMemberId)
        {
            if (_memberHandler.BlackMember(memberId, blackMemberId))
            {
                //True才新增資料庫
            }
        }

        public List<Activity> InputAcivityKeyWord(string keyWord)
        {
            return _activityHandler.InputAcivityKeyWord(keyWord);
        }

        public bool KickOutPariticipant(string memberId, string activityId)
        {
            return _activityHandler.KickOutPariticipant(memberId, activityId);
        }

        public bool AddFriend (string myMemberId, string friendId)
        {
            bool result = _memberHandler.AddFriend(myMemberId, friendId);
            if (result)
            {
                //新增至資料庫
                return true;
            }
            else
            {
                //不新增
                return false;
            }
        }
        /*Willie End*/

        /* Ting Start */
        public async Task<JsonResult> CreateActivity(Activity activityInfo)
        {
            await InitializationModel();
            _activityHandler.CreateActivity(activityInfo);
            await _databaseSystem.InsertActivity(activityInfo);
            return Json(true);
        }

        public Member GetMember(string memberId)
        {
            return _memberHandler.GetMemberById(memberId);
        }

        public bool UpdateUserData(string memberId, Dictionary<string, string> newData)
        {
            return _memberHandler.UpdateUserData(memberId, newData) && _databaseSystem.UpdateMember(memberId, newData);
        }

        public List<string> GetAllParticipants(string activityId)
        {
            return _activityHandler.GetAllParticipants(activityId);
        }

        public bool transferHomeowner(string activityId, string newOwnerId)
        {
            return _activityHandler.transferHomeowner(activityId, newOwnerId);
        }

        public List<string> GetFriendsList(string memberId)
        {
            return _memberHandler.GetFriendsList(memberId);
        }

        public bool InviteFriend(string userName, string friendId, string activityId)
        {
            return _memberHandler.InviteMember(userName, friendId, activityId);
        }

        public bool DeleteFriend(string memberId, string targetId)
        {
            return _memberHandler.DeleteFriend(memberId, targetId);
        }

        public ActionResult Room(string activityId, string userId)
        {
            Activity activity = _activityHandler.FindActivity(activityId);
            Member member = _memberHandler.GetMemberById(userId);
            List<Member> participantsList = new List<Member>();
            List<Member> friendsList = new List<Member>();
            foreach (string memberId in activity.ParticipantList)
            {
                Member participant = _memberHandler.GetMemberById(memberId);
                participantsList.Add(member);
            }
            foreach (string memberId in member.FriendsList)
            {
                Member friend = _memberHandler.GetMemberById(memberId);
                friendsList.Add(friend);
            }

            ViewData["activity_name"] = activity.ActivityName;
            ViewData["ownerId"] = activity.HomeOwnerId;
            ViewData["activity_date"] = "2020/5/10";
            ViewData["participants_count"] = activity.ParticipantList.Count;
            ViewBag.participants = participantsList;
            ViewBag.friends = friendsList;
            return View();
        }
        /* Ting End */

        /*Hsu start*/
        public void CreateNewMember(Dictionary<string, string> memberInfo)
        {
            if (_memberHandler.CreateNewMember(memberInfo))
            {
                _databaseSystem.InsertMember(memberInfo);
            }
        }

        public void LoadAllActivity()
        {
            List<string> activityList = _activityHandler.LoadAllActivity();
        }

        public void JoinActivity(string memberId, string activityId)
        {
            if (_activityHandler.JoinActivity(memberId, activityId))
            {
                _databaseSystem.InsertList(activityId, memberId, "Activity", "ParticipantList");
                //存進firebase
            }
        }

        public void SaveActivity(string activityId, Dictionary<string, string> newData)
        {
            if (_activityHandler.SaveActivity(activityId, newData))
            {
                _databaseSystem.UpdateActivity(activityId, newData);
                //存進firebase
            }
        }

        public void LoadAllFriendInvitation(string memberId)
        {
            List<string> friendInvitation = _memberHandler.LoadAllFriendInvitation(memberId);

        }

        public void ReplyInvitation(string memberId, string inviterId)
        {
            //同意
            _memberHandler.AgreeInvitation(memberId, inviterId);
            _databaseSystem.InsertList(memberId, inviterId, "Member", "FriendList");
            _databaseSystem.InsertList(inviterId, memberId, "Member", "FriendList");
            //拒絕
            _memberHandler.RejectInvitation(memberId, inviterId);
        }

        public ActionResult Logout()
        {
            if (Request.Cookies["MemberId"] != null)
            {
                Response.Cookies["MemberId"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["MemberName"].Expires = DateTime.Now.AddDays(-1);
                Response.Redirect("Index");
            }
            return new EmptyResult();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(FormCollection post)
        {
            await InitializationModel();
            string account = post["account"];
            string password = post["password"];

            //驗證帳號密碼
            if (await _databaseSystem.CheckAccount(account, password))
            {
                Member member = _memberHandler.GetMemberById(account);
                Response.Cookies["MemberName"].Value = member.MemberName;
                Response.Cookies["MemberId"].Value = member.MemberId;
                Response.Redirect("Index");
                return new EmptyResult();
            }
            else
            {
                ViewBag.Msg = "登入失敗...";
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(FormCollection post)
        {
            
            if (string.IsNullOrWhiteSpace(post["password"]) || post["password"] != post["password2"])
            {
                ViewBag.Msg = "密碼輸入錯誤";
                return View();
            }
            else
            {
                Dictionary<string, string> memberInfo = new Dictionary<string, string>();
                foreach (var key in post)
                {
                    if (key.ToString() == "password2" || key.ToString() == "Sexuality_input") continue;
                    memberInfo.Add(key.ToString(), post[key.ToString()]);
                }
                if (_memberHandler.CreateNewMember(memberInfo))
                {
                    _databaseSystem.InsertMember(memberInfo);
                    return View("Index");
                }
                else
                {
                    ViewBag.Msg = "帳號已使用...";
                    return View();
                }
            }
        }

        public ActionResult MemberInfo()
        {
            Dictionary<string, string> memberInfo = new Dictionary<string, string>();
            return View();
        }

        public async Task<ActionResult> test()
        {
            var firebaseClient = new FirebaseClient("https://activitygroup-74f7f.firebaseio.com/");
            var memberListData = await firebaseClient.Child("Member").Child("member1").Child("MemberName").OnceSingleAsync<string>();

            
            ViewBag.test = memberListData;
            
            return View("index");
        }
    }
}