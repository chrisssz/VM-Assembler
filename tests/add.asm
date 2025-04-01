# add.asm
# Take in two numbers, add them, and return the result
# Stephen Marz
# COSC365: Programming Languages and Systems
# 3-February-2025

main:
    stpush "Welcome to the number adder!\n"
    stprint
    # We can put a very large immediate here since pop will stop at
    # the bottom of the stack.
    pop     0xbeec
    goto Down
    nop
    nop
    nop

# Here's the actual start location
BackUp:
    stpush "Enter left: "
    stprint
    pop     16
    input
    # Enter left: takes four pushes, which is 16 bytes
    stpush "Enter right: "
    stprint
    # We have to be accurate here, since on top of this on the stack
    # is the first input.
    pop     20
    input
    add
    stpush "Result = "
    stprint
    pop     12
    print
    exit

Down:
    # Test going back up to a label
    goto BackUp
