# sum.asm
# Test the virtual machine by calculating a running sum.
# Stephen Marz
# COSC365: Programming Languages and Systems
# 3-February-2025

# This will be our running total
push        0

main:
    stpush  "Value to add (0 to quit): "
    stprint
    # Easiest way to know how much to pop is to
    # count in threes and multiply by 4. Above,
    # there are 9 groups of 3, which 9 * 4 = 36.
    pop     36
    input
    # If we get 0, go to quit
    ifez    quit
    add
    goto    main
quit:
    # Remove the last input, which will be 0 to get here
    pop
    stpush "Sum = "
    stprint
    # Pop off Sum =
    pop     8
    # Print the running total
    print
    exit
