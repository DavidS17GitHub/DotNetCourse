using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.DTOs;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserSalaryEFController : ControllerBase
{
    DataContextEF _entityFramework;

    IMapper _mapper;
    public UserSalaryEFController(IConfiguration config)
    {
        _entityFramework = new DataContextEF(config);

        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserSalaryToAddDTO, UserSalary>();
        }));
    }

    [HttpGet("GetUsersSalaries")]
    public IEnumerable<UserSalary> GetUsersSalaries()
    {
        IEnumerable<UserSalary> usersSalaries = _entityFramework.UserSalary.ToList<UserSalary>();
        return usersSalaries;
    }

    [HttpGet("GetSingleUserSalary/{userId}")]
    public UserSalary GetSingleUserSalary(int userId)
    {
        UserSalary? userSalary = _entityFramework.UserSalary
            .Where(u => u.UserId == userId)
            .FirstOrDefault<UserSalary>();

        if (userSalary != null)
        {
            return userSalary;
        }

        throw new Exception("Failed to get User's Salary");
    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary userSalary)
    {
        UserSalary? userSalaryDb = _entityFramework.UserSalary
            .Where(u => u.UserId == userSalary.UserId)
            .FirstOrDefault<UserSalary>();

        if (userSalaryDb != null)
        {
            userSalaryDb.Salary = userSalary.Salary;
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }

            throw new Exception("Failed to update User's Salary");
        }

        throw new Exception("Failed to get User's Salary");
    }

    // [HttpPost("AddUserSalary")]
    // public IActionResult AddUserSalary(UserSalaryToAddDTO userSalary)
    // {
    //     UserSalary userSalaryDb = _mapper.Map<UserSalary>(userSalary);

    //     _entityFramework.Add(userSalaryDb);
    //     if (_entityFramework.SaveChanges() > 0)
    //     {
    //         return Ok();
    //     }

    //     throw new Exception("Failed to add User's Salary");
    // }

    [HttpDelete("DeleteUserSalary/{UserId}")]
    public IActionResult DeleteUserSalary(int UserId)
    {
        UserSalary? userSalaryDb = _entityFramework.UserSalary
            .Where(u => u.UserId == UserId)
            .FirstOrDefault<UserSalary>();

        if (userSalaryDb != null)
        {
            _entityFramework.UserSalary.Remove(userSalaryDb);
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }

            throw new Exception("Failed to update User's Salary");
        }

        throw new Exception("Failed to get User's Salary");
    }
}