using System;
using System.Collections.Generic;
using System.Linq;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using GamerHub.mobile.core.ViewModels.Base;
using GamersHub.Shared.Data.Enums;
using MvvmCross;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android;

namespace GamerHub.mobile.android.Views.Fragments.Base
{
    public class FragmentBase<T> : MvxFragment<T> where T : BaseViewModel
    {
        private string _gamerHubHackedFont = "HACKED.ttf";
        private string _gamersHubJuraFont = "JURA.ttf";

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

            var enumNamesToHackedFont = Enum.GetNames(typeof(GameCategory)).ToList();
            var namesToHackedFont = new List<string>()
                {"GamersHub", "Profile", "Settings", "Vault", "WishList", "Friends"};
            namesToHackedFont = namesToHackedFont.Concat(enumNamesToHackedFont).ToList();
            var tf = namesToHackedFont.Contains(view.Text) ? Typeface.CreateFromAsset(am, _gamerHubHackedFont) : Typeface.CreateFromAsset(am, _gamersHubJuraFont);

            view.SetTypeface(tf, TypefaceStyle.Normal);
        }
    }
}