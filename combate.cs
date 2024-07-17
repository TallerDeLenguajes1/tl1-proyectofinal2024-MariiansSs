namespace combateSpace;
using PersonajesSpace;


public class Combate
{
    private Personajes PersonajeElegido;

    private Personajes PersonajeOponente;

    public int iniciarCombate(Personajes PersonajeElegido, Personajes PersonajeOponente)
    {
        Random decisionAleatoria = new Random();
        int decisionPersonaje;
        int ganador = 0;
        while (PersonajeElegido.Caracteristicas1.Salud >= 0 && PersonajeOponente.Caracteristicas1.Salud >= 0)
        {
            if (PersonajeElegido.Caracteristicas1.Salud > 0)
            {
                decisionPersonaje = decisionAleatoria.Next(1, 3);
                switch (decisionPersonaje)
                {
                    case 1:
                        atacar(PersonajeElegido, PersonajeOponente);
                        break;
                    case 2:
                        if (PersonajeElegido.Caracteristicas1.Salud < 30)
                        {
                            tomarPocion(PersonajeElegido);
                        }
                        break;
                }
            }

            if (PersonajeOponente.Caracteristicas1.Salud > 0)
            {
                decisionPersonaje = decisionAleatoria.Next(1, 3);
                switch (decisionPersonaje)
                {
                    case 1:
                        atacar(PersonajeOponente, PersonajeElegido);
                        break;
                    case 2:
                        if (PersonajeOponente.Caracteristicas1.Salud < 30)
                        {
                            tomarPocion(PersonajeOponente);
                        }
                        break;
                }
            }
        }


        if (PersonajeElegido.Caracteristicas1.Salud <= 0)
        {
            Console.WriteLine($"Lo siento invocador!, {PersonajeElegido.Datos1.Name} ha sido derrotado.");
            Console.WriteLine("FIN DEL JUEGO");
            ganador = 0;
        }


        if (PersonajeOponente.Caracteristicas1.Salud <= 0)
        {
            Console.WriteLine($"{PersonajeOponente.Datos1.Name} ha sido derrotado.");
            Console.WriteLine("Felicidades Invocador, has pasado a la siguiente pelea!");
            PersonajeElegido.Caracteristicas1.Salud = 100;
            ganador = 1;
        }

        return ganador;
    }

    public void atacar(Personajes Atacante, Personajes Defensor)
    {
        Random efectividadRandom = new Random();
        int ataque = Atacante.Caracteristicas1.Destreza * Atacante.Caracteristicas1.Fuerza;
        int efectividad = efectividadRandom.Next(1, 101);
        int defensa = Atacante.Caracteristicas1.Armadura * Atacante.Caracteristicas1.Velocidad;
        int ajuste = 50;
        int danioProvocado = ((ataque * efectividad) - defensa) / ajuste;

        if (Atacante.Caracteristicas1.Nivelfuria == 5)
        {
            danioProvocado = danioProvocado * 2;
        }

        Defensor.Caracteristicas1.Salud -= danioProvocado;
        Defensor.Caracteristicas1.Nivelfuria++;

        Console.WriteLine($"{Atacante.Datos1.Name} ATACO CON UNA EFECTIVIDAD DE {efectividad} Y DAÑO DE {danioProvocado}");
        Console.WriteLine($"VIDA RESTANTE DE {Defensor.Datos1.Name} ES DE %{Defensor.Caracteristicas1.Salud}");

    }
    public void tomarPocion(Personajes Personaje)
    {
        if (Personaje.Caracteristicas1.Salud == 100)
        {
            Personaje.Caracteristicas1.Salud = 100;
            Console.WriteLine($"Vaya! parece que {Personaje.Datos1.Name} ha intentado algo que no sirvio para nada");
        }
        else
        {
            Personaje.Caracteristicas1.Salud += Personaje.Caracteristicas1.Pociondevida;
            if (Personaje.Caracteristicas1.Salud > 100)
            {
                Personaje.Caracteristicas1.Salud = 100;
            }
            Console.WriteLine($"{Personaje.Datos1.Name} SE HA CURADO UN TOTAL DE {Personaje.Caracteristicas1.Pociondevida}");
        }



    }
}