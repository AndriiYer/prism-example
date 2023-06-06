using System.IO;
using Yermolchev.Assignment.Services.Interfaces;

namespace Yermolchev.Assignment.Services
{
    public class FileStreamReaderProvider : IStreamReaderProvider
    {
        private Stream _stream;

        public long TotallyToProcess { get => _stream.Length; }

        public StreamReader GetStreamReader(string path)
        {
            _stream = File.OpenRead(path);

            return new StreamReader(_stream);
        }
    }
}
