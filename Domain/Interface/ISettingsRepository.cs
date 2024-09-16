using Domain.Entities;
using Domain.Models;

namespace Domain.Interface
{
    public interface ISettingsRepository
    {
        Task<SettingsEntity> GetSettingsAsync();
        Task<SettingsEntity> UpdateAsync(SettingsModel settings);
    }
}
