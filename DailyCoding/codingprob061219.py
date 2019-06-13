# Given a list of numbers and a number k, return whether any two numbers from the list add up to k.
# For example, given [10, 15, 3, 7] and k of 17, return true since 10 + 7 is 17.
# Bonus: Can you do this in one pass?

# Soln given below takes the list as input and stops when user enters -1. Next it takes the target number as input.
# It is making and using a probables list which is empty in begining and for each number in the list it checks if it is in the probables. 
# If so then we know the required two numbers are found. If not found in probables it adds the counterpart of the current number to the list.

def findinlist(probables,number):
    for i in probables:
        if number == i:
            return True
    
    return False

inplist = []
inp = int(input())

while inp != -1:
    inplist.append(inp)
    inp = -1
    inp = int(input())

probables = []
inp = int(input())
probables.append(inp)
for i in inplist:
    res = findinlist(probables,i)
    if res == True:
        print("True")
        break
    probables.append(inp - i)