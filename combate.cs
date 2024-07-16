namespace combateSpace;
using PersonajesSpace;
using mensajesSpace;

public class Combate
{
    private Personajes PersonajeElegido;

    private Personajes PersonajeOponente;

    public Combate(Personajes PersonajeElegido, Personajes PersonajeOponente)
    {
        this.PersonajeElegido = PersonajeElegido;
        this.PersonajeOponente = PersonajeOponente;
    }

    public Personajes iniciarCombate(Personajes PersonajeElegido, Personajes PersonajeOponente)
    {
        int decision, danioProvocado,;
        int banderaDecision = 0;
        Personajes ganador = new Personajes();
        while (PersonajeElegido.Caracteristicas1.Salud >= 0 || PersonajeElegido.Caracteristicas1.Salud >= 0)
            Console.WriteLine("Invocador, ¿cual sera tu primera decision de combate?");
        Console.WriteLine("1: ATACAR ");
        Console.WriteLine("2: CURARSE ");
        Console.WriteLine("3: ¡UTILIZAR LA DEFINITIVA! ");
        if (int.TryParse(Console.ReadLine(), out decision))
        {
            while (banderaDecision == 0)
            {
                switch (decision)
                {
                    case 1:
                        danioProvocado = PersonajeElegido.atacar();
                        PersonajeOponente.reducirSalud(danioProvocado);
                        PersonajeOponente.Caracteristicas1.Nivelfuria++;
                        banderaDecision = 1;
                        break;
                    case 2:
                        PersonajeElegido.tomarPocion();
                        banderaDecision = 1;
                        break;
                    case 3:
                        danioProvocado = PersonajeElegido.atacar();
                        if(PersonajeElegido.Caracteristicas1.Nivelfuria == 5)
                        {
                            danioProvocado = danioProvocado * 2;
                            banderaDecision = 1;
                        }else
                        {
                            Console.WriteLine("Invocador, tu personaje esta demasiado tranquilo para usar esta accion!");
                        }
                        break;
             }
            }
        }
        else
            {
                Console.WriteLine($"Mala idea Invocador!, al parecer {PersonajeElegido.Datos1.Name} se ha distraido y perdio su turno");
            }

        if (PersonajeOponente.Caracteristicas1.Salud >= 0)
        {
            Random decisionAleatoria = new Random();
            int decisionOponente = decisionAleatoria.Next(1, 3);
        }



        return ganador;
    }

}