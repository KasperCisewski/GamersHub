using Android.Graphics;
using Android.Views;
using Android.Widget;
using GamerHub.mobile.core.ViewModels.Base;
using MvvmCross;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android;

namespace GamerHub.mobile.android.Views.Fragments.Base
{
    public class FragmentBase<T> : MvxFragment<T> where T : BaseViewModel
    {
        private string _gamerHubFont = "HACKED.ttf";

        public void SetFontsForSharedMenuBar(View view)
        {
            SetFontForView<TextView>(view, Resource.Id.shared_menu_home_text_view);
            SetFontForView<TextView>(view, Resource.Id.shared_menu_games_text_view);
            SetFontForView<TextView>(view, Resource.Id.shared_menu_search_text_view);
            SetFontForView<TextView>(view, Resource.Id.shared_menu_profile_text_view);
        }

        public void SetFontForView<V>(View v, int viewId) where V : TextView
        {
            var context = Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            var am = context.Assets;

            var view = v.FindViewById<V>(viewId) as TextView;

            var tf = Typeface.CreateFromAsset(am, _gamerHubFont);

            view.SetTypeface(tf, TypefaceStyle.Normal);
        }
    }
}