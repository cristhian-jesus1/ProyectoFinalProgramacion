// Esta clase se encarga de mostrar informacion por consola y pedir datos al jugador.
public class Renderizador
{
    // Muestra el logo y el saludo inicial.
    public void MostrarBienvenida()
    {
        Console.WriteLine(ArteAscii.Logo);
        Console.WriteLine("Bienvenido al juego de Batalla Naval!");
        Console.WriteLine("--------------------------------------");
    }

    // Enseña un mensaje de error sencillo.
    public void MostrarError(string mensaje)
    {
        Console.WriteLine(mensaje);
    }

    // Dibuja el tablero del jugador mientras coloca sus barcos.
    public void MostrarTableroColocacion(Tablero tablero, Barco barco)
    {
        Console.WriteLine($"Coloca el barco: {barco.Nombre} (tamano {barco.Tamanio})");
        for (int filaTablero = 0; filaTablero < 10; filaTablero++)
        {
            for (int columnaTablero = 0; columnaTablero < 10; columnaTablero++)
            {
                Casilla casilla = tablero.ObtenerCasilla(filaTablero, columnaTablero);
                Console.Write(casilla.Barco != null ? "S " : ". ");
            }

            Console.WriteLine();
        }
    }

    // Pide la fila, la columna y la orientacion de un barco.
    public (int fila, int columna, bool horizontal) PedirPosicion(Barco barco)
    {
        int fila = LeerNumero($"Fila para {barco.Nombre}:");
        int columna = LeerNumero($"Columna para {barco.Nombre}:");
        bool horizontal = LeerOrientacion();

        return (fila, columna, horizontal);
    }

    // Muestra el tablero propio y el enemigo durante la batalla.
    public void MostrarTablerosBatalla(Tablero propio, Tablero enemigo)
    {
        Console.WriteLine("TU TABLERO:");

        for (int filaPropia = 0; filaPropia < 10; filaPropia++)
        {
            for (int columnaPropia = 0; columnaPropia < 10; columnaPropia++)
            {
                Casilla casilla = propio.ObtenerCasilla(filaPropia, columnaPropia);
                Console.Write(casilla.Barco != null ? "S " : ". ");
            }

            Console.WriteLine();
        }

        Console.WriteLine();
        Console.WriteLine("ENEMIGO:");

        for (int filaEnemiga = 0; filaEnemiga < 10; filaEnemiga++)
        {
            for (int columnaEnemiga = 0; columnaEnemiga < 10; columnaEnemiga++)
            {
                Casilla casilla = enemigo.ObtenerCasilla(filaEnemiga, columnaEnemiga);

                if (!casilla.Disparada)
                {
                    Console.Write(". ");
                }
                else if (casilla.Barco != null)
                {
                    Console.Write("X ");
                }
                else
                {
                    Console.Write("~ ");
                }
            }

            Console.WriteLine();
        }
    }

    // Pide una coordenada para disparar.
    public (int fila, int columna) PedirCoordenada()
    {
        int fila = LeerNumero("Introduce fila para disparar (0-9):");
        int columna = LeerNumero("Introduce columna para disparar (0-9):");

        return (fila, columna);
    }

    // Muestra el resultado del disparo del jugador.
    public void MostrarResultadoDisparo(ResultadoDisparo resultado, int fila, int columna)
    {
        Console.WriteLine($"Disparo en ({fila},{columna}): {resultado}");
    }

    // Muestra el resultado del disparo de la CPU.
    public void MostrarDisparoCpu(ResultadoDisparo resultado, int fila, int columna)
    {
        Console.WriteLine($"CPU disparo en ({fila},{columna}): {resultado}");
    }

    // Informa de quien ha ganado la partida.
    public void MostrarResultadoFinal(bool ganaJugador, Jugador jugador)
    {
        if (ganaJugador)
        {
            Console.WriteLine($"{jugador.Nombre} ha ganado la partida!");
        }
        else
        {
            Console.WriteLine("La CPU ha ganado la partida!");
        }
    }

    // Lee un numero valido entre 0 y 9.
    int LeerNumero(string mensaje)
    {
        while (true)
        {
            Console.WriteLine(mensaje);
            string entrada = Console.ReadLine() ?? string.Empty;

            if (int.TryParse(entrada, out int numero) && numero >= 0 && numero <= 9)
            {
                return numero;
            }

            Console.WriteLine("Introduce un numero valido entre 0 y 9.");
        }
    }

    // Lee si el barco va horizontal o vertical.
    bool LeerOrientacion()
    {
        while (true)
        {
            Console.WriteLine("Horizontal? (s/n):");
            string entrada = (Console.ReadLine() ?? string.Empty).Trim().ToLower();

            if (entrada == "s")
            {
                return true;
            }

            if (entrada == "n")
            {
                return false;
            }

            Console.WriteLine("Responde con s o n.");
        }
    }
}
