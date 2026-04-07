public class Jugador
{
    public string Nombre {get;set;}
    public Tablero Tablero {get;set;}
    public int Disparos {get;set;}
    public int Aciertos{get;set;}
    public int Fallos{get;set;}

    public double Precision
    {
        get
        {
            if (Disparos == 0)return 0;
            return(double)Aciertos/Disparos*100;
        }
    }

    public Jugador(string nombre)
    {
        Nombre = nombre;
        Tablero = new Tablero(false,5);
    }

    public void RegistrarDisparo(ResultadoDisparo resultado)
    {
        Disparos++;

        if (resultado == ResultadoDisparo.Impacto || resultado == ResultadoDisparo.Hundido)
        {
            Aciertos++;
        }else if(resultado == ResultadoDisparo.Agua){Fallos++;}
    }

}