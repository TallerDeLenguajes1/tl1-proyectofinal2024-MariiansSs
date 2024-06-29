using Personajes;
using mensajes;

Mensaje narrador = new Mensaje();
FabricaDePersonajes fabricarPersonaje = new FabricaDePersonajes();

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













