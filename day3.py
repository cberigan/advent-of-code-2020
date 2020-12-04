f = open("input3.txt", "r")
map = []
for x in f:
    y = list(x.strip())
    map.append(y)



def count_trees(down, right):
    x = 0
    y = 0
    x_end = len(map)
    y_end = len(map[0])
    count = 0
    while x < x_end:
        if map[x][y] == "#":
            count+=1
        x += down
        y = (y + right) % y_end
    return count
print(count_trees(1,3))
print(count_trees(1,1) * count_trees(1,3) * count_trees(1,5) * count_trees(1,7) *count_trees(2,1))