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

    public static Paciente BuscarPaciente(string DNI)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Consulta para verificar el usuario
            string sql = "SELECT * FROM Pacientes WHERE DNI = @pDNI";
            
            var usuario = conn.QuerySingleOrDefault<Paciente>(sql, new { pDNI = DNI});

            if (usuario == null)
            {
                throw new Exception("DNI incorrecto.");
            }

            return usuario; // Devuelve el objeto Usuario si las credenciales son correctas
        }
    }
    public static Doctor BuscarDoctor(string DNI)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Consulta para verificar el usuario
            string sql = "SELECT * FROM Doctores WHERE DNI = @pDNI";
            
            var usuario = conn.QuerySingleOrDefault<Doctor>(sql, new { pDNI = DNI});

            if (usuario == null)
            {
                throw new Exception("DNI incorrecto.");
            }

            return usuario; // Devuelve el objeto Usuario si las credenciales son correctas
        }
    }
    public static Farmacia BuscarFarmacia(string TituloPropiedad)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Consulta para verificar el usuario
            string sql = "SELECT * FROM Farmacias WHERE TituloPropiedad = @pTituloPropiedad";
            
            var usuario = conn.QuerySingleOrDefault<Farmacia>(sql, new { pTituloPropiedad = TituloPropiedad});

            if (usuario == null)
            {
                throw new Exception("TituloPropiedad incorrecto.");
            }

            return usuario; // Devuelve el objeto Usuario si las credenciales son correctas
        }
    }
    public static int BuscarIdFarmacia(string TituloPropiedad)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Consulta para verificar el usuario
            string sql = "SELECT IdFarmacia FROM Farmacias WHERE TituloPropiedad = @pTituloPropiedad";
            
            var usuario = conn.QuerySingleOrDefault<int>(sql, new { pTituloPropiedad = TituloPropiedad});

            return usuario; // Devuelve el objeto Usuario si las credenciales son correctas
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
    public static Producto BuscarProducto(string IdProducto)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Consulta para verificar el usuario
            string sql = "SELECT * FROM Productos WHERE IdProducto = @pIdProducto";
            
            Producto usuario = conn.QuerySingleOrDefault<Producto>(sql, new { pIdProducto = IdProducto});

            if (usuario == null)
            {
                throw new Exception("IdProducto incorrecto.");
            }

            return usuario; // Devuelve el objeto Usuario si las credenciales son correctas
        }
    }
    public static void EliminarProducto(string IdProducto)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Consulta para verificar el usuario
            string sql = "DELETE FROM Productos WHERE IdProducto = @pIdProducto";
            
            var usuario = conn.QuerySingleOrDefault<Doctor>(sql, new { pIdProducto = IdProducto});

            if (usuario == null)
            {
                throw new Exception("IdProducto incorrecto.");
            }
        }
    }
    public static void EditarProducto(int idProducto, string nuevoNombre, string nuevoPrecio, string nuevoStock)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Consulta para actualizar sólo el Nombre, Precio y Stock del producto
            string sql = @"
                UPDATE Productos
                SET Nombre = @pNombre, Precio = @pPrecio, Stock = @pStock
                WHERE IdProducto = @pIdProducto";

            int rowsAffected = conn.Execute(sql, new 
            { 
                pNombre = nuevoNombre,
                pPrecio = nuevoPrecio,
                pStock = nuevoStock,
                pIdProducto = idProducto 
            });

            if (rowsAffected == 0)
            {
                throw new Exception("No se encontró el producto o no se pudo actualizar.");
            }
        }
    }
    public static void AgregarProducto(Producto usu, string IdFarmacia)
    {
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            conn.Open();

            // Verificar si el Paciente ya existe
            string checkSql = "SELECT COUNT(*) FROM Productos WHERE Nombre = @pNombre";
            int count = conn.ExecuteScalar<int>(checkSql, new { pNombre = usu.Nombre});

            if (count > 0)
            {
                throw new Exception("El Producto ya existe con ese email o DNI.");
            }

            // Si el Paciente no existe, registrar
            string sql = "INSERT INTO Pacientes (Nombre, Precio, Stock, Descripcion, IdFarmacia) VALUES (@pNombre, @pPrecio, @pStock, @pDescripcion, @pIdFarmacia); SELECT CAST(scope_identity() AS int);";
            conn.ExecuteScalar<int>(sql, new { 
                pNombre = usu.Nombre, 
                pPrecio = usu.Precio, 
                pStock = usu.Stock, 
                pDescripcion = usu.Descripcion, 
                pIdFarmacia = IdFarmacia
            });
        }
    }
}
