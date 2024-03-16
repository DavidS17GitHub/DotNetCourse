namespace DotnetAPI.Models
{
    public partial class UserJobInfoToAddDTO
    {
        public string JobTitle { get; set; }
        public string Department { get; set; }

        public UserJobInfoToAddDTO()
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