using Prism.Mvvm;
using Prism.Navigation;

namespace Yermolchev.Assignment.Core.Mvvm
{
    public abstract class ViewModelBase : BindableBase, IDestructible
    {
        protected ViewModelBase() { }

        public virtual void Destroy() { }
    }
}
