using myfinance_web_dotnet.Models;

namespace myfinance_web_dotnet.Application.ObterPlanoContaUseCase
{
    public class ObterPlanoContaUseCase : IObterPlanoContaUseCase
    {

        List<PlanoContaModel> IObterPlanoContaUseCase.GetListaPlanoContaModel()
        {
            var lista = new List<PlanoContaModel>();
            return lista;
        }
    }
}