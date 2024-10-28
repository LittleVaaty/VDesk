namespace VDesk.Commands.Help;

public class HelpText
{
    public static readonly string UsageText = 
        $"""
         Usage: vdesk [options] [command] [command-options] [arguments]
         
         {ConstantString.VdeskDescription}
         
         Commands:
             create     {ConstantString.CreateDescription}
             get-name   {ConstantString.GetNameDescription}
             get-names  {ConstantString.GetNamesDescription}
             move       {ConstantString.MoveDescription}
             run        {ConstantString.RunDescription}
             set-name   {ConstantString.SetNameDescription}
             switch     {ConstantString.SwitchDescription}
             total      {ConstantString.TotalDescription}
         """;
}