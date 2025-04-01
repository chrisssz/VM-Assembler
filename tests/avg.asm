# avg.asm
# Calculate the average of a set of values
# Stephen Marz
# COSC365: Programming Languages and Systems
# 3-February-2025

# This will be the number of values
push        0

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
    # Now, we have to add an iteration to the number of values
    # To get the number of values in the right spot, we have to
    # swap it with the running total.
    swap
    # Now we push the value to update the number of values (+1)
    push    1
    add
    # Right now, at the stack pointer is the updated number of values.
    # So, swap it back with the running total for the next iteration.
    swap
    goto    main
quit:
    # Remove the last input, which will be 0 to get here
    pop
    # Make sure we have a value, otherwise we will get divide by 0
    ifez    out
    stpush "Avg = "
    stprint
    # Pop off Avg =
    pop     8
    # The running total is right now the denominator, swap it
    # to make it the numerator and the number of iterations the denominator.
    swap
    div
    # Recall that this is *integer* division, so this isn't very accurate :)
    print
out:
    exit
