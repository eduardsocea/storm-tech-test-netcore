using System.Threading;
using System.Threading.Tasks;
using Todo.Models.Gravatar;

namespace Todo.Services
{
    public interface IGravatarClient
    {
        public Task<string> GetGravatarUsernameAsync(string email, CancellationToken cancellationToken = default);
    }
}
