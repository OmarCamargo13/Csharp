//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Asp.NETMVCCRUD.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Nominas
    {
        public int NominaID { get; set; }
        public int EmpleadoID { get; set; }
        public decimal IngresoBase { get; set; }
        public decimal DeduccionDesayuno { get; set; }
        public decimal DeduccionAhorro { get; set; }
        public System.DateTime FechaNomina { get; set; }
    
        public virtual Empleados Empleados { get; set; }
    }
}
