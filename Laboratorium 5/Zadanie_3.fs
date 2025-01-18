let rec permutations lst =
    match lst with
    | [] -> [[]]
    | head :: tail ->
        let tailPerms = permutations tail
        [for perm in tailPerms do
            for i in 0 .. List.length perm do
                yield List.take i perm @ head :: List.skip i perm]

let nums = [1; 2; 3]
let perms = permutations nums
printfn "%A" perms