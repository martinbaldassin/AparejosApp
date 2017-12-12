using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AparejosApp.Models
{
    public static class Enumeradores
    {
        public  enum TipoPedido
        {
            SinEspecificar,
            Normal,
            Urgente,
            Reclamo,
        }
        public enum EstadoFabricacionPedido
        {
            SinSeleccionar,
            Pedido,
            EnProduccion,
            FaltaDeMaterial,
            Terminado,
            Despachado
        }
        public enum TipoFiltroPedidos
        {
            Normal,
            PorRangoFechaYTerminados,
            Terminados
        }
    }
}