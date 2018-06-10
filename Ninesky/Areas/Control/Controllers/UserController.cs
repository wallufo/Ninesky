using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninesky.Core;
using Ninesky.Core.Types;
using Ninesky.Areas.Control.Models;
using Ninesky.Models;

namespace Ninesky.Areas.Control.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [AdminAuthorize]
    public class UserController : Controller
    {
        private UserManage UserManage = new UserManage();
        // GET: Control/User
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Modify(int id)
        {
            var _roles = new RoleManager().FindList();
            List<SelectListItem> _listItems = new List<SelectListItem>(_roles.Count());
            foreach(var _role in _roles)
            {
                _listItems.Add(new SelectListItem() { Text = _role.Name, Value = _role.RoleID.ToString() });
            }
            ViewBag.Roles = _listItems;
            return PartialView(UserManage.Find(id));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Modify(int id,FormCollection form)
        {
            Response _resp = new Response();
            var _user = UserManage.Find(id);
            if(TryUpdateModel(_user,new string[] { "RoleID", "Name", "Sex", "Email" }))
            {
                if (_user == null)
                {
                    _resp.Code = 0;
                    _resp.Message = "用户不存在，可能遭到删除";
                }
                else
                {
                    if (_user.Password != form["Password"].ToString())
                    {
                        _user.Password = Core.General.Security.Sha256(form["Password".ToString()]);
                        _resp = UserManage.Update(_user);
                    }
                }
            }
            else
            {
                _resp.Code = 0;
                _resp.Message = General.GetModelErrorString(ModelState);

            }
            return Json(_resp);

        }
        /// <summary>
        /// 分页列表【json】
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="username">用户名</param>
        /// <param name="name">名称</param>
        /// <param name="sex">性别</param>
        /// <param name="email">Email</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="order">排序</param>
        /// <returns>Json</returns>
        [HttpPost]
        public ActionResult PageListJson(int? roleID, string username, string name, int? sex, string email, int? pageNumber, int? pageSize, int? order)
        {
            Paging<User> _pagingUser = new Paging<User>();
            if (pageNumber != null && pageNumber > 0) _pagingUser.PageIndex = (int)pageNumber;
            if (pageSize != null && pageSize > 0) _pagingUser.PageSize = (int)pageSize;
            var _paging = UserManage.FindPageList(_pagingUser, roleID, username, name, sex, email, null);
            return Json(new { total=_paging.TotalNumber,rows=_paging.Items});
        }
        /// <summary>
        /// 用户名是否可用
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CanUsername(string Username)
        {
            return Json(!UserManage.HasUsername(Username));
        }
       /// <summary>
       /// Email是否可用
       /// </summary>
       /// <param name="Email"></param>
       /// <returns></returns>
        [HttpPost]
        public JsonResult CanEmail(string Email)
        {
            return Json(!UserManage.HasEmail(Email));
        }
        public ActionResult Add()
        {
            var _roles = new RoleManager().FindList();
            List<SelectListItem> _listItems = new List<SelectListItem>(_roles.Count());
            foreach(var _role in _roles)
            {
                _listItems.Add(new SelectListItem() { Text = _role.Name, Value = _role.RoleID.ToString() });
            }
            ViewBag.Roles = _listItems;
            return View();
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Add(AddUserViewModel userViewModel)
        {
            if (UserManage.HasUsername(userViewModel.Username)) ModelState.AddModelError("Username", "用户名已存在");
            if (UserManage.HasEmail(userViewModel.Email)) ModelState.AddModelError("Email", "Email已存在");
            if (ModelState.IsValid)
            {
                User _user = new User();
                _user.RoleID = userViewModel.RoleID;
                _user.Username = userViewModel.Username;
                _user.Name = userViewModel.Name;
                _user.Sex = userViewModel.Sex;
                _user.Password = Core.General.Security.Sha256(userViewModel.Password);
                _user.Email = userViewModel.Email;
                _user.RegTime = System.DateTime.Now;
                var _response = UserManage.Add(_user);
                if (_response.Code == 1) return View("Prompt", new Prompt()
                {
                    Title = "添加用户成功",
                    Message = "您已添加了用户【" + _response.Data.Username + "(" + _response.Data.Name + ")"+"】",
                    Buttons = new List<string> (){"<a href=\""+Url.Action("Index","User")+"\" class=\"bth bth-default\">用户管理</a>",
                    "<a href=\""+Url.Action("Details","User",new { id=_response.Data.UserID})+"\" class=\"bth bth-default\">查看用户</a>",
                    "<a href=\"" + Url.Action("Add", "User") + "\" class=\"btn btn-default\">继续添加</a>"}
                });
                else ModelState.AddModelError("", _response.Message);
            }
            var _roles = new RoleManager().FindList();
            List<SelectListItem> _listitems = new List<SelectListItem>(_roles.Count());
            foreach(var _role in _roles)
            {
                _listitems.Add(new SelectListItem() { Text = _role.Name, Value = _role.RoleID.ToString() });
            }
            ViewBag.Roles = _listitems;
            return View(userViewModel);
           }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            return Json(UserManage.Delete(id));
        }
        }
    
}