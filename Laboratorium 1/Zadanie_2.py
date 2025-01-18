from collections import deque


def bfs_shortest_path(graph, start, end):
    queue = deque([(start, [start])])
    visited = set()

    while queue:
        node, path = queue.popleft()

        if node == end:
            return path

        visited.add(node)

        neighbors = filter(lambda n: n not in visited, graph.get(node, []))
        queue.extend(map(lambda n: (n, path + [n]), neighbors))
    
    return None


graph = {
    'A': ['B', 'C'],
    'B': ['A', 'D', 'E'],
    'C': ['A', 'F'],
    'D': ['B'],
    'E': ['B', 'F'],
    'F': ['C', 'E']
}

start, end = 'A', 'E'
path = bfs_shortest_path(graph=graph, start=start, end=end)

print(f"Shortest path from {start} to {end}: {path}.")
