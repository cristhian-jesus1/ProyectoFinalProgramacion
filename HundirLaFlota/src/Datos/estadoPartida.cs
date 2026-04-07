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

public class JugadorGuardado
{
    public string Nombre {get;set;}
    public int Disparos {get;set;}
    public int Acierts {get;set;}
    public int Fallos{get;set;}
    
    public JugadorGuardado()
    {
        Nombre = "";
    }
}

public class TableroGuardado
{
    public List<BarcoGuardado> Barcos {get;set;}
    public List<CoordenadaGuardada> CasillasDisparadas {get;set;}

    public TableroGuardado()
    {
        Barcos = new List<BarcoGuardado>();
        CasillasDisparadas = new List<CoordenadaGuardada>();
    }
}

public class BarcoGuardado
{
    public string Nombre {get;set;}
    public int Tamanio {get;set;}
    public int Impactos {get;set;}
    public List<CoordenadaGuardada>Casillas {get;set;}

    public BarcoGuardado()
    {
        Nombre = "";
        Casillas = new List<CoordenadaGuardada>();
    }
}

public class CoordenadaGuardada
{
    public int Fila {get;set;}
    public int Columna {get;set;}

    public CoordenadaGuardada(int fila,int columna)
    {
        Fila = fila;
        Columna = columna;
    }
}