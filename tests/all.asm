# all.asm
# This tests the output of the instructions and is not inteded to be ran.
# Stephen Marz
# COSC365
# 23-March-2025

start:
    goto end

# Exit can take a value or not
# Exit defaults to 0.
exit 10
exit 255
exit -255
exit

# Swap can take 2, 1, or no arguments
# No arguments defaults to 0 and 4
swap 8 4
swap 0
swap

# No operation is simple
nop

# Input takes no arguments
input

# String input can take a decimal argument or hex argument or no argument.
# By default stinput with no argument should encode all Fs
stinput 5
stinput 0xf
stinput

# Debug instruction can take an argument as decimal or hex or no argument.
debug 5
debug 0xedFE
debug

# Pop can take an argument or if none, encode 0.
pop 1024
pop

# Binary arithmetic is simple. No arguments for any.
add
sub
mul
div
rem
and
or
xor
lsl
lsr
asr

# Unary arithmetic is also fairly simple.
neg
not

# String print can take an optional decimal or hex argument.
# Otherwise, it is encoded with 0
stprint 0x8765
stprint 127
stprint

# Call requires a label
call start
call end

# Return can take an optional offset
return 0xadde0
return 96
return

# Goto instruction takes a label
goto start

# Binary if instructions take a condition and a label
ifeq start
ifne end
iflt start
ifgt end
ifle start
ifge end

# Unary if instructions take a condition and a label
ifez start
ifnz end
ifpl start
ifmi end

# dup instruction take a decimal, hex, or defaults to 0
dup 0x80
dup -12
dup

# print has four forms and can take a decimal, hex, or nothing which defaults to 0
print 12
print 0x80
print
printo -12
printo 0x1c
printo
printh -128
printh 0xd0
printh
printb -256
printb 0xdead00
printb

# dump takes no parameters
dump

# push takes an optional hex, decimal, or label. By default it pushes 0.
push
push 0x8123
push start
push -225

# stpush is the only pseudo instruction, but it should expand to one
# push for each 3 characters. It also needs to support three escapes \\, \n, and \"
stpush "Hello\\ \"World\"\n"

end:
    exit

