type LinkedList<'T> =
    | Empty
    | Node of 'T * LinkedList<'T>

module LinkedList =
    let rec fromList lst =
        match lst with
        | [] -> Empty
        | x :: xs -> Node(x, fromList xs)

    let rec printList lst =
        match lst with
        | Empty -> printfn ""
        | Node(x, xs) -> printf "%A " x; printList xs

    let rec sumList lst =
        match lst with
        | Empty -> 0
        | Node(x, xs) -> x + sumList xs

    let rec reverse lst =
        let rec aux acc lst =
            match lst with
            | Empty -> acc
            | Node(x, xs) -> aux (Node(x, acc)) xs
        aux Empty lst

    let rec contains el lst =
        match lst with
        | Empty -> false
        | Node(x, xs) -> x = el || contains el xs

    let rec findIndex el lst index =
        match lst with
        | Empty -> None
        | Node(x, xs) -> if x = el then Some index else findIndex el xs (index + 1)

    let rec countOccurrences el lst =
        match lst with
        | Empty -> 0
        | Node(x, xs) -> (if x = el then 1 else 0) + countOccurrences el xs

    let rec merge lst1 lst2 =
        match lst1 with
        | Empty -> lst2
        | Node(x, xs) -> Node(x, merge xs lst2)

    let compareLists lst1 lst2 =
        let rec aux l1 l2 =
            match l1, l2 with
            | Empty, Empty -> []
            | Node(x, xs), Node(y, ys) -> (x > y) :: aux xs ys
            | _ -> failwith "Lists must be of the same length"
        aux lst1 lst2

    let rec filter predicate lst =
        match lst with
        | Empty -> Empty
        | Node(x, xs) ->
            if predicate x then Node(x, filter predicate xs)
            else filter predicate xs

    let rec removeDuplicates lst =
        let rec aux seen lst =
            match lst with
            | Empty -> Empty
            | Node(x, xs) when Set.contains x seen -> aux seen xs
            | Node(x, xs) -> Node(x, aux (Set.add x seen) xs)
        aux Set.empty lst

    let rec partition predicate lst =
        let rec aux (yes, no) lst =
            match lst with
            | Empty -> (fromList yes, fromList no)
            | Node(x, xs) ->
                if predicate x then aux (x :: yes, no) xs
                else aux (yes, x :: no) xs
        aux ([], []) lst

let rec displayMenu () =
    printfn "0. Exit."
    printfn "1. Create linked list;"
    printfn "2. Print list;"
    printfn "3. Sum list;"
    printfn "4. Reverse list;"
    printfn "5. Check element;"
    printfn "6. Find index;"
    printfn "7. Count occurrences;"
    printfn "8. Merge lists;"
    printfn "9. Compare lists;"
    printfn "10. Filter list;"
    printfn "11. Remove duplicates;"
    printfn "12. Partition list;"
    printfn "Choose an operation:"
    System.Console.ReadLine() |> int

let rec mainLoop userList =
    match displayMenu () with
    | 1 ->
        printf "Enter list elements separated by spaces: "
        let lst = System.Console.ReadLine().Split(' ') |> Array.map int |> Array.toList
        let userList = LinkedList.fromList lst
        mainLoop userList
    | 2 -> LinkedList.printList userList; mainLoop userList
    | 3 -> printfn "Sum: %d" (LinkedList.sumList userList); mainLoop userList
    | 4 -> let userList = LinkedList.reverse userList in mainLoop userList
    | 5 ->
        printf "Enter element to check: "
        let el = System.Console.ReadLine() |> int
        printfn "%b" (LinkedList.contains el userList)
        mainLoop userList
    | 6 ->
        printf "Enter element to find index: "
        let el = System.Console.ReadLine() |> int
        match LinkedList.findIndex el userList 0 with
        | Some idx -> printfn "Index: %d" idx
        | None -> printfn "Element not found"
        mainLoop userList
    | 7 ->
        printf "Enter element to count occurrences: "
        let el = System.Console.ReadLine() |> int
        printfn "Count: %d" (LinkedList.countOccurrences el userList)
        mainLoop userList
    | 8 ->
        printf "Enter second list elements: "
        let lst2 = System.Console.ReadLine().Split(' ') |> Array.map int |> Array.toList
        let merged = LinkedList.merge userList (LinkedList.fromList lst2)
        LinkedList.printList merged
        mainLoop userList
    | 9 ->
        printf "Enter second list: "
        let lst2 = System.Console.ReadLine().Split(' ') |> Array.map int |> Array.toList
        try
            let results = LinkedList.compareLists userList (LinkedList.fromList lst2)
            printfn "%A" results
        with
        | ex -> printfn "%s" (ex.Message)
        mainLoop userList
    | 10 ->
        printf "Filter elements greater than: "
        let threshold = System.Console.ReadLine() |> int
        let filtered = LinkedList.filter (fun x -> x > threshold) userList
        LinkedList.printList filtered
        mainLoop userList
    | 11 ->
        let unique = LinkedList.removeDuplicates userList
        LinkedList.printList unique
        mainLoop userList
    | 12 ->
        printf "Partition elements greater than: "
        let threshold = System.Console.ReadLine() |> int
        let yes, no = LinkedList.partition (fun x -> x > threshold) userList
        printf "Matching: "; LinkedList.printList yes
        printf "Remaining: "; LinkedList.printList no
        mainLoop userList
    | 0 -> printfn "Goodbye!"
    | _ -> printfn "Invalid choice."; mainLoop userList

[<EntryPoint>]
let main _ =
    mainLoop Empty
    0