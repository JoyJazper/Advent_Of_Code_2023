using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{

    static String[] input = [];

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
        GetData(url, sessionID).Wait();
        Console.WriteLine("ERNOS: size :" + input.Length);
        int sum = 0;
        Console.WriteLine("Sum : " +  sum);
        char[,] inputMatrix;
        // create matrix
        inputMatrix = CreateMatrix(input);
        // upload data in matrix
        for(int i = 0; i < inputMatrix.GetLength(0); i++)
        {
            int number = -1;
            string numC = "0";
            bool wasDigit = false;
            
        // get number as biscuit[]
            for(int j = 0; j < inputMatrix.GetLength(1); j++)
            {
                if (char.IsDigit(inputMatrix[i, j]))
                {
                    numC += inputMatrix[i, j];
                    wasDigit = true;
                }
                else
                {
                    if (wasDigit)
                    {
                        number = int.Parse(numC);
                        
                    }
                    wasDigit = false;
                }
            }

        }
        // check number validity
        // get int from array
        // add to sum.

    }

    static char[,] CreateMatrix(string[] inputMatrix)
    {
        int rows = inputMatrix.Length;
        int cols = inputMatrix[0].Length;
        char[,] matrix = new char[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            string temp = input[i];
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = temp[j];
            }
        }

        return matrix;
    }


}

public struct Biscuit
{
    int row = -1;
    int col = -1;
    int number = -1;
}