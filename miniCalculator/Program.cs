using System.Data;
class Calculator{
    private static string[] nums = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};
    private static string[] mul = { "multiply", "times", "mul", "x", "*"};
    private static string[] div = { "divided_by", "div", "/"};
    private static string[] plus = { "plus", "add", "+"};
    private static string[] subtract = { "subtract", "minus", "sub", "subtr", "-"};
    public static void Main(string[] args){
        bool continueCalc = true;
        StreamWriter history = new StreamWriter("./CalcHistory.txt");
        while (continueCalc)
        {
            Console.WriteLine("[ Pick a Option ]\nA. Read From A File\nB. Self-Input");
            Console.Write("Your Input (A/B): ");
            string? option = Console.ReadLine();
            string? str;
            if(option == null){
                Console.WriteLine("--- Please Choose A Option! ---");
                continue;
            }
            if(option.ToUpper() == "A"){
                Console.Write("Enter File Path: ");
                string? path = Console.ReadLine();
                if(path == null){
                    Console.WriteLine("--- File Path Is Empty. Go Back To Main Menu. ---");
                    continue;
                } else if (!File.Exists(path)){
                    Console.WriteLine("--- File Does Not Exist. Go Back To Main Menu. ---");
                    continue;
                }
                StreamReader readExpr = new StreamReader(path);
                history.WriteLine("========== FILE CALCULATION ==========");
                while(readExpr.Peek() != -1){
                    str = readExpr.ReadLine();
                    Calculation(str, history);
                }
                history.WriteLine("======================================");
                readExpr.Close();
                Console.Write("Do Anothor Calculation? (y/n) ");
                continueCalc = CheckCond(Console.ReadLine());  // check conditions
                Console.WriteLine();
            } else if (option.ToUpper() == "B")
            {
                Console.Write("Enter Your Arithmetic Operations: ");
                str = Console.ReadLine();   // Warning: Converting null literal or possible null value to non-nullable type.
                Calculation(str, history);
                Console.Write("Do Anothor Calculation? (y/n) ");
                continueCalc = CheckCond(Console.ReadLine());  // check conditions
                Console.WriteLine();
            } else{
                Console.WriteLine("--- Invalid Input! Please Try Again. ---");
                continue;
            }
            history.Flush();            // write to file
        }
        history.Close();    // close file
    }
    public static void Calculation(string? str, StreamWriter history)
    {
        if (str != null && str.Length > 0)
        {
            string[] textSplit = str.Split(' ');
            string strExpr;
            if (ContainStr(textSplit))
            {
                List<string> expr = new List<string>();
                for (int i = 0; i < textSplit.Length; i++)
                {
                    expr.Add(ConvertExpr(textSplit[i].ToLower()));
                }
                strExpr = string.Join("", expr);
                Console.WriteLine("[ Read in: " + str + " ] [ You mean: " + strExpr + " ]");
            }
            else
            {
                strExpr = str;
            }
            DataTable data = new DataTable();
            // Compute() a expression and return a Object value
            // Convert Object to Double
            double val = Convert.ToDouble(data.Compute(strExpr, ""));
            string outputAns = strExpr + " = " + Math.Round(val, 6) + "\n-------------------------------";  //rounded to 6 decimals
            Console.WriteLine("-------------------------------");
            Console.WriteLine(outputAns);    // print to terminal
            history.WriteLine(outputAns);   // write to buffer
        }
        else
        {
            Console.WriteLine("--- PLEASE Enter Some Expression ---");
        }
    }
    public static bool CheckCond(string? c)
    {
        if(c == null){
            Console.WriteLine("--- Invalid NULL Input! Program Terminated. ---");
            return false;
        } else if (c.ToUpper()=="Y")
        {
            return true;
        }
        else if (c.ToUpper()=="N")
        {
            Console.WriteLine("--- Bye! ---");
            return false;
        }
        else
        {
            Console.WriteLine("--- Invalid Input! Program Terminated. ---");
            return false;
        }
    }
    public static bool ContainStr(string[] str){
        for (int i = 0; i < str.Length; i++)
        {
            string s = str[i].ToLower();
            if(nums.Contains(s) || mul.Contains(s) || div.Contains(s) || plus.Contains(s) || subtract.Contains(s)){
                return true;
            }
        }
        return false;
    } 
    public static string ConvertExpr(string str){
        double tmp = 0;
        bool isDouble = Double.TryParse(str, out tmp);
        if(mul.Contains(str)){
            return "*";
        } else if(div.Contains(str)){
            return "/";
        } else if(plus.Contains(str)){
            return "+";
        } else if(subtract.Contains(str)){
            return "-";
        } else if(nums.Contains(str) || isDouble){
            if(isDouble){     // is a number
                return str;
            } else      // is a text number
            {
                return ConvertNums(str);
            }
        } else{
            return "";
        }
    }

    public static string ConvertNums(string num){
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == num)
            {
                return "" + i;
            }
        }
        return "";
    }
}