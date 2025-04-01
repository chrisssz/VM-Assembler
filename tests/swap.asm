# swap.asm
# Test swap with negative offsets
# Stephen Marz
# COSC365: Programming Languages and Systems
# 3-February-2025

main:
    push  10
    push  20
    push  50
    # 50 <---- sp
    # 20  +4
    # 10  +8
    pop   12
    # 50  -12
    # 20  -8
    # 10  -4 
    #     <---- sp
    push  40
    # 50  -8
    # 20  -4
    # 40  <---- sp
    swap  -4   # Swap 20/40
    # 50  -8
    # 40  -4
    # 20  <---- sp
    push  20
    ifne  NotEqual20
    pop   # Pop off 20, which was the testing element.
    # 50  -8
    # 40  -4
    # 20  <---- sp
    swap  -8 -4  # Swap 50/40
    # 40  -8
    # 50  -4
    # 20  <---- sp
    swap  -4 # Swap 20/50
    # 40  -8
    # 20  -4
    # 50  <---- sp
    push    50
    ifne    NotEqual50
    pop
    stpush  "SUCCESS! No errors in swap detected.\n"
    stprint
    exit

NotEqual50:
    stpush  "ERROR: 50 != 50\n"
    stprint
    exit    50

NotEqual20:
    stpush  "ERROR: 20 != 20\n"
    stprint
    exit    20
