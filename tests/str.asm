# str.asm
# Testing string and string print
# Stephen Marz
# COSC365: Programming Languages and Systems
# 3-February-2025

# Both escapes are used for the following string
stpush "is a \"test\" of a ton of different things that will get pushed!\n"
stpush "This "
# The following prints the most recent, which is "This "
stprint
# The following prints sp+8, which will be the second string.
# The reason we skip 8 is because "This " is 5 characters
# So, each push is three characters and either a 1 (continue) or 0 (stop)
# Either way, 5 characters is going to take:
#  (1) "Thi" continue
#  (2) "s " stop
# So, this takes two pushes, which will be 8 bytes.
stprint 8
exit
