import numpy as np
from functools import reduce


def matrix_operation(matrices, operation):
    if operation not in {"+", "@"}:
        raise ValueError("Only supported operations are available (+, @).")
    
    return reduce(lambda a, b: eval(f"a {operation} b"), matrices)


matrices = [
    np.array([[1, 2], [3, 4]]),
    np.array([[5, 6], [7, 8]]),
    np.array([[9, 10], [11, 12]])
]
    
operation = input("+ or @: ").strip()
    
try:
    result = matrix_operation(matrices=matrices, operation=operation)
    print(f"Result: {result}")
except ValueError as error:
    print(error)