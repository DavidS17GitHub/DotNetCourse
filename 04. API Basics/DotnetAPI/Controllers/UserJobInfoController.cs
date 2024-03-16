using DotnetAPI.Data;
using DotnetAPI.DTOs;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserJobInfoController : ControllerBase
{
    DataContextDapper _dapper;
    public UserJobInfoController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetUsersJobInfo")]
    public IEnumerable<UserJobInfo> GetUsersJobInfo()
    {
        string sql = @"
            SELECT [UserId],
                [JobTitle],
                [Department]
            FROM TutorialAppSchema.UserJobInfo";
        IEnumerable<UserJobInfo> userJobInfos = _dapper.LoadData<UserJobInfo>(sql);
        return userJobInfos;
    }

    [HttpGet("GetSingleUserJobInfo/{userId}")]
    public UserJobInfo GetSingleUserJobInfo(int userId)
    {
        string sql = @$"
                SELECT [UserId],
                    [JobTitle],
                    [Department]
                FROM TutorialAppSchema.UserJobInfo
                WHERE UserId = '{userId}'";
        UserJobInfo userJobInfo = _dapper.LoadDataSingle<UserJobInfo>(sql);
        return userJobInfo;
    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
    {
        string sql = @$"
                UPDATE TutorialAppSchema.UserJobInfo
                SET [JobTitle] = '{userJobInfo.JobTitle}',
                    [Department] = '{userJobInfo.Department}'
                WHERE UserId = '{userJobInfo.UserId}'";
        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to update User Job Information");
    }

    // [HttpPost("AddUserJobInfo")]
    // public IActionResult AddUserJobInfo(UserJobInfoToAddDTO userJobInfo)
    // {
    //     string sql = @$"
    //         INSERT INTO TutorialAppSchema.UserJobInfo (
    //             [JobTitle],
    //             [Department]
    //         ) VALUES (
    //             '{userJobInfo.JobTitle}',
    //             '{userJobInfo.Department}'
    //         )
    //         ";
    //     if (_dapper.ExecuteSql(sql))
    //     {
    //         return Ok();
    //     }
    //     throw new Exception("Failed to add User");
    // }

    [HttpDelete("DeleteUserJobInfo/{UserId}")]
    public IActionResult DeletDeleteUserJobInfoeUser(int UserId)
    {
        string sql = @$"DELETE FROM TutorialAppSchema.UserJobInfo WHERE UserId = {UserId.ToString()}";
        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to delete User Job Information");
    }
}