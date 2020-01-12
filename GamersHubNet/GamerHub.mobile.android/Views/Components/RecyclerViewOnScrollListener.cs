using System;
using Android.Support.V7.Widget;
using GamerHub.mobile.core.Infrastructure;

namespace GamerHub.mobile.android.Views.Components
{
    public class RecyclerViewOnScrollListener : RecyclerView.OnScrollListener
    {
        private readonly int _loadWhenNItemsToTheEnd;

        public delegate void LoadMoreEventHandler(object sender, EventArgs e);
        public event LoadMoreEventHandler LoadMoreEvent;

        public RecyclerViewOnScrollListener(int loadWhenNItemsToTheEnd)
        {
            _loadWhenNItemsToTheEnd = loadWhenNItemsToTheEnd;
        }

        public RecyclerViewOnScrollListener()
        {
            _loadWhenNItemsToTheEnd = 0;
        }

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);

            var visibleItemCount = recyclerView.ChildCount;
            var totalItemCount = recyclerView.GetAdapter().ItemCount;
            var layoutManager = recyclerView.GetLayoutManager() as LinearLayoutManager;

            var pastVisiblesItems = layoutManager?.FindFirstVisibleItemPosition();

            if (visibleItemCount > 0 && totalItemCount % StaticAppSettings.PullDataPageSize > 0)
            {
                return;
            }

            if (visibleItemCount + pastVisiblesItems >= totalItemCount - _loadWhenNItemsToTheEnd)
            {
                LoadMoreEvent?.Invoke(this, null);
            }
        }
    }
}