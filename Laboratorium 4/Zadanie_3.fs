let countWords text =
    let words = text.Split([|' '; '\n'; '\t'; ','; '.'; ';'; ':'; '!'|], System.StringSplitOptions.RemoveEmptyEntries)
    words.Length

let countCharactersWithoutSpaces text =
    text.Replace(" ", "").Length

let mostFrequentWord text =
    let words = text.Split([|' '; '\n'; '\t'; ','; '.'; ';'; ':'; '!'|], System.StringSplitOptions.RemoveEmptyEntries)
    let wordCounts = 
        words
        |> Array.fold (fun acc word -> 
            let wordLower = word.ToLower()
            if acc.ContainsKey(wordLower) then
                acc.[wordLower] <- acc.[wordLower] + 1
            else
                acc.Add(wordLower, 1)
            acc) (System.Collections.Generic.Dictionary<string, int>())
    let maxCount = wordCounts.Values |> Seq.max
    let mostFrequentWords = wordCounts |> Seq.filter (fun kv -> kv.Value = maxCount) |> Seq.map fst
    mostFrequentWords |> Seq.toList

let analyzeText () =
    printfn "Text:"
    let text = System.Console.ReadLine()
    
    let wordCount = countWords text
    printfn "Words count: %d" wordCount

    let charCount = countCharactersWithoutSpaces text
    printfn "Chars count: %d" charCount

    let frequentWords = mostFrequentWord text
    printfn "Most frequent words: %A" frequentWords

analyzeText()