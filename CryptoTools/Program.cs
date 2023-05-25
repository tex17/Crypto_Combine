internal class Program
{
    private static void Main(string[] args)
    {
        CalcGez();
    }
    // Вроде бы это RSA
    public static void CalcG()
    {
        double g = 2, p = 37, res = 0;
        while (res != 1 && isGPrimitive(g,p))
        {
            g++;
            res = Math.Pow(g, (p - 1)) % p;
            Console.WriteLine($"----| g = {g} |----");
            Console.WriteLine($"aaa{g}^{p-1}mod{p}={res}");
        }
        Console.WriteLine($"Ответ: g = {g}");

        bool isGPrimitive(double g, double p) //проверка на то, что [g^i mod p != 1] для [i от 1 до p-1]
        {
            double resPri = 0;
            Console.WriteLine("----| степени до p-1 |----");
            for (int i = 1; i < (p-1); i++)
            {
                resPri = Math.Pow(g, i) % p;
                Console.WriteLine($"{g}^{i}mod{p}={resPri}");
                if (resPri == 1)
                {
                    Console.WriteLine($"При g = {g} проверка не пройдена.");
                    return false;
                }
                    
            }
            Console.WriteLine($"При g = {g} проверка пройдена. Остаток 1, при i < p-1 отсутствует.");
            return true;
        }
    }
    public static void CalcGez()
    {
        double g = 0, p = 0, res = 0;
        Console.Write("Введите p = ");
        p = Convert.ToDouble(Console.ReadLine());
        Console.Write("Введите gLimit = ");
        double gL = Convert.ToDouble(Console.ReadLine());
        while (g != gL)
        {
            g++;
            res = Math.Pow(g, (p - 1)) % p;
            Console.WriteLine($"----| g = {g} |----");
            Console.WriteLine($"aaa{g}^{p - 1}mod{p}={res}");
            if (res == 1)
                isGPrimitive(g, p);
        }
        //Console.WriteLine($"Ответ: g = {g}");

        void isGPrimitive(double g, double p)
        {
            double resPri = 0;
            Console.WriteLine("----| степени до p-1 |----");
            for (int i = 1; i < (p - 1); i++)
            {
                resPri = Math.Pow(g, i) % p;
                Console.WriteLine($"{g}^{i}mod{p}={resPri}");
                if (resPri == 1)
                {
                    Console.WriteLine($"Обнаружена единица в остатке.");
                    return;
                }
            }
        }
    }
    private static void TrisemusCipher()
    {
        string rusAlphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        string[,] replacementTable;
        Console.WriteLine("Шифр Трисемуса");
        Console.WriteLine("Введите кодовое слово или фразу, на русском языке: ");
        string? keyword = null;
        while (keyword == null)
        {
            keyword = Console.ReadLine();
            if (keyword == null)
            {
                Console.WriteLine("Введено пустое слово. Повторите ввод.");
            }
            else
            {
                keyword.ToUpper();
                //TODO Отбросить повторяющиеся буквы
                //TODO Убрать пробелы
            }

        }
        replacementTable = new string[keyword.Length, keyword.Length];
    }
}