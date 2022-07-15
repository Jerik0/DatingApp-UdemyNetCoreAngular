using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class UsersController : ControllerBase
  {
    private readonly DataContext _context;
    public UsersController(DataContext context)
    {
      // This gives us access to the database context. Sounds obvious but I wanted to emphasize it here.
      _context = context;
    }

    // api/users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
       return await _context.Users.ToListAsync();
    }

    // api/users/3
    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
      return await _context.Users.FindAsync(id);
    }
  }
}