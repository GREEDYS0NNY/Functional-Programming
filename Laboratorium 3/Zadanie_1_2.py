import json


class Employee:
    def __init__(self, name, age, salary):
        self.name = name
        self.age = age
        self.salary = salary

    def __str__(self):
        return f"Name: {self.name}, Age: {self.age}, Salary: {self.salary}"
    
    def to_dict(self):
        return {"name": self.name, "age": self.age, "salary": self.salary}

    @staticmethod
    def from_dict(data):
        return Employee(data["name"], data["age"], data["salary"])


class EmployeesManager:
    FILE_PATH = "employees.json"

    def __init__(self):
        self.employees = []
        self.load_from_file()
    
    def save_to_file(self):
        with open(self.FILE_PATH, "w", encoding="utf-8") as f:
            json.dump([emp.to_dict() for emp in self.employees], f, ensure_ascii=False, indent=4)

    def load_from_file(self):
        try:
            with open(self.FILE_PATH, "r", encoding="utf-8") as f:
                self.employees = [Employee.from_dict(emp) for emp in json.load(f)]
        except (FileNotFoundError, json.JSONDecodeError):
            self.employees = []

    def add_employee(self, employee):
        self.employees.append(employee)
        self.save_to_file()

    def display_employees(self):
        self.load_from_file()
        for employee in self.employees:
            print(employee)

    def remove_employees_by_age_range(self, min_age, max_age):
        self.employees = [emp for emp in self.employees if not (min_age <= emp.age <= max_age)]
        self.save_to_file()

    def find_employee_by_name(self, name):
        for employee in self.employees:
            if employee.name == name:
                return employee
        return None

    def update_salary_by_name(self, name, new_salary):
        employee = self.find_employee_by_name(name)

        if employee:
            employee.salary = new_salary
            self.save_to_file()
            print(f"Salary for employee {name} has been updated: {new_salary}")
        else:
            print(f"Can not find an employee with name {name}.")


class FrontendManager:
    def __init__(self, manager):
        self.manager = manager

    def login(self):
        username = input("Provide admin name: ")
        password = input("Password: ")
        return username == "admin" and password == "admin"

    def run(self):
        if not self.login():
            print("Invalid admin name or password.")
            return

        while True:
            print("1. Add employee;")
            print("2. Show employees;")
            print("3. Delete an employee from age range;")
            print("4. Update employee's salary;")
            print("5. Exit.")

            choice = input("Choose an action: ")

            match choice:
                case '1':
                    name = input("Provide employee's full name: ")
                    age = int(input("Provide employee's age: "))
                    salary = float(input("Provide employee's salary: "))
                    self.manager.add_employee(Employee(name, age, salary))
                case '2':
                    self.manager.display_employees()
                case '3':
                    min_age = int(input("Minimum age: "))
                    max_age = int(input("Maximum age: "))
                    self.manager.remove_employees_by_age_range(min_age, max_age)
                case '4':
                    name = input("Provide employee's full name: ")
                    new_salary = float(input("New salary: "))
                    self.manager.update_salary_by_name(name, new_salary)
                case '5':
                    break
                case _:
                    print("Incorrect operation.")


manager = EmployeesManager()
frontend_manager = FrontendManager(manager=manager)

frontend_manager.run()
