using System.Data;
using System.Data.SqlClient;

using Domain;

namespace Infrastructure;

public class VentasDbContext
{
    private readonly string _connectionString;
    public VentasDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Venta> List()
    {
        var data = new List<Venta>();
        //var dataClient new Cliente();
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("SELECT [V].[Id],[V].[ClienteId],[V].[ProductoId],[V].[Fecha] FROM [Venta] AS [V] INNER JOIN [Cliente] [C] ON [V].[ClienteId]=[C].[Id] INNER JOIN  [PRODUCTO] AS [P] ON [P].[Id]=[V].[ProductoId]",con);
        // ToDo
        // SELECT [V].[Id],[V].[ClienteId],[C].[Nombre],[C].[Direccion],[C].[Telefono],[V].[ProductoId],[P].[Descripcion],[P].[Precio],[P].[Cantidad],[V].[Fecha] FROM [Venta] AS [V] INNER JOIN [Cliente] [C] ON [V].[ClienteId]=[C].[Id] INNER JOIN  [PRODUCTO] AS [P] ON [P].[Id]=[V].[ProductoId]
        try
        {
            con.Open();
            var dr = cmd.ExecuteReader();
            while( dr.Read()){
                data.Add(
                    new Venta{
                        Id = (Guid)dr["Id"],
                        ClienteId = (Guid)dr["ClienteId"],
                        // cliente secction:
                        //Cliente = (Cliente)dr["Cliente"],
                        ProductoId = (Guid)dr["ProductoId"],
                        //Producto = (Producto)dr["Producto"],
                        Fecha = (DateTime)dr["Fecha"]
                    }
                );
            }
            /*

//            
CREATE TABLE VENTAS(
    [Id] [UNIQUEIDENTIFIER] NOT NULL,
    [ClienteId] [UNIQUEIDENTIFIER] NOT NULL,
    [ProductoId] [UNIQUEIDENTIFIER] NOT NULL,
    [FECHA] [DATETIME]

);
-- UNIQUE ME GARANTIZA QUE UN CAMPO NO SE REPITA
-- 
-- SEMPRE DECLARAR PRIMERO TODO Y LUEGO GENERAR CONSTRAINTS
            */

            // ToDo
            return data;
        }
        catch (Exception) { 
            Console.WriteLine("Error de conexion a base de datos en controlador Ventas");
            throw; }
        finally
        {
            con.Close();
            // ToDo
        }
    }
    public Venta Details(Guid id)
    {
        var data = new Venta();
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("SELECT [V].[Id],[V].[ClienteId],[C].[Nombre],[C].[Direccion],[C].[Correo],[C].[Telefono],[V].[ProductoId],[P].[Descripcion],[P].[Precio],[P].[Cantidad],[V].[Fecha] FROM [Venta] AS [V] INNER JOIN [Cliente] [C] ON [V].[ClienteId]=[C].[Id] INNER JOIN  [PRODUCTO] AS [P] ON [P].[Id]=[V].[ProductoId]",con);
        // ToDo
        cmd.Parameters.Add("@Id",SqlDbType.UniqueIdentifier).Value = id;
        try
        {
            con.Open();
            var dr = cmd.ExecuteReader();
            if(dr.Read()){
                data.Id = (Guid)dr["Id"];
                data.ClienteId = (Guid)dr["ClienteId"];
                data.Cliente = new Cliente{
                    Id = (Guid)dr["Id"],// convertimos el dato id a guid
                    Nombre = (string)dr["Nombre"],
                    Direccion =  (string)dr["Direccion"],
                    Telefono = (string)dr["Telefono"],
                    Correo = (string)dr["Correo"]
                };
                data.ProductoId = (Guid)dr["ProductoId"];
                data.Producto = new Producto{
                    Id = (Guid)dr["Id"],
                    Descripcion = (string)dr["Descripcion"],
                    Precio = (decimal)dr["Precio"],
                    Cantidad = (int)dr["Cantidad"]
                };
                data.Fecha = (DateTime)dr["Fecha"];
            }
            // ToDo
            return data;
        }
        catch (Exception) { throw; }
        finally
        {
            con.Close();
            // ToDo
        }
    }

    public void Create(Venta data)
    {
        // ToDo

        try
        {
            // ToDo
        }
        catch (Exception) { throw; }
        finally
        {
            // ToDo
        }
    }

    public void Edit(Venta data)
    {
        // ToDo

        try
        {
            // ToDo
        }
        catch (Exception) { throw; }
        finally
        {
            // ToDo
        }
    }

    public void Delete(Guid id)
    {
        // ToDo

        try
        {
            // ToDo
        }
        catch (Exception) { throw; }
        finally
        {
            // ToDo
        }
    }
}
