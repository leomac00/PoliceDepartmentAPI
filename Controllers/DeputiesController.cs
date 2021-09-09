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
    public class DeputiesController : ControllerBase
    {//CRUD
        private readonly ApplicationDbContext database;
        public DeputiesController(ApplicationDbContext db)
        {
            this.database = db;
        }
        //POST
        ///<summary>Register a new Deputy to the database based on BODY information.</summary>
        [Authorize(Roles = "Judge")]
        [HttpPost]
        public IActionResult Register([FromBody] DeputyDTO deputyDTO)
        {
            try
            {
                var deputies = database.Deputies.Include(item => item.PoliceDepartment.Adress).ToList();

                if (!deputies.Any(item => item.RegisterId == deputyDTO.RegisterId || item.CPF == deputyDTO.CPF))
                {
                    string shift = DeputyDTO.GetShift(deputyDTO.ShiftCode);

                    var deputy = new Deputy()
                    {
                        Name = deputyDTO.Name,
                        CPF = deputyDTO.CPF,
                        PoliceDepartment = database.PoliceDepartments.Include(item => item.Adress).Where(item => item.Status).First(item => item.Id == deputyDTO.PoliceDepartmentId),
                        Shift = shift,
                        RegisterId = deputyDTO.RegisterId,
                        Status = true,
                    };
                    database.Deputies.Add(deputy);
                    database.SaveChanges();

                    Response.StatusCode = 201;
                    return new ObjectResult(new { Message = "Deputy registration to Database complete!", newDeputy = new { Name = deputy.Name, Shift = deputy.Shift, PoliceDepartment = deputy.PoliceDepartment } });
                }
                else
                {
                    var deputy = deputies.First(item => item.RegisterId.Equals(deputyDTO.RegisterId));
                    if (deputy.Status == false)
                    {
                        deputy.Status = true;
                        database.SaveChanges();
                        Response.StatusCode = 200;
                        return new ObjectResult(new { Message = "Deputy already exists, STATUS changed to active!", Deputy = new { ID = deputy.Id, Name = deputy.Name, Shift = deputy.Shift, PoliceDepartment = deputy.PoliceDepartment } });
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { Message = "Deputy exists", Deputy = new { ID = deputy.Id, Name = deputy.Name, Shift = deputy.Shift, PoliceDepartment = deputy.PoliceDepartment } });

                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while registering the new Deputy.", Error = e.Message });
            }
        }


        //GET
        ///<summary>Returns all deputies from database, or in case of given Id return corresponding deputy.</summary>
        [Authorize(Roles = "Judge, Lawyer")]
        [HttpGet("{id?}")]
        public IActionResult Get(int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var deputies = database.Deputies.Include(item => item.PoliceDepartment.Adress).Where(item => item.Status).ToList();
                    return Ok(deputies);
                }
                else
                {
                    var deputy = database.Deputies.Include(item => item.PoliceDepartment.Adress).Where(item => item.Status && item.Id == id).ToList();
                    return Ok(deputy);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while getting the information.", Error = e.Message });
            }
        }


        //PATCH
        ///<summary>Updates Deputy based on BODY information.</summary>
        [Authorize(Roles = "Judge")]
        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] DeputyDTO deputyDTO, int id)
        {
            try
            {
                string shift = DeputyDTO.GetShift(deputyDTO.ShiftCode);
                var deputy = database.Deputies.Where(item => item.Status).First(item => item.Id == id);
                deputy.Name = deputyDTO.Name;
                deputy.CPF = deputyDTO.CPF;
                deputy.PoliceDepartment = database.PoliceDepartments.Include(item => item.Adress).Where(item => item.Status).First(item => item.Id == deputyDTO.PoliceDepartmentId);
                deputy.Shift = shift;
                deputy.RegisterId = deputyDTO.RegisterId;

                database.SaveChanges();
                Response.StatusCode = 200;
                return new ObjectResult(new { Message = "Deputy´s information updated!", Deputy = new { Name = deputy.Name, Shift = deputy.Shift, PoliceDepartment = deputy.PoliceDepartment } });

            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while updating Deputy´s information.", Error = e.Message });
            }
        }


        //DELETE
        ///<summary>Deletes Deputy based on ID.</summary>
        [Authorize(Roles = "Judge")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var deputy = database.Deputies.Include(item => item.PoliceDepartment.Adress).First(item => item.Id == id);
                deputy.Status = false;

                database.SaveChanges();
                Response.StatusCode = 200;
                return new ObjectResult(new { Message = "Deputy deleted!", Deputy = new { Name = deputy.Name, Shift = deputy.Shift, PoliceDepartment = deputy.PoliceDepartment } });
            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while updating Deputy´s information.", Error = e.Message });
            }
        }

    }
}