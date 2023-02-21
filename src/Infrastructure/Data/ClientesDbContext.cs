using System.Data;
using System.Data.SqlClient;

using Domain;

namespace Infrastructure;
public class ClientesDbContext
{
    private readonly string _connectionString;
    public ClientesDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Cliente> List()
    {
        // creamos una nueva lista que contendra todos los registros de la tabla cliente
        var data = new List<Cliente>();
        var con = new SqlConnection(_connectionString);
        // creamos una nueva conexion con sqlconnection
        var query = new SqlCommand("SELECT [Id],[Nombre],[Direccion],[Telefono],[Correo] FROM [Cliente]",con);
        // obtenemos los datos pero requerimos de un cursor
        // ToDo
        try
        {  
            con.Open();
            var dr = query.ExecuteReader();
            // si la consulta aroja muchos datos requerimos un curor para procesarlos

            while(dr.Read()){
                data.Add(new Cliente{
                    Id = (Guid)dr["Id"],// convertimos el dato id a guid
                    Nombre = (string)dr["Nombre"],
                    Direccion =  (string)dr["Direccion"],
                    Telefono = (string)dr["Telefono"],
                    Correo = (string)dr["Correo"]
                }
                );
            }
            // abrimos la conexion con la base de datos
            // ToDo
            return data;
        }
        catch (Exception) { throw; }
        finally
        {
            con.Close();
            // cerramos la conexion siempre
            // ToDo
        }
    }

    public Cliente Details(Guid id)
    {
        var data = new Cliente();

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

    public void Create(Cliente data)
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

    public void Edit(Cliente data)
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