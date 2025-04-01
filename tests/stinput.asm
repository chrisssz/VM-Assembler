# stinput.asm
# Test the string input instruction.
# Stephen Marz
# COSC365: Programming Languages and Systems
# 3-February-2025

main:
    stpush  "Enter a string: " # This will expand into the pushes needed for the string
    stprint
    pop     24
    # Input a string with a max size of 5
    stinput 5
    # 0 is pushed to the stack on an empty string, so test it
    # and see if we actually got something.
    ifez    NoString
    # This should now print You wrote = 'X' with X being the
    # string truncated to 5 characters.
    stpush  "You wrote = '"
    stprint
    pop     20
    stprint
    stpush "'\n"
    stprint
    exit

NoString:
    stpush "No string was given!\n"
    stprint
    exit 1
