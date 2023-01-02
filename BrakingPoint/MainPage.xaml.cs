namespace BrakingPoint;

public partial class MainPage : ContentPage
{
	

	public MainPage()
	{
		InitializeComponent();
    }

    private void Start_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Races());
    }

}

