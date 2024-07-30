namespace combateSpace;
using PersonajesSpace;
using Spectre.Console;
using System.Threading; // Para retrasar la muestra de mensajes
using HistorialJsonSpace;


public class Combate
{
    private Personajes PersonajeElegido;

    private Personajes PersonajeOponente;

    public int iniciarCombate(Personajes PersonajeElegido, Personajes PersonajeOponente)
    {
        int ganador = 0;
        while (PersonajeElegido.Caracteristicas1.Salud > 0 && PersonajeOponente.Caracteristicas1.Salud > 0)
        {
            //REVISAR PORCENTAJE DE VIDA
            if(decisionPersonaje(PersonajeElegido, PersonajeOponente) == 1)
            {
                mostrarVidaOponente(PersonajeOponente);
            }else{
                mostrarVidaPersonaje(PersonajeElegido);
            }


            if(PersonajeOponente.Caracteristicas1.Salud > 0 ) // Para no mostrar dos veces la salud si es 0
            {
                if(decisionPersonaje(PersonajeOponente, PersonajeElegido) == 1)
                {
                     mostrarVidaPersonaje(PersonajeElegido);
                }else{
                    mostrarVidaOponente(PersonajeOponente);
                }
            }
            


            if (PersonajeElegido.Caracteristicas1.Salud <= 0)
            {
                AnsiConsole.Markup($"[Red]Lo siento invocador!, {PersonajeElegido.Datos1.Name} ha sido derrotado.[/]");
                Console.WriteLine("");
                Thread.Sleep(3000);
                AnsiConsole.Markup("[Red]FIN DEL JUEGO[/]");
                Thread.Sleep(3000);
                ganador = 0;

            }


            if (PersonajeOponente.Caracteristicas1.Salud <= 0)
            {
                AnsiConsole.Markup($"[Cyan]{PersonajeOponente.Datos1.Name} ha sido derrotado.[/]");
                Console.WriteLine("");
                Thread.Sleep(3000);
                PersonajeElegido.Caracteristicas1.Salud = 100;
                ganador = 1;

            }
        }
        return ganador;
    }

    public void atacar(Personajes Atacante, Personajes Defensor)
    {
        
        Random efectividadRandom = new Random();
        int ataque = Atacante.Caracteristicas1.Destreza * Atacante.Caracteristicas1.Fuerza;
        int efectividad = efectividadRandom.Next(1, 101);
        int defensa = Atacante.Caracteristicas1.Armadura * Atacante.Caracteristicas1.Velocidad;
        int ajuste = 60;
        int danioProvocado = ((ataque * efectividad) - defensa) / ajuste;

        if (Atacante.Caracteristicas1.Nivelfuria == 3)
        {
            danioProvocado = danioProvocado * 2;
            Console.WriteLine("");
            AnsiConsole.Markup($"[Red]{Atacante.Datos1.Frase}[/][Black](ESTE PERSONAJE USO SU DEFINITIVA)[/]");
            Console.WriteLine("");
            Atacante.Caracteristicas1.Nivelfuria = 0; //Reseteo el nivel de furia para equilibrar el combate
        }

        Defensor.Caracteristicas1.Salud -= danioProvocado;
        Defensor.Caracteristicas1.Nivelfuria++;

        if (Defensor.Caracteristicas1.Salud < 0)
        {
            Defensor.Caracteristicas1.Salud = 0;
        }
        AnsiConsole.Write($@"{Atacante.Datos1.Name} ATACO CON UNA EFECTIVIDAD DE {efectividad}% Y DAÑO DE {danioProvocado}%");
        Console.WriteLine("");

    }
    public void tomarPocion(Personajes Personaje)
    {
        Personaje.Caracteristicas1.Salud += Personaje.Caracteristicas1.Pociondevida;
        if (Personaje.Caracteristicas1.Salud > 100)
        {
            Personaje.Caracteristicas1.Salud = 100;
        }
        
        Console.WriteLine("");
        
        AnsiConsole.Write($"{Personaje.Datos1.Name} SE HA CURADO UN TOTAL DE {Personaje.Caracteristicas1.Pociondevida}%");

        Console.WriteLine("");
    
    }

    public int decisionPersonaje(Personajes PersonajeElegido, Personajes PersonajeOponente)
    {
        int decision = 0;
        Random decisionAleatoria = new Random();
        int decisionPersonaje;
        if (PersonajeElegido.Caracteristicas1.Salud > 0)
        {
            decisionPersonaje = decisionAleatoria.Next(1, 3);
            if (decisionPersonaje == 1)
            {
                atacar(PersonajeElegido, PersonajeOponente);
                decision = 1;
            }
            else
                if (decisionPersonaje == 2 && PersonajeElegido.Caracteristicas1.Salud < 30)
            {
                tomarPocion(PersonajeElegido);
                decision = 2;
            }
            else
            {
                atacar(PersonajeElegido, PersonajeOponente);
                decision = 1;
            }
        }
        return decision;
    }

    public void mostrarVidaPersonaje(Personajes PersonajeElegido)
    {
        int anchoMinimo = 30;
        int ancho = Math.Max(PersonajeElegido.Caracteristicas1.Salud, anchoMinimo);

        AnsiConsole.Write(new BarChart()
        .Width(ancho)
        .Label("Porcentaje de vida")
        .CenterLabel()
        .AddItem($"{PersonajeElegido.Datos1.Name}", PersonajeElegido.Caracteristicas1.Salud, Color.Cyan1));
        Console.WriteLine("");

        Thread.Sleep(2000);
    }

    public void mostrarVidaOponente(Personajes PersonajeOponente)
    {
        int anchoMinimo = 30;
        int ancho = Math.Max(PersonajeOponente.Caracteristicas1.Salud, anchoMinimo);

        AnsiConsole.Write(new BarChart()
        .Width(ancho)
        .Label("Porcentaje de vida")
        .CenterLabel()
        .AddItem($"{PersonajeOponente.Datos1.Name}", PersonajeOponente.Caracteristicas1.Salud, Color.Red));
        Console.WriteLine("");

        Thread.Sleep(2000);
    }
}



