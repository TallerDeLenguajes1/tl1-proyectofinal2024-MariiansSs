namespace PersonajesSpace;
using System.Text.Json.Serialization;


public class Personajes
{
    private Caracteristicas caracteristicas;
    private Datos datos;
    
    public Personajes(int velocidad, int destreza, int fuerza, int armadura, string nombre, string region, string tipoClase)
    {
        this.Caracteristicas1 = new Caracteristicas(velocidad, destreza, fuerza, armadura);
        this.Datos1 = new Datos(nombre, region, tipoClase);
    }

    public Personajes()
    {
           //Para deserializar
    }

    
    public Caracteristicas Caracteristicas1 { get => caracteristicas; set => caracteristicas = value; }
    public Datos Datos1 { get => datos; set => datos = value; }

    public class Caracteristicas
    {

        private int velocidad;
        private int destreza;
        private int fuerza;
        private int armadura;
        private int salud;
        private int nivelFuria; // 1 - 5
        private int pocionDeVida;

        
        [JsonPropertyName("Velocidad")]
        public int Velocidad { get => velocidad; }

        [JsonPropertyName("Destreza")]
        public int Destreza { get => destreza; }

        [JsonPropertyName("Fuerza")]
        public int Fuerza { get => fuerza; set => fuerza = value; }

        [JsonPropertyName("Armadura")]
        public int Armadura { get => armadura; }

        [JsonPropertyName("Salud")]
        public int Salud { get => salud; set => salud = value; }

        [JsonPropertyName("Nivelfuria")]
        public int Nivelfuria { get => nivelFuria; set => nivelFuria = value; }

        [JsonPropertyName("Pociondevida")]
        public int Pociondevida { get => pocionDeVida; }


        public Caracteristicas(int Velocidad, int Destreza, int Fuerza, int Armadura)
        {
            Random numeroRandom = new Random();

            this.velocidad = Velocidad;
            this.destreza = Destreza;
            this.fuerza = Fuerza;
            this.armadura = Armadura;
            this.salud = 100;
            this.nivelFuria = 0;
            this.pocionDeVida = numeroRandom.Next(0, 31);
        }
    }

    public class Datos
    {
        private string name;

        private string region;//Demacia, Noxus, freljord

        private string tipoClase; //Luchador, Tirador, Asesino, Mago;

        [JsonPropertyName("Name")]
        public string Name { get => name; }

        [JsonPropertyName("Region")]
        public string Region { get => region; }

        [JsonPropertyName("Tipoclase")]
        public string Tipoclase { get => tipoClase; }

        public Datos(string Name, string Region, string Tipoclase)
        {
            this.name = Name;
            this.region = Region;
            this.tipoClase = Tipoclase;
        }
    }

}

public class FabricaDePersonajes
{
    private List<Personajes> listaPersonajes;

    public FabricaDePersonajes()
    {
        listaPersonajes = new List<Personajes>();
    }
    public List<Personajes> CrearPersonajes()
    {
        Random ER = new Random(); // ESTADISTICA RANDOM 

        listaPersonajes.Add(new Personajes(ER.Next(1,10), ER.Next(1,10),ER.Next(1,10), ER.Next(1,10), "Garen", "Demacia", "Luchador"));
        listaPersonajes.Add(new Personajes(ER.Next(1,10), ER.Next(1,10), ER.Next(1,10), ER.Next(1,10), "Lux", "Demacia", "Mago"));
        listaPersonajes.Add(new Personajes(ER.Next(1,10), ER.Next(1,10), ER.Next(1,10), ER.Next(1,10), "Lucian", "Demacia", "Tirador"));
        listaPersonajes.Add(new Personajes(ER.Next(1,10), ER.Next(1,10), ER.Next(1,10), ER.Next(1,10), "Darius", "Noxus", "Luchador"));
        listaPersonajes.Add(new Personajes(ER.Next(1,10), ER.Next(1,10), ER.Next(1,10), ER.Next(1,10), "Samira", "Noxus", "Tirador"));
        listaPersonajes.Add(new Personajes(ER.Next(1,10), ER.Next(1,10), ER.Next(1,10), ER.Next(1,10), "Swain", "Noxus", "Mago"));
        listaPersonajes.Add(new Personajes(ER.Next(1,10), ER.Next(1,10), ER.Next(1,10), ER.Next(1,10), "Ashe", "Freljord", "Tirador"));
        listaPersonajes.Add(new Personajes(ER.Next(1,10), ER.Next(1,10), ER.Next(1,10), ER.Next(1,10), "Sejuani", "Freljord", "Luchador"));
        listaPersonajes.Add(new Personajes(ER.Next(1,10), ER.Next(1,10), ER.Next(1,10), ER.Next(1,10), "Lissandra", "Freljord", "Mago"));
        listaPersonajes.Add(new Personajes(ER.Next(1,10), ER.Next(1,10), ER.Next(1,10), ER.Next(1,10), "RekSai", "Vacio", "Luchador"));

        return listaPersonajes;
    }

    public Personajes buscarPersonajes(int opcion, List<Personajes> listaPersonajes)
    {
        Random personajeRandom = new Random();
        int bandera = 0;
        while (bandera == 0)
        {
            foreach (Personajes personaje in listaPersonajes)
            {
                if (opcion >= 0 && opcion <= 9)
                {
                    bandera = 1;
                    break;
                }
                else
                {
                    Console.WriteLine("Lo siento!, por ahora no tenemos disponible esa opcion, porfavor, elige las anteriormente mencionadas");
                    string nuevaOpcion = Console.ReadLine();

                    if(int.TryParse(nuevaOpcion, out opcion))
                    {
                        break;
                    }
                    else
                    {
                      Console.WriteLine("Demasiados intentos Invocador, hemos elegido un personaje aleatorio para ti!");
                      opcion = personajeRandom.Next(0,10);
                    }
                }
            }
        }

        return listaPersonajes[opcion];
    }

    public void mostrarPersonajeAElegir(List<Personajes> listaPersonajes)
    {
        Console.WriteLine("----PERSONAJES PARA ELEGIR-----");
        for (int i = 0; i < listaPersonajes.Count; i++)
        {
            Personajes mostrarPersonaje = listaPersonajes[i];
            Console.WriteLine($"[{i}] {mostrarPersonaje.Datos1.Name} {mostrarPersonaje.Datos1.Region} {mostrarPersonaje.Datos1.Tipoclase}");
            
        }
    }

    public Personajes generarOponente(List<Personajes> listaPersonajes)
    {
        Random oponenteRandom = new Random();
        int oponente = oponenteRandom.Next(listaPersonajes.Count);

        return listaPersonajes[oponente];
    }
    
}





