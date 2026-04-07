public class Casilla
{
    public int Fila {get;}
    public int Columna {get;}
    public Barco?Barco {get; private set;}
    public bool Disparada {get; private set;}

    public Casilla(int fila, int columna, bool disparada)
    {
        this.Fila = fila;
        this.Columna = columna;
        this.Disparada = disparada;
    }
    // En estas propiedades usamos el get para poder ver si el disparo ha sido impacto o es agua
    // Usamos propiedades en vez de metodo porque solo queremos consultar su valor sin cambiar el estado
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

    public void ColocarBarco(Barco barco)
    {
        Barco = barco;    
    }

    public void YaDisparado()
    {
        Disparada = true;
    }
}