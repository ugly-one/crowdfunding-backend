namespace crowdfunding_backend.Controllers

open Microsoft.AspNetCore.Mvc
open Types
open ResultExtensions
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
    member __.Create request =
        createProject request.Name request.Goal

    [<HttpGet>]
    [<Route("[Controller]/{id}")>]
    member __.Get id  = 
        getProject id
            |> Result.mapError GeneralError 
            |> resultIntoContentResult

    [<HttpPost>]
    [<Route("[Controller]/{id}/invest")>]
    member __.Invest (id: ProjectId) (request : InvestRequest) =
        let account = getAccount request.AcountId
        let project = getProject id
        let updateAccountProjectInStorage = updateAccountAndProject updateAccount updateProject
        let investAndDeduct = liftInvestError investAndDeduct request.Amount
        let updateStorage = liftGeneralError updateAccountProjectInStorage

        resultMerge account project 
            |> Result.mapError GeneralError
            |> Result.bind investAndDeduct
            |> Result.bind updateStorage
            |> Result.map (fun (_,project) -> project) // skip the account, we want to return only the project
            |> resultIntoContentResult
