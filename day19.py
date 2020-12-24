
from itertools import product
import regex



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
    print(part2(rule_lookup, messages))
    
def use_regex_rules(rule_lookup):
    for rule_id in rule_lookup:
        r = rule_lookup[rule_id]
        r = regex.sub(r'\s*(\d+)\s*',r'(?&r\1)',r)
        r = regex.sub(r'"(\w+)"',r'\1',r)
        yield "(?P<r{}>{})".format(rule_id,r)

def part2(rule_lookup, messages):
    r=regex.compile(r'(?(DEFINE){})^(?&r0)$'.format(''.join(use_regex_rules(rule_lookup))))
    return sum(1 if r.match(m) else 0 for m in messages)
    

    file.close()



part1()