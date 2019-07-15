using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FirebasePerfTest
{
    public class NetworkHelper {

        public static async Task GetRequestAsync(Uri uri) {
            try {
                using (var client = new HttpClient()) {
                    using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri)) {
                        using (var responseMessage = await client.SendAsync(requestMessage)) {
                            var stringResponse = await responseMessage.Content.ReadAsStringAsync();

                            return;
                        }
                    }
                }
            } catch (Exception exception) {
                Console.WriteLine(exception.StackTrace);
                throw exception;
            }
        }

    }
}
