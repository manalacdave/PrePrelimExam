using DaveMañalac.PrelimExam.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json.Serialization;

namespace DaveMañalac.PrelimExam.Pages
{


    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly object Restsharp;
        private object ViewModel;
        private Student? viewModel;

        public string Message { get; private set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IndexModel(Student? viewModel) => this.SetViewModel(viewModel);

        public Student? GetViewModel()
        {
            return viewModel;
        }

        public void SetViewModel(Student? value)
        {
            viewModel = value;
        }

        public Task<IActionResult> OnGet(
            IActionResult page, string? Weather)


        {
            if (page is null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            this.SetViewModel(new Student
            {
                Weather = Weather
            }
            );

            {
                var client = new RestSharp.Portable.HttpClient.ResClient("https://fcc-weather-api.glitchme/api/");
                object request = RestSharp.Portable.RestRequest(
                    "current?lat=14.8781&lon120.4546", Restsharp.Portable.Method.GET);
                //request.AddParameter(new Parameter() { nameof = "lat", ValueTask = 14.8781 });
                //request.AddParameter(new Parameter() { nameof = "long", ValueTask = 120.4546 });

                IRestResponse response = (IRestResponse)client.execute(request);

                var content = response.Content;


            }
        }
        public void OnPostView(int id)
        {
            Message = $"View handler fired for {id}";
        }

        public class WeatherData
        {
            public WeatherMain? Main { get; set; }

            public List<WeatherDetails>? Weather { get; set; }
        }
        public class WeatherMain
        {
            public string? Temp { get; set; }

            [JsonPropertyName("feels-like")]
            public string? FeelsLike { get; set; }
            public string? Pressure { get; set; }
            public string? Humidity { get; set; }
        }

        public class WeatherDetails
        {
            public string? Temp { get; set; }
            public string? Icon { get; set; }

            public string? Description { get; set; }
        }

    }

}