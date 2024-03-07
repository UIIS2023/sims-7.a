using BookingApp.Domain.Models;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Repository.Mutual
{
    public class ForumCommentRepository:IForumCommentRepository
    {

        private const string filePath = "../../../Resources/Data/forumComments.csv";

        private readonly Serializer<ForumComment> serializer;
        private List<ForumComment> forumComments;

        public ForumCommentRepository()
        {
            serializer = new Serializer<ForumComment>();
            forumComments = serializer.FromCSV(filePath); 
        }

        public ForumComment Save(ForumComment forumComment)
        {
            forumComment.id = NextId();
            forumComments = serializer.FromCSV(filePath);
            forumComments.Add(forumComment);
            serializer.ToCSV(filePath, forumComments);
            return forumComment;
        }

        public List<ForumComment> GetByThreadId(int id)
        {
            List<ForumComment> comments = new List<ForumComment>();
            forumComments = serializer.FromCSV(filePath);
            foreach(var c in forumComments)
            {
                if (c.forumThread.id == id) comments.Add(c);
            }
            return comments;
        }


        private int NextId() 
        {
            forumComments = serializer.FromCSV(filePath);
            if (forumComments.Count < 1)
                return 1;
            return forumComments.Max(x => x.id) + 1;
        }

    }
}
