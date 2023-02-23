using System.ComponentModel.DataAnnotations;

namespace Domain;
public class Venta
{
    [Key]
    public Guid Id { get; set; }
    //Cliente
    public Guid ClienteId { get; set; }
    //public virtual Cliente Cliente { get; set; }
    //Producto
    public Guid ProductoId { get; set; }
    //public virtual Producto Producto { get; set; }
    [Required]
    public DateTime Fecha { get; set; }
}

/*
Un método virtual puro es un método virtual que exige que una clase derivada implemente un método y no permite la creación de
 instancias de la clase base o clase abstracta.

https://techinfo.wiki/metodo-virtual/
*/
