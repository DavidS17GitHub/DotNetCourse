using DotnetAPI.Data;
using DotnetAPI.DTOs;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserSalaryController : ControllerBase
{
    DataContextDapper _dapper;
    public UserSalaryController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetUsersSalaries")]
    public IEnumerable<UserSalary> GetUsersSalaries()
    {
        string sql = @$"
            SELECT [UserId],
                [Salary]
            FROM TutorialAppSchema.UserSalary";
        IEnumerable<UserSalary> usersSalaries = _dapper.LoadData<UserSalary>(sql);
        return usersSalaries;
    }

    [HttpGet("GetSingleUserSalary/{userId}")]
    public UserSalary GetSingleUserSalary(int userId)
    {
        string sql = @$"
            SELECT [UserId],
                [Salary]
            FROM TutorialAppSchema.UserSalary
            WHERE UserId = '{userId}'";
        UserSalary userSalary = _dapper.LoadDataSingle<UserSalary>(sql);
        return userSalary;
    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary userSalary)
    {
        string sql = @$"
            UPDATE TutorialAppSchema.UserSalary
                SET [Salary] = '{userSalary.Salary}'
                WHERE UserId = '{userSalary.UserId}'";
        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to update User's Salary");
    }

    // [HttpPost("AddUserSalary")]
    // public IActionResult AddUserSalary(UserSalaryToAddDTO userSalary)
    // {
    //     string sql = @$"
    //             INSERT INTO TutorialAppSchema.UserSalary (
    //                 [Salary]
    //             ) VALUES (
    //                 '{userSalary.Salary}'
    //             )";
    //     if (_dapper.ExecuteSql(sql))
    //     {
    //         return Ok();
    //     }

    //     throw new Exception("Failed to add User");

    // }

    [HttpDelete("DeleteUserSalary/{UserId}")]
    public IActionResult DeleteUserSalary(int UserId)
    {
        string sql = @$"DELETE FROM TutorialAppSchema.UserSalary WHERE UserId = {UserId.ToString()}";
        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to delete User's Salary");
    }
}