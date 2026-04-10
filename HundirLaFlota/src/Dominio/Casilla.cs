// Esta clase representa una casilla del tablero.
// Guarda su posicion, si tiene barco y si ya fue disparada.
public class Casilla
{
    public int Fila {get;}
    public int Columna {get;}
    public Barco?Barco {get; private set;}
    public bool Disparada {get; private set;}

    // Crea una casilla con su posicion inicial.
    public Casilla(int fila, int columna, bool disparada)
    {
        this.Fila = fila;
        this.Columna = columna;
        this.Disparada = disparada;
    }

    // Indica si la casilla no tiene ningun barco.
    public bool EstaVacia()   
    {
        if (Barco == null)
        {
            return true;
        }else
        {
            return false;
        }
    }

    // Indica si el disparo en esta casilla fue un impacto.
    public bool EsImpacto()
    {
        if(Disparada == true && Barco != null)
        {
            return true;
        }else
        {
            return false;
        }
    }

    // Indica si el disparo en esta casilla cayo al agua.
    public bool EsAgua()
    {
        if(Disparada == true && Barco == null)
        {
            return true;
        }else
        {
            return false;
        }
    }

    // Coloca un barco dentro de esta casilla.
    public void ColocarBarco(Barco barco)
    {
        Barco = barco;    
    }

    // Marca la casilla como disparada.
    public void YaDisparado()
    {
        Disparada = true;
    }
}
