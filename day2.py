f = open("input2.txt", "r")
valid = 0
valid2 = 0
for x in f:
    [min,max, letter, pw] = x.split(",")
    min = int(min)
    max = int(max)
    count = 0
    count2 = 0
    index = 1
    for c in pw:
        if c == letter:
            count+=1
        if (index == min or index == max) and letter == c:
            count2+=1
        index+=1
    
    if count >= min and count <= max:
        valid+=1
    if count2 == 1:
        valid2+=1  


print(valid)
print(valid2)