
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class GameServices : IGameServices
    {
        private readonly applicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imgPath;

        public GameServices(applicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _applicationDbContext = applicationDbContext;
            _webHostEnvironment = webHostEnvironment;
            _imgPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.imgPath}";
        }
        public async Task Create(CreateGameFormViewModel viewModel)
        {
            var ImageName = await SaveCover(viewModel.Image);

            Game game = new()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                CategoryId = viewModel.CategoryId,
                Image = ImageName,
                Devices = viewModel.SelectedDevices.Select(d => new GameDevice { DeviceId = d}).ToList()
            };

            _applicationDbContext.Add(game);
            _applicationDbContext.SaveChanges();
        }

        public IEnumerable<Game> GetAllGames()
        {
            var games = _applicationDbContext.Games
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(g => g.Device)
                .AsNoTracking()
                .ToList();
            return games;
        }

        public Game? GetById(int id)
        {
            var game = _applicationDbContext.Games
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(g => g.Device)
                .AsNoTracking()
                .SingleOrDefault(g => g.Id == id);
            return game;
        }

        public async Task<Game?> Edit(EditGameFormViewModel model)
        {
            var game = _applicationDbContext.Games
           .Include(g => g.Devices)
           .SingleOrDefault(g => g.Id == model.Id);

            if (game is null)
                return null;

            var hasNewCover = model.Image != null;
            var oldCover = game.Image;

            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();

            if (hasNewCover)
            {
                game.Image = await SaveCover(model.Image!);
            }

            var effectedRows = _applicationDbContext.SaveChanges();

            if (effectedRows > 0)
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(_imgPath, oldCover);
                    File.Delete(cover);
                }

                return game;
            }
            else
            {
                var cover = Path.Combine(_imgPath, game.Image);
                File.Delete(cover);

                return null;
            }
        }

        public bool Delete(int id)
        {
            bool isDeleted = false;

            var game = _applicationDbContext.Games.Find(id);

            if (game is null)
                return isDeleted;

            _applicationDbContext.Games.Remove(game);
            var effectedRows = _applicationDbContext.SaveChanges();

            if (effectedRows > 0)
            {
                isDeleted = true;
                var cover = Path.Combine(_imgPath,game.Image);
                File.Delete(cover);
            }


            return isDeleted;
        }
        private async Task<string> SaveCover(IFormFile img)
        {
            var ImageName = $"{Guid.NewGuid()}{Path.GetExtension(img.FileName)}";

            var path = Path.Combine(_imgPath, ImageName);

            using var stream = File.Create(path);
            await img.CopyToAsync(stream);

            return ImageName;
        }
    }
}
