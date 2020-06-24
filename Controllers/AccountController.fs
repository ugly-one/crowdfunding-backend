namespace crowdfunding_backend.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging

[<ApiController>]
[<Route("[controller]")>]
type AccountController (logger : ILogger<AccountController>) =
    inherit ControllerBase()

    [<HttpGet>]
    member __.Get() : string =
        "TODO"
