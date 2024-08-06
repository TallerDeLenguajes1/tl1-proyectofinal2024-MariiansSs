namespace PersonajesSpace;
using System.Text.Json.Serialization;
using Spectre.Console; // Mostrar tablas/diseños por consola


public class Personaje
{
    private Caracteristicas caracteristicas;
    private Datos datos;

    public Personaje(int velocidad, int destreza, int fuerza, int armadura, string nombre, string region, string tipoClase, string frase)
    {
        this.getCaracteristicas = new Caracteristicas(velocidad, destreza, fuerza, armadura);
        this.getDatos = new Datos(nombre, region, tipoClase, frase);
    }

    public Personaje()
    {
        //Para deserializar
    }


    public Caracteristicas getCaracteristicas { get => caracteristicas; set => caracteristicas = value; }
    public Datos getDatos { get => datos; set => datos = value; }

    public class Caracteristicas
    {

        private int velocidad;
        private int destreza;
        private int fuerza;
        private int armadura;
        private int salud;
        private int nivelFuria;
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
            this.pocionDeVida = numeroRandom.Next(1, 81);
        }
    }

    public class Datos
    {
        private string name;

        private string region;//Demacia, Noxus, freljord

        private string tipoClase; //Luchador, Tirador, Asesino, Mago;

        private string frase;

        [JsonPropertyName("Name")]
        public string Name { get => name; }

        [JsonPropertyName("Region")]
        public string Region { get => region; }

        [JsonPropertyName("Tipoclase")]
        public string Tipoclase { get => tipoClase; }

        [JsonPropertyName("Frase")]
        public string Frase { get => frase; }


        public Datos(string Name, string Region, string Tipoclase, string Frase)
        {
            this.name = Name;
            this.region = Region;
            this.tipoClase = Tipoclase;
            this.frase = Frase;
        }
    }

    public int atacar()
    {
        Random efectividadRandom = new Random();
        int ataque = getCaracteristicas.Destreza * getCaracteristicas.Fuerza;
        int efectividad = efectividadRandom.Next(1, 101);
        int defensa = getCaracteristicas.Armadura * getCaracteristicas.Velocidad;
        int ajuste = 55;
        int danioProvocado = ((ataque * efectividad) - defensa) / ajuste;

        if (getCaracteristicas.Nivelfuria == 3)
        {
            danioProvocado = danioProvocado * 2;
            Console.WriteLine("");
            AnsiConsole.Markup($"[Red]{getDatos.Frase}[/][Black](ESTE PERSONAJE USO SU DEFINITIVA)[/]");
            Console.WriteLine("");
            getCaracteristicas.Nivelfuria = 0; //Reseteo el nivel de furia para equilibrar el combate
        }
        AnsiConsole.Write($@"{getDatos.Name} ATACO CON UNA EFECTIVIDAD DE {efectividad}% Y DAÑO DE {danioProvocado}%");
        Console.WriteLine("");
        return danioProvocado;
    }

    public void restarSalud(int danioProvocado)
    {
        caracteristicas.Salud -= danioProvocado;
        caracteristicas.Nivelfuria++;

    }

    public void restaurarSalud()
    {
        caracteristicas.Salud = 100;
    }

    public void tomarPocion()
    {
        getCaracteristicas.Salud += getCaracteristicas.Pociondevida;
        if (getCaracteristicas.Salud > 100)
        {
            getCaracteristicas.Salud = 100;
        }

        Console.WriteLine("");

        AnsiConsole.Write($"{getDatos.Name} SE HA CURADO UN TOTAL DE {getCaracteristicas.Pociondevida}%");

        Console.WriteLine("");
    }

    public void aumentarDanio()
    {
        getCaracteristicas.Fuerza++;
        if (getCaracteristicas.Fuerza == 10)
        {
            getCaracteristicas.Fuerza = 10;
        }
    }

    public void subirFuria()
    {
        getCaracteristicas.Nivelfuria++;
        if(getCaracteristicas.Nivelfuria > 3)
        {
            getCaracteristicas.Nivelfuria = 3;
        }
        
    }
}

public class FabricaDePersonajes
{
    private List<Personaje> listaPersonajes;

