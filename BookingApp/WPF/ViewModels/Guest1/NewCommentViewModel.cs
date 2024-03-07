using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces.Guest1;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.Guest1
{
    public class NewCommentViewModel:ObservableObject, IDataErrorInfo
    {
        public string commentTitle { get; set; }
        public string comment { get; set; }

        private ForumCommentService commentService;
        private ForumThreadService threadService;

        public RelayCommand<ICloseable> AddCommentCommand { get; set; }
        public RelayCommand<ICloseable> CloseCommand { get; set; }

        private ForumThread forumThread;
        private int userId;

        public NewCommentViewModel(ForumThread forumThread,int userId)
        {
            commentService = new ForumCommentService();
            threadService = new ForumThreadService();
            commentTitle = "Re: " + forumThread.threadName;
            OnPropertyChanged("commentTitle");

            this.forumThread = forumThread;
            this.userId = userId;

            AddCommentCommand = new RelayCommand<ICloseable>((window) => { AddComment(window); });
            CloseCommand = new RelayCommand<ICloseable>((window) => { Close(window); });
        }

        private void AddComment(ICloseable window)
        {

            if(!string.IsNullOrEmpty(commentTitle) && !string.IsNullOrEmpty(comment))
            {
                var user = new User();
                user.id = userId;
                var comment = new ForumComment(forumThread,user, this.comment, DateTime.Now,commentTitle,commentService.WasUserAtLocation(userId,forumThread.forLocation.idLocation));
                commentService.Save(comment);

                CheckCommentCnt();

                window.Close();
            }
        }

        private void CheckCommentCnt()
        {

            if (commentService.IsForumImportant(forumThread.id))
            {
                forumThread.isImportant = true;
                threadService.UpdateThread(forumThread);
                
            }

        }

        private void Close(ICloseable window)
        {
            window.Close();
        }

        public string Error => null;


        public string this[string columnName]
        {
            get
            {
                string msg = null;
                switch (columnName)
                {
                    case "commentTitle":                                       
                        if (string.IsNullOrWhiteSpace(commentTitle))
                            msg = "Title cannot be empty or white space!";
                        break;
                    case "comment":
                        if (string.IsNullOrWhiteSpace(comment))
                            msg = "Comment cannot be empty or white space!";
                        break;
                }
                return msg;
            }
        }

    }
}
