using FicheUEAuthApi.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
namespace FicheUEAuthApi.Controllers;

[Route("users")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
	
	private readonly IUserManagement _userManagement;
	private readonly IMapper _mapper;
	
	public UsersController( IUserManagement userManagement, IMapper mapper)
	 => (_userManagement, _mapper) = (userManagement, mapper);

    [HttpPost]
    [Route("addUser")]
	[Authorize(Roles = "Admin")]
    public IActionResult AddUser(User user)
	{
		if (user is null)
			return BadRequest("Invaid user");
		var userAdded = _userManagement.AddUser(user).Result;
		return Created("GetUserById", _mapper.Map<UserDTO>(userAdded));
	}
	
	[HttpGet]
    [Route("getById/{Id}", Name = "GetUserById")]
    public IActionResult GetUser(Guid Id)
	{
		try
		{
			var user = _userManagement.GetUserById(Id).Result;
			return Ok(_mapper.Map<UserDTO>(user));
		}
		catch (Exception)
		{
			return NotFound();
		}
	}

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult GetALlUsers()
    {
        try
        {
            var users = _userManagement.GetAllUsers().Result;
            return Ok(_mapper.Map<IEnumerable<UserDTO>>(users));
        }
        catch (Exception)
        {
            return NotFound();
        }
    }


    [HttpGet]
    [Route("getByEmail/{Email}", Name = "GetUserByEmail")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetUser(string Email)
	{
		try
		{
			var user = _userManagement.GetUserByEmail(Email).Result;
			return Ok(_mapper.Map<UserDTO>(user));
		}
		catch (Exception)
		{
			return NotFound();
		}
	}
	
	[HttpDelete]
    [Route("{Id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteUser(Guid Id)
	{
		try
		{
			var response = _userManagement.DeleteUser(Id).Result;
			if(response)
				return NoContent();
			return BadRequest("Erreur");
		}
		catch (Exception)
		{
			return NotFound();
		}
	}

	
	[HttpPatch]
    [Route("updateUser")]
    public IActionResult UpdateUser([FromBody] User user)
	{
		try
		{
			if (user is null) return BadRequest("Invalid user");
			var response = _userManagement.UpdateUser(user).Result;
			return Ok(_mapper.Map<UserDTO>(response));
		}
		catch (Exception)
		{
			return NotFound();
		}
	}

}
