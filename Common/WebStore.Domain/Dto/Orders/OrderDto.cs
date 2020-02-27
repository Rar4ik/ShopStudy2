using System;
using System.Collections.Generic;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Dto.Orders
{
    public class OrderDto:NamedEntity
    {
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<OrderItemsDto> OrderItems { get; set; }
    }
}