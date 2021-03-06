﻿using GoogleBooks.Client.Dtos.Output;
using GoogleBooks.Client.Interfaces;
using GoogleBooks.Domain.Domain;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoogleBooks.Client.Services
{
    public class GoogleBooksClientService : IGoogleBooksClientService
    {
        private readonly IUrlFactory _urlFactory;
        private readonly HttpClient _httpClient;

        public GoogleBooksClientService
        (
            IUrlFactory urlFactory,
            HttpClient httpClient
        )
        {
            _urlFactory = urlFactory;
            _httpClient = httpClient;
        }

        public async Task<GoogleBookDetailsFull> GetBookDetailsAsync(string bookId)
        {
            try
            {
                _urlFactory.SetBookDetailsUrl(bookId);
                
                string response = await GetResponseStringAsync();

                return DeserializeResponse<GoogleBookDetailsFull>(response);
            }
            catch
            {
                throw;
            }
        }

        public async Task<GoogleBooksCatalog> GetBooksCatalogAsync(BooksCatalog domainBooksCatalog)
        {
            try
            {
                _urlFactory.SetBooksCatalogUrl(domainBooksCatalog);

                string response = await GetResponseStringAsync();

                return DeserializeResponse<GoogleBooksCatalog>(response);
            }
            catch
            {
                throw;
            }
        }

        #region Private Methods
        private async Task<string> GetResponseStringAsync()
            => await _httpClient.GetStringAsync(_urlFactory.Url);

        private T DeserializeResponse<T>(string response) where T : class
            => JsonConvert.DeserializeObject<T>(response);

        #endregion
    }
}
