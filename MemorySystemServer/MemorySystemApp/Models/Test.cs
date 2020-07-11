using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MemorySystem.Infrastructure.AutomapperSettings;

using MemorySystemApp.Data.Models;

namespace MemorySystemApp.Models
{
    public class Test : IMapFrom<Picture>
    {
        public string Description { get; set; }
    }
}
