using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace SCv20_Tools.Core.Services {

    public class SerializationService {
        private static SerializationService _instance;

        private SerializationService() {
            Settings = new JsonSerializerSettings() { 
                ContractResolver = new CamelCasePropertyNamesContractResolver(), 
                Formatting       = Newtonsoft.Json.Formatting.Indented 
            };
        }

        public Formatting Formatting {
            get;
            set;
        }

        public JsonSerializerSettings Settings {
            get;
            set;
        }

        public static SerializationService GetInstance() {
            lock (typeof(SerializationService)) {
                if (_instance == null)
                    _instance = new SerializationService();

                return _instance;
            }
        }

        public T Deserialize<T>(string json) {
            JsonSerializer ser = new JsonSerializer();
            var graph = JsonConvert.DeserializeObject(json, typeof(T), this.Settings);
            return (T)graph;
        }

        public dynamic Deserialize(string json) {
            dynamic d = JObject.Parse(json);
            return d;
        }

        public T DeserializeFile<T>(string path) {
            using (StreamReader sr = new StreamReader(path)) {
                JsonSerializer ser = new JsonSerializer();
                String file = sr.ReadToEnd();
                var graph = JsonConvert.DeserializeObject(file, typeof(T), this.Settings);
                return (T)graph;
            }
        }

        public dynamic DeserializeFile(string path) {
            using (StreamReader sr = new StreamReader(path)) {
                String file = sr.ReadToEnd();
                dynamic d = JObject.Parse(file);
                return d;
            }
        }

        public string Serialize(object data) {
            using (var fileWriter = new StringWriter()) {
                if (data != null) {
                    JsonTextWriter writer = new JsonTextWriter(fileWriter) {
                        Formatting = Formatting
                    };

                    JsonSerializer serializer = JsonSerializer.Create(Settings);
                    serializer.Serialize(writer, data);
                    writer.Flush();

                    return fileWriter.ToString();
                }
            }

            return null;
        }

        public void SerializeFile(object data, string path) {
            using (var fileWriter = File.CreateText(path)) {
                if (data != null) {
                    JsonTextWriter writer = new JsonTextWriter(fileWriter) {
                        Formatting = Formatting
                    };

                    JsonSerializer serializer = JsonSerializer.Create(Settings);
                    serializer.Serialize(writer, data);

                    writer.Flush();
                }
            }
        }
    }
}