    public FabricaDePersonajes()
    {
        listaPersonajes = new List<Personaje>();
    }
    public List<Personaje> CrearPersonajes()
    {
        Random ER = new Random(); // ESTADISTICA RANDOM 

        listaPersonajes.Add(new Personaje(ER.Next(10, 110), ER.Next(10, 110), ER.Next(10, 110), ER.Next(10, 110), "Garen", "Demacia", "Luchador", "JUUSTIICIAAAA!!!"));
        listaPersonajes.Add(new Personaje(ER.Next(1, 11), ER.Next(1, 11), ER.Next(1, 11), ER.Next(1, 11), "Lux", "Demacia", "Mago", "INCANDESCENCIA!!!"));
        listaPersonajes.Add(new Personaje(ER.Next(1, 11), ER.Next(1, 11), ER.Next(1, 11), ER.Next(1, 11), "Lucian", "Demacia", "Tirador", "NO HABRA PERDON!!!"));
        listaPersonajes.Add(new Personaje(ER.Next(1, 11), ER.Next(1, 11), ER.Next(1, 11), ER.Next(1, 11), "Darius", "Noxus", "Luchador", "NO PUEDES ESCAPAR!!!"));
        listaPersonajes.Add(new Personaje(ER.Next(1, 11), ER.Next(1, 11), ER.Next(1, 11), ER.Next(1, 11), "Samira", "Noxus", "Tirador", "VAMOS, VAMOS, VAMOS!!!"));
        listaPersonajes.Add(new Personaje(ER.Next(1, 11), ER.Next(1, 11), ER.Next(1, 11), ER.Next(1, 11), "Swain", "Noxus", "Mago", "EL PODER DEL IMPERIO!!!"));
        listaPersonajes.Add(new Personaje(ER.Next(1, 11), ER.Next(1, 11), ER.Next(1, 11), ER.Next(1, 11), "Ashe", "Freljord", "Tirador", "FUEGO A DISCRECION!!!"));
        listaPersonajes.Add(new Personaje(ER.Next(1, 11), ER.Next(1, 11), ER.Next(1, 11), ER.Next(1, 11), "Sejuani", "Freljord", "Luchador", "CONGELATE!!!"));
        listaPersonajes.Add(new Personaje(ER.Next(1, 11), ER.Next(1, 11), ER.Next(1, 11), ER.Next(1, 11), "Lissandra", "Freljord", "Mago", "CONGELATE Y ROMPE!!!"));
        listaPersonajes.Add(new Personaje(ER.Next(1, 11), ER.Next(1, 11), ER.Next(1, 11), ER.Next(1, 11), "RekSai", "Vacio", "Luchador", "*Rugidos y ruidos extranios*"));

        return listaPersonajes;
    }

    public Personaje buscarPersonajes(int opcion, List<Personaje> listaPersonajes)
    {
        Random personajeRandom = new Random();
        int bandera = 0;
        while (bandera == 0)
        {
            foreach (Personaje personaje in listaPersonajes)
            {
                if (opcion >= 0 && opcion <= 9)
                {
                    bandera = 1;
                    break;
                }
                else
                {
                    AnsiConsole.Markup("[Red]Lo siento!, por ahora no tenemos disponible esa opcion, porfavor, elige nuevamente las anteriormente mencionadas[/]");
                    string nuevaOpcion = Console.ReadLine();

                    if (int.TryParse(nuevaOpcion, out opcion))
                    {
                        break;
                    }
                    else
                    {
                        AnsiConsole.Markup("[Red]Demasiados intentos Invocador, hemos elegido un personaje aleatorio para ti![/]");
                        opcion = personajeRandom.Next(0, 10);
                    }
                }
            }
        }

        return listaPersonajes[opcion];
    }

    public void mostrarPersonajeAElegir(List<Personaje> listaPersonajes)
    {
        var tabla = new Table().Title("[Blue]PERSONAJES[/]");
        tabla.AddColumn("[Black]ID[/]");
        tabla.AddColumn("[Black]NOMBRE[/]");
        tabla.AddColumn("[Black]REGION[/]");  // Agrego columnas a la tabla
        tabla.AddColumn("[Black]CLASE[/]");

        tabla.Border(TableBorder.Ascii2).BorderColor(Color.Blue);
        for (int i = 0; i < listaPersonajes.Count; i++)
        {
            Personaje mostrarPersonaje = listaPersonajes[i];
            tabla.AddRow(
                i.ToString(), // Índice como texto simple  
                mostrarPersonaje.getDatos.Name, // Nombre
                mostrarPersonaje.getDatos.Region, // Región
                mostrarPersonaje.getDatos.Tipoclase // Clase
            ); // Agrego filas

        }

        AnsiConsole.Render(tabla); // Mostrar tabla
    }

    public Personaje generarOponente(List<Personaje> listaPersonajes)
    {
        Random oponenteRandom = new Random();
        int oponente = oponenteRandom.Next(listaPersonajes.Count);

        return listaPersonajes[oponente];
    }

}





