﻿using PersonajesSpace;
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

string nombreArchivo = @"json\Personajes.json";
string historialArchivo = @"json\Historial.json";
Mensajes narrador = new Mensajes(); //Mostrar mensajes
mostrarPanel paneles = new mostrarPanel(); // Mostrar paneles
List<Personajes> listaPersonajes = new List<Personajes>(); //Para ir avanzando en el combate y sacando de la lista
List<Partida> historialPartidas = new List<Partida>();
Personajes personajeElegido = new Personajes(); // Personaje jugador
Personajes oponenteGenerado = new Personajes(); // Personaje oponente
FabricaDePersonajes fabricarPersonaje = new FabricaDePersonajes(); // Para crear Personajes
PersonajesJson jsonPersonajes = new PersonajesJson(nombreArchivo);
HistorialJson jsonHistorialCombates = new HistorialJson(historialArchivo);
Combate combates = new Combate();
int opcionPersonaje;
string caracterOpcionPersonaje, opcionSeguirJugando;
int bandera = 0, bandera2 = 0, ganaLocal = 1, seguirJugando = 0;
const int TIEMPO_ESPERA = 1500;


// CONSUMO DE API
Root estadoClima = await servicioClima.ObtenerClima(); // OBTENGO EL CLIMA



// PRESENTACION DEL JUEGO
narrador.Bienvenida();
narrador.mensajeIntroduccion();

