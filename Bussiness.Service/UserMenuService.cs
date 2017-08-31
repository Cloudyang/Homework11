using CodeFirstDB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Service
{
    public class UserMenuService : BaseService
    {
        public UserMenuService(DbContext context) : base(context)
        {
        }

        /// <summary>
        /// 增用户 (随机测试10个用户)
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public int AddUsers(IEnumerable<User> users)
        {
            return base.Insert(users);
        }

        public int AddMenu(Menu menu)
        {
            return base.Insert(menu);
        }

        /// <summary>
        /// 增菜单 (随机测试10个菜单，要求起码三层父子关系id/parentid，SourcePath=父SourcePath+/+GUID)
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
        public int AddMenu(int parentId, Menu menu)
        {
            var pMenu = base.Find<Menu>(parentId);
            menu.ParentId = pMenu.Id;
            menu.SourcePath = $"{pMenu.SourcePath}/{Guid.NewGuid().ToString()}";
            return this.AddMenu(menu);
        }

        /// <summary>
        /// 设置某个用户和10个菜单的映射关系（User  Menu  UserMenuMapping）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public int AddUserMenuMapping(int userId,int menuId)
        {
            return base.Insert<UserMenuMapping>(new UserMenuMapping
            {
                UserId = userId,
                MenuId = menuId
            });
        }

        /// <summary>
        /// 设置某个用户和10个菜单的映射关系（User  Menu  UserMenuMapping）
        /// </summary>
        /// <param name="user"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
        public int AdduserMenuMapping(User user,Menu menu)
        {
            return AddUserMenuMapping(user.Id, menu.Id);
        }
        public int UpdateMenu(Menu menu)
        {
            return base.Update(menu);
        }

        /// <summary>
        /// 物理删除某菜单的时候，删除其全部的映射
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public int DeleteMenu(Menu menu)
        {
            base.Delete(base.Query<UserMenuMapping>(um => um.MenuId == menu.Id));
            return base.Delete(menu);
        }

        /// <summary>
        /// 物理删除某菜单的时候，删除其全部的映射
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteMenu(int id)
        {
            base.Delete(base.Query<UserMenuMapping>(um => um.MenuId == id));
            return base.Delete<Menu>(id);
        }

        /// <summary>
        /// 找出拥有某菜单的全部用户列表
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public List<User> FindByMenuAllUser(int menuId)
        {
            var userIds = base.Query<UserMenuMapping>(um => um.MenuId == menuId).Select(u => u.UserId);
            List<User> users = base.Query<User>(u => userIds.Any(uId => uId == u.Id)).ToList();
            return users;
        }

        /// <summary>
        /// 找出某用户拥有的全部菜单列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Menu> FindByUserAllMenu(int userId)
        {
            var menuIds = base.Query<UserMenuMapping>(um => um.UserId == userId).Select(m => m.MenuId);
            List<Menu> menus = base.Query<Menu>(m => menuIds.Any(d => d == m.Id)).ToList();
            return menus;
        }

        /// <summary>
        /// 找出某用户拥有的全部菜单列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<Menu> FindByUserAllMenu(User user)
        {
            return FindByUserAllMenu(user.Id);
        }

        /// <summary>
        /// 根据菜单id找出全部子菜单的列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<Menu> FindAllChlidrenMenu(int parentId)
        {
            var menu = base.Find<Menu>(parentId);
            List<Menu> menus = base.Query<Menu>(m => m.SourcePath.StartsWith(menu.SourcePath)).ToList();
            return menus;
        }

        /// <summary>
        /// 找出名字中包含"系统"的菜单列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<Menu> QueryMenu(string keyword)
        {
            List<Menu> menus = base.Query<Menu>(m => m.Name.Contains(keyword)).ToList();
            return menus;
        }
    }
}
