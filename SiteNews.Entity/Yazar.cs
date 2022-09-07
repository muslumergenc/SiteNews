using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SiteNews.Entity
{
    public class Yazar
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Yazar Adı Zorunludur.")]
        public string Ad { get; set; }
        public string Foto { get; set; }
        [Required(ErrorMessage ="Tarih Boş Geçilemez")]
        public DateTime Tarih { get; set; }
        public List<Haber> Habers { get; set; }
    }
}
