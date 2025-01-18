let rec fibonacci n =
    if n <= 1 then
        n
    else
        fibonacci (n - 1) + fibonacci (n - 2)

let fibonacciTailRecursive n =
    let rec aux a b n =
        if n = 0 then a
        else aux b (a + b) (n - 1)
    aux 0 1 n