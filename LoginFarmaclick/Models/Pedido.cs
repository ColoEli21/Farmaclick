namespace LoginFarmaclick.Models;
public class Pedido
{
    public int IdPedido { get; set; }
    public int IdPaciente { get; set; }
    public int IdFarmacia { get; set; }
    public int IdProducto { get; set; }
    public string Direccion { get; set; }
    public DateTime Fecha { get; set; }
    public string NombreProducto{ get; set; }
    public string NombreFarmacia{ get; set; }
    public string NombrePaciente{ get; set; }
}
