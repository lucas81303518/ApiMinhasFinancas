using ApiMinhasFinancas.Dtos.Documentos;
using System.ComponentModel.DataAnnotations;

namespace ApiMinhasFinancas.Dtos.TipoContas
{
    public class ReadTipoContaDto
    {
        public int Id { get; set; }        
        public string NomeConta { get; set; }        
        public int Tipo { get; set; }       
    }
}
