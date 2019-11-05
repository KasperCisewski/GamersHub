using GamerHub.mobile.core.Models.LoginAndRegistration;
using GamerHub.mobile.core.ViewModels.Base;
using System.ComponentModel;

namespace GamerHub.mobile.core.ViewModels.Registration
{
    public partial class RegistrationViewModel : BaseViewModel<LoginModel>
    {
        public RegistrationViewModel()
        {

        }

        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            IsValidForm = IsValidEmail && IsValidName && IsValidPassword && IsValidRepetablePassword;
        }
        public override void Prepare(LoginModel parameter)
        {
            if (parameter.UserEmailOrName.Contains("@"))
            {
                Email = parameter.UserEmailOrName;
            }
            else
            {
                Name = parameter.UserEmailOrName;
            }
        }
    }
}
