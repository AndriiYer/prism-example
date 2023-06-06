using System.IO;
using System.Threading;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;
using Yermolchev.Assignment.Services;
using Yermolchev.Assignment.Services.Interfaces;

namespace Yermolchev.Assignment.Modules.TextTool.Tests.Services
{
    public class FileStreamReaderProviderFixture
    {
        private readonly string _fileName = "fileName";
        private readonly string _sourceString = @"1:1 Adam Seth Enos
1:2 Cainan Adam Seth Iared";

        private readonly Dictionary<string, int> _textParsingServiceDefaultResult = new Dictionary<string, int>
        {
            {"Adam", 2 },
            {"Seth", 2 },
            {"1:1", 1 },
            {"Enos", 1 },
            {"1:2", 1 },
            {"Cainan", 1 },
            {"Iared", 1 }
        };

        private Mock<IStreamReaderProvider> _streamReaderProviderMock;

        public FileStreamReaderProviderFixture()
        {
            _streamReaderProviderMock = new Mock<IStreamReaderProvider>();
            var streamReader = GetSourceStreamReader();
            _streamReaderProviderMock.Setup(x => x.GetStreamReader(_fileName)).Returns(streamReader);
            _streamReaderProviderMock.Setup(x => x.TotallyToProcess).Returns(streamReader.BaseStream.Length);
        }

        [Fact]
        public async void Having_StreamReader_When_ParseText_Then_RetriveExpectedResult()
        {
            //Arrange
            var textParsingService = new TextParsingService(_streamReaderProviderMock.Object);
            var cancellationTockenSource = new CancellationTokenSource();

            //Act
            var result = await textParsingService.GetWordsStatistics(_fileName, cancellationTockenSource.Token);

            //Assert
            result.Should().BeEquivalentTo(_textParsingServiceDefaultResult);
        }

        private StreamReader GetSourceStreamReader()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(_sourceString);
            writer.Flush();
            stream.Position = 0;

            return new StreamReader(stream);
        }
    }
}
