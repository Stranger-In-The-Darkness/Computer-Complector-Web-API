using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.DataContext;
using ComputerComplectorWebAPI.Interfaces;
using ComputerComplectorWebAPI.Models.Users;
using ComputerComplectorWebAPI.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerComplectorWebAPI.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class UsersController : Controller
    {
		private UsersContext _dbContext;
		private IUserService _userService;

		public UsersController(UsersContext context, IUserService service)
		{
			_dbContext = context;
			_userService = service;
		}

		[AllowAnonymous]
		[HttpPost("authenticate")]
		public IActionResult Authenticate([FromBody]LoginModel userParam)
		{
			var user = _userService.Authenticate(userParam.Name, userParam.Password);

			if (user == null)
				return BadRequest(new { message = "Username or password is incorrect" });

			return Ok(new { Name = user.Name, Email = user.Email, Role = user.ROLE.Name, Token = user.Token });
		}

		[Authorize(Roles = "ADMIN")]
		[HttpGet]
		public IActionResult GetAll()
		{
			var users = _userService.GetAll();
			return Ok(users);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var user = _userService.GetById(id);

			if (user == null)
			{
				return NotFound();
			}

			// only allow admins to access other user records
			var currentUserId = int.Parse(User.Identity.Name);
			if (id != currentUserId && !User.IsInRole("Admin"))
			{
				return Forbid();
			}

			return Ok(user);
		}
	}
}