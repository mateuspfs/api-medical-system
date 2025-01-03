namespace SistemaMedico.Utilies
{
    public class PagedResult<T>
    {
        public int TotalPages { get; set; }
        public List<T> Items { get; set; }
    }

}
