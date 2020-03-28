using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using GamerHub.mobile.android.Views.Fragments.Base;
using GamerHub.mobile.core.ViewModels;
using GamerHub.mobile.core.ViewModels.CoreApp.Games;
using MvvmCross;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GamerHub.mobile.android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.main_frame_layout, true)]
    [Register("GamerHub.mobile.Android.Views.Fragments.FragmentGamesChosenForUser")]
    public class FragmentGamesChosenForUser : FragmentBase<GamesChosenForUserViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.Fragment_Games_Chosen_For_User, null);

            var recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.games_chosen_for_user_recycler_view);

            var layoutManager = new GridLayoutManager(Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity, 2);
            recyclerView.SetLayoutManager(layoutManager);

            SetFontForView<TextView>(view, Resource.Id.title_text_view);
            SetFontsForSharedMenuBar(view);

            return view;
        }
    }
}