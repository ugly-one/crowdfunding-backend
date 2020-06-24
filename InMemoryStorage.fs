module InMemoryStorage 
open System.Collections
open Types

    let createAccount (inMemory : Hashtable) =
        let lastUsedId = inMemory.Count
        let newAccount = {Id=lastUsedId+1; Available=0u; Invested=0u}
        inMemory.Add(newAccount.Id, newAccount) |> ignore
        newAccount