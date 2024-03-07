﻿using BookingApp.Domain.RepositoryInterfaces.Guest1;
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
using System.Windows.Shapes;

namespace BookingApp.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for NewForumThreadView.xaml
    /// </summary>
    public partial class NewForumThreadView : Window,ICloseable
    {
        public NewForumThreadView(int userId)
        {
            InitializeComponent();
            this.DataContext = new NewForumThreadViewModel(userId);
        }
    }
}
