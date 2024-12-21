using Application.IServices;
using Application.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace University.Units.Controllers;

[Authorize(Roles = "Student")]
public class UnitSelectionController : ControllerBase
{
    private readonly IUnitSelectionService _unitSelectionService;
    private readonly ITokenService _tokenService;
    private readonly ILogger<UnitSelectionController> _logger;

    public UnitSelectionController(IUnitSelectionService unitSelectionService, ITokenService tokenService, ILogger<UnitSelectionController> logger)
    {
        _unitSelectionService = unitSelectionService;
        _tokenService = tokenService;
        _logger = logger;
    }

    [HttpPost]
    [Route("University/UnitSelection/ChooseUnitSelection")]
    public void UnitSelection([FromBody] UnitSelectionRequest request)
    {
        _logger.LogInformation("users choose Units Selection");
        _unitSelectionService.UnitSelection(request);
    }

    
    
  
    
    
}
