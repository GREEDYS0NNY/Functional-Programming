open System

let findLongestWord (text: string) =
    text.Split([| ' '; '\t'; '\n'; '\r'; '.'; ','; ';'; '!' |], StringSplitOptions.RemoveEmptyEntries)
    |> Array.fold (fun acc word -> if word.Length > fst acc then (word.Length, word) else acc) (0, "")
    
[<EntryPoint>]
let main argv =
    printfn "Enter a text:"
    let inputText = Console.ReadLine()

    let (length, longestWord) = findLongestWord inputText

    if length > 0 then
        printfn "The longest word is: %s" longestWord
        printfn "Length: %d" length
    else
        printfn "No words found."

    0