
from itertools import product



def get_combinations(rule_lookup, rule_num):
    if rule_lookup[rule_num] == '"a"':
        return ['a']
    if rule_lookup[rule_num] == '"b"':
        return ['b']
    or_rules = rule_lookup[rule_num].split(' | ')
    ors = []
    for or_rule in or_rules:
        ands = []
        for sub_rule in or_rule.split(' '):
            ands.append(get_combinations(rule_lookup, sub_rule))
        for x in product(*ands):
            option = ''
            for i in x:
                option += i
            ors.append(option)
    return ors

def build_rules_map(rules):
    rule_lookup = {}
    for rule in rules:
        [id, exp] = rule.split(':')
        rule_lookup[id] = exp.strip()
    return rule_lookup

def part1():
    file = open("input19.txt")

    input = file.read()

    [rules,messages] = input.split('\n\n')
    rules = rules.splitlines()
    messages = messages.splitlines()
    rule_lookup = build_rules_map(rules)

    # part 1
    combinations = get_combinations(rule_lookup,'0')
    count = 0
    for message in messages:
        if message in combinations:
            count += 1
    print(count)


    #part 2
    rule_lookup['8'] = '42 | 42 8'
    rule_lookup['11'] = '42 31 | 42 11 31'
    
    combinations = get_combinations(rule_lookup,'0')
    count = 0
    for message in messages:
        if message in combinations:
            count += 1
    print(count)
    print()
    

    
    

    file.close()



part1()