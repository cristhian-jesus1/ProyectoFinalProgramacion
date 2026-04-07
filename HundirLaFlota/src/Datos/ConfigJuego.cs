
using System.Text.Json;

public class ConfigJuego
{
    public bool MostrarColores{get;set;}
    public string NombreJugador{get;set;}

    public ConfigJuego()
    {
        MostrarColores = true;
        NombreJugador = "Jugador";
    }
    public static ConfigJuego Cargar()
    {
        string carpetaDatos = Path.Combine(Directory.GetCurrentDirectory(), "datos");
        string rutaConfig = Path.Combine(carpetaDatos,"config.json");

        if (File.Exists(rutaConfig)==false)
        {
            ConfigJuego configPorDefecto = new ConfigJuego();
            configPorDefecto.Guardar();
            return configPorDefecto;
        }
        try
        {
            string json = File.ReadAllText(rutaConfig);
            ConfigJuego? config = JsonSerializer.Deserialize<ConfigJuego>(json);

            if (config == null)
            {
                return new ConfigJuego();
            }
            return config;
        }
        catch 
        {
            
            return new ConfigJuego();
        }
    }

    public void Guardar()
    {
        string carpetaDatos = Path.Combine(Directory.GetCurrentDirectory(),"datos");
        string rutaConfig = Path.Combine(carpetaDatos,"config.json");

        if (Directory.Exists(carpetaDatos)==false)
        {
            Directory.CreateDirectory(carpetaDatos);           
        }

        JsonSerializerOptions opciones = new JsonSerializerOptions();
        opciones.WriteIndented = true;

        string json = JsonSerializer.Serialize(this,opciones);
        File.WriteAllText(rutaConfig,json);
    }

}