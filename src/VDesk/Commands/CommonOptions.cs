using System.CommandLine;
using VDesk.Utils;

namespace VDesk.Commands;

internal static class CommonOptions
{
    public static readonly CliOption<string?> NameOptions = new("--name", "-n")
    {
        Description = "Name of the desktop on witch the command is run",
        Required = true
    };

    public static readonly CliOption<int?> IndexOptions = new("--index", "-i")
    {
        Description = "Index of the desktop on witch the command is run (start at 1)",
        Required = true
    };

    public static readonly CliOption<bool?> NoSwitchOption = new("--no-switch", "-s")
    {
        Description = "Don't switch to virtual desktop"
    };


    public static readonly CliOption<HalfSplit?> HalfSplitOption = new("--half-split")
    {
        Description = "Position the window on the specified half of the screen"
    };
}