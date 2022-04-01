﻿using System;
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
    public class EventSubmissionController : ControllerBase
    {
        private readonly IEventSubmissionRepository _eventSubmissionRepository;

        public EventSubmissionController(IEventSubmissionRepository eventSubmissionRepository)
        {
            _eventSubmissionRepository = eventSubmissionRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventSubmission eventSubmission)
        {
            try
            {
                var existEventSubmission = await _eventSubmissionRepository.Get(eventSubmission.EventId);
                if (existEventSubmission == null) throw new Exception();

                var result = await _eventSubmissionRepository.Create(eventSubmission);
                return Ok(new {id = result});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Event ID error!");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _eventSubmissionRepository.Get(id);
            if (result == null)
            {
                return NotFound();
            }

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _eventSubmissionRepository.Get();
            if (result == null)
            {
                return NotFound();
            }

            return new JsonResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, EventSubmission eventSubmission)
        {
            var exist = await _eventSubmissionRepository.Get(id);
            if (exist == null)
            {
                return NotFound();
            }

            var result = await _eventSubmissionRepository.Update(id, eventSubmission);
            return new JsonResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var exist = await _eventSubmissionRepository.Get(id);
            if (exist == null)
            {
                return NotFound();
            }

            var result = await _eventSubmissionRepository.Delete(id);
            return new JsonResult(result);
        }
    }
}