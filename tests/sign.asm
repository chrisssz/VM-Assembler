# sign.asm
# Determine value's sign by using bitwise operators.
# Stephen Marz
# COSC365: Programming Languages and Systems
# 3-February-2025

main:
    stpush  "Enter value: "
    stprint
    pop     20
    # left >> right
    # push the left first
    input
    # then push the right
    push    31
    # x >> 31
    lsr
    ifez    positive
    stpush  "Value is negative.\n"
    stprint
    exit

positive:
    stpush  "Value is positive.\n"
    stprint
    exit
