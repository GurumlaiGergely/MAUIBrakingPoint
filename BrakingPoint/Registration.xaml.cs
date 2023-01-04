using BrakingPoint.ViewModel;

namespace BrakingPoint;

public partial class Registration : ContentPage
{
	public Registration()
	{
		InitializeComponent();
        this.BindingContext = new LoginViewModel(this.Navigation);
    }

    
}