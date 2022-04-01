using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HospitalController : ControllerBase
    {
        private readonly IHospitalRepository _hospitalRepository;

        public HospitalController(IHospitalRepository hospitalRepository)
        {
            _hospitalRepository = hospitalRepository;
        }
        

        [HttpPost]
        public async Task<IActionResult> Create(Hospital hospital)
        {
            var result = await _hospitalRepository.Create(hospital);
            return Ok(new {id = result});
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var hospital = await _hospitalRepository.Get(id);
                if (hospital == null)
                {
                    throw new Exception();
                }

                return Ok(hospital);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Hospital ID error!");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var hospital = await _hospitalRepository.Get();
                if (hospital == null)
                {
                    throw new Exception();
                }

                return Ok(hospital);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Hospital ID error!");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Hospital hospital)
        {
            try
            {
                var result = await _hospitalRepository.Update(id, hospital);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Hospital ID error!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _hospitalRepository.Delete(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Hospital ID error!");
            }
        }
    }
}