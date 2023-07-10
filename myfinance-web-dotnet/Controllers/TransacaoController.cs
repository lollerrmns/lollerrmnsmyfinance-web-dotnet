using Microsoft.AspNetCore.Mvc;
using myfinance_web_dotnet.Models;
using myfinance_web_dotnet.Domain;
namespace myfinance_web_dotnet.Controllers
{
    public class TransacaoController : Controller
    {
        private readonly ILogger<TransacaoController> _logger;
        private readonly MyFinanceDbController _myFinanceDbContext;

        public TransacaoController(
            ILogger<TransacaoController> logger,
            MyFinanceDbController myFinanceDbContext
        )
        {
            _logger = logger;
            _myFinanceDbContext = myFinanceDbContext;
        }

        public IActionResult Index()
        {
            var listaTransacoes = _myFinanceDbContext.Transacao;
            var listaTransacaoModel = new List<TransacaoModel>();
            foreach (var item in listaTransacoes)
            {
                var transacaoModel = new TransacaoModel()
                {
                    Id = item.Id,
                    Historico = item.Historico,
                    Data = item.Data,
                    Valor = item.Valor,
                    PlanoContaId = item.PlanoContaId
                };
                listaTransacaoModel.Add(transacaoModel);
            }
            ViewBag.listaPlanoConta = listaTransacaoModel;
            return View();
        }

        [HttpGet]
        [Route("TCadastro")]
        [Route("TCadastro/{id}")]
        public IActionResult TCadastro(int id)
        {
            var planoConta = new PlanoContaModel();
            if (id != null)
            {
                var planoContaDomain = _myFinanceDbContext.PlanoConta.Where(x => x.Id == id).FirstOrDefault();
                planoConta.Id = planoContaDomain.Id;
                planoConta.Descricao = planoContaDomain.Descricao;
                planoConta.Tipo = planoContaDomain.Tipo;

            }
            return View(planoConta);
        }


        [HttpGet]
        public IActionResult TCadastro()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        [HttpPost]
        [Route("TCadastro")]
        [Route("TCadastro/{input}")]
        public IActionResult TCadastro(PlanoContaModel input)
        {
            var planoConta = new PlanoConta()
            {
                Id = input.Id,
                Descricao = input.Descricao,
                Tipo = input.Tipo
            };
            _myFinanceDbContext.PlanoConta.Add(planoConta);
            _myFinanceDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
