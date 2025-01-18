open System

let isPalindrome (text: string) =
    let cleanedText = text.Replace(" ", "").ToLower()
    let reversedText = String(Array.rev (cleanedText.ToCharArray()))
    cleanedText = reversedText

[<EntryPoint>]
let main argv =
    printfn "Enter the text:"
    let inputText = Console.ReadLine()

    if isPalindrome inputText then
        printfn "The given text is a palindrome."
    else
        printfn "The given text is not a palindrome."

    0