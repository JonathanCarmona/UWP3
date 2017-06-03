using Newtonsoft.Json;
using Openweather3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace OpenWeather3
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await muestraDatos();
        }

        public async Task muestraDatos()
        {
            try
            {
                String data = await obtenDatosOpenWeather();
                OpenWeather op = JsonConvert.DeserializeObject<OpenWeather>(data);

                String path = "Assets/" + op.weather[0].icon + ".png";

                if (File.Exists(path))
                {
                    imageWeather.Source = new BitmapImage(new Uri("ms-appx:///" + path));
                    textTemperature.Text = string.Format("{0:0.0} \u00b0", op.main.temp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Task<String> obtenDatosOpenWeather()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client.GetStringAsync(@"http://localhost/actividades/clima");
        }
        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            await muestraDatos();
        }
    }
}
