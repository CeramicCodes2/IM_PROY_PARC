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
        //var con = new SqlConnection(_connectionString);
        // creamos una nueva conexion con sqlconnection
        //var query = new SqlCommand("SELECT [Id],[Nombre],[Direccion],[Telefono],[Correo] FROM [Cliente]",con);
        // obtenemos los datos pero requerimos de un cursor
        (SqlConnection con,SqlCommand query) = sqlConnectionAndCommand("SELECT [Id],[Nombre],[Direccion],[Telefono],[Correo] FROM [Cliente]");

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
       // var con = new SqlConnection();
        //var query = new SqlCommand("SELECT [Id],[Nombre],[Direccion],[Telefono],[Correo] FROM [Cliente] where [Id] = @Id",con);
        
        (SqlConnection con,SqlCommand query) = sqlConnectionAndCommand("SELECT [Id],[Nombre],[Direccion],[Telefono],[Correo] FROM [Cliente] where [Id] = @Id");
        // de parametro requiere un id
        query.Parameters.Add("@Id",SqlDbType.UniqueIdentifier).Value = id;
        // obtenemos los datos del id
        // ToDo
        try
        {   
            con.Open();
            var dr = query.ExecuteReader();
            if (dr.Read()){
                // si no es nulo 
                data.Id = (Guid)dr["Id"];
                data.Nombre = (string)dr["Nombre"];
                data.Direccion = (string)dr["Direccion"];
                data.Telefono = (string)dr["Telefono"];
                data.Correo = (string)dr["Correo"];
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
    public (SqlConnection,SqlCommand) sqlConnectionAndCommand (string com){
        /* ESTE METODO CREA Y RETORNA UN OBJETO DE CONEXION CON UN COMANDO*/
        // -> tuple:(sqlconnection,sqlcommand)
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand(com,con);
        return (con,cmd);
    }
    public void Create(Cliente data)
    {
        (SqlConnection con,SqlCommand cmd) = sqlConnectionAndCommand("INSERT INTO [Cliente] ([Nombre],[Direccion],[Telefono],[Correo]) VALUES (@Nombre,@Direccion,@Telefono,@Correo)");
        // ToDo
        cmd.Parameters.Add("Nombre",SqlDbType.NVarChar, 128).Value = data.Nombre;
        cmd.Parameters.Add("Direccion",SqlDbType.NVarChar, 128).Value = data.Direccion;
        cmd.Parameters.Add("Telefono",SqlDbType.NVarChar, 128).Value = data.Telefono;
        cmd.Parameters.Add("Correo",SqlDbType.NVarChar, 128).Value = data.Correo;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            // consultas de un solo valor o inserts
            // ToDo
        }
        catch (Exception) { throw; }
        finally
        {
            con.Close();
            // ToDo
        }
    }

    public void Edit(Cliente data)
    {
        // ToDo
        (SqlConnection con,SqlCommand cmd) = sqlConnectionAndCommand("UPDATE [Cliente] SET [Nombre] = @Nombre,[Direccion] = @Direccion,[Telefono] = @Telefono WHERE [Id] = @Id");
        cmd.Parameters.Add("Nombre",SqlDbType.NVarChar,128).Value = data.Nombre;
        cmd.Parameters.Add("Direccion",SqlDbType.NVarChar,128).Value = data.Direccion;
        cmd.Parameters.Add("Telefono",SqlDbType.NVarChar,128).Value = data.Telefono;
        cmd.Parameters.Add("Correo",SqlDbType.NVarChar,128).Value = data.Correo;
        cmd.Parameters.Add("Id",SqlDbType.UniqueIdentifier).Value = data.Id;
        
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();

            // ToDo
        }
        catch (Exception) { throw; }
        finally
        {
            con.Close();
            // ToDo
        }
    }

    public void Delete(Guid id)
    {
        (SqlConnection con,SqlCommand cmd) = sqlConnectionAndCommand("DELETE FROM [Cliente] WHERE [Id] = @Id");
        cmd.Parameters.Add("Id",SqlDbType.UniqueIdentifier).Value = id;

        // ToDo

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            // ToDo
        }
        catch (Exception) { throw; }
        finally
        {
            con.Close();
            // ToDo
        }
    }
}