using ContosoUniversity.Comon.Interfaces;
using ContosoUniversity.DataAccess.Entity;
using ContosoUniversity.DataAccess.Interfaces;
using ContosoUniversity.Web.Helper;
using ContosoUniversity.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Web.Controllers
{
    public class AdminsController : Controller
    {
        private IAdminServices _adminServices;
        private Mapping _map;
        public AdminsController(IAdminServices adminServices,Mapping map)
        {
            _adminServices = adminServices;
            _map = map;
        }
        public ViewResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SignUp([Bind("UserID,FirstName,LastName,EmailAddress,Password")] AdminModel adminModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(adminModel.Password);

                    var adminEntity = _map.adminModelToAdmin(adminModel);
                    adminEntity.Password = hashedPassword;
                    await _adminServices.addAdmin(adminEntity);


                    return Json(new { success = true }); 
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, error = ex.Message });
                }

            }


            return Json(new { success = false, error = "Model validation failed" });
        }

        public ViewResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> LogIn([Bind("EmailAddress,Password")] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var admin = await _adminServices.GetAdminByEmail(loginModel.EmailAddress);
                    Console.WriteLine("pw",admin.Password);
                    Console.WriteLine(loginModel.Password);


                    if (admin != null &&  VerifyPassword(loginModel.Password,admin.Password)){
                        // genetate token
                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MyserectKey@12345678"));
                        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                        var tokenOptions = new JwtSecurityToken(
                            issuer: "https://localhost:5000",
                            audience: "https://localhost:5000",
                            claims: new List<Claim>() {
                        new Claim("role","admin"),new Claim("name",admin.FirstName)},
                            expires: DateTime.Now.AddMinutes(10),
                            signingCredentials: signinCredentials
                        );
                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                        return Json(new { success = true, token = tokenString });


                    }
                    else
                    {
                        Console.WriteLine("pw", admin.Password);
                        return Json(new { success = false, error = "login failed" });
                    }
                }catch(Exception ex)
                {
                    return Json(new { success = false, error = ex.Message });
                }
                
            }
            return Json(new { success = false, error = "Model validation failed" });
        }
        private bool VerifyPassword(string enteredPassword, string HashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, HashedPassword);


        }

    }
   
}
