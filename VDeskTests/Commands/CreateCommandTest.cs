using AutoFixture;
using McMaster.Extensions.CommandLineUtils;
using VDesk.Commands;
using VDesk.Interop;

namespace VDeskTests.Commands
{
    public class CreateCommandTest : TestingContext<CreateCommand>
    {
        [Fact]
        public void OnExecute_WhenThreeVirtualDesktop_ShouldCallCreateTwoTimes()
        {
            // Arrange
            var guidArray = Fixture.CreateMany<Guid>().ToList();
            GetFakeFor<IVirtualDesktopProvider>().Setup(s => s.GetDesktop()).Returns(guidArray);
            var commandLineApp = new CommandLineApplication();
            ClassUnderTest.Number = 5;

            // Act
            ClassUnderTest.Execute(commandLineApp);

            // Assert
            GetFakeFor<IVirtualDesktopProvider>().Verify(s => s.CreateDesktop(), Times.Exactly(2));
        }

        [Fact]
        public void OnExecute_WhenThreeVirtualDesktop_ShouldNeverCallCreate()
        {
            // Arrange
            var guidArray = Fixture.CreateMany<Guid>().ToList();
            GetFakeFor<IVirtualDesktopProvider>().Setup(s => s.GetDesktop()).Returns(guidArray);
            var commandLineApp = new CommandLineApplication();
            ClassUnderTest.Number = 3;

            // Act
            ClassUnderTest.Execute(commandLineApp);

            // Assert
            GetFakeFor<IVirtualDesktopProvider>().Verify(s => s.CreateDesktop(), Times.Never);
        }
    }
}