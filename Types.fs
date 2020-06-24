module Types 
open System.Collections

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