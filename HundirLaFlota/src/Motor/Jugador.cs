// Esta clase guarda los datos basicos de un jugador:
// su nombre, su tablero y sus estadisticas.
public class Jugador
{
    public string Nombre {get;set;}
    public Tablero Tablero {get;set;}
    public int Disparos {get;set;}
    public int Aciertos{get;set;}
    public int Fallos{get;set;}

    // Esta propiedad calcula el porcentaje de acierto del jugador.
    public double Precision
    {
        get
        {
            if (Disparos == 0)return 0;
            return(double)Aciertos/Disparos*100;
        }
    }

    // El constructor crea un jugador nuevo con su tablero vacio.
    public Jugador(string nombre)
    {
        Nombre = nombre;
        Tablero = new Tablero(false,5);
    }

    // Este metodo actualiza las estadisticas despues de cada disparo.
    public void RegistrarDisparo(ResultadoDisparo resultado)
    {
        Disparos++;

        if (resultado == ResultadoDisparo.Impacto || resultado == ResultadoDisparo.Hundido)
        {
            Aciertos++;
        }else if(resultado == ResultadoDisparo.Agua){Fallos++;}
    }

}
