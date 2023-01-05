using BrakingPoint.Models;
using Plugin.ValidationRules;
using Plugin.ValidationRules.Interfaces;
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
                    await App.Current.MainPage.DisplayAlert("Error!", "Incorrect username or password", "Ok");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error!", "Incorrect username or password", "Ok");
            }
            
        }

        private void OnRegisterCommand(object obj)
        {
            LoginModel lm = new LoginModel();
            lm.UserName = UserName;
            lm.Password = Password;
            lm.PasswordConf = PasswordConf;
            lm.Email= Email;
            if (lm.Email != null && lm.UserName != null && lm.Password != null && lm.PasswordConf != null)
            {

                if (!char.IsLetter(Password[0]))
                {
                    App.Current.MainPage.DisplayAlert("Error!", "Password's first character must be a letter.", "Ok");
                }

                if (!char.IsUpper(Password[0]))
                {
                    App.Current.MainPage.DisplayAlert("Error!", "Password's first letter must be Capitalize.", "Ok");
                }

                if (Password.Length < 8)
                {
                    App.Current.MainPage.DisplayAlert("Error!", "Password length must be 8 characters minimum.", "Ok");
                }

                if (!Password.Any(char.IsDigit))
                {
                    App.Current.MainPage.DisplayAlert("Error!", "Your password must contain numbers.", "Ok");
                }

                if (!Password.Any(char.IsSymbol) && !Password.Any(char.IsPunctuation))
                {
                    App.Current.MainPage.DisplayAlert("Error!", "Your password must contain symbols.", "Ok");
                }
                else
                {
                    if (lm.Password == lm.PasswordConf)
                    {
                        App.Connection.SaveLoginDataAsync(lm);
                        App.Current.MainPage.DisplayAlert("Success!", "Account creation was successful!", "Ok");
                        Navigation.PushModalAsync(new Login());
                    }
                    else
                    {
                        App.Current.MainPage.DisplayAlert("Error!", "Passwords must match!", "Ok");
                    }
                }
                
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Error!", "Fill in all fields!", "Ok");
            }
        }

    public class PasswordRule : IValidationRule<string>
        {
            public string ValidationMessage { get; set; }

            public bool Check(string value)
            {
                

                return true;
            }
        }
    }
}
