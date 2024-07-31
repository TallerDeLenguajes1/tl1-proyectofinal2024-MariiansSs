namespace climaApi;
using System.Text.Json.Serialization;
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