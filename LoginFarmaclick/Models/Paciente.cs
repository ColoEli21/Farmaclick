namespace LoginFarmaclick.Models;
using System.Text.Json;
[Serializable]
public class Paciente
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string DNI { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }
    public int IdPaciente { get; set; }

    public string Contraseña { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public static Paciente? FromString(string? json)
    {
        if (json is null)
        {
            return null;
        }

        return JsonSerializer.Deserialize<Paciente>(json);
    }

}
