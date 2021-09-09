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
    public class AutopsiesController : ControllerBase
    {//CRUD
        private readonly ApplicationDbContext database;
        public AutopsiesController(ApplicationDbContext db)
        {
            this.database = db;
        }
        //POST
        ///<summary>Register a new Autopsy to the database based on BODY information.</summary>
        [Authorize(Roles = "Judge")]
        [HttpPost]
        public IActionResult Register([FromBody] AutopsyDTO autopsyDTO)
        {
            try
            {
                var autopsies = database.Autopsies.Include(item => item.Coroner).Include(item => item.Victim).ToList();

                if (!autopsies.Any(item => item.Victim.Id == autopsyDTO.VictimId && item.Description.Equals(autopsyDTO.Description)))
                {
                    var autopsy = new Autopsy()
                    {
                        Victim = database.Victims.Find(autopsyDTO.VictimId),
                        Coroner = database.Coroners.Find(autopsyDTO.CoronerId),
                        Date = Convert.ToDateTime(autopsyDTO.Date),
                        Description = autopsyDTO.Description,
                        Status = true,
                    };
                    database.Autopsies.Add(autopsy);
                    database.SaveChanges();

                    Response.StatusCode = 201;
                    return new ObjectResult(new { Message = "Autopsy registration to Database complete!", newAutopsy = new { Victim = autopsy.Victim, Coroner = autopsy.Coroner, Date = autopsy.Date, Description = autopsy.Description } });
                }
                else
                {
                    var autopsy = autopsies.First(item => item.Victim.Id == autopsyDTO.VictimId && item.Description.Equals(autopsyDTO.Description));
                    if (autopsy.Status == false)
                    {
                        autopsy.Status = true;
                        database.SaveChanges();
                        Response.StatusCode = 200;
                        return new ObjectResult(new { Message = "Autopsy already exists, STATUS changed to active!", Autopsy = new { Victim = autopsy.Victim, Coroner = autopsy.Coroner, Date = autopsy.Date, Description = autopsy.Description } });
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { Message = "Autopsy exists", Autopsy = new { Victim = autopsy.Victim, Coroner = autopsy.Coroner, Date = autopsy.Date, Description = autopsy.Description } });
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while registering the new Autopsy.", Error = e.Message });
            }
        }


        //GET
        ///<summary>Returns all autopsies from database, or in case of given Id return corresponding autopsy.</summary>
        [Authorize(Roles = "Judge, Lawyer")]
        [HttpGet("{id?}")]
        public IActionResult Get(int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var autopsies = database.Autopsies.Include(item => item.Coroner).Include(item => item.Victim).Where(item => item.Status).ToList();
                    return Ok(autopsies);
                }
                else
                {
                    var autopsy = database.Autopsies.Include(item => item.Coroner).Include(item => item.Victim).Where(item => item.Status && item.Id == id).ToList();
                    return Ok(autopsy);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while getting the information.", Error = e.Message });
            }
        }


        //PATCH
        ///<summary>Updates Autopsy based on BODY information.</summary>
        [Authorize(Roles = "Judge")]
        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] AutopsyDTO autopsyDTO, int id)
        {
            try
            {
                var autopsy = database.Autopsies.Include(item => item.Coroner).Include(item => item.Victim).Where(item => item.Status).First(item => item.Id == id);
                autopsy.Victim = database.Victims.Find(autopsyDTO.VictimId);
                autopsy.Coroner = database.Coroners.Find(autopsyDTO.CoronerId);
                autopsy.Date = Convert.ToDateTime(autopsyDTO.Date);
                autopsy.Description = autopsyDTO.Description;

                database.SaveChanges();
                Response.StatusCode = 200;
                return new ObjectResult(new { Message = "Autopsy´s information updated!", Autopsy = new { Victim = autopsy.Victim, Coroner = autopsy.Coroner, Date = autopsy.Date, Description = autopsy.Description } });

            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while updating the information.", Error = e.Message });
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
                var autopsy = database.Autopsies.Include(item => item.Coroner).Include(item => item.Victim).Where(item => item.Status).First(item => item.Id == id);
                autopsy.Status = false;

                database.SaveChanges();
                Response.StatusCode = 200;
                return new ObjectResult(new { Message = "Autopsy deleted!", Autopsy = new { Victim = autopsy.Victim, Coroner = autopsy.Coroner, Date = autopsy.Date, Description = autopsy.Description } });
            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while updating Autopsy´s information.", Error = e.Message });
            }
        }
    }
}
