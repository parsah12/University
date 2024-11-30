using BCrypt.Net;
using University.User.Model.IRepository;
using University.User.Model.Entities;


namespace University.User.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly UniversityContext _universityContext;

    public UserRepository(UniversityContext universityContext)
    {
        _universityContext = universityContext;
    }

    public bool AddUser(UserEntity user)
    {
        if (user is not null)
        {
            var hashPass = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password, 8);
            var entity = new UserEntity
            {
                UserName = user.UserName,
                Password = hashPass.ToString(),
                Age = user.Age,
                EntryDate = user.EntryDate,
                FieldOfStudy = user.FieldOfStudy,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MeliCode = user.MeliCode,
                Role = user.Role,
            };
            _universityContext.Users!.Add(entity);
            _universityContext.SaveChanges();
            return true;
        }
        return false;


    }


    public List<UserEntity> GetAllUsers()
    {
        var res = _universityContext.Users!.ToList();
        return res;
    }

    public UserEntity? GetUserBy(string userName)
    {
        var user = _universityContext.Users!.Where(e => e.UserName == userName).FirstOrDefault();
        return user;
    }

    public int UnitSelection(UnitSelectionEntity unitSelection)
    {
        if (unitSelection is not null)
        {
            _universityContext.UnitSelections!.Add(unitSelection);
            var res = _universityContext.SaveChanges();
            return res;
        }
        return 0;
    }
}
