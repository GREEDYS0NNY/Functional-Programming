from functools import lru_cache


def procedural_knapsack(items, capacity):
    n = len(items)
    dp = [[0] * (capacity + 1) for _ in range(n + 1)]

    for i in range(1, n + 1):
        weight, value = items[i - 1]
        for w in range(capacity + 1):
            if weight > w:
                dp[i][w] = dp[i - 1][w]
            else:
                dp[i][w] = max(dp[i - 1][w], dp[i - 1][w - weight] + value)

    w = capacity
    selected_items = []

    for i in range(n, 0, -1):
        if dp[i][w] != dp[i - 1][w]:
            selected_items.append(items[i - 1])
            w -= items[i - 1][0]
    
    return dp[n][capacity], selected_items


@lru_cache(None)
def functional_knapsack(index, items, capacity):
    if index == 0 or capacity == 0:
        return 0, []
    
    weight, value = items[index - 1]
    if weight > capacity:
        return functional_knapsack(index - 1, items, capacity)
    
    without_item = functional_knapsack(index - 1, items, capacity)
    with_item_value, with_item_list = functional_knapsack(index - 1, items, capacity - weight)
    with_item_value += value
    
    return max(without_item, (with_item_value, with_item_list + [items[index - 1]]), key=lambda x: x[0])


items = [(2, 3), (3, 4), (2, 5), (3, 8)]
capacity = 5

proc_max_value, proc_selected_items = procedural_knapsack(items=items, capacity=capacity)
func_max_value, func_selected_items = functional_knapsack(index=len(items), items=tuple(items), capacity=capacity)

print(f"Procedural. Maximum value: {proc_max_value}\nSelected items: {proc_selected_items}")
print(f"Functional. Maximum value: {func_max_value}\nSelected items: {func_selected_items}")
