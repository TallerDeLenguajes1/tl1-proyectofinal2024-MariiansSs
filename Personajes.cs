namespace Personajes;

public class Personaje
{
    private Caracteristicas caracteristicas;

    private Datos datos;
    public Personaje(int velocidad, int destreza, int fuerza, int armadura, string nombre, string region, string tipoClase)
    {
        this.caracteristicas = new Caracteristicas(velocidad, destreza, fuerza, armadura);
        this.datos = new Datos(nombre, region, tipoClase);
    }
    public class Caracteristicas
    {
        private int velocidad;

        private int destreza;

        private int fuerza;

        private int armadura;

        private int salud;

        private int nivelFuria; // 1 - 5

        public int Velocidad { get => velocidad; }
        public int Destreza { get => destreza; }
        public int Fuerza { get => fuerza; set => fuerza = value; }
        public int Armadura { get => armadura; }
        public int Salud { get => salud; set => salud = value; }
        public int Nivelfuria {get => nivelFuria; set => nivelFuria = value;}

        
        public Caracteristicas(int Velocidad, int Destreza, int Fuerza, int Armadura)
        {
            this.velocidad = Velocidad;
            this.destreza = Destreza;
            this.fuerza = Fuerza;
            this.armadura = Armadura;
            this.salud = 100;
            this.nivelFuria = 0;
        }
    }

    public class Datos
    {
        private string name;

        private string region;//Demacia, Noxus, freljord

        private string tipoClase; //Luchador, Tirador, Asesino, Mago;

        public string Name { get => name;}
        public string Region { get => region;}
        public string Tipoclase { get => tipoClase;}

        public Datos(string Name, string Region, string Tipoclase)
        {
            this.name = Name;
            this.region = Region;
            this.tipoClase = Tipoclase;
        }
    }

}




