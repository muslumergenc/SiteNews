using SiteNews.Business.Abstract;
using SiteNews.Data.Abstract;
using SiteNews.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteNews.Business.Concrete
{
    public class YazarManager : IYazarService
    {
        readonly private IUnitOfWork _unitOfWork;

        public YazarManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(Yazar entity)
        {
            _unitOfWork.Yazars.Create(entity);
            _unitOfWork.Kayit();
        }

        public async Task<Yazar> CreateAsync(Yazar entity)
        {
            await _unitOfWork.Yazars.CreateAsync(entity);
            await _unitOfWork.KayitAsync();
            return entity;
        }

        public void Delete(Yazar entity)
        {
            _unitOfWork.Yazars.Delete(entity);
            _unitOfWork.Kayit();
        }

        public async Task<List<Yazar>> GetAll()
        {
            return await _unitOfWork.Yazars.GetAll();
        }

        public async Task<Yazar> GetById(int id)
        {
          return  await _unitOfWork.Yazars.GetById(id);
        }

        public void Update(Yazar entity)
        {
            _unitOfWork.Yazars.Update(entity);
            _unitOfWork.Kayit();
        }
    }
}
