using GamerHub.mobile.core.Models.LoginAndRegistration;
using GamerHub.mobile.core.ViewModels.Base;
using System.ComponentModel;
using System.Threading.Tasks;

namespace GamerHub.mobile.core.ViewModels.Registration
{
    public partial class RegistrationViewModel : BaseViewModel<LoginModel>
    {
        public RegistrationViewModel()
        {

        }

        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            IsValidForm = IsValidEmail && IsValidName && IsValidPassword && IsValidrepeatablePassword;
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

        public Task ValidName()
        {
            throw new NotImplementedException();
        }

        public Task ValidEmail()
        {
            throw new NotImplementedException();
        }

        public void ValidPassword()
        {
            throw new NotImplementedException();
        }

        public void ValidRepeatablePassword()
        {
            throw new NotImplementedException();
        }
    }
}
