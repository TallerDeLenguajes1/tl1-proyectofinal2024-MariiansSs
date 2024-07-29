namespace HistorialJsonSpace;
using System.Text.Json;
using helperJson;
using PersonajesSpace;

    public class HistorialJson
{
    string nombreArchivo {get;}
    private HelperDeJson helperArchivos = new HelperDeJson();

    public HistorialJson(string NombreArchivo)
    {
        this.nombreArchivo = NombreArchivo;
    }
    public void GuardarGanador(Personajes ganador, string informacion, string nombreArchivo)
    {
        List<Partida> historial = new List<Partida>();

        if (Existe(nombreArchivo))
        {
            historial = LeerGanadores(nombreArchivo);
        }

        historial.Add(new Partida { Ganador = ganador, Informacion = informacion });
        string historialJson = JsonSerializer.Serialize(historial);
        helperArchivos.GuardarPersonajes(nombreArchivo, historialJson);
    }

    public List<Partida> LeerGanadores(string nombreArchivo)
    {
        if (!Existe(nombreArchivo))
        {
            return new List<Partida>();
        }
        
        string jsonDocument = helperArchivos.AbrirArchivoTexto(nombreArchivo);
        var historial = JsonSerializer.Deserialize<List<Partida>>(jsonDocument);

        return historial;
    }

    public bool Existe(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo))
        {
            // Verifico si el archivo tiene datos
            string contenidoArchivo = File.ReadAllText(nombreArchivo);
            return !string.IsNullOrEmpty(contenidoArchivo); //Controla si el archivo no esta vacio
        }

        return false;  //El archivo no existe o está vacío
    }
}

public class Partida
{
    public Personajes Ganador { get; set; }
    public string Informacion { get; set; }
}