using BookingApp.Application.UseCases.Guest1;
using BookingApp.Application.UseCases.Owner;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class ForumCommentService
    {
        private IForumCommentRepository commentRepository;
        private IUserRepository userRepository;
        private ForumThreadService threadService;
        private ReservationService reservationService;
        private AccommodationService accommodationService;

        public ForumCommentService()
        {
            commentRepository = Injector.Injector.CreateInstance<IForumCommentRepository>();
            userRepository = Injector.Injector.CreateInstance<IUserRepository>();
            reservationService = new ReservationService();
            threadService =  new ForumThreadService();
            accommodationService = new AccommodationService();
        }

        public List<ForumComment> GetByThreadId(int id)
        {
            var comments = commentRepository.GetByThreadId(id);
            UpdateThread(comments, id);
            UpdateUser(comments);

            return comments;

        }

        public ForumComment Save(ForumComment comment)
        {
            return commentRepository.Save(comment);
        }

        private void UpdateThread(List<ForumComment> comments,int id)
        {

            ForumThread thread = threadService.GetById(id);
            foreach(var c in comments)
            {
                c.forumThread = thread;
            }

        }

        private void UpdateUser(List<ForumComment> comments)
        {
            foreach(var c in comments)
            {
                foreach(var u in userRepository.GetAll())
                {
                    if (c.user.id == u.id) c.user = u;
                }
            }

        }

        public bool WasUserAtLocation(int userId,int locationId)
        {

            foreach(var reservation in reservationService.GetByUserId(userId))
            {
                if (reservation.accomodation.idLocation == locationId) return true;
            }

            return false;

        }

        public bool IsForumImportant(int threadId)
        {
            int userComments = CountCommentsForGuest(threadId);
            int ownerComments = CountCommentsForOwner(threadId);

            if (userComments >= 20 && ownerComments >= 10) return true;
            return false;
        }

       
        private int CountCommentsForGuest(int threadId)
        {
            int commentCnt = 0;
            foreach(var comment in GetByThreadId(threadId))
            { 
                if(comment.user.userType == USER_TYPE.Customer_Booking && WasUserAtLocation(comment.user.id, comment.forumThread.forLocation.idLocation)) commentCnt++;
            }
            return commentCnt;
        }

        private int CountCommentsForOwner(int threadId)
        {
            int commentCnt = 0;
            foreach(var comment in GetByThreadId(threadId))
            {
                if (comment.user.userType == USER_TYPE.Owner && accommodationService.isOwnerAccommodationAtLocation(comment.user.id, comment.forumThread.forLocation.idLocation)) commentCnt++;
            }
            return commentCnt;
        }

        

    }
}
