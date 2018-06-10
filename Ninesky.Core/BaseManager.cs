using Ninesky.DataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninesky.Core.Types;
using System.Data.Entity;
namespace Ninesky.Core
{
    /// <summary>
    /// 管理类的基类
    /// </summary>
    /// <typeparam name="T">模型类</typeparam>
    public class BaseManager<T> where T :class
    {
        /// <summary>
        /// 数据仓储类
        /// </summary>
        protected Repository<T> Repository;
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public BaseManager() : this(ContextFactory.CurrentContext())
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext">数据上下文</param>
        public BaseManager(DbContext dbContext)
        {
            Repository = new Repository<T>(dbContext);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <returns>成功时属性【Data】为添加后的数据实体</returns>
        public virtual Response Add(T entity)
        {
            Response _reponse = new Response();
            if (Repository.Add(entity) > 0)
            {
                _reponse.Code = 1;
                _reponse.Message = "添加数据成功";
                _reponse.Data = entity;
            }
            else
            {
                _reponse.Code = 0;
                _reponse.Message = "添加数据失败";
            }
            return _reponse;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <returns>成功时属性【Data】为更新后的数据实体</returns>
        public virtual Response Update(T entity)
        {
            Response _response = new Response();
            if (Repository.Update(entity) > 0)
            {
                _response.Code = 1;
                _response.Message = "更新数据成功";
                _response.Data = entity;
            }
            else
            {
                _response.Code = 0;
                _response.Message = "更新数据失败";
            }
            return _response;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns>Code：0-删除失败；1-删除陈功；10-记录不存在</returns>
        public virtual Response Delete(int ID)
        {
            Response _response = new Response();
            var _entity = Find(ID);
            if (_entity == null)
            {
                _response.Code = 10;
                _response.Message = "记录不存在";
            }
            else
            {
                if (Repository.Delete(_entity) > 0)
                {
                    _response.Code = 1;
                    _response.Message = "删除数据成功";
                }
                else
                {
                    _response.Code = 0;
                    _response.Message = "删除数据失败";
                }
            }
            return _response;
           
        }
        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns>实体</returns>
        public virtual T Find(int ID)
        {
            return Repository.Find(ID);
        }
        /// <summary>
        /// 查找数据列表-【所有数据】
        /// </summary>
        /// <returns>所有数据</returns>
        public IQueryable<T> FindList()
        {
            return Repository.FindList();
        }
        /// <summary>
        /// 查找分页数据
        /// </summary>
        /// <param name="paging">分页数据</param>
        /// <returns>分页数据</returns>
        public Paging<T> FindPageList(Paging<T> paging)
        {
            paging.Items = Repository.FindPageList(paging.PageSize, paging.PageIndex, out paging.TotalNumber).ToList();
            return paging;
        }
        /// <summary>
        /// 总记录数
        /// </summary>
        /// <returns>总记录数</returns>
        public virtual int Count()
        {
            return Repository.Count();
        }
    }
}
