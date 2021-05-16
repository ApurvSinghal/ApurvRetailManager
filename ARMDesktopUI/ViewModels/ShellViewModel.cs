 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARMDesktopUI.EventModels;
using ARMDesktopUI.Library.Api;
using ARMDesktopUI.Library.Models;
using Caliburn.Micro;

namespace ARMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<Object>, IHandle<LogOnEvent>
    {
        private SalesViewModel _salesVM;
        private IEventAggregator _eventAggregator;
        private ILoggedInUserModel _loggedInUserModel;
        private IAPIHelper _aPIHelper;
        public ShellViewModel(IEventAggregator eventAggregator,SalesViewModel salesVM,
            ILoggedInUserModel loggedInUserModel, IAPIHelper aPIHelper)
        {
            _salesVM = salesVM;
            _eventAggregator = eventAggregator;
            _loggedInUserModel = loggedInUserModel;
            _aPIHelper = aPIHelper;

            _eventAggregator.Subscribe(this);
            
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
            NotifyOfPropertyChange(() => IsLoggedIn); 
        }

        public void UserManagement()
        {
            ActivateItem(IoC.Get<UserDisplayViewModel>());
        }

        public void ExitApplication()
        {
            TryClose();
        }

        public void LogOut()
        {
            _loggedInUserModel.ResetUser();
            _aPIHelper.LogOffUser();
            ActivateItem(IoC.Get<LoginViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);

        }

        public bool IsLoggedIn
        {
            get
            {
                bool output = false;
                if (!string.IsNullOrEmpty(_loggedInUserModel.Token))
                { 
                    output = true;
                }
                return output;
            }
        }
    }
}