//Declerations
string fullEquation;
string opperators = "^rx/+-";

//Get results
fullEquation = GetEquationWithResults(opperators);

//output
Console.WriteLine(fullEquation);

//Subroutines
string GetEquationWithResults(string opperators)
{
    string userEquation = string.Empty;
    float result = 0;
    bool validResult = false;

    while (!validResult)
    {
        Console.Write("Enter Equation (s for settings): ");
        userEquation = Console.ReadLine().ToLower().Trim();

        if (userEquation == "s")
        {
            displaySettings(opperators);
        }
        
        else
        {
            try
            {
                result = float.Parse(SolveEquation(userEquation, opperators).Trim());
                validResult = true;
            }
            catch (FormatException)
            {
                ErrorMessage("Invaild Input");
            }
        }
    }

    return $"{userEquation} = {result}";
}

string SolveEquation(string equation, string opperators, int dep = 0 )
{
    //Console.WriteLine($"\n{dep}: equation = {equation}");

    int orderOfOp = opperators.Length / 2;
    Queue<int> opIndex = new Queue<int> { };
    opIndex.Enqueue(-1);
    char op = ' ';

    for (int i = 0; i < equation.Length; i++)
    {
        if (opperators.Contains(equation[i]))
        {
            if (orderOfOp > (indexOfAny(opperators, equation[i].ToString()) / 2) || orderOfOp == 0)
            {
                if (opIndex.Count != 1) opIndex.Dequeue();
                opIndex.Enqueue(i);

                orderOfOp = indexOfAny(opperators, equation[i].ToString()) / 2;
                op = equation[i];
            }

            else if (opIndex.Count != 3)
            {
                opIndex.Enqueue(i);
            }
        }
    }

    if (opIndex.Count <= 1)
    {
        return equation;
    }
    else if (opIndex.Count == 2)
    {
        opIndex.Enqueue(equation.Length);
    }

    string NewEquation = "", equationStart, equationEnd;
    float result = 0;
    float[] nums = new float[2] { 0, 0 };

    equationStart = equation.Substring(0, opIndex.Peek() +1);
 
    for (int i = 0; i < 2; i++)
    {
        int startIndex = opIndex.Dequeue() + 1;
        //Console.WriteLine(equation.Substring(startIndex, opIndex.Peek() - startIndex).Trim());
        nums[i] = float.Parse(equation.Substring(startIndex, opIndex.Peek() - startIndex).Trim());
    }

    equationEnd = equation.Substring(opIndex.Peek(), equation.Length - opIndex.Peek());

    if (op == '^')
        result = (float)Math.Pow(nums[0], nums[1]);
    else if (op == 'r')
        result = (float)Math.Pow(nums[0], 1 / nums[1]);
    else if (op == 'x')
        result = nums[0] * nums[1];
    else if (op == '/')
        result = nums[0] / nums[1];
    else if (op == '+')
        result = nums[0] + nums[1];
    else if (op == '-')
    {
        result = nums[0] - nums[1];
        //Console.WriteLine($"op = {op} num0 = {nums[0]} result = {result} num1 = {nums[1]}");
    }
    else
        ErrorMessage("Opperator issue"); 

    NewEquation = $"{equationStart}{result}{equationEnd}";

    return SolveEquation(NewEquation, opperators, dep+1);
    
}

int indexOfAny(string location, string target)
{
    for (int i = 0; i < location.Length; i++)
    {
        if (target.Contains(location[i]))
        {
            return i;
        }
    }

    return -1;

}

void displaySettings(string opperators)
{
    Console.WriteLine("Valid opperators are:");

    foreach (char op in opperators)
    {
        Console.Write($"{op} ");
    }

    Console.WriteLine("\nCannot work with negative numbers and most negative results curently");
}
static void ErrorMessage(string message)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(message);
    Console.ResetColor();
}
