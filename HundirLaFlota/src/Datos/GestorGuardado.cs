using System.Text.Json;

public class GestorGuardado
{
    string carpetaDatos;
    string rutaGuardado;

    public GestorGuardado()
    {
        carpetaDatos = Path.Combine(Directory.GetCurrentDirectory(),"datos");
        rutaGuardado = Path.Combine(carpetaDatos,"partida.json");
    }

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
    public void EliminarGuardado()
    {
        if (File.Exists(rutaGuardado))
        {
            File.Delete(rutaGuardado);
        }
    }

    public void EliminarTodasLasPartidas()
    {
        EliminarGuardado();
    }

    public bool ExistePartidaGuardada()
    {
        return File.Exists(rutaGuardado);
    }
}