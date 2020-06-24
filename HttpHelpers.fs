module HttpHelpers
    
    open Microsoft.AspNetCore.Mvc
    
    let intoActionResult result = 
        match result with 
        | Ok x -> JsonResult (x) :> IActionResult
        | Error _ -> StatusCodeResult( 404 ) :> IActionResult