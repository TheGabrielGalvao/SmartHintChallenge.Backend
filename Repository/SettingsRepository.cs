using Database;
using Domain.Entities;
using Domain.Interface;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Repository
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly AppDbContext _context;

        public SettingsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SettingsEntity> GetSettingsAsync()
        {
            var settings = await _context.Settings.FirstOrDefaultAsync();
            return settings;
        }

        public async Task<SettingsEntity> UpdateAsync(SettingsModel settings)
        {
            var updatedSettings = await _context.Settings.FirstOrDefaultAsync();

            updatedSettings.StateRegistrationForIndividual = settings.StateRegistrationForIndividual;

            _context.Settings.Update(updatedSettings);
            
            await _context.SaveChangesAsync();

            return updatedSettings;
        }
    }
}
