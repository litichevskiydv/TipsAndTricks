// Target - The task you want to start. Runs the Default task if not specified.
var target = Argument("Target", "Default");
// Configuration - The build configuration (Debug/Release) to use.
// 1. If command line parameter parameter passed, use that.
// 2. Otherwise if an Environment variable exists, use that.
var configuration = 
    HasArgument("Configuration") 
        ? Argument<string>("Configuration") 
        : EnvironmentVariable("Configuration") ?? "Release";
 
// A directory path to an Artifacts directory.
var artifactsDirectory = MakeAbsolute(Directory("./artifacts"));
 
// Deletes the contents of the Artifacts folder if it should contain anything from a previous build.
Task("Clean")
    .Does(() =>
    {
        CleanDirectory(artifactsDirectory);
    });
 
// Find all csproj projects and build them using the build configuration specified as an argument.
 Task("Build")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        var projects = GetFiles("../**/*.csproj");
        foreach(var project in projects)
        {
            DotNetCoreBuild(
                project.GetDirectory().FullPath,
                new DotNetCoreBuildSettings()
                {
                    Configuration = configuration
                });
        }
    });
 
// Look under a 'Tests' folder and run dotnet test against all of those projects.
// Then drop the XML test results file in the Artifacts folder at the root.
Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var projects = GetFiles("../test/**/*.csproj");
        foreach(var project in projects)
        {
            DotNetCoreTool(
                project.FullPath,
                "xunit",
                new ProcessArgumentBuilder() 
                    .Append("-configuration " + configuration)
                    .Append("-nobuild")
                    .Append("-xml " + artifactsDirectory.CombineWithFilePath(project.GetFilenameWithoutExtension()).FullPath + ".xml")
                );
        }
    });
 
// The default task to run if none is explicitly specified. In this case, we want
// to run everything starting from Clean, all the way up to Test.
Task("Default")
    .IsDependentOn("Test");
 
// Executes the task specified in the target argument.
RunTarget(target);