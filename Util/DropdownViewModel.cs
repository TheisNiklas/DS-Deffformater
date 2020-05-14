using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Tribalwars.UI.DeffRequester.Util
{
    public class DropdownViewModel
    {
        public IList<int> DropdownRequestedDeffItems { get; }

        public DropdownViewModel()
        {
            DropdownRequestedDeffItems = new List<int>(Enumerable.Range(1, 15));
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
