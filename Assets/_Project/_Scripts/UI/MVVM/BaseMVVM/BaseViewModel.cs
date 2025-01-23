using System;
using UniRx;
using Zenject;

namespace UIControl.BaseMVVM
{
    public abstract class BaseViewModel : IInitializable,IDisposable
    {
        public readonly ReactiveProperty<int> ReactiveValue = new();
        
        private BaseModel _baseModel;

        protected BaseViewModel(BaseModel baseModel)
        {
            _baseModel = baseModel;
        }

        public void Initialize()
        {
            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        protected virtual void OnValueChanged(int value)
        {
            ReactiveValue.Value = value;
        }

    }
}