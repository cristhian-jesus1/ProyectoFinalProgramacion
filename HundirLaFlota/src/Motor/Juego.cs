// Este enum indica en que fase esta la partida.
enum Fase
{
    Colocacion,
    Batalla,
    Terminado
}

// Esta clase coordina toda la partida.
// Aqui se controlan turnos, guardado, carga y marcador.
public class Juego
{
    Jugador jugador;
    Cpu cpu;
    Renderizador render;
    Fase fase;
    GestorGuardado gestorGuardado;
    static Marcador marcador = new Marcador();

    // El constructor prepara una partida nueva con sus servicios basicos.
    public Juego()
    {
        ConfigJuego config = ConfigJuego.Cargar();

        jugador = new Jugador(config.NombreJugador);
        cpu = new Cpu();
        render = new Renderizador();
        gestorGuardado = new GestorGuardado();
        fase = Fase.Colocacion;
    }

    // Empieza una partida nueva desde cero.
    public void Iniciar()
    {
        render.MostrarBienvenida();
        FaseColocacion();

        if (FaseBatalla())
        {
            FaseFinal();
        }
    }

    // Intenta cargar una partida guardada y seguir desde ese punto.
    public bool ContinuarPartida()
    {
        EstadoPartida? estado = gestorGuardado.Cargar();

        if (estado == null)
        {
            return false;
        }

        CargarDesdeEstado(estado);
        render.MostrarBienvenida();

        if (fase == Fase.Colocacion)
        {
            FaseColocacion();
        }

        if (fase == Fase.Batalla && FaseBatalla())
        {
            FaseFinal();
        }

        return true;
    }

    // Muestra por consola las mejores puntuaciones guardadas.
    public static void MostrarEstadisticas()
    {
        List<EntradaMarcador> entradas = marcador.ObtenerEntradas();

        Console.WriteLine("=== ESTADISTICAS ===");

        if (entradas.Count == 0)
        {
            Console.WriteLine("Todavia no hay partidas ganadas registradas.");
            return;
        }

        int posicion = 1;
        foreach (EntradaMarcador entrada in entradas)
        {
            Console.WriteLine(
                $"{posicion}. {entrada.nombreJugador} | Puntuacion: {entrada.puntuacion:F2} | " +
                $"Precision: {entrada.precision:F2}% | Disparos: {entrada.disparos} | Fecha: {entrada.fecha:dd/MM/yyyy HH:mm}");
            posicion++;
        }
    }

    // En esta fase el jugador coloca sus barcos y la CPU coloca los suyos.
    public void FaseColocacion()
    {
        fase = Fase.Colocacion;

        List<Barco> flotaJugador = Flota.CreacionFlota();
        List<Barco> flotaCpu = Flota.CreacionFlota();

        foreach (Barco barco in flotaJugador)
        {
            bool colocado = false;

            while (!colocado)
            {
                render.MostrarTableroColocacion(jugador.Tablero, barco);
                var (fila, columna, horizontal) = render.PedirPosicion(barco);

                colocado = jugador.Tablero.ColocarBarco(barco, fila, columna, horizontal);

                if (!colocado)
                {
                    render.MostrarError("No se puede colocar ahi.");
                }
            }
        }

        cpu.ColocarFlotaAleatoria(flotaCpu);
        fase = Fase.Batalla;
    }

    // En esta fase ambos jugadores se disparan hasta que alguien gana o se guarda la partida.
    public bool FaseBatalla()
    {
        fase = Fase.Batalla;

        while (!jugador.Tablero.TodosHundidos && !cpu.Tablero.TodosHundidos)
        {
            render.MostrarTablerosBatalla(jugador.Tablero, cpu.Tablero);

            Console.WriteLine("Escribe G para guardar la partida y volver al menu.");
            Console.Write("Introduce fila para disparar (0-9): ");
            string entradaFila = Console.ReadLine() ?? string.Empty;

            if (entradaFila.Equals("G", StringComparison.OrdinalIgnoreCase))
            {
                GuardarPartida();
                Console.WriteLine("Partida guardada correctamente.");
                Console.WriteLine("Pulsa una tecla para volver al menu...");
                Console.ReadKey();
                return false;
            }

            if (!int.TryParse(entradaFila, out int fila) || fila < 0 || fila > 9)
            {
                render.MostrarError("Fila no valida.");
                continue;
            }

            Console.Write("Introduce columna para disparar (0-9): ");
            string entradaColumna = Console.ReadLine() ?? string.Empty;

            if (!int.TryParse(entradaColumna, out int columna) || columna < 0 || columna > 9)
            {
                render.MostrarError("Columna no valida.");
                continue;
            }

            ResultadoDisparo resultado = cpu.Tablero.Disparar(fila, columna);
            jugador.RegistrarDisparo(resultado);
            render.MostrarResultadoDisparo(resultado, fila, columna);

            if (cpu.Tablero.TodosHundidos)
            {
                break;
            }

            CoordenadaGuardada objetivo = cpu.ElegirObjetivo();
            ResultadoDisparo resultadoCpu = jugador.Tablero.Disparar(objetivo.Fila, objetivo.Columna);
            cpu.RegistrarDisparo(resultadoCpu);
            render.MostrarDisparoCpu(resultadoCpu, objetivo.Fila, objetivo.Columna);
        }

        return true;
    }

