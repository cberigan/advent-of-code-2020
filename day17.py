from itertools import product
import operator
def run_cycles(dimensions):
    input = '''#.##....
.#.#.##.
###.....
....##.#
#....###
.#.#.#..
.##...##
#..#.###'''
    state = {}
    curr_state = input.splitlines()
    # load state into dictionary
    for y in range(0, len(curr_state)):
        for x in range(0, len(curr_state[0])):
            p = [x,y]
            for _ in range(0, dimensions-len(p)):
                p.append(0)
            state[tuple(p)] = curr_state[y][x]
    
    cycles = 6
    for i in range(0,cycles):
        #create space to view
        for p in list(state):
            for step in product([-1,0,1],repeat=dimensions):
                view = tuple(map(operator.add, p, step))
                if view not in state:
                    state[view] = '.'
        
        state_next = {}
        for p,v in state.items():
            count = 0
            for step in product([-1,0,1],repeat=dimensions):
                test = tuple(map(operator.add, p, step))
                if p == test:
                    continue
                if test not in state:
                    continue
                if state[test] == '#':
                    count+=1
            if v == '#' and (count == 3 or count == 2):
                state_next[p] = '#'
            elif v == '.' and count == 3:
                state_next[p] = '#'
            else:
                state_next[p] = '.'
            
        state = state_next
    print(count_total_active(state))
    
def count_total_active(state):
    total_active = 0
    for p,v in state.items():
        if v == '#':
            total_active+=1    
    return total_active 
            

run_cycles(3)
run_cycles(4)