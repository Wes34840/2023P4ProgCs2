namespace SplitOpdracht
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string content = File.ReadAllText(Environment.CurrentDirectory + "\\stringsplit.txt");
            content = content.Replace(" ", "");
            string[] keyvalue = content.Split(":");
            string[] cijferspervak = keyvalue[1].Split(",", StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine(keyvalue[0]);

            foreach (string cijfer in cijferspervak)
            {
                Console.WriteLine(cijfer);
            }

            

        }
    }
}