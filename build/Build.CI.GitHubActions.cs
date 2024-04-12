using Hexagrams.Nuke.Components;
using Nuke.Common.CI.GitHubActions;

[GitHubActions(
    "continuous",
    GitHubActionsImage.WindowsLatest,
    GitHubActionsImage.UbuntuLatest,
    GitHubActionsImage.MacOsLatest,
    FetchDepth = 0,
    OnPullRequestBranches = ["main"],
    OnPushBranches = ["main", "release/v*"],
    PublishArtifacts = true,
    InvokedTargets = [nameof(ITest.Test)],
    CacheKeyFiles = ["global.json", "src/**/*.csproj"])]
partial class Build
{
}
