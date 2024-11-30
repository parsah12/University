using University.User.Model.Entities;

namespace University.User.Model.IRepository
{
    public interface IUserRepository
    {
        bool AddUser(UserEntity user);
        List<UserEntity> GetAllUsers();
        UserEntity? GetUserBy(string userName);
        int UnitSelection(UnitSelectionEntity unitSelection);


    }
}
