namespace DotnetAPI
{
    public partial class User
    /* We use partial in case we want to add to this class from another file 
    It is mostly a good practice */
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public bool Active { get; set; }

        public User()
        {
            if (FirstName == null)
            {
                FirstName = "";
            }
            if (LastName == null)
            {
                LastName = "";
            }
            if (Email == null)
            {
                Email = "";
            }
            if (Gender == null)
            {
                Gender = "";
            }
        }

    }
}