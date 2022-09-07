using System;
using System.Threading.Tasks;

namespace SiteNews.Data.Abstract
{
    public interface IUnitOfWork:IDisposable
    {
        IHaberRepository Habers { get; }
        IKategoriRepository Kategoris { get; }
        IYazarRepository Yazars { get; }
        IVideoRepository Videos { get; }
        ISosyalMedyaRepository SosyalMedya { get; }
        void Kayit();
        Task<int> KayitAsync();
    }
}