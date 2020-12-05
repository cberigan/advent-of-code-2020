import math
def part1():
    seats = set()
    f = open("input5.txt", "r")
    input = []
    batch = []
    count = 0
    max = -1
    for x in f:
        x = x.strip()
        row = 0
        r = 127
        r0 = 0
        mid = 0
        for i in x[0:7]:
            mid = (r + r0) / 2
            if i == "F":
                r = math.floor(mid)
            if i == "B":
                r0 = math.ceil(mid)
        row = r
        col = 0
        mid = 0
        r = 7
        r0 = 0
        for i in x[-3:]:
            mid = (r + r0) / 2
            if i == "R":
                r0 = math.ceil(mid)
            if i == "L":
                r = math.floor(mid)
        col = r

        seatId = col + row * 8
        seats.add(seatId)
        if seatId > max:
            max = seatId
    
    for i in range(0, max):
        if i not in seats:
            print(i) 


part1()
