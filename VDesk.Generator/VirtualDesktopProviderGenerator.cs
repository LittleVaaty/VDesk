using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace VDesk.Generator;

[Generator]
public class VirtualDesktopProviderGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
//#if DEBUG
//        if (!Debugger.IsAttached) Debugger.Launch();
//#endif

        var provider = context.SyntaxProvider.ForAttributeWithMetadataName("VDesk.Generator.GeneratedVirtualDesktopProviderAttribute",
            predicate: static (node, _) => node is ClassDeclarationSyntax,
            transform: static (ctx, _) => ctx.TargetSymbol is INamedTypeSymbol classSymbol
            ? new { Syntax = (ClassDeclarationSyntax) ctx.TargetNode, Symbol = classSymbol } : null)
            .Where(m => m is not null);

        var compilation = context.CompilationProvider.Combine(provider.Collect());

        context.RegisterSourceOutput(compilation, (context, data) =>
        {
            foreach (var syntax in data.Right)
            {
                if(syntax is null) continue;

                var hasCreateDesktopMethod = syntax.Syntax.Members.Any(m => m is MethodDeclarationSyntax { Identifier.Text: "CreateDesktop" });
                var hasGetDesktopMethod = syntax.Syntax.Members.Any(m => m is MethodDeclarationSyntax { Identifier.Text: "GetDesktop" });
                var hasMoveToDesktopMethod = syntax.Syntax.Members.Any(m => m is MethodDeclarationSyntax { Identifier.Text: "MoveToDesktop" });
                var hasSwitchToDesktopMethod = syntax.Syntax.Members.Any(m => m is MethodDeclarationSyntax { Identifier.Text: "Switch" });
                var hasSetDesktopNameMethod = syntax.Syntax.Members.Any(m => m is MethodDeclarationSyntax { Identifier.Text: "SetDesktopName" });

                var theCode = GetVirtualDesktopProviderCode(syntax.Symbol.ContainingNamespace.ToDisplayString(), hasCreateDesktopMethod, hasGetDesktopMethod, hasMoveToDesktopMethod, hasSwitchToDesktopMethod, hasSetDesktopNameMethod);

                context.AddSource($"{syntax.Symbol.ToDisplayString()}.cs", theCode);
            }
        });
    }

    private static string GetVirtualDesktopProviderCode(string fullNamespace, bool hasCreateDesktopMethod, bool hasGetDesktopMethod, bool hasMoveToDesktopMethod, bool hasSwitchToDesktopMethod, bool hasSetDesktopNameMethod)
    {
        return $$"""
                                using FluentResults;
                                using System.Runtime.InteropServices;
                                using VDesk.Errors;
                                
                                namespace {{fullNamespace}};

                                public partial class VirtualDesktopProvider 
                                {
                                    private readonly IVirtualDesktopManagerInternal _virtualDesktopManagerInternal;
                                    private readonly IApplicationViewCollection _applicationViewCollection;
                                    private Dictionary<Guid, IVirtualDesktop> _knownDesktops = new();
                                    
                                    private VirtualDesktopProvider(IVirtualDesktopManagerInternal virtualDesktopManagerInternal, IApplicationViewCollection applicationViewCollection)
                                    {
                                        _virtualDesktopManagerInternal = virtualDesktopManagerInternal;
                                        _applicationViewCollection = applicationViewCollection;
                                    }
                                    
                                
                                    public static VirtualDesktopProvider Create()
                                    {
                                        
                                        var resultApplicationViewCollection = CreateInstance<IApplicationViewCollection>(null);
                                        var resultVirtualDesktopManagerInternal = CreateInstance<IVirtualDesktopManagerInternal>(CLSID.VirtualDesktopManagerInternal);
                                    
                                        return new VirtualDesktopProvider(resultVirtualDesktopManagerInternal.Value, resultApplicationViewCollection.Value);
                                    }
                                    
                                    protected static Result<T> CreateInstance<T>(Guid? guidService)
                                    {
                                        try
                                        {
                                            var clsid = CLSID.ImmersiveShell;
                                            var iid = new Guid(SharedCOM.IServiceProvider.IID);
                                            var hr = Ole32.CoCreateInstance(ref clsid, /* No aggregation */ 0, (uint)Ole32.CLSCTX.CLSCTX_LOCAL_SERVER, ref iid, out object comObject);
                                            Marshal.ThrowExceptionForHR(hr);
                                            var serviceProvider = (SharedCOM.IServiceProvider)comObject;
                                            var instance = serviceProvider.QueryService(guidService ?? typeof(T).GUID, typeof(T).GUID);
                                            return Result.Ok((T) instance);
                                        }
                                        catch (Exception)
                                        {
                                            return Result.Fail(new InitializationError(typeof(T)));
                                        }
                                    }
                                    
                                {{(!hasCreateDesktopMethod ? GetCreateDesktopCode() : string.Empty)}}
                                
                                {{(!hasGetDesktopMethod ? GetGetDesktopCode() : string.Empty)}}
                                
                                {{(!hasMoveToDesktopMethod ? GetMoveToDesktopCode() : string.Empty)}}
                                
                                {{(!hasSwitchToDesktopMethod ? GetSwitchToDesktopCode() : string.Empty)}}
                                
                                {{(!hasSetDesktopNameMethod ? GetSetDesktopNameCode() : string.Empty)}}
                                }
                                """;
    }

    private static string GetSetDesktopNameCode()
    {
        return """
                   public void SetDesktopName(Guid virtualDesktopId, string name)
                   {
                       if (_knownDesktops.TryGetValue(virtualDesktopId, out var virtualDesktop))
                       {
                           _virtualDesktopManagerInternal.SetDesktopName(virtualDesktop, name);
                       }
                       else
                       {
                           throw new KeyNotFoundException($"cannot found virtualdesktop with key {virtualDesktopId}");
                       }
                   }
               """;
    }

    private static string GetSwitchToDesktopCode()
    {
        return """
                   public void Switch(Guid virtualDesktopId)
                   {
                       if (_knownDesktops.TryGetValue(virtualDesktopId, out var virtualDesktop))
                       {
                           _virtualDesktopManagerInternal.SwitchDesktop(virtualDesktop);
                       }
                       else
                       {
                           throw new KeyNotFoundException($"cannot found virtualdesktop with key {virtualDesktopId}");
                       }
                   }
               """;
    }

    private static string GetMoveToDesktopCode()
    {
        return """
                   public void MoveToDesktop(IntPtr hWnd, Guid virtualDesktopId)
                   {
                       if (_knownDesktops.TryGetValue(virtualDesktopId, out var virtualDesktop))
                       {
                           var applicationView = _applicationViewCollection.GetViewForHwnd(hWnd);
                           _virtualDesktopManagerInternal.MoveViewToDesktop(applicationView, virtualDesktop);
                       }
                       else
                       {
                           throw new KeyNotFoundException($"cannot found virtualdesktop with key {virtualDesktopId}");
                       }
                   }
               """;
    }

    private static string GetCreateDesktopCode()
    {
        return """
                   public Guid CreateDesktop()
                   {
                       var virtualDesktop = _virtualDesktopManagerInternal
                           .CreateDesktop();
                       _knownDesktops.Add(virtualDesktop.GetID(), virtualDesktop);
                   
                       return virtualDesktop.GetID();
                   }
               """;
    }

    private static string GetGetDesktopCode()
    {
        return """
                   public IList<Guid> GetDesktop()
                   {
                       var array = _virtualDesktopManagerInternal.GetDesktops();
                       if (array == null) new List<Guid>();
                   
                       var count = array.GetCount();
                       var vdType = typeof(IVirtualDesktop);
                   
                       for (var i = 0u; i < count; i++)
                       {
                           var ppvObject = (IVirtualDesktop) array.GetAt(i, vdType.GUID);
                           _knownDesktops.Add(ppvObject.GetID(), ppvObject);
                       }
                   
                       return _knownDesktops.Keys.ToList();
                   }
               """;
    }
}

