using System.Windows.Input;
using GamerHub.mobile.core.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Games
{
    public partial class GamesViewModel
    {
        private MvxObservableCollection<GameCategoryModel> _categoryList = new MvxObservableCollection<GameCategoryModel>();

        public MvxObservableCollection<GameCategoryModel> CategoryList
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
