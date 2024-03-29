namespace GameZone.Services
{
    public interface IGameServices
    {
        Task Create(CreateGameFormViewModel viewModel);

        IEnumerable<Game> GetAllGames();

        Game? GetById(int id);

        Task<Game?> Edit(EditGameFormViewModel viewModel);

        bool Delete (int id);
    }
}
