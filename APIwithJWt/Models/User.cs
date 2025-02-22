﻿

namespace APIwithJWt.Models
{
    public class User
    {
        public int ID { get; set; }

        public string Fullname { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
