using FastFlat.Models;

namespace FastFlat.ViewModels
{
    public class SearchViewModel
    {
        public string Contry { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int NoOfBeds { get; set; }
    }

}
