using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Ninesky.Core
{
    public class NineskyContext:DbContext
    {
        /// <summary>
        /// 管理员
        /// </summary>
        public DbSet<Administrator> Administrators { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public DbSet<Role> Roles { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<User> Users { get; set; }
        public NineskyContext() : base("DefaultConnection")
        {
            Database.SetInitializer<NineskyContext>(new CreateDatabaseIfNotExists<NineskyContext>());
        }
    }
}
