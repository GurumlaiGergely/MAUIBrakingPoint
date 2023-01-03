using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;

namespace BrakingPoint;

public partial class Races : ContentPage
{
    private List<string> driversList = new List<string>()
    {
        "Alexander Albon", "Fernando Alonso", "Valtteri Bottas", "Nyck deVries", "Pierre Gasly", "Lewis Hamilton", "Nico Hulkenberg", "Nicholas Latifi", "Charles Leclerc", "Kevin Magnussen", "Lando Norris", "Esteban Ocon", "Sergio Perez", "Daniel Riccardo", "George Russell", "Carlos Sainz", "Mick Schumacher", "Lance Stroll", "Yuki Tsunoda", "Max Verstappen", "Sebastian Vettel", "Guanyu Zhou" 
    };
    public Races()
    {
        InitializeComponent();
        driverPicker.ItemsSource = driversList;
    }



    public class Rootobject
    {
        public Mrdata MRData { get; set; }
    }

    public class Mrdata
    {
        public string xmlns { get; set; }
        public string series { get; set; }
        public string url { get; set; }
        public string limit { get; set; }
        public string offset { get; set; }
        public string total { get; set; }
        public Drivertable DriverTable { get; set; }
    }

    public class Drivertable
    {
        public string season { get; set; }
        public string driverId { get; set; }
        public Driver[] Drivers { get; set; }
    }

    public class Driver
    {
        public string driverId { get; set; }
        public string permanentNumber { get; set; }
        public string code { get; set; }
        public string url { get; set; }
        public string givenName { get; set; }
        public string familyName { get; set; }
        public string dateOfBirth { get; set; }
        public string nationality { get; set; }
    }



    private void races_Clicked(object sender, EventArgs e)
    {

    }

    private async void driverPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        string picked = "";

        if(driverPicker.SelectedItem.ToString() == "Kevin Magnussen")
            picked = "kevin_magnussen";
        else if (driverPicker.SelectedItem.ToString() == "Nyck deVries")
            picked = "de_vries";
        else if (driverPicker.SelectedItem.ToString() == "Mick Schumacher")
            picked = "mick_schumacher";
        else if (driverPicker.SelectedItem.ToString() == "Max Verstappen")
            picked = "max_verstappen";
        else
            picked = driverPicker.SelectedItem.ToString().Split(' ')[1].ToLower();

        HttpClient client = new HttpClient();
        var text = await client.GetStringAsync($"https://ergast.com/api/f1/2022/drivers/{picked}.json");
        Rootobject root = JsonConvert.DeserializeObject<Rootobject>(text);
        var data = root.MRData.DriverTable.Drivers[0];

        driverName.Text = $"Név: {data.givenName} {data.familyName}\n" +
            $"Szám: {data.permanentNumber}\n" +
            $"Kód: {data.code}\n" +
            $"Születési dátum: {data.dateOfBirth}\n" +
            $"Nemzetiség: {data.nationality}";
    }
}