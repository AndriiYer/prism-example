using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Yermolchev.Assignment.Services.Interfaces
{
    public interface ITextParsingService
    {
        event EventHandler<ProgressInfoEventArgs> ParsingProgressChanged;

        Task<Dictionary<string, int>> GetWordsStatistics(string path, CancellationToken token);
    }
}