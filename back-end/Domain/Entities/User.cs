namespace back_end.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Password { get; set; }
        public bool IsEmailVerified { get; set; }
        public string? VerificationToken { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}
