using UniRx;

namespace UIControl.BaseMVVM
{
    public abstract class BaseModel
    {
        public readonly ReactiveProperty<int> Value = new ();
    }
}