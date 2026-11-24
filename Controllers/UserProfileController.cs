using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HouseRules.Data;
using HouseRules.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HouseRules.Models;

namespace HouseRules.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserProfileController : ControllerBase
{
    private HouseRulesDbContext _dbContext;

    public UserProfileController(HouseRulesDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Get()
    {
        return Ok(_dbContext
            .UserProfiles
            .Include(up => up.IdentityUser)
            .Select(up => new UserProfileDTO
            {
                Id = up.Id,
                FirstName = up.FirstName,
                LastName = up.LastName,
                Address = up.Address,
                IdentityUserId = up.IdentityUserId,
                Email = up.IdentityUser.Email,
                UserName = up.IdentityUser.UserName
            })
            .ToList());
    }
    
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetUserById(int id)
    {
        UserProfile userProfileToReturn = _dbContext.UserProfiles
        .Include(up => up.ChoreAssignments)
            .ThenInclude(ac => ac.Chore)
        .Include(up => up.ChoreCompletions)
            .ThenInclude(cc => cc.Chore)
        .Include(up=> up.IdentityUser)
        .Where(up=> up.Id == id).FirstOrDefault();

        if (userProfileToReturn == null)
        {
            return (NotFound());
        }

        return Ok(new UserProfileDTO
        {
            Id=userProfileToReturn.Id,
            FirstName=userProfileToReturn.FirstName,
            LastName=userProfileToReturn.LastName,
            Address=userProfileToReturn.Address,
            IdentityUserId=userProfileToReturn.IdentityUserId,
            IdentityUser=userProfileToReturn.IdentityUser,
            ChoreAssignments=userProfileToReturn.ChoreAssignments,
            ChoreCompletions=userProfileToReturn.ChoreCompletions
        });
    }
}