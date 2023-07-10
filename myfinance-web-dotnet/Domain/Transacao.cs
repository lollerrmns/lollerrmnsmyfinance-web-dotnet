namespace myfinance_web_dotnet.Domain
{
    public class Transacao
    {
        public int? Id {get; set;}
        public DateTime? Data {get; set;}
        public decimal? Valor {get; set;}
        public string? Histrico {get; set;}
        public int? PlanoContaId {get; set;}
    }
}