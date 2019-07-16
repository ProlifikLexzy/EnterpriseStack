using System;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Shared;
using MyApp.Shared.Context;
using MyApp.Shared.EF;
using MyApp.Shared.EF.Repository;
using MyApp.Shared.EF.Services;
using MyApp.Shared.Net.WorkerService;
using MyApp.Shared.PubSub;
using MyApp.Shared.PubSub.KafkaImpl;

namespace MyApp.App
{
    public partial class Startup
    {
        public void ConfigureDIService(IServiceCollection services)
        {
                   services.AddSingleton<IConsumerClient<BusMessage>>(service =>
            {
                var topics = Configuration.GetSection("Kafka").GetValue<string>("topics").ToString().Split(",");
                var env = service.GetRequiredService<IHostingEnvironment>();
                var consumerClient = new ConsumerClient<BusMessage>(env, Configuration);
                consumerClient.Subscribe(topics.ToList());
                return consumerClient;
            });


            services.AddSingleton<BoundedMessageChannel<BusMessage>>();
            services.AddHostedService<EvenHubProcessorService>();
            services.AddHostedService<EventHubReaderService>();
            services.AddScoped<IDbContext, MyAppDbContext>();
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IRepository<>), typeof(EntityRepository<>));
        }
    }
}
