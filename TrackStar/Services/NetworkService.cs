using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TrackStar.Services
{
    public class NetworkService
    {
        private static int MaxThreadCount = 3;

        private HttpClient httpClient;

        SemaphoreSlim semaphore;
        public NetworkService()
        {
            this.httpClient = new HttpClient();
            this.semaphore = new SemaphoreSlim(NetworkService.MaxThreadCount, NetworkService.MaxThreadCount);

        }


        // returns json string as result or throw error
        public async Task<string> RequestWithRetry(string url)
        {
            int maxRetries = 3;
            bool acquired = false;
            int sleep = 1000; // 1 second

            await semaphore.WaitAsync();
            acquired = true;
            try
            {
                for (int i = 0; i < maxRetries; i++)
                {
                    var result = await this.httpClient.GetAsync(url);

                    if (result.IsSuccessStatusCode)
                    {
                        return await result.Content.ReadAsStringAsync();
                    }

                    // Log failed attempt
                    var errorContent = await result.Content.ReadAsStringAsync();
                    Console.WriteLine($"Attempt {i + 1} failed: {result.StatusCode} - {errorContent}");

                    // Last attempt? Throw.
                    if (i == maxRetries - 1)
                    {
                        throw new HttpRequestException(
                            $"Request failed after {maxRetries} attempts. " +
                            $"Status: {result.StatusCode}, Response: {errorContent}"
                        );
                    }

                    await Task.Delay(sleep);
                    sleep = (int)(sleep * 1.5);
                }

                throw new HttpRequestException($"Request failed after {maxRetries} attempts."); // should never hit
            }
            finally
            {
                if (acquired)
                    semaphore.Release();
            }
        }


    }
}
