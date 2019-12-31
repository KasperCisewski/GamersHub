using System.Collections.Generic;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using GamerHub.mobile.android.Views.Fragments.Base;
using GamerHub.mobile.core.ViewModels;
using GamerHub.mobile.core.ViewModels.CoreApp.Game;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Plugin.Messenger;

namespace GamerHub.mobile.android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.main_frame_layout, true)]
    [Register("GamerHub.mobile.Android.Views.Fragments.FragmentGameView")]
    public class FragmentGameView : FragmentBase<GameViewModel>, ViewPager.IOnPageChangeListener
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.Fragment_Game_View, null);

            var pager = view.FindViewById<ViewPager>(Resource.Id.game_view_main_with_view_pager);

            if (pager != null)
            {
                var fragments = new List<MvxViewPagerFragmentInfo>
                {
                    new MvxViewPagerFragmentInfo(
                        "Screenshots",
                        "",
                        typeof(FragmentGameScreenshotsView),
                        ViewModel.GameScreenshotsViewModel
                    ),
                    new MvxViewPagerFragmentInfo(
                        "Video",
                        "",
                        typeof(FragmentGameVideoView),
                        ViewModel.GameVideoViewModel
                    ),
                    new MvxViewPagerFragmentInfo(
                        "Prices",
                        "",
                    typeof(FragmentGamePricesView),
                    ViewModel.GamePricesViewModel
                    )
                };
                pager.Adapter = new MvxFragmentPagerAdapter(Activity, ChildFragmentManager, fragments);
                pager.AddOnPageChangeListener(this);
            }

            return view;
        }

        public void OnPageScrollStateChanged(int state)
        {
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
        }

        public void OnPageSelected(int position)
        {
            if (position == 1)
            {
                 ViewModel.GameVideoViewModel.GetYoutubeVideo();
            }
        }
    }
}