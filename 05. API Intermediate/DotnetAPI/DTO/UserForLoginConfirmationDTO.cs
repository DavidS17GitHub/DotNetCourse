namespace DotnetAPI.DTOs
{
    partial class UserForLoginConfirmationDTO
    {
        byte[] PasswordHash {get; set;}
        byte[] PasswordSalt {get; set;}
        public UserForLoginConfirmationDTO()
        {
            if ( PasswordHash == null)
            {
                PasswordHash = new byte[0];
            }
            if ( PasswordSalt == null)
            {
                PasswordSalt = new byte[0];
            }
        }
    }
}