import ast


def validate_code(code):
    try:
        ast.parse(code)
        return True
    except SyntaxError as error:
        print(error)
        return False


def execute_code(template, **kwargs):
    try:
        code = template.format(**kwargs)

        if validate_code(code):
            exec_locals = {}
            exec(code, {}, exec_locals)

            return exec_locals
    except Exception as error:
        print(error)

    return None


template = """
def test_function(x):
    return x {operation} {value}
"""

operation = input("Choose an operation (+, -, *, /): ").strip()
value = input("Provide a value: ").strip()

result = execute_code(template=template, operation=operation, value=value)

if result and "test_function" in result:
    test_function = result["test_function"]
    print(f"Result: {test_function(10)}")
