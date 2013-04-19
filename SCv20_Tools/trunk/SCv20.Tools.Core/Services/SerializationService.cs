using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace SCv20.Tools.Core.Services {
    public class SerializationService {
        private static SerializationService _instance;


        private SerializationService() {
            Settings = new JsonSerializerSettings();
            Formatting = Newtonsoft.Json.Formatting.Indented;
        }

        
        public static SerializationService GetInstance() {
            lock (typeof(SerializationService)) {
                if (_instance == null)
                    _instance = new SerializationService();

                return _instance;
            }
        }
        

        public JsonSerializerSettings Settings {
            get;
            set;
        }


        public Formatting Formatting {
            get;
            set;
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


        public T Deserialize<T>(string json) {
            JsonSerializer ser = new JsonSerializer();
            var graph = JsonConvert.DeserializeObject(json, typeof(T), this.Settings);
            return (T)graph;
        }


        public T DeserializeFile<T>(string path) {
            using (StreamReader sr = new StreamReader(path)) {
                JsonSerializer ser = new JsonSerializer();
                String file = sr.ReadToEnd();
                var graph = JsonConvert.DeserializeObject(file, typeof(T), this.Settings);
                return (T)graph;
            }
        }


        public dynamic Deserialize(string json) {
            dynamic d = JObject.Parse(json);
            return d;
        }


        public dynamic DeserializeFile(string path) {
            using (StreamReader sr = new StreamReader(path)) {
                String file = sr.ReadToEnd();
                dynamic d = JObject.Parse(file);
                return d;
            }
        }
        
    }
}
