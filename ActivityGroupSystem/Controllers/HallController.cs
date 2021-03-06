﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Firebase.Database;
using Firebase.Database.Query;
using System.Threading.Tasks;
using ActivityGroupSystem.Models;
using Newtonsoft.Json;

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
            ViewBag.Title = "揪團平台";
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

        [HttpPost()]
        public async Task<JsonResult> GetInvitedActivity(string memberId)
        {
            await InitializationModel();
            Member member = _memberHandler.GetMemberById(memberId);
            Dictionary<string, string> invitedList = new Dictionary<string, string>();
            List<Dictionary<string, string>> gridResult = new List<Dictionary<string, string>>();
            if (member != null)
            {
                invitedList = member.InvitedList;
            }

            if (invitedList.Count > 0)
            {
                foreach (var tempInvited in invitedList)
                {
                    Activity activity = _activityHandler.FindActivity(tempInvited.Key);
                    Dictionary<string, string> result = new Dictionary<string, string>();
                    if (activity != null)
                    {
                        result["ActivityId"] = activity.ActivityId;
                        result["ActivityName"] = activity.ActivityName;
                        result["HomeOwnerId"] = activity.HomeOwnerId;
                        result["NumberOfPeople"] = activity.NumberOfPeople;
                        result["ActivityNote"] = activity.ActivityNote;
                        result["ActivityDate"] = activity.ActivityDate;
                        result["InviteMember"] = tempInvited.Value;
                        gridResult.Add(result);
                    }
                }
            }

            return Json(gridResult);
        }

        public async Task<JsonResult> BlackMember(FormCollection post)
        {
            await InitializationModel();
            if (_memberHandler.BlackMember(Request.Cookies["MemberId"].Value, post["MemberId"]))
            {
                _memberHandler.DeleteFriend(Request.Cookies["MemberId"].Value, post["MemberId"]);

                UpdateMembe(Request.Cookies["MemberId"].Value);
                UpdateMembe(post["MemberId"]);
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

        public async Task<JsonResult> AddFriend (FormCollection post)
        {
            await InitializationModel();
            bool result = _memberHandler.AddFriendInvitation(Request.Cookies["MemberId"].Value, post["MemberId"]);
            if (result)
            {
                UpdateMembe(post["MemberId"]);
                UpdateMembe(Request.Cookies["MemberId"].Value);
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

        public List<string> GetAllParticipants(string activityId)
        {
            return _activityHandler.GetAllParticipants(activityId);
        }

        public List<string> GetFriendsList(string memberId)
        {
            return _memberHandler.GetFriendsList(memberId);
        }

        public async Task<JsonResult> DeleteInvitedList(string memberId, string activityId)
        {
            await InitializationModel();
            Member member = _memberHandler.GetMemberById(memberId);
            if (member.InvitedList.Count > 0)
            {
                bool matchInvited = false;
                foreach (var tempList in member.InvitedList)
                {
                    if (tempList.Key == activityId)
                    {
                        matchInvited = true;
                    }
                }

                if (matchInvited)
                {
                    member.InvitedList.Remove(activityId);
                    await _databaseSystem.UpdateInvitedList(memberId, member.InvitedList);
                }
            }
            return Json("");
        }

        public async Task<ActionResult> Room(string activityId, string userId)
        {
            await InitializationModel();
            Activity activity = _activityHandler.FindActivity(activityId);
            Member member = _memberHandler.GetMemberById(userId);

            if (_activityHandler.JoinActivity(userId, activityId))
            {
                List<string> participantList = _activityHandler.FindActivity(activityId).ParticipantList;
                await _databaseSystem.UpdateParticipantList(activityId, participantList);
                await DeleteInvitedList(userId, activityId);
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

            ViewData["participants_count"] = activity.ParticipantList.Count;
            ViewBag.Title = "揪團平台 " + activity.ActivityName + "的活動房間";
            ViewBag.activity = activity;
            ViewBag.participants = participantsList;
            ViewBag.friends = friendsList;
            return View();
        }

        public async Task<ActionResult> updateActivity(string activityId, string activityName, string avtivityPeople, string avtivityDescription, string avtivityDate)
        {
            Dictionary<string, string> newData = new Dictionary<string, string>();
            newData.Add("ActivityName", activityName);
            newData.Add("NumberOfPeople", avtivityPeople);
            newData.Add("ActivityNote", avtivityDescription);
            newData.Add("ActivityDate", avtivityDate);
            await _databaseSystem.UpdateActivity(activityId, newData);
            return Json(new { success = true, responseText = "更新成功" }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> TransferHomeowner(string activityId, string newOwnerId)
        {
            await InitializationModel();
            try
            {
                _activityHandler.TransferHomeowner(activityId, newOwnerId);
                Dictionary<string, string> newData = new Dictionary<string, string>();
                newData.Add("HomeOwnerId", newOwnerId);
                await _databaseSystem.UpdateActivity(activityId, newData);
                return Json(new { success = true, responseText = "轉移成功" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, responseText = "轉移失敗" }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> KickOutPariticipant(string activityId, string targetId)
        {
            await InitializationModel();
            try
            {
                int result = _activityHandler.KickOutPariticipant(targetId, activityId);
                if (result == 0)
                {
                    List<string> participantList = _activityHandler.FindActivity(activityId).ParticipantList;
                    await _databaseSystem.UpdateParticipantList(activityId, participantList);
                    return Json(new { success = true, responseText = "踢出成功" }, JsonRequestBehavior.AllowGet);
                }
                else if (result == 1)
                {
                    return Json(new { success = true, responseText = "踢出失敗，該使用已不在參加者名單" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, responseText = "踢出失敗，活動不存在" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { success = false, responseText = "踢出失敗" }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> InviteFriend(string userName, string activityId, string targetId)
        {
            await InitializationModel();
            try
            {
                if (!_activityHandler.IsParticipant(activityId, targetId))
                {
                    _memberHandler.InviteMember(userName, targetId, activityId);
                    Dictionary<string, string> invitedList = _memberHandler.GetMemberById(targetId).InvitedList;
                    await _databaseSystem.UpdateInvitedList(targetId, invitedList);
                    return Json(new { success = true, responseText = "邀請成功" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, responseText = "該好友已經是本活動的參加者囉" }, JsonRequestBehavior.AllowGet);
                }
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
                        List<string> participantList = _activityHandler.FindActivity(activityId).ParticipantList;
                        await _databaseSystem.UpdateParticipantList(activityId, participantList);
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
            await InitializationModel();
            try
            {
                if (_activityHandler.IsParticipant(activityId, memberId))
                {
                    Message message = new Message(memberId, memberName, messageContent);
                    _activityHandler.SendMessage(activityId, memberId, memberName, messageContent);
                    await _databaseSystem.SendMessage(activityId, message);
                    return Json(new { success = true, responseText = "發送成功", type = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, responseText = "很抱歉您非本活動的參加者故無法發言", type = 1 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { success = false, responseText = "發送失敗", type = 1 }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> updateChatroom(string activityId, string memberId)
        {
            await InitializationModel();
            try
            {
                int index = 0;
                _activityHandler.InitializeChatroom(activityId, await _databaseSystem.GetChatData(activityId));
                List<Message> messages = _activityHandler.GetChatData(activityId);
                // 清除黑名單人員的訊息
                while (messages.Count > index)
                {
                    if (_memberHandler.IsBlack(memberId, messages[index].MemberId))
                    {
                        messages.RemoveAt(index);
                    }
                    else
                        index++;
                }
                return Json(new { success = true, responseText = messages }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, responseText = "連結聊天室失敗" }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> IsParticipant(string activityId, string memberId)
        {
            await InitializationModel();
            bool result = _activityHandler.IsParticipant(activityId, memberId);
            return Json(new { success = true, responseText = result.ToString() }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> CloseActivity(string activityId, string memberId)
        {
            await InitializationModel();
            int result = _activityHandler.CloseActivity(activityId, memberId);
            if (result == 0)
            {
                await _databaseSystem.Delete(activityId, "Activity");
                return Json(new { success = true, responseText = "關閉成功" }, JsonRequestBehavior.AllowGet);
            }
            else if (result == 1)
                return Json(new { success = true, responseText = "關閉失敗，活動不存在" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = true, responseText = "關閉失敗，您沒有權限" }, JsonRequestBehavior.AllowGet);
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

        public void UpdateMembe(string memberId)
        {
            _databaseSystem.UpdateMemberInfo(_memberHandler.GetMemberById(memberId));
        }

        public async Task<JsonResult> RejectInvitation(FormCollection post)
        {
            await InitializationModel();

            if (_memberHandler.RejectInvitation(Request.Cookies["MemberId"].Value, post["MemberId"]))
            {
                UpdateMembe(Request.Cookies["MemberId"].Value);
                return Json("拒絕好友邀請");
            }
            return Json("拒絕好友邀請");

        }

        public async Task<JsonResult> AgreeInvitation(FormCollection post)
        {
            await InitializationModel();
            
            if(_memberHandler.AgreeInvitation(Request.Cookies["MemberId"].Value, post["MemberId"]))
            {
                UpdateMembe(Request.Cookies["MemberId"].Value);
                UpdateMembe(post["MemberId"]);

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
            string loginMessage = _memberHandler.CheckLoginAccount(account, password);

            //驗證帳號密碼
            if (loginMessage == "")
            {
                Member member = _memberHandler.GetMemberById(account);
                Response.Cookies["MemberName"].Value = member.MemberName;
                Response.Cookies["MemberId"].Value = member.MemberId;
                Response.Redirect("/");
                return new EmptyResult();
            }
            else
            {
                ViewBag.Msg = loginMessage;
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
            Response.Cookies["MemberName"].Value = memberInfo["MemberName"];
            _databaseSystem.UpdateMemberInfo(_memberHandler.GetMemberById(Request.Cookies["MemberId"].Value));
            //Response.Redirect(Request.Url.ToString());
            return Json("");
        }

        public async Task<JsonResult> DeleteFriend(FormCollection post)
        {
            await InitializationModel();
            if (_memberHandler.DeleteFriend(Request.Cookies["MemberId"].Value, post["MemberId"]))
            {
                UpdateMembe(Request.Cookies["MemberId"].Value);
                UpdateMembe(post["MemberId"]);

                return Json("刪除好友成功");
            }
            return Json("刪除好友失敗");
        }

        public async Task<JsonResult> DeleteBlack(FormCollection post)
        {
            await InitializationModel();
            if (_memberHandler.DeleteBlack(Request.Cookies["MemberId"].Value, post["MemberId"]))
            {
                UpdateMembe(Request.Cookies["MemberId"].Value);
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

        public async Task<ActionResult> OtherMemberInfo(string memberId)
        {
            await InitializationModel();
            Member member = _memberHandler.GetMemberById(memberId);
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