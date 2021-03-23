 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARMDesktopUI.EventModels;
using Caliburn.Micro;

namespace ARMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<Object>, IHandle<LogOnEvent>
    {
        private SalesViewModel _salesVM;
        private IEventAggregator _eventAggregator;
        public ShellViewModel(IEventAggregator eventAggregator,SalesViewModel salesVM)
        {
            _salesVM = salesVM;
            _eventAggregator = eventAggregator;

            _eventAggregator.Subscribe(this);
            
            ActivateItem(IoC.Get <LoginViewModel>());
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
        }
    }
}