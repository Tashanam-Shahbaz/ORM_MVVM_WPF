﻿using ORM_MVVM_WPF.ViewModels;
using ORM_MVVM_WPF.ViewModels.Admin;
using ORM_MVVM_WPF.ViewModels.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ORM_MVVM_WPF.ViewModels.Seller;

namespace ORM_MVVM_WPF.Commands
{
    public class UpdateViewCommands : ICommand
    {
        private MainViewModel viewModel;
        // Dictionary to map parameter strings to ViewModel types
        Dictionary<string, Type> viewModelMappings = new Dictionary<string, Type>
        {
            { "login", typeof(LoginViewModel) },
            { "signup", typeof(SignupViewModel) },
            { "personalinfo", typeof(CustomerPersonalInfoViewModel) },
            { "customerviewitem", typeof(CustomerItemViewModel) },
            { "customervieworder", typeof(CustomerOrderViewModel) },
            { "admin_manage_order", typeof(AdminOrderViewModel) },
            { "admin_manage_item", typeof(AdminItemViewModel) },
            { "seller_manage_order", typeof(SellerOrderViewModel) },
            { "seller_manage_item", typeof(SellerItemViewModel) },
            { "admin_manage_seller", typeof(AdminSellerViewModel)}
        };
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
                string parameterString = parameter.ToString();

                if (viewModelMappings.ContainsKey(parameterString))
                {
                    Type viewModelType = viewModelMappings[parameterString];
                    viewModel.SelectedViewModel = (BaseViewModel)Activator.CreateInstance(viewModelType);
                }
                else
                {
                    // throw new ArgumentException("Unknown parameter: " + parameterString);
                }
            }
        }
    }
}
