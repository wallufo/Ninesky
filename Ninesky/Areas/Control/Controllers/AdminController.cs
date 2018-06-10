using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninesky.Core;
using Ninesky.Core.General;
using Ninesky.Areas.Control.Models;
using Ninesky.Core.Types;

namespace Ninesky.Areas.Control.Controllers
{
    [AdminAuthorize]
    public class AdminController : Controller
    {
        // GET: Control/Admin
        private AdministratorManager adminManager = new AdministratorManager();
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 添加JSON
        /// </summary>
        /// <param name="addAdmin"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName("AddPartialView")]
        public JsonResult AddJson(AddAdminViewModel addAdmin)
        {
            Response _res = new Response();
            if (ModelState.IsValid)
            {
                if (adminManager.HasAccounts(addAdmin.Accounts))
                {
                    _res.Code = 0;
                    _res.Message = "账号已存在";
                }
                else
                {
                    Administrator _admin = new Administrator();
                    _admin.Accounts = addAdmin.Accounts;
                    _admin.CreateTime = System.DateTime.Now;
                    _admin.Password = Security.Sha256(addAdmin.Password);
                    _res = adminManager.Add(_admin);
                }
            }
            else
            {
                _res.Code = 0;
                _res.Message = General.GetModelErrorString(ModelState);
            }
            return Json(_res);
        }
        /// <summary>
        /// 管理员列表
        /// </summary>
        /// <returns></returns>
        public JsonResult ListJson()
        {
            return Json(adminManager.FindList());
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
            {
                if (ModelState.IsValid)
                {
                    string _password = Security.Sha256(loginViewModel.Password);
                    //Console.Write(_password);
                    var _response = adminManager.Verify(loginViewModel.Accounts, _password);
                    if (_response.Code == 1)
                    {
                        var _admin = adminManager.Find(loginViewModel.Accounts);
                        Session.Add("AdminID", _admin.AdministratorID);
                        Session.Add("Accounts", _admin.Accounts);
                        _admin.LoginTime = DateTime.Now;
                        _admin.LoginIP = Request.UserHostAddress;
                        adminManager.Update(_admin);
                        return RedirectToAction("Index", "Home");
                    }
                    else if (_response.Code == 2) ModelState.AddModelError("Accounts", _response.Message);
                    else if (_response.Code == 3) ModelState.AddModelError("Password", _response.Message);
                    else ModelState.AddModelError("", _response.Message);
                }
                return View(loginViewModel);
            }
        public ActionResult Logout()
            {
                Session.Clear();
                return RedirectToAction("Login");
            }
        /// <summary>
        /// 添加【分部视图】
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AddpartialView()
        {
            return PartialView();
        }
        /// <summary>
        /// 删除 
        /// Response.Code:1-成功，2-部分删除，0-失败
        /// Response.Data:删除的数量
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteJson(List<int> ids)
        {
            int _total = ids.Count();
            Response _res = new Response();
            int _currentAdminID = int.Parse(Session["AdminID"].ToString());
            if (ids.Contains(_currentAdminID))
            {
                ids.Remove(_currentAdminID);
            }
            _res = adminManager.Delete(ids);
            if (_res.Code == 1 && _res.Data < _total)
            {
                _res.Code = 2;
                _res.Message = "共提交删除" + _total + "名管理员，实际删除" + _res.Data + "名管理员。\n原因：怒能删除当前登录的管理员账号";
            }
            else if (_res.Code == 2)
            {
                _res.Message = "共提交删除" + _total + "名管理员，实际删除" + _res.Data + "名管理员。";
            }
            return Json(_res);
        }
        /// <summary>
        /// 重置密码【Ninesky】
        /// </summary>
        /// <param name="id">管理员ID</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ResetPassword(int id)
        {
            string _password = "Ninesky";
            Response _resp = adminManager.ChangePassword(id, Security.Sha256(_password));
            if (_resp.Code == 1) _resp.Message = "密码重置为：" + _password;
            return Json(_resp);
        }
        /// <summary>
        /// 我的资料
        /// </summary>
        /// <returns></returns>
        public ActionResult MyInfo()
        {
            return View(adminManager.Find(Session["Accounts"].ToString()));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult MyInfo(FormCollection form)
        {
            var _admin = adminManager.Find(Session["Accounts"].ToString());
            if (_admin.Password != form["Password"])
            {
                _admin.Password = Security.Sha256(form["Password"]);
                var _resp = adminManager.ChangePassword(_admin.AdministratorID, _admin.Password);
                if (_resp.Code == 1) ViewBag.Message = "<div class=\"alert alert-success\" role=\"alert\"><span class=\"glyphicon glyphicon-ok\">修改密码成功!</span></div>";
                else ViewBag.Message= "<div class=\"alert alert-danger\" role=\"alert\"><span class=\"glyphicon glyphicon-remove\"></span>修改密码失败！</div>";

            }
            return View(_admin);
        }
    }
}