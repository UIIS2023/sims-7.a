using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IForumCommentRepository
    {
        public ForumComment Save(ForumComment forumComment);
        public List<ForumComment> GetByThreadId(int id);
    }
}
