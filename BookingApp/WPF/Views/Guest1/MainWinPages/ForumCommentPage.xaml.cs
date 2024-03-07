﻿using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.Guest1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Views.Guest1.MainWinPages
{
    /// <summary>
    /// Interaction logic for ForumCommentPage.xaml
    /// </summary>
    public partial class ForumCommentPage : Page
    {
        public ForumCommentPage(ForumThread forumThread,int userId,Frame mainFrame)
        {
            InitializeComponent();
            this.DataContext = new ForumCommentViewModel(forumThread,userId,mainFrame);
        }
    }
}
