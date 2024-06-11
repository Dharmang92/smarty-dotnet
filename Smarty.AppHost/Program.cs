var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.Smarty_ApiService>("apiservice");

builder.AddProject<Projects.Smarty_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
