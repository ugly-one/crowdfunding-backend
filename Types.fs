module Types 
open System.Collections

    [<Measure>] type DKK

    type InvestError = 
        | InsufficientFunds
        | InvestingTooMuch
        
    type GeneralError = 
        | ProjectNotFound
        | AccountNotFound

    type ResultError = 
        | InvestError of InvestError
        | GeneralError of GeneralError

    type ProjectId = int
    type ProjectName = string

    type Project = {
        Id: ProjectId
        Name: ProjectName
        Goal: decimal<DKK> // perhaps it could have it's own type with some validation to prevent insanly huge goals
        Funded: decimal<DKK>
    }

    type AccountId = int

    type Account = {
        Id : AccountId
        Available: decimal<DKK>
        Invested: decimal<DKK>
    }

    [<CLIMutable>]
    type Deposit = {
        Amount: decimal<DKK>
    }

    type CreateAccount = unit -> Account
    type GetAccount = AccountId -> Result<Account, GeneralError>
    type UpdateAccount = Account -> Result<Account, GeneralError>    

    type CreateProject = ProjectName -> decimal<DKK> -> Project
    type GetProject = ProjectId -> Result<Project, GeneralError>
    type UpdateProject = Project -> Result<Project, GeneralError>