    // Termina la partida, guarda la puntuacion y borra el guardado temporal.
    public void FaseFinal()
    {
        fase = Fase.Terminado;
        bool ganaJugador = cpu.Tablero.TodosHundidos;

        if (ganaJugador)
        {
            marcador.AgregarEntrada(new EntradaMarcador(
                jugador.Nombre,
                jugador.Disparos,
                jugador.Aciertos,
                jugador.Fallos,
                jugador.Precision,
                CalcularPuntuacion(),
                DateTime.Now));
        }

        gestorGuardado.EliminarGuardado();
        render.MostrarResultadoFinal(ganaJugador, jugador);
        Console.WriteLine("Pulsa una tecla para volver al menu...");
        Console.ReadKey();
    }

    // Guarda la partida actual en archivo.
    void GuardarPartida()
    {
        gestorGuardado.Guardar(CrearEstadoPartida());
    }

    // Convierte la partida actual en datos simples para JSON.
    EstadoPartida CrearEstadoPartida()
    {
        return new EstadoPartida
        {
            Jugador = new JugadorGuardado
            {
                Nombre = jugador.Nombre,
                Disparos = jugador.Disparos,
                Acierts = jugador.Aciertos,
                Fallos = jugador.Fallos
            },
            Cpu = new JugadorGuardado
            {
                Nombre = cpu.Nombre,
                Disparos = cpu.Disparos,
                Acierts = cpu.Aciertos,
                Fallos = cpu.Fallos
            },
            TableroJugador = CrearTableroGuardado(jugador.Tablero),
            TableroCpu = CrearTableroGuardado(cpu.Tablero),
            TurnoJugador = true,
            FaseActual = fase.ToString(),
            Configuracion = new ConfigJuego
            {
                NombreJugador = jugador.Nombre,
                MostrarColores = true
            },
            ObjetivosCpu = cpu.ObtenerObjetivosPendientes()
        };
    }

    // Convierte un tablero normal en un tablero pensado para guardarse.
    TableroGuardado CrearTableroGuardado(Tablero tablero)
    {
        TableroGuardado tableroGuardado = new TableroGuardado();

        foreach (Barco barco in tablero.ObtenerBarcos())
        {
            BarcoGuardado barcoGuardado = new BarcoGuardado
            {
                Nombre = barco.Nombre,
                Tamanio = barco.Tamanio,
                Impactos = barco.Impactos
            };

            foreach (Casilla casilla in barco.Casillas)
            {
                barcoGuardado.Casillas.Add(new CoordenadaGuardada(casilla.Fila, casilla.Columna));
            }

            tableroGuardado.Barcos.Add(barcoGuardado);
        }

        for (int fila = 0; fila < 10; fila++)
        {
            for (int columna = 0; columna < 10; columna++)
            {
                Casilla casilla = tablero.ObtenerCasilla(fila, columna);
                if (casilla.Disparada)
                {
                    tableroGuardado.CasillasDisparadas.Add(new CoordenadaGuardada(fila, columna));
                }
            }
        }

        return tableroGuardado;
    }

    // Reconstruye la partida completa a partir del archivo guardado.
    void CargarDesdeEstado(EstadoPartida estado)
    {
        jugador = new Jugador(estado.Jugador.Nombre)
        {
            Disparos = estado.Jugador.Disparos,
            Aciertos = estado.Jugador.Acierts,
            Fallos = estado.Jugador.Fallos,
            Tablero = Tablero.CrearDesdeEstado(estado.TableroJugador)
        };

        cpu = new Cpu
        {
            Disparos = estado.Cpu.Disparos,
            Aciertos = estado.Cpu.Acierts,
            Fallos = estado.Cpu.Fallos,
            Tablero = Tablero.CrearDesdeEstado(estado.TableroCpu)
        };

        cpu.EstablecerObjetivos(estado.ObjetivosCpu);

        if (!Enum.TryParse(estado.FaseActual, out fase))
        {
            fase = Fase.Batalla;
        }
    }

    // Calcula una puntuacion sencilla para el ranking.
    double CalcularPuntuacion()
    {
        return jugador.Aciertos * 100 - jugador.Fallos * 10;
    }
}
