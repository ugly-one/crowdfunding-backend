namespace crowdfunding_backend.Controllers

open Microsoft.AspNetCore.Mvc
open Types
open HttpHelpers

[<CLIMutable>]
type CreateProjectRequest = {
    Name: ProjectName
    Goal: uint32
}

[<ApiController>]
type ProjectController (createProject: CreateProject, getProject: GetProject) =
    inherit ControllerBase()

    [<HttpPost>]
    [<Route("create-project")>]
    member __.Create request : IActionResult =
        JsonResult(createProject request.Name request.Goal) :> IActionResult

    [<HttpGet>]
    [<Route("[Controller]/{id}")>]
    member __.Get id : IActionResult = 
        let project = getProject id
        intoActionResult project