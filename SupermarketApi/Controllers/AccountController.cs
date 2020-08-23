namespace SupermarketApi.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SupermarketApi.Dtos;
    using SupermarketApi.Entities.Identity;
    using SupermarketApi.Errors;
    using SupermarketApi.Mapping;

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IBuilder<HttpStatusCode, ApiResponse> apiResponseBuilder;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IBuilder<HttpStatusCode, ApiResponse> apiResponseBuilder)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.apiResponseBuilder = apiResponseBuilder;
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
                    Token = "This will be a token",
                    DisplayName = user.DisplayName,
                });
        }
    }
}
