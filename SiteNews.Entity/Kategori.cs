using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SiteNews.Entity
{
    public class Kategori
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Başlık Zorunludur !")]
        public string Ad { get; set; }
        [Required(ErrorMessage ="Url Zorunludur !")]
        public string Url { get; set; }
        [Required(ErrorMessage = "Herhangi Bir Sıra Numarası Belirtiniz !")]
        public byte Sira { get; set; }
        [MaxLength(150),Required(ErrorMessage ="Max 150 Karakter yazılabilir.")]
        public string SeoDesc { get; set; }
        public List<Haber> Habers { get; set; }
    }
}
