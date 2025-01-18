open System

let removeDuplicates (words: string list) =
    words |> List.distinct

[<EntryPoint>]
let main argv =
    printfn "Enter words separated by spaces:"
    let inputText = Console.ReadLine()
    
    let wordsList = inputText.Split([| ' ' |], StringSplitOptions.RemoveEmptyEntries) |> Array.toList
    
    let uniqueWords = removeDuplicates wordsList
    
    printfn "Unique words: %A" uniqueWords

    0