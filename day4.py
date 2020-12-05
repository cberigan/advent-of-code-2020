def part1():
    f = open("input4_joe.txt", "r")
    input = []
    batch = []
    count = 0
    for x in f:
        x = x.strip()
        if not x:
            if set(["byr","iyr","eyr","hgt","hcl","pid","ecl"]).issubset(batch):
                count += 1
            batch = []
            continue
        pairs = x.split(" ")
        for pair in pairs:
            [key, val] = pair.split(":")
            batch.append(key)

    print(count)

def bet(v, min, max):
    return min <= int(v) <= max



def part2():
    f = open("input4_joe.txt", "r")
    input = []
    batch = []
    count = 0
    eyes = set([
        "amb",
        "blu",
        "brn",
        "gry",
        "grn",
        "hzl",
        "oth"
    ])
    for x in f:
        x = x.strip()
        if not x:
            validKeys = 0
            for [k, v] in batch:
                if k == "byr" and bet(v, 1920,2002): 
                    validKeys += 1
                if k == "iyr" and bet(v, 2010,2020): 
                    validKeys += 1
                if k == "eyr" and bet(v, 2020,2030): 
                    validKeys += 1
                if k == "hgt":
                    if "cm" in v and bet(v.replace('cm',''), 150,193):
                        validKeys += 1
                    if "in" in v and bet(v.replace('in',''), 59,76):
                        validKeys += 1
                if  k == "hcl" and v[0] == '#':
                    validChars = 0
                    for c in v[1:]:
                        if 48 <= ord(c) <= 57 or 97 <= ord(c) <= 102:
                            validChars += 1
                    if validChars == 6:
                        validKeys +=1
                    
                if  k == "ecl" and v in eyes:
                    validKeys+= 1
                if  k == "pid":
                    validChars = 0
                    for c in v:
                        if 48 <= ord(c) <= 57:
                            validChars += 1
                    if validChars == 9:
                        validKeys +=1
            if validKeys == 7:
                count+=1
            batch = []
            continue
        pairs = x.split(" ")
        for pair in pairs:
            [key, val] = pair.split(":")
            batch.append([key, val])

    print(count)
part1()
part2()
