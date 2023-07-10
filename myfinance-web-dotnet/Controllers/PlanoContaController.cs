using Microsoft.AspNetCore.Mvc;
using myfinance_web_dotnet.Models;
using myfinance_web_dotnet.Domain;
using myfinance_web_dotnet.Application.ObterPlanoContaUseCase;

namespace myfinance_web_dotnet.Controllers
{
    public class PlanoContaController : Controller
    {
        private readonly ILogger<PlanoContaController> _logger;
        private readonly MyFinanceDbController _myFinanceDbContext;
        private readonly IObterPlanoContaUseCase _obterPlanoContaUseCase;

        public PlanoContaController(
            ILogger<PlanoContaController> logger,
            MyFinanceDbController myFinanceDbContext,
            IObterPlanoContaUseCase obterPlanoContaUseCase

        )
        {
            _logger = logger;
            _myFinanceDbContext = myFinanceDbContext;
            _obterPlanoContaUseCase = obterPlanoContaUseCase;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var listaPlanoContas = _myFinanceDbContext.PlanoConta;
            var listaPlanoContaModel = new List<PlanoContaModel>();
            foreach (var item in listaPlanoContas)
            {
                var planoContaModel = new PlanoContaModel()
                {
                    Id = item.Id,
                    Descricao = item.Descricao,
                    Tipo = item.Tipo
                };
                listaPlanoContaModel.Add(planoContaModel);
            }
            ViewBag.listaPlanoConta = listaPlanoContaModel;
            return View();




        }

        [HttpGet]
        [Route("PlanoConta/Cadastro")]
        [Route("PlanoConta/Cadastro/{id}")]
        public IActionResult Cadastro(int? id)
        {
            Console.WriteLine("--------------Cadastro(int? id)-------------");
            var planoConta = new PlanoContaModel();
            if (id != null)
            {
                //var planoContaDomain = _myFinanceDbContext.PlanoConta.Where(x => x.Id == id).FirstOrDefault();
                var planoContaDomain = _myFinanceDbContext.PlanoConta.Find(id);
                if (planoContaDomain != null)
                {
                    planoConta.Id = planoContaDomain.Id;
                    planoConta.Descricao = planoContaDomain.Descricao;
                    planoConta.Tipo = planoContaDomain.Tipo;
                }
            }
            return View(planoConta);
        }


        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [Route("PlanoConta/Cadastro")]
        [Route("PlanoConta/Cadastro/{id}")]
        public IActionResult Cadastro(PlanoContaModel input)
        {
            var planoConta = new PlanoConta()
            {
                Id = input.Id,
                Descricao = input.Descricao,
                Tipo = input.Tipo
            };

            if (planoConta.Id == null)
            {
                _myFinanceDbContext.PlanoConta.Add(planoConta);
            }
            else
            {
                _myFinanceDbContext.PlanoConta.Attach(planoConta);
                _myFinanceDbContext.Entry(planoConta).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }

            _myFinanceDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("PlanoConta/Excluir/{id}")]
        public IActionResult Excluir(int? id)
        {
            Console.WriteLine("--------------EXCLUIR-------------");
            var planoConta = new PlanoConta() { Id = id };
            _myFinanceDbContext.PlanoConta.Remove(planoConta);
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
