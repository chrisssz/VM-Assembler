# for.asm
# Repeatedly take input and iterate using a for loop
# Stephen Marz
# COSC365: Programming Languages and Systems
# 3-February-2025

main:
    stpush  "How many iterations? "
    stprint
    pop     0xbeec
    input
    ifez    Exit
    ifmi    NegError
    # Push an iterator i = 1
    push    1

TopLoop:
    stpush  "i = "
    stprint
    pop     8
    # Print the iteration we are on
    print
    # Push 1 so that we can add it to the iterator
    push    1
    # Add 1 to the iterator. This pops two operands
    # off the stack and pushes the result, so the stack
    # is pointing to the result.
    add
    # ifle looks at the top two values on the stack and
    # compares them. The target is first (left) and the
    # iteration is second (right), so this is
    # target >= iteration
    ifge    TopLoop

BottomLoop:
    goto    Exit

NegError:
    stpush  "ERROR: You gave a negative number!\n"
    stprint
    pop     0xbeec
    goto    main

Exit:
    exit
