using SiteNews.Business.Abstract;
using SiteNews.Data.Abstract;
using SiteNews.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteNews.Business.Concrete
{
    public class VideoManager : IVideoService
    {
        readonly private IUnitOfWork _unitOfWork;

        public VideoManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(Video entity)
        {
            _unitOfWork.Videos.Create(entity);
            _unitOfWork.Kayit();
        }

        public async Task<Video> CreateAsync(Video entity)
        {
            await _unitOfWork.Videos.CreateAsync(entity);
            await _unitOfWork.KayitAsync();
            return entity;
        }

        public void Delete(Video entity)
        {
            _unitOfWork.Videos.Delete(entity);
            _unitOfWork.Kayit();
        }

        public async Task<List<Video>> GetAll()
        {
            return await _unitOfWork.Videos.GetAll();
        }

        public async Task<Video> GetById(int id)
        {
            return await _unitOfWork.Videos.GetById(id);
        }

        public void Update(Video entity)
        {
            _unitOfWork.Videos.Update(entity);
            _unitOfWork.Kayit();
        }
    }
}
