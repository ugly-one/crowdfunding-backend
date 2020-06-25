module ResultExtensions
    
    open Microsoft.AspNetCore.Mvc
    open System
    open System.Text.Json
    open Types

    type ErrorMessage = {
        Error: string
    }

    let investErrorToErrorMessage (error: InvestError) = 
        match error with 
        | InsufficientFunds -> ({Error = "Insufficient funds"}, 500)
        | InvestingTooMuch -> ({Error = "Cannot invest that much"}, 500)

    let generalErrorToErrorMessage (error : GeneralError) = 
        match error with 
        | ProjectNotFound -> ({Error = "Project not found"}, 404)
        | AccountNotFound -> ({Error = "Account not found"}, 404)

    let resultIntoContentResult result =
        match result with 
        | Ok x -> ContentResult (Content= JsonSerializer.Serialize(x))
        | Error err -> 
            let (message, statusCode) = 
                match err with 
                | InvestError investError  -> investErrorToErrorMessage investError
                | GeneralError generalError -> generalErrorToErrorMessage generalError
            ContentResult (Content= JsonSerializer.Serialize<ErrorMessage>(message), StatusCode= Nullable statusCode)

    // TODO this should be in some general module
    let resultMerge r1 r2 = 
        match r2 with 
        | Error msg -> Error msg
        | Ok r2' -> 
            match r1 with
            | Error msg -> Error msg
            | Ok r1' -> Ok(r1',r2')

    let liftInvestError f arg1 arg2 = 
        let result = f arg1 arg2
        result |> Result.mapError InvestError

    let liftGeneralError f arg1 = 
        let result = f arg1 
        result |> Result.mapError GeneralError