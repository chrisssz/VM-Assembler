# call.asm
# Demonstrates call and return
# Stephen Marz
# COSC365: Programming Languages and Systems
# 3-February-2025

main:
    stpush "Welcome to the caller!\n"
    stprint
    # Pop the entire stack
    pop 0xbeec
    call SubRoutine
    # Print the gathered integer
    stpush "Val = "
    stprint
    pop 8
    print -32
    stpush "After subroutine!\n"
    stprint
    exit 0

SubRoutine:
    stpush "Enter an integer: "
    stprint
    input
    # With 24 bytes for stpush and 4 more bytes for input
    # we have to move down 28 to get to the return address.
    return 28
    exit 123

