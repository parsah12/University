using Model.Entites;
using Model.IRepository;

namespace Infrastructure.Repository;

public class UnitSelectionRepository : IUnitSelectionRepository
{
    private readonly UniversityContext _universityContext;

    public UnitSelectionRepository(UniversityContext universityContext)
    {
        _universityContext = universityContext;
    }

    public int UnitSelection(UnitSelectionEntity unitSelection)
    {
        if (unitSelection is not null)
        {
            try
            {

                _universityContext.UnitSelections!.Add(unitSelection);
                var res = _universityContext.SaveChanges();
                return res;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        return 0;
    }
}
