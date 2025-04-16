var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.CrudClientes_ApiService>("apiservice");

builder.AddProject<Projects.CrudClientes_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
