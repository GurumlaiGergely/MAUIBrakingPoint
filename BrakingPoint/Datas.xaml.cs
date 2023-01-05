using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;
using BrakingPoint.Models;


namespace BrakingPoint;

public partial class Datas : ContentPage
{
    private List<string> driversList = new List<string>()
    {
        "Alexander Albon", "Fernando Alonso", "Valtteri Bottas", "Nyck deVries", "Pierre Gasly", "Lewis Hamilton", 
        "Nico Hulkenberg", "Nicholas Latifi", "Charles Leclerc", "Kevin Magnussen", "Lando Norris", "Esteban Ocon", 
        "Sergio Perez", "Daniel Ricciardo", "George Russell", "Carlos Sainz", "Mick Schumacher", "Lance Stroll", 
        "Yuki Tsunoda", "Max Verstappen", "Sebastian Vettel", "Guanyu Zhou" 
    };

    private List<string> racesList = new List<string>()
    {
        "Bahrain Grand Prix", "Saudi Arabian Grand Prix", "Australian Grand Prix", "Emilia Romagna Grand Prix",
        "Miami Grand Prix", "Spanish Grand Prix", "Monaco Grand Prix", "Azerbaijan Grand Prix", "Canadian Grand Prix", 
        "British Grand Prix", "Austrian Grand Prix", "French Grand Prix", "Hungarian Grand Prix", "Belgian Grand Prix",
        "Dutch Grand Prix", "Italian Grand Prix", "Singapore Grand Prix", "Japanese Grand Prix", "United States Grand Prix",
        "Mexico City Grand Prix", "São Paulo Grand Prix", "Abu Dhabi Grand Prix"
    };
    public Datas()
    {
        InitializeComponent();
        driverPicker.ItemsSource = driversList;
        racePicker.ItemsSource = racesList;
    }



    


    private async void driverPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (driverPicker.SelectedIndex != -1)
        {
            string picked = "";

            if (driverPicker.SelectedItem.ToString() == "Kevin Magnussen")
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
            DriverModel.Rootobject root = JsonConvert.DeserializeObject<DriverModel.Rootobject>(text);
            var data = root.MRData.DriverTable.Drivers[0];

            stats1.Text = $"Name: {data.givenName} {data.familyName}\n" +
                $"Car number: {data.permanentNumber}\n" +
                $"Code: {data.code}\n" +
                $"Date of birth: {data.dateOfBirth}\n" +
                $"Nationality: {data.nationality}";

            Image.IsVisible = true;
            Image.Source = $"{picked}.png";
            Border.IsVisible= true;
            racePicker.SelectedIndex = -1;

            stats2.IsVisible = false;
            betweenLabel.IsVisible = false;
            stats2.Text = "";
            info.IsVisible = false;

            circuitStats.IsVisible = false;
            circuitStats.Text = "";
        }
        else
            driverPicker.SelectedIndex = -1;
    }




    private async void racePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (racePicker.SelectedIndex != -1)
        {
            int round = racePicker.SelectedIndex + 1;
            string picked = round.ToString();

            HttpClient client = new HttpClient();
            var text = await client.GetStringAsync($"https://ergast.com/api/f1/2022/{picked}/results.json");
            RaceModel.Rootobject root = JsonConvert.DeserializeObject<RaceModel.Rootobject>(text);

            string datas1 = "";
            string datas2 = "";

            for (int i = 0; i < 20; i++)
            {
                var data = root.MRData.RaceTable.Races[0].Results[i];
                if (i < 10)
                    datas1 += $" {data.position} | {data.Driver.code} | {data.laps}\n";
                else
                    datas2 += $" {data.position} | {data.Driver.code} | {data.laps}\n";

            }
            stats1.Text = datas1;
            stats2.Text = datas2;
            //stats2.FontSize = 20;

            stats2.IsVisible = true;
            betweenLabel.IsVisible = true;
            info.IsVisible = true;

            var circuitData = root.MRData.RaceTable.Races[0];
            circuitStats.Text = $"Name: {circuitData.Circuit.circuitName}\n" +
                $"Location: {circuitData.Circuit.Location.locality}, {circuitData.Circuit.Location.country}\n" +
                $"Date: {circuitData.date}, {circuitData.time}";
            circuitStats.IsVisible = true;

            Image.Source = "";
            Image.IsVisible = false;
            Border.IsVisible = false;
            driverPicker.SelectedIndex = -1;
        }
        else
            racePicker.SelectedIndex = -1;
    }
}