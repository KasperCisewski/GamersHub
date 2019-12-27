﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GamerHub.mobile.android.Views.Components;
using GamerHub.mobile.android.Views.Fragments.Base;
using GamerHub.mobile.core.Infrastructure;
using GamerHub.mobile.core.ViewModels.CoreApp.Games;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;

namespace GamerHub.mobile.android.Views.Fragments
{
    public class GamesCategoryView : FragmentBase<GamesCategoryViewModel>
    {
        private MvxRecyclerView _recyclerView;
        private RecyclerViewOnScrollListener _scrollListener;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.Fragment_Games_Category_List, null);

            return view;
        }

        public override void OnStart()
        {
            base.OnStart();
            _recyclerView = View.FindViewById<MvxRecyclerView>(Resource.Id.recycler_games_category_list_view);
            _scrollListener = new RecyclerViewOnScrollListener(StaticAppSettings.PullDataPageSize / 2);
            _scrollListener.LoadMoreEvent += _scrollListener_LoadMoreEvent;
            _recyclerView.AddOnScrollListener(_scrollListener);
            SearchForGames(true);
        }


        private void _scrollListener_LoadMoreEvent(object sender, EventArgs e)
        {
            SearchForGames(false);
        }

        private void SearchForGames(bool replace)
        {
            Activity.RunOnUiThread(async () =>
            {
                await ViewModel.SearchGames(replace);
                ViewModel.HideKeyboard();
            });
        }
    }
}