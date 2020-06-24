namespace crowdfunding_backend.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Types


[<ApiController>]
type AccountController (logger : ILogger<AccountController>, createAccount : CreateAccount) =
    inherit ControllerBase()

    [<HttpGet>]
    [<Route("[controller]")>]
    member __.Get() : string =
        "TODO"

    [<HttpPost>]
    [<Route("create-account")>]
    member __.CreateAccount () : Account = 
        createAccount ()
