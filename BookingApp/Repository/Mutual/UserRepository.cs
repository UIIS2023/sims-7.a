using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.Design;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Repository.Mutual
{
    class UserRepository:IUserRepository
    {
        private const string filePath = "../../../Resources/Data/users.csv";

        private readonly Serializer<User> serializer;

        private List<User> users;

        public UserRepository()
        {
            serializer = new Serializer<User>();
            users = serializer.FromCSV(filePath);
        }

        public User GetByUsername(string username)
        {
            users = serializer.FromCSV(filePath);
            return users.FirstOrDefault(u => u.username == username);
        }

        public User GetUsernameById(int id)
        {
            users = serializer.FromCSV(filePath);
            return users.FirstOrDefault(u => u.id == id);
        }

        public List<User> GetAll()
        {
            return serializer.FromCSV(filePath);
        }
        public User Save(User user)
        {

            user.id = NextId();
            users = serializer.FromCSV(filePath);
            users.Add(user);
            serializer.ToCSV(filePath, users);
            return user;

        }

        public User Update(User user)
        {
            users = serializer.FromCSV(filePath);

            User current = users.Find(x => x.id == user.id);
            int index = users.IndexOf(current);
            users.Remove(current);

            users.Insert(index,user);
            serializer.ToCSV(filePath,users);

            return user;

        }

        private int NextId()
        {
            users = serializer.FromCSV(filePath);
            if (users.Count < 1)
            {
                return 1;
            }
            return users.Max(c => c.id) + 1;
        }

        public User GetById(int id)
        {
            users = serializer.FromCSV(filePath);
            return users.FirstOrDefault(u => u.id == id);
        }
    }
}
