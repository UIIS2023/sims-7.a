using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.Mutual
{
    public class ForumThreadRepository:IForumThreadRepository
    {
        private const string filePath = "../../../Resources/Data/forumThreads.csv";

        private readonly Serializer<ForumThread> serializer;
        private List<ForumThread> forumThreads;

        public ForumThreadRepository()
        {
            serializer = new Serializer<ForumThread>();
            forumThreads = serializer.FromCSV(filePath);   
        }

        public ForumThread Save(ForumThread thread)
        {
            thread.id = NextId();
            forumThreads = serializer.FromCSV(filePath);
            forumThreads.Add(thread);
            serializer.ToCSV(filePath, forumThreads);
            return thread;

        }

        public List<ForumThread> GetAll()
        {
            return serializer.FromCSV(filePath).ToList();
        }

        public ForumThread Update(ForumThread thread)
        {
            forumThreads = serializer.FromCSV(filePath);
            ForumThread current = forumThreads.Find(t => t.id == thread.id);
            int index = forumThreads.IndexOf(current);
            forumThreads.Remove(current);
            forumThreads.Insert(index,thread);
            serializer.ToCSV(filePath, forumThreads);
            return thread;

        }

        private int NextId()
        {
            forumThreads = serializer.FromCSV(filePath);
            if (forumThreads.Count < 1)
                return 1;
            return forumThreads.Max(x => x.id) + 1; 
        }


    }
}
