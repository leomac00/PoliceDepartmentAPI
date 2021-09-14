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
    public class ArrestsController : ControllerBase
    {//GET, PATCH
        private readonly ApplicationDbContext database;
        public ArrestsController(ApplicationDbContext db)
        {
            this.database = db;
        }
        //POST
        ///<summary>Register a new Arrest to the database based on BODY information.</summary>
        [Authorize(Roles = "Judge")]
        [HttpPost]
        public IActionResult Register([FromBody] ArrestDTO arrestDTO)
        {
            try
            {
                Predicate<Arrest> arrestChecks = a =>
                a.Perpetrator.Id == arrestDTO.PerpetratorId
                && a.Crime.Id == arrestDTO.CrimeId
                && a.Officer.Id == arrestDTO.OfficerId
                && a.Deputy.Id == arrestDTO.DeputyId;

                var arrests = database.Arrests
                .Include(item => item.Officer)
                .Include(item => item.Perpetrator)
                .Include(item => item.Deputy)
                .Include(item => item.Crime)
                .ToList();

                var arrestExists = arrests.Any(a => arrestChecks(a));

                if (!arrestExists)
                {
                    var arrest = new Arrest()
                    {
                        Officer = database.PoliceOfficers
                        .Where(item => item.Status)
                        .First(item => item.Id == arrestDTO.OfficerId),

                        Deputy = database.Deputies
                        .Include(item => item.PoliceDepartment.Adress)
                        .Where(item => item.Status)
                        .First(item => item.Id == arrestDTO.DeputyId),

                        Crime = database.Crimes
                        .Include(item => item.Adress)
                        .Include(item => item.Victim)
                        .Where(item => item.Status)
                        .First(item => item.Id == arrestDTO.CrimeId),

                        Perpetrator = database.Perpetrators
                        .Where(item => item.Status)
                        .First(item => item.Id == arrestDTO.PerpetratorId),

                        Date = Convert.ToDateTime(arrestDTO.Date),

                        Status = true,
                    };
                    database.Arrests.Add(arrest);
                    database.SaveChanges();

                    Response.StatusCode = 201;
                    return new ObjectResult(new
                    {
                        Message = "Arrest registration to Database complete!",
                        newArrest = new
                        {
                            Officer = arrest.Officer,
                            Deputy = arrest.Deputy,
                            Crime = arrest.Crime,
                            Perpetrator = arrest.Perpetrator,
                            Date = arrest.Date
                        }
                    });
                }
                else
                {
                    var arrest = arrests.First(a => arrestChecks(a));

                    if (arrest.Status == false)
                    {
                        arrest.Status = true;
                        database.SaveChanges();
                        Response.StatusCode = 200;
                        return new ObjectResult(new
                        {
                            Message = "Arrest already exists, STATUS changed to active!",
                            Arrest = new
                            {
                                Officer = arrest.Officer,
                                Deputy = arrest.Deputy,
                                Crime = arrest.Crime,
                                Perpetrator = arrest.Perpetrator,
                                Date = arrest.Date
                            }
                        });
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new
                        {
                            Message = "Arrest exists",
                            Arrest = new
                            {
                                Officer = arrest.Officer,
                                Deputy = arrest.Deputy,
                                Crime = arrest.Crime,
                                Perpetrator = arrest.Perpetrator,
                                Date = arrest.Date
                            }
                        });
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Msg = "An error occurred while registering the new Arrest.",
                    Error = e.Message
                });
            }
        }


        //GET
        ///<summary>Returns all arrests from database, or in case of given Id return corresponding arrest.</summary>
        [Authorize(Roles = "Judge, Lawyer")]
        [HttpGet("{id?}")]
        public IActionResult Get(int id = 0)
        {
            try
            {
                var arrests = database.Arrests
                     .Include(item => item.Officer)
                     .Include(item => item.Perpetrator)
                     .Include(item => item.Deputy.PoliceDepartment.Adress)
                     .Include(item => item.Crime.Victim)
                     .Where(item => item.Status)
                     .ToList();

                if (id == 0)
                {
                    return Ok(arrests);
                }
                else
                {
                    var arrest = arrests.Where(item => item.Id == id);
                    return Ok(arrest);
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
        ///<summary>Updates Arrest based on BODY information.</summary>
        [Authorize(Roles = "Judge")]
        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] ArrestDTO arrestDTO, int id)
        {
            try
            {
                var arrest = database.Arrests
                  .Include(item => item.Officer)
                  .Include(item => item.Perpetrator)
                  .Include(item => item.Deputy)
                  .Include(item => item.Crime)
                  .Where(item => item.Status && item.Id == id)
                  .First(item => item.Id == id);


                arrest.Officer = database.PoliceOfficers
                .Where(item => item.Status)
                .First(item => item.Id == arrestDTO.OfficerId);

                arrest.Deputy = database.Deputies
                .Where(item => item.Status)
                .First(item => item.Id == arrestDTO.DeputyId);

                arrest.Crime = database.Crimes
                .Where(item => item.Status)
                .First(item => item.Id == arrestDTO.CrimeId);

                arrest.Perpetrator = database.Perpetrators
                .Where(item => item.Status)
                .First(item => item.Id == arrestDTO.PerpetratorId);

                arrest.Date = Convert.ToDateTime(arrestDTO.Date);

                database.SaveChanges();
                Response.StatusCode = 200;
                return new ObjectResult(new
                {
                    Message = "Arrest´s information updated!",
                    Arrest = new
                    {
                        Officer = arrest.Officer,
                        Deputy = arrest.Deputy,
                        Crime = arrest.Crime,
                        Perpetrator = arrest.Perpetrator,
                        Date = arrest.Date
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
        ///<summary>Deletes Crime based on ID.</summary>
        [Authorize(Roles = "Judge")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var arrest = database.Arrests
                    .Include(item => item.Officer)
                    .Include(item => item.Perpetrator)
                    .Include(item => item.Deputy)
                    .Include(item => item.Crime)
                    .Where(item => item.Status && item.Id == id)
                    .First(item => item.Id == id);

                arrest.Status = false;

                database.SaveChanges();
                Response.StatusCode = 200;
                return new ObjectResult(new
                {
                    Message = "Arrest deleted!",
                    Arrest = new
                    {
                        Officer = arrest.Officer,
                        Deputy = arrest.Deputy,
                        Crime = arrest.Crime,
                        Perpetrator = arrest.Perpetrator,
                        Date = arrest.Date
                    }
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Msg = "An error occurred while updating Autopsy´s information.",
                    Error = e.Message
                });
            }
        }
    }
}