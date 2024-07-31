using PersonajesSpace;
using mensajesSpace;
using PersonajesJS;
using combateSpace;
using Spectre.Console;
using System.Text;
using System.Threading;
using HistorialJsonSpace;
using climaApi;
using System.Text.Json.Serialization;
using System.Text.Json;

string nombreArchivo = @"C:\TallerPractica\ProyectoFinal\tl1-proyectofinal2024-MariiansSs\Personajes.json";
string historialArchivo = @"C:\TallerPractica\ProyectoFinal\tl1-proyectofinal2024-MariiansSs\Historial.json";
Mensajes narrador = new Mensajes(); //Mostrar mensajes
List<Personajes> Personajes = new List<Personajes>(); //Para ir avanzando en el combate y sacando de la lista
List<Partida> historialPartidas = new List<Partida>();
Personajes personajeElegido = new Personajes(); // Personaje jugador
Personajes oponenteGenerado = new Personajes(); // Personaje oponente
FabricaDePersonajes fabricarPersonaje = new FabricaDePersonajes(); // Para crear Personajes
PersonajesJson jsonPersonajes = new PersonajesJson(nombreArchivo);
HistorialJson jsonHistorialCombates = new HistorialJson(historialArchivo); 
Combate combates = new Combate();
int opcionPersonaje;
string caracterOpcionPersonaje, opcionSeguirJugando;
int bandera = 0,bandera2 = 0, finBatalla = 1, seguirJugando = 0;


// CONSUMO DE API
Root estadoClima = await ObtenerClima();

    static async Task<Root> ObtenerClima()
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


// PRESENTACION DEL JUEGO
narrador.Bienvenida();
narrador.mensajeIntroduccion();

