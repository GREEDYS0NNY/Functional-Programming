open System

let formatEntry (entry: string) =
    let parts = entry.Split([| ';' |], StringSplitOptions.RemoveEmptyEntries)
    if parts.Length = 3 then
        let firstName = parts.[0].Trim()
        let lastName = parts.[1].Trim()
        let age = parts.[2].Trim()
        Some (sprintf "%s, %s (%s years)" lastName firstName age)
    else
        None

let processEntries (entries: string list) =
    entries
    |> List.choose formatEntry
    |> List.iter (printfn "%s")

[<EntryPoint>]
let main argv =
    printfn "Enter multiple entries (format: First name; Last name; Age), one per line."
    printfn "Enter an empty line to finish."

    let rec readLines acc =
        let input = Console.ReadLine()
        if String.IsNullOrWhiteSpace input then acc
        else readLines (input :: acc)

    let entries = readLines [] |> List.rev

    printfn "\nFormatted Output:"
    processEntries entries

    0