using GamerHub.mobile.core.Models.LoginAndRegistration;
using GamerHub.mobile.core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
            IsValidForm = IsValidEmail && IsValidLogin && IsValidPassword && IsValidRepetablePassword;
        }
        public override void Prepare(LoginModel parameter)
        {
            if (parameter.UserEmailOrName.Contains("@"))
            {
                Email = parameter.UserEmailOrName;
            }
            else
            {
                Login = parameter.UserEmailOrName;
            }
        }
    }
}
