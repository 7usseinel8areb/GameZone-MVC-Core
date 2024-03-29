
namespace GameZone.Services
{
    public class DevicesServices : IDevicesServices
    {
        private readonly applicationDbContext _applicationDbContext;

        public DevicesServices(applicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IEnumerable<SelectListItem> GetSelectLists()
        {
            return _applicationDbContext.Devices
                .OrderBy(d => d.Name)
                .Select(d => new SelectListItem {Value = d.Id.ToString(),Text = d.Name})
                .AsNoTracking()
                .ToList();
        }
    }
}
