// Esta clase es el punto de entrada del programa.
// Su trabajo es mostrar el menu principal y ejecutar la opcion elegida.
internal class Program
{
    public static void Main(string[] args)
    {
        bool salir = false;

        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("=== HUNDIR LA FLOTA ===");
            Console.WriteLine("1. Jugar nueva partida");
            Console.WriteLine("2. Continuar partida");
            Console.WriteLine("3. Guardar partida actual");
            Console.WriteLine("4. Ver estadisticas");
            Console.WriteLine("5. Salir");
            Console.Write("Selecciona una opcion: ");

            string opcion = Console.ReadLine() ?? string.Empty;

            // El switch decide que accion hacer segun el numero elegido.
            switch (opcion)
            {
                case "1":
                    Juego juegoNuevo = new Juego();
                    juegoNuevo.Iniciar();
                    break;

                case "2":
                    Juego juegoGuardado = new Juego();
                    if (!juegoGuardado.ContinuarPartida())
                    {
                        Console.WriteLine("No hay ninguna partida guardada disponible.");
                        Pausar();
                    }
                    break;

                case "3":
                    Console.WriteLine("La opcion de guardar partida esta disponible durante el combate escribiendo G.");
                    Pausar();
                    break;

                case "4":
                    Juego.MostrarEstadisticas();
                    Pausar();
                    break;

                case "5":
                    salir = true;
                    break;

                default:
                    Console.WriteLine("Opcion no valida.");
                    Pausar();
                    break;
            }
        }
    }

    // Este metodo pausa la consola para que el jugador pueda leer el mensaje.
    static void Pausar()
    {
        Console.WriteLine("Pulsa una tecla para continuar...");
        Console.ReadKey();
    }
}
