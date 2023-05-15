using System.IO;

namespace FileIOOpdracht
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\sbroe\\Documents\\Ma\\bewijzenmap\\module4\\m4prog\\2023P4ProgCs2\\FileIOOpdracht\\FileIOOpdracht";
            string readPath = path + "\\ReadThisFile.txt";
            string writePath = path + "\\MyNewFile.txt";

            string[] content = File.ReadAllLines(readPath);

            foreach (string line in content)
            {
                Console.WriteLine(line);
            }

            File.CreateText(writePath).Dispose();

            string createText = "Dit is een stuk tekst";
            string appendText = "This is supposed to go under the previous text";

            File.WriteAllText(writePath, createText + Environment.NewLine);
            File.AppendAllText(writePath, appendText + Environment.NewLine);

            Directory.CreateDirectory(path + "\\output");
            var f = new FileInfo("MyNewFile.txt");
            File.Move("MyNewFile.txt", path + "\\output\\MyNewFile", true);

        }
    }
}