using Android.Runtime;
using GamerHub.mobile.android.Views.Fragments.Base;
using GamerHub.mobile.core.ViewModels;
using GamerHub.mobile.core.ViewModels.CoreApp.Games;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GamerHub.mobile.android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.main_frame_layout, true)]
    [Register("GamerHub.mobile.Android.Views.Fragments.FragmentGamesView")]
    public class FragmentGamesView : FragmentBase<GamesViewModel>
    {
    }
}