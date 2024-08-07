﻿using PersonajesSpace;
using mensajesSpace;
using PersonajesJS;
using combateSpace;
using Spectre.Console;
using System.Text;
using System.Threading;
using HistorialJsonSpace;
using climaApi;
using rankingGanadoresJson;
using System.Text.Json.Serialization;
using System.Text.Json;

string nombrePersonajesArchivo = @"json\Personajes.json";
string nombreHistorialArchivo = @"json\Historial.json";
string nombreArchivoRanking = @"json\RankingHistorico.json";
Mensajes narrador = new Mensajes(); //Mostrar mensajes
mostrarPanel paneles = new mostrarPanel(); // Mostrar paneles
List<Personaje> listaPersonajes = new List<Personaje>(); //Para ir avanzando en el combate y sacando de la lista
List<Partida> historialPartidas = new List<Partida>(); // Lista para mostrar historial de combates
List<Ganador> historialGanadores = new List<Ganador>();
Personaje personajeElegido = new Personaje(); // Personaje jugador
Personaje oponenteGenerado = new Personaje(); // Personaje oponente
FabricaDePersonajes fabricarPersonaje = new FabricaDePersonajes(); // Para crear Personajes
PersonajesJson jsonPersonajes = new PersonajesJson(nombrePersonajesArchivo);
HistorialJson jsonHistorialCombates = new HistorialJson(nombreHistorialArchivo);
rankingGanadores jsonRankingGanadores = new rankingGanadores(nombreArchivoRanking);
Combate combates = new Combate();
int opcionPersonaje;
string caracterOpcionPersonaje, opcionSeguirJugando, nombreRankingHistorico, opcionVerGanadores;
int bandera = 0, bandera2 = 0, bandera3 = 0, ganaLocal = 1, seguirJugando = 0, verGanadores;
const int TIEMPO_ESPERA = 1500;


// CONSUMO DE API
Root estadoClima = await servicioClima.ObtenerClima(); // OBTENGO EL CLIMA

// PRESENTACION DEL JUEGO
narrador.Bienvenida();
narrador.mensajeIntroduccion();

