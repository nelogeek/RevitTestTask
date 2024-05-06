using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitTestTask
{
    public class MyDockablePaneViewModel : INotifyPropertyChanged
    {
        private string _documentTitle;

        public string DocumentTitle
        {
            get { return _documentTitle; }
            set
            {
                _documentTitle = value;
                OnPropertyChanged("DocumentTitle");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
