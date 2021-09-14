using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using DesafioAPI.Data;
using DesafioAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace DesafioAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class UsersController : ControllerBase
    { //CRUD
        private readonly ApplicationDbContext database;
        public UsersController(ApplicationDbContext database)
        {
            this.database = database;
        }
        //POST
        ///<summary>Register a new user in database based on BODY information.</summary>
        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserDTO userDTO)
        {
            Predicate<User> userChecks = user =>
            user.CPF == userDTO.CPF
            || user.RegisterId == userDTO.RegisterId;

            var usersTable = database.Users.ToList();

            bool userExists = usersTable.Any(user => userChecks(user));

            try
            {
                if (!userExists)
                {
                    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
                    string userRole = UserDTO.GetRole(userDTO.UserRole);
                    var user = new User()
                    {
                        Name = userDTO.Name,
                        CPF = userDTO.CPF,
                        RegisterId = userDTO.RegisterId,
                        UserRole = userRole,
                        Password = hashedPassword,
                        Status = true,
                    };

                    database.Users.Add(user);
                    database.SaveChanges();

                    Response.StatusCode = 201;
                    return new ObjectResult(new
                    {
                        Message = "Registration complete!",
                        newUser = new
                        {
                            Name = user.Name,
                            RegisterId = user.RegisterId,
                            Role = user.UserRole
                        }
                    });
                }
                else
                {
                    var user = database.Users.First(user => userChecks(user));
                    if (user.Status)
                    {
                        return BadRequest(new
                        {
                            Msg = "User already exists in database.",
                            user = new
                            {
                                ID = user.Id,
                                Name = user.Name,
                                RegisterId = user.RegisterId,
                                Role = user.UserRole
                            }
                        });
                    }
                    else
                    {
                        user.Status = true;
                        database.SaveChanges();
                        Response.StatusCode = 200;
                        return new ObjectResult(new
                        {
                            Message = "User already exists, STATUS changed to active!",
                            user = new
                            {
                                ID = user.Id,
                                Name = user.Name,
                                RegisterId = user.RegisterId,
                                Role = user.UserRole
                            }
                        });
                    }

                }

            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Msg = "An error occured while registering new user.",
                    Error = e.Message
                });
            }
        }
        ///<summary>Logs a user in based on BODY information.</summary>
        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserCredentials userLogin)
        {
            User user;
            try
            {
                user = database.Users
                .Where(u => u.Status == true)
                .First(u => u.RegisterId == userLogin.RegisterId);

                if (user != null && user.Status == true)
                {
                    bool validPassword = BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password);
                    if (validPassword)
                    {
                        int hoursValid = 2;

                        //Encrypting security key
                        string securityKey = "Do you believe in life after love";
                        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
                        var encryptedCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

                        //Claims
                        var userClaims = new List<Claim>();

                        userClaims.Add(new Claim("id", user.Id.ToString()));
                        userClaims.Add(new Claim(ClaimTypes.Role, user.UserRole));


                        //Creating JWT
                        var JWT = new JwtSecurityToken(
                        issuer: "PoliceDepartment",
                        expires: DateTime.Now.AddHours(hoursValid),
                        audience: "common_user",
                        signingCredentials: encryptedCredentials,
                        claims: userClaims
                      );
                        var token = new JwtSecurityTokenHandler().WriteToken(JWT);
                        return Ok(new
                        {
                            Message = "Your token is valid for the next [ " + hoursValid + " ] Hour(s)",
                            Token = token
                        });
                    }
                    else
                    {
                        Response.StatusCode = 401;
                        return new ObjectResult(new { Message = "Incorrect Password." });
                    }
                }
                else
                {
                    Response.StatusCode = 401;
                    return new ObjectResult(new
                    {
                        Message = "User not found."
                    });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Msg = "An error occured while logging in.",
                    Error = e.Message
                });
            }
        }

        //GET
        ///<summary>Returns all police officers from database, or in case of given Id return corresponding police officer.</summary>
        [Authorize(Roles = "Judge, Lawyer")]
        [HttpGet("{id?}")]
        public IActionResult Get(int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var users = database.Users.Where(item => item.Status).ToList();
                    var usersInfo = new List<UserInfo>();
                    foreach (var item in users)
                    {
                        var userInfo = new UserInfo()
                        {
                            Name = item.Name,
                            CPF = item.CPF,
                            RegisterId = item.RegisterId,
                            UserRole = item.UserRole,
                        };
                        usersInfo.Add(userInfo);
                    }
                    return Ok(usersInfo);
                }
                else
                {
                    var user = database.Users.Where(item => item.Status).First(item => item.Id == id);
                    var userInfo = new UserInfo()
                    {
                        Name = user.Name,
                        CPF = user.CPF,
                        RegisterId = user.RegisterId,
                        UserRole = user.UserRole,
                    };
                    return Ok(userInfo);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Msg = "An error occurred while getting the information.",
                    Error = e.Message
                });
            }
        }

        //DELETE
        ///<summary>Deletes a user based on its RegisterId (UserName).</summary>
        [Authorize(Roles = "Judge")]
        [HttpDelete("Delete/{registerId}")]
        public IActionResult Delete(string registerId)
        {
            try
            {
                var user = database.Users
                .Where(u => u.Status == true)
                .First(u => u.RegisterId == registerId);

                user.Status = false;
                database.SaveChanges();
                return Ok(new { Msg = "User >>> " + user.Name + " <<< was deleted!" });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Msg = "An error occurred while deleting the requested user.",
                    Error = e.Message
                });
            }
        }
    }
}