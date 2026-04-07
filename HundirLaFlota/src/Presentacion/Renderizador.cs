public class Renderizador
{
    public void MostrarBienvenida()
    {
        Console.WriteLine("¡Bienvenido al juego de Batalla Naval!");
        Console.WriteLine("--------------------------------------");
    }
    public void MostrarError(string mensaje)
    {
        System.Console.WriteLine(mensaje);
    }

    public void MostrarTableroColocacion(Tablero tablero, Barco barco)
    {
        Console.WriteLine($"Coloca el barco: {barco.Nombre} (tamaño {barco.Tamanio})");
        for (int f = 0; f < 10; f++)
        {
            for (int c = 0; c < 10; c++)
            {
                var casilla = tablero.ObtenerCasilla(f, c);
                if (casilla.Barco != null)
                    Console.Write("S ");
                else
                    Console.Write(". ");
            }
            Console.WriteLine();
        }
    }
    public (int fila,int columna,bool horizontal)PedirPosicion(Barco barco){
        return (0,0,true);
    }
    public void MostrarTablerosBatalla(Tablero propio, Tablero enemigo)
    {
        Console.WriteLine("TU TABLERO:");

        for (int f = 0; f < 10; f++)
        {
            for (int c = 0; c < 10; c++)
            {
                var casilla = propio.ObtenerCasilla(f, c);

                if (casilla.Barco != null)
                    Console.Write("S ");
                else
                    Console.Write(". ");
            }
            Console.WriteLine();
        }

        Console.WriteLine("\nENEMIGO:");

        for (int f = 0; f < 10; f++)
        {
            for (int c = 0; c < 10; c++)
            {
                var casilla = enemigo.ObtenerCasilla(f, c);

                if (casilla.Disparada)
                {
                    if (casilla.Barco != null)
                        Console.Write("X ");
                    else
                        Console.Write("~ ");
                }
                else
                {
                    Console.Write(". ");
                }
            }
            Console.WriteLine();
        }
    }
    public (int fila, int columna) PedirCoordenada()
    {
        Console.WriteLine("Introduce fila para disparar (0-9):");
        int fila = int.Parse(Console.ReadLine());

        Console.WriteLine("Introduce columna para disparar (0-9):");
        int columna = int.Parse(Console.ReadLine());

        return (fila, columna);
    }
    public void MostrarResultadoDisparo(ResultadoDisparo resultado, int fila, int columna)
    {
        Console.WriteLine($"Disparo en ({fila},{columna}): {resultado}");
    }
    public void MostrarDisparoCpu(ResultadoDisparo resultado, int fila, int columna)
    {
        Console.WriteLine($"CPU disparó en ({fila},{columna}): {resultado}");
    }
    public void MostrarResultadoFinal(bool ganaJugador, Jugador jugador)
    {
        if (ganaJugador)
            Console.WriteLine($"{jugador.Nombre} ha ganado la partida!");
        else
            Console.WriteLine("La CPU ha ganado la partida!");
    }
}

