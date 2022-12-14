using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Restaurant.Booking;
using Restaurant.Booking.Consumers;
using Restaurant.Kitchen;
using Restaurant.Kitchen.Consumers;
using Restaurant.Kitchen.Models;
using Restaurant.Messages;
using Restaurant.Messages.Implements;
using Restaurant.Messages.MemoryDb;

namespace Restaurant.Tests;

/// <summary>
/// Базовые тесты саги
/// </summary>
[TestFixture]
public class SagaTests
{
    [OneTimeSetUp]
    public async Task Init()
    {
        _provider = new ServiceCollection()
            .AddMassTransitInMemoryTestHarness(cfg =>
            {
                cfg.AddConsumer<KitchenTableBookedConsumer>();
                cfg.AddConsumer<RestaurantBookingRequestConsumer>();

                cfg.AddSagaStateMachine<RestaurantBookingSaga, RestaurantBooking>()
                    .InMemoryRepository();
                cfg.AddSagaStateMachineTestHarness<RestaurantBookingSaga, RestaurantBooking>();
                cfg.AddDelayedMessageScheduler();
            })
        .AddLogging()
        .AddTransient<Restaurant.Booking.Models.Restaurant>()
            .AddTransient<Manager>()
            .AddSingleton<IMemoryRepository<IBookingRequest>, MemoryRepository<IBookingRequest>>()
            .BuildServiceProvider(true);

        _harness = _provider.GetRequiredService<InMemoryTestHarness>();

        _harness.OnConfigureInMemoryBus += configurator => configurator.UseDelayedMessageScheduler();

        await _harness.Start();
    }

    private ServiceProvider _provider;
    private InMemoryTestHarness _harness;

    [OneTimeTearDown]
    public async Task TearDown()
    {
        await _harness.Stop();
        await _provider.DisposeAsync();
    }

    /// <summary>
    /// Проверить на работостпособность всю цепочку от запроса создать заказ
    /// до получения уведомления о статусе заказа
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task All_job_is_done()
    {
        var orderId = NewId.NextGuid();
        var clientId = NewId.NextGuid();

        await _harness.Bus.Publish(new BookingRequest(orderId,
            clientId,
            null,
            DateTime.Now));

        Assert.That(await _harness.Published.Any<IBookingRequest>());
        Assert.That(await _harness.Consumed.Any<IBookingRequest>());

        var sagaHarness = _provider
            .GetRequiredService<ISagaStateMachineTestHarness<RestaurantBookingSaga, RestaurantBooking>>();

        Assert.That(await sagaHarness.Consumed.Any<IBookingRequest>());
        Assert.That(await sagaHarness.Created.Any(x => x.CorrelationId == orderId));

        var saga = sagaHarness.Created.Contains(orderId);

        Assert.That(saga, Is.Not.Null);
        Assert.That(saga.ClientId, Is.EqualTo(clientId));
        Assert.That(await _harness.Published.Any<ITableBooked>());
        Assert.That(await _harness.Published.Any<IKitchenReady>());
        Assert.That(await _harness.Published.Any<INotify>());
        Assert.That(saga.CurrentState, Is.EqualTo(2));

        await _harness.OutputTimeline(TestContext.Out, options => options.Now().IncludeAddress());
    }
}