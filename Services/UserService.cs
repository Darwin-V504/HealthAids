using HealthAids.Models;
using HealthAids.Structures;

namespace HealthAids.Services
{
    public class UserService
    {
        private readonly Structures.List<User> _users; // Usando nuestra estructura List
        private int _nextId = 1;

        public UserService()
        {
            _users = new Structures.List<User>();

            // Usuario de prueba
            _users.Add(new User
            {
                Id = _nextId++,
                Name = "Usuario Prueba",
                Email = "test@test.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                Phone = "123456789",
                CreatedAt = DateTime.Now
            });
        }

        public User? Register(string name, string email, string password, string phone)
        {
            // Verificar si ya existe
            var existing = _users.Find(u => u.Email == email);
            if (existing != null) return null;

            var user = new User
            {
                Id = _nextId++,
                Name = name,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Phone = phone,
                CreatedAt = DateTime.Now
            };

            _users.Add(user);
            return user;
        }

        public User? Login(string email, string password)
        {
            var user = _users.Find(u => u.Email == email);
            if (user == null) return null;

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return user;
        }

        public User? GetById(int id)
        {
            return _users.Find(u => u.Id == id);
        }

        public User? GetByEmail(string email)
        {
            return _users.Find(u => u.Email == email);
        }

        public Structures.List<User> GetAll()
        {
            return _users.GetAll();
        }
        public bool Update(int id, string name, string phone)
        {
            var user = _users.Find(u => u.Id == id);
            if (user == null) return false;

            var updatedUser = new User
            {
                Id = user.Id,
                Name = name,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Phone = phone,
                CreatedAt = user.CreatedAt
            };

            return _users.Update(u => u.Id == id, updatedUser);
        }

        public bool Delete(int id)
        {
            return _users.Remove(u => u.Id == id);
        }
    }
}