using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StudentsDiary
{
    //FileHelper może być interfejsem ISerializeFiles wykorzystywanym przez klasy XmlFileHelper i JsonFileHelper
    public class FileHelper<T> where T: new()       
    {
        private string _filePath;
        public FileHelper(string filePath)
        {
            _filePath = filePath;
        }

        public void SerializeToFile(T students)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var streamWriter = new StreamWriter(_filePath))
            {
                serializer.Serialize(streamWriter, students);
                streamWriter.Close();
            }
            // obiekt zadeklarowany w parametrze metody using, po jej ukończeniu zostanie zawsze 
            // usunięty z pamięci
        }

        public T DeserializeFromFile()
        {
            if (!File.Exists(_filePath))
                return new T();

            var serializer = new XmlSerializer(typeof(T));

            using (var streamReader = new StreamReader(_filePath))
            {
                var students = (T)serializer.Deserialize(streamReader);
                streamReader.Close();
                return students;
            }
        }
    }
}
//Kod na serializacją i deserializację danych z wykorzystaniem napisanych metod
//====================================================================================
//List<Student> students = new List<Student>();
//students.Add(new Student { FirstName = "Jan" });
//students.Add(new Student { FirstName = "Andrzej" });
//students.Add(new Student { FirstName = "Marian" });
//
//SerializeToFile(students);
//var students = DeserializeFromFile();
//foreach (var student in students)
//{
//    MessageBox.Show(student.FirstName);
//}

/*Poniższy kod działa identycznie jak zastosowana ostatecznie metoda
 * +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
 *
 *public void SerializeToFile(List<Student> students) 
{
        var serializer = new XmlSerializer(typeof(List<Student>));
        StreamWriter streamWriter = null;
    try
    {
        streamWriter = new StreamWriter(_filePath);
        serializer.Serialize(streamWriter, students );
        streamWriter.Close(); // streamy trzeba ręcznie zamknąć
    }
    finally
    {
        streamWriter.Dispose(); // i koniecznie wyczyścić z pamięci.  
    }
} */
