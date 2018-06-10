using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninesky.Core.Types;

namespace Ninesky.Core
{
    public class AdministratorManager:BaseManager<Administrator>
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="admin">管理员实体</param>
        /// <returns></returns>
        public override Response Add(Administrator admin)
        {
            Response _resp = new Response();
            if (HasAccounts(admin.Accounts))
            {
                _resp.Code = 0;
                _resp.Message = "账号已存在";
            }
            else _resp = base.Add(admin);
            return _resp;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="administratorID">主键</param>
        /// <param name="password">新密码【密文】</param>
        /// <returns></returns>
        public Response ChangePassword(int administratorID,string password)
        {
            Response _resp = new Response();
            var _admin = Find(administratorID);
            if (_admin == null)
            {
                _resp.Code = 0;
                _resp.Message = "该主键的管理员不存在";
            }
            else
            {
                _admin.Password = password;
                _resp = Update(_admin);
            }
            return _resp;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="administratorID">主键</param>
        /// <returns></returns>
        public override Response Delete(int administratorID)
        {
            Response _resp = new Response();
            if (Count() == 1)
            {
                _resp.Code = 0;
                _resp.Message = "不能删除唯一的管理员账号";
            }
            else _resp = base.Delete(administratorID);
            return _resp;
        }
        /// <summary>
        /// 删除【批量】返回值Code：1-成功，2-部分删除，0-失败
        /// </summary>
        /// <param name="administratorIDList"></param>
        /// <returns></returns>
        public Response Delete(List<int> administratorIDList)
        {
            Response _resp = new Response();
            int _totalDel = administratorIDList.Count;
            int _totalAdmin = Count();
            foreach (var i in administratorIDList)
            {
                if (_totalAdmin > 1)
                {
                    base.Repository.Delete(new Administrator() { AdministratorID = i }, false);
                    _totalAdmin--;
                }
                else _resp.Message = "最少保留1名管理员";

            }
            _resp.Data = base.Repository.Save();
            if (_resp.Data == _totalDel)
            {
                _resp.Code = 1;
                _resp.Message = "成功删除" + _resp.Data + "名管理员";
            }
            else if(_resp.Data>0)
            {
                _resp.Code = 2;
                _resp.Message = "成功删除" + _resp.Data + "名管理员";

            }
            else
            {
                _resp.Code = 0;
                _resp.Message = "删除失败";
            }
            return _resp;
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="accounts">帐号</param>
        /// <returns></returns>
        public Administrator Find(string accounts)
        {
            return base.Repository.Find(a => a.Accounts == accounts);
        }
        /// <summary>
        /// 帐号是否存在
        /// </summary>
        /// <param name="accounts">帐号</param>
        /// <returns></returns>
        public bool HasAccounts(string accounts)
        {
            return base.Repository.IsContains(a => a.Accounts.ToUpper() == accounts.ToUpper());
        }
        /// <summary>
        /// 更新登录信息
        /// </summary>
        /// <param name="administratorID">主键</param>
        /// <param name="ip">IP地址</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public Response UpadateLoginInfo(int administratorID,string ip,DateTime time)
        {
            Response _resp = new Response();
            var _admin = Find(administratorID);
            if (_admin == null)
            {
                _resp.Code = 0;
                _resp.Message = "该主键的管理员不存在";
            }
            else
            {
                _admin.LoginIP = ip;
                _admin.LoginTime = time;
                _resp = Update(_admin);
            }
            return _resp;
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="accounts">帐号</param>
        /// <param name="password">密码【密文】</param>
        /// <returns>Code:1-成功;2-帐号不存在;3-密码错误</returns>
        public Response Verify(string accounts,string password)
        {
            Response _resp = new Response();
            var _admin = base.Repository.Find(a => a.Accounts == accounts);
            if (_admin == null)
            {
                _resp.Code = 2;
                _resp.Message = "账号为：【" + accounts + "】的管理员不存在";
            }
            else if (_admin.Password == password)
            {
                _resp.Code = 1;
                _resp.Message = "验证通过";
            }
            else
            {
                _resp.Code = 3;
                _resp.Message = "账号密码错误";
            }
            return _resp;

        }
    }
}
