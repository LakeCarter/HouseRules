using System.Security.Claims;
using HouseRules.Data;
using HouseRules.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Reader;

namespace HouseRules.Controllers;

[ApiController]
[Route("api/{controller}")]

public class ChoreController : ControllerBase
{
    private HouseRulesDbContext _dbContext;

    public ChoreController(HouseRulesDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        return Ok(_dbContext.Chores
        );
    }

    [HttpGet("{id}")]
    // [Authorize]
    public IActionResult GetChoreById(int id)
    {
        Chore ChoresToReturn = _dbContext.Chores
        .Include(c => c.ChoreAssignments)
        .Include(c => c.ChoreCompletions)
        .SingleOrDefault(c => c.Id == id);

        if (ChoresToReturn == null)
        {
            return NotFound();
        }

        return Ok(ChoresToReturn);

    }

    [HttpPost("{id}/complete")]
    [Authorize]

    public IActionResult CompleteChore(int id, [FromQuery] int userId)
    {
        Chore choreToComplete = _dbContext.Chores.SingleOrDefault(c => c.Id == id);
        if (choreToComplete == null)
        {
            return NotFound();
        }

        string identityId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        UserProfile initiatingUser = _dbContext.UserProfiles
        .SingleOrDefault(up => userId == 0 ? up.IdentityUserId == identityId : up.Id == userId);

        if (initiatingUser == null)
        {
            return BadRequest();
        }

        _dbContext.ChoreCompletions.Add(new ChoreCompletion
        {
            UserProfileId = initiatingUser.Id,
            ChoreId = choreToComplete.Id,
            CompletedOn = DateTime.Now
        });

        _dbContext.SaveChanges();
        return NoContent();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]

    public IActionResult CreateChore(Chore chore)
    {
        _dbContext.Chores.Add(chore);
        _dbContext.SaveChanges();
        return Created($"api/chore/{chore.Id}", chore);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]

    public IActionResult UpdateChore(int id, Chore chore)
    {
        Chore choreToUpdate = _dbContext.Chores.SingleOrDefault(c => c.Id == id);

        if (choreToUpdate == null)
        {
            return NotFound();
        }
        if (id != chore.Id)
        {
            return BadRequest();
        }

        choreToUpdate.Name = chore.Name;
        choreToUpdate.Difficulty = chore.Difficulty;
        choreToUpdate.ChoreFrequencyDays = chore.ChoreFrequencyDays;

        _dbContext.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]

    public IActionResult DeleteChore(int id)
    {
        Chore choreToDelete = _dbContext.Chores.SingleOrDefault(c => c.Id == id);
        if (choreToDelete == null)
        {
            return NotFound();
        }

        _dbContext.Chores.Remove(choreToDelete);
        _dbContext.SaveChanges();
        return NoContent();
    }

    [HttpPost("{id}/assign")]
    [Authorize(Roles = "Admin")]

    public IActionResult AssignChore(int id, [FromQuery] int userId)
    {
        Chore choreToAssign = _dbContext.Chores.SingleOrDefault(c => c.Id == id);
        if (choreToAssign == null)
        {
            return NotFound();
        }

        string identityId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        UserProfile UserToAssign = _dbContext.UserProfiles
        .SingleOrDefault(up => userId == 0 ? up.IdentityUserId == identityId : up.Id == userId);
        if (UserToAssign == null)
        {
            return BadRequest();
        }

        if (_dbContext.ChoreAssignments.Any(ca => ca.ChoreId == choreToAssign.Id && ca.UserProfileId == UserToAssign.Id))
        {
            return BadRequest("User is already assigned to this chore");
        }

        _dbContext.ChoreAssignments.Add(new ChoreAssignment
        {
            ChoreId = choreToAssign.Id,
            UserProfileId = UserToAssign.Id
        });

        _dbContext.SaveChanges();
        return NoContent();
    }

    [HttpPost("{id}/unassign")]
    [Authorize(Roles = "Admin")]

    public IActionResult UnassignChore(int id, [FromQuery] int userId)
    {
        Chore choreToUnassign = _dbContext.Chores.SingleOrDefault(c => c.Id == id);
        if (choreToUnassign == null)
        {
            return NotFound();
        }

        string identityId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        UserProfile UserToUnassign = _dbContext.UserProfiles
        .SingleOrDefault(up => userId == 0 ? up.IdentityUserId == identityId : up.Id == userId);

        if (UserToUnassign == null)
        {
            return BadRequest();
        }

        ChoreAssignment choreAssignment = _dbContext.ChoreAssignments
        .SingleOrDefault(ca => ca.ChoreId == choreToUnassign.Id && ca.UserProfileId == UserToUnassign.Id);

        if (choreAssignment == null)
        {
            return BadRequest("User is not assigned this chore");
        }

        _dbContext.ChoreAssignments.Remove(choreAssignment);
        _dbContext.SaveChanges();
        return NoContent();
    }
}