namespace crowdfunding_backend.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Types



[<ApiController>]
type AccountController 
    (   createAccount : CreateAccount, 
        getAccount : GetAccount,
        updateAccount : UpdateAccount) =
    inherit ControllerBase()

    let intoActionResult result = 
        match result with 
        | Ok x -> JsonResult (x) :> IActionResult
        | Error _ -> StatusCodeResult( 404 ) :> IActionResult

    [<HttpGet>]
    [<Route("[controller]/{id}")>]
    member __.Get (id: AccountId) : IActionResult =
        let account = getAccount id
        intoActionResult account

    [<HttpPost>]
    [<Route("create-account")>]
    member __.CreateAccount () : Account = 
        createAccount ()

    [<HttpPost>]
    [<Route("[controller]/{id}/deposit")>]
    member __.Deposit (id: AccountId) (deposit: Deposit) : IActionResult = 
        
        let increaseFunds deposit account = 
            {account with Available = account.Available + deposit.Amount}
        
        let depositIntoAccount = increaseFunds deposit

        getAccount id
            |> Result.map depositIntoAccount 
            |> Result.bind updateAccount
            |> intoActionResult