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
    public class AdressesController : ControllerBase
    {
        private readonly ApplicationDbContext database;

        public AdressesController(ApplicationDbContext db)
        {
            this.database = db;
        }
        //POST
        ///<summary>Register a new Adress based on BODY information.</summary>
        [Authorize(Roles = "Judge")]
        [HttpPost]
        public IActionResult Register([FromBody] AdressDTO adressDTO)
        {
            try
            {
                var adressExists = database.Adresses.Any(a =>
                a.Street == adressDTO.Street
                && a.City == adressDTO.City
                && a.Number == adressDTO.Number
                && a.State == adressDTO.State
                && a.ZIPCode == adressDTO.ZIPCode);
                if (adressExists)
                {
                    var adress = database.Adresses.First(a =>
                  a.Street == adressDTO.Street
                    && a.City == adressDTO.City
                    && a.Number == adressDTO.Number
                    && a.State == adressDTO.State
                    && a.ZIPCode == adressDTO.ZIPCode);
                    if (adress.Status)
                    {
                        return Ok(new { Msg = "Adress already exists in database.", adress = new { ID = adress.Id, ZipCode = adress.ZIPCode, City = adress.City, State = adress.State, Street = adress.Street, Number = adress.Number } });
                    }
                    else
                    {
                        adress.Status = true;
                        database.SaveChanges();
                        return Ok(new { Message = "Adress already exists, STATUS changed to active!", adress = new { ID = adress.Id, ZipCode = adress.ZIPCode, City = adress.City, State = adress.State, Street = adress.Street, Number = adress.Number } });
                    }
                }
                else
                {
                    var adressToInsert = new Adress()
                    {
                        Street = adressDTO.Street,
                        Number = adressDTO.Number,
                        City = adressDTO.City,
                        State = adressDTO.State,
                        ZIPCode = adressDTO.ZIPCode,
                        Status = true,
                    };
                    database.Adresses.Add(adressToInsert);
                    database.SaveChanges();
                    Response.StatusCode = 201;
                    return new ObjectResult(new { Message = "Registration complete!", newAdress = new { ZipCode = adressToInsert.ZIPCode, City = adressToInsert.City, State = adressToInsert.State, Street = adressToInsert.Street, Number = adressToInsert.Number } });

                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Msg = "An error occured while registering.",
                    Error = e.Message
                });
            }
        }

        //GET
        ///<summary>Returns all adresses from database, or in case of given Id return corresponding adress.</summary>
        [Authorize(Roles = "Judge, Lawyer")]
        [HttpGet("{id?}")]
        public IActionResult Get(int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var Adresses = database.Adresses.Where(a => a.Status).ToList();
                    return Ok(Adresses);
                }
                else
                {
                    var Adress = database.Adresses.Where(a => a.Status && a.Id == id).ToList();
                    return Ok(Adress);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while getting the information.", Error = e.Message });
            }
        }


        //PATCH
        ///<summary>Updates Adress based on BODY information.</summary>
        [Authorize(Roles = "Judge")]
        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] AdressDTO adressDTO, int id)
        {
            try
            {
                var adress = database.Adresses.Where(a => a.Status).First(a => a.Id == id);
                adress.City = adressDTO.City;
                adress.State = adressDTO.State;
                adress.Street = adressDTO.Street;
                adress.Number = adressDTO.Number;
                adress.ZIPCode = adressDTO.ZIPCode;

                database.SaveChanges();
                Response.StatusCode = 200;
                return new ObjectResult(new { Message = "Adress´s information updated!", adress = new { ZipCode = adress.ZIPCode, City = adress.City, State = adress.State, Street = adress.Street, Number = adress.Number } });

            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while updating Address´s information.", Error = e.Message });
            }
        }


        //DELETE
        [Authorize(Roles = "Judge")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var adress = database.Adresses.Where(a => a.Status).First(a => a.Id == id);
                adress.Status = false;
                database.SaveChanges();
                return Ok(new { Msg = "Adress was deleted!", deletedAdress = new { ZipCode = adress.ZIPCode, City = adress.City, State = adress.State, Street = adress.Street, Number = adress.Number } });
            }
            catch (Exception e)
            {
                return BadRequest(new { Msg = "An error occurred while deleting Address´s information.", Error = e.Message });
            }
        }
    }
}