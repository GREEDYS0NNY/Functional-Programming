let exchangeRates = 
    Map.ofList [
        ("USD", 1.0);
        ("EUR", 0.85);
        ("GBP", 0.75);
    ]

let convertAmount amount fromCurrency toCurrency =
    match exchangeRates.TryFind fromCurrency, exchangeRates.TryFind toCurrency with
    | Some(fromRate), Some(toRate) ->
        let baseAmount = amount / fromRate
        let convertedAmount = baseAmount * toRate
        convertedAmount
    | _ -> 
        printfn "Invalid currency."
        0.0

let convertCurrency () =
    printfn "Amount to convert:"
    let amount = System.Console.ReadLine() |> float
    
    printfn "From currency (USD, EUR, GBP):"
    let fromCurrency = System.Console.ReadLine().ToUpper()
    
    printfn "To currency (USD, EUR, GBP):"
    let toCurrency = System.Console.ReadLine().ToUpper()
    
    let result = convertAmount amount fromCurrency toCurrency
    if result > 0.0 then
        printfn "Converted sum: %.2f %s" result toCurrency

convertCurrency()