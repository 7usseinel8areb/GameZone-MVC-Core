namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ICategoriesServices _categoriesServices;//add service at Program
        private readonly IDevicesServices _devicesServices;
        private readonly IGameServices _gameServices;
        public GamesController(ICategoriesServices categoriesServices, IDevicesServices devicesServices, IGameServices gameServices)
        {
            _categoriesServices = categoriesServices;
            _devicesServices = devicesServices;
            _gameServices = gameServices;
        }

        public IActionResult Index()
        {
            var games = _gameServices.GetAllGames();
            return View(games);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new()
            {
                Categories = _categoriesServices.GetSelectList(),
                Devices = _devicesServices.GetSelectLists()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(CreateGameFormViewModel newGame) 
        {
            if(!ModelState.IsValid)
            {
                newGame.Categories = _categoriesServices.GetSelectList();
                newGame.Devices = _devicesServices.GetSelectLists();
                return View(newGame);
            }
            await _gameServices.Create(newGame);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            Game? game = _gameServices.GetById(id);
            if(game is null)
                return NotFound();
            return View(game);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _gameServices.GetById(id);
            if (game is null)
                return NotFound();
            EditGameFormViewModel viewModel = new()
            {
                 Id = id,
                 Categories = _categoriesServices.GetSelectList(),
                 Devices = _devicesServices.GetSelectLists(),
                 CategoryId = game.CategoryId,
                 Description = game.Description,
                 Name = game.Name,
                 SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                 CurrentImage = game.Image
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel newGame)
        {
            if (!ModelState.IsValid)
            {
                newGame.Categories = _categoriesServices.GetSelectList();
                newGame.Devices = _devicesServices.GetSelectLists();
                return View(newGame);
            }

            var game = await _gameServices.Edit(newGame);

            if (game is null)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            bool isDeleted = _gameServices.Delete(id);
            return isDeleted?RedirectToAction("Index"):BadRequest();
        }
    }
}
