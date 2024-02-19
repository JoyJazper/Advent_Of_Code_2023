using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{

    static String[] input = [];
    static char[,] inputMatrix;
    static int Sum = 0;

    static string[] StringToArray(string inputString)
    {
        string[] words = inputString.Split('\n');
        return words;
    }
       
    public static async Task GetData(string url, string CookieString)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Cookie", CookieString);

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    input = StringToArray(data);
                }
                else
                {
                    Console.WriteLine("Failed to retrieve data. Status code: " + response.StatusCode);
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error retrieving data: " + ex.Message);
            }
        }
    }
    public static void Main(string[] args)
    {
        Console.Clear();
        String sessionID = "session=53616c7465645f5f344a5449077ad5e964028f53e8d6a4d3547c6e5feec6c2c39e2c2b02a2910e99f08e9cdefa219d42570dc4b592c7aa921f6e4528afea2a0e";
        String url = "https://adventofcode.com/2023/day/3/input";
        //string path = Path.Combine(Environment.CurrentDirectory, @"", "Input.txt");
        //Console.WriteLine(path);
        //try 
        //{
        //    input = File.ReadAllLines(path);
        //}
        //catch
        //{
        //    Console.WriteLine("ERNOS : This is wrong path");
        //}
        if(!(input.Length > 0))
        {
            Console.WriteLine("ERNOS : Didn't get input.");
        }
        
        GetData(url, sessionID).Wait();
        Console.WriteLine("ERNOS: size :" + input.Length);
        Console.WriteLine("Sum : " +  Sum);
        
        // create matrix
        inputMatrix = CreateMatrix(input);
        // upload data in matrix
        for(int i = 0; i < inputMatrix.GetLength(0); i++)
        {
            string numC = "0";
            bool wasDigit = false;
            int columnFirst = -1, row = -1;
            Console.WriteLine("Sum : " + Sum);
            // get number as biscuit[]
            for (int j = 0; j < inputMatrix.GetLength(1); j++)
            {
                Console.WriteLine("Sum : " + Sum);
                if (char.IsDigit(inputMatrix[i, j]))
                {

                    if (!wasDigit)
                    {
                        columnFirst = j;
                        row = i;
                        Console.WriteLine("is first number : " + columnFirst + ", row : " + row );
                    }
                    numC += inputMatrix[i, j];
                    wasDigit = true;
                }
                else
                {
                    if (wasDigit)
                    {
                        bool check = CheckIfValid(row, columnFirst, j);

                        if (check)
                        {
                            Sum += GetNumber(row, columnFirst, j);
                            Console.WriteLine("Summing : " + Sum);
                        }
                        
                    }
                    columnFirst = -1;
                    row = -1;
                    wasDigit = false;
                }
            }
            Console.WriteLine("The sum is : " + Sum);
        }
        // check number validity
        // get int from array
        // add to sum.

    }

    public static bool CheckIfValid(int row, int columnFirst, int columnLast)
    {
        // check left
        if(columnFirst > 0)
        {
            if (IsSpecialChar(inputMatrix[row, columnFirst - 1]))
            {
                return true;
            }
        }

        // check right
        if (columnLast < inputMatrix.GetLength(1)-1)
        {
            if (IsSpecialChar(inputMatrix[row, columnLast + 1]))
            {
                return true;
            }
        }

        // check top
        if(row > 0)
            for (int i = columnFirst; i <= columnLast; i++)
            {
                if (IsSpecialChar(inputMatrix[row - 1, i]))
                {
                    return true;
                }
            }

        // check bottom
        if (row < inputMatrix.GetLength(0)-1)
            for (int i = columnFirst; i <= columnLast; i++)
            { 
                if (IsSpecialChar(inputMatrix[row + 1, i]))
                {
                    return true;
                }
            }

        // check diagonals
        if (columnFirst > 0)
        {
            if(row > 0)
                if (IsSpecialChar(inputMatrix[row - 1, columnFirst - 1]))
                {
                    return true;
                }
            if(row < inputMatrix.GetLength(0) - 1)
                if (IsSpecialChar(inputMatrix[row + 1, columnFirst - 1]))
                {
                    return true;
                }
        }
        if (columnFirst < inputMatrix.GetLength(1) - 1)
        {
            if (row > 0)
                if (IsSpecialChar(inputMatrix[row - 1, columnFirst + 1]))
                {
                    return true;
                }
            if (row < inputMatrix.GetLength(0) - 1)
                if (IsSpecialChar(inputMatrix[row + 1, columnFirst + 1]))
                {
                    return true;
                }
        }

        return false;
    }

    public static bool IsSpecialChar(char data)
    {
        if(data != '.')
        {
            if ((int)data < (int)'0' || (int)data > (int)'9')
            {
                Console.WriteLine("Found a valid statement.");
                return true;
            }
        }
        return false;
    }

    static char[,] CreateMatrix(string[] inputMatrix)
    {
        int rows = input.Length;
        int cols = input[0].Length;
        char[,] matrix = new char[rows, cols];

        for (int i = 0; i < rows - 1; i++)
        {
            string temp = input[i];
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = temp[j];
            }
        }

        return matrix;
    }

    static int GetNumber(int row, int columnFirst, int columnLast)
    {
        string result = "";
        for(int i = columnFirst; i <= columnLast - 1; i++){
            result += inputMatrix[row, i];

        }
        return int.Parse(result);
    }
}

public struct Biscuit
{
    
}