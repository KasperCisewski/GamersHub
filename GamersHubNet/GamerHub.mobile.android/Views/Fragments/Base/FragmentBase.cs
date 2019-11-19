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