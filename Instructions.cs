public class Exit : IInstruction {
	private readonly int _code;
	public Exit(int code=0) {
		_code = code;
	}
	public int Encode() {
		return _code;
	}
}

public class Swap : IInstruction {
	private readonly int _from, _to;
	public Swap(int fro=4, int to=0) {
		_from = fro;
		_to = to;
	}
	public int Encode() {
		return (0b00000001 << 24) | (_from << 12) | _to;
	}
}

public class Nop : IInstruction {
	public Nop() {}
	public int Encode() {
		return 0b0010 << 24;
	}
}

public class Input : IInstruction {
	public Input() {}
	public int Encode() {
		return 0b0100 << 24;
	}
}

public class Stinput : IInstruction {
	private readonly int _UMC;
	public Stinput(int UMC = 0x00FFFFFF) {
		_UMC = 0xFFFFFF & UMC;
	}
	public int Encode() {
		return (0b0101 << 24) | _UMC;
	}
}

public class Debug : IInstruction {
	private readonly int _value;
	public Debug(int val = 0) {
		_value = val;
	}
	public int Encode() {
		return (0b1111 << 24) | _value;
	}
}

public class Pop : IInstruction {
	private readonly int _offset;
	public Pop(int offset = 4) {
		_offset = offset & ~3;
	}
	public int Encode() {
		return (0b0001 << 28) | _offset;
	}
}

public class Add : IInstruction {
	public Add() {}
	public int Encode() {
		return 0b00100000 << 24;
	}
}

public class Sub : IInstruction {
	public Sub() {}
	public int Encode() {
		return 0b00100001 << 24;
	}
}

public class Mul : IInstruction {
	public Mul() {}
	public int Encode() {
		return 0b00100010 << 24;
	}
}

public class Div : IInstruction {
	public Div() {}
	public int Encode() {
		return 0b00100011 << 24;
	}
}

public class Rem : IInstruction {
	public Rem() {}
	public int Encode() {
		return 0b00100100 << 24;
	}
}

public class And : IInstruction {
	public And() {}
	public int Encode() {
		return 0b00100101 << 24;
	}
}

public class Or : IInstruction {
	public Or() {}
	public int Encode() {
		return 0b00100110 << 24;
	}
}

public class Xor : IInstruction {
	public Xor() {}
	public int Encode() {
		return 0b00100111 << 24;
	}
}

public class Lsl : IInstruction {
	public Lsl() {}
	public int Encode() {
		return 0b00101000 << 24;
	}
}

public class Lsr : IInstruction {
	public Lsr() {}
	public int Encode() {
		return 0b00101001 << 24;
	}
}

public class Asr : IInstruction {
	public Asr() {}
	public int Encode() {
		return 0b00101011 << 24;
	}
}

public class Neg : IInstruction {
	public Neg() {}
	public int Encode() {
		return 0b00110000 << 24;
	}
}

public class Not : IInstruction {
	public Not() {}
	public int Encode() {
		return 0b00110001 << 24;
	}
}

public class Stprint : IInstruction {
	private readonly int _offset;
	public Stprint(int offset=0) {
		_offset = offset & ~3;
	}
	public int Encode() {
		return (0b0100 << 28) | _offset;
	}
}

public class Call : IInstruction {
	private readonly int _offset;
	public Call(int offset) {
		_offset = offset & ~3;
	}
	public int Encode() {
		return (0b0101 << 28) | _offset;
	}
}

public class Return : IInstruction {
	private readonly int _offset;
	public Return(int offset=0) {
		_offset = offset & ~3;
	}
	public int Encode() {
		return (0b0110 << 28) | _offset;
	}
}

public class Goto : IInstruction {
	public readonly int _offset;
	public Goto(int offset) {
		_offset = offset & ~3;
	}
	public int Encode() {
		return (0b0111 << 28) | _offset;
	}
}

public class Ifeq : IInstruction {
	public readonly int _offset;
	public Ifeq(int offset) {
		_offset = offset & ~ 3;
	}
	public int Encode() {
		return (0b1000000 << 25) | _offset;
	}
}

public class Ifne : IInstruction {
	public readonly int _offset;
	public Ifne(int offset) {
		_offset = offset & ~ 3;
	}
	public int Encode() {
		return (0b1000001 << 25) | _offset;
	}
}

public class Iflt : IInstruction {
	public readonly int _offset;
	public Iflt(int offset) {
		_offset = offset & ~ 3;
	}
	public int Encode() {
		return (0b1000010 << 25) | _offset;
	}
}

public class Ifgt : IInstruction {
	public readonly int _offset;
	public Ifgt(int offset) {
		_offset = offset & ~ 3;
	}
	public int Encode() {
		return (0b1000011 << 25) | _offset;
	}
}

public class Ifle : IInstruction {
	public readonly int _offset;
	public Ifle(int offset) {
		_offset = offset & ~ 3;
	}
	public int Encode() {
		return (0b1000100 << 25) | _offset;
	}
}

public class Ifge : IInstruction {
	public readonly int _offset;
	public Ifge(int offset) {
		_offset = offset & ~ 3;
	}
	public int Encode() {
		return (0b1000101 << 25) | _offset;
	}
}

public class Ifez : IInstruction {
	public readonly int _offset;
	public Ifez(int offset) {
		_offset = offset & ~ 3;
	}
	public int Encode() {
		return (0b100100 << 26) | _offset;
	}
}

public class Ifnz : IInstruction {
	public readonly int _offset;
	public Ifnz(int offset) {
		_offset = offset & ~ 3;
	}
	public int Encode() {
		return (0b100101 << 26) | _offset;
	}
}

public class Ifmi : IInstruction {
	public readonly int _offset;
	public Ifmi(int offset) {
		_offset = offset & ~ 3;
	}
	public int Encode() {
		return (0b100110 << 26) | _offset;
	}
}

public class Ifpl : IInstruction {
	public readonly int _offset;
	public Ifpl(int offset) {
		_offset = offset & ~ 3;
	}
	public int Encode() {
		return (0b100111 << 26) | _offset;
	}
}

public class Dup : IInstruction {
    private readonly int _offset;
    public Dup(int offset=0) {
        _offset = offset & ~3;
    }
    public int Encode() {
        return (0b1100 << 28) | _offset;
    }
}

public class Print : IInstruction {
	private readonly int _offset;
	public Print(int offset=0) {
		_offset = offset;
	}
	public int Encode() {
		return (0b1101 << 28) | _offset << 2 | 00;
	}
}

public class Printh : IInstruction {
	private readonly int _offset;
	public Printh(int offset=0) {
		_offset = offset;
	}
	public int Encode() {
		return (0b1101 << 28) | _offset << 2 | 01;
	}
}

public class Printb : IInstruction {
	private readonly int _offset;
	public Printb(int offset=0) {
		_offset = offset;
	}
	public int Encode() {
		return (0b1101 << 28) | _offset << 2 | 10;
	}
}

public class Printo : IInstruction {
	private readonly int _offset;
	public Printo(int offset=0) {
		_offset = offset;
	}
	public int Encode() {
		return (0b1101 << 28) | _offset << 2 | 11;
	}
}

public class Dump : IInstruction {
	public Dump() {}
	public int Encode() {
		return 0b1110 << 28;
	}
}

public class Push: IInstruction {
	private readonly int _offset;
	public Push(int offset=0) {
		_offset = offset;
	}
	public int Encode() {
		return (0b1111 << 28) | _offset;
	}
}
