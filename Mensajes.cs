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
En este coliseo de batalla, cada invocador elegira un campeon que luchara ferozmente contra 
su oponente, demostrando su maestria en combate, estrategia y dominio 
del campeon elegido.

Las reglas son simples: el vencedor de cada combate avanzara a la 
siguiente ronda, enfrentandose a un nuevo rival. Asi continuara la 
contienda, hasta que solo quede un campeon en pie. Este ultimo 
guerrero, habiendo superado a todos los demas, sera proclamado el ganador de esta jornada de duelos.

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
    private Personajes personaje;

    public mostrarPanel(Personajes Personaje)
    {
        this.personaje = Personaje;
    }

    public void mostrar1(Personajes Personaje)
    {
        Console.WriteLine("");
        var panelOponente = new Panel($"[Black]NOMBRE:[/][Red]{Personaje.Datos1.Name}[/] [Black]REGION:[/][Red]{Personaje.Datos1.Region}[/] [Black]CLASE:[/][Red]{Personaje.Datos1.Tipoclase}[/]");
        panelOponente.Header = new PanelHeader("¡TU OPONENTE HA APARECIDO!").Centered();
        panelOponente.Border = BoxBorder.Ascii;
        panelOponente.BorderColor(Color.Red);
        panelOponente.Header.Centered();
        AnsiConsole.Write(panelOponente);
    }

    public void mostrar2(Personajes Personaje)
    {
        Console.WriteLine("");
        var panelOponente = new Panel($"[Black]NOMBRE:[/][Red]{Personaje.Datos1.Name}[/] [Black]REGION:[/][Red]{Personaje.Datos1.Region}[/] [Black]CLASE:[/][Red]{Personaje.Datos1.Tipoclase}[/]");
        panelOponente.Header = new PanelHeader("¡TU NUEVO OPONENTE HA APARECIDO!").Centered();
        panelOponente.Border = BoxBorder.Ascii;
        panelOponente.BorderColor(Color.Red);
        panelOponente.Header.Centered();
        AnsiConsole.Write(panelOponente);
    }

    public void mostrar3(Personajes Personaje)
    {
        Console.WriteLine("");
        var panelOponente = new Panel($"[Black]NOMBRE:[/][Red]{Personaje.Datos1.Name}[/] [Black]REGION:[/][Red]{Personaje.Datos1.Region}[/] [Black]CLASE:[/][Red]{Personaje.Datos1.Tipoclase}[/]");
        panelOponente.Header = new PanelHeader("¡TU ULTIMO OPONENTE HA APARECIDO!");
        panelOponente.Border = BoxBorder.Ascii;
        panelOponente.BorderColor(Color.Red);
        panelOponente.Header.Centered();
        AnsiConsole.Write(panelOponente);
    }
}

