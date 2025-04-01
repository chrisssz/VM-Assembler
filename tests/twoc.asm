# twoc.asm
# Manual two's complement tester.
# Stephen Marz
# COSC365: Programming Languages and Systems
# 3-February-2025

main:
    stpush  "Enter value: "
    stprint
    pop     20
    input
    # ~input
    not
    push    1
    # 1 + ~input
    add
    stpush  "Two's complement = "
    stprint
    pop     28
    print
    exit

