namespace crowdfunding_backend.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Types


[<ApiController>]
type AccountController (logger : ILogger<AccountController>, createAccount : CreateAccount, getAccount : GetAccount) =
    inherit ControllerBase()

    [<HttpGet>]
    [<Route("[controller]/{id}")>]
    member __.Get (id: AccountId) : IActionResult =
        let account = getAccount id
        match account with 
        | Ok acc -> JsonResult (acc) :> IActionResult
        | Error msg -> StatusCodeResult( 404 ) :> IActionResult

    [<HttpPost>]
    [<Route("create-account")>]
    member __.CreateAccount () : Account = 
        createAccount ()
