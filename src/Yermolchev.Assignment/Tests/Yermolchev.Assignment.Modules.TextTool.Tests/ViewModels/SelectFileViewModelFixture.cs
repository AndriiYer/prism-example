using Moq;
using Prism.Regions;
using Xunit;
using Yermolchev.Assignment.Modules.TextTool.ViewModels;

namespace Yermolchev.Assignment.Modules.TextTool.Tests.ViewModels
{
    /// <summary>
    /// This is an example to show possibility of testing all others existing view models
    /// </summary>
    public class SelectFileViewModelFixture
    {
        private Mock<IRegionManager> _regionManagerMock;

        public SelectFileViewModelFixture()
        {
            _regionManagerMock = new Mock<IRegionManager>();
        }

        [Fact]
        public void Having_FileName_When_Changed_Then_INotifyPropertyChangedCalled()
        {
            //Arrange
            var vm = new SelectFileViewModel(_regionManagerMock.Object);

            //Act
            //Assert
            Assert.PropertyChanged(vm, nameof(vm.FileName), () => vm.FileName = "Changed");
        }
    }
}
