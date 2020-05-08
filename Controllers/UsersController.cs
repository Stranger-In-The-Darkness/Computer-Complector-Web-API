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
using Microsoft.Extensions.Logging;

namespace ComputerComplectorWebAPI.Controllers
{
	/// <summary>
	/// API Controller for user authentification
	/// </summary>
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class UsersController : Controller
    {
		/// <summary>
		/// DB Context for users data
		/// </summary>
		private UsersContext _dbContext;

		/// <summary>
		/// Users authentification service
		/// </summary>
		private IUserService _userService;

		/// <summary>
		/// Logger
		/// </summary>
		private ILogger<UsersController> _logger;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context">User data Context</param>
		/// <param name="service">Users authentification service</param>
		/// <param name="logger">Logger</param>
		public UsersController(UsersContext context, IUserService service, ILogger<UsersController> logger)
		{
			_dbContext = context;
			_userService = service;
			_logger = logger;
		}

		/// <summary>
		/// Authentificate user with given <see cref="LoginModel"/>
		/// </summary>
		/// <param name="userParam">Given user data</param>
		/// <returns><see cref="OkObjectResult"/> if authenticated, <see cref="BadRequestObjectResult"/> otherwise</returns>
		[AllowAnonymous]
		[HttpPost("authenticate")]
		public IActionResult Authenticate([FromBody]LoginModel userParam)
		{
			var user = _userService.Authenticate(userParam.Name, userParam.Password);

			if (user == null)
				return BadRequest(new { message = "Username or password is incorrect" });

			return Ok(new { user.Name, user.Email, Role = user.ROLE.Name, user.Token });
		}

		/// <summary>
		/// Administrative function. Get all of <see cref="User"/> records
		/// </summary>
		/// <returns><see cref="OkObjectResult"/> and <see cref="IEnumerable{User}"/> of <see cref="User"/></returns>
		[Authorize(Roles = "ADMIN")]
		[HttpGet]
		public IActionResult GetAll()
		{
			var users = _userService.GetAll();
			return Ok(users);
		}

		/// <summary>
		/// Get user data via ID. Available only for user and admin
		/// </summary>
		/// <param name="id">User ID</param>
		/// <returns><see cref="OkObjectResult"/> and user data or <see cref="ForbidResult"/></returns>
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