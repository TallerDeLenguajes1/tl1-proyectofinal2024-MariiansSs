using Personajes;
Console.WriteLine("Hello, World!");

FabricaDePersonajes Personajes = new FabricaDePersonajes();

Personaje Elegido = Personajes.buscarPersonajes(2);

Personajes.mostrarPersonaje(Elegido);






