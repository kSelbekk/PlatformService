using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _context;

        public PlatformRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreatePlatform(Platform platform)
        {
            if (platform is null)
                throw new ArgumentNullException(nameof(platform));

            _context.platforms.Add(platform);
        }

        public Platform GetPlatformById(int id)
        {
            return _context.platforms.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Platform> GetPlatforms()
        {
            return _context.platforms.ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
