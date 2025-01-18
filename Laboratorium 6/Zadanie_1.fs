open System

let countWords (text: string) =
    text.Split([| ' '; '\t'; '\n'; '\r' |], StringSplitOptions.RemoveEmptyEntries)
    |> Array.length

let countCharactersWithoutSpaces (text: string) =
    text.Replace(" ", "").Length

[<EntryPoint>]
let main argv =
    printfn "Enter the text:"
    let inputText = Console.ReadLine()

    let wordCount = countWords inputText
    let charCount = countCharactersWithoutSpaces inputText

    printfn "Number of words: %d" wordCount
    printfn "Number of characters (without spaces): %d" charCount

    0