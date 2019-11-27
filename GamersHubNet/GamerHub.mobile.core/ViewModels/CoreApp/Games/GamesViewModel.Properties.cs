using System.Collections.ObjectModel;
using System.Windows.Input;
using GamerHub.mobile.core.Models;
using GamersHub.Shared.Contracts.Responses;
using MvvmCross.Commands;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Games
{
    public partial class GamesViewModel
    {
        private ObservableCollection<GameCategoryModel> _categoryList;

        public ObservableCollection<GameCategoryModel> CategoryList
        {
            get => _categoryList;
            set => SetProperty(ref _categoryList, value);
        }

        private ICommand _clickCategory;
        public ICommand ClickCategory => _clickCategory ?? (_clickCategory = new MvxAsyncCommand<GameCategoryModel>(OpenCategoryGames));

        private ICommand _clickGamesChosenForUser;

        public ICommand ClickGamesChosenForUser => _clickGamesChosenForUser ?? (_clickGamesChosenForUser = new MvxAsyncCommand(OpenGamesChosenForUser));
    }
}
