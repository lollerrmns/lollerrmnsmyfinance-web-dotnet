using myfinance_web_dotnet.Models;
using myfinance_web_dotnet.Services.Interfaces;

namespace myfinance_web_dotnet.Service.PlanoConta
{

    public class ObterPlanoContaUseCase : IPlanoContaService
    {
        private readonly IPlanoContaService _planoContaService;

        public ObterPlanoContaUseCase(IPlanoContaService planoContaService){
            _planoContaService = planoContaService;
        }
        public List<PlanoContaModel> istaPlanoContaModel()
        {
            var lista = new List<PlanoContaModel>();
            return lista;
        }
    }
}