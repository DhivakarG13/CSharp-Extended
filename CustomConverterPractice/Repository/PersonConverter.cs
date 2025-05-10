using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Repository
{
    public class PersonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Person).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            string type = jo["Type"]?.ToString();

            switch (type)
            {
                case nameof(Student):
                    return new Student(jo["Name"].ToString(), (int)jo["Age"], (double)jo["Grade"]);
                case nameof(Employee):
                    return new Employee(jo["Name"].ToString(), (int)jo["Age"], (double)jo["Salary"]);
                default:
                    return new Person(jo["Name"].ToString(), (int)jo["Age"]);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JObject jo = JObject.FromObject(value);
            jo.Add("Type", value.GetType().Name);
            jo.WriteTo(writer);
        }
    }
}