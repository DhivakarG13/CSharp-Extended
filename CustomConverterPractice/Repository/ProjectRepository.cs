using Models;
using Newtonsoft.Json;

namespace Repository
{
    public class ProjectRepository
    {
        private string _fileName;
        public List<Person> PeopleData;

        public ProjectRepository(string fileName)
        {
            _fileName = fileName;
            PeopleData = FetchFromDatabase();
        }

        public List<Person> FetchFromDatabase()
        {
            using (FileStream fileStream = new FileStream(_fileName, FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.Converters.Add(new PersonConverter());
                    string jsonString = streamReader.ReadToEnd();
                    List<Person> convertedData = JsonConvert.DeserializeObject<List<Person>>(jsonString, settings) ?? new List<Person>();
                    return convertedData;
                }
            }
        }

        public T FetchFromDatabase<T>(string fileName) where T : new()
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    string jsonString = streamReader.ReadToEnd();
                    T convertedData = JsonConvert.DeserializeObject<T>(jsonString) ?? new T();
                    return convertedData;
                }
            }
        }

        public void WriteToDatabase()
        {
            WriteToDatabase<List<Person>>(_fileName);
        }

        public void WriteToDatabase<T>(string fileName)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.Converters.Add(new PersonConverter());
                    string jsonString = JsonConvert.SerializeObject(PeopleData, settings);
                    streamWriter.Write(jsonString);
                    streamWriter.Flush();
                }
            }
        }
    }
}