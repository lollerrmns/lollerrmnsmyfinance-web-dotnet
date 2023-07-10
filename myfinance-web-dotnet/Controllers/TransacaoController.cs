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
        [Route("Transacao/Cadastro")]
        [Route("Transacao/Cadastro/{id}")]
        public IActionResult Cadastro(int id)
        {
            var transacao = new TransacaoModel();
            if (id != null)
            {
                //var transacaoDomain = _myFinanceDbContext.Transacao.Where(x => x.Id == id).FirstOrDefault();
                var transacaoDomain = _myFinanceDbContext.Transacao.Find(id);
                if (transacaoDomain != null)
                {
                    transacao.Id = transacaoDomain.Id;
                    transacao.Historico = transacaoDomain.Historico;
                    transacao.Valor = transacaoDomain.Valor;
                    transacao.Data = transacaoDomain.Data;
                    transacao.PlanoContaId = transacaoDomain.PlanoContaId;
                }

            }
            return View(transacao);
        }


        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [Route("Transacao/Cadastro")]
        [Route("Transacao/Cadastro/{id}")]
        public IActionResult Cadastro(TransacaoModel input)
        {
            var transacao = new Transacao()
            {
                Id = input.Id,
                Historico = input.Historico,
                Valor = input.Valor,
                Data = input.Data,
                PlanoContaId = input.PlanoContaId
            };

            if (transacao.Id == null)
            {
                _myFinanceDbContext.Transacao.Add(transacao);
            }
            else
            {
                _myFinanceDbContext.Transacao.Attach(transacao);
                _myFinanceDbContext.Entry(transacao).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }

            _myFinanceDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("Transacao/Excluir/{id}")]
        public IActionResult Excluir(int? id)
        {
            Console.WriteLine("--------------EXCLUIR-------------");
            var transacao = new Transacao() { Id = id };
            _myFinanceDbContext.Transacao.Remove(transacao);
            _myFinanceDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
