using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public /*  If you don't use a public constructor, the system will throw this exception:
    System.InvalidOperationException: A suitable constructor for 
    type 'DotnetAPI.Controllers.UserController' could not be located. */ 
        UserController() // This is the constructor
    {

    }

    // [HttpGet("test")]
    // // public IActionResult Test() 
    // // An IActionResult basically means an API response, so when we use an IActionResult as the response 
    // // from one of our controller methods it's pretty safe to return any piece of data that we would want to return
    // public string[] Test(string testValue /* We can create parameters 
    //                         to take arguments for our API, if created in this fashion:
    //                         <T> ArgumentName, it will be required, and can be accessed in the
    //                         URL as a query, using the ? question mark */) 
    // {
    //     string[] responseArray = new string[] {
    //         "test1",
    //         "test2"
    //     };
    //     return responseArray;
    // }

    // [HttpGet("test/{testValue}")]
    // public string[] Test(string testValue /* If we pass the value in the URL,
    //                             then it would be considered an explicit value or a path argument
    //                             instead of a query parameter */) 
    // {
    //     string[] responseArray = new string[] {
    //         "test1",
    //         "test2",
    //         testValue
    //     };
    //     return responseArray;
    // }

    [HttpGet("GetUsers/{testValue}")]
    public string[] GetUsers(string testValue /* If we pass the value in the URL,
                                then it would be considered an explicit value or a path argument
                                instead of a query parameter */) 
    {
        string[] responseArray = new string[] {
            "test1",
            "test2",
            testValue
        };
        return responseArray;
    }

}