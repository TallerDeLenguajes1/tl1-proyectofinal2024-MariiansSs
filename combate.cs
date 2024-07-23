namespace combateSpace;
using PersonajesSpace;
using System.Threading; // Para retrasar la muestra de mensajes


public class Combate
{
    private Personajes PersonajeElegido;

    private Personajes PersonajeOponente;

    public int iniciarCombate(Personajes PersonajeElegido, Personajes PersonajeOponente)
    {
        Random decisionAleatoria = new Random();
        int decisionPersonaje;
        int ganador = 0;
        while (PersonajeElegido.Caracteristicas1.Salud > 0 && PersonajeOponente.Caracteristicas1.Salud > 0)
        {
            if (PersonajeElegido.Caracteristicas1.Salud > 0)
            {
                decisionPersonaje = decisionAleatoria.Next(1, 3);
                if(decisionPersonaje == 1)
                {
                    atacar(PersonajeElegido, PersonajeOponente);
                    
                }else 
                    if(decisionPersonaje == 2 && PersonajeElegido.Caracteristicas1.Salud < 30)
                    {
                        tomarPocion(PersonajeElegido);
                        
                    }else
                    {
                        atacar(PersonajeElegido, PersonajeOponente);     
                    }   
            }
        
             if (PersonajeOponente.Caracteristicas1.Salud > 0)
            {
                decisionPersonaje = decisionAleatoria.Next(1, 3);
                if(decisionPersonaje == 1)
                {
                    atacar(PersonajeOponente, PersonajeElegido);
                    
                }else 
                    if(decisionPersonaje == 2 && PersonajeOponente.Caracteristicas1.Salud < 30)
                    {
                        tomarPocion(PersonajeOponente);
                        
                    }else
                    {
                        atacar(PersonajeOponente, PersonajeElegido);
                        
                    }
            }   

        if (PersonajeElegido.Caracteristicas1.Salud <= 0)
        {
            Console.WriteLine($"Lo siento invocador!, {PersonajeElegido.Datos1.Name} ha sido derrotado.");
            Thread.Sleep(3000);
            Console.WriteLine("FIN DEL JUEGO");
            Thread.Sleep(3000);
            ganador = 0;
        }


        if (PersonajeOponente.Caracteristicas1.Salud <= 0)
        {
            Console.WriteLine($"{PersonajeOponente.Datos1.Name} ha sido derrotado.");
            Thread.Sleep(3000);
            Console.WriteLine("Felicidades Invocador, has pasado a la siguiente pelea!");
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
        int ajuste = 50;
        int danioProvocado = ((ataque * efectividad) - defensa) / ajuste;

        if (Atacante.Caracteristicas1.Nivelfuria == 5)
        {
            danioProvocado = danioProvocado * 2;
        }

        Defensor.Caracteristicas1.Salud -= danioProvocado;
        Defensor.Caracteristicas1.Nivelfuria++;

        if(Defensor.Caracteristicas1.Salud < 0 )
        {
            Defensor.Caracteristicas1.Salud = 0;
        }
        Console.WriteLine($@"{Atacante.Datos1.Name} ATACO CON UNA EFECTIVIDAD DE {efectividad} Y DAÑO DE {danioProvocado} - VIDA RESTANTE DE {Defensor.Datos1.Name} ES DE %{Defensor.Caracteristicas1.Salud}");
        Thread.Sleep(2000);
    }
    public void tomarPocion(Personajes Personaje)
    {
            Personaje.Caracteristicas1.Salud += Personaje.Caracteristicas1.Pociondevida;
            if (Personaje.Caracteristicas1.Salud > 100)
            {
                Personaje.Caracteristicas1.Salud = 100;
            }
            Console.WriteLine($"{Personaje.Datos1.Name} SE HA CURADO UN TOTAL DE {Personaje.Caracteristicas1.Pociondevida}");
            Thread.Sleep(2000);
        }
    }
    


    