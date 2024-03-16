using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.DTOs;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserJobInfoEFController : ControllerBase
{
    DataContextEF _entityFramework;
    IMapper _mapper;
    public UserJobInfoEFController(IConfiguration config)
    {
        _entityFramework = new DataContextEF(config);

        _mapper = new Mapper(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<UserJobInfoToAddDTO, UserJobInfo>();
        }));
    }

    [HttpGet("GetUserJobInfo")]
    public IEnumerable<UserJobInfo> GetUserJobInfo()
    {
        IEnumerable<UserJobInfo> userJobInfos = _entityFramework.UserJobInfo.ToList<UserJobInfo>();
        return userJobInfos;
    }

    [HttpGet("GetSingleUserJobInfo/{userId}")]
    public UserJobInfo GetSingleUserJobInfo(int userId)
    {
        UserJobInfo? userJobInfo = _entityFramework.UserJobInfo
            .Where(u => u.UserId == userId)
            .FirstOrDefault<UserJobInfo>();

        if (userJobInfo != null)
        {
        return userJobInfo;
        }

        throw new Exception("Failed to get User Job Information");
    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
    {
        UserJobInfo? userJobInfoDb = _entityFramework.UserJobInfo
            .Where(u => u.UserId == userJobInfo.UserId)
            .FirstOrDefault<UserJobInfo>();

        if (userJobInfoDb != null)
        {
            userJobInfoDb.JobTitle = userJobInfo.JobTitle;
            userJobInfoDb.Department = userJobInfo.Department;
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }

            throw new Exception("Failed to update User Job Information");
        }

        throw new Exception("Failed to get User Job Information");
    }

    // [HttpPost("AddUserJobInfo")]
    // public IActionResult AddUserJobInfo(UserJobInfoToAddDTO userJobInfo)
    // {
    //     UserJobInfo userJobInfoDb = _mapper.Map<UserJobInfo>(userJobInfo);

    //     _entityFramework.Add(userJobInfoDb);
    //     if (_entityFramework.SaveChanges() > 0)
    //     {
    //         return Ok();
    //     }

    //     throw new Exception("Failed to add User Job Information");

    // }

    [HttpDelete("DeleteUserJobInfo/{UserId}")]
    public IActionResult DeletDeleteUserJobInfoeUser(int UserId)
    {
        UserJobInfo? userJobInfoDb = _entityFramework.UserJobInfo
            .Where(u => u.UserId == UserId)
            .FirstOrDefault<UserJobInfo>();

        if (userJobInfoDb != null)
        {
            _entityFramework.UserJobInfo.Remove(userJobInfoDb);
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }

            throw new Exception("Failed to update User Job Information");
        }

        throw new Exception("Failed to get User Job Information");
    }
}