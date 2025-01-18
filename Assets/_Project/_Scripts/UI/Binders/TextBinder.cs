using System;
using MVVM;
using TMPro;
using UniRx;

namespace UIControl.Binders
{
    public class TextBinder : IBinder , IObserver<string>
    {
        private readonly TextMeshProUGUI _view;
        private readonly IReadOnlyReactiveProperty<string> _property;
        private IDisposable _handle;

        public TextBinder(IReadOnlyReactiveProperty<string> property, TextMeshProUGUI view)
        {
            _property = property;
            _view = view;
        }

        public void Bind()
        {
            OnNext(_property.Value);
            _property.Subscribe(this);
        }

        public void Unbind()
        {
            _handle?.Dispose();
            _handle = null;
        }
        
        public void OnNext(string value)
        {
            _view.text = value; 
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }
    }
}