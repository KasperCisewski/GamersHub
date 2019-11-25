using System.Threading.Tasks;
using System.Windows.Input;
using GamerHub.mobile.core.ViewModels.Base;
using GamerHub.mobile.core.ViewModels.CoreApp.Games;
using GamerHub.mobile.core.ViewModels.CoreApp.Home;
using GamerHub.mobile.core.ViewModels.CoreApp.Profile;
using GamerHub.mobile.core.ViewModels.CoreApp.Search;
using MvvmCross.Commands;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Shared
{
    public class SharedMenuViewModel : BaseViewModel
    {

        public ICommand GoToHomeViewCommand => new MvxAsyncCommand(GoToHomeView);
        public ICommand GoToGamesViewCommand => new MvxAsyncCommand(GoToGamesView);
        public ICommand GoToSearchViewCommand => new MvxAsyncCommand(GoToSearchView);
        public ICommand GoToProfileViewCommand => new MvxAsyncCommand(GoToProfileView);
        private async Task GoToHomeView()
        {
            await ShowViewModelAndRemoveHistory<HomeViewModel>();
        }

        private async Task GoToGamesView()
        {
            await ShowViewModelAndRemoveHistory<GamesViewModel>();
        }

        private async Task GoToSearchView()
        {
            await ShowViewModelAndRemoveHistory<SearchViewModel>();
        }

        private async Task GoToProfileView()
        {
            await ShowViewModelAndRemoveHistory<ProfileViewModel>();
        }
    }
}
