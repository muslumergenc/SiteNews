using SiteNews.Business.Abstract;
using SiteNews.Data.Abstract;
using SiteNews.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteNews.Business.Concrete
{
    public class SosyalMedyaManger : ISosyalMedyaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SosyalMedyaManger(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(SosyalMedya entity)
        {
            _unitOfWork.SosyalMedya.Create(entity);
            _unitOfWork.Kayit();
        }

        public async Task<SosyalMedya> CreateAsync(SosyalMedya entity)
        {
            await _unitOfWork.SosyalMedya.CreateAsync(entity);
            await _unitOfWork.KayitAsync();
            return entity;
        }

        public void Delete(SosyalMedya entity)
        {
            _unitOfWork.SosyalMedya.Delete(entity);
            _unitOfWork.Kayit();
        }

        public async Task<List<SosyalMedya>> GetAll()
        {
            return await _unitOfWork.SosyalMedya.GetAll();
        }

        public async Task<SosyalMedya> GetById(int id)
        {
            return await _unitOfWork.SosyalMedya.GetById(id);
        }

        public void Update(SosyalMedya entity)
        {
            _unitOfWork.SosyalMedya.Update(entity);
            _unitOfWork.Kayit();
        }
    }
}
