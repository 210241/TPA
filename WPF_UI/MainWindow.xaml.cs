﻿using System.Windows;
using AplicationLogic.Mapper;
using AplicationLogic.ViewModel;
using Logging;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Logger logger = new Logger();

            DataContext = new MainViewModel(
                new FileDialog(logger),
                logger,
                new Reflection.Reflection(logger),
                new NodeItemMapper()
            );
        }
    }
}