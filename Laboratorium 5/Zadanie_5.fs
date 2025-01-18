let rec quickSort lst =
    match lst with
    | [] | [_] -> lst
    | pivot :: tail ->
        let lessThanPivot, greaterThanPivot = List.partition (fun x -> x < pivot) tail
        (quickSort lessThanPivot) @ (pivot :: quickSort greaterThanPivot)

let quickSortIterative lst =
    let rec loop stack =
        match stack with
        | [] -> []
        | [] :: tail -> loop tail
        | pivot :: tail ->
            let lessThanPivot, greaterThanPivot = List.partition (fun x -> x < pivot) tail
            let stack' = lessThanPivot :: greaterThanPivot :: tail
            pivot :: loop stack'
        
    loop [lst]

let arr = [3; 6; 8; 10; 1; 2; 1]
let sortedArr = quickSort arr
let sortedArrIterative = quickSortIterative arr

printfn "Recursive: %A" sortedArr
printfn "Iterative: %A" sortedArrIterative