using BrakingPoint.ViewModel;

namespace BrakingPoint;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
		this.BindingContext = new LoginViewModel(this.Navigation);
	}
}