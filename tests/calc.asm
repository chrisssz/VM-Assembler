# calc.asm
# A menu-based calculator
# Stephen Marz
# COSC365: Programming Languages and Systems
# 3-February-2025

goto Menu
main:
    stpush "\n\n"
    stprint
    pop     4
Menu:
    stpush "0 - exit\n1 - add\n2 - subtract\n3 - multiply\n4 - divide\n5 - mod\n6 - and\n7 - or\n8 - xor\n9 - lsl\n10 - lsr\n11 - asr\nEnter menu option: "
    stprint
    pop     0xbeec
    # Input menu option [0..7]
    input
    ifez    Out
    push    1
    ifeq    GoAdd
    pop
    push    2
    ifeq    GoSub
    # If we get here, 1 isn't right, so pop off 1
    pop
    push    3
    ifeq    GoMul
    pop
    push    4
    ifeq    GoDiv
    pop
    push    5
    ifeq    GoMod
    pop
    push    6
    ifeq    GoAnd
    pop
    push    7
    ifeq    GoOr
    pop
    push    8
    ifeq    GoXor
    pop
    push    9
    ifeq    GoLsl
    pop
    push    10
    ifeq    GoLsr
    pop
    push    11
    ifeq    GoAsr
    pop
    # If none of the above, goto OutError
    goto    OutError

GetInputs:
    stpush  "Enter left operand: "
    stprint
    pop     28
    input
    stpush  "Enter right operand: "
    stprint
    pop     28
    input
    # right <-- sp + 0
    # left      sp + 4
    # return    sp + 8
    swap    8
    # return <-- sp + 0
    # left       sp + 4
    # right      sp + 8
    swap    4 8
    # return <-- sp + 0
    # right      sp + 4
    # left       sp + 8
    return

GoXor:
    call    GetInputs
    xor
    print
    goto    main

GoOr:
    call    GetInputs
    or
    print
    goto    main

GoAnd:
    call    GetInputs
    and
    print
    goto    main

GoMod:
    call    GetInputs
    rem
    print
    goto    main

GoDiv:
    call    GetInputs
    div
    print
    goto    main

GoMul:
    call    GetInputs
    mul
    print
    goto    main

GoSub:
    call    GetInputs
    sub
    print
    goto    main

GoAdd:
    call    GetInputs
    add
    print
    goto    main

GoLsl:
    call    GetInputs
    lsl
    print
    goto    main

GoLsr:
    call    GetInputs
    lsr
    print
    goto    main

GoAsr:
    call    GetInputs
    asr
    print
    goto    main

Out:
    exit

# If we get an error
OutError:
    stpush  "Invaild menu option.\n"
    stprint
    exit    1

# Divide by zero error
OutDivError:
    stpush  "DIVIDE BY ZERO!\n"
    stprint
    exit    2

