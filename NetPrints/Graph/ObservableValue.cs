using PropertyChanged;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace NetPrints.Graph
{
    [DataContract]
    public class ObservableValue<T> : INotifyPropertyChanged
    {
        public delegate void ObservableValueChangedEventHandler(object sender, EventArgs eventArgs);

        [DataMember]
        [DoNotNotify]
        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                OnValueChanged?.Invoke(this, EventArgs.Empty);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        private T value;

        public ObservableValue(T value)
        {
            this.value = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event ObservableValueChangedEventHandler OnValueChanged;

        public static implicit operator T (ObservableValue<T> observableValue)
        {
            return observableValue.Value;
        }

        public static implicit operator ObservableValue<T>(T value)
        {
            return new ObservableValue<T>(value);
        }
    }
}
