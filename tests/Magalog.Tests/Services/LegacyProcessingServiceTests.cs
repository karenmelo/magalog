using Magalog.Application.Services;
using Magalog.Tests.Fixtures;
using Moq;
using Moq.AutoMock;

namespace Magalog.Tests.Services
{
    [Collection(nameof(FileCollection))]
    public class LegacyProcessingServiceTests
    {
        private readonly FileTestFixture _fileTestFixture;

        public LegacyProcessingServiceTests(FileTestFixture fileTestFixture)
        {
            _fileTestFixture = fileTestFixture;
        }

        [Fact]
        public async Task LegacyProcessingFile_ValidFile_ShouldProcessCorrectly()
        {
            //arrange
            var fileContent = _fileTestFixture.GenerateLines(2).ToString();
            var mocker = new AutoMocker();
            var service = mocker.GetMock<LegacyProcessingService>().Object;      

            //act
            await service.ProcessLegacyFile(fileContent);

            //assert
            mocker.GetMock<LegacyProcessingService>().Verify(l => l.ProcessLegacyFile(fileContent), Times.Once);   
        }

    }
}
