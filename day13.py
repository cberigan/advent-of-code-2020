from functools import reduce
from sympy.ntheory.modular import crt 
def part1():
    buses = [19,41,37,821,13,17,29,463,23]
    timestamp_start = 1001612
    #buses = [7,13,59,31,19]
    #timestamp_start = 939

    timestamp_end = timestamp_start
    min_bus = 10000000
    next_bus = 0
    while next_bus == 0:
        timestamp_end += 1
        for bus in buses:
            if timestamp_end % bus == 0:
                next_bus = bus
                break
    print(next_bus * (timestamp_end - timestamp_start))\

def part2(input):
    input = input.split(',')
    n= []
    a = []
    for i in range(0, len(input)):
        if input[i] == 'x':
            continue
        n.append(int(input[i]))
        a.append(int(input[i]) - i)
    
    print(crt(n, a))
part1()
part2('19,x,x,x,x,x,x,x,x,41,x,x,x,37,x,x,x,x,x,821,x,x,x,x,x,x,x,x,x,x,x,x,13,x,x,x,17,x,x,x,x,x,x,x,x,x,x,x,29,x,463,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,23')

print(1068781 % 13)