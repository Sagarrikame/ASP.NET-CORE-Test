namespace QHRMProject.Models
{
    public class Product
    {
        public int SN { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public int Price { get; set; }
    }
}