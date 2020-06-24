namespace crowdfunding_backend

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.HttpsPolicy;
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Types
open System.Collections

type Startup private () =
    new (configuration: IConfiguration) as this =
        Startup() then
        this.Configuration <- configuration

    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services: IServiceCollection) =
        // Add framework services.
        services.AddControllers() |> ignore

        let accountsInMemory = Dictionary<AccountId, Account>()
        let create = fun () -> InMemoryStorage.createAccount accountsInMemory
        let get = InMemoryStorage.getAccount accountsInMemory
        let update = InMemoryStorage.updateAccount accountsInMemory
        services.AddSingleton<CreateAccount>(create) |> ignore
        services.AddSingleton<GetAccount>(get) |> ignore
        services.AddSingleton<UpdateAccount>(update) |> ignore

        let projectsInMemory = Dictionary<ProjectId, Project>()
        let createProject = InMemoryStorage.createProject projectsInMemory
        let getProject = InMemoryStorage.getProject projectsInMemory
        let updateProject = InMemoryStorage.updateProject projectsInMemory
        services.AddSingleton<CreateProject>(createProject) |> ignore
        services.AddSingleton<GetProject>(getProject) |> ignore
        services.AddSingleton<UpdateProject>(updateProject) |> ignore

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore

        app.UseHttpsRedirection() |> ignore
        app.UseRouting() |> ignore

        app.UseAuthorization() |> ignore

        app.UseEndpoints(fun endpoints ->
            endpoints.MapControllers() |> ignore
            ) |> ignore

    member val Configuration : IConfiguration = null with get, set
