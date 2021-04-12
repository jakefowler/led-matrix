using LedMatrix.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace LedMatrix.Controllers
{
    public class AnkiController : Controller
    {
        class AnkiDateReps
        {
            public List<List<JsonElement>> result { get; set; }
            public object error { get; set; }
        }

        private readonly ILedStripTranslation _ledStripTranslation;
        public static readonly HttpClient Client = new HttpClient();
        private const string url = "http://localhost:8765";
        public AnkiController (ILedStripTranslation ledStripTranslation)
        {
            _ledStripTranslation = ledStripTranslation;
        }
        public async Task<List<HabitDayReps>> RetrieveAnkiRepsPerDay()
        {
            List<HabitDayReps> habitDayReps = new List<HabitDayReps>();
            var call = new { action = "getNumCardsReviewedByDay", version = 6 };
            var json = JsonSerializer.Serialize(call);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await Client.PostAsync(url, data);
                response.EnsureSuccessStatusCode();
                AnkiDateReps myDeserializedClass = JsonSerializer.Deserialize<AnkiDateReps>(response.Content.ReadAsStringAsync().Result);
                foreach (var dateReps in myDeserializedClass.result)
                {
                    DateTime dateTime = Convert.ToDateTime(dateReps[0].ToString());
                    int reps = dateReps[1].GetInt32();
                    habitDayReps.Add(new HabitDayReps(dateTime, reps));
                }
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return habitDayReps;
        }
    }
}
