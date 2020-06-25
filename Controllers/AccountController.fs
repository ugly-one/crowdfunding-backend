﻿namespace crowdfunding_backend.Controllers

open Microsoft.AspNetCore.Mvc
open Types
open HttpHelpers
open Investor
open System.Text.Json
open Microsoft.FSharp.Linq
open System

[<ApiController>]
type AccountController 
    (   createAccount : CreateAccount, 
        getAccount : GetAccount,
        updateAccount : UpdateAccount) =
    inherit ControllerBase()

    [<HttpGet>]
    [<Route("[controller]/{id}")>]
    member __.Get (id: AccountId) =
        let account = getAccount id
        intoActionResult account

    [<HttpPost>]
    [<Route("create-account")>]
    member __.CreateAccount () : Account = 
        createAccount ()

    [<HttpPost>]
    [<Route("[controller]/{id}/deposit")>]
    member __.Deposit (id: AccountId) (deposit: Deposit)  = 
        
        getAccount id
            |> Result.map (depositIntoAccount deposit)
            |> Result.bind updateAccount
            |> intoActionResult