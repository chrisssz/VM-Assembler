using System;
using System.Linq;
using System.Collections.Generic;

class Pass1 {

	public static void Main() {
		int memLoc = 0;
		int lineNo = 1;
		string fileName = "";
		string delCom, pushst, buf;
		string[] lines = {""}, split;
		List<string> hex = new List<string>();
		Dictionary<int, string[]> memory = new Dictionary<int, string[]>();
		Dictionary<int[], string> labels = new Dictionary<int[], string>();

		Console.WriteLine("Enter a Filename");

		try {
			fileName = Console.ReadLine() ?? "";

			lines = File.ReadAllLines(fileName);
		} catch {
			Console.WriteLine($"{fileName} does not exist");
		}


		foreach (string line in lines)
		{
			delCom = line.Split('#')[0].Trim();
			if (delCom == "") {
				continue;
			}

			split = delCom.Split(' ', StringSplitOptions.RemoveEmptyEntries);

			if (split[0] != "stpush") {
				if (split[0].Contains(':')) {
					labels.Add(new int[] {memLoc, lineNo} , split[0].Substring(0, split[0].Length-1));
				} else {
					memory.Add(memLoc, split);
					memLoc += 4;
				}
			} else {
				pushst = string.Join(' ', split.Skip(1)).Trim('"');

				pushst = pushst.Replace("\\\\", "\\");
				pushst = pushst.Replace("\\\"", "\"");
				pushst = pushst.Replace("\\\n", "|");

				buf = "";
				for (int i = 0; i < pushst.Length; i++) {
					if (pushst[i] == '|') buf = ((int)'|').ToString("X") + buf;
					else buf = ((int)pushst[i]).ToString("X") + buf;
					
					if (i % 3 == 2 && i != pushst.Length-1) {
						buf = "0x01" + buf;
						/* Console.WriteLine(buf); */
						hex.Add(buf);
						buf = "";
					} else if (i == pushst.Length-1) {
						while(buf.Length < 8) {
							buf = "00" + buf;
						}
						buf = "0x" + buf;
						/* Console.WriteLine(buf); */
						hex.Add(buf);
						buf = "";
					}
				}

				hex.Reverse();
				foreach (string cmd in hex) {
					memory.Add(memLoc, new string[] {"push", cmd});
					memLoc += 4;
				} 
				hex.Clear();

				memory.Add(memLoc, new string[] {"stprint"});

				memLoc += 4;
			}

			lineNo++;
				
			/* foreach (string liner in split){ */
    			/* Console.WriteLine(liner); */
			/* } */

		}

		foreach (var entry in memory) {
			Console.WriteLine($"{entry.Key}: {string.Join(" ", entry.Value)}");
		}

		foreach (var entry in labels) {
			Console.WriteLine($"{entry.Key[0]}: {entry.Value} {entry.Key[1]}");
		}

		WriteBytes(memory, labels);
	}

	public static void WriteBytes(Dictionary<int, string[]> memory, Dictionary<int[], string> labels) {
		using (BinaryWriter writer = new BinaryWriter(File.Open("tmp.txt", FileMode.Create))) {
			foreach (var entry in memory) {
				string cmd = entry.Value[0];

				if (cmd == "exit") {
					Exit exit;
					try {
						exit = new Exit(int.Parse(entry.Value[1]));
					} catch {
						exit = new Exit();
					}
				
            		writer.Write(exit.Encode());
        		} else if (cmd == "swap") {
					Swap swap;
					try {
						swap = new Swap(int.Parse(entry.Value[1]), int.Parse(entry.Value[2]));
					} catch {
						try {
						swap = new Swap(int.Parse(entry.Value[1]));
						} catch {
							swap = new Swap();
						}
					}
					writer.Write(swap.Encode());
				} else if (cmd == "nop") {
					Nop nop = new Nop();
					writer.Write(nop.Encode());
				} else if (cmd == "input") {
					Input inp = new Input();
					writer.Write(inp.Encode());
				} else if (cmd == "stinput") {
					Stinput stinput;
					if (entry.Value.Length == 2) {
						try {
							stinput = new Stinput(int.Parse(entry.Value[1]));
						} catch {
							stinput = new Stinput(int.Parse(entry.Value[1].Substring(2), System.Globalization.NumberStyles.HexNumber));
						}
					} else {
						stinput = new Stinput();
					}
					writer.Write(stinput.Encode());
				} else if (cmd == "debug") {
					Debug debug;
					if (entry.Value.Length == 2) {
						try {
							debug = new Debug(int.Parse(entry.Value[1]));
						} catch {
							debug = new Debug(int.Parse(entry.Value[1].Substring(2), System.Globalization.NumberStyles.HexNumber));
						}
					} else {
						debug = new Debug();
					}
					writer.Write(debug.Encode());
				} else if (cmd == "pop") {
					Pop pop;
					if (entry.Value.Length == 2) {
						pop = new Pop(int.Parse(entry.Value[1]));
					} else {
						pop = new Pop();
					}
					writer.Write(pop.Encode());
				} else if (cmd == "add") {
					Add add = new Add();
					writer.Write(add.Encode());
				} else if (cmd == "sub") {
					Sub sub = new Sub();
					writer.Write(sub.Encode());
				} else if (cmd == "mul") {
					Mul mul = new Mul();
					writer.Write(mul.Encode());
				} else if (cmd == "div") {
					Div div  = new Div();
					writer.Write(div.Encode());
				} else if (cmd == "rem") {
					Rem rem = new Rem();
					writer.Write(rem.Encode());
				} else if (cmd == "and") {
					And and = new And();
					writer.Write(and.Encode());
				} else if (cmd == "or") {
					Or or = new Or();
					writer.Write(or.Encode());
				} else if (cmd == "xor") {
					Xor xor = new Xor();
					writer.Write(xor.Encode());
				} else if (cmd == "lsl") {
					Lsl lsl = new Lsl();
					writer.Write(lsl.Encode());
				} else if (cmd == "lsr") {
					Lsr lsr = new Lsr();
					writer.Write(lsr.Encode());
				} else if (cmd == "asr") {
					Asr asr = new Asr();
					writer.Write(asr.Encode());
				} else if (cmd == "neg") {
					Neg neg = new Neg();
					writer.Write(neg.Encode());
				} else if (cmd == "not") {
					Not not = new Not();
					writer.Write(not.Encode());
				} else if (cmd == "stprint") {
					Stprint stprint;
					if (entry.Value.Length == 2) {
						try {
							stprint = new Stprint(int.Parse(entry.Value[1]));
						} catch {
							stprint = new Stprint(int.Parse(entry.Value[1].Substring(2), System.Globalization.NumberStyles.HexNumber));
						}
					} else {
						stprint = new Stprint();
					}
					writer.Write(stprint.Encode());
				} else if (cmd == "call") {
					int loc = labels.FirstOrDefault(x => x.Value == entry.Value[1]).Key[0];
					Call call = new Call(loc);
					writer.Write(call.Encode());
				} else if (cmd == "return") {
					Return ret;
					if (entry.Value.Length == 2) {
						ret = new Return(int.Parse(entry.Value[1]));
					} else {
						ret = new Return();
					}
					writer.Write(ret.Encode());
				}
			}
		}
	}
}
