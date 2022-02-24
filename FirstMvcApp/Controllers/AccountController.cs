using System.Security.Claims;
using AutoMapper;
using FirstMvcApp.Core.Interfaces;
using FirstMvcApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FirstMvcApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        private readonly ISourceService _sourceService;
        private readonly IRssService _rssService;
        private readonly IHtmlParserService _htmlParserService;

        public AccountController(IMapper mapper,
            IAccountService accountService, 
            ILogger<AccountController> logger, IConfiguration configuration,
            ISourceService sourceService, IRssService rssService, IHtmlParserService htmlParserService)
        {
            _mapper = mapper;
            _accountService = accountService;
            _logger = logger;
            _configuration = configuration;
            _sourceService = sourceService;
            _rssService = rssService;
            _htmlParserService = htmlParserService;
            //_newsService = newsService;`
        }


        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginModel model)
        {
            if (await _accountService.CheckPassword(model.Email, model.Password))
            {
                var claims = new List<Claim>() { new Claim(ClaimTypes.Name, model.Email) };
                var claimsIdentity = new ClaimsIdentity(claims, authenticationType: "Cookies");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (!await _accountService.CheckUserWithThatEmailIsExistAsync(model.Email))
                {
                    var userId = await _accountService.CreateUserAsync(model.Email);
                    await _accountService.SetRoleAsync(userId, "User");
                    await _accountService.SetPasswordAsync(userId, model.Password);
                }
                else
                {
                    ModelState.TryAddModelError("UserAlreadyExist", "User is already exist");
                }
            }
            return View(model);
        }
    }


}
