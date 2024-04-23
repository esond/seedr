using System.Collections.Generic;
using System.Linq;
using Hexagrams.Nuke.Components;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.IO.FileSystemTasks;

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

    [Solution]
    readonly Solution Solution;
    Solution IHasSolution.Solution => Solution;

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

    AbsolutePath MonitorAppArtifactDirectory => this.FromComponent<IHasArtifacts>().ArtifactsDirectory / "monitor-app";

    Target Publish => t => t
        .DependsOn(Setup)
        .DependsOn<IClean>()
        .DependsOn<IRestore>()
        .Produces(MonitorAppArtifactDirectory)
        .Executes(() =>
        {
            var platforms = new[] { "android", "windows10.0.19041.0" };

            var monitorProject = Solution.GetAllProjects("Seedr.Monitor").Single();

            var versioning = this.FromComponent<IHasVersioning>().Versioning;
            var configuration = this.FromComponent<IHasConfiguration>().Configuration;

            var publishSettings = new Configure<DotNetPublishSettings>(s => s
                .SetProject(monitorProject)
                .SetConfiguration(configuration)
                .SetNoRestore(SucceededTargets.Contains(((IRestore) this).Restore))
                .SetAssemblyVersion(versioning.AssemblySemVer)
                .SetFileVersion(versioning.AssemblySemFileVer)
                .SetInformationalVersion(versioning.InformationalVersion));

            DotNetPublish(s => s
                .Apply(publishSettings)
                .CombineWith(platforms, (settings, platform) => settings
                    .SetFramework($"net8.0-{platform}")));

            CopyDirectoryRecursively(monitorProject.Directory / "bin" / configuration,
                MonitorAppArtifactDirectory);
        });

    bool IReportCoverage.CreateCoverageHtmlReport => true;

    IEnumerable<Project> ITest.TestProjects => Partition.GetCurrent(Solution.GetAllProjects("*.Tests"));
}
