# abs.asm
# Absolute value tester
# Stephen Marz
# COSC365: Programming Languages and Systems
# 3-February-2025

main:
    goto StartAbs
    # These instructions should never be executed
    push 1
    push 2
    push 3
    push 4
# Starting point
StartAbs:
    stpush "Enter integer value: "
    stprint
    # We can use a large offset to clear the stack since it will stop
    # at the bottom of the stack
    pop  0xbeec
    # Input an integer value
    input
    # If the value is positive or 0, jump to Out
    ifpl    Out
    # If we get here, the value is negative, so negate it
    neg
Out:
    stpush "Absolute value = "
    stprint
    # We have to be accurate here since the value to print is just above it.
    # 24 = [Abs] [olu] [te ] [val] [ue ] [= ]  (6x4 = 24)
    # We can also pop off the value and do print, but this demonstrates using
    # print's offset.
    print 24
    exit
