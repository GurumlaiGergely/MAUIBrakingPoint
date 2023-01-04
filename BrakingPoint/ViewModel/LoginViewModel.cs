using BrakingPoint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BrakingPoint.ViewModel
{
    public class LoginViewModel
    {
        private string _username, _password, _passwordConf, _email;

        public string UserName { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }
        public string PasswordConf { get => _passwordConf; set => _passwordConf = value; }
        public string Email { get => _email; set => _email = value; }


        public ICommand RegisterCommand { private set; get; }

        public ICommand LoginCommand { private set; get; }

        private INavigation Navigation;
        public LoginViewModel(INavigation navigation)
        {
            RegisterCommand = new Command(OnRegisterCommand);
            LoginCommand = new Command(OnLoginCommand);
            Navigation = navigation;
        }

        private async void OnLoginCommand(object obj)
        {
            var loginData = await App.Connection.GetLoginDataAsync(UserName);
            if (loginData != null)
            {
                if(string.Equals(loginData.Password, Password))
                {
                    await Navigation.PushModalAsync(new Datas());
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Hiba!", "Hibás felhasználónév vagy jelszó", "Ok");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Hiba!", "Hibás felhasználónév vagy jelszó", "Ok");
            }
            
        }

        private void OnRegisterCommand(object obj)
        {
            LoginModel lm = new LoginModel();
            lm.UserName = UserName;
            lm.Password = Password;
            lm.PasswordConf = PasswordConf;
            lm.Email= Email;
            if (lm.Email == null || lm.UserName == null || lm.Password == null || lm.PasswordConf == null)
            {
                if (lm.Password == lm.PasswordConf)
                {
                    App.Connection.SaveLoginDataAsync(lm);
                    App.Current.MainPage.DisplayAlert("Siker!", "Sikeres regisztráció!", "Ok");
                    Navigation.PushModalAsync(new Login());
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Hiba!", "A jelszavak nem egyeznek!", "Ok");
                }
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Hiba!", "Kérem töltsön ki minden mezőt!", "Ok");
            }
        }
    }
}
