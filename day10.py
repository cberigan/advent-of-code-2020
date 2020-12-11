from collections import defaultdict 
with open('untitled13.txt') as f:
    lis = [int(line.strip()) for line in f.readlines()]
# print(sorted(ws))
ones = 0
threes = 0
lis.sort()
lis.append(lis[-1] + 3)
lis.insert(0,0)
print(lis)
for i, x in enumerate(lis):
#     print(lis[i])
#     print(i)
    if lis[i + 1] - lis[i] == 1:
#         print(lis[i + 1], lis[i])
#         print(x, i)
        ones += 1
#         print(ones)
    if lis[i + 1] - lis[i] == 3:
#         print(lis[i + 1], lis[i])
#         print(x, i)
        threes += 1
#         print(threes)
    if lis[i] == lis[-2]:
        break
    i += 1
print(ones * threes)

counter = defaultdict(int)
counter[0] = 1
for i in range(1, len(lis)):
    for j in range(i)[::-1]:
        if lis[i] - lis[j] > 3:
            continue
        print(i,j)
#         print(counter[j])
        print(counter[j])
        counter[i] = counter[i] + counter[j]
# print(counter)
print(counter[i])