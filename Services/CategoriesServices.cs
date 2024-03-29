namespace GameZone.Services
{
    public class CategoriesServices : ICategoriesServices
    {
        private readonly applicationDbContext _applicationDbContext;

        public CategoriesServices(applicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _applicationDbContext.Categories
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .AsNoTracking()
                .ToList();
        }
    }
}
