using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace PuppeteerSharpTest
{
    public class Genlogin
    {
        private String PORT;

        public Genlogin(String port = "55555")
        {
            PORT = port;
        }

        private String GetMethod(String url)
        {
            var json = "";

            using (var client = new HttpClient())
            {
                var endpoint = new Uri(url);
                var result = client.GetAsync(endpoint).Result;
                json = result.Content.ReadAsStringAsync().Result;
            }
            return json;
        }

        public String PostMethod(String url, String body)
        {
            var json = "";
            using (var client = new HttpClient())
            {
                var endpoint = new Uri(url);
                var payload = new StringContent(body, Encoding.UTF8, "application/json");
                var result = client.PostAsync(url, payload).Result;
                json = result.Content.ReadAsStringAsync().Result;
            }

            return json;

        }
        public String PutMethod(String url, String body)
        {
            var json = "";
            using (var client = new HttpClient())
            {
                var endpoint = new Uri(url);
                var payload = new StringContent(body, Encoding.UTF8, "application/json");
                var result = client.PutAsync(url, payload).Result;
                json = result.Content.ReadAsStringAsync().Result;
            }
            return json;
        }

        public String DeleteMethod(String url)
        {
            var json = "";
            using (var client = new HttpClient())
            {
                var endpoint = new Uri(url);
                var result = client.DeleteAsync(url).Result;
                json = result.Content.ReadAsStringAsync().Result;
            }
            return json;
        }

        public String openProfile(String id)
        {
            var mo = GetMethod($"http://localhost:{PORT}/profiles/{id}/start-automation");
            var jsonResult = (JObject)JsonConvert.DeserializeObject(mo);
            return jsonResult["data"]["wsEndpoint"].ToString();
        }

        public String closeProfile(String id)
        {
            var mo = GetMethod($"http://localhost:{PORT}/profiles/{id}/stop-automation");
            var jsonResult = (JObject)JsonConvert.DeserializeObject(mo);
            return jsonResult.ToString();
        }

        //Các loại hiện đang hỗ trợ
        // sử dụng thay ở phần type
        /*  
  HTTP = 'httpProxy',
  SOCKS4 = 'socks4Proxy',
  SOCKS5 = 'socks5Proxy',
}*/
        public String createProfile(String name)
        {
            var body = "{\r\n    \"profile_data\": {\r\n        \"name\": \"" + name + 
                "\",\r\n        \"browser\": \"Chrome\",\r\n        \"os\": \"Windows\",\r\n       " +
                " \"userAgent\": \"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.0.0 Safari/537.36 Edg/107.0.1418.42\",\r\n        " +
                "\"timezone\": \"Asia/Ho_Chi_Minh\",\r\n        \"webRTC\": {\r\n            \"ip\": \"\",\r\n            " +
                "\"type\": \"Real\"\r\n        },\r\n        \"proxy\": {\r\n            " +
                "\"type\": \"withoutProxy\",\r\n            \"host\": \"\",\r\n            \"port\": \"\",\r\n            \"login\": \"\",\r\n            \"password\": \"\",\r\n            " + // thay type và các thông tin proxy bạn cần ở đây
                "\"options\": {\r\n                \"location\": null,\r\n                \"apiKey\": \"\",\r\n                \"changeIpOnStartup\": false\r\n            },\r\n           " +
                " \"proxyInfo\": {\r\n                \"ip\": \"\",\r\n                \"timezone\": \"\",\r\n                \"country\": \"\"\r\n            }\r\n        },\r\n        " +
                "\"geolocation\": {\r\n            \"type\": \"Prompt\",\r\n            \"lat\": \"\",\r\n            \"lng\": \"\",\r\n            \"accuracy\": null\r\n        },\r\n       " +
                " \"navigator\": {\r\n            \"screenResolution\": null,\r\n            \"languages\": [],\r\n            \"platform\": \"Win32\",\r\n            \"hardwareConcurrency\": null,\r\n           " +
                " \"doNotTrack\": false\r\n        },\r\n        \"hardware\": {\r\n            \"canvas\": \"Off\",\r\n            \"clientRects\": \"Noise\",\r\n            \"audioContext\": \"Noise\",\r\n            " +
                "\"font\": \"Noise\",\r\n            \"webGL\": \"Noise\",\r\n            \"webGLVendor\": \"Google Inc. (AMD)\",\r\n            \"webGLRenderer\": \"ANGLE (AMD, AMD Radeon(TM) R5 Graphics Direct3D11 vs_5_0 ps_5_0, D3D11-26.20.14050.2)\"\r\n        },\r\n        \"other\": {\r\n            \"startUrl\": \"\",\r\n            \"liteMode\": \"\",\r\n            \"extensions\": \"\",\r\n            \"chromeArguments\": \"\"\r\n        },\r\n        \"cookies\": \"\",\r\n        \"tags\": []\r\n    },\r\n    \"share_status\": true\r\n}";
            
            var mo = PostMethod($"http://localhost:{PORT}/profiles/", body);
            var jsonResult = (JObject)JsonConvert.DeserializeObject(mo);
            return jsonResult.ToString();
        }

        public String searchProfile(String search, String type = "id")
        {
            //id
            if (type.Equals("id"))
            {
                var mo = GetMethod($"http://localhost:{PORT}/profiles/{search}");
                var jsonResult = (JObject)JsonConvert.DeserializeObject(mo);
                return jsonResult.ToString();
            }
            //name
            else
            {
                var mo = GetMethod($"http://localhost:{PORT}/profiles?search={search}");
                var jsonResult = (JObject)JsonConvert.DeserializeObject(mo);
                return jsonResult.ToString();
            }
        }

        public String updateProfile(String id,String body)
        {
            //Bạn có thể lấy data profiel đấy xong chỉnh sử những trường cần xong rồi put tất cả lên
            body = "{\r\n    \"profile_data\": {\r\n        \"name\": \"" + "cxcz" +
                "\",\r\n        \"browser\": \"Chrome\",\r\n        \"os\": \"Windows\",\r\n       " +
                " \"userAgent\": \"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.0.0 Safari/537.36 Edg/107.0.1418.42\",\r\n        " +
                "\"timezone\": \"Asia/Ho_Chi_Minh\",\r\n        \"webRTC\": {\r\n            \"ip\": \"\",\r\n            " +
                "\"type\": \"Real\"\r\n        },\r\n        \"proxy\": {\r\n            " +
                "\"type\": \"withoutProxy\",\r\n            \"host\": \"\",\r\n            \"port\": \"\",\r\n            \"login\": \"\",\r\n            \"password\": \"\",\r\n            " + // thay type và các thông tin proxy bạn cần ở đây
                "\"options\": {\r\n                \"location\": null,\r\n                \"apiKey\": \"\",\r\n                \"changeIpOnStartup\": false\r\n            },\r\n           " +
                " \"proxyInfo\": {\r\n                \"ip\": \"\",\r\n                \"timezone\": \"\",\r\n                \"country\": \"\"\r\n            }\r\n        },\r\n        " +
                "\"geolocation\": {\r\n            \"type\": \"Prompt\",\r\n            \"lat\": \"\",\r\n            \"lng\": \"\",\r\n            \"accuracy\": null\r\n        },\r\n       " +
                " \"navigator\": {\r\n            \"screenResolution\": null,\r\n            \"languages\": [],\r\n            \"platform\": \"Win32\",\r\n            \"hardwareConcurrency\": null,\r\n           " +
                " \"doNotTrack\": false\r\n        },\r\n        \"hardware\": {\r\n            \"canvas\": \"Off\",\r\n            \"clientRects\": \"Noise\",\r\n            \"audioContext\": \"Noise\",\r\n            " +
                "\"font\": \"Noise\",\r\n            \"webGL\": \"Noise\",\r\n            \"webGLVendor\": \"Google Inc. (AMD)\",\r\n            \"webGLRenderer\": \"ANGLE (AMD, AMD Radeon(TM) R5 Graphics Direct3D11 vs_5_0 ps_5_0, D3D11-26.20.14050.2)\"\r\n        },\r\n        \"other\": {\r\n            \"startUrl\": \"\",\r\n            \"liteMode\": \"\",\r\n            \"extensions\": \"\",\r\n            \"chromeArguments\": \"\"\r\n        },\r\n        \"cookies\": \"\",\r\n        \"tags\": []\r\n    },\r\n    \"share_status\": true\r\n}";
           
            var mo = PutMethod($"http://localhost:{PORT}/profiles/{id}", body);
            var jsonResult = (JObject)JsonConvert.DeserializeObject(mo);
            return jsonResult.ToString();
        }

        public String deleteProfile(String id)
        {   
            var mo = DeleteMethod($"http://localhost:{PORT}/profiles/{id}");
            var jsonResult = (JObject)JsonConvert.DeserializeObject(mo);
            return jsonResult.ToString();
        }



    }
}