while(seguirJugando != 1)
{
    // VERIFICO SI EXISTEN LOS PERSONAJES, SI NO, LOS CREO
if (jsonPersonajes.Existe(nombreArchivo))
{
    Personajes = jsonPersonajes.LeerPersonajes(nombreArchivo); // Lee los Personajes del json y guardo en la lista
}
else
{
    Personajes = fabricarPersonaje.CrearPersonajes(); // Crea los Personajes y los guardo en una lista
    jsonPersonajes.GuardarPersonajes(Personajes, nombreArchivo); // Guardo los Personajes en el json
}

// VERIFICO SI EXISTE UN HISTORIAL VIEJO Y LO BORRO
if(jsonHistorialCombates.Existe(historialArchivo))
{
    File.Delete(historialArchivo);
}

// ELEGIENDO PERSONAJE
narrador.preguntaSobrePersonaje();
Thread.Sleep(3000);
Console.WriteLine("");
Console.WriteLine("");
Console.WriteLine("");
fabricarPersonaje.mostrarPersonajeAElegir(Personajes); 
caracterOpcionPersonaje = Console.ReadLine();
while (bandera != 1)
{
    if (int.TryParse(caracterOpcionPersonaje, out opcionPersonaje))
    {
        personajeElegido = fabricarPersonaje.buscarPersonajes(opcionPersonaje, Personajes);

        Console.WriteLine("");
        //PANEL VISUAL DE PERSONAJE ELEGIDO
        var panelPersonaje = new Panel($"[Black]NOMBRE:[/][Cyan]{personajeElegido.Datos1.Name}[/] [Black]REGION:[/][Cyan]{personajeElegido.Datos1.Region}[/] [Black]CLASE:[/][Cyan]{personajeElegido.Datos1.Tipoclase}[/]");
        panelPersonaje.Header = new PanelHeader("PERSONAJE ELEGIDO");
        panelPersonaje.Border = BoxBorder.Ascii;
        panelPersonaje.BorderColor(Color.Aquamarine1);
        panelPersonaje.Header.Centered();
        AnsiConsole.Write(panelPersonaje); //MUESTRO EL PANEL
        Thread.Sleep(3000);
        Console.WriteLine("");

        Personajes.Remove(personajeElegido);


        bandera = 1;
    }
    else
    {
        narrador.errorPersonaje();
        narrador.preguntaSobrePersonaje();
        fabricarPersonaje.mostrarPersonajeAElegir(Personajes);
        caracterOpcionPersonaje = Console.ReadLine();
    }
}

//GENERANDO OPONENTE
oponenteGenerado = fabricarPersonaje.generarOponente(Personajes);
Personajes.Remove(oponenteGenerado);
Console.WriteLine("");

//PANEL VISUAL DE OPONENTE GENERADO
mostrarPanel paneles = new mostrarPanel(oponenteGenerado);
paneles.mostrarOponente1(oponenteGenerado);
Thread.Sleep(3000);
Console.WriteLine("");

//COMBATE
while (finBatalla == 1 && Personajes.Count > 0)
{
    finBatalla = combates.iniciarCombate(personajeElegido, oponenteGenerado);
    if (finBatalla == 1)
    {
        jsonHistorialCombates.GuardarGanador(personajeElegido, oponenteGenerado, $"{personajeElegido.Datos1.Name} ES EL GANADOR", historialArchivo);
        oponenteGenerado = fabricarPersonaje.generarOponente(Personajes);
        Personajes.Remove(oponenteGenerado);
        if(Personajes.Count > 1)
        {
            AnsiConsole.Markup("[Cyan]Felicidades Invocador, has pasado a la siguiente pelea![/]");
            Thread.Sleep(3000);
            Console.WriteLine("");
            paneles.mostrarOponente2(oponenteGenerado);  
            Thread.Sleep(3000);
            Console.WriteLine("");
        }else
         if(Personajes.Count == 1)
         {
             AnsiConsole.Markup("[Cyan]Felicidades Invocador, has pasado a la siguiente pelea![/]");
             Thread.Sleep(3000);
             Console.WriteLine("");
             paneles.mostrarOponente3(oponenteGenerado);
             Thread.Sleep(3000);
             Console.WriteLine("");
         }else
         {
            AnsiConsole.Markup("[Cyan]Felicidades Invocador, no queda mas nadie en el campo de batalla![/]");
            Thread.Sleep(3000);
            Console.WriteLine("");
            AnsiConsole.Markup("[Cyan]FELICIDADES, ERES EL GANADOR![/]");
         }
    }else
    {
        jsonHistorialCombates.GuardarGanador(oponenteGenerado, personajeElegido, $"{oponenteGenerado.Datos1.Name} TE HA DERROTADO", historialArchivo);
    }
}

// Muestro el historial de partidas
Console.WriteLine("");
Console.WriteLine("");
AnsiConsole.Markup("[RED]HISTORIAL DE COMBATES[/]");
Thread.Sleep(2000);
Console.WriteLine("");
List<Partida> historial = jsonHistorialCombates.LeerGanadores(historialArchivo);
int i = 1;
            foreach (var partida in historial)
            {
                Console.WriteLine("");
                var tablaHistorial = new Table().Title($"[Blue]COMBATE {i}[/]");
                tablaHistorial.Border(TableBorder.Ascii2).BorderColor(Color.DarkGoldenrod);
                tablaHistorial.AddColumn($"[CYAN]{partida.Ganador.Datos1.Name}[/] vs [RED]{partida.Perdedor.Datos1.Name}[/]"); 
                tablaHistorial.AddRow($"[BLACK]{partida.Informacion}[/]");
                i++;
                AnsiConsole.Render(tablaHistorial);
                Thread.Sleep(2000);
            }
            
        

Console.WriteLine("");
AnsiConsole.Markup($"[Red]INVOCADOR, Deseas volver a jugar? [0]=NO , [1]=SI [/]");
Console.WriteLine("");
opcionSeguirJugando = Console.ReadLine();
bandera2 = 0;
while(bandera2 != 1)
{
    if(int.TryParse(opcionSeguirJugando, out seguirJugando))
    {
        if(seguirJugando == 1)
        {
            AnsiConsole.Markup($"[Red]REINICIANDO...[/]");
            File.Delete(historialArchivo); // ELIMINO EL HISTORIAL DE LA PARTIDA
            seguirJugando = 0; // Para que siga el juego
            bandera2 = 1; // Para que salga de este ciclo
            bandera = 0; // Para que muestre nuevamente el personaje elegido
            finBatalla = 1; // Para que vuelvan a empezar los combates
        }else
        {
            if(seguirJugando == 0)
            {
                AnsiConsole.Markup($"[Red]HASTA LUEGO, INVOCADOR![/]");
                seguirJugando = 1;
                bandera2 = 1;
            }else
            {
                AnsiConsole.Markup($"[Red]INVOCADOR, hubo un error inesperado asi que el juego terminara!, HASTA LUEGO[/]");
                seguirJugando = 1;
                bandera2 = 1;
            }
        }
    }else
    {
        Console.WriteLine("");
        AnsiConsole.Markup($"[Red]INVOCADOR, nuestro sistema no conoce esa decision![/]");
        Console.WriteLine("");
        AnsiConsole.Markup($"[Red]INVOCADOR, Deseas volver a jugar?[/]");
        opcionSeguirJugando = Console.ReadLine();
    }
}

}























