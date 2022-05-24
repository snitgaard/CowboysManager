using CowboysManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowboysManager.Core.Interfaces
{
    public interface IPlatformRepository<T>
    {
        Platform CreatePlatform(Platform platform);

        IEnumerable<Platform> GetAllPlatformsByUserId(long userid);
        IEnumerable<Platform> GetAllPlatforms();
    }
}
