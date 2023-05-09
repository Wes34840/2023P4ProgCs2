namespace FileDirOpdracht
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            DirectoryInfo dir = new DirectoryInfo(@"C:\Users\sbroe\Documents\Ma\bewijzenmap\module4\m4prog\2023P4ProgCs2");
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                Console.WriteLine(file.Name);
            }

            DirectoryInfo[] directories = dir.GetDirectories();
            foreach(DirectoryInfo directory in directories)
            {
                Console.WriteLine(directory.Name);
                ShowAllInsideDir(directory);
            }
        }

        static void ShowAllInsideDir(DirectoryInfo dir)
        {
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                Console.WriteLine(file.Name);
            }
            DirectoryInfo[] directories = dir.GetDirectories();
            if (directories != null)
            {
                foreach (DirectoryInfo directory in directories)
                {
                    Console.WriteLine(directory.Name);
                    ShowAllInsideDir(directory);
                }
            }
        } // idk how the fuck I did this first try and didn't brick my pc lmao
    }
}