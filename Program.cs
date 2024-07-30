using PersonajesSpace;
using mensajesSpace;
using PersonajesJS;
using combateSpace;
using Spectre.Console;
using System.Text;
using System.Threading;
using HistorialJsonSpace;

string nombreArchivo = @"C:\TallerPractica\ProyectoFinal\tl1-proyectofinal2024-MariiansSs\Personajes.json";
string historialArchivo = @"C:\TallerPractica\ProyectoFinal\tl1-proyectofinal2024-MariiansSs\Historial.json";
Mensajes narrador = new Mensajes(); //Mostrar mensajes
List<Personajes> Personajes = new List<Personajes>(); //Para ir avanzando en el combate y sacando de la lista
Personajes personajeElegido = new Personajes(); // Personaje jugador
Personajes oponenteGenerado = new Personajes(); // Personaje oponente
FabricaDePersonajes fabricarPersonaje = new FabricaDePersonajes(); // Para crear Personajes
PersonajesJson jsonPersonajes = new PersonajesJson(nombreArchivo);
HistorialJson jsonHistorialCombates = new HistorialJson(historialArchivo); 
Combate combates = new Combate();
int opcionPersonaje;
string caracterOpcionPersonaje;
int bandera = 0, finBatalla = 1;


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


// PRESENTACION DEL JUEGO
narrador.Bienvenida();
narrador.mensajeIntroduccion();


// ELEGIENDO PERSONAJE
narrador.preguntaSobrePersonaje();
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
mostrarPanel oponente = new mostrarPanel(oponenteGenerado);
oponente.mostrar1(oponenteGenerado);
Thread.Sleep(3000);
Console.WriteLine("");

//COMBATE
while (finBatalla == 1 && Personajes.Count > 0)
{
    finBatalla = combates.iniciarCombate(personajeElegido, oponenteGenerado);
    if (finBatalla == 1)
    {
        oponenteGenerado = fabricarPersonaje.generarOponente(Personajes);
        Personajes.Remove(oponenteGenerado);
        if(Personajes.Count > 1)
        {
            AnsiConsole.Markup("[Cyan]Felicidades Invocador, has pasado a la siguiente pelea![/]");
            Thread.Sleep(3000);
            Console.WriteLine("");
            oponente.mostrar2(oponenteGenerado);  
            Thread.Sleep(3000);
            Console.WriteLine("");
        }else
         if(Personajes.Count == 1)
         {
             AnsiConsole.Markup("[Cyan]Felicidades Invocador, has pasado a la siguiente pelea![/]");
             Thread.Sleep(3000);
             Console.WriteLine("");
             oponente.mostrar3(oponenteGenerado);
             Thread.Sleep(3000);
             Console.WriteLine("");
         }else
         {
            AnsiConsole.Markup("[Cyan]Felicidades Invocador, no queda mas nadie en el campo de batalla![/]");
            Thread.Sleep(3000);
            Console.WriteLine("");
            jsonHistorialCombates.GuardarGanador(personajeElegido,"GANADOR",historialArchivo);
            AnsiConsole.Markup("[Cyan]FELICIDADES, ERES EL GANADOR![/]");
         }
    }
}




















