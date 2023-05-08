namespace ListOpdracht
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            List<string> birds = new List<string>() { "uil", "kraai", "hond", "papegaai"};
            List<string> birds2 = new List<string>() { "meeuw", "duif" }; 
            birds.AddRange(birds2);
            birds.Remove("hond");
            foreach (string bird in birds)
            {
                Console.WriteLine(bird);
            }
        }
    }
}