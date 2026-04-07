
enum Fase{
    Colocacion,
    Batalla,
    Terminado
    }
public class Juego
{
    Jugador jugador;
    Cpu cpu;
    Renderizador render;
    Fase fase;

    public Juego()
    {
        jugador = new Jugador("Jugador");
        cpu = new Cpu();
        render = new Renderizador();

        fase = Fase.Colocacion;
    }

    public void Iniciar()
    {
        render.MostrarBienvenida();

        FaseColocacion();
        FaseBatalla();
        FaseFinal();
    }
    

    public void FaseColocacion()
    {
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
                    render.MostrarError("No se puede colocar ahí.");
            }
        }

        cpu.ColocarFlotaAleatoria(flotaCpu);
    }
    public void FaseBatalla()
    {
        while (!jugador.Tablero.TodosHundidos && !cpu.Tablero.TodosHundidos)
        {
            // Turno jugador
            render.MostrarTablerosBatalla(jugador.Tablero, cpu.Tablero);

            var (fila, columna) = render.PedirCoordenada();

            ResultadoDisparo resultado = cpu.Tablero.Disparar(fila, columna);
            jugador.RegistrarDisparo(resultado);

            render.MostrarResultadoDisparo(resultado, fila, columna);

            if (cpu.Tablero.TodosHundidos) break;

            // Turno CPU
            var objetivo = cpu.ElegirObjetivo();

            ResultadoDisparo resultadoCpu = jugador.Tablero.Disparar(objetivo.Fila, objetivo.Columna);
            cpu.RegistrarDisparo(resultadoCpu);

            render.MostrarDisparoCpu(resultadoCpu, objetivo.Fila, objetivo.Columna);
        }
    }

    public void FaseFinal()
    {
        bool ganaJugador = cpu.Tablero.TodosHundidos;
        render.MostrarResultadoFinal(ganaJugador, jugador);
    }
}
