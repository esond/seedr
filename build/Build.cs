using System.Collections.Generic;
using System.Linq;
using Hexagrams.Extensions.Common.Serialization;
using Hexagrams.Nuke.Components;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Serilog;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

// ReSharper disable RedundantExtendsListEntry
// ReSharper disable InconsistentNaming

[DotNetVerbosityMapping]
[ShutdownDotNetAfterServerBuild]
partial class Build : NukeBuild,
    IHasGitRepository,
    IHasVersioning,
    IHasArtifacts,
    IRestore,
    IFormat,
    ICompile,
    ITest,
    IReportCoverage,
    IPack,
    IPush
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main() => Execute<Build>(x => ((ICompile) x).Compile);

    protected override void OnBuildInitialized() => Log.Information("{VersionInfo}", GitVersion.ToJson(true));

    [Required]
    [Solution]
    readonly Solution Solution;
    Solution IHasSolution.Solution => Solution;

    [Required]
    [GitVersion(NoFetch = true)]
    readonly GitVersion GitVersion;
    GitVersion IHasVersioning.Versioning => GitVersion;

    public IEnumerable<AbsolutePath> ExcludedFormatPaths => Enumerable.Empty<AbsolutePath>();

    public bool RunFormatAnalyzers => true;

    Target Setup => t => t
        .Before<IClean>()
        .Executes(() =>
        {
            DotNetWorkloadInstall(s => s
                .SetWorkloadId("maui")
                .SetIgnoreFailedSources(true));
        });

    Target ICompile.Compile => t => t
        .Inherit<ICompile>()
        .DependsOn<IFormat>(x => x.VerifyFormat)
        .DependsOn(Setup);

    public Project MonitorProject => Solution.GetAllProjects("Seedr.Monitor").Single();

    public Configure<DotNetPublishSettings> PublishSettings => s => s
        .SetProject(MonitorProject)
        .SetConfiguration(this.FromComponent<IHasConfiguration>().Configuration)
        .SetNoRestore(SucceededTargets.Contains(((IRestore) this).Restore))
        .SetAssemblyVersion(GitVersion.AssemblySemVer)
        .SetFileVersion(GitVersion.AssemblySemFileVer)
        .SetInformationalVersion(GitVersion.InformationalVersion);

    Target PublishWindows => t => t
        .DependsOn(Setup)
        .DependsOn<IRestore>()
        .Executes(() =>
        {
            var framework = "net8.0-windows10.0.19041.0";

            DotNetPublish(s => s
                .Apply(PublishSettings)
                .SetFramework(framework));
        });
    Target PublishIos => t => t
        .DependsOn(Setup)
        .DependsOn<IClean>()
        .DependsOn<IRestore>()
        .Executes(() =>
        {
            DotNetPublish(s => s
                .Apply(PublishSettings)
                .SetFramework("net8.0-ios"));
        });

    bool IReportCoverage.CreateCoverageHtmlReport => true;

    IEnumerable<Project> ITest.TestProjects => Partition.GetCurrent(Solution.GetAllProjects("*.Tests"));
}
