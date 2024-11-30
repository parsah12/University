using Application.IServices;
using Application.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace University.Units.Controllers;

[Authorize]
public class UnitSelectionController : ControllerBase
{
    private readonly IUnitSelectionService _unitSelectionService;
    private readonly ITokenService _tokenService;

    public UnitSelectionController(IUnitSelectionService unitSelectionService, ITokenService tokenService)
    {
        _unitSelectionService = unitSelectionService;
        _tokenService = tokenService;
    }

    [HttpPost]
    [Route("University/UnitSelection/ChooseUnitSelection")]
    public void UnitSelection([FromBody] UnitSelectionRequest request)
    {
        _unitSelectionService.UnitSelection(request);
    }

    
}
