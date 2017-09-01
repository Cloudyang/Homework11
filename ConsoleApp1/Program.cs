using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using Bussiness.Interface;
using CodeFirstDB;

namespace ConsoleApp1
{
    class Program
    {
        private static IBaseService _BaseService = UnityResolve<IBaseService>.GetInstance();
        private static IUserMenuService _UserMenuService = UnityResolve<IUserMenuService>.GetInstance();
        static void Main(string[] args)
        {
            #region 2 建一个控制台程序/单元测试，自己完成配置Unity，做到能经过Unity创建对象去做数据查询，项目中不要出现具体的Bussiness.Service
            Console.WriteLine("====================已往所有用户===========================");
            var users = _BaseService.Set<User>();
            Show(users);
            Console.WriteLine("=====================已往所有菜单==========================");
            var menus = _BaseService.Set<Menu>();
            Show(menus);
            #endregion

            #region a 增用户 (随机测试10个用户)
            {
                List<User> userList = new List<User>();
                for (int i = 0; i < 10; i++)
                {
                    var user = new User
                    {
                        Account = $"yy{i}",
                        Password = $"{i}",
                        CompanyName = "VK",
                        CompanyId = 1,
                        Email = "yy@test.com",
                        Mobile = "13566626562",
                        Name = $"Name{i}",
                        State = 1,
                        UserType = 1,
                        CreateTime = DateTime.Now,
                        CreatorId = 1,
                    };
                    userList.Add(user);
                }
                var iResult = _UserMenuService.AddUsers(userList);
                Console.WriteLine($"成功插入{iResult}条记录");
            }
            #endregion

            #region b 增菜单 (随机测试10个菜单，要求起码三层父子关系id/parentid，SourcePath=父SourcePath+/+GUID)
            {
                int firstLevel = 0;
                int secondLevel = 0;
                for (int i = 0; i < 10; i++)
                {
                    var level = i % 3;
                    var menu = new Menu
                    {
                        Id = i,
                        Description = $"第{i}次创建",
                        MenuLevel = level,
                        Name = $"系统菜单{level}_{i}",
                        State = 1,
                        SourcePath = Guid.NewGuid().ToString(),
                        ParentId = 0,
                        Sort = i,
                        CreateTime = DateTime.Now,
                        CreatorId = 1
                    };
                    switch (level)
                    {
                        case 0:
                            firstLevel = menu.Id;
                            _UserMenuService.AddMenu(menu);
                            break;
                        case 1:
                            secondLevel = menu.Id;
                            _UserMenuService.AddMenu(firstLevel, menu);
                            break;
                        case 2:
                            _UserMenuService.AddMenu(secondLevel, menu);
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion

            #region c 设置某个用户和10个菜单的映射关系（User  Menu  UserMenuMapping）
            {
                int userId = new Random().Next(20);
                for (int i = 0; i < 10; i++)
                {
                    int menuId = new Random(DateTime.Now.Millisecond).Next(10);
                    _UserMenuService.AddUserMenuMapping(userId, menuId);
                }
            }
            #endregion

            #region d 找出某用户拥有的全部菜单列表
            {
                int userId = new Random().Next(10);
                List<Menu> menuList = _UserMenuService.FindByUserAllMenu(userId);
                Console.WriteLine("================d 找出某用户拥有的全部菜单列表==============================");
                Show(menuList);
            }
            #endregion

            #region e 找出拥有某菜单的全部用户列表
            {
                var menuId = new Random().Next(10);
                var userList = _UserMenuService.FindByMenuAllUser(menuId);
                Console.WriteLine("=================e 找出拥有某菜单的全部用户列表=============================");
                Show(userList);
            }
            #endregion

            #region e 根据菜单id找出全部子菜单的列表
            {
                var menuId = new Random().Next(10);
                var menuList = _UserMenuService.FindAllChlidrenMenu(menuId);
                Console.WriteLine("=================e 根据菜单id找出全部子菜单的列表=============================");
                Show(menuList);
            }
            #endregion

            #region f 找出名字中包含"系统"的菜单列表
            {
                var menuList = _UserMenuService.QueryMenu("系统");
                Console.WriteLine("=================f 找出名字中包含\"系统\"的菜单列表=============================");
                Show(menuList);
            }
            #endregion

            #region 物理删除某用户的时候，删除其全部的映射
            {
                int userId = new Random().Next(20);
                var iResult = _UserMenuService.DeleteUser(userId);
                Console.WriteLine($"成功删除ID:{userId}用户并删除其全部的映射,{iResult}条");
            }
            #endregion

            #region h 物理删除某菜单的时候，删除其全部的映射
            {
                int menuId = new Random().Next(20);
                var iResult = _UserMenuService.DeleteMenu(menuId);
                Console.WriteLine($"成功删除ID:{menuId}菜单并删除其全部的映射,{iResult}条");
            }
            #endregion
            Console.ReadKey();
        }

        static void Show<T>(IEnumerable<T> entityList)
        {
            if (entityList == null) return;
            Type type = typeof(T);
            foreach (var prop in type.GetProperties())
            {
                Console.Write($"{prop.Name}\t");
            }
            foreach (var entity in entityList)
            {
                Console.WriteLine();
                foreach (var prop in type.GetProperties())
                {
                    Console.Write($"{prop.GetValue(entity)}\t");
                }
            }
            Console.WriteLine();
        }
    }
}
