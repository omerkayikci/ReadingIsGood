using ReadingIsGood.Common.Enums;
using ReadingIsGood.Core.Entities;
using ReadingIsGood.Core.Request;
using ReadingIsGood.Core.Response;
using System.Collections.Generic;
using System.Linq;

namespace ReadingIsGood.Application.Extensions
{
    public static class OrderMapperExtensions
    {
        public static OrderResponse ToOrderResponse(this Order response)
        {
            IReadOnlyList<OrderItemResponse> orderItemResponses = response.OrderItems.Select(r => new OrderItemResponse
            {
                TraceId = r.TraceId,
                ProductId = r.ProductId,
                ProductSku = r.ProductSku,
                Quantity = r.Quantity,
                TotalPrice = r.TotalPrice
            }).ToList();

            return new OrderResponse
            {
                CustomerId = response.CustomerId,
                OrderCount = orderItemResponses.Count,
                OrderItems = orderItemResponses,
                OrderNote = response.OrderNote,
                OrderStatus = response.OrderStatus
            };
        }

        public static IReadOnlyList<AllOrderResponse> ToCustomerOrdersResponse(this IReadOnlyList<Order> response)
        {
            return response.Select(r => new AllOrderResponse
            {
                OrderId = r.Id,
                OrderStatus = r.OrderStatus
            }).ToList();
        }

        public static Order ToOrder(this CreateOrderRequest request)
        {
            List<OrderItem> orderItems = request.OrderItems.Select(r => new OrderItem
            {
                ProductId = r.ProductId,
                ProductSku = r.ProductSku,
                Quantity = (int)r.Quantity,
                TotalPrice = r.TotalPrice
            }).ToList();

            return new Order
            {
                OrderItems = orderItems,
                OrderStatus = OrderStatus.Processing,
                CustomerId = request.CustomerId,
                OrderNote = request.OrderNote,
                OrderCount = orderItems.Count
            };
        }
    }
}
