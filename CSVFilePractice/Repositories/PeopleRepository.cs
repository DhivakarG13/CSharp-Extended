using System.Globalization;
using CSVFilePractice.Constants;
using CSVFilePractice.Models;
using CsvHelper;
using CsvHelper.Configuration;

namespace CSVFilePractice.Repositories
{
    public class PeopleRepository
    {
        private string _fileName;

        public List<Person> People { get; set; }

        public PeopleRepository(string fileName)
        {
            _fileName = fileName;
            People = LoadFromDatabaseUsingCSVHelper();
        }

        public void SaveToDatabaseSimple()
        {
            using (FileStream fileStream = new FileStream(_fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter streamWriter = new(fileStream))
                {
                    streamWriter.WriteLine("Name,Weight,Gender");
                    foreach (Person person in People)
                    {
                        streamWriter.WriteLine($"{person.Name},{person.Weight},{(int)person.Gender}");
                    }
                }
            }
        }

        public List<Person> LoadFromDatabaseSimple()
        {
            List<Person> temporaryPeopleData = new List<Person>();
            using (FileStream fileStream = new FileStream(_fileName, FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader streamReader = new(fileStream))
                {
                    streamReader.ReadLine(); // Ignoring Headers
                    string? LineData;
                    do
                    {
                        LineData = streamReader.ReadLine();
                        if (LineData != null)
                        {
                            string[] objectValues = LineData.Split(",");
                            temporaryPeopleData.Add(new Person(objectValues[0], double.Parse(objectValues[1]), (GenderOptions)int.Parse(objectValues[2])));
                        }
                    }
                    while (LineData != null);
                }
            }

            People = temporaryPeopleData;
            return People;
        }

        public void SaveToDatabaseUsingCSVHelper()
        {
            using (FileStream fileStream = new FileStream(_fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter streamWriter = new(fileStream))
                {
                    using (var csv = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(People);
                    }
                }
            }
        }// Writes enum as String so there'll be suffering while reading

        public List<Person> LoadFromDatabaseUsingCSVHelper()
        {
            List<Person> temporaryPeopleData = new List<Person>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            };

            if(!File.Exists(_fileName))
            {
                return new List<Person>();
            }
            using (StreamReader streamReader = new(_fileName))
            {
                streamReader.ReadLine();
                using (var csv = new CsvReader(streamReader, config))
                {
                    temporaryPeopleData = csv.GetRecords<Person>().ToList();
                }
            }
            People = temporaryPeopleData;
            return People;
        }
    }
}
