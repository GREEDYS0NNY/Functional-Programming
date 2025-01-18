def process_data(data):    
    numbers = list(filter(lambda x: isinstance(x, (int, float)), data))
    max_number = max(numbers, default=None)

    strings = list(filter(lambda x: isinstance(x, str), data))
    longest_string = max(strings, key=len, default=None)

    tuples = list(filter(lambda x: isinstance(x, tuple), data))
    longest_tuple = max(tuples, key=len, default=None)

    return {
        "max_number": max_number,
        "longest_string": longest_string,
        "longest_tuple": longest_tuple
    }


data = [42, "hello", (1, 2, 3), [4, 5, 6], {"key": "value"}, 33, "something to show", (7, 8)]
result = process_data(data=data)

print("Max number:", result["max_number"])
print("Longest string:", result["longest_string"])
print("Longest tuple:", result["longest_tuple"])
