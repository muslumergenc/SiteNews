using SiteNews.Business.Abstract;
using SiteNews.Data.Abstract;
using SiteNews.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteNews.Business.Concrete
{
    public class HaberManager : IHaberService
    {
        readonly private IUnitOfWork _unitOfWork;

        public HaberManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(Haber entity)
        {
            _unitOfWork.Habers.Create(entity);
            _unitOfWork.Kayit();
        }

        public async Task<Haber> CreateAsync(Haber entity)
        {
            await _unitOfWork.Habers.CreateAsync(entity);
            await _unitOfWork.KayitAsync();
            return entity;
        }

        public void Delete(Haber entity)
        {
            _unitOfWork.Habers.Delete(entity);
            _unitOfWork.Kayit();
        }

        public async Task<List<Haber>> GetAll()
        {
            return await _unitOfWork.Habers.GetAll();
        }

        public async Task<Haber> GetById(int id)
        {
            return await _unitOfWork.Habers.GetById(id);
        }

        public async Task<Haber> GetByUrl(string url)
        {
            return await _unitOfWork.Habers.GetByUrl(url);
        }

        public async Task<List<Haber>> ListById()
        {
            return await _unitOfWork.Habers.ListById();
        }

        public async Task<List<Haber>> ListByKategori(int katId)
        {
           return await _unitOfWork.Habers.ListByKategori(katId);
        }

        public async Task<List<Haber>> ListByMakale()
        {
            return await _unitOfWork.Habers.ListByMakale();
        }

        public async Task<List<Haber>> ListByMakaleId()
        {
            return await _unitOfWork.Habers.ListByMakaleId();
        }

        public async Task<List<Haber>> ListByMakaleOkuma()
        {
            return await _unitOfWork.Habers.ListByMakaleOkuma();
        }

        public async Task<List<Haber>> ListByOkuma()
        {
            return await _unitOfWork.Habers.ListByOkuma();
        }

        public async Task<List<Haber>> ListByYazar(int yazarId)
        {
            return await _unitOfWork.Habers.ListByYazar(yazarId);
        }

        public void Update(Haber entity)
        {
            _unitOfWork.Habers.Update(entity);
            _unitOfWork.Kayit();
        }
    }
}
