
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
                    Historico = item.Histrico,
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
        [Route("Cadastro")]
        [Route("Cadastro/{id}")]
        public IActionResult Cadastro(int id)
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
        public IActionResult Cadastro()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        [HttpPost]
        [Route("Cadastro")]
        [Route("Cadastro/{input}")]
        public IActionResult Cadastro(PlanoContaModel input)
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
