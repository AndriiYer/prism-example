using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Yermolchev.Assignment.Services.Interfaces;

namespace Yermolchev.Assignment.Services
{
    public class TextParsingService : ITextParsingService
    {
        private readonly IStreamReaderProvider _streamReaderProvider;

        public event EventHandler<ProgressInfoEventArgs> ParsingProgressChanged;

        public TextParsingService(IStreamReaderProvider streamReaderProvider)
        {
            _streamReaderProvider = streamReaderProvider;
        }

        protected virtual void OnParsingProgressChanged(long total, long current)
        {
            ParsingProgressChanged?.Invoke(this, new ProgressInfoEventArgs { TotallyToProcess = total, CurrentlyProcessed = current });
        }

        public async Task<Dictionary<string, int>> GetWordsStatistics(string path, CancellationToken token)
        {
            using var reader = _streamReaderProvider.GetStreamReader(path);
            OnParsingProgressChanged(_streamReaderProvider.TotallyToProcess, 0);
            var processed = 0;
            var wordsStatistics = new Dictionary<string, int>();
            var builder = new StringBuilder();
            var code = 0;
            await Task.Run(() =>
            {
                while ((code = reader.Read()) != -1 && !token.IsCancellationRequested)
                {
                    processed++;
                    var symbol = Convert.ToChar(code);
                    if (Char.IsWhiteSpace(symbol) || _streamReaderProvider.TotallyToProcess == processed)
                    {
                        if(_streamReaderProvider.TotallyToProcess == processed)
                        {
                            builder.Append(symbol);
                        }

                        if (builder.Length > 0)
                        {
                            var word = builder.ToString();
                            if (wordsStatistics.ContainsKey(word))
                            {
                                wordsStatistics[word]++;
                            }
                            else
                            {
                                wordsStatistics[word] = 1;
                            }
                        }

                        OnParsingProgressChanged(_streamReaderProvider.TotallyToProcess, processed);
                        builder.Clear();
                    }
                    else
                    {
                        builder.Append(symbol);
                    }
                }

                wordsStatistics = wordsStatistics
                    .OrderByDescending(x => x.Value)
                    .ToDictionary(k => k.Key, v => v.Value);
            });

            return wordsStatistics;
        }
    }
}
