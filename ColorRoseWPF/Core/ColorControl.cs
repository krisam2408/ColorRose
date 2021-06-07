using ColorRoseWPF.Core.Abstracts;
using System;
using System.Windows.Input;

namespace ColorRoseWPF.Core
{
    public class ColorControl : BaseNotifyPropertyChanged
    {
        public string Name { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        private int _value;
        public int Value
        {
            get { return _value; }
            set
            {
                if(value >= MinValue && value <= MaxValue)
                    if(SetValue(ref _value, value))
                        propertyChangedAction.Invoke();
            }
        }

        public ICommand UpCommand { get { return new RelayCommand(e => ValueChange(1)); } }
        public ICommand DownCommand { get { return new RelayCommand(e => ValueChange(-1)); } }

        private Action propertyChangedAction;

        public ColorControl(Action propertyChangedAction)
        {
            this.propertyChangedAction = propertyChangedAction;
        }

        private void ValueChange(int i)
        {
            Value += i;
        }
    }
}
