#region tasks 
using SweeftTasks;
using System.Text.Json;


await GenerateCountryDataFiles();
static bool IsPalindrome(string text)
{
    string emptyString = "";
    foreach (char c in text)
    {
        if (char.IsLetterOrDigit(c))
        {
            emptyString += c;
        }
    }

    emptyString = emptyString.ToLower();

    for (int i = 0; i < emptyString.Length / 2; i++)
    {
        if (emptyString[i] != emptyString[emptyString.Length - i - 1])
        {
            return false;
        }
    }
    return true;
}

// Example result for 80 would be [1 1 1 0 0] meaning that it's 1 (50tetri) 1(20tetri) 1(10tetri) 0 0
static int[] MinSplit(int amount)
{
    int[] coins = { 50, 20, 10, 5, 1 };
    int[] result = new int[coins.Length];
    for (int i = 0; i < coins.Length; i++)
    {
        result[i] = amount / coins[i];
        amount %= coins[i];
    }
    return result;
}


static int NotContains(int[] array)
{
    int n = array.Length;

    int j = 0;
    for (int i = 0; i < n; i++)
    {
        if (array[i] > 0)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
            j++;
        }
    }

    for (int i = 0; i < j; i++)
    {
        int index = Math.Abs(array[i]) - 1;
        if (index < j && array[index] > 0)
        {
            array[index] = -array[index];
        }
    }

    for (int i = 0; i < j; i++)
    {
        if (array[i] > 0)
        {
            return i + 1;
        }
    }

    return j + 1;
}

static bool IsProperly(string sequence)
{
    int count = 0;

    foreach (char c in sequence)
    {
        if (c == '(')
        {
            count++; 
        }
        else if (c == ')')
        {
            count--;
        }

        if (count < 0) 
        {
            return false;
        }
    }

    return count == 0;
}
 static int CountVariants(int stairCount)
{
    if (stairCount <= 0)
    {
        return 0; 
    }
    else if (stairCount == 1)
    {
        return 1; 
    }

    int[] variants = new int[stairCount + 1]; 
    variants[0] = 1;
    variants[1] = 1; 

    for (int i = 2; i <= stairCount; i++)
    {
        variants[i] = variants[i - 1] + variants[i - 2];
    }

    return variants[stairCount]; 
}


static async Task GenerateCountryDataFiles()
{
    using var client = new HttpClient();
    var response = await client.GetAsync("https://restcountries.com/v3.1/all");
    var json = await response.Content.ReadAsStringAsync();
    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    var countries = JsonSerializer.Deserialize<Country[]>(json, options);

    foreach (var country in countries)
    {
        var fileName = $"{country.Name}.txt";
        var fileContent = $"Region: {country.Region}\n" +
                          $"Subregion: {country.Subregion}\n" +
                          $"Latlng: {string.Join(",", country.Latlng)}\n" +
                          $"Area: {country.Area}\n" +
                          $"Population: {country.Population}\n";

        File.WriteAllText(fileName, fileContent);
    }
}

#endregion