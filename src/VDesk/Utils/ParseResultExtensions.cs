using System.CommandLine;

namespace VDesk.Utils;

public static class ParseResultExtensions
{
    public static bool HasOption(this ParseResult parseResult, CliOption option) =>
        parseResult.GetResult(option) is not null;
}