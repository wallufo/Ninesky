using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninesky.Core.Types;
using Ninesky.DataLibrary;

namespace Ninesky.Core.Category
{
    public class CategoryManager:BaseManager<Category>
    {
        #region 添加栏目
        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="category">栏目数据</param>
        /// <returns></returns>
        public override Response Add(Category category)
        {
            return Add(category,new CategoryGeneral() { CategoryID=category.CategoryID,ContentView="Index",View="Index"});
        }
        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="category">基本信息</param>
        /// <param name="general">常规栏目信息</param>
        /// <returns></returns>
        public Response Add(Category category,CategoryGeneral general)
        {
            Response _response = new Response() { Code=1};
            _response = base.Add(category);
            general.CategoryID = category.CategoryID;
            var _generalManage = new CategoryGeneralManager();
            _generalManage.Add(general);
            return _response;
        }
        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="category">基本信息</param>
        /// <param name="page">单页栏目信息</param>
        /// <returns></returns>
        public Response Add(Category category,CategoryPage page)
        {
            Response _response = new Response() { Code = 1 };
            _response = base.Add(category);
            page.CategoryID = category.CategoryID;
            var _pageManager = new CategoryPageManager();
            _pageManager.Add(page);
            return _response;
        }
        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="category">基本信息</param>
        /// <param name="link">外部链接信息</param>
        /// <returns></returns>
        public Response Add(Category category,CategoryLink link)
        {
            Response _response = new Response() { Code = 1 };
            _response = base.Add(category);
            link.CategoryID = category.CategoryID;
            var _linkManager = new CategoryLinkManager();
            _linkManager.Add(link);
            return _response;
        }
        #endregion
        public List<Category> FindChildren(int id)
        {
            return Repository.FindList(c => c.ParentID == id, new OrderParam() {Method=OrderMethod.ASC,PropertyName="Order" }).ToList();
        }
    }
}
