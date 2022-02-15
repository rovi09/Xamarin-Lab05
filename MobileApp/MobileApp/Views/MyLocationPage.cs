using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileApp.Views
{
    public class MyLocationPage : ContentPage
    {
       
           private Label LocationLabel;
        private Xamarin.Forms.Maps.Map MyMap;

            public MyLocationPage()
            {
                LocationLabel = new Label { Text = "0,0" };
            var LocationButton = new Button { Text = "My location", Command = new Command(() => ShowLocation()) };

            MyMap = new Xamarin.Forms.Maps.Map();
            MyMap.HasZoomEnabled = true;
            MyMap.VerticalOptions = LayoutOptions.FillAndExpand;

            

            var grid = new Grid{
                Children = {
                                LocationLabel,
                                LocationButton,
                                MyMap,
                            }

            };
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });

            

            // ubicar los elementos en el grid
            LocationLabel.SetValue(Grid.RowProperty, 0);            
            LocationButton.SetValue(Grid.RowProperty, 1);
            MyMap.SetValue(Grid.RowProperty, 2);


            Content = grid;
            }
        
            private async void ShowLocation()
            {
                // https://docs.microsoft.com/en-us/xamarin/essentials/permissions
                var permission = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                if (permission != PermissionStatus.Granted)
                {
                    permission = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                };

                if (permission != PermissionStatus.Granted)
                {
                    await DisplayAlert("Error", "Es requerido el permiso de ubicacion para esta funcionalidad", "Cerrar");
                    return;
                }

                var gpsLocation = await Geolocation.GetLocationAsync();

                if (gpsLocation != null)
                {
                    LocationLabel.Text = $"{gpsLocation.Latitude},{gpsLocation.Longitude}";
                }
            }
    }
}
