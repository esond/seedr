using Nuke.Common.CI.GitHubActions;

[GitHubActions(
    "continuous-windows",
    GitHubActionsImage.WindowsLatest,
    FetchDepth = 0,
    OnPullRequestBranches = ["main"],
    OnPushBranches = ["main", "release/v*"],
    PublishArtifacts = true,
    InvokedTargets = [nameof(PublishWindows)],
    CacheKeyFiles = ["global.json", "src/**/*.csproj"])]
[GitHubActions(
    "continuous-ios",
    GitHubActionsImage.MacOsLatest,
    FetchDepth = 0,
    OnPullRequestBranches = ["main"],
    OnPushBranches = ["main", "release/v*"],
    PublishArtifacts = true,
    InvokedTargets = [nameof(PublishIos)],
    CacheKeyFiles = ["global.json", "src/**/*.csproj"])]
partial class Build;
