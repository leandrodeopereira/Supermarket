﻿namespace SupermarketApi.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SupermarketApi.Dtos;
    using SupermarketApi.Entities.Identity;
    using SupermarketApi.Errors;
    using SupermarketApi.Mapping;
    using SupermarketApi.Services;

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IBuilder<HttpStatusCode, ApiResponse> apiResponseBuilder;
        private readonly ITokenService tokenService;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IBuilder<HttpStatusCode, ApiResponse> apiResponseBuilder,
            ITokenService tokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.apiResponseBuilder = apiResponseBuilder;
            this.tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            if (login is null)
            {
                return this.BadRequest(this.apiResponseBuilder.Build(HttpStatusCode.BadRequest));
            }

            var user = await this.userManager.FindByEmailAsync(login.Email).ConfigureAwait(false);

            if (user is null)
            {
                return this.Unauthorized(this.apiResponseBuilder.Build(HttpStatusCode.Unauthorized));
            }

            var result = await this.signInManager.CheckPasswordSignInAsync(user, login.Password, false).ConfigureAwait(false);

            return !result.Succeeded
                ? this.Unauthorized(this.apiResponseBuilder.Build(HttpStatusCode.Unauthorized))
                : (ActionResult<UserDto>)this.Ok(new UserDto
                {
                    Email = user.Email,
                    Token = this.tokenService.CreateToken(user),
                    DisplayName = user.DisplayName,
                });
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto register)
        {
            if (register is null)
            {
                return this.BadRequest(this.apiResponseBuilder.Build(HttpStatusCode.BadRequest));
            }

            var user = new AppUser
            {
                DisplayName = register.DisplayName,
                Email = register.Email,
                UserName = register.Email,
            };

            var result = await this.userManager.CreateAsync(user, register.Password).ConfigureAwait(false);

            return !result.Succeeded
                ? this.BadRequest(this.apiResponseBuilder.Build(HttpStatusCode.BadRequest))
                : (ActionResult<UserDto>)this.Ok(new UserDto
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = this.tokenService.CreateToken(user),
                });
        }
    }
}
