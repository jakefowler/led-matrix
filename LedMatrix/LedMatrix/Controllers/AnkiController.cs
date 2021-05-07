using LedMatrix.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace LedMatrix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnkiController : Controller
    {
        class AnkiDateReps
        {
            public List<List<JsonElement>> result { get; set; }
            public object error { get; set; }
        }

        private readonly string ankiDataFilePath = "~"; 
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
        public async Task<List<HabitDayRep>> GetAnkiRepsPerDay()
        {
            const string action = "getNumCardsReviewedByDay";
            List<HabitDayRep> habitDayReps = new List<HabitDayRep>();
            HttpResponseMessage response = await CallAnkiApi(action);
            AnkiDateReps myDeserializedClass = JsonSerializer.Deserialize<AnkiDateReps>(response.Content.ReadAsStringAsync().Result);
            foreach (var dateReps in myDeserializedClass.result)
            {
                DateTime dateTime = Convert.ToDateTime(dateReps[0].ToString());
                int reps = dateReps[1].GetInt32();
                habitDayReps.Add(new HabitDayRep(dateTime, reps));
            }
            return habitDayReps;
        }
        public async Task<bool> SyncAnkiCollection()
        {
            const string action = "sync";
            HttpResponseMessage response = await CallAnkiApi(action);
            return response.IsSuccessStatusCode;
        }
        [HttpGet]
        public async Task<JsonResult> Heatmap()
        {
            await SyncAnkiCollection();
            return new JsonResult(await GetAnkiRepsPerDay());
        }
        public async Task<List<HabitDayRep>> ReadAnkiData()
        {
            List<HabitDayRep> habitDayReps = new List<HabitDayRep>();
            string jsonString = await System.IO.File.ReadAllTextAsync(ankiDataFilePath);
            AnkiDateReps myDeserializedClass = JsonSerializer.Deserialize<AnkiDateReps>(jsonString);
            foreach (var dateReps in myDeserializedClass.result)
            {
                DateTime dateTime = Convert.ToDateTime(dateReps[0].ToString());
                int reps = dateReps[1].GetInt32();
                habitDayReps.Add(new HabitDayRep(dateTime, reps));
            }
            return habitDayReps;
        }
        public async Task<bool> StoreAnkiData(List<HabitDayRep> habitDayReps)
        {
            if (System.IO.File.Exists(ankiDataFilePath))
            {
                System.IO.File.Delete(ankiDataFilePath);
            }
            using FileStream filestream = System.IO.File.Create(ankiDataFilePath);
            await JsonSerializer.SerializeAsync(filestream, habitDayReps);
            return true;
        }
    }
}
