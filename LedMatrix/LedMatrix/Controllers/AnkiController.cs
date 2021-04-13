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
        private const int ankiApiVersion = 6;
        public AnkiController (ILedStripTranslation ledStripTranslation)
        {
            _ledStripTranslation = ledStripTranslation;
        }
        public async Task<HttpResponseMessage> CallAnkiApi(string callAction)
        {
            var call = new { action = callAction, version = ankiApiVersion };
            var json = JsonSerializer.Serialize(call);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await Client.PostAsync(url, data);
            responseMessage.EnsureSuccessStatusCode();
            return responseMessage;
        }
        public async Task<List<HabitDayReps>> GetAnkiRepsPerDay()
        {
            const string action = "getNumCardsReviewedByDay";
            List<HabitDayReps> habitDayReps = new List<HabitDayReps>();
            HttpResponseMessage response = await CallAnkiApi(action);
            AnkiDateReps myDeserializedClass = JsonSerializer.Deserialize<AnkiDateReps>(response.Content.ReadAsStringAsync().Result);
            foreach (var dateReps in myDeserializedClass.result)
            {
                DateTime dateTime = Convert.ToDateTime(dateReps[0].ToString());
                int reps = dateReps[1].GetInt32();
                habitDayReps.Add(new HabitDayReps(dateTime, reps));
            }
            return habitDayReps;
        }
        public async Task<bool> SyncAnkiCollection()
        {
            const string action = "sync";
            HttpResponseMessage response = await CallAnkiApi(action);
            return response.IsSuccessStatusCode;
        }
    }
}
