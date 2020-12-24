def find_subexp_end(exp, start):
    paran = 0
    curr = start
    while True:
        if exp[curr] == '(':
            paran+=1
        elif exp[curr] == ')':
            if paran == 0:
                return curr
            else:
                paran-=1
        curr+=1

def apply_op(value, op, operand):
    if value == None:
        value = operand
    else:
        if op == '+':
            value += operand
        else:
            value *= operand
    return value

def eval_expression1(exp):
    value = None
    op = None
    i = 0
    while i < len(exp):
        if exp[i] == '(':
            sub_start = i + 1
            sub_end = find_subexp_end(exp,sub_start)
            exp_value = eval_expression1(exp[sub_start:sub_end])
            value = apply_op(value,op,exp_value)
            i = sub_end+1
            continue
        if exp[i].isnumeric():
            value = apply_op(value,op,int(exp[i]))
        else:
            op = exp[i]
        i+=1
    return value

def eval_expression2(exp):
    value = None
    multi = []
    i = 0
    while i < len(exp):
        if exp[i] == '(':
            sub_start = i + 1
            sub_end = find_subexp_end(exp,sub_start)
            exp_value = eval_expression2(exp[sub_start:sub_end])
            if value == None:
                value = exp_value
            else:
                value += exp_value
            i = sub_end+1
            continue
        if exp[i].isnumeric():
            if value == None:
                value = int(exp[i])
            else:
                value += int(exp[i])
        elif exp[i] == '*':
            multi.append(value)
            value = None
        i+=1
    multi.append(value)
    prod = 1
    for x in multi:
        prod *= x
    return prod

exp = '((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2'.replace(' ', '')
print(eval_expression2(exp)) 

with open('input18.txt') as f:
    lines = f.readlines()

#part1 
total = 0

for exp in lines:
    exp = exp.replace(' ', '')
    total += eval_expression1(exp)
print(total)   

#part2
total = 0
for exp in lines:
    exp = exp.replace(' ', '')
    total += eval_expression2(exp)
print(total) 