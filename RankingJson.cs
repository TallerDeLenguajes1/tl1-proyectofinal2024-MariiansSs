namespace rankingGanadoresJson;
using System.Text.Json.Serialization;
using System.Text.Json;
using helperJson;
using PersonajesSpace;

public class rankingGanadores
{
    string nombreArchivo {get;}
    private HelperDeJson helperArchivos = new HelperDeJson();

    public rankingGanadores(string NombreArchivo)
    {
        this.nombreArchivo = NombreArchivo;
    }
    public void GuardarGanador(string nombreGanador ,Personaje personajeUtilizado, string nombreArchivo)
    {
        DateTime fecha = DateTime.Now;
        string fechaFormateada = fecha.ToString("dd-MM-yyyy HH:mm:ss");
        List<Ganador> historial = new List<Ganador>();
        if (Existe(nombreArchivo))
        {
            historial = LeerGanadores(nombreArchivo);
        }

        historial.Add(new Ganador { NombreGanador = nombreGanador, personajeUtilizado = personajeUtilizado, Fecha = fechaFormateada});
        string rankingGanadores = JsonSerializer.Serialize(historial);
        helperArchivos.GuardarPersonajes(nombreArchivo, rankingGanadores);
    }

    public List<Ganador> LeerGanadores(string nombreArchivo)
    {
        if (!Existe(nombreArchivo))
        {
            return new List<Ganador>();
        }
        
        string jsonDocument = helperArchivos.AbrirArchivoTexto(nombreArchivo);
        var historial = JsonSerializer.Deserialize<List<Ganador>>(jsonDocument);

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

public class Ganador
{
    public string NombreGanador { get; set; }

    public string Fecha { get; set; }

    public Personaje personajeUtilizado {get; set;}
}