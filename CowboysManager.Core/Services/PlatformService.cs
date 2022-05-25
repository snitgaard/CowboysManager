using CowboysManager.Core.Entities;
using CowboysManager.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowboysManager.Core.Services
{
    public class PlatformService : IPlatformService
    {
        private readonly IPlatformRepository<Platform> _platformRepo;

        public PlatformService(IPlatformRepository<Platform> platformRepo)
        {
            _platformRepo = platformRepo;
        }

        public Platform CreatePlatform(Platform platform)
        {
            if (platform == null)
            {
                throw new ArgumentException("Platform is missing");
            }
            if (_platformRepo.GetAllPlatformsByUserId(platform.UserId).FirstOrDefault(p => p.Name == platform.Name) != null)
            {
                throw new InvalidOperationException("This Platform already exists");
            }
            if (!IsValidPlatform(platform))
            {
                throw new InvalidOperationException("Invalid platform property");
            }
            return _platformRepo.CreatePlatform(platform);
        }

        public IEnumerable<Platform> GetAllPlatformsByUserId(long userid)
        {
            return _platformRepo.GetAllPlatformsByUserId(userid);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _platformRepo.GetAllPlatforms();
        }

        public bool IsValidPlatform(Platform platform)
        {
            return !string.IsNullOrEmpty(platform.Name);
        }

    }
}
