using PersonajesSpace;
using mensajesSpace;
using PersonajesJS;
using combateSpace;
using Spectre.Console;
using System.Text;


string nombreArchivo = @"C:\TallerPractica\ProyectoFinal\tl1-proyectofinal2024-MariiansSs\Personajes.json";
Mensajes narrador = new Mensajes();
List<Personajes> Personajes = new List<Personajes>();
Personajes personajeElegido = new Personajes();
Personajes oponenteGenerado = new Personajes();
FabricaDePersonajes fabricarPersonaje = new FabricaDePersonajes();
PersonajesJson jsonPersonajes = new PersonajesJson(nombreArchivo);
Combate combates = new Combate();
int opcionPersonaje;
string caracterOpcionPersonaje;
int bandera = 0, finBatalla = 1;


// VERIFICO SI EXISTEN LOS PERSONAJES, SI NO, LOS CREO

if (jsonPersonajes.Existe(nombreArchivo))
{
    Personajes = jsonPersonajes.LeerPersonajes(nombreArchivo);
}
else
{
    Personajes = fabricarPersonaje.CrearPersonajes();
    jsonPersonajes.GuardarPersonajes(Personajes, nombreArchivo);
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
        var panelPersonaje = new Panel($"[Black]NOMBRE:[/][Cyan]{personajeElegido.Datos1.Name}[/] [Black]REGION:[/][Cyan]{personajeElegido.Datos1.Region}[/] [Black]CLASE:[/][Cyan]{personajeElegido.Datos1.Tipoclase}[/]");
        panelPersonaje.Header = new PanelHeader("PERSONAJE ELEGIDO");
        panelPersonaje.Border = BoxBorder.Ascii;
        panelPersonaje.BorderColor(Color.Aquamarine1);
        panelPersonaje.Header.Centered();
        AnsiConsole.Write(panelPersonaje);
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

var panelOponente = new Panel($"[Black]NOMBRE:[/][Red]{oponenteGenerado.Datos1.Name}[/] [Black]REGION:[/][Red]{oponenteGenerado.Datos1.Region}[/] [Black]CLASE:[/][Red]{oponenteGenerado.Datos1.Tipoclase}[/]");
panelOponente.Header = new PanelHeader("¡TU OPONENTE HA APARECIDO!");
panelOponente.Border = BoxBorder.Ascii;
panelOponente.BorderColor(Color.Red);
panelOponente.Header.Centered();
AnsiConsole.Write(panelOponente);

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
             Console.WriteLine(@$"¡TU NUEVO OPONENTE HA APARECIDO EN EL CAMPO DE BATALLA!
Nombre:{oponenteGenerado.Datos1.Name} Region:{oponenteGenerado.Datos1.Region} Clase:{oponenteGenerado.Datos1.Tipoclase}");
        }else
        {
             Console.WriteLine(@$"¡EL ULTIMO OPONENTE ESTA AQUI! MUCHA SUERTE INVOCADOR
Nombre:{oponenteGenerado.Datos1.Name} Region:{oponenteGenerado.Datos1.Region} Clase:{oponenteGenerado.Datos1.Tipoclase}");
        }
       
    }
}

if (Personajes.Count == 0)
{
    Console.WriteLine("FELICIDADES, ERES EL GANADOR!");
}
















