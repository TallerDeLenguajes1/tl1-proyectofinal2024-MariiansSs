namespace Personajes;

public class Personaje
{
    private Caracteristicas caracteristicas;

    private Datos datos;
    public Personaje(int velocidad, int destreza, int fuerza, int armadura, string nombre, string region, string tipoClase)
    {
        this.Caracteristicas1 = new Caracteristicas(velocidad, destreza, fuerza, armadura);
        this.Datos1 = new Datos(nombre, region, tipoClase);
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

        public int Velocidad { get => velocidad; }
        public int Destreza { get => destreza; }
        public int Fuerza { get => fuerza; set => fuerza = value; }
        public int Armadura { get => armadura; }
        public int Salud { get => salud; set => salud = value; }
        public int Nivelfuria { get => nivelFuria; set => nivelFuria = value; }

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

        public string Name { get => name; }
        public string Region { get => region; }
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
    private List<Personaje> listaPersonajes;

    public FabricaDePersonajes()
    {
        listaPersonajes = new List<Personaje>();
        CrearPersonajes();
    }

    private void CrearPersonajes()
    {
        listaPersonajes.Add(new Personaje(2, 1, 6, 9, "Garen", "Demacia", "Luchador"));
        listaPersonajes.Add(new Personaje(4, 2, 8, 7, "Lux", "Demacia", "Mago"));
        listaPersonajes.Add(new Personaje(9, 7, 9, 2, "Lucian", "Demacia", "Tirador"));
        listaPersonajes.Add(new Personaje(2, 1, 9, 6, "Darius", "Noxus", "Luchador"));
        listaPersonajes.Add(new Personaje(9, 7, 9, 2, "Samira", "Noxus", "Tirador"));
        listaPersonajes.Add(new Personaje(2, 4, 7, 7, "Swain", "Noxus", "Mago"));
        listaPersonajes.Add(new Personaje(9, 7, 9, 2, "Ashe", "Freljord", "Tirador"));
        listaPersonajes.Add(new Personaje(1, 1, 6, 10, "Sejuani", "Freljord", "Luchador"));
        listaPersonajes.Add(new Personaje(5, 5, 8, 5, "Lissandra", "Freljord", "Mago"));
    }

    public Personaje buscarPersonajes(int opcion)
    {
        int bandera = 0;
        while (bandera == 0)
        {
            foreach (Personaje personaje in listaPersonajes)
            {
                if (opcion >= 0 || opcion <= 8)
                {
                    bandera = 1;
                }
                else
                {
                    Console.WriteLine("INGRESE UN VALOR VALIDO");
                }
            }
        }

        return listaPersonajes[opcion];
    }

    public void mostrarPersonaje(Personaje PersonajeAmostrar)
    {
        Console.Write("---PERSONAJE ELEGIDO---\n");
        Console.Write("NOMBRE DEL PERSONAJE:" + PersonajeAmostrar.Datos1.Name + "\n");
        Console.Write("REGION DEL PERSONAJE:" + PersonajeAmostrar.Datos1.Region + "\n");
        Console.Write("CLASE DEL PERSONAJE:" + PersonajeAmostrar.Datos1.Tipoclase + "\n");
    }

}