namespace LoginFarmaclick.Models;
using System.Text.Json;
[Serializable]

public class Farmacia
{
    public string Nombre { get; set; }
    public string TituloPropiedad { get; set; }
    public string Direccion { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }
    public int IdFarmacia { get; set; }
    public string Contraseña { get; set; }

public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public static Farmacia? FromString(string? json)
    {
        if (json is null)
        {
            return null;
        }

        return JsonSerializer.Deserialize<Farmacia>(json);
    }
}
