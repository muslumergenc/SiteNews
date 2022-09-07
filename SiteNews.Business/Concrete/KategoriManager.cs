using SiteNews.Business.Abstract;
using SiteNews.Data.Abstract;
using SiteNews.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteNews.Business.Concrete
{
    public class KategoriManager : IKategoriService
    {
        readonly private IUnitOfWork _unitOfWork;

        public KategoriManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(Kategori entity)
        {
            _unitOfWork.Kategoris.Create(entity);
            _unitOfWork.Kayit();
        }

        public async Task<Kategori> CreateAsync(Kategori entity)
        {
            await _unitOfWork.Kategoris.CreateAsync(entity);
            await _unitOfWork.KayitAsync();
            return entity;
        }

        public void Delete(Kategori entity)
        {
            _unitOfWork.Kategoris.Delete(entity);
            _unitOfWork.Kayit();
        }

        public async Task<List<Kategori>> GetAll()
        {
            return await _unitOfWork.Kategoris.GetAll();
        }

        public async Task<Kategori> GetById(int id)
        {
            return await _unitOfWork.Kategoris.GetById(id);
        }

        public async Task<List<Kategori>> GetBySira()
        {
            return await _unitOfWork.Kategoris.GetBySira();
        }

        public void Update(Kategori entity)
        {
            _unitOfWork.Kategoris.Update(entity);
            _unitOfWork.Kayit();
        }
    }
}
