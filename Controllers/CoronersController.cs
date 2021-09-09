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
    public class CoronersController : ControllerBase
    {//CRUD
        private readonly ApplicationDbContext database;
        public CoronersController(ApplicationDbContext db)
        {
            this.database = db;
        }
        //POST
        ///<summary>Register a new Coroner to the database based on BODY information.</summary>
        [Authorize(Roles = "Judge")]
        [HttpPost]
        public IActionResult Register([FromBody] CoronerDTO coronerDTO)
        {
            try
            {
                var coroners = database.Coroners.ToList();

                if (!coroners.Any(item => item.RegisterId == coronerDTO.RegisterId || item.CPF == coronerDTO.CPF))
                {
                    var coroner = new Coroner()
                    {
                        Name = coronerDTO.Name,
                        CPF = coronerDTO.CPF,
                        RegisterId = coronerDTO.RegisterId,
                        Status = true,
                    };
                    database.Coroners.Add(coroner);
                    database.SaveChanges();

                    Response.StatusCode = 201;
                    return new ObjectResult(new { Message = "Coroner registration to Database complete!", newCoroner = new { Name = coroner.Name, RegisterID = coroner.RegisterId } });
                }
                else
                {
                    var coroner = coroners.First(item => item.RegisterId.Equals(coronerDTO.RegisterId));
                    if (coroner.Status == false)
                    {
                        coroner.Status = true;
                        database.SaveChanges();
                        Response.StatusCode = 200;
                        return new ObjectResult(new { Message = "Coroner already exists, STATUS changed to active!", Coroner = new { ID = coroner.Id, Name = coroner.Name, RegisterID = coroner.RegisterId } });
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { Message = "Coroner exists", Coroner = new { ID = coroner.Id, Name = coroner.Name, RegisterID = coroner.RegisterId } });
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while registering the new Coroner.", Error = e.Message });
            }
        }


        //GET
        ///<summary>Returns all coroners from database, or in case of given Id return corresponding police coroner.</summary>
        [Authorize(Roles = "Judge, Lawyer")]
        [HttpGet("{id?}")]
        public IActionResult Get(int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var coroners = database.Coroners.Where(item => item.Status).ToList();
                    return Ok(coroners);
                }
                else
                {
                    var coroner = database.Coroners.Where(item => item.Status && item.Id == id).ToList();
                    return Ok(coroner);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while getting the information.", Error = e.Message });
            }
        }


        //PATCH
        ///<summary>Updates Coroner based on BODY information.</summary>
        [Authorize(Roles = "Judge")]
        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] CoronerDTO coronerDTO, int id)
        {
            try
            {
                var coroner = database.Coroners.Where(item => item.Status).First(item => item.Id == id);
                coroner.RegisterId = coronerDTO.RegisterId;
                coroner.Name = coronerDTO.Name;
                coroner.CPF = coronerDTO.CPF;

                database.SaveChanges();
                Response.StatusCode = 200;
                return new ObjectResult(new { Message = "Coroner´s information updated!", Coroner = new { Name = coroner.Name, RegisterID = coroner.RegisterId } });

            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while updating Coroner´s information.", Error = e.Message });
            }
        }


        //DELETE
        ///<summary>Deletes Coroner based on ID.</summary>
        [Authorize(Roles = "Judge")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var coroner = database.Coroners.First(item => item.Id == id);
                coroner.Status = false;

                database.SaveChanges();
                Response.StatusCode = 200;
                return new ObjectResult(new { Message = "Coroner deleted!", Coroner = new { Name = coroner.Name, RegisterID = coroner.RegisterId } });
            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while deleting Coroners´s information.", Error = e.Message });
            }
        }

    }
}