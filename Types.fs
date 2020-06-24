module Types 
open System.Collections

    type ProjectId = int
    type ProjectName = string

    type Project = {
        Id: ProjectId
        Name: ProjectName
        Goal: uint32 // TODO perhaps it could have it's own type with some validation to prevent insanly huge goals
        Funded: uint32 // TODO how to make sure it's not possible to collect more than the goal?
    }

    type AccountId = int

    type Account = {
        Id : AccountId
        Available: uint32
        Invested: uint32
    }

    [<CLIMutable>]
    type Deposit = {
        Amount: uint32
    }

    type CreateAccount = unit -> Account
    type GetAccount = AccountId -> Result<Account, string>
    type UpdateAccount = Account -> Result<Account, string>    

    type CreateProject = ProjectName -> uint32 -> Project
    type GetProject = ProjectId -> Result<Project,string>