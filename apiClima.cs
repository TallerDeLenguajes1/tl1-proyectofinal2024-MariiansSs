namespace climaApi;
using System.Text.Json.Serialization;
using System.Text.Json;
public class Root
{
    [JsonPropertyName("current")]
    public Current Current { get; set; }
}

public class Current
{
    [JsonPropertyName("condition")]
    public Condition Condition { get; set; }
}

public class Condition
{
    [JsonPropertyName("text")]
    public string Text { get; set; }
}

public class servicioClima
{
    // Metodo estatico
    public static async Task<Root> ObtenerClima()
    {
    var url = @"http://api.weatherapi.com/v1/forecast.json?key=cd6f9d741b394a52a8d154939243107&q=Argentina&days=1&aqi=no&alerts=no";
    try
    {
        HttpClient cliente = new HttpClient();
        HttpResponseMessage response = await cliente.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        Root clima = JsonSerializer.Deserialize<Root>(responseBody);
        return clima;

    }
    catch (HttpRequestException e)
    {
        Console.WriteLine("Problemas de acceso a la API");
        Console.WriteLine("Message :{0} ", e.Message);
        return null;
    }
    }
}