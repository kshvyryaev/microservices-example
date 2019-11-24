using Micro.Common.Events;
using Micro.Common.Services;

namespace Micro.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .SubscribeToEvent<UserCreated>()
                .SubscribeToEvent<UserAuthenticated>()
                .SubscribeToEvent<ActivityCreated>()
                .Build()
                .Run();
        }
    }
}
