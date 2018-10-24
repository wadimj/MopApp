using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class TemperatureRepository : ITemperatureRepository, IDisposable
    {
        private TemperatureContext _context;
        private bool _disposed = false;

        public TemperatureRepository(TemperatureContext context)
        {
            _context = context;
        }

        public IEnumerable<Temperature> GetTemperatures()
        {
            return _context.Temperatures.ToList();
        }

        public Temperature GetTemperatureById(int temperatureId)
        {
            return _context.Temperatures.Find(temperatureId);
        }

        public void InsertTemperature(Temperature temperature)
        {
            _context.Temperatures.Add(temperature);
        }

        public void DeleteTemperature(int temperatureId)
        {
            var temperature = _context.Temperatures.Find(temperatureId);
            _context.Temperatures.Remove(temperature);
        }

        public void UpdateTemperature(Temperature temperature)
        {
            _context.Entry(temperature).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}