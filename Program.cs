namespace robot;
class Program
{
    private static string[] generateMap(int rows, int cols)
    {
        Random rand = new Random();
        List<string> map = new();

        for (int i = 0; i < rows; i++)
        {
            StringBuilder builder = new();
            for (int j = 0; j < cols; j++)
                builder.Append(rand.Next(0, 1000) > 50 || (i == 0 && j == 0) ? "." : "X");

            map.Add(builder.ToString());

        }

        return map.ToArray();

    }

    static void Main(string[] args)
    {
        Console.Clear();
        string[] testCase = Program.generateMap(25, 100);

        bool debug = false;

#if DEBUG
        debug = true;
        Console.WriteLine("DEBUG MODE ENABLED");
        Thread.Sleep(3000);
#endif

        Robot r = new Robot(testCase, debug);
        int res = r.Traverse();

        Console.Clear();
        Console.WriteLine(r.Grid.ToString());

        Console.WriteLine("\n\n--------------\n");
        Console.WriteLine(res);

    }
}
