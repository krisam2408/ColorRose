using System;

namespace ColorRoseWPF.Core
{
    public class ColorControl
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
                if (_value != value)
                {
                    _value = value;
                    propertyChangedAction.Invoke();
                }
            }
        }

        private Action propertyChangedAction;

        public ColorControl(Action propertyChangedAction)
        {
            this.propertyChangedAction = propertyChangedAction;
        }
    }
}
