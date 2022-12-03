using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace FicheUEAuthApi.Controllers;

[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IAuthentification _authentification;


	public AuthController(IAuthentification authentification)
	{
		_authentification = authentification;
	}

	[HttpGet]
	[Route("auth/{credential}")]
	public IActionResult AuthenticateUser(string credential)
	{
		/*
		  exemple de credential : email=MonEmail@gmail.com&&password=MonMotDePasse
		 */

		var data = credential.Split("&&").ToList();
		(var email, var password) = 
			( data[0][(data[0].IndexOf("=") + 1)..],
			  data[1][(data[1].IndexOf("=") + 1)..]);
        string pattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
		if(Regex.IsMatch(email, pattern))
		{
			var token = _authentification.AuthenticateUser(email, password).Result;
			if(!String.IsNullOrEmpty(token)) 
				return Ok(token);
			
		}
        return NotFound();
	}


}
