using ApiMinhasFinancas.Dtos.TipoContas;

namespace ApiMinhasFinancas.Data.Dtos.Documentos
{
    public class ReadTipoContaTotalDocs: ReadTipoContaDto
    {
        public double ValorTotal { get; set; }
    }
}
