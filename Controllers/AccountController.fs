namespace crowdfunding_backend.Controllers

open Microsoft.AspNetCore.Mvc
open Types
open HttpHelpers

[<ApiController>]
type AccountController 
    (   createAccount : CreateAccount, 
        getAccount : GetAccount,
        updateAccount : UpdateAccount) =
    inherit ControllerBase()

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