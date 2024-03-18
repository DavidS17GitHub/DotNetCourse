namespace DotnetAPI.DTOs
{
    partial class UserForLoginDTO
    {
        string Email {get; set;}
        string Password {get; set;}
        public UserForLoginDTO()
        {
            if ( Email == null)
            {
                Email = "";
            }
            if ( Password == null)
            {
                Password = "";
            }
        }
    }
}