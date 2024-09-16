using AutoMapper;
using Domain.Interface;
using Domain.Models;

namespace Service
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _repository;
        private readonly IMapper _mapper;

        public SettingsService(ISettingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SettingsModel> GetSettings()
        {
            var response = await _repository.GetSettingsAsync();
            return _mapper.Map<SettingsModel>(response);
        }

        public async Task<SettingsModel> UpdateAsync(SettingsModel settings)
        {
            var response = await _repository.UpdateAsync(settings);
            return _mapper.Map<SettingsModel>(response);
        }
    }
}
