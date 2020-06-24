module Types 
open System.Collections

    type AccountId = int

    type Account = {
        Id : AccountId
        Available: uint32
        Invested: uint32
    }

    type CreateAccount = unit -> Account // TODO return Result

    type GetAccount = AccountId -> Result<Account, string>
        