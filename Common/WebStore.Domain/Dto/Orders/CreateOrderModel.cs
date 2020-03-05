using System.Collections.Generic;
using System.Text;
using WebStore.Domain.ViewModels;

namespace WebStore.Domain.Dto.Orders
{
    public class CreateOrderModel
    {
        public OrderViewModel orderViewModel { get; set; }
        public List<OrderItemsDto> orderItems { get; set; }
    }
}
