using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;
using BrakingPoint.Models;


namespace BrakingPoint;

public partial class Datas : ContentPage
{
    private List<string> driversList = new List<string>()
    {
        "Alexander Albon", "Fernando Alonso", "Valtteri Bottas", "Nyck deVries", "Pierre Gasly", "Lewis Hamilton", "Nico Hulkenberg", "Nicholas Latifi", "Charles Leclerc", "Kevin Magnussen", "Lando Norris", "Esteban Ocon", "Sergio Perez", "Daniel Ricciardo", "George Russell", "Carlos Sainz", "Mick Schumacher", "Lance Stroll", "Yuki Tsunoda", "Max Verstappen", "Sebastian Vettel", "Guanyu Zhou" 
    };

    private List<string> racesList = new List<string>()
    {
        "Bahreini nagydíj", "Szaúd-arábiai nagydíj", "Ausztrál nagydíj", "Emilia-romagnai nagydíj", "Miami nagydíj", "Spanyol nagydíj", "Monacói nagydíj", "Azeri nagydíj", "Kanadai nagydíj", "Brit nagydíj", "Osztrák nagydíj", "Francia nagydíj", "Magyar nagydíj", "Belga nagydíj", "Holland nagydíj", "Olasz nagydíj", "Szingapúri nagydíj", "Japán nagydíj", "Amerikai nagydíj", "Mexikóvárosi nagydíj", "São Paulo-i nagydíj", "Abu-Dzabi nagydíj"
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

            driverName.Text = $"Név: {data.givenName} {data.familyName}\n" +
                $"Szám: {data.permanentNumber}\n" +
                $"Kód: {data.code}\n" +
                $"Születési dátum: {data.dateOfBirth}\n" +
                $"Nemzetiség: {data.nationality}";

            Image.IsVisible = true;
            Image.Source = $"{picked}.png";
            Border.IsVisible= true;
            racePicker.SelectedIndex = -1;
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

            string datas = "";

            for (int i = 0; i < 20; i++)
            {
                var data = root.MRData.RaceTable.Races[0].Results[i];
                datas += $"                 {data.position} | {data.Driver.code} | {data.laps}\n";

            }
            driverName.Text = $"Helyezés | Versenyző | Futott kör \n{datas}";
            driverName.FontSize = 20;

            Image.Source = "";
            Image.IsVisible = false;
            Border.IsVisible = false;
            driverPicker.SelectedIndex = -1;
        }
        else
            racePicker.SelectedIndex = -1;
    }
}