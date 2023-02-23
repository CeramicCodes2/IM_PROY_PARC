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
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("SELECT [Id],[Fecha] FROM [Ventas]",con);
        // ToDo
        try
        {
            con.Open();
            var dr = cmd.ExecuteReader();
            while( dr.Read()){
                data.Add(
                    new Venta{
                        Id = (Guid)dr["Id"],
                        //ClienteId = (Guid)dr["ClienteId"],
                        //Cliente = (Cliente)dr["Cliente"],
                        //ProductoId = (Guid)dr["ProductoId"],
                        //Producto = (Producto)dr["Producto"],
                        Fecha = (DateTime)dr["Fecha"]
                    }
                );
            }

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

        // ToDo
        try
        {
            // ToDo
            return data;
        }
        catch (Exception) { throw; }
        finally
        {
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
