using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using GamerHub.mobile.android.Views.Fragments.Base;
using GamerHub.mobile.core.ViewModels;
using GamerHub.mobile.core.ViewModels.CoreApp.Home;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GamerHub.mobile.android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.main_frame_layout, true)]
    [Register("GamerHub.mobile.Android.Views.Fragments.FragmentHomeView")]
    public class FragmentHomeView : FragmentBase<HomeViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.Fragment_Home_View, null);


            var comingSoonRecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.coming_soon_games_recycler_view);
            comingSoonRecyclerView.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Horizontal, false));

            var brandNewRecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.brand_new_games_recycler_view);
            brandNewRecyclerView.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Horizontal, false));

            var hottestRecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.hottest_games_recycler_view);
            hottestRecyclerView.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Horizontal, false));

            var onSaleRecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.on_sale_games_games_recycler_view);
            onSaleRecyclerView.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Horizontal, false));
            
            SetFontForView<TextView>(view, Resource.Id.app_info_text_view);

            return view;
        }
    }
}