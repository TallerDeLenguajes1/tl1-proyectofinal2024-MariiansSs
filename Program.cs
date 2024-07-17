﻿using PersonajesSpace;
using mensajesSpace;
using PersonajesJS;
using combateSpace;

string nombreArchivo = @"C:\TallerPractica\ProyectoFinal\tl1-proyectofinal2024-MariiansSs\Personajes.json";
Mensajes narrador = new Mensajes();
List<Personajes> Personajes = new List<Personajes>();
Personajes personajeElegido = new Personajes();
Personajes oponenteGenerado = new Personajes();
FabricaDePersonajes fabricarPersonaje = new FabricaDePersonajes();
PersonajesJson jsonPersonajes = new PersonajesJson(nombreArchivo);
Combate combates = new Combate();
int opcionPersonaje;
string caracterOpcionPersonaje;
int bandera = 0, finBatalla = 1;




// VERIFICO SI EXISTEN LOS PERSONAJES, SI NO, LOS CREO

if(jsonPersonajes.Existe(nombreArchivo))
{
    Personajes = jsonPersonajes.LeerPersonajes(nombreArchivo);
}else
{
    Personajes = fabricarPersonaje.CrearPersonajes();
    jsonPersonajes.GuardarPersonajes(Personajes, nombreArchivo);
}

// PRESENTACION DEL JUEGO
narrador.Bienvenida();
narrador.mensajeIntroduccion();



// ELEGIENDO PERSONAJE
narrador.preguntaSobrePersonaje();
fabricarPersonaje.mostrarPersonajeAElegir(Personajes); // Consultar como centrar 
caracterOpcionPersonaje = Console.ReadLine();
while (bandera != 1)
{
if(int.TryParse(caracterOpcionPersonaje, out opcionPersonaje))
{
    personajeElegido = fabricarPersonaje.buscarPersonajes(opcionPersonaje, Personajes);
    narrador.mostrarPersonajeElegido();
    Console.WriteLine($"{personajeElegido.Datos1.Name} {personajeElegido.Datos1.Region} {personajeElegido.Datos1.Tipoclase}");
    Personajes.Remove(personajeElegido);


    bandera = 1;
}else
{
    narrador.errorPersonaje();
    narrador.preguntaSobrePersonaje();
    fabricarPersonaje.mostrarPersonajeAElegir(Personajes);
    caracterOpcionPersonaje = Console.ReadLine();
}
}

//GENERANDO OPONENTE
oponenteGenerado = fabricarPersonaje.generarOponente(Personajes);
Personajes.Remove(oponenteGenerado);
Console.WriteLine(@$"¡TU OPONENTE HA APARECIDO EN EL CAMPO DE BATALLA!
Nombre:{oponenteGenerado.Datos1.Name} Region:{oponenteGenerado.Datos1.Region} Clase:{oponenteGenerado.Datos1.Tipoclase}");

//COMBATE
while(finBatalla == 1 && Personajes.Count > 0)
{
    finBatalla = combates.iniciarCombate(personajeElegido, oponenteGenerado);
    if(finBatalla == 1)
    {
        oponenteGenerado = fabricarPersonaje.generarOponente(Personajes);
        Personajes.Remove(oponenteGenerado);
    }
}

if(Personajes.Count == 0)
{
    Console.WriteLine("FELICIDADES, ERES EL GANADOR!");
}
















