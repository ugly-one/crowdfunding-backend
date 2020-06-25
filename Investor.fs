module Investor
    open Types
    open ResultExtensions

    let depositIntoAccount deposit account = 
        {account with Available = account.Available + deposit.Amount}

    let investAndDeduct amount (account,project) = 
        let invest amount project = 
            let missingAmount = project.Goal - project.Funded
            if amount > missingAmount 
            then Error InvestError.InvestingTooMuch 
            else Ok {project with Funded = project.Funded + amount}

        let deductFromAccount amount account = 
            if account.Available < amount then Error InvestError.InsufficientFunds else {account with Available = account.Available - amount} |> Ok

        let project = invest amount project
        let account = deductFromAccount amount account
        resultMerge account project

    let updateAccountAndProject updateAccount updateProject (account,project) : Result<(Account*Project),GeneralError>= 
        // TODO if it was a DB - use transaction to make sure we won't update only one "table"
        let updatedAccount = updateAccount account
        let updatedProject = updateProject project
        resultMerge updatedAccount updatedProject