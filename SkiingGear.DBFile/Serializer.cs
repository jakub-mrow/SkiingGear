using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace MularczykMrowczynski.SkiingGear.SkiingGear.DBFile
{
    internal class Serializer
    {
        public static void Serialize<T>(string filePath, List<T> output)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                #pragma warning disable SYSLIB0011
                new BinaryFormatter().Serialize(fileStream, output);
            }
        }

        public static List<T> Deserialize<T>(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                return (List<T>)new BinaryFormatter().Deserialize(fileStream);
            }
        }
    }
}
