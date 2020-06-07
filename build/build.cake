
Task("Clean")
    .Does(() =>
{
    DotNetCoreClean("../Preludio.sln");
});

Task("Build")
    .Does(() =>
{
    DotNetCoreBuild("../Preludio.sln");
});

Task("Pack")
    .Does(() =>
{
    var settings = new DotNetCorePackSettings
    {
         Configuration = "Release",
         OutputDirectory = @"C:\local-nuget"
    };

     DotNetCorePack("../Preludio.sln", settings);
});

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Build")
    .IsDependentOn("Pack")
    ;

RunTarget("Default");