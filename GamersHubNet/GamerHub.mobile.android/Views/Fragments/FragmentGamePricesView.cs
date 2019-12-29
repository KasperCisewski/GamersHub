using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using GamerHub.mobile.android.Views.Fragments.Base;
using GamerHub.mobile.core.ViewModels;
using GamerHub.mobile.core.ViewModels.CoreApp.Game;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GamerHub.mobile.android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.main_frame_layout, true)]
    [Register("GamerHub.mobile.Android.Views.Fragments.FragmentGamePricesView")]
    public class FragmentGamePricesView : FragmentBase<GamePricesViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.Fragment_Game_Prices_View, null);

            var gamePricesRecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.game_prices_recycler_view);
            gamePricesRecyclerView.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Horizontal, false));

            //SetFontForView<TextView>(gamePricesRecyclerView, Resource.Id.game_prices_title_text_view);
            //SetFontForView<TextView>(gamePricesRecyclerView, Resource.Id.game_prices_description_text_view);
            //SetFontForView<TextView>(gamePricesRecyclerView, Resource.Id.game_prices_price_text_view);
            //SetFontForView<TextView>(gamePricesRecyclerView, Resource.Id.game_prices_shop_name_text_view);

            return view;
        }
    }
}