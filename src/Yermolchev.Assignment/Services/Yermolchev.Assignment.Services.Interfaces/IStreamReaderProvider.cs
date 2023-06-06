using System.IO;

namespace Yermolchev.Assignment.Services.Interfaces
{
    public interface IStreamReaderProvider
    {
        long TotallyToProcess { get; }

        StreamReader GetStreamReader(string uri);
    }
}
