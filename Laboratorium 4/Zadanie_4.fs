type Account = { AccountNumber: string; Balance: float }

let accounts = ref Map.empty<string, Account>

let createAccount accountNumber initialDeposit =
    let newAccount = { AccountNumber = accountNumber; Balance = initialDeposit }
    accounts := !accounts.Add(accountNumber, newAccount)
    printfn "Account %s successfully created with deposit %.2f" accountNumber initialDeposit

let deposit accountNumber amount =
    match !accounts |> Map.tryFind accountNumber with
    | Some(account) ->
        let updatedAccount = { account with Balance = account.Balance + amount }
        accounts := !accounts.Add(accountNumber, updatedAccount)
        printfn "Deposited to %s in amount of %.2f. Updated balance: %.2f" accountNumber amount updatedAccount.Balance
    | None ->
        printfn "Error: Can not find account with number %s." accountNumber

let withdraw accountNumber amount =
    match !accounts |> Map.tryFind accountNumber with
    | Some(account) ->
        if account.Balance >= amount then
            let updatedAccount = { account with Balance = account.Balance - amount }
            accounts := !accounts.Add(accountNumber, updatedAccount)
            printfn "From account %s withdrawed %.2f. Updated balance: %.2f" accountNumber amount updatedAccount.Balance
        else
            printfn "Error: Not enough balance on account %s." accountNumber
    | None ->
        printfn "Error: Can not find account with number %s." accountNumber

let showBalance accountNumber =
    match !accounts |> Map.tryFind accountNumber with
    | Some(account) -> printfn "Balance on %s: %.2f" accountNumber account.Balance
    | None -> printfn "Error: Can not find account with number %s." accountNumber

let bankApp () =
    let rec menu () =
        printfn "1. Create a new account;"
        printfn "2. Deposit;"
        printfn "3. Withdraw;"
        printfn "4. Show balance;"
        printfn "5. Exit."
        printfn "\nChoose an operation:"
        
        match System.Console.ReadLine() with
        | "1" ->
            printfn "Provide account number:"
            let accountNumber = System.Console.ReadLine()
            printfn "Provide initial deposit:"
            let initialDeposit = System.Console.ReadLine() |> float
            createAccount accountNumber initialDeposit
            menu ()
        | "2" ->
            printfn "Provide account number:"
            let accountNumber = System.Console.ReadLine()
            printfn "Amount to deposit:"
            let amount = System.Console.ReadLine() |> float
            deposit accountNumber amount
            menu ()
        | "3" ->
            printfn "Provide account number:"
            let accountNumber = System.Console.ReadLine()
            printfn "Amount to withdraw:"
            let amount = System.Console.ReadLine() |> float
            withdraw accountNumber amount
            menu ()
        | "4" ->
            printfn "Provide account number:"
            let accountNumber = System.Console.ReadLine()
            showBalance accountNumber
            menu ()
        | "5" -> 
            printfn "Exit..."
        | _ ->
            printfn "Invalid option."
            menu ()
    
    menu ()

bankApp ()