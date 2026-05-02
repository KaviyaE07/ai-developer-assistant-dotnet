using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.DeveloperAssistant.Application.Interfaces
{
    public interface IAIProvider
    {
        Task<string> GetResponseAsync(string prompt);
    }
}
