namespace EnumOpdracht
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");


            Colors[] ColorList = Enum.GetValues(typeof(Colors)).Cast<Colors>().ToArray();

            foreach (Colors color in ColorList)
            {
                Console.WriteLine(color.ToString());
            }

            Console.WriteLine("Choose one and hope for the best");
            string value = Console.ReadLine();


            Colors parsedColor = Enum.Parse<Colors>(value);


            if (parsedColor == Colors.purple)
            {
                Console.WriteLine("You guessed correctly!");
            }
            else
            {
                Console.WriteLine("Incorrect, deleting system32...");
            }
        }
    }
}