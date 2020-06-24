module Investor
    open Types
    open HttpHelpers

    let depositIntoAccount deposit account = 
        {account with Available = account.Available + deposit.Amount}

    /// Invests into given project from given account. 
    /// Invested amount may be smaller than requested - investing is possible only up to the Project's goal.
    let investAndDeduct amount (account,project) = 
        let invest amount (project: Project) = 
            let missingAmount = project.Goal - project.Funded
            let actualInvestment = min missingAmount amount
            ({project with Funded = project.Funded + actualInvestment}, actualInvestment)

        let deductFromAccount amount account = 
            if account.Available < amount then Error "Cannot invest that much" else {account with Available = account.Available - amount} |> Ok

        let (project, amount) = invest amount project
        let result = deductFromAccount amount account
        match result with 
        | Error msg -> Error msg
        | Ok account -> Ok (account, project)

    let updateAccountAndProject updateAccount updateProject (account,project) = 
        // TODO if it was a DB - use transaction to make sure we won't update only one "table"
        let updatedAccount = updateAccount account
        let updatedProject = updateProject project
        resultMerge updatedAccount updatedProject