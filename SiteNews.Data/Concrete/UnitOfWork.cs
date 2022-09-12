using SiteNews.Data.Abstract;
using SiteNews.Data.Concrete.EfCore;
using System.Threading.Tasks;

namespace SiteNews.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CoreContext _context;
        private KategoriRepository _kategoriRepository;
        private HaberRepository _haberRepository;
        private YazarRepository _yazarRepository;
        private VideoRepository _videoRepository;
        private SosyalMedyaRepository _sosyalMedyaRepository;
        public UnitOfWork(CoreContext context)
        {
            _context = context;
        }

        public IHaberRepository Habers => _haberRepository ??= new HaberRepository(_context);
        public IKategoriRepository Kategoris => _kategoriRepository ??= new KategoriRepository(_context);
        public IYazarRepository Yazars => _yazarRepository ??= new YazarRepository(_context);
        public IVideoRepository Videos => _videoRepository ??= new VideoRepository(_context);
        public ISosyalMedyaRepository SosyalMedya => _sosyalMedyaRepository ??= new SosyalMedyaRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Kayit()
        {
            _context.SaveChanges();
        }

        public async Task<int> KayitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
