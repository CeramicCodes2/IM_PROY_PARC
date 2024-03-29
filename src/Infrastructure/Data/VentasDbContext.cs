﻿using System.Data;
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
    public List<VentaGraph> getDataGraph(){
        var data = new List<VentaGraph>();
     var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("SELECT COUNT([P].[ID]) * [P].[CANTIDAD] operacion FROM [PRODUCTO] [P] INNER JOIN [VENTA] [V] ON V.PRODUCTOID=P.ID GROUP BY [P].[CANTIDAD];",con);
        try
        {
            con.Open();
            var dr = cmd.ExecuteReader();
            while( dr.Read()){
                data.Add(
                    new VentaGraph{
                        operacion = (int)dr["operacion"]
                        }
                );
            }
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
    public List<Venta> List()
    {
        var data = new List<Venta>();
        //var dataClient new Cliente();
        var con = new SqlConnection(_connectionString);
        //var cmd = new SqlCommand("SELECT [V].[Id],[V].[ClienteId],[V].[ProductoId],[C].[Nombre],[C].[Direccion],[C].[Telefono],[V].[ProductoId],[P].[Descripcion],[P].[Precio],[P].[Cantidad], [V].[Fecha] FROM [Venta] AS [V] INNER JOIN [Cliente] [C] ON [V].[ClienteId]=[C].[Id] INNER JOIN  [PRODUCTO] AS [P] ON [P].[Id]=[V].[ProductoId]",con);
        var cmd = new SqlCommand("SELECT [V].[Id],[V].[ClienteId],[C].[Nombre],[C].[Direccion],[C].[Correo],[C].[Telefono],[V].[ProductoId],[P].[Descripcion],[P].[Precio],[P].[Cantidad],[V].[Fecha] FROM [Venta] AS [V] INNER JOIN [Cliente] [C] ON [V].[ClienteId]=[C].[Id] INNER JOIN  [PRODUCTO] AS [P] ON [P].[Id]=[V].[ProductoId]",con);

        //[C].[Nombre],[C].[Direccion],[C].[Telefono],[V].[ProductoId],
        //[P].[Descripcion],[P].[Precio],[P].[Cantidad]

        // ToDo
        // SELECT [V].[Id],[V].[ClienteId],[C].[Nombre],[C].[Direccion],[C].[Telefono],[V].[ProductoId],[P].[Descripcion],[P].[Precio],[P].[Cantidad],[V].[Fecha] FROM [Venta] AS [V] INNER JOIN [Cliente] [C] ON [V].[ClienteId]=[C].[Id] INNER JOIN  [PRODUCTO] AS [P] ON [P].[Id]=[V].[ProductoId]
        try
        {
            con.Open();
            var cData = new Cliente();
            var dr = cmd.ExecuteReader();
            while( dr.Read()){
                data.Add(
                    new Venta{
                        Id = (Guid)dr["Id"],
                        ClienteId = (Guid)dr["ClienteId"],
                        // cliente secction:
                        Cliente = new Cliente{
                            Id = (Guid)dr["ClienteId"],// convertimos el dato id a guid
                            Nombre = (string)dr["Nombre"],
                            Direccion =  (string)dr["Direccion"],
                            Telefono = (string)dr["Telefono"],
                            Correo = (string)dr["Correo"]
                        },
                        ProductoId = (Guid)dr["ProductoId"],
                        Producto = new Producto
                            {
                                Id = (Guid)dr["ProductoId"],
                                Descripcion = (string)dr["Descripcion"],
                                Precio = (decimal)dr["Precio"],
                                Cantidad = (int)dr["Cantidad"]
                            },
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
        var cmd = new SqlCommand("SELECT [V].[Id],[V].[ClienteId],[C].[Nombre],[C].[Direccion],[C].[Correo],[C].[Telefono],[V].[ProductoId],[P].[Descripcion],[P].[Precio],[P].[Cantidad],[V].[Fecha] FROM [Venta] AS [V] INNER JOIN [Cliente] AS [C] ON [V].[ClienteId]=[C].[Id] INNER JOIN  [PRODUCTO] AS [P] ON [P].[Id]=[V].[ProductoId] WHERE [V].[Id]=@Id",con);
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
        var con = new SqlConnection(_connectionString);
        var com = new SqlCommand("INSERT INTO [VENTA]([ClienteId],[ProductoId],[Fecha]) VALUES (@ClienteId,@ProductoId,@Fecha)",con);
        // ToDo
        com.Parameters.Add("ClienteId",SqlDbType.UniqueIdentifier).Value = data.ClienteId;
        com.Parameters.Add("ProductoId",SqlDbType.UniqueIdentifier).Value = data.ProductoId;
        com.Parameters.Add("Fecha",SqlDbType.DateTime).Value = data.Fecha;
        // 1) checamos si el id existe
        var ccom = new SqlCommand("SELECT COUNT([Id]) FROM [Cliente] WHERE [Id]=@Id",con);
        ccom.Parameters.Add("Id",SqlDbType.UniqueIdentifier).Value = data.ClienteId;

        var pcom =  new SqlCommand("SELECT COUNT([Id]) FROM [PRODUCTO] WHERE [Id] = @Id",con);
        pcom.Parameters.Add("Id",SqlDbType.UniqueIdentifier).Value = data.ProductoId;
        try
        {
            con.Open();
            var clientaff = (Int32)ccom.ExecuteScalar();
            var prodaff = (Int32)pcom.ExecuteScalar();
            // debe ser 1 si es cero esta mal
            if(clientaff == 1 & prodaff == 1){
               com.ExecuteNonQuery();
                // solo insertamos si los datos existen
            }
            //com.ExecuteNonQuery();
            // ToDo
        }
        catch (Exception) { throw; }
        finally
        {
            con.Close();
            // ToDo

        }
    }

    public void Edit(Venta data)
    {
        // ToDo
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("UPDATE [VENTA] SET [ProductoId]=@ProductoId,[ClienteId]=@ClienteId,[Fecha]=@Fecha WHERE [Id]=@Id",con);
        cmd.Parameters.Add("Id",SqlDbType.UniqueIdentifier).Value = data.Id; 
        cmd.Parameters.Add("ProductoId",SqlDbType.UniqueIdentifier).Value = data.ProductoId;
        cmd.Parameters.Add("ClienteId",SqlDbType.UniqueIdentifier).Value = data.ClienteId;
        cmd.Parameters.Add("Fecha",SqlDbType.DateTime).Value = data.Fecha;
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
        // ToDo
        var con = new SqlConnection(_connectionString);
        var com = new SqlCommand("DELETE FROM [VENTA] WHERE ID=@Id",con);

        // ToDo
        com.Parameters.Add("Id",SqlDbType.UniqueIdentifier).Value = id;


        try
        {
            con.Open();
            com.ExecuteNonQuery();
            // ToD1o
        }
        catch (Exception) { throw; }
        finally
        {
            con.Close();
            // ToDo
        }
    }
}