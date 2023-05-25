static class Pages
{
    static string[] mainMenuItems = new string[]
    {
        "1. Меню шифровок",
        "2. Инструкция",
        "3. Об авторе",
        "q. Выход"
    };
    static readonly string alphRus = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";//TODO Во всех методах заменить алфавиты на этот, об
    public static void MainMenu()
    {
        Console.Clear();//очистка экрана перед работой

        int maxItemLength = 0;
        foreach (string item in mainMenuItems)//расчёт размеров элемента меню
        {
            if (item.Length > maxItemLength)
                maxItemLength = item.Length;
        }

        /*
         * Алгоритм, который будет вычислять разницу между 
         * максимальной длинной пункта меню и текущим для вывода
         * Если она есть, то:
         * ---
         * Формировать строку следующим образом:
         * Писать название текущего пункта меню
         * Заполнять оставшееся пространство до максимума пробелами
         * ---
         * Затем будет рисовать стенку, отступать от неё {spaceWidth} пробела
         * Писать сформированную строку
         * Отступать {spaceWidth} пробела и расовать стенку
         * 
         * цикл для всего mainMenuItems[]
         */

        Console.WriteLine("\u2554 Главное меню \u2557");
        foreach (string item in mainMenuItems)//вывод меню
        {
            int deltaWidth = maxItemLength - item.Length,
                borderSpacesNum = 3;

            string borderSpaces = "",
                   itemWithSpaces = item;

            if (deltaWidth > 0)
                itemWithSpaces = item + borderSpaces;

            borderSpaces = string.Join("", Enumerable.Repeat(" ", borderSpacesNum));
            // TODO сделать нормальные отступы Console.WriteLine("\u2551" + borderSpaces + itemWithSpaces + borderSpaces + "\u2551");
            Console.WriteLine("\u2551" + borderSpaces + itemWithSpaces);
        }

        Console.WriteLine("Введите номер пункта меню: ");

        string input_num = Console.ReadLine();

        switch (input_num)
        {
            case "1":
                Console.WriteLine("Загрузка меню шифровок...");
                EncryptionMethodSelectionMenu();
                break;
            case "2":
                break;
            default:
                Console.WriteLine("Ошибка набора, повторите ввод: ");
                Console.WriteLine("---В разработке, требуется перезапуск---");

                break;
        }
    }
    static void EncryptionMethodSelectionMenu()
    {
        Console.Clear();//очистка экрана перед работой


        string input_num = "";

        string[] encryptionMethodsList = new string[]
        {
            "1.Шифр гаммирования",
            "2. Генерация гаммы через регистр сдвига с линейной обратной связью",
            "3. Генерация гаммы методом BBS",
            "4. Шифр ADFGVX",
            "5. Шифр Эль-Гамаля",
            "6. Хэш md5"
        };

        foreach (string method in encryptionMethodsList)
        {
            Console.WriteLine(method);
        }
        Console.Write("Выберите способ работы: ");
        input_num = Console.ReadLine();

        switch (input_num)
        {
            case "1":
                Console.WriteLine("Загрузка шифра гаммирования...");
                CipherSelectionMenu();
                break;
            case "2":
                Console.WriteLine("Загрузка генерации гаммы через РСЛОС...");
                RlosGammaGenerator();
                break;
            case "3":
                Console.WriteLine("Загрузка генератора гаммы по ББС...");
                BbsGammaGenerator();
                break;
            case "4":
                Console.WriteLine("Загрузка шифра ADFGVX...");
                ADFGVXCipher();
                break;
            case "5":
                Console.WriteLine("Загрузка шифра Эль-Гамаля...");
                ElGamalCipherEncrypt();
                break;
            case "6":
                Console.WriteLine("Загрузка md5 хэша...");
                MD5Hash();
                break;
            default:
                break;
        }
    }

    //vvv--- Шифры в один метод ---vvv
    static void ADFGVXCipher()
    {
        string[,] replaceTable = new string[7, 7];
        string alph = "МНЛФХЦЧШТОПРСУЭЮЯДЕЁЖАБВГЗИЙКЩЪЫЬ---"; //TODO Прикрутить рандомизацию алфавита

        replaceTable[0, 1] = "A";
        replaceTable[0, 2] = "D";
        replaceTable[0, 3] = "F";
        replaceTable[0, 4] = "G";
        replaceTable[0, 5] = "V";
        replaceTable[0, 6] = "X";

        replaceTable[1, 0] = "A";
        replaceTable[2, 0] = "D";
        replaceTable[3, 0] = "F";
        replaceTable[4, 0] = "G";
        replaceTable[5, 0] = "V";
        replaceTable[6, 0] = "X";

        int alph_i = 0; //итератор для перебора строки
        for (int i = 1; i < 7; i++) //TODO Получать длины динамически
        {
            for (int j = 1; j < 7; j++)
            {
                replaceTable[i, j] = alph[alph_i].ToString();
                Console.Write(alph[alph_i]);
                alph_i++;
            }
            Console.WriteLine();
        }

        Console.WriteLine("Введите открытое сообщение: ");
        string openMsg = Console.ReadLine();
        string replacedMsg = "";
        alph_i = 0;
        while (alph_i != openMsg.Length * 2)
        {
            for (int i = 1; i < 7; i++) //TODO Получать длины динамически
            {
                for (int j = 1; j < 7; j++)
                {
                    if (replaceTable[i, j] == openMsg[alph_i].ToString())
                    {
                        replacedMsg += replaceTable[i, 0] + replaceTable[0, j]; //Нужно руками проверить таблицу
                        alph_i++;
                        Console.WriteLine(replacedMsg);
                    }
                }
            }
        }

        Console.WriteLine("---");
        Console.WriteLine(replacedMsg);
        Console.ReadLine();
    }
    //^^^--- 

    //vvv--- Генераторы ---vvv
    static void BbsGammaGenerator()//TODO Полностью переделать
    {
        int p = 11, q = 23, m = 253, xStart = 109, xCurrent;
        string gamma = $"{xStart}";
        xCurrent = xStart;
        Console.Write($"Введите необходимаю длину гаммы: ");
        int gammaLength = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("# |  Dec-code  |  Bin-code  | Paritet bit");
        for (int i = 0; i < gammaLength; i++)
        {
            Console.WriteLine($"{i}. | {xCurrent} | {Convert.ToString(xCurrent, 2)} | {CalcParitetBit(xCurrent)} |");
            gamma += CalcParitetBit(xCurrent);
            xCurrent = CalcNextX(xCurrent, m);
        }

        Console.ReadLine();

        static int CalcNextX(int prevX, int m)
        {
            return (prevX * prevX) % m;
        }
        static int CalcParitetBit(int num)
        {
            string numBin = Convert.ToString(num, 2);
            int edinicNum = 0;
            foreach (char item in numBin)
            {
                if (item == '1')
                    edinicNum++;
            }
            if (edinicNum % 2 == 0)
            {
                return 0;
            }
            else return 1;
        }
    }
    static void RlosGammaGenerator() //TODO Расширить на любую длинну гаммы, если возможно вычислять по формуле
    {
        string initialize_val;
        int gammaLength;
        int[] oldRegister;
        int[] newRegister;
        int[] gamma;

        Console.Write("Введите необходимую длину гаммы (не меньше 1): ");
        gammaLength = Convert.ToInt32(Console.ReadLine());
        if (gammaLength < 1)
            gammaLength = 1;
        oldRegister = new int[gammaLength];
        newRegister = new int[gammaLength];
        gamma = new int[gammaLength];

        Console.Write($"Введите инициализирующее значение ({gammaLength} нулей и единиц): ");
        initialize_val = Console.ReadLine();
        for (int i = 0; i < oldRegister.Length; i++)
        {
            oldRegister[i] = Convert.ToInt32(initialize_val[i].ToString());
        }

        for (int j = 0; j < gammaLength; j++)
        {
            Console.WriteLine($"Исходное состояние регистра: ");
            foreach (int item in oldRegister)
                Console.Write(item);

            Console.WriteLine();

            Console.WriteLine("Вычисления: ");
            Array.Reverse(oldRegister);
            int calc_symb = (oldRegister[4] % 2) ^ (oldRegister[3] % 2) ^ (oldRegister[2] % 2) ^ (oldRegister[0] % 2);

            Console.WriteLine($"{j}. {oldRegister[4] % 2} + {oldRegister[3] % 2} + {oldRegister[2] % 2} + {oldRegister[0] % 2} = {calc_symb}");
            Array.Reverse(oldRegister); //TODO Нужно изначально записывать в массив инвертированную гамму, чтобы не делать это два раза

            if (calc_symb % 2 == 0)
                calc_symb = 0;
            else calc_symb = 1;

            newRegister[0] = calc_symb;
            for (int i = 1; i < oldRegister.Length; i++)
            {
                newRegister[i] = oldRegister[i - 1];
            }

            gamma[j] = oldRegister[oldRegister.Length - 1];

            for (int i = 0; i < oldRegister.Length; i++) //Clone()?
            {
                oldRegister[i] = newRegister[i];
            }


            Console.WriteLine("Изменённое состояние регистра: ");
            foreach (int item in newRegister)
            {
                Console.Write(item);
            }
            Console.WriteLine("\n------------------------------------");
        }

        Console.Write($"Итоговая гамма: ");
        foreach (int item in gamma)
            Console.Write(item);
        Console.ReadLine();
    }
    //^^^---

    //vvv--- Шифр Цезаря ---vvv
    static void CipherSelectionMenu()
    {
        Console.Clear();//очистка экрана перед работой


        string[] methodsList = new string[]
        {
            "1. Единичный рассчёт для шифрования гаммированием по модулю N",
            "2. Полный вывод шифрования гаммированием по модулю N = 33, для русского языка",
            "3. Полный вывод шифрования гаммированием по модулю 2, для русского языка"
        };

        foreach (string method in methodsList)
        {
            Console.WriteLine(method);
        }
        Console.Write("Выберите способ работы: ");

        string input_num = Console.ReadLine();

        switch (input_num)
        {
            case "1":
                GammaCipherN();
                break;
            case "2":
                MassGammaCipherRusN();
                //MassGammaCipherRusNmk2();
                break;
            case "3":
                MassGammaCipherBin();
                break;
            case "4":
                break;
            default:
                break;
        }
    }
    static void GammaCipherN()
    {
        Console.Clear();//очистка экрана перед работой

        string? input = null;
        Console.WriteLine("Введите N (33 для русского языка):");
        Console.WriteLine("Внимание! На ввод принимаются числа, а не символы.");
        int alphNum = Convert.ToInt32(Console.ReadLine());
        while (input != "q")
        {
            Console.Write("Введите значение Pi: ");
            int srcSymbNum = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите значение Ki: ");
            int gammaSymbNum = Convert.ToInt32(Console.ReadLine());

            Console.Write("Результат Ci: ");
            int cryptoSymbNum = (srcSymbNum + gammaSymbNum) % alphNum;
            Console.WriteLine(cryptoSymbNum);

            Console.WriteLine("Введите q для выхода или иное значение для продолжения:");
            input = Console.ReadLine();
        }
    }
    static void MassGammaCipherRusN()
    {
        Console.Clear();//очистка экрана перед работой

        //string alphRus = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        string openMsg;
        string gamma;
        string encryptMsg;
        int[] openMsgCode;
        int[] gammaCode;
        int[] encryptMsgCode;

        Console.WriteLine("Введите открытое сообщение(Pi): ");
        openMsg = Console.ReadLine().Trim().ToUpper();
        openMsgCode = CalcCodeArr(alphRus, openMsg);

        Console.WriteLine("Введите гамму(Ki): ");
        gamma = Console.ReadLine().Trim().ToUpper();

        if (gamma.Length < openMsg.Length)
            for (int i = 0; i < openMsg.Length - gamma.Length; i++)
                gamma += gamma[i];
        else if (gamma.Length > openMsg.Length)
        {
            gamma.Remove(gamma.Length - openMsg.Length);
        }
        gammaCode = new int[gamma.Length];

        for (int i = 0; i < gamma.Length; i++)
        {
            for (int j = 0; j < alphRus.Length; j++)
            {
                if (gamma[i] == alphRus[j])
                {
                    gammaCode[i] = j;
                }
            }
        }

        Console.WriteLine("Подробный расчёт для каждой пары: ");
        encryptMsgCode = new int[openMsgCode.Length];
        for (int i = 0; i < openMsgCode.Length; i++)
        {
            encryptMsgCode[i] = (openMsgCode[i] + gammaCode[i]) % alphRus.Length;
            Console.WriteLine($"{i + 1}. {encryptMsgCode[i]} = {openMsgCode[i]} + {gammaCode[i]} mod {openMsgCode.Length}");
        }

        Console.WriteLine($"Pi: \u2551{string.Join('\u2551', openMsgCode)}");
        Console.WriteLine($"Ki: \u2551{string.Join('\u2551', gammaCode)}");
        Console.WriteLine($"Ci: \u2551{string.Join('\u2551', encryptMsgCode)}");

        string temp = " ";
        for (int i = 0; i < encryptMsgCode.Length; i++)
        {
            temp += alphRus[encryptMsgCode[i]];
        }
        encryptMsg = temp.Trim();
        Console.WriteLine($"Итоговый шифротекст: {encryptMsg}");

        Console.WriteLine("Нажмите enter для возврата в главное меню");
        Console.ReadLine();

        static int[] CalcCodeArr(string alph, string msg)
        {
            int[] msgCode = new int[msg.Length];
            for (int i = 0; i < msg.Length; i++)
            {
                for (int j = 0; j < alph.Length; j++)
                {
                    if (msg[i] == alph[j])
                    {
                        msgCode[i] = j;
                    }
                }
            }
            return msgCode;
        }
    }
    static void MassGammaCipherRusNmk2()//рефакторинг метода MassGammaCipherRusN()
    {
        Console.Clear();//очистка экрана перед работой

        string alph = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

        int[] encryptMsgCode;
        int[] openMsgCode;
        int[] gammaCode;

        string encryptMsg;

        Console.WriteLine("Введите открытое сообщение(Pi): ");
        string openMsg = Console.ReadLine(); //TODO Прикрутить обработку ошибок ввода

        Console.WriteLine("Введите гамму(Ki): ");
        string gamma = Console.ReadLine();

        openMsgCode = CalcMsgCode(alph, openMsg);
        gammaCode = CalcGammaCode(openMsg, gamma, alph);
        encryptMsgCode = CalcEncryptMsgCode(openMsgCode, gammaCode, alph);

        Console.WriteLine("Таблица кодов: ");
        Console.WriteLine($"Pi: \u2551{string.Join('\u2551', openMsgCode)}");
        Console.WriteLine($"Ki: \u2551{string.Join('\u2551', gammaCode)}");
        Console.WriteLine($"Ci: \u2551{string.Join('\u2551', encryptMsgCode)}");

        Console.WriteLine("Подробные расчёты для каждого символа: ");
        for (int i = 0; i < openMsgCode.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {encryptMsgCode[i]} = {openMsgCode[i]} + {gammaCode[i]} mod {openMsgCode.Length}");
        }

        Console.ReadLine();

        static int[] CalcMsgCode(string msg, string alph)
        {
            int[] msgCode = new int[msg.Length];
            for (int i = 0; i < msg.Length; i++)
            {
                for (int j = 0; j < alph.Length; j++)
                {
                    if (msg[i] == alph[j])
                    {
                        msgCode[i] = j;
                    }
                }
            }
            return msgCode;
        }
        static int[] CalcGammaCode(string openMsg, string gamma, string alph) //TODO разбить на два метода и вынести метод равнения гаммы, переработав его в метод равнения длинны второй строки к первой, через повторение символов второй строки
        {
            if (gamma.Length > openMsg.Length)
                gamma.Remove(gamma.Length - openMsg.Length);
            else if (gamma.Length < openMsg.Length)
                gamma += gamma.Reverse()
                              .ToString()
                              .Remove(openMsg.Length - gamma.Length)
                              .Reverse()
                              .ToString();

            return CalcMsgCode(alph, gamma);
        }
        static int[] CalcEncryptMsgCode(int[] openMsgCode, int[] gammaCode, string alph)
        {
            int[] encryptMsgCode = new int[openMsgCode.Length];
            for (int i = 0; i < openMsgCode.Length; i++)
            {
                encryptMsgCode[i] = (openMsgCode[i] + gammaCode[i]) % alph.Length;
            }
            return encryptMsgCode;
        }
    }//TODO доделать, сравнить результаты с исходником
    static void MassGammaCipherBin()
    {
        string alph = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

        string openMsg;
        string gamma;
        string encryptMsg;
        int[] openMsgCode;
        int[] gammaCode;
        int[] encryptMsgCode;

        // XOR по инту даёт тот же результат, что и по бинарному числу

        Console.Write("Введите открытое сообщение: ");
        openMsg = Console.ReadLine().ToUpper(); // TODO Сделать проверку ввода
        openMsgCode = CalcCodeArr(alph, openMsg);

        Console.Write("Введите гамму(ключ): ");
        gamma = Console.ReadLine().ToUpper(); // TODO Сделать проверку ввода

        //--------Кусок, который следует вынести в общий метод-------
        if (gamma.Length < openMsg.Length)
            for (int i = 0; i < openMsg.Length - gamma.Length; i++)
                gamma += gamma[i];
        else if (gamma.Length > openMsg.Length)
        {
            gamma.Remove(gamma.Length - openMsg.Length);
        }
        //------------------------------------------------------------
        gammaCode = CalcCodeArr(alph, gamma);

        encryptMsgCode = new int[openMsg.Length];
        for (int i = 0; i < openMsg.Length; i++)
        {
            encryptMsgCode[i] = openMsgCode[i] ^ gammaCode[i];
        }

        Console.WriteLine($"Pi: ");
        foreach (int item in openMsgCode)
        {
            Console.Write($"\u2551 {Convert.ToString(item, 2)}"); //работает, но двоичный вид не совпадает с кодировкой
        }
        Console.WriteLine();

        Console.WriteLine($"Ki: \u2551{string.Join('\u2551', gammaCode)}");

        Console.WriteLine($"Ci: \u2551{string.Join('\u2551', encryptMsgCode)}");

        Console.WriteLine($"Шифрограмма в десятичном виде: ");
        Console.ReadLine();

        static int[] CalcCodeArr(string alph, string msg) // TODO следует вынести в общий метод
        {
            int[] msgCode = new int[msg.Length];
            for (int i = 0; i < msg.Length; i++)
            {
                for (int j = 0; j < alph.Length; j++)
                {
                    if (msg[i] == alph[j])
                    {
                        msgCode[i] = j;
                    }
                }
            }
            return msgCode;
        }
    }
    //^^^---

    //vvv--- Шифр Эль-Гамаля
    //TODO Реализовать все необходимые методы для полной реализации шифра Эль-Гамаля
    static void ElGamalCipherEncrypt() //шифровние по алгоритму Эль-Гамаля
    {
        string openMsg, input = " ";
        double k = 36, a, b;
        double p = 53/*37*/, g = 8/*2*/, y = 35/*32*/, x = 3/*5*/; //TODO Запрашивать параметры у пользователя
        double[] openMsgCodes, encryptMsgA, encryptMsgB;

        Console.Clear();
        Console.WriteLine("Введите открытое сообщение (Т): ");
        openMsg = Console.ReadLine();
        openMsgCodes = new double[openMsg.Length];

        for (int i = 0; i < openMsgCodes.Length; i++)//Заполняем массив алфавитными номерами символов
        {
            for (int j = 0; j < alphRus.Length; j++)
            {
                if (openMsg[i] == alphRus[j])
                {
                    openMsgCodes[i] = j;
                }
            }
        }
        for (int i = 0; i < openMsg.Length; i++) //Выводим получившиеся пары символ-номер
        {
            Console.Write($"{openMsgCodes[i] + 1} - {openMsg[i]} || ");
        }

        Random random = new Random();
        encryptMsgA = new double[openMsg.Length];
        encryptMsgB = new double[openMsg.Length];
        Console.Write("\nk: | ");
        for (int i = 0; i < openMsgCodes.Length; i++)//Формируем шифротекст
        {
            //k = random.Next(1, (int)(p - 1));
            Console.Write(k + " | ");
            encryptMsgA[i] = Math.Pow(g, k) % p;
            double temp = (openMsgCodes[i] + 1);
            double temp1 = Math.Pow(y, k); //TODO Не хватает длины числа, отсюда идёт ошибка вычислений
            double temp2 = temp1 * temp;
            encryptMsgB[i] = temp2 % p;
        }

        Console.Write("\nT: | ");
        foreach (double opnMsgItem in openMsgCodes)
        {
            Console.Write((opnMsgItem + 1) + " | ");
        }
        Console.Write("\nA: | ");
        foreach (double encrAItem in encryptMsgA)
        {
            Console.Write(encrAItem + " | ");
        }
        Console.Write("\nB: | ");
        foreach (double encrBItem in encryptMsgB)
        {
            Console.Write(encrBItem + " | ");
        }

        ElGamalCipherDecryptSymb(encryptMsgA, encryptMsgB, p, x);
        Console.ReadLine();

    } //TODO Убрать дешифровку и переписать в метод с параметрами и возвращаемым значением
    static void ElGamalCipherDecryptSymb(double[] encryptMsgA, double[] encryptMsgB, double p, double x) //TODO Сделать возвращаемое значение
    {
        int[] decryptedMsgCodes = new int[encryptMsgA.Length];
        Console.WriteLine("Дешифровка: ");
        Console.Write("(a^x)^-1: ");
        for (int i = 0; i < encryptMsgA.Length; i++)
        {
            double aPow = Math.Pow(encryptMsgA[i], p - 1 - x);
            Console.Write(aPow + " | ");
            decryptedMsgCodes[i] = (int)(encryptMsgB[i] * aPow % p);
            //decryptedMsgCodes[i] = Convert.ToInt32(encryptMsgB[i] * (1 / Math.Pow(encryptMsgA[i], x)) % p);
        }

        Console.Write("\nT: ");
        foreach (double symbNum in decryptedMsgCodes)
        {
            Console.Write(alphRus[(int)symbNum - 1] + " | ");
        }
    }
    //^^^--- 

    //vvv--- md5 хэширование -- 
    //TODO заменить эту голимую чушь нормальным кодом
    static void MD5Hash() //хэширование набора символов алгоритмом MD5
    {
        Console.Clear();//очистка экрана перед работой

        Console.WriteLine("Введите символы, для хэширования: ");
        string srcA, srcB, srcC, srcD;
        int roundNum;
        Console.ReadLine();
    }
    static string SummMod2in32(string num1, string num2)
    {
        string numResult = "";
        if (num1.Length > num2.Length)//Добавление снезначащих нулей слева
        {
            num2 = AddNonAffectingZeros(num2, num1.Length - num2.Length);
        }
        else if (num1.Length < num2.Length)
        {
            num1 = AddNonAffectingZeros(num1, num2.Length - num1.Length);
        }

        if (num1.Length == num2.Length)
        {
            num1 = ReversArr(num1);
            num2 = ReversArr(num2);
            numResult = "";
            string leftAddNum = "0";
            for (int i = 0; i < num1.Length; i++)
            {
                if (num1[i] == '0' && num2[i] == '0')
                {
                    if (leftAddNum == "1")
                    {
                        numResult += "1";
                        leftAddNum = "0";
                    }
                    else if (leftAddNum == "0")
                    {
                        numResult += "0";
                    }
                }
                else if (num1[i] == '1' && num2[i] == '1')
                {
                    if (leftAddNum == "1")
                    {
                        numResult += "1";
                        leftAddNum = "1";
                    }
                    else
                    {
                        numResult += "0";
                        leftAddNum = "1";
                    }
                }
                else
                {
                    if (leftAddNum == "1")
                    {
                        numResult += "0";
                    }
                    else if (leftAddNum == "0")
                    {
                        numResult += "1";
                    }
                }
            }

            if (leftAddNum == "1")
                numResult += "1";
            numResult = ReversArr(numResult);
        }
        return numResult;
    }
    static decimal CalcK(int roundNum, int iteratNum)
    {

    }
    /// <summary>
    /// Возвращает перевёрнутую строку
    /// </summary>
    /// <param name="srcArr"></param>
    /// <returns></returns>
    static string ReversArr(string srcArr)
    {
        char[] reversArr = new char[srcArr.Length];
        for (int i = 0; i < srcArr.Length; i++)
        {
            reversArr[i] = srcArr[srcArr.Length - 1 - i];
        }
        string output = "";
        foreach (char item in reversArr)
        {
            output += item;
        }
        return output;
    }
    /// <summary>
    /// Добавляет не значащие нули слева
    /// </summary>
    /// <param name="number"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    static string AddNonAffectingZeros(string number, int length)
    {
        number = ReversArr(number);
        for (int i = 0; i < length - number.Length + 1; i++)
        {
            number += "0";
        }
        number = ReversArr(number);
        return number;
    }
    static string ByteAnd(string num1, string num2)
    {
        if (num1.Length > num2.Length)
            num2 = AddNonAffectingZeros(num2, num1.Length - num2.Length);
        else if (num2.Length > num1.Length)
            num1 = AddNonAffectingZeros(num1, num2.Length - num1.Length);

        string result = "";
        for (int i = 0; i < num1.Length; i++)
        {
            if (num1[i] == '1' && num2[i] == '1')
                result += "1";
            else result += "0";
        }
        return result;
    }
    static string ByteOr(string num1, string num2)
    {
        if (num1.Length > num2.Length)
            num2 = AddNonAffectingZeros(num2, num1.Length - num2.Length);
        else if (num2.Length > num1.Length)
            num1 = AddNonAffectingZeros(num1, num2.Length - num1.Length);

        string result = "";
        for (int i = 0; i < num1.Length; i++)
        {
            if (num1[i] == '0' && num2[i] == '0')
                result += "0";
            else result += "1";
        }
        return result;
    }
    static string ByteNot(string num1)
    {
        string result = "";
        for (int i = 0; i < num1.Length; i++)
        {
            if (num1[i] == '0')
                result += "1";
            else result += "0";
        }
        return result;
    }
    static string RoundFunc_F(string B, string C, string D)
    {
        string result = "";
        result = ByteOr(
                        ByteAnd(B, C),
                        ByteAnd(ByteNot(B), D)
                            );
        return result;
    }
    static string RoundFunc_G(string B, string C, string D)
    {
        string result = "";
        result = ByteOr(
                        ByteAnd(B, D),
                        ByteAnd(ByteNot(D), C)
                            );
        return result;
    }
    static string RoundFunc_H(string B, string C, string D)
    {
        string result = "";
        result = SummMod2in32(B, C);
        result = SummMod2in32(result, D);
        return result;
    }
    static string RoundFunc_I(string B, string C, string D)
    {
        string result = "";
        result = SummMod2in32(C, ByteOr( ByteNot(D), B) ); 
        return result;
    }
    static string FromBinToHex(string num)
    {
        int targetLength = num.Length;
        if (targetLength % 4 != 0)
        {
            while (targetLength % 4 != 0)
                targetLength++;
            num = AddNonAffectingZeros(num, targetLength);
        }

        string[,] SymbsTable = new string[,] { { "0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111", "1000", "1001","1010","1011","1100","1101","1110","1111"} , 
                                               { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F"} 
                                             };
        string result = "";
        string item = "";
        for (int i = 0; i < num.Length; i += 4)
        {
            item = num[i].ToString() + num[i + 1].ToString() + num[i + 2].ToString() + num[i + 3].ToString();
            for (int j = 0; j < 16; j++)
            {
                if (item == SymbsTable[0, j])
                {
                    result += SymbsTable[1, j].ToString();//?
                    //break;
                } 
            }
        }
        return result;
    }
    static string RoundOne(string A, string B, string C, string D) 
    {
        string result = "";
        for (int i = 0; i < 16; i++)
        {

        }
        return result;
    }
    //^^^---
}