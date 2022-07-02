using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.DTO;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ICartRepository cartRepo;



        public AccountController(UserManager<ApplicationUser> userManager, ICartRepository cartRepo, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.cartRepo = cartRepo;
        }



        [HttpPost]
        [Route("CustomerRegister")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            //save database
            ApplicationUser userModel = new ApplicationUser();
            userModel.Email = registerDto.Email;
            userModel.UserName = registerDto.UserName;

            IdentityResult result = await userManager.CreateAsync(userModel, registerDto.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(userModel, false);
                Cart Cart = new Cart();
                Cart.ApplicationUser = userModel;
                Cart.Id= Guid.NewGuid();
                cartRepo.Add(Cart);

                var creatingClaimsResult = await userManager.AddClaimsAsync(userModel, new List<Claim>
                 {
                        new Claim (ClaimTypes.NameIdentifier, userModel.UserName),
                        new Claim (ClaimTypes.Email, userModel.Email),
                        new Claim (ClaimTypes.Role, "Customer")
                 });

                if (!creatingClaimsResult.Succeeded)
                {
                    await userManager.DeleteAsync(userModel);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Sorry try again");
                }
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return BadRequest(ModelState);
            }
        }



        [HttpPost]
        [Route("AdminRegister")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> RegisterAdmin(RegisterDto registerDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            //save database
            ApplicationUser userModel = new ApplicationUser();
            userModel.Email = registerDto.Email;
            userModel.UserName = registerDto.UserName;

            IdentityResult result = await userManager.CreateAsync(userModel, registerDto.Password);
            if (result.Succeeded)
            {
                var creatingClaimsResult = await userManager.AddClaimsAsync(userModel, new List<Claim>
                 {
                        new Claim (ClaimTypes.NameIdentifier, userModel.UserName),
                        new Claim (ClaimTypes.Email, userModel.Email),
                        new Claim (ClaimTypes.Role, "Admin")
                 });

                if (!creatingClaimsResult.Succeeded)
                {
                    await userManager.DeleteAsync(userModel);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Sorry try again");
                }
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return BadRequest(ModelState);
            }
        }



        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<TokenDto>> Login(LoginDto loginDto)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            //check
            ApplicationUser userModel=await userManager.FindByEmailAsync(loginDto.Email);
            if (userModel != null)
            {
                var isLocked = await userManager.IsLockedOutAsync(userModel);
                if (isLocked)
                {
                    return Unauthorized("You're locked. Please try again later");
                }
                if (await userManager.CheckPasswordAsync(userModel, loginDto.Password) == true)
                {
                    var userClaims = await userManager.GetClaimsAsync(userModel);

                    return GenerateToken(userClaims.ToList());

                    //var claims = new List<Claim>();
                    //claims.Add(new Claim(ClaimTypes.NameIdentifier, userModel.Id));
                    //claims.Add(new Claim(ClaimTypes.Name, userModel.UserName));
                    //claims.Add(new Claim(ClaimTypes.Role, "customer"));
                    //var roles = await userManager.GetRolesAsync(userModel);
                    //foreach(var role in roles)
                    //{
                    //    claims.Add(new Claim(ClaimTypes.Role, role));
                    //}
                    //claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    ////////////////////////Token/////////////////////////
                    //var key = 
                    //    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecrtKey"]));
                    //var token = new JwtSecurityToken(
                    //    audience: configuration["JWT:ValidAudience"],
                    //    issuer: configuration["JWT:ValidIssuer"],
                    //    expires: DateTime.Now.AddHours(1),
                    //    claims: claims,
                    //    signingCredentials:new SigningCredentials(key,SecurityAlgorithms.HmacSha256)
                    //    );
                    //return Ok(new
                    //{
                    //    token=new JwtSecurityTokenHandler().WriteToken(token) ,
                    //    expiration=token.ValidTo
                    //});

                }
                else
                {
                    await userManager.AccessFailedAsync(userModel);
                    return Unauthorized("Password or Email Wrong");
                }
            }
            return Unauthorized("Email or password are wrong");



        }
        #region Helpers
        private TokenDto GenerateToken(List<Claim> userClaims)
        {
            #region Getting Secret Key ready
            var secretKey = configuration["JWT:SecrtKey"];
            var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKey);
            var key = new SymmetricSecurityKey(secretKeyInBytes);
            #endregion

            #region Combining Secret Key with Hashing Algorithm
            var methodUsedInGeneratingToken = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            #endregion

            #region Putting all together (Define the token)
            var jwt = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.Now.AddDays(30),
                notBefore: DateTime.Now,
                audience: configuration["JWT:ValidAudience"],
                issuer: configuration["JWT:ValidIssuer"],
                signingCredentials: methodUsedInGeneratingToken);
            #endregion

            #region Generate the defined Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var resultToken = tokenHandler.WriteToken(jwt);
            #endregion

            return new TokenDto
            {
                Token = resultToken,
                ExpiryDate = jwt.ValidTo
            };
        }
        #endregion


    }
}
