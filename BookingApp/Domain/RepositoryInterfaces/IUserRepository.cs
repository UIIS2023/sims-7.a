using System.Collections.Generic;
using BookingApp.Domain.Models;

namespace BookingApp.Domain.RepositoryInterfaces;

public interface IUserRepository
{
    public User GetByUsername(string username);
    public User GetUsernameById(int id);
    public List<User> GetAll();
    public User Save(User user);

    public User Update(User user);

    public User GetById(int id);

}