using Domain.Models;

namespace Domain.Interface
{
    public interface ISettingsService
    {
        Task<SettingsModel> GetSettings();
        Task<SettingsModel> UpdateAsync(SettingsModel settings);
    }
}
