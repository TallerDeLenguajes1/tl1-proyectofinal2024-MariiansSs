using PersonajesSpace;
using mensajesSpace;

Mensajes narrador = new Mensajes();
List<Personajes> Personajes = new List<Personajes>();
FabricaDePersonajes fabricarPersonaje = new FabricaDePersonajes();
PersonajesJson guardar = new PersonajesJson();




narrador.Bienvenida();
narrador.mensajeIntroduccion();



/*
int opcionPersonaje;
string caracterOpcionPersonaje;
int bandera = 0;
fabricarPersonaje.mostrarPersonajeParaElegir();

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
    fabricarPersonaje.mostrarPersonajeParaElegir();
    narrador.preguntaSobrePersonaje();
    caracterOpcionPersonaje = Console.ReadLine();
}
}
*/













