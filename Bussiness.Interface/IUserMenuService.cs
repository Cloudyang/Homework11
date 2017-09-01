using System.Collections.Generic;
using CodeFirstDB;

namespace Bussiness.Interface
{
    public interface IUserMenuService
    {
        int AddMenu(Menu menu);
        int AddMenu(int parentId, Menu menu);
        int AddUserMenuMapping(User user, Menu menu);
        int AddUserMenuMapping(int userId, int menuId);
        int AddUsers(IEnumerable<User> users);
        int DeleteMenu(Menu menu);
        int DeleteMenu(int menuId);
        int DeleteUser(User menu);
        int DeleteUser(int userId);
        List<Menu> FindAllChlidrenMenu(int parentId);
        List<User> FindByMenuAllUser(int menuId);
        List<Menu> FindByUserAllMenu(User user);
        List<Menu> FindByUserAllMenu(int userId);
        List<Menu> QueryMenu(string keyword);
        int UpdateMenu(Menu menu);
    }
}