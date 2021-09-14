using System;
using System.Linq;
using DesafioAPI.Data;
using DesafioAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class PoliceOfficersController : ControllerBase
    {
        private readonly ApplicationDbContext database;
        public PoliceOfficersController(ApplicationDbContext db)
        {
            this.database = db;
        }
        //POST
        ///<summary>Register a new Police Officer to the database based on BODY information.</summary>
        [Authorize(Roles = "Judge")]
        [HttpPost]
        public IActionResult Register([FromBody] PoliceOfficerDTO officerDTO)
        {
            try
            {
                Predicate<PoliceOfficer> POChecks = p =>
                p.RegisterId.Equals(officerDTO.RegisterId)
                || p.CPF.Equals(officerDTO.CPF);

                var officers = database.PoliceOfficers.ToList();
                var officerExists = officers.Any(item => POChecks(item));

                if (!officerExists)
                {
                    string rank = PoliceOfficerDTO.GetRank(officerDTO.RankCode);
                    var officer = new PoliceOfficer()
                    {
                        Name = officerDTO.Name,
                        CPF = officerDTO.CPF,
                        Rank = rank,
                        RegisterId = officerDTO.RegisterId,
                        Status = true,
                    };
                    database.PoliceOfficers.Add(officer);
                    database.SaveChanges();

                    Response.StatusCode = 201;
                    return new ObjectResult(new
                    {
                        Message = "Police Officer registration to Database complete!",
                        newOfficer = new
                        {
                            Name = officer.Name,
                            RegisterID = officer.RegisterId,
                            Rank = officer.Rank
                        }
                    });
                }
                else
                {
                    var officer = officers.First(item => POChecks(item));
                    if (officer.Status == false)
                    {
                        officer.Status = true;
                        database.SaveChanges();
                        Response.StatusCode = 200;
                        return new ObjectResult(new
                        {
                            Message = "Police Officer already exists, STATUS changed to active!",
                            Officer = new
                            {
                                ID = officer.Id,
                                Name = officer.Name,
                                RegisterID = officer.RegisterId,
                                Rank = officer.Rank
                            }
                        });
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new
                        {
                            Message = "Police Officer exists",
                            Officer = new
                            {
                                ID = officer.Id,
                                Name = officer.Name,
                                RegisterID = officer.RegisterId,
                                Rank = officer.Rank
                            }
                        });
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Msg = "An error occurred while registering the new Officer.",
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
                    var officers = database.PoliceOfficers
                    .Where(item => item.Status)
                    .ToList();

                    return Ok(officers);
                }
                else
                {
                    var officer = database.PoliceOfficers
                    .Where(item => item.Status && item.Id == id)
                    .ToList();

                    return Ok(officer);
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

        ///<summary>Returns police officer from database based on name given.</summary>
        [Authorize(Roles = "Judge, Lawyer")]
        [HttpGet("name/{name}")]
        public IActionResult GetByName(string name)
        {
            try
            {
                var officer = database.PoliceOfficers
                .Where(item => item.Status && item.Name
                .Replace(" ", string.Empty)
                .Equals(name.Replace(" ", string.Empty), StringComparison.InvariantCultureIgnoreCase))
                .ToList();

                return Ok(officer);
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


        //PATCH
        ///<summary>Updates Police Officer based on BODY information.</summary>
        [Authorize(Roles = "Judge")]
        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] PoliceOfficerDTO officerDTO, int id)
        {
            try
            {
                string rank = PoliceOfficerDTO.GetRank(officerDTO.RankCode);
                var officer = database.PoliceOfficers
                .Where(item => item.Status)
                .First(item => item.Id == id);

                officer.RegisterId = officerDTO.RegisterId;
                officer.Name = officerDTO.Name;
                officer.CPF = officerDTO.CPF;
                officer.Rank = rank;

                database.SaveChanges();
                Response.StatusCode = 200;
                return new ObjectResult(new
                {
                    Message = "Police Officer´s information updated!",
                    Officer = new
                    {
                        Name = officer.Name,
                        RegisterID = officer.RegisterId,
                        Rank = officer.Rank
                    }
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Msg = "An error occurred while updating the information.",
                    Error = e.Message
                });
            }
        }


        //DELETE
        ///<summary>Deletes Police Officer based on ID.</summary>
        [Authorize(Roles = "Judge")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var officer = database.PoliceOfficers
                .First(item => item.Id == id && item.Status);
                officer.Status = false;

                database.SaveChanges();
                Response.StatusCode = 200;
                return new ObjectResult(new
                {
                    Message = "Police Officer deleted!",
                    Officer = new
                    {
                        Name = officer.Name,
                        RegisterID = officer.RegisterId,
                        Rank = officer.Rank
                    }
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Msg = "An error occurred while deleting information.",
                    Error = e.Message
                });
            }
        }
    }
}