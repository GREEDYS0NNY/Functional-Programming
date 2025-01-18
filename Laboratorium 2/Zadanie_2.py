import numpy as np
import re


def is_valid_operation(operation, matrices):
    try:
        if not re.match(r'^[a-zA-Z0-9_+@().\[\], ]+$', operation):
            return False
        
        local_dict = {key: np.array(value) for key, value in matrices.items()}
        result = eval(operation, {"__builtins__": None}, local_dict)
        
        if isinstance(result, np.ndarray):
            return True
    except Exception:
        return False
    
    return False
    

def execute_matrix_operation(operation, matrices):
    if is_valid_operation(operation, matrices):
        local_dict = {key: np.array(value) for key, value in matrices.items()}
        return eval(operation, {"__builtins__": None}, local_dict)
    else:
        raise ValueError("Incorrect operation or matrices.")


operations = ["A + B", "A @ C", "A.T"]
matrices = {
    "A": [[1, 2], [3, 4]],
    "B": [[5, 6], [7, 8]],
    "C": [[1, 2, 3], [4, 5, 6]]
}

for operation in operations:
    try:
        result = execute_matrix_operation(operation=operation, matrices=matrices)
        print(f"Result of {operation}: {result}")
    except ValueError as error:
        print(error)
