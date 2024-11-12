namespace LoginFarmaclick.Models;
using System.Text.Json;
[Serializable]
public class Doctor
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string DNI { get; set; }
    public string Matricula { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }
    public int IdDoctor { get; set; }
    public string Contraseña { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public static Doctor? FromString(string? json)
    {
        if (json is null)
        {
            return null;
        }

        return JsonSerializer.Deserialize<Doctor>(json);
    }
}
