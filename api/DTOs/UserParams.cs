namespace api.DTOs
{
public class UserParams
    {
        public int Offset {get; set;}
        public int PageSize {get; set;}
        public string FilterBy {get; set;}
        public string SortBy {get; set;}
        public string SortOrder {get; set;}
    }
}