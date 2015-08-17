using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MachineTagEditor.Infrastructure.Containers
{
    public class UnitAttributeContainer: INotifyPropertyChanged
    {
        public UnitAttributeContainer(string AttributeName = null,
                                      string AttributeText = null,
                                      bool LastRow = false)
        {
            this.AttributeName = AttributeName;
            this.AttributeText = AttributeText;
            this.isLastRow = LastRow;
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool _isLastRow = false;
        public bool isLastRow { 
            get
            {
                return _isLastRow;
            }
            set
            {
                if (_isLastRow != value)
                    NotifyPropertyChanged();

                _isLastRow = value;
            }
        }

        public string AttributeName { get; set; }
        public string AttributeText { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
