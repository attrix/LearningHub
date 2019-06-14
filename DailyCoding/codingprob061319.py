# Given an array of integers, return a new array such that each element at index i of the new array is the product of all the numbers in the original array except the one at i.

# For example, if our input was [1, 2, 3, 4, 5], the expected output would be [120, 60, 40, 30, 24]. If our input was [3, 2, 1], the expected output would be [2, 3, 6].

# Follow-up: what if you can't use division?

inplist = []
inp = int(input())

while inp != -1:
    inplist.append(inp)
    inp = -1
    inp = int(input())

res = 1
for i in inplist:
    res = res * i

listsize = len(inplist)
for i in range(listsize):
    if (inplist[i] != 0):
        inplist[i] = res/inplist[i]

print(inplist)


