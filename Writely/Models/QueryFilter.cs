namespace Writely.Models
{
    public class QueryFilter
    {
        public string Tags { get; set; } = "";
        public int Limit { get; set; } = 0;
        public string OrderBy { get; set; } = "date-desc";
    }
}