while (seguirJugando != 1)
{
    // SI EXISTE UNA LISTA DE PERSONAJES LA BORRA
    if (jsonPersonajes.Existe(nombrePersonajesArchivo))
    {
        File.Delete(nombrePersonajesArchivo);
    }
    listaPersonajes.Clear(); // LIMPIO LA LISTA PARA ASGINAR NUEVAMENTE LOS PERSONAJES
    
    // VUELVO A CREAR LOS PERSONAJES
    listaPersonajes = fabricarPersonaje.CrearPersonajes(); // Crea los Personajes y los guardo en una lista
    jsonPersonajes.GuardarPersonajes(listaPersonajes, nombrePersonajesArchivo); // Guardo los Personajes en el json

    // VERIFICO SI EXISTE UN HISTORIAL VIEJO Y LO BORRO
    if (jsonHistorialCombates.Existe(nombreHistorialArchivo))
    {
        File.Delete(nombreHistorialArchivo);
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
        int combateActual = combates.iniciarCombate(personajeElegido, oponenteGenerado, estadoClima.Current.Condition.Text);

        if (combateActual == 1)
        { // si gano, guardo gadaor
            jsonHistorialCombates.GuardarGanador(personajeElegido, oponenteGenerado, $"{personajeElegido.getDatos.Name} ES EL GANADOR", nombreHistorialArchivo); // guardo ganador

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
                    jsonHistorialCombates.GuardarGanador(personajeElegido, oponenteGenerado, $"{personajeElegido.getDatos.Name} ES EL GANADOR", nombreHistorialArchivo); // guardo ganador
                }
                else
                {
                    jsonHistorialCombates.GuardarGanador(oponenteGenerado, personajeElegido, $"{oponenteGenerado.getDatos.Name} TE HA DERROTADO", nombreHistorialArchivo); // guardo ganador 
                }
            }

        }
        else
        { // si pierdo guardo como perdedor
            jsonHistorialCombates.GuardarGanador(oponenteGenerado, personajeElegido, $"{oponenteGenerado.getDatos.Name} TE HA DERROTADO", nombreHistorialArchivo); // guardo ganador
        }
        ganaLocal = combateActual;
        listaPersonajes.Remove(oponenteGenerado);
    }
    if (ganaLocal == 1)
    {
        Console.WriteLine("");
        AnsiConsole.Markup("[Cyan]FELICIDADES INVOCADOR, NO QUEDA MAS NADIE EN EL CAMPO DE BATALLA![/]");
        Thread.Sleep(TIEMPO_ESPERA);
        Console.WriteLine("");
        AnsiConsole.Markup("[Cyan]ERES EL GANADOR!!![/]");
        Thread.Sleep(TIEMPO_ESPERA);
        Console.WriteLine("");
        AnsiConsole.Markup("[Cyan]INVOCADOR, INGRESA TU NOMBRE/ALIAS PARA QUE QUEDE EN LA GLORIA ETERNA[/]");
        Console.WriteLine("");
        nombreRankingHistorico = Console.ReadLine();
        Thread.Sleep(TIEMPO_ESPERA);

        //GUARDO EL GANADOR DE TODOS LOS COMBATES
        if (nombreArchivoRanking != null)
        {
            jsonRankingGanadores.GuardarGanador(nombreRankingHistorico, personajeElegido, nombreArchivoRanking);

        }
    }

    // Muestro el historial de partidas
    Console.WriteLine("");
    Console.WriteLine("");
    AnsiConsole.Markup("[RED][italic]HISTORIAL DE COMBATES[/][/]");
    Thread.Sleep(TIEMPO_ESPERA);
    Console.WriteLine("");
    List<Partida> historial = jsonHistorialCombates.LeerGanadores(nombreHistorialArchivo);
    int numeroCombate = 1;
    foreach (var partida in historial)
    {
        Console.WriteLine("");
        var tablaHistorial = new Table().Title($"[Blue]COMBATE {numeroCombate}[/]");
        tablaHistorial.Border(TableBorder.Ascii2).BorderColor(Color.DarkGoldenrod);
        tablaHistorial.AddColumn($"[CYAN]{partida.Ganador.getDatos.Name}[/] vs [RED]{partida.Perdedor.getDatos.Name}[/]");
        tablaHistorial.AddRow($"[BLACK]{partida.Informacion}[/]");
        numeroCombate++;
        AnsiConsole.Render(tablaHistorial);
        Thread.Sleep(TIEMPO_ESPERA);
    }


    //MUESTRA DEL RANKING HISTORICO
    Console.WriteLine("");
    AnsiConsole.Markup($"[Red]INVOCADOR, DESEAS VER EL RANKING HISTORICO DE GANADORES? 0=NO , 1=SI [/]");
    Console.WriteLine("");
    opcionVerGanadores = Console.ReadLine();
    bandera3 = 0;
    while (bandera3 != 1)
    {
        if (int.TryParse(opcionVerGanadores, out verGanadores))
        {
            if (verGanadores == 1)
            {
                if (jsonRankingGanadores.Existe(nombreArchivoRanking))
                {
                    historialGanadores = jsonRankingGanadores.LeerGanadores(nombreArchivoRanking);
                    int numeroGanador = 1;
                    Console.WriteLine("");
                    AnsiConsole.Markup("[RED][italic]RANKING HISTORICO DE GANADORES[/][/]");
                    foreach (var Ganadores in historialGanadores)
                    {
                        Console.WriteLine("");
                        var tablaGanadores = new Table().Title($"[Blue]GANADOR {numeroGanador}[/]");
                        tablaGanadores.AddColumn("[RED]NOMBRE/ALIAS[/]");
                        tablaGanadores.AddColumn($"[Blue]{Ganadores.NombreGanador}[/]");
                        tablaGanadores.AddColumn($"[RED]FECHA[/]");
                        tablaGanadores.AddColumn($"[Blue]{Ganadores.Fecha}[/]");
                        tablaGanadores.AddColumn("[RED]PERSONAJE UTILIZADO[/]");
                        tablaGanadores.AddColumn($"[Blue]{Ganadores.personajeUtilizado.getDatos.Name}[/]");
                        tablaGanadores.Border(TableBorder.Ascii2).BorderColor(Color.DarkGoldenrod);
                        numeroGanador++;
                        AnsiConsole.Render(tablaGanadores); // Mostrar tabla
                        Thread.Sleep(TIEMPO_ESPERA);
                    }
                    bandera3 = 1;
                }
                else
                {
                    Console.WriteLine("");
                    AnsiConsole.Markup($"[Red]INVOCADOR, NADIE HA PODIDO LLEGAR HASTA LA GLORIA ETERNA, INTENTA SER EL PRIMERO!!![/]");
                    Console.WriteLine("");
                    bandera3 = 1;
                }
            }
            else
            {
                if (verGanadores == 0)
                {
                    Console.WriteLine("");
                    AnsiConsole.Markup($"[Red]QUIZAS PODRIAS HABER APRENDIDO ALGO DE ELLOS...[/]");
                    Console.WriteLine("");
                    bandera3 = 1;
                }
                else
                {
                    Console.WriteLine("");
                    AnsiConsole.Markup($"[Red]Hubo un error inesperado, ingresa nuevamente la opcion.[/]");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    AnsiConsole.Markup($"[Red]INVOCADOR, DESEAS VER EL RANKING HISTORICO DE GANADORES? 0=NO , 1=SI [/]");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    opcionVerGanadores = Console.ReadLine();
                }
            }
        }
        else
        {
            AnsiConsole.Markup($"[Red]Hubo un error inesperado, ingresa nuevamente la opcion.[/]");
            Console.WriteLine("");
            Console.WriteLine("");
            AnsiConsole.Markup($"[Red]INVOCADOR, DESEAS VER EL RANKING HISTORICO DE GANADORES? 0=NO , 1=SI [/]");
            Console.WriteLine("");
             Console.WriteLine("");
            opcionVerGanadores = Console.ReadLine();
        }
    }


    //PREGUNTAR SI QUIERE VOLVER A JUGAR
    Console.WriteLine("");
    AnsiConsole.Markup($"[Red]INVOCADOR, DESEAS VOLVER A JUGAR? 0=NO , 1=SI [/]");
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
                File.Delete(nombreHistorialArchivo); // ELIMINO EL HISTORIAL DE LA PARTIDA
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
                    AnsiConsole.Markup($"[Red]INVOCADOR, HUBO UN ERROR INESPERADO ASI QUE EL JUEGO TERMINARA!, HASTA LUEGO[/]");
                    seguirJugando = 1;
                    bandera2 = 1;
                }
            }
        }
        else
        {
            Console.WriteLine("");
            AnsiConsole.Markup($"[Red]Nuestro sistema no conoce esa decision![/]");
            Console.WriteLine("");
            Console.WriteLine("");
            AnsiConsole.Markup($"[Red]INVOCADOR, DESEAS VOLVER A JUGAR? 0=NO , 1=SI[/]");
            Console.WriteLine("");
            opcionSeguirJugando = Console.ReadLine();
        }
    }

}

























