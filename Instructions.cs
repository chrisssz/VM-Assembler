public class Exit : IInstruction {
	private readonly int _code;
	public Exit(int code) {
		_code = code;
	}
	public int Encode() {
		return _code;
	}
}

public class Swap : IInstruction {
	private readonly int _from, _to;
	public Swap(fro=4, to=0) {
		_from = fro;
		_to = to;
	}
	public int Encode() {
		return (0b00000001 << 24) | (_from << 12) | _to
	}
}

public class Nop : IInstruction {
	public Nop() {};
	public int Encode() {
		return 0b0010 << 24;
	}
}

public class Input : IInstruction {
	public Input() {};
	public int Encode() {
		return 0b0100 << 24;
	}
}

public class Stinput : IInstruction {
	private readonly int _UMC;
	public Stinput(int UMC = 0x00FFFFFF) {
		_UMC = UMC;
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

public class Dup : IInstruction {
    private readonly int _offset;
    public Dup(int offset) {
        _offset = offset & ~3;
    }
    public int Encode() {
        return (0b1100 << 28) | _offset;
    }
}
