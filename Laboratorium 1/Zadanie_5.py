from functools import reduce


def procedural_schedule(tasks):
    tasks.sort(key=lambda x: x[1])

    selected_tasks = []
    total_reward = 0
    last_end_time = 0

    for task in tasks:
        start, end, reward = task
        if start >= last_end_time:
            selected_tasks.append(task)
            total_reward += reward
            last_end_time = end

    return total_reward, selected_tasks


def functional_schedule(tasks):
    sorted_tasks = sorted(tasks, key=lambda x: x[1])

    def select_task(acc, task):
        selected, last_end_time, total_reward = acc
        start, end, reward = task

        if start >= last_end_time:
            return (selected + [task], end, total_reward + reward)

        return acc

    selected_tasks, _, total_reward = reduce(select_task, sorted_tasks, ([], 0, 0))

    return total_reward, selected_tasks


tasks = [(1, 3, 50), (2, 5, 20), (3, 6, 100), (7, 8, 200), (5, 7, 150)]
proc_total_reward, proc_selected_tasks = procedural_schedule(tasks=tasks)
func_total_reward, func_selected_tasks = functional_schedule(tasks=tasks)

print(f"Procedural. Total reward: {proc_total_reward}\nSelected tasks: {proc_selected_tasks}")
print(f"Functional. Total reward: {func_total_reward}\nSelected tasks: {func_selected_tasks}")
