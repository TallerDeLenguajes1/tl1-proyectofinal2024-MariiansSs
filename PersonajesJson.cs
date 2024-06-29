using Personajes;
using System.Text.Json;

public class PersonajesJson
{
    public FabricaDePersonajes lista;

    public PersonajesJson(FabricaDePersonajes fabricaPersonaje)
    {
        lista = fabricaPersonaje;
    }
    public void GuardarPersonajes(string nombreArchivo)
    {
        string personajesJson = JsonSerializer.Serialize(lista);
        using(var archivo = new FileStream(nombreArchivo, FileMode.Create))
        {
            using(var strWriter = new StreamWriter(archivo))
            {
                strWriter.WriteLine("{0}",lista);
                strWriter.Close();
            }
        }
    }
}