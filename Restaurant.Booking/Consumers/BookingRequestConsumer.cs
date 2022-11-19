using MassTransit;
using Restaurant.Messages.Implements;
using Restaurant.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Messages.MemoryDb;

namespace Restaurant.Booking.Consumers
{
    public class RestaurantBookingRequestConsumer : IConsumer<IBookingRequest>
    {
        private readonly Restaurant.Booking.Models.Restaurant _restaurant;

        private readonly IMemoryRepository<BookingRequestModel> _memoryRepository;

        public RestaurantBookingRequestConsumer(
            Restaurant.Booking.Models.Restaurant restaurant,
            IMemoryRepository<BookingRequestModel> memoryRepository)
        {
            _restaurant = restaurant;
            _memoryRepository = memoryRepository;
        }

        public async Task Consume(ConsumeContext<IBookingRequest> context)
        {
            var model = _memoryRepository.Get().FirstOrDefault(i => i.OrderId == context.Message.OrderId);

            if (model is not null && model.CheckMessageId(context.MessageId.ToString()))
            {
                Console.WriteLine(context.MessageId.ToString());
                Console.WriteLine("Second time");
                return;
            }

            var requestModel = new BookingRequestModel(context.Message.OrderId, context.Message.ClientId,
                context.Message.PreOrder, context.Message.CreationDate, context.MessageId.ToString());

            Console.WriteLine(context.MessageId.ToString());
            Console.WriteLine("First time");
            var resultModel = model?.Update(requestModel, context.MessageId.ToString()) ?? requestModel;
            _memoryRepository.AddOrUpdate(resultModel);

            var result = await _restaurant.BookTableAsync(1);

            await context.Publish<ITableBooked>(new TableBooked(context.Message.OrderId, result ?? false, Dish.Pizza));
        }
    }
}
