using System;
using System.Linq;
using DesafioAPI.Data;
using DesafioAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class VictimsController : ControllerBase
    {//CRUD
        private readonly ApplicationDbContext database;
        public VictimsController(ApplicationDbContext db)
        {
            this.database = db;
        }
        //POST
        ///<summary>Register a new Victim to the database based on BODY information.</summary>
        [Authorize(Roles = "Judge")]
        [HttpPost]
        public IActionResult Register([FromBody] VictimDTO victimDTO)
        {
            try
            {
                var victims = database.Victims.ToList();

                if (!victims.Any(v => v.CPF == victimDTO.CPF))
                {
                    var victim = new Victim()
                    {
                        Name = victimDTO.Name,
                        CPF = victimDTO.CPF,
                        Status = true,
                    };
                    database.Victims.Add(victim);
                    database.SaveChanges();

                    Response.StatusCode = 201;
                    return new ObjectResult(new { Message = "Victim registration to Database complete!", newVictim = new { Name = victim.Name, CPF = victim.CPF } });
                }
                else
                {
                    var victim = victims.First(v => v.CPF == victimDTO.CPF);
                    if (victim.Status == false)
                    {
                        victim.Status = true;
                        database.SaveChanges();
                        Response.StatusCode = 200;
                        return new ObjectResult(new { Message = "Victim already exists, STATUS changed to active!", victim = new { ID = victim.Id, Name = victim.Name, CPF = victim.CPF } });
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { Message = "Victim already exists", victim = new { ID = victim.Id, Name = victim.Name, CPF = victim.CPF } });

                    }
                }

            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while registering the new victim.", Error = e.Message });
            }
        }


        //GET
        ///<summary>Returns all victims from database, or in case of given Id return corresponding victim.</summary>
        [Authorize(Roles = "Judge, Lawyer")]
        [HttpGet("{id?}")]
        public IActionResult Get(int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var victims = database.Victims.Where(v => v.Status).ToList();
                    return Ok(victims);
                }
                else
                {
                    var victim = database.Victims.Where(v => v.Status && v.Id == id).ToList();
                    return Ok(victim);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while getting the information.", Error = e.Message });
            }
        }


        //PATCH
        ///<summary>Updates Victim based on BODY information.</summary>
        [Authorize(Roles = "Judge")]
        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] VictimDTO victimDTO, int id)
        {
            try
            {
                var victim = database.Victims.Where(a => a.Status).First(a => a.Id == id);
                victim.Name = victimDTO.Name;
                victim.CPF = victimDTO.CPF;

                database.SaveChanges();
                Response.StatusCode = 200;
                return new ObjectResult(new { Message = "Victim´s information updated!", victim = new { Name = victim.Name, CPF = victim.CPF } });

            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while updating victim´s information.", Error = e.Message });
            }
        }


        //DELETE
        ///<summary>Deletes Victim based on ID.</summary>
        [Authorize(Roles = "Judge")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var victim = database.Victims.Find(id);
                victim.Status = false;

                database.SaveChanges();
                Response.StatusCode = 200;
                return new ObjectResult(new { Message = "Victim deleted!", victim = new { Name = victim.Name, CPF = victim.CPF } });
            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while updating victim´s information.", Error = e.Message });
            }
        }
    }
}