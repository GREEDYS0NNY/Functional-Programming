open System

type UserData = {
    WeightKg: float
    HeightCm: float
}

let calculateBMI (user: UserData) =
    let heightM = user.HeightCm / 100.0
    user.WeightKg / (heightM * heightM)

let categorizeBMI bmi =
    match bmi with
    | bmi when bmi < 18.5 -> "Underweight"
    | bmi when bmi >= 18.5 && bmi < 24.9 -> "Normal"
    | bmi when bmi >= 25.0 && bmi < 29.9 -> "Overweight"
    | _ -> "Obesity"

[<EntryPoint>]
let main argv =
    printf "Your weight (kg): "
    let weight = Console.ReadLine() |> float
    
    printf "Your height (cm): "
    let height = Console.ReadLine() |> float
    
    let user = { WeightKg = weight; HeightCm = height }
    let bmi = calculateBMI user
    let category = categorizeBMI bmi
    
    printfn "Your BMI: %.2f" bmi
    printfn "Category: %s" category
    
    0