using System;
using System.Linq;
using DesafioAPI.Data;
using DesafioAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioAPI.Controllers
{//CRUD
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class CrimesController : ControllerBase
    {
        private readonly ApplicationDbContext database;

        public CrimesController(ApplicationDbContext db)
        {
            this.database = db;
        }
        //POST
        ///<summary>Register a new Crime to the database based on BODY information.</summary>
        [Authorize(Roles = "Judge")]
        [HttpPost]
        public IActionResult Register([FromBody] CrimeDTO crimeDTO)
        {
            try
            {
                var crimes = database.Crimes.Include(item => item.Perpetrator).Include(item => item.Victim).Include(item => item.Adress).ToList();

                if (!crimes.Any(item => item.Perpetrator.Id == crimeDTO.PerpetratorId && item.Victim.Id == crimeDTO.VictimId && item.Description.Equals(crimeDTO.Description)))
                {
                    var crime = new Crime()
                    {
                        Perpetrator = database.Perpetrators.Find(crimeDTO.PerpetratorId),
                        Victim = database.Victims.Find(crimeDTO.VictimId),
                        Date = Convert.ToDateTime(crimeDTO.Date),
                        Description = crimeDTO.Description,
                        Adress = database.Adresses.Find(crimeDTO.AdressId),
                        Status = true,
                    };
                    database.Crimes.Add(crime);
                    database.SaveChanges();

                    Response.StatusCode = 201;
                    return new ObjectResult(new { Message = "Crime registration to Database complete!", newCrime = new { Perpetrator = crime.Perpetrator, Victim = crime.Victim, Date = crime.Date, Adress = crime.Adress } });
                }
                else
                {
                    var crime = crimes.First(item => item.Perpetrator.Id == crimeDTO.PerpetratorId && item.Victim.Id == crimeDTO.VictimId && item.Description.Equals(crimeDTO.Description));
                    if (crime.Status == false)
                    {
                        crime.Status = true;
                        database.SaveChanges();
                        Response.StatusCode = 200;
                        return new ObjectResult(new { Message = "Crime already exists, STATUS changed to active!", Crime = new { ID = crime.Id, Perpetrator = crime.Perpetrator, Victim = crime.Victim, Date = crime.Date, Adress = crime.Adress } });
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { Message = "Crime exists", Crime = new { ID = crime.Id, Perpetrator = crime.Perpetrator, Victim = crime.Victim, Date = crime.Date, Adress = crime.Adress } });
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while registering the new Crime.", Error = e.Message });
            }
        }


        //GET
        ///<summary>Returns all crimes from database, or in case of given Id return corresponding crime.</summary>
        [Authorize(Roles = "Judge, Lawyer")]
        [HttpGet("{id?}")]
        public IActionResult Get(int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var crimes = database.Crimes.Include(item => item.Adress).Include(item => item.Perpetrator).Include(item => item.Victim).Where(item => item.Status).ToList();
                    return Ok(crimes);
                }
                else
                {
                    var crime = database.Crimes.Include(item => item.Adress).Include(item => item.Perpetrator).Include(item => item.Victim).Where(item => item.Status && item.Id == id).ToList();
                    return Ok(crime);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while getting the information.", Error = e.Message });
            }
        }


        //PATCH
        ///<summary>Updates Crime based on BODY information.</summary>
        [Authorize(Roles = "Judge")]
        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] CrimeDTO crimeDTO, int id)
        {
            try
            {
                var crime = database.Crimes.Include(item => item.Perpetrator).Include(item => item.Victim).Include(item => item.Adress).Where(item => item.Status).First(item => item.Id == id);
                crime.Perpetrator = database.Perpetrators.Find(crimeDTO.PerpetratorId);
                crime.Victim = database.Victims.Find(crimeDTO.VictimId);
                crime.Date = Convert.ToDateTime(crimeDTO.Date);
                crime.Description = crimeDTO.Description;
                crime.Adress = database.Adresses.Find(crimeDTO.AdressId);

                database.SaveChanges();
                Response.StatusCode = 200;
                return new ObjectResult(new { Message = "Crime´s information updated!", Crime = new { ID = crime.Id, Perpetrator = crime.Perpetrator, Victim = crime.Victim, Date = crime.Date, Adress = crime.Adress } });

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
                var crime = database.Crimes.Include(item => item.Adress).Include(item => item.Perpetrator).Include(item => item.Victim).First(item => item.Id == id);
                crime.Status = false;

                database.SaveChanges();
                Response.StatusCode = 200;
                return new ObjectResult(new { Message = "Crime deleted!", Crime = new { ID = crime.Id, Perpetrator = crime.Perpetrator, Victim = crime.Victim, Date = crime.Date, Adress = crime.Adress } });
            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while updating Crime´s information.", Error = e.Message });
            }
        }
    }
}