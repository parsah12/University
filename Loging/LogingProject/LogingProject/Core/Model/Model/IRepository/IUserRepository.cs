using Project.Core.Model.Entities;

namespace Project.Core.Model.IRepository
{
    public interface IUserRepository
    {
        bool AddUser(UserEntity user);
        List<UserEntity> GetAllUsers();
        UserEntity? GetUserBy(string userName);
        int UnitSelection(UnitSelectionEntity unitSelection);


    }
}
