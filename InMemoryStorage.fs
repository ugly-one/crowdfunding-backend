module InMemoryStorage 

    open Types
    open System.Collections.Generic

    let createAccount (inMemory : Dictionary<AccountId,Account>) =
        let lastUsedId = inMemory.Count
        let newAccount = {Id=lastUsedId+1; Available=0u; Invested=0u}
        inMemory.Add(newAccount.Id, newAccount) |> ignore
        newAccount

    let getAccount (inMemory : Dictionary<AccountId,Account>) (id: AccountId) : Result<Account,string> = 
        try 
            inMemory.Item id |> Ok
        with 
        | :? KeyNotFoundException -> "account with given id " + id.ToString() + " not found" |> Error;

    let updateAccount (inMemory : Dictionary<AccountId,Account>) account =
        try
            inMemory.[account.Id] <- account
            Ok account
        with 
        | :? KeyNotFoundException -> "account with given id " + id.ToString() + " not found" |> Error;

    let createProject (inMemory: Dictionary<ProjectId, Project>) name goal = 
        let lastUsedId = inMemory.Count
        let newProject = {Id=lastUsedId+1; Name=name; Goal=goal; Funded=0u}
        inMemory.Add(newProject.Id, newProject)
        newProject
            