open System

[<Sealed>]
type BankAccount(accountNumber: string, initialBalance: decimal) =
    let mutable balance = initialBalance
    
    member val AccountNumber = accountNumber with get
    member this.Balance = balance
    
    member this.Deposit(amount: decimal) =
        if amount > 0m then
            balance <- balance + amount
            printfn "Deposited %M into account %s. New balance: %M" amount this.AccountNumber balance
        else
            printfn "Deposit amount must be positive."
    
    member this.Withdraw(amount: decimal) =
        if amount > 0m && amount <= balance then
            balance <- balance - amount
            printfn "Withdrew %M from account %s. New balance: %M" amount this.AccountNumber balance
        else
            printfn "Invalid withdrawal amount. Insufficient balance or non-positive amount."

[<Sealed>]
type Bank() =
    let mutable accounts = Map.empty<string, BankAccount>
    
    member this.CreateAccount(accountNumber: string, initialBalance: decimal) =
        if Map.containsKey accountNumber accounts then
            printfn "Account with number %s already exists." accountNumber
        else
            let account = BankAccount(accountNumber, initialBalance)
            accounts <- Map.add accountNumber account accounts
            printfn "Created account %s with balance %M." accountNumber initialBalance
    
    member this.GetAccount(accountNumber: string) =
        match Map.tryFind accountNumber accounts with
        | Some account -> account
        | None -> printfn "Account %s not found." accountNumber; null
    
    member this.UpdateAccount(accountNumber: string, newBalance: decimal) =
        match Map.tryFind accountNumber accounts with
        | Some _ ->
            accounts <- Map.add accountNumber (BankAccount(accountNumber, newBalance)) accounts
            printfn "Updated balance of account %s to %M." accountNumber newBalance
        | None -> printfn "Account %s not found." accountNumber
    
    member this.DeleteAccount(accountNumber: string) =
        if Map.containsKey accountNumber accounts then
            accounts <- Map.remove accountNumber accounts
            printfn "Deleted account %s." accountNumber
        else
            printfn "Account %s not found." accountNumber

[<EntryPoint>]
let main argv =
    let bank = Bank()
    
    bank.CreateAccount("12345", 1000m)
    bank.CreateAccount("67890", 500m)
    
    let acc1 = bank.GetAccount("12345")
    if acc1 <> null then acc1.Deposit(200m)
    
    let acc2 = bank.GetAccount("67890")
    if acc2 <> null then acc2.Withdraw(100m)
    
    bank.UpdateAccount("12345", 1500m)
    bank.DeleteAccount("67890")
    
    0