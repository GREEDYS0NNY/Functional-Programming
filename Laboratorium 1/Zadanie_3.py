from functools import reduce


def procedural_schedule_tasks(tasks):
    total_time = 0
    waiting_time = 0
    order = []

    tasks.sort(key=lambda x: x[1])

    for task in tasks:
        order.append(task[0])
        waiting_time += total_time
        total_time += task[1]
    
    return order, waiting_time


def functional_schedule_tasks(tasks):
    sorted_tasks = sorted(tasks, key=lambda x: x[1])
    order = list(map(lambda x: x[0], sorted_tasks))

    waiting_time = reduce(lambda acc, task: (acc[0] + acc[1], acc[1] + task[1]), sorted_tasks, (0, 0))[0]

    return order, waiting_time


tasks = [("Task 1", 3, 50), ("Task 2", 1, 30), ("Task 3", 2, 40)]

proc_order, proc_waiting_time = procedural_schedule_tasks(tasks=tasks)
func_order, func_waiting_time = functional_schedule_tasks(tasks=tasks)

print(f"Procedural. Order: {proc_order}\nWaiting time: {proc_waiting_time}")
print(f"Functional. Order: {func_order}\nWaiting time: {func_waiting_time}")
