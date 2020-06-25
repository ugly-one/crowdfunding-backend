module HttpHelpers
    
    open Microsoft.AspNetCore.Mvc
    open System
    open System.Text.Json

    type ErrorMessage = {
        Error: string
    }

    let intoActionResult result = 
        match result with 
        | Ok x -> ContentResult (Content= JsonSerializer.Serialize(x))
        | Error msg -> ContentResult (Content= JsonSerializer.Serialize<ErrorMessage>({Error= msg}), StatusCode= Nullable 404)

    // TODO this should be in some general module
    let resultMerge r1 r2 = 
        match r2 with 
        | Error msg -> Error msg
        | Ok r2' -> 
            match r1 with
            | Error msg -> Error msg
            | Ok r1' -> Ok(r1',r2')