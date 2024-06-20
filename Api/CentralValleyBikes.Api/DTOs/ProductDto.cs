namespace CentralValleyBikes.Api.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public BrandDto Brand { get; set; }
        public CategoryDto Category { get; set; }
        public int ModelYear { get; set; }
        public decimal ListPrice { get; set; }
    }
}
