// Esta clase crea la lista de barcos que se usa en cada partida.
public class Flota
{
    // Devuelve la flota completa del juego.
    public static List<Barco> CreacionFlota()
    {
        List<Barco>barcos = new List<Barco>();

        barcos.Add(new Barco("Portaaviones 🛳️", 5,0));
        barcos.Add(new Barco("Acorazado 🚢", 4,0));
        barcos.Add(new Barco("Destructor ⚓", 3,0));
        barcos.Add(new Barco("Submarino 🔱", 3,0));
        barcos.Add(new Barco("Patrullera ⛴️", 2,0));
       
       return barcos;
    }
}
