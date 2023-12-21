﻿using ORM_MVVM_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ORM_MVVM_WPF.Commands
{
    public class UpdateViewCommands : ICommand
    {
        private MainViewModel viewModel;
        public UpdateViewCommands(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            if (CanExecuteChanged != null)
            {
                if (parameter.ToString() == "login")
                {
                    viewModel.SelectedViewModel = new LoginViewModel();
                }
                else if (parameter.ToString() == "signup")
                {
                    viewModel.SelectedViewModel = new SignupViewModel();
                }
            }
        }
    }
}
