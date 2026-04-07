public class Barco
{
    public string Nombre {get;}
    public int Tamanio {get;}
    public int Impactos {get; private set;} // Con el private set nos permite cambiarlo pero solo desde dentro
    public List<Casilla> Casillas {get;}  

    public Barco(string nombre, int tamanio, int impactos)
    {
        this.Nombre = nombre;
        this.Tamanio = tamanio;
        this.Impactos = impactos;
        this.Casillas = new List<Casilla>();
    }

    public void RecibirImpacto()
    {
        Impactos++;
    }

    public bool EstaHundido()
    {
        return Impactos >= Tamanio;
    }
    
}