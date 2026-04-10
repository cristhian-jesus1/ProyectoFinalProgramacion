// Esta clase representa un barco del juego.
// Guarda su nombre, tamano, impactos y casillas ocupadas.
public class Barco
{
    public string Nombre {get;}
    public int Tamanio {get;}
    // Los impactos solo se cambian desde el propio barco para evitar errores.
    public int Impactos {get; private set;}
    public List<Casilla> Casillas {get;}  

    // Crea un barco nuevo.
    public Barco(string nombre, int tamanio, int impactos)
    {
        this.Nombre = nombre;
        this.Tamanio = tamanio;
        this.Impactos = impactos;
        this.Casillas = new List<Casilla>();
    }

    // Suma un impacto cuando el barco recibe un disparo.
    public void RecibirImpacto()
    {
        Impactos++;
    }

    // Devuelve true si el barco ya esta hundido.
    public bool EstaHundido()
    {
        return Impactos >= Tamanio;
    }
    
}
