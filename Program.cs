using System;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
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
        String url = "https://adventofcode.com/2023/day/1/input";
        GetData(url, sessionID).Wait();
        Console.WriteLine("ERNOS: size :" + input.Length);
        int sum = 0;
        foreach (string input in input)
        {
            sum += GetValueFromString(input);             
        }
        Console.WriteLine("Sum : " +  sum);
    }

    static int GetValueFromString(string inputString)
    {
        Console.WriteLine("ERNOS : Incoming : " + inputString);
        int newint = 0;
        StringChecker checker = new StringChecker();
        newint += checker.GetCode(inputString.ToCharArray());
        return newint;
    }
}

public class StringChecker
{
    public int GetCode(char[] chars)
    {
        string debug = new string(chars);
        Console.WriteLine(debug);
        int firstInt = -1;
        int lastInt = -1;
        // traverse all the char 
        for (int i = 0; i < chars.Length; i++)
        {
            if (firstInt != -1)
                break;

            foreach (string check in stringNumbers.Keys)
            {
                if (i + check.Length - 1 < chars.Length)
                {
                    char[] newArray = new char[check.Length];
                    Array.Copy(chars, i, newArray, 0, check.Length);
                    bool isMatch = IsMatch(newArray, check.ToCharArray());

                    if (isMatch)
                    {
                        firstInt = stringNumbers[check];
                        break;
                    }
                }
            }
        }

        for (int i = 0; i < chars.Length; i++)
        {
            if (lastInt != -1)
                break;

             foreach (string check in stringNumbers.Keys)
             {

                 int j = chars.Length - i - 1;
                 if ((j + 1) - check.Length >= 0)
                 {
                     char[] newArray = new char[check.Length];
                     Array.Copy(chars, (j + 1) - check.Length, newArray, 0, check.Length);
                     bool isMatch = IsMatch(newArray, check.ToCharArray());
                     if (isMatch)
                     {
                         lastInt = stringNumbers[check];
                         break;
                     }
                 }
             }
        }
        if(firstInt != -1 &&  lastInt != -1)
        //if(firstInt != -1)
        {
            return ((firstInt*10) + lastInt);
            // return ((firstInt*10));
        }

        return 0;
    }



    // check if a match
    private bool IsMatch(char[] a, char[] b)
    {
        if(a.Length == b.Length)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if(a[i] != b[i])
                {
                    return false;
                } 
            }
            return true;
        }
        return false;
    }


    private static Dictionary<string, int> stringNumbers = new Dictionary<string, int>
    {
        {"zero", 0},
        {"one", 1},
        {"two", 2},
        {"three", 3},
        {"four", 4},
        {"five", 5},
        {"six", 6},
        {"seven", 7},
        {"eight", 8},
        {"nine", 9},
        {"0", 0},
        {"1", 1},
        {"2", 2},
        {"3", 3},
        {"4", 4},
        {"5", 5},
        {"6", 6},
        {"7", 7},
        {"8", 8},
        {"9", 9}
    };
}