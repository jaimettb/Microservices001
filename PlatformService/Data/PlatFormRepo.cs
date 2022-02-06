using System;
using System.Collections.Generic;
using System.Linq;
using PlatformService.Models;

namespace PlatformService.Data{
    public class PlatFormRepo : IPlatformRepo
    {
        private readonly AppDbContext _context;

        public PlatFormRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreatePlatform(Platform plat)
        {
            if(plat == null){
                throw new ArgumentNullException(nameof(plat));
            }

            _context.PlatForms.Add(plat);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.PlatForms.ToList();
        }

        public Platform GetPlatformById(int id)
        {
            return _context.PlatForms.FirstOrDefault(p=> p.Id == id);
        }

        public bool SaveChanges()
        {
            return(_context.SaveChanges() >= 0);
        }
    }
}