using System.Text.Json;

class Marcador
{
    List<EntradaMarcador>entradas;
    string carpetaDatos;
    string rutaMarcador;

    public Marcador()
    {
        entradas = new List<EntradaMarcador>();
        carpetaDatos = Path.Combine(Directory.GetCurrentDirectory(),"datos");
        rutaMarcador = Path.Combine(carpetaDatos,"marcador.json");
        Cargar();
    }
    public List<EntradaMarcador> ObtenerEntradas()
    {
        return entradas;
    }

    public void AgregarEntrada(EntradaMarcador entrada)
    {
        entradas.Add(entrada);
        OrdenarEntradas();
        LimitarTop10();
        Guardar();
    }

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

    void OrdenarEntradas()
    {
        
        entradas = entradas
            .OrderByDescending(entrada => entrada.puntuacion)
            .ThenBy(entrada => entrada.fecha)
            .ToList();
    }

    void LimitarTop10()
    {
        
        if (entradas.Count > 10)
        {
            entradas = entradas.Take(10).ToList();
        }
    }
}