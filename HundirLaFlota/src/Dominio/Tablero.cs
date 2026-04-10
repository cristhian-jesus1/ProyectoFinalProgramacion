using System.Net.NetworkInformation;

// Esta clase representa el tablero completo del juego.
// Se encarga de colocar barcos, recibir disparos y reconstruirse al cargar partida.
public class Tablero
{
    bool todosHundidos;
    int barcosRestantes;

    List<Barco>barcos =  new List<Barco>();
    Casilla[,]casillas = new Casilla[10,10];

    // Indica si ya no queda ningun barco vivo en el tablero.
    public bool TodosHundidos
    {
        get
        {
            return todosHundidos;
        }
    }

    // Indica cuantos barcos siguen en pie.
    public int BarcosRestantes
    {
        get
        {
            return barcosRestantes;
        }
    }

    // Devuelve la lista de barcos colocados en este tablero.
    public List<Barco> ObtenerBarcos()
    {
        return barcos;
    }

    // Crea un tablero vacio de 10x10.
    public Tablero(bool todosHundidos, int barcosRestantes)
    {
        this.todosHundidos = todosHundidos;
        this.barcosRestantes = barcosRestantes;

        for (int fila = 0; fila < 10; fila++)
        {
            for (int columna = 0; columna < 10; columna++)
            {
                casillas[fila,columna] = new Casilla(fila,columna,false);
            }
        }
    }

    // Comprueba si todos los barcos del tablero estan hundidos.
    public bool FlotaHundida(Barco EstaHundido)
    {
        foreach(Barco barco in barcos)
        {
            if (!barco.EstaHundido())
            {
                return false;
            }
        }
        return true;
    }

    // Cuenta cuantos barcos siguen sin hundirse.
    public int ContarBarcosSobrantes(Barco EstaHundido)
    {
        int contador = 0;

        foreach (Barco barco in barcos)
        {
            if (!barco.EstaHundido())
            {
                contador++;
            }
        }
        return contador;
    }

    // Devuelve una casilla concreta del tablero.
    public Casilla ObtenerCasilla(int filas,int columnas)
    {
        return casillas[filas,columnas];
    }

    // Comprueba si un barco cabe en una posicion respetando las reglas.
    public bool PuedeColocar(Barco barco, int fila,int columna, bool esHorizontal)
    {
        for (int i = 0; i < barco.Tamanio; i++)
        {
            int filaActual = fila;
            int columnaActual = columna;

            if (esHorizontal)
            {
                columnaActual = columna + i;
            }
            else
            {
                filaActual = fila + i;
            }

            if (filaActual <0 || filaActual>=10 || columnaActual<0 || columnaActual>=10)
            {
                return false;
            }

            for (int desFila = -1; desFila <= 1; desFila++)
            {
                for (int desColumna = -1; desColumna <= 1; desColumna++)
                {
                    int nuevaFila = filaActual + desFila;
                    int nuevaColumna = columnaActual +desColumna;

                    if (nuevaFila < 0 || nuevaFila >=10 || nuevaColumna<0 || nuevaColumna >= 10)
                    {
                        continue;
                    }

                    if (casillas[nuevaFila,nuevaColumna].EstaVacia()!=true)
                    {
                        return false;
                    }
                }
                
            }
        }
        return true;
    }

    // Coloca un barco si la posicion es valida.
    public bool ColocarBarco (Barco barco, int fila,int columna,bool esHorizontal)
    {
        if (PuedeColocar(barco,fila,columna,esHorizontal)== false)
        {
            return false;
        }
        barco.Casillas.Clear();

        for (int i = 0; i < barco.Tamanio; i++)
        {
            int filaActual = fila;
            int columnaActual =columna;

            if (esHorizontal)
            {
                columnaActual = columna + i;
            }
            else
            {
                filaActual = fila + i;
            }
            casillas[filaActual,columnaActual].ColocarBarco(barco);
            barco.Casillas.Add(casillas[filaActual,columnaActual]);
        }
        if (barcos.Contains(barco)== false)
        {
            barcos.Add(barco);
        }
        return true;
    }

    // Procesa un disparo y devuelve si fue agua, impacto o hundido.
    public ResultadoDisparo Disparar(int fila,int columna)
    {
        Casilla casilla = casillas[fila,columna];

        if (casilla.Disparada)
        {
            return ResultadoDisparo.YaDisparado;
        }
        casilla.YaDisparado();

        if (casilla.EstaVacia())
        {
            return ResultadoDisparo.Agua;
        }

        Barco barcoImpactado = casilla.Barco!;
        barcoImpactado.RecibirImpacto();

        if (barcoImpactado.EstaHundido())
        {
            barcosRestantes--;

            if (barcosRestantes <= 0)
            {
                barcosRestantes = 0;
                todosHundidos = true;
            }
            return ResultadoDisparo.Hundido;
        }else{return ResultadoDisparo.Impacto;}
    }

    // Reconstruye un tablero normal a partir de un tablero guardado.
    public static Tablero CrearDesdeEstado(TableroGuardado estado)
    {
        int barcosRestantes = 0;
        
        foreach (BarcoGuardado barcoGuardado in estado.Barcos)
        {
          if (barcoGuardado.Impactos < barcoGuardado.Tamanio)
          {
            barcosRestantes++;
          } 
        }

        bool todosHundidos = barcosRestantes == 0;
        Tablero tablero = new Tablero(todosHundidos, barcosRestantes);

        foreach (BarcoGuardado barcoGuardado in estado.Barcos)
        {
            Barco barco = new Barco(barcoGuardado.Nombre, barcoGuardado.Tamanio, 0);
            foreach (CoordenadaGuardada coordenada in barcoGuardado.Casillas)
            {
                Casilla casilla = tablero.casillas[coordenada.Fila,coordenada.Columna];
                casilla.ColocarBarco(barco);
                barco.Casillas.Add(casilla);
            }
            tablero.barcos.Add(barco);
        }

        foreach (CoordenadaGuardada coordenada in estado.CasillasDisparadas)
        {
            tablero.casillas[coordenada.Fila,coordenada.Columna].YaDisparado();
        }

        // Recalcula los impactos reales mirando las casillas que ya fueron disparadas.
        foreach (Barco barco in tablero.barcos)
        {
            foreach (Casilla casilla in barco.Casillas)
            {
                if (casilla.Disparada)
                {
                    barco.RecibirImpacto();
                }
            }
        }

        return tablero;
    }
}
