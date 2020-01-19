using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GamerHub.mobile.android.Views.Fragments.Base;
using GamerHub.mobile.core.ViewModels;
using GamerHub.mobile.core.ViewModels.CoreApp.GamesVault;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GamerHub.mobile.android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.main_frame_layout, true)]
    [Register("GamerHub.mobile.Android.Views.Fragments.FragmentGamesVaultHeatMapView")]
    public class FragmentGamesVaultHeatMapView : FragmentBase<GamesVaultHeatMapViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.Fragment_Games_Vault_Heat_Map_View, null);

            SetFontForView<TextView>(view, Resource.Id.title_text_view);
            SetFontForView<TextView>(view, Resource.Id.description_text_view);

            return view;
        }
    }
}