using FluentAssertions;
using McMaster.Extensions.CommandLineUtils;
using VDesk.Commands;
using VDesk.Interop;
using IConsole = VDesk.Utils.IConsole;

namespace VDeskTests.Commands;

public class TotalCommandTest : TestingContext<TotalCommand>
{
    [Fact]
    public void OnExecute_When3Desktop_ShouldPrint3()
    {
        //Arrange
        var virtualDesktopIds = Fixture.CreateMany<Guid>().ToList();
        GetFakeFor<IVirtualDesktopProvider>().Setup(s => s.GetDesktop()).Returns(virtualDesktopIds);
        GetFakeFor<IConsole>();
        var commandLineApp = new CommandLineApplication();

        //Act
        var result = ClassUnderTest.OnExecute(commandLineApp);

        //Assert
        using (new AssertionScope())
        {
            result.Should().Be(0);
            GetFakeFor<IConsole>().Verify(c => c.WriteLine($"Number of desktopIds: {virtualDesktopIds.Count}"), Times.Once);
        }
    }
}