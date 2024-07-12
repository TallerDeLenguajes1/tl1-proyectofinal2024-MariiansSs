using PersonajesSpace;
using mensajesSpace;
using PersonajesJS;

string nombreArchivo = @"C:\TallerPractica\ProyectoFinal\tl1-proyectofinal2024-MariiansSs\Personajes.json";
Mensajes narrador = new Mensajes();
List<Personajes> Personajes = new List<Personajes>();
FabricaDePersonajes fabricarPersonaje = new FabricaDePersonajes();
PersonajesJson jsonPersonajes = new PersonajesJson(nombreArchivo);
int opcionPersonaje;
string caracterOpcionPersonaje;
int bandera = 0;


Personajes = fabricarPersonaje.CrearPersonajes();
if(jsonPersonajes.Existe(nombreArchivo))
{
    Personajes = jsonPersonajes.LeerPersonajes(nombreArchivo);
}else
{

}
jsonPersonajes.GuardarPersonajes(Personajes, nombreArchivo);


narrador.Bienvenida();
narrador.mensajeIntroduccion();
narrador.preguntaSobrePersonaje();

caracterOpcionPersonaje = Console.ReadLine();
while (bandera != 1)
{
if(int.TryParse(caracterOpcionPersonaje, out opcionPersonaje))
{
    bandera = 1;
    fabricarPersonaje.buscarPersonajes(opcionPersonaje);
}else
{
    Console.WriteLine("INGRESE UN NUMERO VALIDO");
    fabricarPersonaje.mostrarPersonajeAElegir();
    narrador.preguntaSobrePersonaje();
    caracterOpcionPersonaje = Console.ReadLine();
}
}














