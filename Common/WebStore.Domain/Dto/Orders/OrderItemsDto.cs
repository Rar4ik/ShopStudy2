using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Dto.Orders
{
    public class OrderItemsDto : BaseEntity
    {
        public decimal Price { get; set; }
        public  int Quantity { get; set; }
    }
}