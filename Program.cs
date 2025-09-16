//Declerations
float result = 0;
string equation = string.Empty;
char[] opperators = ['+', '/', 'x', '-', '^', '%'];

//Get results
equation = GetValidEquation(opperators);
result = SolveEquation(equation, opperators);

//output
Console.WriteLine($"{equation} = {result}");


//Subroutines
string GetValidEquation(char[] opperators)
{
    string userEquation = string.Empty;

    while (!isValidEquation(userEquation, opperators))
    {
        Console.Write("Enter Equation: ");
        userEquation = Console.ReadLine().ToLower().Trim();
    }

    return userEquation;
}

bool isValidEquation(string equation, char[] opperators)
{
    int opperatorCount = 0;

    if (equation.Length < 3)
    {
        return false;
    }

    for (int i = 0; i < equation.Length; i++)
    {
        if (equation[i] == ' ')
        {
            if (char.IsDigit(equation[i - 1]) == char.IsDigit(equation[i + 1])) { return false; } // not the most readable work on it- essentualliy (5 +) is ok (6 7) isn't 
        }

        else if (equation[i] == '.')
        {
            if (!char.IsDigit(equation[i - 1]) || !char.IsDigit(equation[i + 1])) { return false; } // not readable either, makes sure either side of the dot is a number 
        }

        else if (!char.IsDigit(equation[i]) && !opperators.Contains(equation[i]))
        {
            Console.WriteLine($"{equation[i]} isn't a number or opperator");
            return false;
        }

        else if (opperators.Contains(equation[i]))
        {
            opperatorCount++;
        }
    }

    if (opperatorCount != 1)
    {
        Console.WriteLine("You can only use one opperator/ you need 1 opperator");
        return false;
    }

    return true;
}

float SolveEquation(string equation, char[] opperators)
{
    float num1, num2;
    int i = 0, opperatorIndex = -1;
    char opperator;

    while (opperatorIndex == -1)
    {
        opperatorIndex = equation.IndexOf(opperators[i]);
        i++;
    }

    opperator = opperators[i - 1];

    num1 = float.Parse(equation.Substring(0, opperatorIndex));
    num2 = float.Parse(equation.Substring(opperatorIndex + 1));

    if (opperator == 'x')
    {
        return num1 * num2;
    }
    else if (opperator == '/')
    {
        return float.Round(num1 / num2, 3);
    }
    else if (opperator == '+')
    {
        return num1 + num2;
    }
    else if (opperator == '-')
    {
        return num1 - num2;
    }
    else if (opperator == '^')
    {
        return float.Round((float)Math.Pow(num1, num2), 3);
    }
    else if (opperator == '%')
    {
        return num1 % num2;
    }
    else
    {
        Console.WriteLine("Invalid Opperator: setting result to -1");
        return -1;
    }
}