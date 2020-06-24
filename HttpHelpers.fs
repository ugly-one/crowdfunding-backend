module HttpHelpers
    
    open Microsoft.AspNetCore.Mvc

    type ErrorMessage = {
        Error: string
    }

    let intoActionResult result = 
        match result with 
        | Ok x -> JsonResult (x) :> IActionResult
        | Error msg -> JsonResult {Error=msg} :> IActionResult

    let resultMerge r1 r2 = 
        match r2 with 
        | Error msg -> Error msg
        | Ok r2' -> 
            match r1 with
            | Error msg -> Error msg
            | Ok r1' -> Ok(r1',r2')