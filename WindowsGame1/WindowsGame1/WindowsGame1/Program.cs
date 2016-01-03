using System;

class Program
{
    static void Main()
    {
        string i = "aw";
        TestForW(i);
    }

    public static void TestForW(string i)
    {
        string[] chars = i.Split();

        for (int k = 0; k < i.Length; k++)
        {
            if (chars[k] == "w")
            {
                Console.WriteLine(true);
                return;
            }
        }
        Console.WriteLine(false);
        return;
    }
}