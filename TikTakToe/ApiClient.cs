using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using TikTakToe.Models;

namespace TikTakToe
{
    public interface IApiClient
    {
        Task<IList<Field>> GetAllItemsAsync();
        Task<Field> GetItemByIdAsync(int id);
        Task CreateItemAsync();
        Task UpdateItemAsync(Field field);
        Task UpdatePlayersAsync(string newPlayersValue, int id);
    }

    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(string baseAddress)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
        }

        public async Task<IList<Field>> GetAllItemsAsync()
        {
            var response = await _httpClient.GetAsync("/api/Fields");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var resultDto= JsonConvert.DeserializeObject<List<FieldDto>>(content);
            var result = new List<Field>();
            foreach (var resultDtoField in resultDto)
            {
                result.Add(Parser.FromFieldDtoToField(resultDtoField));
            }
            return result;
        }

        public async Task<Field> GetItemByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Fields/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var resultDto = JsonConvert.DeserializeObject<FieldDto>(content);
            return Parser.FromFieldDtoToField(resultDto);
        }

        public async Task CreateItemAsync()
        {
            var response = await _httpClient.PostAsync("/api/Fields", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateItemAsync(Field field)
        {
            var FieldDto = Parser.FromFieldToFieldDto(field);

            var json = JsonConvert.SerializeObject(FieldDto.Positions);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/Fields/{FieldDto.Id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdatePlayersAsync(string newPlayersValue, int id)
        {
            var response = await _httpClient.PutAsync($"/api/Fields/{id}/{newPlayersValue}", null);
            response.EnsureSuccessStatusCode();
        }
    }
}
