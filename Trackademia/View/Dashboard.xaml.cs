using Microsoft.Maui.Controls;
using System;
using System.Text.Json;

namespace Trackademia.View
{
    public partial class Dashboard : ContentPage
    {
        private readonly DashboardViewModel _viewModel;

        public Dashboard()
        {
            InitializeComponent();
            _viewModel = new DashboardViewModel();
            BindingContext = _viewModel;

            // Subscribe to chart data changes
            _viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(DashboardViewModel.ChartData))
                {
                    UpdateChart();
                }
            };
        }

        private void UpdateChart()
        {
            string htmlContent = $@"
               <!DOCTYPE html>
               <html>
               <head>
                   <meta charset='utf-8'>
                   <meta name='viewport' content='width=device-width, initial-scale=1'>
                   <script src='https://cdnjs.cloudflare.com/ajax/libs/Chart.js/3.7.0/chart.min.js'></script>
                   <link href='https://fonts.googleapis.com/css2?family=Poppins:wght@400;600&display=swap' rel='stylesheet'>
                   <style>
                       body {{ 
                           margin: 0; 
                           padding: 10px; 
                           background-color: transparent;
                           font-family: 'Poppins', sans-serif;
                       }}
                       canvas {{ 
                           max-width: 100%;
                           max-height: 100%;
                       }}
                   </style>
               </head>
               <body>
                   <canvas id='myChart'></canvas>
                   <script>
                       const ctx = document.getElementById('myChart').getContext('2d');
                       const data = {_viewModel.ChartData};
                       Chart.defaults.font.family = 'Poppins';
                       
                       new Chart(ctx, {{
                           type: 'doughnut',
                           data: {{
                               labels: Object.keys(data),
                               datasets: [{{
                                   data: Object.values(data),
                                   backgroundColor: [
                                       '#7C84A3',  // BMMA
                                       '#404454',  // BSCS
                                       '#1672EC'   // BSIT
                                   ],
                                   borderWidth: 0,
                                   hoverOffset: 4
                               }}]
                           }},
                           options: {{
                               responsive: true,
                               maintainAspectRatio: true,
                               animation: {{
                                   animateScale: true,
                                   animateRotate: true,
                                   duration: 1500
                               }},
                               plugins: {{
                                   legend: {{
                                       position: 'bottom',
                                       labels: {{
                                           padding: 20,
                                           font: {{
                                               size: 14,
                                               family: 'Poppins'
                                           }}
                                       }}
                                   }},
                                   tooltip: {{
                                       backgroundColor: 'white',
                                       titleColor: '#2B60DE',
                                       bodyColor: '#2B60DE',
                                       borderColor: '#2B60DE',
                                       borderWidth: 1,
                                       padding: 12,
                                       titleFont: {{
                                           family: 'Poppins',
                                           size: 14
                                       }},
                                       bodyFont: {{
                                           family: 'Poppins',
                                           size: 14
                                       }},
                                       callbacks: {{
                                           label: function(tooltipItem) {{
                                               return tooltipItem.label + ': ' + tooltipItem.raw + ' students';
                                           }}
                                       }}
                                   }}
                               }}
                           }}
                       }});
                   </script>
               </body>
               </html>";

            ChartWebView.Source = new HtmlWebViewSource { Html = htmlContent };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadDashboardDataCommand.Execute(null);
        }

        private async void OnViewStudentsButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                await button.ScaleTo(0.95, 50);
                await button.ScaleTo(1, 50);
            }
            await Shell.Current.GoToAsync("//UserPage");
        }

        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                await button.ScaleTo(0.95, 50);
                await button.ScaleTo(1, 50);
            }
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}