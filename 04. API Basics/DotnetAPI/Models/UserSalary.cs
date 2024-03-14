namespace DotnetAPI
{
    public partial class UserSalary
    /* We use partial in case we want to add to this class from another file 
    It is mostly a good practice */
    {
        public int UserId { get; set; }
        public decimal Salary { get; set; }

    }
}