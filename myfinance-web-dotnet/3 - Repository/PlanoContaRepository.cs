using myfinance_web_dotnet.Models;
using myfinance_web_dotnet.Repository.Interfaces;

namespace myfinance_web_dotnet.Repository
{

    public class PlanoContaRepository : IPlanoContaRepository
    {
        private readonly MyFinanceDbController _myFinanceDbContext;
        public List<PlanoContaModel> listaPlanoContaModel()
        {
            return null;
            //throw new NotImplementedException();
        }
    }
}