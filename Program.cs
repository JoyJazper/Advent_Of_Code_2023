using System;
using System.Net.Http;
using System.Threading.Tasks;
using DotNetEnv;

class Program
{
    static int xMax;
    static int yMax;
    static int zMax;
    static int totalMax;

    static String[] input = [];

    static string[] StringToArray(string inputString)
    {
        string[] words = inputString.Split('\n');
        return words;
    }


    public static async Task GetData(string url, string cookieString)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Cookie", cookieString);

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
        //Env.TraversePath().Load();
        //string? url = Environment.GetEnvironmentVariable("URL");
        //string? cookieString = Environment.GetEnvironmentVariable("SESSION_ID");
        string url = "https://adventofcode.com/2023/day/2/input";
        string cookieString = "session=53616c7465645f5f344a5449077ad5e964028f53e8d6a4d3547c6e5feec6c2c39e2c2b02a2910e99f08e9cdefa219d42570dc4b592c7aa921f6e4528afea2a0e";

        xMax = 12;
        yMax = 13;
        zMax = 14;
        totalMax = 39;

        GetData(url, cookieString).Wait();
        Console.WriteLine("ERNOS: size :" + input.Length);
        int sum = 0;

        foreach(string line in input)
        {
            Console.WriteLine(line);
            string[] games = GetGames(line);
            if(games.Length > 0)
            {
                int GameID = GetGameID(games[0]);
                bool isValid = true;
                foreach (string game in games)
                {
                    Console.WriteLine(game);
                    int x = GetValue(game, "red");
                    int y = GetValue(game, "green");
                    int z = GetValue(game, "blue");
                    if(!IsValid(x, y, z))
                        isValid = false;
                }

                if (isValid)
                {
                    Console.WriteLine("Sum after ID : " + GameID + " is " + sum);
                    sum += GameID;
                }
            }
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

    // parse the string into an array for strings
    // remove game number till : and create a string array with ;
    private static string[] GetGames(string data)
    {
        string[] result = [];
        if (!data.Contains(";") || data == "") return result;
        string[] gameCollection = data.Split(";");
        return gameCollection;
    }

    private static int GetGameID(string game)
    {
        int index = game.IndexOf(":");
        int result = 0;
        char value;
        int multiplier = 0;
        for (int i = index - 1; i > 0; i--)
        {
            if (!(index < game.Length)) break;

            value = game[i];

            if (value == ' ') break;

            if ((int)value <= (int)'9' && (int)value >= (int)'0')
            {
                result += ((int)value - ((int)'0')) * (int)(MathF.Pow(10, multiplier));
            }
            multiplier++;
        }
        Console.WriteLine("GameID : " +  result);
        return result;
    }

    // convert the red into x, blue in y and green in z and create a struct

    private static int GetValue(string game, string id)
    {
        int index = game.IndexOf(id);
        int result = 0;
        char value;
        int multiplier = 0;
        for(int i = index-2; i > 0; i--)
        {
            if (!(index < game.Length)) break;

            value = game[i];
            
            if (value == ' ') break;

            if ((int)value <= (int)'9' && (int)value >= (int)'0')
            {
                result += ((int)value - ((int)'0')) * (int)(MathF.Pow(10,multiplier));
            }
            multiplier++;
        }
        return result;
    }

    // check (x+y+z < total) (x<xmax) (y<ymax) (z<zmax) bool(true / false)
    private static bool IsValid(int x, int y, int z)
    {
        if(!(x+y+z < totalMax))
        {
            return false;
        }
        if(x > xMax)
        {
            return false;
        }
        if(y > yMax)
        {
            return false;
        }
        if(z > zMax)
        {
            return false;
        }
        return true;
    }
}