using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.Dto.Orders;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
        {
            public OrdersClient(IConfiguration config) : base(config, WebApi.Orders) { }

            public IEnumerable<OrderDto> GetUserOrders(string UserName) => Get<List<OrderDto>>($"{_ServiceAdress}/user/{UserName}");

            public OrderDto GetOrderById(int id) => Get<OrderDto>($"{_ServiceAdress}/{id}");

            public OrderDto CreateOrder(CreateOrderModel OrderModel, string UserName) =>
                Post($"{_ServiceAdress}/{UserName}", OrderModel)
                    .Content
                    .ReadAsAsync<OrderDto>()
                    .Result;
        }
}
