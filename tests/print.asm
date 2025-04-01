# print.asm
# Test print and its associated formats
# Stephen Marz
# COSC365: Programming Languages and Systems
# 3-February-2025

main:
    stpush  "Enter a value: "
    stprint
    pop     0xbeec
    input
    stpush  "In decimal: "
    stprint
    pop     16
    print
    stpush  "In hex    : "
    stprint
    pop     16
    printh
    stpush  "In binary : "
    stprint
    pop     16
    printb
    stpush  "In octal  : "
    stprint
    pop     16
    printo
    exit

