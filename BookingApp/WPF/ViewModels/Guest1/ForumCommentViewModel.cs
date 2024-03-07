using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.Views.Guest1;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using BookingApp.Application.UseCases.Guest1;
using System.ComponentModel;

namespace BookingApp.WPF.ViewModels.Guest1
{
    public class ForumCommentViewModel : ObservableObject
    {

        private ForumThreadService threadService;
        private ForumCommentService commentService;
        public List<ForumComment> forumComments { get; set; }
        public string threadName { get; set; }

        private ForumThread forumThread;
        private int userId;
        private Frame mainFrame;

        public RelayCommand NewCommentCommand { get; set; }
        public RelayCommand BackCommand { get; set; }
        public RelayCommand CloseThreadCommand { get; set; }

        public Visibility CloseThreadVisibility { get; set; }
        public Visibility AddCommentVisibility { get; set; }

        public bool ttEnabled { get; set; }
        private HelpService helpService;


        public ForumCommentViewModel(ForumThread forumThread, int userId, Frame mainFrame)
        {
            commentService = new ForumCommentService();
            threadService = new ForumThreadService();

            forumComments = commentService.GetByThreadId(forumThread.id);

            threadName = forumThread.threadName;

            this.forumThread = forumThread;
            this.userId = userId;
            this.mainFrame = mainFrame;

            helpService = new HelpService();
            ttEnabled = helpService.GetByUserId(userId).ttEnabled;

            CloseThreadVisibility = Visibility.Hidden;
            if (userId == forumThread.opUser.id && forumThread.isOpen) CloseThreadVisibility = Visibility.Visible;

            AddCommentVisibility = Visibility.Visible;
            if (!forumThread.isOpen) AddCommentVisibility = Visibility.Hidden;

            NewCommentCommand = new RelayCommand(() => { OpenCommentWindow(); });
            BackCommand = new RelayCommand(() => { GoBack(); });
            CloseThreadCommand = new RelayCommand(() => { CloseThread(); });



        }

        private void OpenCommentWindow()
        {
            var CommentWindow = new NewCommentWindow(forumThread, userId);

            CommentWindow.Closed += (s, e) =>
            {
                forumComments = commentService.GetByThreadId(forumThread.id);
                OnPropertyChanged("forumComments");
            };

            CommentWindow.ShowDialog();
        }

        private void GoBack()
        {
            mainFrame.GoBack();
        }

        private void CloseThread()
        {


            var result = MessageBox.Show("Confirm CLOSE thread", "Close thread!", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:

                    forumThread.isOpen = false;
                    AddCommentVisibility = Visibility.Hidden;
                    CloseThreadVisibility = Visibility.Hidden;
                    threadService.UpdateThread(forumThread);

                    OnPropertyChanged(nameof(AddCommentVisibility));
                    OnPropertyChanged(nameof(CloseThreadVisibility));

                    MessageBox.Show("Thread closed!");
                    break;
                case MessageBoxResult.No:
                    break;
            }

        }

        



    }
}
