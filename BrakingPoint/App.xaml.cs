using BrakingPoint.Data;

namespace BrakingPoint;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
	static Database connection;
	public static Database Connection
	{
		get
		{
			if (connection == null)
			{
                connection = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SqLiteSample.db"));
			}
			return connection;
		}
	}
}
