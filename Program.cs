﻿using PersonajesSpace;
using mensajesSpace;
using PersonajesJS;

string nombreArchivo = @"C:\TallerPractica\ProyectoFinal\tl1-proyectofinal2024-MariiansSs\Personajes.json";
Mensajes narrador = new Mensajes();
List<Personajes> Personajes = new List<Personajes>();
Personajes personajeElegido = new Personajes();
FabricaDePersonajes fabricarPersonaje = new FabricaDePersonajes();
PersonajesJson jsonPersonajes = new PersonajesJson(nombreArchivo);
int opcionPersonaje;
string caracterOpcionPersonaje;
int bandera = 0;




if(jsonPersonajes.Existe(nombreArchivo))
{
    Personajes = jsonPersonajes.LeerPersonajes(nombreArchivo);
}else
{
    Personajes = fabricarPersonaje.CrearPersonajes();
    jsonPersonajes.GuardarPersonajes(Personajes, nombreArchivo);
}

narrador.Bienvenida();
narrador.mensajeIntroduccion();
narrador.preguntaSobrePersonaje();
fabricarPersonaje.mostrarPersonajeAElegir(Personajes);
caracterOpcionPersonaje = Console.ReadLine();

while (bandera != 1)
{
if(int.TryParse(caracterOpcionPersonaje, out opcionPersonaje))
{
    personajeElegido = fabricarPersonaje.buscarPersonajes(opcionPersonaje, Personajes);
    Console.WriteLine("---EL PERSONAJE ELEGIDO ES:---");
    Console.WriteLine($"{personajeElegido.Datos1.Name} {personajeElegido.Datos1.Region} {personajeElegido.Datos1.Tipoclase}");

    bandera = 1;
}else
{
    Console.WriteLine("Lo siento!, no existe ese personaje actualmente");
    narrador.preguntaSobrePersonaje();
    fabricarPersonaje.mostrarPersonajeAElegir(Personajes);
    caracterOpcionPersonaje = Console.ReadLine();
}
}














