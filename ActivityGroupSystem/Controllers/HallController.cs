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
        public async Task<JsonResult> GetUnJoinActivity(string memberId)
        {
            await InitializationModel();
            List<Activity> unJoinActivity = _activityHandler.GetUnJoinActivity(memberId);
            return Json(unJoinActivity);
        }

        [HttpPost()]
        public async Task<JsonResult> GetManageActivity(string memberId)
        {
            await InitializationModel();
            List<Activity> allManageActivity = _activityHandler.GetManageActivity(memberId);
            return Json(allManageActivity);
        }

        [HttpPost()]
        public async Task<JsonResult> GetJoinActivity(string memberId)
        {
            await InitializationModel();
            List<Activity> allJoinActivity = _activityHandler.GetJoinActivity(memberId);
            return Json(allJoinActivity);
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
            Activity activity = _activityHandler.EnterJoinedActivity(memberId, activityId);
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

        /*public void SendMessage(string memberId, string activityId, string message)
        {
            _activityHandler.SendMessage(memberId, activityId, message);
        }*/

        public void SearchMemberInfo(string keyWord)
        {
            List<Member> searchResult = _memberHandler.SearchMemberInfo(keyWord);
        }

        public async Task<JsonResult> BlackMember(FormCollection post)
        {
            await InitializationModel();
            if (_memberHandler.BlackMember(Request.Cookies["MemberId"].Value, post["MemberId"]))
            {
                UpdateBlackList(Request.Cookies["MemberId"].Value);
                return Json("加入黑名單成功");
                //True才新增資料庫
            }
            return Json("加入黑名單失敗");
        }

        public async Task<JsonResult> InputAcivityKeyWord(string keyWord)
        {
            await InitializationModel();
            return Json(_activityHandler.InputAcivityKeyWord(keyWord));
        }

        /*public bool KickOutPariticipant(string memberId, string activityId)
        {
            return _activityHandler.KickOutPariticipant(memberId, activityId);
        }*/

        public async Task<JsonResult> AddFriend (FormCollection post)
        {
            await InitializationModel();
            bool result = _memberHandler.AddFriendInvitation(Request.Cookies["MemberId"].Value, post["MemberId"]);
            if (result)
            {
                UpdateFriendInvitation(Request.Cookies["MemberId"].Value);
                //新增至資料庫
                return Json("發送好友邀請成功");
            }
            else
            {
                return Json("發送好友邀請失敗");
            }
        }
        /*Willie End*/

        /* Ting Start */
        public async Task<JsonResult> CreateActivity(Activity activityInfo, string memberId)
        {
            await InitializationModel();
            activityInfo.ParticipantList.Add(memberId);
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

        /*public bool transferHomeowner(string activityId, string newOwnerId)
        {
            return _activityHandler.transferHomeowner(activityId, newOwnerId);
        }*/

        public List<string> GetFriendsList(string memberId)
        {
            return _memberHandler.GetFriendsList(memberId);
        }

        /*public bool InviteFriend(string userName, string friendId, string activityId)
        {
            return _memberHandler.InviteMember(userName, friendId, activityId);
        }*/

        /*public bool DeleteFriend(string memberId, string targetId)
        {
            return _memberHandler.DeleteFriend(memberId, targetId);
        }*/

        public async Task<ActionResult> Room(string activityId, string userId, string isJoin)
        {
            await InitializationModel();
            Activity activity = _activityHandler.FindActivity(activityId);
            Member member = _memberHandler.GetMemberById(userId);

            if (isJoin == "1") // 1代表使用者點擊參加, 0代表使用者點擊進入
            {
                _activityHandler.JoinActivity(userId, activityId);
            }

            List<Member> participantsList = new List<Member>();
            List<Member> friendsList = new List<Member>();
            foreach (string memberId in activity.ParticipantList)
            {
                Member participant = _memberHandler.GetMemberById(memberId);
                participantsList.Add(participant);
            }
            foreach (string memberId in member.FriendList)
            {
                Member friend = _memberHandler.GetMemberById(memberId);
                friendsList.Add(friend);
            }

            ViewData["activity_id"] = activityId;
            ViewData["activity_name"] = activity.ActivityName;
            ViewData["owner_id"] = activity.HomeOwnerId;
            ViewData["activity_date"] = "2020/5/10";
            ViewData["participants_count"] = activity.ParticipantList.Count;
            ViewBag.participants = participantsList;
            ViewBag.friends = friendsList;
            return View();
        }

        public ActionResult updateActivity(string activityId, string activityName, string avtivityPeople, string avtivityDescription, string avtivityDate)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            // update
            return Json(new { success = true, responseText = "更新成功" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult transferHomeowner(string activityId, string newOwnerId)
        {
            try
            {
                _activityHandler.TransferHomeowner(activityId, newOwnerId);
                return Json(new { success = true, responseText = "轉移成功" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, responseText = "轉移失敗" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult KickOutPariticipant(string activityId, string targetId)
        {
            try
            {
                _activityHandler.KickOutPariticipant(targetId, activityId);
                return Json(new { success = true, responseText = "踢出成功" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, responseText = "踢出失敗" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult InviteFriend(string userName, string activityId, string targetId)
        {
            try
            {
                _memberHandler.InviteMember(userName, targetId, activityId);
                return Json(new { success = true, responseText = "邀請成功" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, responseText = "邀請失敗" }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> LeaveActivity(string activityId, string memberId)
        {
            await InitializationModel();
            try
            {
                Activity activity = _activityHandler.FindActivity(activityId);
                if (activity.ParticipantList.Contains(memberId))
                {
                    if (activity.HomeOwnerId != memberId)
                    {
                        activity.Leave(memberId);
                        return Json(new { success = true, responseText = "退出成功" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                        return Json(new { success = false, responseText = "請先至參加者名單轉移房主" }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { success = false, responseText = "您不再參加者名單中" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, responseText = "退出失敗" }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> SendMessage(string memberId, string memberName, string activityId, string messageContent)
        {
            try
            {
                Message message = new Message(memberId, memberName, messageContent);
                await _databaseSystem.SendMessage(activityId, message);
                return Json(new { success = true, responseText = "發送成功" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, responseText = "發送失敗" }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> updateChatroom(string activityId)
        {
            try
            {
                List<Message> messages = await _databaseSystem.GetChatData(activityId);
                return Json(new { success = true, responseText = messages }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, responseText = "連結聊天室失敗" }, JsonRequestBehavior.AllowGet);
            }
        }
        /* Ting End */

        /*Hsu start*/
        public void JoinActivity(string memberId, string activityId)
        {
            //InitializationModel();
            if (_activityHandler.JoinActivity(memberId, activityId))
            {
                Activity activity = _activityHandler.FindActivity(activityId);
                string participantsList = memberId + ",";
                foreach (string member in activity.ParticipantList)
                {
                    participantsList += member + ",";
                }
                Dictionary<string, string> newData = new Dictionary<string, string>();
                newData.Add("ParticipantList", participantsList);
                _databaseSystem.UpdateActivity(activityId, newData);
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

        public void UpdateFriendList(string memberId)
        {
            _databaseSystem.UpdateMemberInfo(_memberHandler.GetMemberById(memberId));
        }

        public void UpdateBlackList(string memberId)
        {
            _databaseSystem.UpdateMemberInfo(_memberHandler.GetMemberById(memberId));
        }

        public void UpdateFriendInvitation(string memberId)
        {
            _databaseSystem.UpdateMemberInfo(_memberHandler.GetMemberById(memberId));
        }

        public async Task<JsonResult> RejectInvitation(FormCollection post)
        {
            await InitializationModel();

            if (_memberHandler.RejectInvitation(Request.Cookies["MemberId"].Value, post["MemberId"]))
            {
                UpdateFriendInvitation(Request.Cookies["MemberId"].Value);
                return Json("拒絕好友邀請");
            }
            return Json("拒絕好友邀請");

        }

        public async Task<JsonResult> AgreeInvitation(FormCollection post)
        {
            await InitializationModel();
            
            if(_memberHandler.AgreeInvitation(Request.Cookies["MemberId"].Value, post["MemberId"]))
            {
                UpdateFriendList(Request.Cookies["MemberId"].Value);
                UpdateFriendList(post["MemberId"]);

                UpdateFriendInvitation(Request.Cookies["MemberId"].Value);
                return Json("同意好友邀請");
            }
            return Json("同意好友失敗");

        }

        public ActionResult Logout()
        {
            if (Request.Cookies["MemberId"] != null)
            {
                Response.Cookies["MemberId"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["MemberName"].Expires = DateTime.Now.AddDays(-1);
                Response.Redirect("/");
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
                Response.Redirect("/");
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
            return PartialView("Register");
        }

        public async Task<JsonResult> Registers(FormCollection post)
        {
            await InitializationModel();
            
            if (string.IsNullOrWhiteSpace(post["Password"]) || post["Password"] != post["Password2"])
            {
                return Json("密碼輸入錯誤");
            }
            else
            {
                Dictionary<string, string> memberInfo = new Dictionary<string, string>();
                foreach (var key in post)
                {
                    if (key.ToString() == "Password2") continue;
                    memberInfo.Add(key.ToString(), post[key.ToString()]);
                }
                if (_memberHandler.CreateNewMember(memberInfo))
                {
                    Member member = new Member(memberInfo);
                    _databaseSystem.UpdateMemberInfo(_memberHandler.GetMemberById(memberInfo["MemberId"]));
                    return Json("");
                }
                else
                {
                    return Json("帳號已使用...");
                }
            }
        }

        public async Task<JsonResult> UpdateMemberInfo(FormCollection post)
        {
            await InitializationModel();
            Dictionary<string, string> memberInfo = new Dictionary<string, string>();
            foreach (var key in post)
            {
                memberInfo.Add(key.ToString(), post[key.ToString()]);
            }
            _memberHandler.UpdateUserData(Request.Cookies["MemberId"].Value, memberInfo);
            _databaseSystem.UpdateMemberInfo(_memberHandler.GetMemberById(Request.Cookies["MemberId"].Value));
            return Json("");
        }

        public async Task<JsonResult> DeleteFriend(FormCollection post)
        {
            await InitializationModel();
            if (_memberHandler.DeleteFriend(Request.Cookies["MemberId"].Value, post["MemberId"]))
            {
                UpdateFriendList(Request.Cookies["MemberId"].Value);
                
                return Json("刪除好友成功");
            }
            return Json("刪除好友失敗");
        }

        public async Task<JsonResult> DeleteBlack(FormCollection post)
        {
            await InitializationModel();
            if (_memberHandler.DeleteBlack(Request.Cookies["MemberId"].Value, post["MemberId"]))
            {
                UpdateBlackList(Request.Cookies["MemberId"].Value);
                return Json("刪除黑名單成功");
            }
            return Json("刪除黑名單失敗");
        }

        public async Task<ActionResult> MemberInfo()
        {
            await InitializationModel();
            Member member = _memberHandler.GetMemberById(Request.Cookies["MemberId"].Value);
            ViewBag.member = member;
            return PartialView("MemberInfo");
        }

        public async Task<ActionResult> OtherMemberInfo()
        {
            await InitializationModel();
            Member member = _memberHandler.GetMemberById(Request.Cookies["MemberId"].Value);
            ViewBag.member = member;
            return PartialView("OtherMemberInfo");
        }


        public async Task<JsonResult> GetFriendList()
        {
            await InitializationModel();
            List<string> friendList = _memberHandler.GetFriendsList(Request.Cookies["MemberId"].Value);
            List<Member> memberList = new List<Member>();
            foreach (string memberId in friendList)
            {
                memberList.Add(_memberHandler.GetMemberById(memberId));
            }
            
            return Json(memberList);
        }

        public async Task<JsonResult> GetBlackList()
        {
            await InitializationModel();
            List<string> blackList = _memberHandler.GetBlackList(Request.Cookies["MemberId"].Value);
            List<Member> memberList = new List<Member>();
            foreach (string memberId in blackList)
            {
                memberList.Add(_memberHandler.GetMemberById(memberId));
            }

            return Json(memberList);
        }

        public async Task<JsonResult> GetInvitationList()
        {
            await InitializationModel();
            List<string> invitationList = _memberHandler.GetInvitationList(Request.Cookies["MemberId"].Value);
            List<Member> memberList = new List<Member>();
            foreach (string MemberId in invitationList)
            {
                memberList.Add(_memberHandler.GetMemberById(MemberId));
            }

            return Json(memberList);
        }
    }
}