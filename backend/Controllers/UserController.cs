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
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                // Check wrong email or password.
                var existedUser = await _userRepository.GetByEmail(user.email);
                if (existedUser == null)
                    return BadRequest("Wrong email!");
                if (!_userRepository.CheckUserPassword(existedUser, user.password))
                    return BadRequest("Wrong password!");

                // Execute login.
                var token = _userRepository.Login(existedUser);

                return Ok(new {token});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Login error!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            try
            {
                var exist = await _userRepository.CheckUserEmail(user.email);
                if (exist) return BadRequest("User existed!");
                if (user.isAdmin)
                {
                    user.hospital_id = null;
                }

                var id = await _userRepository.Create(user);
                return Ok(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Create user error!");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var user = await _userRepository.Get(id);
                if (user == null)
                {
                    throw new Exception();
                }

                return Ok(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("User id error!");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var user = await _userRepository.Get();
                if (user == null)
                {
                    throw new Exception();
                }

                return Ok(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Get user error!");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, User user)
        {
            try
            {
                var exist = await _userRepository.Get(id);
                if (exist == null)
                {
                    throw new Exception();
                }

                var result = await _userRepository.Update(id, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("User id error!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var exist = await _userRepository.Get(id);
                if (exist == null)
                {
                    throw new Exception();
                }

                var result = await _userRepository.Delete(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("User id error!");
            }
        }
    }
}