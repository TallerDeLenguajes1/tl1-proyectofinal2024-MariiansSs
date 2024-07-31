namespace PersonajesSpace;
using System.Text.Json.Serialization;
using Spectre.Console; // Mostrar tablas/diseños por consola


public class Personajes
{
    private Caracteristicas caracteristicas;
    private Datos datos;
    
    public Personajes(int velocidad, int destreza, int fuerza, int armadura, string nombre, string region, string tipoClase, string frase)
    {
        this.Caracteristicas1 = new Caracteristicas(velocidad, destreza, fuerza, armadura);
        this.Datos1 = new Datos(nombre, region, tipoClase,frase);
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
        public string Frase {get => frase;}
        

        public Datos(string Name, string Region, string Tipoclase, string Frase)
        {
            this.name = Name;
            this.region = Region;
            this.tipoClase = Tipoclase;
            this.frase = Frase;
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

        listaPersonajes.Add(new Personajes(ER.Next(1,11), ER.Next(1,11),ER.Next(1,11), ER.Next(1,11), "Garen", "Demacia", "Luchador","JUUSTIICIAAAA!!!"));
        listaPersonajes.Add(new Personajes(ER.Next(1,11), ER.Next(1,11), ER.Next(1,11), ER.Next(1,11), "Lux", "Demacia", "Mago","INCANDESCENCIA!!!"));
        listaPersonajes.Add(new Personajes(ER.Next(1,11), ER.Next(1,11), ER.Next(1,11), ER.Next(1,11), "Lucian", "Demacia", "Tirador","NO HABRA PERDON!!!"));
        listaPersonajes.Add(new Personajes(ER.Next(1,11), ER.Next(1,11), ER.Next(1,11), ER.Next(1,11), "Darius", "Noxus", "Luchador","NO PUEDES ESCAPAR!!!"));
        listaPersonajes.Add(new Personajes(ER.Next(1,11), ER.Next(1,11), ER.Next(1,11), ER.Next(1,11), "Samira", "Noxus", "Tirador","VAMOS, VAMOS, VAMOS!!!"));
        listaPersonajes.Add(new Personajes(ER.Next(1,11), ER.Next(1,11), ER.Next(1,11), ER.Next(1,11), "Swain", "Noxus", "Mago","EL PODER DEL IMPERIO!!!"));
        listaPersonajes.Add(new Personajes(ER.Next(1,11), ER.Next(1,11), ER.Next(1,11), ER.Next(1,11), "Ashe", "Freljord", "Tirador","FUEGO A DISCRECION!!!"));
        listaPersonajes.Add(new Personajes(ER.Next(1,11), ER.Next(1,11), ER.Next(1,11), ER.Next(1,11), "Sejuani", "Freljord", "Luchador","CONGELATE!!!"));
        listaPersonajes.Add(new Personajes(ER.Next(1,11), ER.Next(1,11), ER.Next(1,11), ER.Next(1,11), "Lissandra", "Freljord", "Mago","CONGELATE Y ROMPE!!!"));
        listaPersonajes.Add(new Personajes(ER.Next(1,11), ER.Next(1,11), ER.Next(1,11), ER.Next(1,11), "RekSai", "Vacio", "Luchador","*Rugidos y ruidos extranios*"));

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
                    AnsiConsole.Markup("[Red]Lo siento!, por ahora no tenemos disponible esa opcion, porfavor, elige nuevamente las anteriormente mencionadas[/]");
                    string nuevaOpcion = Console.ReadLine();

                    if(int.TryParse(nuevaOpcion, out opcion))
                    {
                        break;
                    }
                    else
                    {
                      Console.WriteLine("[Red]Demasiados intentos Invocador, hemos elegido un personaje aleatorio para ti![/]");
                      opcion = personajeRandom.Next(0,10);
                    }
                }
            }
        }

        return listaPersonajes[opcion];
    }

    public void mostrarPersonajeAElegir(List<Personajes> listaPersonajes)
    {
        var tabla = new Table().Title("[Blue]PERSONAJES[/]"); 
        tabla.AddColumn("[Black]ID[/]");  
        tabla.AddColumn("[Black]NOMBRE[/]");
        tabla.AddColumn("[Black]REGION[/]");  // Agrego columnas a la tabla
        tabla.AddColumn("[Black]CLASE[/]"); 
        
        tabla.Border(TableBorder.Ascii2).BorderColor(Color.Blue);
        for (int i = 0; i < listaPersonajes.Count; i++)
        {
            Personajes mostrarPersonaje = listaPersonajes[i];
            tabla.AddRow(
                i.ToString(), // Índice como texto simple  
                mostrarPersonaje.Datos1.Name, // Nombre
                mostrarPersonaje.Datos1.Region, // Región
                mostrarPersonaje.Datos1.Tipoclase // Clase
            ); // Agrego filas

        }
       
        AnsiConsole.Render(tabla); // Mostrar tabla
    }

    public Personajes generarOponente(List<Personajes> listaPersonajes)
    {
        Random oponenteRandom = new Random();
        int oponente = oponenteRandom.Next(listaPersonajes.Count);

        return listaPersonajes[oponente];
    }
    
}





