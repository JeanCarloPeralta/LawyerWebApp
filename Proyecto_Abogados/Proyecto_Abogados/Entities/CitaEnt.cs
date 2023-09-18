namespace Proyecto_Abogados.Entities
{
    public class CitaEnt
    {
        public long Id_Cita { get; set; }
        public long Id_Abogado { get; set; }
        public string Nombre_Cliente { get; set; }
        public System.DateTime Fecha_Hora { get; set; }
        public int Telefono { get; set; }
        public string Correo_Electronico { get; set; }
        public long Id_Usuario { get; set; }
    }
}