while (seguirJugando != 1)
{
    // VERIFICO SI EXISTEN LOS PERSONAJES, SI NO, LOS CREO
    if (jsonPersonajes.Existe(nombreArchivo))
    {
        listaPersonajes = jsonPersonajes.LeerPersonajes(nombreArchivo); // Lee los Personajes del json y guardo en la lista
    }
    else
    {
        listaPersonajes = fabricarPersonaje.CrearPersonajes(); // Crea los Personajes y los guardo en una lista
        jsonPersonajes.GuardarPersonajes(listaPersonajes, nombreArchivo); // Guardo los Personajes en el json
    }

    // VERIFICO SI EXISTE UN HISTORIAL VIEJO Y LO BORRO
    if (jsonHistorialCombates.Existe(historialArchivo))
    {
        File.Delete(historialArchivo);
    }

    // ELEGIENDO PERSONAJE
    narrador.preguntaSobrePersonaje();
    Thread.Sleep(TIEMPO_ESPERA);
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("");
    fabricarPersonaje.mostrarPersonajeAElegir(listaPersonajes);
    caracterOpcionPersonaje = Console.ReadLine();
    while (bandera != 1)
    {
        if (int.TryParse(caracterOpcionPersonaje, out opcionPersonaje))
        {
            personajeElegido = fabricarPersonaje.buscarPersonajes(opcionPersonaje, listaPersonajes);

            //PANEL VISUAL DE PERSONAJE ELEGIDO
            paneles.mostrarPersonajeElegido(personajeElegido);
            
            listaPersonajes.Remove(personajeElegido);


            bandera = 1;
        }
        else
        {
            narrador.errorPersonaje();
            narrador.preguntaSobrePersonaje();
            fabricarPersonaje.mostrarPersonajeAElegir(listaPersonajes);
            caracterOpcionPersonaje = Console.ReadLine();
        }
    }

    //GENERANDO OPONENTE
    oponenteGenerado = fabricarPersonaje.generarOponente(listaPersonajes);
    listaPersonajes.Remove(oponenteGenerado);
    Console.WriteLine("");

    //PANEL VISUAL DE OPONENTE GENERADO
    paneles.mostrarOponente1(oponenteGenerado);
    Thread.Sleep(TIEMPO_ESPERA);
    Console.WriteLine("");


    //COMBATE
    while (ganaLocal == 1 && listaPersonajes.Count != 0)
    {
        int combateActual = combates.iniciarCombate(personajeElegido, oponenteGenerado,estadoClima.Current.Condition.Text);

        if (combateActual == 1)
        { // si gano, guardo gadaor
            jsonHistorialCombates.GuardarGanador(personajeElegido, oponenteGenerado, $"{personajeElegido.getDatos.Name} ES EL GANADOR", historialArchivo); // guardo ganador

            oponenteGenerado = fabricarPersonaje.generarOponente(listaPersonajes);
            if (listaPersonajes.Count > 1)
            {
                Thread.Sleep(TIEMPO_ESPERA);
                Console.WriteLine("");
                paneles.mostrarOponente2(oponenteGenerado);
                Thread.Sleep(TIEMPO_ESPERA);
                Console.WriteLine("");
            }
            if (listaPersonajes.Count == 1)
            {
                Thread.Sleep(TIEMPO_ESPERA);
                Console.WriteLine("");
                paneles.mostrarOponente3(oponenteGenerado);
                Thread.Sleep(TIEMPO_ESPERA);
                Console.WriteLine("");

                combateActual = combates.iniciarCombate(personajeElegido, oponenteGenerado, estadoClima.Current.Condition.Text);
                if (combateActual == 1)
                {
                    jsonHistorialCombates.GuardarGanador(personajeElegido, oponenteGenerado, $"{personajeElegido.getDatos.Name} ES EL GANADOR", historialArchivo); // guardo ganador
                }
                else
                {
                    jsonHistorialCombates.GuardarGanador(oponenteGenerado, personajeElegido, $"{oponenteGenerado.getDatos.Name} TE HA DERROTADO", historialArchivo); // guardo ganador 
                }
            }

        }
        else
        { // si pierdo guardo como perdedor
            jsonHistorialCombates.GuardarGanador(oponenteGenerado, personajeElegido, $"{oponenteGenerado.getDatos.Name} TE HA DERROTADO", historialArchivo); // guardo ganador
        }
        ganaLocal = combateActual;
        listaPersonajes.Remove(oponenteGenerado);
    }
    if (ganaLocal == 1)
    {
        Console.WriteLine("");
        AnsiConsole.Markup("[Cyan]Felicidades Invocador, no queda mas nadie en el campo de batalla![/]");
        Thread.Sleep(TIEMPO_ESPERA);
        Console.WriteLine("");
        AnsiConsole.Markup("[Cyan]ERES EL GANADOR!![/]");
    }

    // Muestro el historial de partidas
    Console.WriteLine("");
    Console.WriteLine("");
    AnsiConsole.Markup("[RED]HISTORIAL DE COMBATES[/]");
    Thread.Sleep(TIEMPO_ESPERA);
    Console.WriteLine("");
    List<Partida> historial = jsonHistorialCombates.LeerGanadores(historialArchivo);
    int i = 1;
    foreach (var partida in historial)
    {
        Console.WriteLine("");
        var tablaHistorial = new Table().Title($"[Blue]COMBATE {i}[/]");
        tablaHistorial.Border(TableBorder.Ascii2).BorderColor(Color.DarkGoldenrod);
        tablaHistorial.AddColumn($"[CYAN]{partida.Ganador.getDatos.Name}[/] vs [RED]{partida.Perdedor.getDatos.Name}[/]");
        tablaHistorial.AddRow($"[BLACK]{partida.Informacion}[/]");
        i++;
        AnsiConsole.Render(tablaHistorial);
        Thread.Sleep(TIEMPO_ESPERA);
    }



    Console.WriteLine("");
    AnsiConsole.Markup($"[Red]INVOCADOR, Deseas volver a jugar? 0=NO , 1=SI [/]");
    Console.WriteLine("");
    opcionSeguirJugando = Console.ReadLine();
    bandera2 = 0;
    while (bandera2 != 1)
    {
        if (int.TryParse(opcionSeguirJugando, out seguirJugando))
        {
            if (seguirJugando == 1)
            {
                AnsiConsole.Markup($"[Red]REINICIANDO...[/]");
                File.Delete(historialArchivo); // ELIMINO EL HISTORIAL DE LA PARTIDA
                seguirJugando = 0; // Para que siga el juego
                bandera2 = 1; // Para que salga de este ciclo
                bandera = 0; // Para que muestre nuevamente el personaje elegido
                ganaLocal = 1; // Para que vuelvan a empezar los combates
            }
            else
            {
                if (seguirJugando == 0)
                {
                    AnsiConsole.Markup($"[Red]HASTA LUEGO, INVOCADOR![/]");
                    seguirJugando = 1;
                    bandera2 = 1;
                }
                else
                {
                    AnsiConsole.Markup($"[Red]INVOCADOR, hubo un error inesperado asi que el juego terminara!, HASTA LUEGO[/]");
                    seguirJugando = 1;
                    bandera2 = 1;
                }
            }
        }
        else
        {
            Console.WriteLine("");
            AnsiConsole.Markup($"[Red]INVOCADOR, nuestro sistema no conoce esa decision![/]");
            Console.WriteLine("");
            AnsiConsole.Markup($"[Red]INVOCADOR, Deseas volver a jugar?[/]");
            opcionSeguirJugando = Console.ReadLine();
        }
    }

}

























