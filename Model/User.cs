namespace TestRazor.Model
{
    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string OrdersBetId { get; set; }

        public string ItemsList { get; set; }
        public string HashPass { get; set; }
        public string Salt { get; set; }
    }
}
