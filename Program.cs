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
        Console.WriteLine(inputString);
        int firstint = -1;
        int secondint = 0;
        char[] chars = inputString.ToCharArray();
        foreach(char c in chars)
        {
            //Console.WriteLine(c + " and toint : " + (int)c);
            if ((int)c <= (int)'9' && (int)c >= (int)'0') {
                if(firstint == -1) 
                    firstint = (int)c - (int)'0';
                secondint = (int)c - (int)'0';
            }
            
        }
        if(firstint == -1) firstint = 0;
        Console.WriteLine("ERNOS: number : " + ((firstint * 10) + secondint));
        return (firstint * 10) + secondint;
    }
}