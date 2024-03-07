using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IForumThreadRepository
    {
        public ForumThread Save(ForumThread thread);
        public List<ForumThread> GetAll();
        public ForumThread Update(ForumThread thread);
    }
}
