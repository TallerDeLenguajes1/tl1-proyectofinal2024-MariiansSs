namespace combateSpace;
using PersonajesSpace;
using Spectre.Console;
using System.Threading; // Para retrasar la muestra de mensajes
using HistorialJsonSpace;



public class Combate
{
    private Personaje PersonajeElegido;

    private Personaje PersonajeOponente;

    const int TIEMPO_ESPERA = 1300;
    public int iniciarCombate(Personaje PersonajeElegido, Personaje PersonajeOponente, string estadoClima)
    {
        int ganador = 0;
        while (PersonajeElegido.getCaracteristicas.Salud > 0 && PersonajeOponente.getCaracteristicas.Salud > 0)
        {
            aumentarDanioSegunClima(PersonajeElegido, estadoClima);
            aumentarDanioSegunClima(PersonajeOponente, estadoClima);

            if (decisionPersonaje(PersonajeElegido, PersonajeOponente) == 1)
            {
                mostrarVidaOponente(PersonajeOponente);
            }
            else
            {
                mostrarVidaPersonaje(PersonajeElegido);
            }

            if (PersonajeOponente.getCaracteristicas.Salud > 0) // Para no mostrar dos veces la salud si es 0
            {
                if (decisionPersonaje(PersonajeOponente, PersonajeElegido) == 1)
                {
                    mostrarVidaPersonaje(PersonajeElegido);
                }
                else
                {
                    mostrarVidaOponente(PersonajeOponente);
                }
            }
            if (PersonajeElegido.getCaracteristicas.Salud <= 0)
            {
                AnsiConsole.Markup($"[Red]Lo siento invocador!, {PersonajeElegido.getDatos.Name} ha sido derrotado.[/]");
                Console.WriteLine("");
                Thread.Sleep(TIEMPO_ESPERA);
                AnsiConsole.Markup("[Red]FIN DEL JUEGO[/]");
                Thread.Sleep(TIEMPO_ESPERA);
                ganador = 0;

            }
            if (PersonajeOponente.getCaracteristicas.Salud <= 0)
            {
                AnsiConsole.Markup($"[Cyan]{PersonajeOponente.getDatos.Name} ha sido derrotado.[/]");
                Thread.Sleep(TIEMPO_ESPERA);
                PersonajeElegido.restaurarSalud();
                PersonajeElegido.subirFuria();
                ganador = 1;

            }
        }
        return ganador;
    }

    public int decisionPersonaje(Personaje Atacante, Personaje Defensor)
    {
        int decision = 0;
        int danio;
        Random decisionAleatoria = new Random();
        int decisionPersonaje;
        if (Atacante.getCaracteristicas.Salud > 0)
        {
            decisionPersonaje = decisionAleatoria.Next(1, 3);
            if (decisionPersonaje == 1)
            {
                danio = Atacante.atacar();
                Defensor.restarSalud(danio);
                decision = 1;
            }
            else
            {
                if (decisionPersonaje == 2 && Atacante.getCaracteristicas.Salud < 30)
                {
                    Atacante.tomarPocion(); // Solo puede curarse dos veces por combate
                    decision = 2;
                }
                else
                {
                    danio = Atacante.atacar();
                    Defensor.restarSalud(danio);
                    decision = 1;
                }
            }

        }
        return decision;
    }

    public void mostrarVidaPersonaje(Personaje PersonajeElegido)
    {
        int anchoMinimo = 30;
        int ancho = Math.Max(PersonajeElegido.getCaracteristicas.Salud, anchoMinimo);

        AnsiConsole.Write(new BarChart()
        .Width(ancho)
        .Label("Porcentaje de vida")
        .CenterLabel()
        .AddItem($"{PersonajeElegido.getDatos.Name}", PersonajeElegido.getCaracteristicas.Salud, Color.Cyan1));
        Console.WriteLine("");

        Thread.Sleep(TIEMPO_ESPERA);
    }

    public void mostrarVidaOponente(Personaje PersonajeOponente)
    {
        int anchoMinimo = 30;
        int ancho = Math.Max(PersonajeOponente.getCaracteristicas.Salud, anchoMinimo);

        AnsiConsole.Write(new BarChart()
        .Width(ancho)
        .Label("Porcentaje de vida")
        .CenterLabel()
        .AddItem($"{PersonajeOponente.getDatos.Name}", PersonajeOponente.getCaracteristicas.Salud, Color.Red));
        Console.WriteLine("");

        Thread.Sleep(TIEMPO_ESPERA);
    }

    public void aumentarDanioSegunClima(Personaje Personaje, string estadoClima)
    {
        if (estadoClima == "Sunny")
        {
            if (Personaje.getDatos.Region == "Demacia")
            {
                Personaje.aumentarDanio();
            }
        }
        if (estadoClima == "Cloudy")
        {
            if (Personaje.getDatos.Region == "Noxus")
            {
                Personaje.aumentarDanio();
            }
        }
        if (estadoClima == "Rain")
        {
            if (Personaje.getDatos.Region == "Freljord")
            {
                Personaje.aumentarDanio();
            }
        }
        if (estadoClima == "Thunderstorm")
        {
            if (Personaje.getDatos.Region == "Vacio")
            {
                Personaje.aumentarDanio();
            }
        }
    }

}



