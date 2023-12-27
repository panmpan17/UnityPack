using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;



namespace MPack
{
    public static class ChatGPTRequest
    {
        // private const string OPENAI_APIKEY = "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";
        private const string API_KEY_FILE_PATH = "Assets/MPack/Language/Scripts/Editor/GPTAPIKey.asset";

        [MenuItem("MPack/ChatGPT/Create API key file")]
        public static void CreateAPIKeyFile()
        {
            var key = ScriptableObject.CreateInstance<StringVariable>();
            AssetDatabase.CreateAsset(key, API_KEY_FILE_PATH);
            AssetDatabase.SaveAssets();
        }

        public static string GetAPIKey()
        {
            var key = AssetDatabase.LoadAssetAtPath<StringVariable>(API_KEY_FILE_PATH);

            if (key == null)
                return "";

            return key.Value;
        }

        public static async Task<ResponseJSON> Translate(string toLanguage, string message)
        {
            string endpoint = "https://api.openai.com/v1/chat/completions"; // Adjust endpoint as needed

            Message[] messages = new Message[]
            {
                new Message ( "system", "You are a translation assistant." ),
                new Message ( "user", $"Can you translate the follow text into {toLanguage}? \"{message}\"" ),
            };

            var data = new
            {
                model="gpt-3.5-turbo",
                messages=messages
            };

            // Install Unity Package "com.unity.nuget.newtonsoft-json"
            var response = await GetChatResponse(endpoint, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseJSON>(response);
        }

        static async Task<string> GetChatResponse(string endpoint, string data)
        {
            string key = GetAPIKey();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {key}");

                var content = new StringContent(data, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(endpoint, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
            }
        }

        public struct ResponseJSON
        {
            public string id;
            public string @object;
            public int created;
            public string model;
            public Choice[] choices;
            public Usage usage;

            public struct Choice
            {
                public int index;
                public Message message;
                public string finish_reason;
            }

            public struct Usage
            {
                public int prompt_tokens;
                public int completion_tokens;
                public int total_tokens;
            }
        }

        public struct Message
        {
            public string role;
            public string content;

            public Message(string role, string content)
            {
                this.role = role;
                this.content = content;
            }
        }
    }
}