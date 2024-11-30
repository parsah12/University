using Application.IServices;
using Application.Requests;
using Model.Entites;
using Model.IRepository;

namespace ApplicationService.Services;

public class UnitSelectionService : IUnitSelectionService
{
    private readonly ITokenService _tokenService;
    private readonly IUnitSelectionRepository _unitSelectionRepository;
    public UnitSelectionService(ITokenService tokenService, IUnitSelectionRepository unitSelectionRepository)
    {
        _tokenService = tokenService;
        _unitSelectionRepository = unitSelectionRepository;
    }

    public int UnitSelection(UnitSelectionRequest unitSelection)
    {
        var entity = new UnitSelectionEntity
        {
            CourseId = unitSelection.CourseId,
            UserId = unitSelection.UserId,
            TeacherId = unitSelection.TeacherId,
        };
        _unitSelectionRepository.UnitSelection(entity);

        return entity.Id;
    }
}

