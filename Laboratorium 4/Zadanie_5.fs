type Board = string list

let displayBoard (board: Board) =
    printfn "\n%s | %s | %s" board.[0] board.[1] board.[2]
    printfn "---------"
    printfn "%s | %s | %s" board.[3] board.[4] board.[5]
    printfn "---------"
    printfn "%s | %s | %s" board.[6] board.[7] board.[8]
    printfn ""

let checkWinner (board: Board) =
    let winningCombinations = [
        [0; 1; 2]; [3; 4; 5]; [6; 7; 8];
        [0; 3; 6]; [1; 4; 7]; [2; 5; 8];
        [0; 4; 8]; [2; 4; 6]
    ]

    match winningCombinations |> List.tryFind (fun [a; b; c] -> 
        board.[a] = board.[b] && board.[b] = board.[c] && board.[a] <> " ") with
    | Some [a; b; c] -> Some board.[a]
    | None ->
        if board |> List.contains " " then
            None
        else
            Some "Draw"

let computerMove (board: Board) =
    let availablePositions = board |> List.mapi (fun i x -> if x = " " then Some i else None) |> List.choose id
    let random = System.Random()
    let move = availablePositions.[random.Next(availablePositions.Length)]
    move

let playerMove (board: Board) =
    printfn "Enter your move (1-9):"
    let position = 
        let input = System.Console.ReadLine() |> int
        if input >= 1 && input <= 9 && board.[input - 1] = " " then
            input - 1
        else
            printfn "Invalid move! Try again."
            playerMove board
    position

let makeMove (board: Board) position symbol =
    board |> List.mapi (fun i x -> if i = position then symbol else x)

let rec playGame (board: Board) turn =
    displayBoard board
    match checkWinner board with
    | Some "X" -> printfn "You win!"; displayBoard board
    | Some "O" -> printfn "Computer wins!"; displayBoard board
    | Some "Draw" -> printfn "It's a draw!"; displayBoard board
    | None ->
        if turn % 2 = 0 then
            let position = playerMove board
            let newBoard = makeMove board position "X"
            playGame newBoard (turn + 1)
        else
            let position = computerMove board
            printfn "Computer chooses position %d" (position + 1)
            let newBoard = makeMove board position "O"
            playGame newBoard (turn + 1)

let startGame () =
    let initialBoard = [" "; " "; " "; " "; " "; " "; " "; " "; " "]
    playGame initialBoard 0

startGame ()