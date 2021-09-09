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
    public class PerpetratorsController : ControllerBase
    {//CRUD
        private readonly ApplicationDbContext database;
        public PerpetratorsController(ApplicationDbContext db)
        {
            this.database = db;
        }
        //POST
        ///<summary>Register a new Perpetrator to the database based on BODY information.</summary>
        [Authorize(Roles = "Judge")]
        [HttpPost]
        public IActionResult Register([FromBody] PerpetratorDTO PerpetratorDTO)
        {
            try
            {
                var perpetrators = database.Perpetrators.ToList();

                if (!perpetrators.Any(p => p.CPF == PerpetratorDTO.CPF))
                {
                    var perpetrator = new Perpetrator()
                    {
                        Name = PerpetratorDTO.Name,
                        CPF = PerpetratorDTO.CPF,
                        Status = true,
                    };
                    database.Perpetrators.Add(perpetrator);
                    database.SaveChanges();

                    Response.StatusCode = 201;
                    return new ObjectResult(new { Message = "Perpetrator registration to Database complete!", newPerpetrator = new { Name = perpetrator.Name, CPF = perpetrator.CPF } });
                }
                else
                {
                    var perpetrator = perpetrators.First(p => p.CPF == PerpetratorDTO.CPF);
                    if (perpetrator.Status == false)
                    {
                        perpetrator.Status = true;
                        database.SaveChanges();
                        Response.StatusCode = 200;
                        return new ObjectResult(new { Message = "Perpetrator already exists, STATUS changed to active!", perpetrator = new { ID = perpetrator.Id, Name = perpetrator.Name, CPF = perpetrator.CPF } });
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { Message = "Perpetrator already exists", perpetrator = new { ID = perpetrator.Id, Name = perpetrator.Name, CPF = perpetrator.CPF } });

                    }
                }

            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while registering the new Perpetrator.", Error = e.Message });
            }
        }


        //GET
        ///<summary>Returns all perpetrators from database, or in case of given Id return corresponding perpetrator.</summary>
        [Authorize(Roles = "Judge, Lawyer")]
        [HttpGet("{id?}")]
        public IActionResult Get(int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var perpetrators = database.Perpetrators.Where(p => p.Status).ToList();
                    return Ok(perpetrators);
                }
                else
                {
                    var perpetrator = database.Perpetrators.Where(p => p.Status && p.Id == id).ToList();
                    return Ok(perpetrator);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while getting the information.", Error = e.Message });
            }
        }


        //PATCH
        ///<summary>Updates Perpetrator based on BODY information.</summary>
        [Authorize(Roles = "Judge")]
        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] PerpetratorDTO perpetratorDTO, int id)
        {
            try
            {
                var perpetrator = database.Perpetrators.Where(a => a.Status).First(a => a.Id == id);
                perpetrator.Name = perpetratorDTO.Name;
                perpetrator.CPF = perpetratorDTO.CPF;

                database.SaveChanges();
                Response.StatusCode = 200;
                return new ObjectResult(new { Message = "Perpetrator´s information updated!", perpetrator = new { Name = perpetrator.Name, CPF = perpetrator.CPF } });

            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while updating perpetrator´s information.", Error = e.Message });
            }
        }


        //DELETE
        ///<summary>Deletes Perpetrator based on ID.</summary>
        [Authorize(Roles = "Judge")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var perpetrator = database.Perpetrators.Find(id);
                perpetrator.Status = false;

                database.SaveChanges();
                Response.StatusCode = 200;
                return new ObjectResult(new { Message = "Perpetrator deleted!", perpetrator = new { Name = perpetrator.Name, CPF = perpetrator.CPF } });
            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while deleting perpetrator.", Error = e.Message });
            }
        }

    }
}