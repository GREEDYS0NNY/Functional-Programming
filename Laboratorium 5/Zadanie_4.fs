let rec hanoi n source target aux =
    if n = 1 then
        printfn "Move disk 1 from %s to %s" source target
    else
        hanoi (n - 1) source aux target
        printfn "Move disk %d from %s to %s" n source target
        hanoi (n - 1) aux target source

let hanoiIterative n source target aux =
    let totalMoves = 1 <<< n
    let mutable pegs = List.init 3 (fun _ -> [])
    
    pegs.[0] <- [1..n] |> List.rev
    
    for move in 1 .. totalMoves - 1 do
        let from, toMove = 
            if (move &&& move - 1) % 3 = 0 then (0, 2)
            elif (move &&& move - 1) % 3 = 1 then (2, 1)
            else (1, 0)
            
        let disk = List.head pegs.[from]
        pegs.[from] <- List.tail pegs.[from]
        pegs.[toMove] <- disk :: pegs.[toMove]
        printfn "Move disk %d from peg %s to peg %s" disk
            (if from = 0 then source elif from = 1 then aux else target)
            (if toMove = 0 then source elif toMove = 1 then aux else target)

hanoi 3 "A" "C" "B"
hanoiIterative 3 "A" "C" "B"