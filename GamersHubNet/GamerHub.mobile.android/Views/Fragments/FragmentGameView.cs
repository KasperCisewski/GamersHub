using System.Text.RegularExpressions;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using GamerHub.mobile.android.Views.Fragments.Base;
using GamerHub.mobile.core.Models.Messenger;
using GamerHub.mobile.core.ViewModels;
using GamerHub.mobile.core.ViewModels.CoreApp.Game;
using MvvmCross;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Plugin.Messenger;

namespace GamerHub.mobile.android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.main_frame_layout, true)]
    [Register("GamerHub.mobile.Android.Views.Fragments.FragmentGameView")]
    public class FragmentGameView : FragmentBase<GameViewModel>
    {
        private MvxSubscriptionToken _openVideoCardToken;
        private int _displayWidth;
        private int _displayHeight;
        private View _view;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            _view = this.BindingInflate(Resource.Layout.Fragment_Game_View, null);

            SetFontForView<TextView>(_view, Resource.Id.release_date_text);
            SetFontForView<TextView>(_view, Resource.Id.game_category_text);
            SetFontForView<TextView>(_view, Resource.Id.description_text);
            SetFontForView<TextView>(_view, Resource.Id.game_title);
            SetFontForView<Button>(_view, Resource.Id.button_add_game_to_vault);
            SetFontForView<Button>(_view, Resource.Id.button_add_game_to_wish_list);
            SetFontForView<Button>(_view, Resource.Id.delete_game_from_wish_list);
            SetFontForView<Button>(_view, Resource.Id.delete_game_from_vault);
            SetFontForView<TextView>(_view, Resource.Id.button_show_screenshots_tab);
            SetFontForView<TextView>(_view, Resource.Id.button_show_video_tab);

            SetFontsForSharedMenuBar(_view);


            var recyclerView = _view.FindViewById<MvxRecyclerView>(Resource.Id.game_screenshot_recycler_view);

            var layoutManager = new GridLayoutManager(Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity, 2);
            recyclerView.SetLayoutManager(layoutManager);

            var metrics = Resources.DisplayMetrics;
            _displayWidth = (ConvertPixelsToDp(metrics.WidthPixels)) * 2;
            _displayHeight = (ConvertPixelsToDp(metrics.HeightPixels)) / (2);
            ConfigureEvents();

            return _view;
        }


        private void ConfigureEvents()
        {
            _openVideoCardToken = ViewModel.Messenger.Subscribe<OpenVideoCardView>(e =>
            {
                ConfigureYoutubeView();
            });
        }

        private void DisposeEvents()
        {
            ViewModel.Messenger.Unsubscribe<OpenVideoCardView>(_openVideoCardToken);
        }

        public override void OnResume()
        {
            if (!string.IsNullOrWhiteSpace(ViewModel.VideoUrl))
            {
                ConfigureYoutubeView();
            }
            base.OnResume();
        }

        public override void OnStop()
        {
            base.OnStop();
            DisposeEvents();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            DisposeEvents();
        }

        private void ConfigureYoutubeView()
        {
            var strUrl = ViewModel.VideoUrl;
            var id = GetVideoID(strUrl);

            if (!string.IsNullOrEmpty(id))
            {
                strUrl = $"http://youtube.com/embed/{id}";
            }

            var myWebView = _view.FindViewById<WebView>(Resource.Id.video_View);

            myWebView.SetWebChromeClient(new WebChromeClient());
            var settings = myWebView.Settings;
            settings.JavaScriptEnabled = true;
            settings.UseWideViewPort = true;
            settings.LoadWithOverviewMode = true;
            settings.JavaScriptCanOpenWindowsAutomatically = true;
            settings.DomStorageEnabled = true;
            settings.SetRenderPriority(WebSettings.RenderPriority.High);
            settings.BuiltInZoomControls = false;

            myWebView.SetWebChromeClient(new WebChromeClient());

            settings.AllowFileAccess = true;
            settings.SetPluginState(WebSettings.PluginState.On);
            var strYouTubeUrl = @"<html><body><iframe width=" + _displayWidth + " height=" + _displayHeight + " src=" + strUrl + " allowfullscreen=\"allowfullscreen\" mozallowfullscreen=\"mozallowfullscreen\" msallowfullscreen=\"msallowfullscreen\" oallowfullscreen=\"oallowfullscreen\" webkitallowfullscreen=\"webkitallowfullscreen\"></iframe></body></html>";

            myWebView.LoadDataWithBaseURL(null, strYouTubeUrl, "text/html", "UTF-8", null);

            myWebView.RefreshPlugins(true);
            myWebView.Reload();
        }

        private string GetVideoID(string strVideoUrl)
        {
            const string regExpPattern = @"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)";
            var regEx = new Regex(regExpPattern);
            var match = regEx.Match(strVideoUrl);
            return match.Success ? match.Groups[1].Value : null;
        }

        private int ConvertPixelsToDp(float pixelValue)
        {
            var dp = (int)((pixelValue) / Resources.DisplayMetrics.Density);
            return dp;
        }
    }
}