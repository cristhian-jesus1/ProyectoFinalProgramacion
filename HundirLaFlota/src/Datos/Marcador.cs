using System.Text.Json;

// Esta clase guarda el ranking de mejores partidas ganadas.
class Marcador
{
    List<EntradaMarcador>entradas;
    string carpetaDatos;
    string rutaMarcador;

    // Prepara la ruta del marcador y carga los datos si existen.
    public Marcador()
    {
        entradas = new List<EntradaMarcador>();
        carpetaDatos = Path.Combine(Directory.GetCurrentDirectory(),"datos");
        rutaMarcador = Path.Combine(carpetaDatos,"marcador.json");
        Cargar();
    }

    // Devuelve todas las entradas del ranking.
    public List<EntradaMarcador> ObtenerEntradas()
    {
        return entradas;
    }

    // Añade una nueva entrada, la ordena y la guarda en disco.
    public void AgregarEntrada(EntradaMarcador entrada)
    {
        entradas.Add(entrada);
        OrdenarEntradas();
        LimitarTop10();
        Guardar();
    }

    // Guarda el ranking en un archivo JSON.
    public void Guardar()
    {
        if (Directory.Exists(carpetaDatos)== false)
        {
            Directory.CreateDirectory(carpetaDatos);
        }
        JsonSerializerOptions opciones = new JsonSerializerOptions();
        opciones.WriteIndented = true;

        string json = JsonSerializer.Serialize(entradas, opciones);
        File.WriteAllText(rutaMarcador, json);
    }

    // Carga el ranking desde el archivo si ya existe.
    public void Cargar()
    {
        
        entradas.Clear();

       
        if (File.Exists(rutaMarcador) == false)
        {
            return;
        }

        try
        {
            string json = File.ReadAllText(rutaMarcador);
            List<EntradaMarcador>? entradasCargadas = JsonSerializer.Deserialize<List<EntradaMarcador>>(json);

            if (entradasCargadas != null)
            {
                entradas = entradasCargadas;
                OrdenarEntradas();
                LimitarTop10();
            }
        }
        catch
        {
        
            entradas = new List<EntradaMarcador>();
        }
    }

    // Ordena las partidas de mejor a peor puntuacion.
    void OrdenarEntradas()
    {
        
        entradas = entradas
            .OrderByDescending(entrada => entrada.puntuacion)
            .ThenBy(entrada => entrada.fecha)
            .ToList();
    }

    // Deja solo las 10 mejores posiciones del ranking.
    void LimitarTop10()
    {
        
        if (entradas.Count > 10)
        {
            entradas = entradas.Take(10).ToList();
        }
    }
}
