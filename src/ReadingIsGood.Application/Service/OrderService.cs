using Microsoft.Extensions.Logging;
using ReadingIsGood.Application.Extensions;
using ReadingIsGood.Common.Enums;
using ReadingIsGood.Common.ExceptionHandling;
using ReadingIsGood.Core.Entities;
using ReadingIsGood.Core.Query;
using ReadingIsGood.Core.Repositories.Abstractions;
using ReadingIsGood.Core.Request;
using ReadingIsGood.Core.Response;
using ReadingIsGood.Core.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadingIsGood.Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRespository orderRespository;
        private readonly IProductRepository productRepository;
        public OrderService(IOrderRespository orderRespository,
            IProductRepository productRepository)
        {
            this.orderRespository = orderRespository;
            this.productRepository = productRepository;
        }

        public async Task<string> CreateOrderAsync(CreateOrderRequest orderRequest)
        {
            Order order = orderRequest.ToOrder();

            foreach (var orderItem in order.OrderItems)
            {
                Product? product = await this.productRepository.GetProductByIdAsync(orderItem.ProductId);

                if (product == null)
                {
                    throw new ReadingIsGoodException($"Product cannot be null", System.Net.HttpStatusCode.BadRequest, logLevel: LogLevel.Warning);
                }

                if (product.Stock <= 0)
                {
                    throw new ReadingIsGoodException($"Stock value is non-tradable value. Stock:{product.Stock}, ProductId:{product.Id}", System.Net.HttpStatusCode.BadRequest, logLevel: LogLevel.Warning);
                }

                if (!await this.productRepository.CheckProductStockAvailablityAsync(product, orderItem.Quantity))
                {
                    throw new ReadingIsGoodException($"there is insufficient stock in the system. TraceId: {orderItem.TraceId}", System.Net.HttpStatusCode.BadRequest, logLevel: LogLevel.Warning);
                }

                await this.productRepository.UpdateProductStockAsync(orderItem.ProductId, orderItem.Quantity, DateTime.UtcNow, true);
            }

            await this.orderRespository.CreateOrderAsync(order);

            return order.Id;
        }

        public async Task<OrderResponse> GetOrderByIdAsync(GetOrderQuery getOrderrequest)
        {
            Order? order = await this.orderRespository.GetOrderByIdAsync(getOrderrequest.OrderId, getOrderrequest.CustomerId);

            if (order == null)
            {
                throw new ReadingIsGoodException($"Order not found: OrderId: {getOrderrequest.OrderId}, CustomerId: {getOrderrequest.CustomerId}", System.Net.HttpStatusCode.NotFound, logLevel: LogLevel.Warning);
            }

            return order.ToOrderResponse();
        }

        public async Task<IReadOnlyList<AllOrderResponse>> GetAllOrderByCustomerAsync(GetAllOrderQuery getAllOrderRequest)
        {
            if (getAllOrderRequest.Limit == null || getAllOrderRequest.Limit <= 0)
            {
                getAllOrderRequest.Limit = 1000;
            }

            if (getAllOrderRequest.Offset == null || getAllOrderRequest.Offset < 0)
            {
                getAllOrderRequest.Offset = 0;
            }

            IReadOnlyList<Order> orders = await this.orderRespository.GetOrdersAsync(getAllOrderRequest.CustomerId, (int)getAllOrderRequest.Limit, (int)getAllOrderRequest.Offset);

            return orders.ToCustomerOrdersResponse();
        }

        public async Task<string> UpdateOrderStatusAsync(OrderStatusUpdateRequest orderStatusUpdateRequest)
        {
            Order? order = await this.orderRespository.GetOrderByIdAsync(orderStatusUpdateRequest.OrderId, orderStatusUpdateRequest.CustomerId);

            if (order == null)
            {
                throw new ReadingIsGoodException($"Order not found: OrderId: {orderStatusUpdateRequest.OrderId}", System.Net.HttpStatusCode.NotFound, logLevel: LogLevel.Warning);
            }

            order.OrderStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), orderStatusUpdateRequest.OrderStatus);

            return order.Id;
        }
    }
}
