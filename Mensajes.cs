namespace mensajesSpace;
using System.Threading; // Para retrasar la muestra de mensajes
using PersonajesSpace;
using Spectre.Console; // Mostrar tablas/diseños por consola
public class Mensajes
{
    public void Bienvenida()
    {
        Console.WriteLine("");
        AnsiConsole.Markup(@"[Black]
██████╗░██╗███████╗███╗░░██╗██╗░░░██╗███████╗███╗░░██╗██╗██████╗░░█████╗░  ░█████╗░  ██╗░░░░░░█████╗░
██╔══██╗██║██╔════╝████╗░██║██║░░░██║██╔════╝████╗░██║██║██╔══██╗██╔══██╗  ██╔══██╗  ██║░░░░░██╔══██╗
██████╦╝██║█████╗░░██╔██╗██║╚██╗░██╔╝█████╗░░██╔██╗██║██║██║░░██║██║░░██║  ███████║  ██║░░░░░███████║
██╔══██╗██║██╔══╝░░██║╚████║░╚████╔╝░██╔══╝░░██║╚████║██║██║░░██║██║░░██║  ██╔══██║  ██║░░░░░██╔══██║
██████╦╝██║███████╗██║░╚███║░░╚██╔╝░░███████╗██║░╚███║██║██████╔╝╚█████╔╝  ██║░░██║  ███████╗██║░░██║
╚═════╝░╚═╝╚══════╝╚═╝░░╚══╝░░░╚═╝░░░╚══════╝╚═╝░░╚══╝╚═╝╚═════╝░░╚════╝░  ╚═╝░░╚═╝  ╚══════╝╚═╝░░╚═╝

░██████╗░██████╗░██╗███████╗████████╗░█████╗░
██╔════╝░██╔══██╗██║██╔════╝╚══██╔══╝██╔══██╗
██║░░██╗░██████╔╝██║█████╗░░░░░██║░░░███████║
██║░░╚██╗██╔══██╗██║██╔══╝░░░░░██║░░░██╔══██║
╚██████╔╝██║░░██║██║███████╗░░░██║░░░██║░░██║
░╚═════╝░╚═╝░░╚═╝╚═╝╚══════╝░░░╚═╝░░░╚═╝░░╚═╝

██╗███╗░░██╗██╗░░░██╗░█████╗░░█████╗░░█████╗░██████╗░░█████╗░██████╗░
██║████╗░██║██║░░░██║██╔══██╗██╔══██╗██╔══██╗██╔══██╗██╔══██╗██╔══██╗
██║██╔██╗██║╚██╗░██╔╝██║░░██║██║░░╚═╝███████║██║░░██║██║░░██║██████╔╝
██║██║╚████║░╚████╔╝░██║░░██║██║░░██╗██╔══██║██║░░██║██║░░██║██╔══██╗
██║██║░╚███║░░╚██╔╝░░╚█████╔╝╚█████╔╝██║░░██║██████╔╝╚█████╔╝██║░░██║
╚═╝╚═╝░░╚══╝░░░╚═╝░░░░╚════╝░░╚════╝░╚═╝░░╚═╝╚═════╝░░╚════╝░╚═╝░░╚═╝[/]");

        Console.WriteLine("");


        Thread.Sleep(3000); // Retraso de 3 segundos
    }

    public void mensajeIntroduccion()
    {
        Console.WriteLine("");
        AnsiConsole.Markup(@"[italic][Gray]
Hoy, presenciamos un evento singular y epico: un enfrentamiento uno
contra uno donde solo los mas fuertes, habiles y astutos prevaleceran.

Preparense, afilen sus habilidades y desaten el poder de sus campeones.
La batalla por la supremacia individual en la Grieta del Invocador 
comienza ahora. ¡Que los mejores invocadores se enfrenten y que la 
victoria corone al mas digno![/][/]
");
        Console.WriteLine("");
        Thread.Sleep(5000);
    }

    public void preguntaSobrePersonaje()
    {
        Console.WriteLine("");
        AnsiConsole.Markup("[italic][Gray]Invocador, ¿que campeon elegiras para enfrentarte en la Grieta del Invocador? introduce el numero correspondiente a tu eleccion:[/][/]");
    }

    public void errorPersonaje()
    {
        Console.WriteLine("");
        AnsiConsole.Markup(@"[Red]Lo siento invocador, ese personaje no se encuentra disponible![/]");
        Console.WriteLine("");
    }
}

public class mostrarPanel
{
    private Personaje personaje;

    
    public void mostrarPersonajeElegido(Personaje Personaje)
    {
        Console.WriteLine("");
        var panelPersonaje = new Panel($"[Black]NOMBRE:[/][Cyan]{Personaje.getDatos.Name}[/] [Black]REGION:[/][Cyan]{Personaje.getDatos.Region}[/] [Black]CLASE:[/][Cyan]{Personaje.getDatos.Tipoclase}[/]");
        panelPersonaje.Header = new PanelHeader("PERSONAJE ELEGIDO");
        panelPersonaje.Border = BoxBorder.Ascii;
        panelPersonaje.BorderColor(Color.Aquamarine1);
        panelPersonaje.Header.Centered();
        AnsiConsole.Write(panelPersonaje); //MUESTRO EL PANEL
        Thread.Sleep(2000);
        Console.WriteLine("");
    }
    public void mostrarOponente1(Personaje Personaje)
    {
        Console.WriteLine("");
        var panelOponente = new Panel($"[Black]NOMBRE:[/][Red]{Personaje.getDatos.Name}[/] [Black]REGION:[/][Red]{Personaje.getDatos.Region}[/] [Black]CLASE:[/][Red]{Personaje.getDatos.Tipoclase}[/]");
        panelOponente.Header = new PanelHeader("¡TU OPONENTE HA APARECIDO!").Centered();
        panelOponente.Border = BoxBorder.Ascii;
        panelOponente.BorderColor(Color.Red);
        panelOponente.Header.Centered();
        AnsiConsole.Write(panelOponente);
    }

    public void mostrarOponente2(Personaje Personaje)
    {
        AnsiConsole.Markup("[Cyan]Felicidades Invocador, has pasado a la siguiente pelea![/]");
        Console.WriteLine("");
        Console.WriteLine("");
        Thread.Sleep(2000);
        var panelOponente = new Panel($"[Black]NOMBRE:[/][Red]{Personaje.getDatos.Name}[/] [Black]REGION:[/][Red]{Personaje.getDatos.Region}[/] [Black]CLASE:[/][Red]{Personaje.getDatos.Tipoclase}[/]");
        panelOponente.Header = new PanelHeader("¡TU NUEVO OPONENTE HA APARECIDO!").Centered();
        panelOponente.Border = BoxBorder.Ascii;
        panelOponente.BorderColor(Color.Red);
        panelOponente.Header.Centered();
        AnsiConsole.Write(panelOponente);
    }

    public void mostrarOponente3(Personaje Personaje)
    {
        AnsiConsole.Markup("[Cyan]Felicidades Invocador, has pasado a la siguiente pelea![/]");
        Console.WriteLine("");
        Console.WriteLine("");
        Thread.Sleep(2000);
        var panelOponente = new Panel($"[Black]NOMBRE:[/][Red]{Personaje.getDatos.Name}[/] [Black]REGION:[/][Red]{Personaje.getDatos.Region}[/] [Black]CLASE:[/][Red]{Personaje.getDatos.Tipoclase}[/]");
        panelOponente.Header = new PanelHeader("¡TU ULTIMO OPONENTE HA APARECIDO!");
        panelOponente.Border = BoxBorder.Ascii;
        panelOponente.BorderColor(Color.Red);
        panelOponente.Header.Centered();
        AnsiConsole.Write(panelOponente);
    }

}

