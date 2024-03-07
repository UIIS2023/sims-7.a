using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.WPF.Views.Guest1;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Controls;
using BookingApp.WPF.Views.Guest1.MainWinPages;
using BookingApp.Application.UseCases.Guest1;

namespace BookingApp.WPF.ViewModels.Guest1
{
    public class ForumViewModel:ObservableObject
    {
        private ForumThreadService forumThreadService;

        public List<ForumThread> forumThreads { get; set; }

        public RelayCommand NewThreadCommand { get; set; }
        public RelayCommand<ForumThread> OpenThreadCommand { get; set; }

        private Frame mainFrame;

        public bool ttEnabled { get; set; }
        private HelpService helpService;


        private int userId;

        public ForumViewModel(int userId,Frame mainFrame)
        {
            forumThreadService = new ForumThreadService();
            forumThreads = forumThreadService.GetAll();

            this.userId = userId;

            this.mainFrame = mainFrame;

            NewThreadCommand = new RelayCommand(() => { NewThread();});
            OpenThreadCommand = new RelayCommand<ForumThread>((thread) => { OpenThreadComments(thread); });

            helpService = new HelpService();
            ttEnabled = helpService.GetByUserId(userId).ttEnabled;

            mainFrame.ContentRendered += (s, e) => 
            {
                forumThreads = forumThreadService.GetAll();
                OnPropertyChanged(nameof(forumThreads));
            };
            
        }

        private void NewThread() 
        {
            var NewThreadWindow = new NewForumThreadView(userId);

            NewThreadWindow.Closed += (s, e) => {
                forumThreads = forumThreadService.GetAll();
                OnPropertyChanged("forumThreads");
            };

            NewThreadWindow.ShowDialog();
        }

        private void OpenThreadComments(ForumThread thread)
        {
            var ThreadCommentPage = new ForumCommentPage(thread,userId,mainFrame);
            mainFrame.Content = ThreadCommentPage;
        }

       

    }
}
