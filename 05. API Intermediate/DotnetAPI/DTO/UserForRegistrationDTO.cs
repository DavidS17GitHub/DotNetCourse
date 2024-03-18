namespace DotnetAPI.DTOs
{
    partial class UserForRegistrationDTO
    {
        string Email {get; set;}
        string Password {get; set;}
        string PasswordConfirm {get; set;}
        public UserForRegistrationDTO()
        {
            if ( Email == null)
            {
                Email = "";
            }
            if ( Password == null)
            {
                Password = "";
            }
            if ( PasswordConfirm == null)
            {
                PasswordConfirm = "";
            }
        }
    }
}