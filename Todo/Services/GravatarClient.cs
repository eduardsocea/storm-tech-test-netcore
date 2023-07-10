using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Todo.Configurations;
using Todo.Models.Gravatar;

namespace Todo.Services
{
    public class GravatarClient : IGravatarClient
    {
        private const string UriExtension = ".json";
        private readonly GravatarOptions _gravatarOptions;

        public GravatarClient(IOptions<GravatarOptions> gravatarOptions)
        {
            _gravatarOptions = gravatarOptions?.Value ?? throw new ArgumentNullException(nameof(gravatarOptions));
        }

        public async Task<string> GetGravatarUsernameAsync(string email, CancellationToken cancellationToken = default)
        {
            using var client = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(_gravatarOptions.RequestTimeout)
            };

            var request = new HttpRequestMessage(HttpMethod.Get, BuildProfileUri(email));
            request.Headers.UserAgent.Add(new ProductInfoHeaderValue(".NET", "1"));
            var response = await client.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return email;
            }


            var gravatarDto = await response.Content.ReadFromJsonAsync<GravatarDto>(cancellationToken: cancellationToken);
            return gravatarDto?.Entry?.FirstOrDefault()?.PreferredUsername ?? email;
        }

        private string BuildProfileUri(string email)
            => string.Format(_gravatarOptions.ProfileUri, Gravatar.GetHash(email), UriExtension);
    }
}
