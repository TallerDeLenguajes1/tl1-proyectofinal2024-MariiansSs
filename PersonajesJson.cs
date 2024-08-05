namespace PersonajesJS;
using PersonajesSpace;
using System.Text.Json;
using helperJson;



public class PersonajesJson
{
    string nombreArchivo {get;}
    private HelperDeJson helperArchivos = new HelperDeJson();

    public PersonajesJson(string NombreArchivo)
    {
        this.nombreArchivo = NombreArchivo;
    }
    public void GuardarPersonajes(List<Personaje> personaje, string NombreArchivo)
    {
        //"--Serializando--"
        string personajesJson = JsonSerializer.Serialize(personaje);
        //"--Guardando--"
        helperArchivos.GuardarPersonajes(nombreArchivo, personajesJson);
    }

    public List<Personaje> LeerPersonajes(string NombreArchivo)
    {
        //"--Abriendo--"
        string jsonDocument = helperArchivos.AbrirArchivoTexto(NombreArchivo);
        //"--Deserializando--"
        List<Personaje> listaPersonajes = JsonSerializer.Deserialize<List<Personaje>>(jsonDocument);
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