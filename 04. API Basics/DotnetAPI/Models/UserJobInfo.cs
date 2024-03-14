namespace DotnetAPI
{
    public partial class UserJobInfo
    /* We use partial in case we want to add to this class from another file 
    It is mostly a good practice */
    {
        public int UserId { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }

        public UserJobInfo()
        {
            if (JobTitle == null)
            {
                JobTitle = "";
            }
            if (Department == null)
            {
                Department = "";
            }
        }

    }
}