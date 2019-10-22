using GamerHub.mobile.core.Services;
using MvvmCross.Base;
using MvvmCross.IoC;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace GamerHub.mobile.core.ViewModels.Base
{
    public abstract class BaseViewModel<T> : BaseViewModel, IMvxViewModel<T>
    {
        public abstract void Prepare(T parameter);
    }
    public abstract class BaseViewModel : MvxViewModel
    {
        [MvxInject]
        public IKeyboardService KeyboardService { get; set; }

        [MvxInject]
        public IViewHistoryService ViewHistoryService { get; set; }

        [MvxInject]
        public IGlobalStateService GlobalStateService { get; set; }

        [MvxInject]
        public IMvxMessenger Messenger { get; set; }

        [MvxInject]
        public IMvxMainThreadAsyncDispatcher MainThreadAsyncDispatcher { get; set; }

        [MvxInject]
        public IMvxNavigationService NavigationService { get; set; }
        public Action InitView { get; set; }

        protected BaseViewModel()
        {
            PropertyChanged += (sender, args) =>
            {
                OnPropertyChanged(sender, args);
            };
        }

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        public async Task ShowViewModel<T>() where T : BaseViewModel
        {
            await NavigationService.Navigate<T>();
        }

        public async Task ShowViewModel<T, TK>(TK param) where T : BaseViewModel<TK>
        {
            await NavigationService.Navigate<T, TK>(param);
        }

        public void HideKeyboard()
        {
            KeyboardService.HideKeyboard();
        }

        public async Task ShowViewModelAndRemoveHistory<T>() where T : BaseViewModel
        {
            ViewHistoryService.ClearHistory();
            await ShowViewModel<T>();
        }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            base.ViewDestroy(viewFinishing);
            PropertyChanged -= OnPropertyChanged;
        }
    }
}
