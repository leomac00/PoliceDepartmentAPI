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
    [Route("/api/v1/PoliceDepartments")]
    public class PDController : ControllerBase
    {//CRUD
        private readonly ApplicationDbContext database;
        public PDController(ApplicationDbContext db)
        {
            this.database = db;
        }
        //POST
        ///<summary>Register a new Police Department to the database based on BODY information.</summary>
        [Authorize(Roles = "Judge")]
        [HttpPost]
        public IActionResult Register([FromBody] PoliceDepartmentDTO policeDepartmentDTO)
        {
            try
            {
                Predicate<PoliceDepartment> PDChecks = p => p.Adress.Id == policeDepartmentDTO.AdressId;

                var PDs = database.PoliceDepartments
                .Include(item => item.Adress)
                .ToList();
                var PDExists = PDs.Any(item => PDChecks(item));

                if (!PDExists)
                {
                    var PD = new PoliceDepartment()
                    {
                        Adress = database.Adresses.First(item => item.Id == policeDepartmentDTO.AdressId
                        && item.Status),

                        PhoneNumber = policeDepartmentDTO.PhoneNumber,
                        Name = policeDepartmentDTO.Name,
                        Status = true,
                    };
                    database.PoliceDepartments.Add(PD);
                    database.SaveChanges();

                    Response.StatusCode = 201;
                    return new ObjectResult(new
                    {
                        Message = "Police Department registration to Database complete!",
                        newPD = new
                        {
                            Name = PD.Name,
                            PhoneNumber = PD.PhoneNumber,
                            Adress = PD.Adress
                        }
                    });
                }
                else
                {
                    var PD = PDs.First(item => PDChecks(item));
                    if (PD.Status == false)
                    {
                        PD.Status = true;
                        database.SaveChanges();
                        Response.StatusCode = 200;
                        return new ObjectResult(new
                        {
                            Message = "Police Department already exists, STATUS changed to active!",
                            PD = new
                            {
                                ID = PD.Id,
                                Name = PD.Name,
                                PhoneNumber = PD.PhoneNumber,
                                Adress = PD.Adress
                            }
                        });
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new
                        {
                            Message = "Police Department exists",
                            PD = new
                            {
                                ID = PD.Id,
                                Name = PD.Name,
                                PhoneNumber = PD.PhoneNumber,
                                Adress = PD.Adress
                            }
                        });

                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Msg = "An error occurred while registering the new Police Department.",
                    Error = e.Message
                });
            }
        }


        //GET
        ///<summary>Returns all PDs from database, or in case of given Id return corresponding Police Department.</summary>
        [Authorize(Roles = "Judge, Lawyer")]
        [HttpGet("{id?}")]
        public IActionResult Get(int id = 0)
        {
            try
            {
                var PDs = database.PoliceDepartments
                    .Include(item => item.Adress)
                    .Where(item => item.Status)
                    .ToList();

                if (id == 0)
                {
                    return Ok(PDs);
                }
                else
                {
                    var PD = PDs.Where(item => item.Id == id);
                    return Ok(PD);
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


        //PATCH
        ///<summary>Updates Police Department based on BODY information.</summary>
        [Authorize(Roles = "Judge")]
        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] PoliceDepartmentDTO policeDepartmentDTO, int id)
        {
            try
            {
                var PD = database.PoliceDepartments.Where(item => item.Status).First(item => item.Id == id);
                PD.Adress = database.Adresses.Find(policeDepartmentDTO.AdressId);
                PD.PhoneNumber = policeDepartmentDTO.PhoneNumber;
                PD.Name = policeDepartmentDTO.Name;

                database.SaveChanges();
                Response.StatusCode = 200;
                return new ObjectResult(new
                {
                    Message = "Police Department´s information updated!",
                    PD = new
                    {
                        Name = PD.Name,
                        PhoneNumber = PD.PhoneNumber,
                        Adress = PD.Adress
                    }
                });

            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Msg = "An error occurred while updating Police Department´s information.",
                    Error = e.Message
                });
            }
        }


        //DELETE
        ///<summary>Deletes Police Department based on ID.</summary>
        [Authorize(Roles = "Judge")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var PD = database.PoliceDepartments
                .Include(item => item.Adress)
                .First(item => item.Id == id && item.Status);

                PD.Status = false;

                database.SaveChanges();
                Response.StatusCode = 200;
                return new ObjectResult(new
                {
                    Message = "Police Department deleted!",
                    PD = new
                    {
                        Name = PD.Name,
                        PhoneNumber = PD.PhoneNumber,
                        Adress = PD.Adress
                    }
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Msg = "An error occurred while deleting Police Department´s information.",
                    Error = e.Message
                });
            }
        }
    }
}