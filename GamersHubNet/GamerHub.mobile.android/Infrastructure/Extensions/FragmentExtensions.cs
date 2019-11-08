using System;
using System.Reactive;
using System.Reactive.Linq;
using GamerHub.mobile.android.Views.Fragments.Base;
using GamerHub.mobile.core.ViewModels.Base;

namespace GamerHub.mobile.android.Infrastructure.Extensions
{
    public static class FragmentExtensions
    {
        public static IObservable<EventPattern<T>> InitObservableFromEvent<T, K>(this FragmentBase<K> fragment, object target, string eventName) where K : BaseViewModel
        {
            return Observable.FromEventPattern<T>(target, eventName);
        }
    }
}