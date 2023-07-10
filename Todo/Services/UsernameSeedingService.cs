using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todo.Configurations;

namespace Todo.Services
{
    public class UsernameSeedingService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly GravatarOptions _gravatarOptions;

        public UsernameSeedingService(IServiceProvider serviceProvider, IOptions<GravatarOptions> gravatarOptions)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _gravatarOptions = gravatarOptions?.Value ?? throw new ArgumentNullException(nameof(gravatarOptions));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await SeedUsernames(stoppingToken);
                await Task.Delay(new TimeSpan(0, _gravatarOptions.SeedingRetryTime, 0), stoppingToken);
            }
        }

        private async Task SeedUsernames(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var gravatarClient = scope.ServiceProvider.GetRequiredService<IGravatarClient>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            foreach (var user in userManager.Users)
            {
                var username = await gravatarClient.GetGravatarUsernameAsync(user.Email, cancellationToken);
                if (username != user.Email)
                {
                    await userManager.SetUserNameAsync(user, username);
                }
            }
        }
    }
}
