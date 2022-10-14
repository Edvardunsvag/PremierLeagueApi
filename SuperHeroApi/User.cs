using System;
namespace SuperHeroApi
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Score { get; set; }
        public string Email { get; set; } = string.Empty;



        public User()
        {
        }
    }
}

