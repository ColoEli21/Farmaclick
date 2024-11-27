using System.Data.SqlClient;
using Dapper;
namespace LoginFarmaclick.Models;

public static class BD
{
    private static string _ConnectionString = "Server=localhost;DataBase=FARMACLICK;Trusted_Connection=true;";
    public static void RegistrarPaciente(Paciente usu)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Verificar si el Paciente ya existe
            string checkSql = "SELECT COUNT(*) FROM Pacientes WHERE Email = @pEmail OR DNI = @pDNI";
            int count = conn.ExecuteScalar<int>(checkSql, new { pEmail = usu.Email, pDNI = usu.DNI });

            if (count > 0)
            {
                throw new Exception("El Paciente ya existe con ese email o DNI.");
            }

            // Si el Paciente no existe, registrar
            string sql = "INSERT INTO Pacientes (Contraseña, Nombre, Apellido, DNI, Email, Telefono) VALUES (@pContraseña, @pNombre, @pApellido, @pDNI, @pEmail, @pTelefono); SELECT CAST(scope_identity() AS int);";
            conn.ExecuteScalar<int>(sql, new { 
                pContraseña = usu.Contraseña, 
                pNombre = usu.Nombre, 
                pApellido = usu.Apellido, 
                pDNI = usu.DNI, 
                pEmail = usu.Email, 
                pTelefono = usu.Telefono 
            });
        }
    }

    public static void RegistrarDoctor(Doctor usu)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Verificar si el Doctor ya existe
            string checkSql = "SELECT COUNT(*) FROM Doctores WHERE Email = @pEmail OR DNI = @pDNI";
            int count = conn.ExecuteScalar<int>(checkSql, new { pEmail = usu.Email, pDNI = usu.DNI });

            if (count > 0)
            {
                throw new Exception("El Doctor ya existe con ese email o DNI.");
            }

            // Si el Doctor no existe, registrar
            string sql = "INSERT INTO Doctores (Contraseña, Nombre, Apellido, DNI, Matricula, Email, Telefono) VALUES (@pContraseña, @pNombre, @pApellido, @pDNI, @pMatricula, @pEmail, @pTelefono); SELECT CAST(scope_identity() AS int);";
                conn.ExecuteScalar<int>(sql, new { 
                pContraseña = usu.Contraseña, 
                pNombre = usu.Nombre, 
                pApellido = usu.Apellido, 
                pDNI = usu.DNI, 
                pMatricula = usu.Matricula, 
                pEmail = usu.Email, 
                pTelefono = usu.Telefono 
            });
        }
    }

    public static void RegistrarFarmacia(Farmacia usu)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Verificar si la Farmacia ya existe
            string checkSql = "SELECT COUNT(*) FROM Farmacias WHERE Email = @pEmail OR TituloPropiedad = @pTituloPropiedad";
            int count = conn.ExecuteScalar<int>(checkSql, new { pEmail = usu.Email, pTituloPropiedad = usu.TituloPropiedad });

            if (count > 0)
            {
                throw new Exception("La Farmacia ya existe con ese email o TituloPropiedad.");
            }

            // Si la Farmacia no existe, registrar
            string sql = "INSERT INTO Farmacias (Contraseña, Nombre, TituloPropiedad, Direccion, Email, Telefono) VALUES (@pContraseña, @pNombre, @pTituloPropiedad, @pDireccion, @pEmail, @pTelefono); SELECT CAST(scope_identity() AS int);";
            conn.ExecuteScalar<int>(sql, new { 
                pContraseña = usu.Contraseña, 
                pNombre = usu.Nombre, 
                pTituloPropiedad = usu.TituloPropiedad, 
                pDireccion = usu.Direccion, 
                pEmail = usu.Email, 
                pTelefono = usu.Telefono 
            });
        }
    }

    
    public static Paciente IniciarSesionPaciente(string email, string contraseña)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Consulta para verificar el usuario
            string sql = "SELECT * FROM Pacientes WHERE Email = @pEmail AND Contraseña = @pContraseña";
            
            var usuario = conn.QuerySingleOrDefault<Paciente>(sql, new { pEmail = email, pContraseña = contraseña });

            return usuario; // Devuelve el objeto Usuario si las credenciales son correctas
        }
    }
    public static Doctor IniciarSesionDoctor(string email, string contraseña)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Consulta para verificar el usuario
            string sql = "SELECT * FROM Doctores WHERE Email = @pEmail AND Contraseña = @pContraseña";
            
            var usuario = conn.QuerySingleOrDefault<Doctor>(sql, new { pEmail = email, pContraseña = contraseña });

            if (usuario == null)
            {
                throw new Exception("Email o contraseña incorrectos.");
            }

            return usuario; // Devuelve el objeto Usuario si las credenciales son correctas
        }
    }
    public static Farmacia IniciarSesionFarmacia(string email, string contraseña)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Consulta para verificar el usuario
            string sql = "SELECT * FROM Farmacias WHERE Email = @pEmail AND Contraseña = @pContraseña";
            
            var usuario = conn.QuerySingleOrDefault<Farmacia>(sql, new { pEmail = email, pContraseña = contraseña });

            if (usuario == null)
            {
                throw new Exception("Email o contraseña incorrectos.");
            }

            return usuario; // Devuelve el objeto Usuario si las credenciales son correctas
        }
    }
    public static List<Producto> BuscarProductos(int IdFarmacia)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Consulta para obtener los productos relacionados con la IdFarmacia
            string sql = "SELECT * FROM Productos WHERE IdFarmacia = @pIdFarmacia";
            
            List<Producto> productos = conn.Query<Producto>(sql, new { pIdFarmacia = IdFarmacia }).ToList();

            return productos; // Devolvemos la lista de productos
        }
    }
    public static Producto BuscarProducto(int IdProducto)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Consulta para obtener los productos relacionados con la IdProducto
            string sql = "SELECT * FROM Productos WHERE IdProducto = @pIdProducto";
            
            Producto productos = conn.QuerySingleOrDefault<Producto>(sql, new { pIdProducto = IdProducto });

            return productos; // Devolvemos la lista de productos
        }
    }
    public static Farmacia BuscarFarmacia(int IdFarmacia)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Consulta para obtener los Farmacias relacionados con la IdFarmacia
            string sql = "SELECT * FROM Farmacias WHERE IdFarmacia = @pIdFarmacia";
            
            Farmacia Farmacias = conn.QuerySingleOrDefault<Farmacia>(sql, new { pIdFarmacia = IdFarmacia });

            return Farmacias; // Devolvemos la lista de productos
        }
    }
    public static void EliminarProducto(int IdProducto)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Consulta para verificar el usuario
            string sql = "DELETE FROM Productos WHERE IdProducto = @pIdProducto";
            
            var usuario = conn.QuerySingleOrDefault<Doctor>(sql, new { pIdProducto = IdProducto});

        }
    }
    public static void EditarProducto(Producto usu, int IdFarmacia)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Consulta para actualizar sólo el Nombre, Precio y Stock del producto
            string sql = @"
                UPDATE Productos
                SET Nombre = @pNombre, Precio = @pPrecio, Stock = @pStock
                WHERE IdProducto = @pIdProducto";

            conn.Execute(sql, new 
            { 
                pNombre = usu.Nombre, 
                pPrecio = usu.Precio, 
                pStock = usu.Stock, 
                pDescripcion = usu.Descripcion, 
                pIdFarmacia = IdFarmacia
            });

        }
    }
    public static void AgregarProducto(Producto usu, int IdFarmacia)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Verificar si el Paciente ya existe
            string checkSql = "SELECT COUNT(*) FROM Productos WHERE Nombre = @pNombre";
            int count = conn.Execute(checkSql, new { pNombre = usu.Nombre});

            if (count > 0)
            {
                throw new Exception("El Producto ya existe con ese email o DNI.");
            }

            // Si el Paciente no existe, registrar
            string sql = "INSERT INTO Productos (Nombre, Precio, Stock, Descripcion, IdFarmacia) VALUES (@pNombre, @pPrecio, @pStock, @pDescripcion, @pIdFarmacia); SELECT CAST(scope_identity() AS int);";
            conn.Execute(sql, new { 
                pNombre = usu.Nombre, 
                pPrecio = usu.Precio, 
                pStock = usu.Stock, 
                pDescripcion = usu.Descripcion, 
                pIdFarmacia = IdFarmacia
            });
        }
    }
    public static List<Producto> Buscar(string query)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Consulta para obtener los productos relacionados con la IdFarmacia y cuyo nombre coincida con el término de búsqueda
            string sql = "SELECT * FROM Productos WHERE Nombre LIKE @pQuery";

            // Ejecutar la consulta usando Dapper
            List<Producto> productos = conn.Query<Producto>(sql, new 
            { 
                pQuery = "%" + query + "%" // Los signos % permiten la búsqueda parcial (LIKE)
            }).ToList();

            return productos; // Devolvemos la lista de productos que coinciden
        }
    }
    public static List<Pedido> BuscarPedidos(int IdPaciente)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Consulta para obtener los Pedidos relacionados con la IdPaciente
            string sql = "SELECT P.*, Pr.Nombre as NombreProducto, F.Nombre as NombreFarmacia FROM Pedidos P INNER JOIN Productos Pr ON P.IdProducto=Pr.IdProducto INNER JOIN Farmacias F ON P.IdFarmacia=F.IdFarmacia WHERE P.IdPaciente = @pIdPaciente";
            
            List<Pedido> Pedidos = conn.Query<Pedido>(sql, new { pIdPaciente = IdPaciente }).ToList();

            return Pedidos; // Devolvemos la lista de Pedidos
        }
    }
    public static List<Pedido> BuscarPedidosFarmacia(int IdPaciente)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Consulta para obtener los Pedidos relacionados con la IdPaciente
            string sql = "SELECT P.*, Pr.Nombre as NombreProducto, F.Nombre as NombrePaciente FROM Pedidos P INNER JOIN Productos Pr ON P.IdProducto=Pr.IdProducto INNER JOIN Pacientes F ON P.IdPaciente=F.IdPaciente WHERE P.IdPaciente = @pIdPaciente";
            
            List<Pedido> Pedidos = conn.Query<Pedido>(sql, new { pIdPaciente = IdPaciente }).ToList();

            return Pedidos; // Devolvemos la lista de Pedidos
        }
    }
    public static void AgregarPedido(Producto usu, int IdPaciente, string Direccion)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Si el Paciente no existe, registrar
            string sql = "INSERT INTO Pedidos (IdProducto, IdFarmacia, Fecha, Direccion, IdPaciente) VALUES (@pIdProducto, @pIdFarmacia, @pFecha, @pDireccion, @pIdPaciente); SELECT CAST(scope_identity() AS int);";
            conn.Execute(sql, new { 
                pIdProducto = usu.IdProducto, 
                pIdFarmacia = usu.IdFarmacia, 
                pFecha = DateTime.Now, 
                pDireccion = Direccion, 
                pIdPaciente = IdPaciente
            });
        }
    }
}
