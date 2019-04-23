using Otc.ComponentModel.DataAnnotations;

namespace Reply.Manager.Images.TmdbAdapter
{
    public class TmdbAdapterConfiguration
    {
        [Required]
        public string TmdbApiUrlBase { get; set; }
        [Required]
        public string TmdbApiKey { get; set; }
        public int TempoDeCacheDaPesquisaEmSegundos { get; set; } = 20;
    }
}
