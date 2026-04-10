// Esta clase agrupa toda la informacion necesaria para guardar una partida completa.
public class EstadoPartida
{
    public JugadorGuardado Jugador {get;set;}
    public JugadorGuardado Cpu{get;set;}
    public TableroGuardado TableroJugador{get;set;}
    public TableroGuardado TableroCpu{get;set;}
    public bool TurnoJugador {get;set;}
    public string FaseActual{get;set;}
    public ConfigJuego Configuracion {get;set;}
    public List<CoordenadaGuardada>ObjetivosCpu{get;set;}

    // Crea un estado vacio para rellenarlo luego.
    public EstadoPartida()
    {
        Jugador = new JugadorGuardado();
        Cpu = new JugadorGuardado();
        TableroJugador = new TableroGuardado();
        TableroCpu = new TableroGuardado();
        TurnoJugador = true;
        FaseActual = "";
        Configuracion = new ConfigJuego();
        ObjetivosCpu = new List<CoordenadaGuardada>();
    }
}

// Esta clase guarda solo los datos simples de un jugador.
public class JugadorGuardado
{
    public string Nombre {get;set;}
    public int Disparos {get;set;}
    public int Acierts {get;set;}
    public int Fallos{get;set;}
    
    // Crea un jugador guardado vacio.
    public JugadorGuardado()
    {
        Nombre = "";
    }
}

// Esta clase guarda la informacion necesaria para reconstruir un tablero.
public class TableroGuardado
{
    public List<BarcoGuardado> Barcos {get;set;}
    public List<CoordenadaGuardada> CasillasDisparadas {get;set;}

    // Crea listas vacias para barcos y disparos.
    public TableroGuardado()
    {
        Barcos = new List<BarcoGuardado>();
        CasillasDisparadas = new List<CoordenadaGuardada>();
    }
}

// Esta clase guarda un barco en formato simple para JSON.
public class BarcoGuardado
{
    public string Nombre {get;set;}
    public int Tamanio {get;set;}
    public int Impactos {get;set;}
    public List<CoordenadaGuardada>Casillas {get;set;}

    // Crea un barco guardado vacio.
    public BarcoGuardado()
    {
        Nombre = "";
        Casillas = new List<CoordenadaGuardada>();
    }
}

// Esta clase guarda una coordenada del tablero.
public class CoordenadaGuardada
{
    public int Fila {get;set;}
    public int Columna {get;set;}

    // Crea una coordenada con fila y columna.
    public CoordenadaGuardada(int fila,int columna)
    {
        Fila = fila;
        Columna = columna;
    }
}
