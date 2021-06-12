namespace ViewModels
{
    public class ViewDataRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; } = 1;
        public int TotalCount { get; set; }
    }
}
