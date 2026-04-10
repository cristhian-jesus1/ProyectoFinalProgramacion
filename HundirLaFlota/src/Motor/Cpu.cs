// Esta clase representa a la CPU.
// Hereda de Jugador y añade logica automatica.
class Cpu : Jugador{
    private List<CoordenadaGuardada>objetivos;
    private Random random;

    // Aqui se preparan los disparos posibles y se mezclan al azar.
    public Cpu() : base("CPU")
    {
        random = new Random();
        objetivos = new List<CoordenadaGuardada>();

        for (int fila = 0; fila < 10; fila++)
        {
            for (int columna = 0; columna < 10; columna++)
            {
                objetivos.Add(new CoordenadaGuardada(fila,columna));
            }
        }

        Barajar();
    }

    // Mezcla la lista para que la CPU no repita siempre el mismo orden.
    private void Barajar()
    {
        for (int i = objetivos.Count-1; i > 0; i--)
        {
            int j = random.Next(i+1);

            var temp = objetivos[i];
            objetivos[i] = objetivos[j];
            objetivos[j] = temp;
        }
    }

    // Devuelve la siguiente casilla objetivo de la CPU.
    public CoordenadaGuardada ElegirObjetivo()
    {
        CoordenadaGuardada objetivo = objetivos[0];
        objetivos.RemoveAt(0);
        return objetivo;
    }

    // Coloca los barcos de la CPU en posiciones validas al azar.
    public void ColocarFlotaAleatoria(List<Barco> barcos)
    {
        foreach (Barco barco in barcos)
        {
            bool colocado = false;
            while (!colocado)
            {
                int fila = random.Next(10);
                int columna = random.Next(10);
                bool horizontal = random.Next(2) == 0;

                colocado = Tablero.ColocarBarco(barco,fila,columna, horizontal);
            }
        }
    }

    // Sirve para guardar los objetivos que aun le quedan a la CPU.
    public List<CoordenadaGuardada> ObtenerObjetivosPendientes()
    {
        return objetivos
            .Select(objetivo => new CoordenadaGuardada(objetivo.Fila, objetivo.Columna))
            .ToList();
    }

    // Sirve para restaurar esos objetivos al cargar una partida.
    public void EstablecerObjetivos(List<CoordenadaGuardada> objetivosGuardados)
    {
        objetivos = objetivosGuardados
            .Select(objetivo => new CoordenadaGuardada(objetivo.Fila, objetivo.Columna))
            .ToList();
    }
}
