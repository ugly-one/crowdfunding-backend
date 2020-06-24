namespace crowdfunding_backend.Controllers

open Microsoft.AspNetCore.Mvc
open Types
open HttpHelpers
open Investor

[<CLIMutable>]
type CreateProjectRequest = {
    Name: ProjectName
    Goal: uint32
}

[<CLIMutable>]
type InvestRequest = {
    AcountId: AccountId
    Amount: uint32
}

[<ApiController>]
type ProjectController 
    (createProject: CreateProject, 
    getProject: GetProject, 
    updateProject: UpdateProject,
    getAccount : GetAccount,
    updateAccount: UpdateAccount
    ) =
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

    [<HttpPost>]
    [<Route("[Controller]/{id}/invest")>]
    member __.Invest (id: ProjectId) (request : InvestRequest) : IActionResult =

        let account = getAccount request.AcountId
        let project = getProject id
        let updateAccountProjectInStorage = updateAccountAndProject updateAccount updateProject
        
        resultMerge account project 
                    |> Result.bind (investAndDeduct request.Amount)
                    |> Result.bind updateAccountProjectInStorage
                    |> Result.map (fun (_,project) -> project)
                    |> intoActionResult
