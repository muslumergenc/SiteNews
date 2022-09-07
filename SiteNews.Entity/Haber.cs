using System;
using System.ComponentModel.DataAnnotations;

namespace SiteNews.Entity
{
    public class Haber
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Başlık Zorunludur")]
        public string Baslik { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "Kısa Açıklama Zorunludur")]
        public string KisaAciklama { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "Açıklama Zorunludur")]
        public string Detay { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "Url Zorunludur")]
        public string Url { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "SeoDescription Zorunludur"),MaxLength(150,ErrorMessage ="Max. 150 karakter.")]
        public string SeoDesc { get; set; }

        [Required(ErrorMessage = "Fotoğraf Zorunludur")]
        public string Foto { get; set; }
        public DateTime Tarih { get; set; }
        public int? KategoriId { get; set; }
        public bool EditorMu { get; set; }
        public bool MansetMi { get; set; }
        public bool EkMansetMi { get; set; }
        public bool PopulerMi { get; set; }
        public bool SonHaberMi { get; set; }
        public bool MakaleMi { get; set; }
        public int Okunma { get; set; }
        public int? YazarId { get; set; }
        public Yazar Yazars { get; set; }
        public Kategori Kategoris { get; set; }
    }
}
