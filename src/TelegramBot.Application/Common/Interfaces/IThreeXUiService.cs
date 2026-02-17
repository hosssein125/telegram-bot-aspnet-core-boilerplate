using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TelegramBot.Application.Common.Models.ThreeXUi;

namespace TelegramBot.Application.Common.Interfaces
{
    public interface IThreeXUiService
    {
        Task<bool> LoginAsync(string username, string password, CancellationToken cancellationToken = default);
        Task<List<InboundDto>> GetInboundsAsync(CancellationToken cancellationToken = default);
        Task<InboundDto?> GetInboundAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> AddInboundAsync(InboundDto inbound, CancellationToken cancellationToken = default);
        Task<bool> UpdateInboundAsync(int id, InboundDto inbound, CancellationToken cancellationToken = default);
        Task<bool> DeleteInboundAsync(int id, CancellationToken cancellationToken = default);
    }
}
