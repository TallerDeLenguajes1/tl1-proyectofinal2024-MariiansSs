using Personajes;
using System.Text.Json;
using helperJson;

public class PersonajesJson
{

    string nombreArchivo = "Personajes.json";
    private HelperDeJson helperArchivos = new HelperDeJson();

    public void GuardarPersonajes(List<Personaje> personaje, string NombreArchivo)
    {
        Console.WriteLine("--Serializando--");
        string personajesJson = JsonSerializer.Serialize(personaje);
        Console.WriteLine("Archivo Serializado : " + personajesJson);
        Console.WriteLine("--Guardando--");
        helperArchivos.GuardarPersonajes(nombreArchivo, personajesJson);
    }

    public List<Personaje> LeerPersonajes(string NombreArchivo)
    {
        Console.WriteLine("--Abriendo--");
        string jsonDocument = helperArchivos.AbrirArchivoTexto(NombreArchivo);
        Console.WriteLine("--Deserializando--");
        var listaPersonajes = JsonSerializer.Deserialize<List<Personaje>>(jsonDocument);
        Console.WriteLine("--Mostrando datos recuperardos--");
        return listaPersonajes;
    }


    public bool Existe(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo))
        {
            // Verificar si el archivo tiene datos
            string fileContent = File.ReadAllText(nombreArchivo);
            return !string.IsNullOrEmpty(fileContent);
        }
        return false; // El archivo no existe o está vacío
    }

}