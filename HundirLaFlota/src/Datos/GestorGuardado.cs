using System.Text.Json;

// Esta clase se ocupa de guardar, cargar y borrar la partida actual.
public class GestorGuardado
{
    string carpetaDatos;
    string rutaGuardado;

    // Prepara la carpeta y la ruta del archivo de guardado.
    public GestorGuardado()
    {
        carpetaDatos = Path.Combine(Directory.GetCurrentDirectory(),"datos");
        rutaGuardado = Path.Combine(carpetaDatos,"partida.json");
    }

    // Guarda en JSON el estado actual de la partida.
    public void Guardar(EstadoPartida estado)
    {
        if (Directory.Exists(carpetaDatos) == false)
        {
            Directory.CreateDirectory(carpetaDatos);
        }

        JsonSerializerOptions opciones = new JsonSerializerOptions();
        opciones.WriteIndented = true;

        string json = JsonSerializer.Serialize(estado,opciones);
        File.WriteAllText(rutaGuardado,json);
    }

    // Intenta cargar la partida guardada desde disco.
    public EstadoPartida? Cargar()
    {
        if (ExistePartidaGuardada()== false)
        {
            return null;
        }
        try
        {
            string json = File.ReadAllText(rutaGuardado);
            EstadoPartida? estado = JsonSerializer.Deserialize<EstadoPartida>(json);
            return estado;
        }
        catch
        {
            return null;
        }
    }

    // Borra el archivo de guardado actual.
    public void EliminarGuardado()
    {
        if (File.Exists(rutaGuardado))
        {
            File.Delete(rutaGuardado);
        }
    }

    // De momento reutiliza el mismo borrado porque solo hay una partida guardada.
    public void EliminarTodasLasPartidas()
    {
        EliminarGuardado();
    }

    // Comprueba si existe una partida guardada.
    public bool ExistePartidaGuardada()
    {
        return File.Exists(rutaGuardado);
    }
}
