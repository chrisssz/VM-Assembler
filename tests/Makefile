ASMB=../assembler/ass.exe
SOURCES=$(wildcard *.asm)
OUT=$(patsubst %.asm,%.v,$(SOURCES))

all: $(OUT)

%.v: %.asm Makefile $(ASMB)
	$(ASMB) $< $@

clean:
	$(RM) $(OUT)
