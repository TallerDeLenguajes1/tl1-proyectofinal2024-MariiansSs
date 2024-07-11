using PersonajesSpace;
using System.Text.Json;
using helperJson;

public class PersonajesJson
{

    string nombreArchivo = @"C:\TallerPractica\ProyectoFinal\tl1-proyectofinal2024-MariiansSs\Historial.json";
    private HelperDeJson helperArchivos = new HelperDeJson();

    public void GuardarPersonajes(List<Personajes> personaje, string NombreArchivo)
    {
        Console.WriteLine("--Serializando--");
        string personajesJson = JsonSerializer.Serialize(personaje);
        Console.WriteLine("Archivo Serializado : " + personajesJson);
        Console.WriteLine("--Guardando--");
        helperArchivos.GuardarPersonajes(nombreArchivo, personajesJson);
    }

    public List<Personajes> LeerPersonajes(string NombreArchivo)
    {
        Console.WriteLine("--Abriendo--");
        string jsonDocument = helperArchivos.AbrirArchivoTexto(NombreArchivo);
        Console.WriteLine("--Deserializando--");
        var listaPersonajes = JsonSerializer.Deserialize<List<Personajes>>(jsonDocument);
        Console.WriteLine("--Mostrando datos recuperardos--");
        return listaPersonajes;
    }


    public bool Existe(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo))
        {
            // Verifico si el archivo tiene datos
            string contenidoArchivo = File.ReadAllText(nombreArchivo);
            return !string.IsNullOrEmpty(contenidoArchivo); // Controla si el archivo no esta vacio
        }
        return false; //El archivo no existe o está vacío
    }

}