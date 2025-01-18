using MVVM;
using Zenject;

namespace UIControl.Binders
{
    public class BindersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BinderFactory.RegisterBinder<TextBinder>();
        }
    }
}