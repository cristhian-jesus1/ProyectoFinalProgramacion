class Cpu : Jugador{
    private List<CoordenadaGuardada>objetivos;
    private Random random;

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

    public CoordenadaGuardada ElegirObjetivo()
    {
        CoordenadaGuardada objetivo = objetivos[0];
        objetivos.RemoveAt(0);
        return objetivo;
    }

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
}