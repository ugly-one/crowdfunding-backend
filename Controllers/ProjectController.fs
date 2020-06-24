namespace crowdfunding_backend.Controllers

open Microsoft.AspNetCore.Mvc
open Types

[<CLIMutable>]
type CreateProjectRequest = {
    Name: ProjectName
    Goal: uint32
}

[<ApiController>]
type ProjectController (createProject: CreateProject) =
    inherit ControllerBase()

    [<HttpPost>]
    [<Route("create-project")>]
    member __.Create request : IActionResult =
        JsonResult(createProject request.Name request.Goal) :> IActionResult
