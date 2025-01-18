type BinaryTree<'a> =
    | Empty
    | Node of 'a * BinaryTree<'a> * BinaryTree<'a>

let rec searchRecursive tree value =
    match tree with
    | Empty -> false
    | Node (v, left, right) ->
        if v = value then true
        else
            searchRecursive left value || searchRecursive right value

let searchIterative tree value =
    let rec loop stack =
        match stack with
        | [] -> false
        | Empty :: rest -> loop rest
        | Node (v, left, right) :: rest ->
            if v = value then true
            else loop (right :: left :: rest)
    
    loop [tree]