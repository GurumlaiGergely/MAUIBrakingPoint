namespace BrakingPoint;

public partial class MainPage : ContentPage
{
	

	public MainPage()
	{
		InitializeComponent();
    }

    private void Login_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Login());
    }

    private void register_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Registration());
    }
}

