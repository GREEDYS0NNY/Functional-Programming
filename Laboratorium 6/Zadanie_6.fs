open System

let replaceWord (text: string) (oldWord: string) (newWord: string) =
    text.Replace(oldWord, newWord)

[<EntryPoint>]
let main argv =
    printfn "Enter the original text:"
    let inputText = Console.ReadLine()

    printfn "Enter the word to search for:"
    let searchWord = Console.ReadLine()

    printfn "Enter the replacement word:"
    let replacementWord = Console.ReadLine()

    let modifiedText = replaceWord inputText searchWord replacementWord

    printfn "\nModified text:"
    printfn "%s" modifiedText

    0