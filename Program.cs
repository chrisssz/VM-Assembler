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
				pushst = pushst.Replace("\\n", "\n");

				buf = "";
				for (int i = 0; i < pushst.Length; i++) {
					if (pushst[i] == '|') buf = ((int)'|').ToString("X") + buf;
					else buf = ((int)pushst[i]).ToString("X") + buf;
					
					if (i % 3 == 2 && i != pushst.Length-1) {
						buf = "0x01" + buf;
						hex.Add(buf);
						buf = "";
					} else if (i == pushst.Length-1) {
						while(buf.Length < 8) {
							buf = "00" + buf;
						}
						buf = "0x" + buf;
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
			}

			lineNo++;
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
			writer.Write(new byte[] {0xDE, 0xAD, 0xBE, 0xEF});
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
						try {
							pop = new Pop(int.Parse(entry.Value[1]));
						} catch {
							pop = new Pop(int.Parse(entry.Value[1].Substring(2), System.Globalization.NumberStyles.HexNumber));
						}
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
					int loc = labels.FirstOrDefault(x => x.Value == entry.Value[1]).Key[0] - entry.Key;
					Call call = new Call(loc);
					writer.Write(call.Encode());
				} else if (cmd == "return") {
					Return ret;
					if (entry.Value.Length == 2) {
						try {
							ret = new Return(int.Parse(entry.Value[1]));
						} catch {
							ret = new Return(int.Parse(entry.Value[1].Substring(2), System.Globalization.NumberStyles.HexNumber));
						}
					} else {
						ret = new Return();
					}
					writer.Write(ret.Encode());
				} else if (cmd == "goto") {
					int loc = labels.FirstOrDefault(x => x.Value == entry.Value[1]).Key[0] - entry.Key;
					Goto gogo = new Goto(loc);
					writer.Write(gogo.Encode());
				} else if (cmd == "ifeq") {
					int loc = labels.FirstOrDefault(x => x.Value == entry.Value[1]).Key[0] - entry.Key;
					Ifeq ifeq = new Ifeq(loc);
					writer.Write(ifeq.Encode());
				} else if (cmd == "ifne") {
					int loc = labels.FirstOrDefault(x => x.Value == entry.Value[1]).Key[0] - entry.Key;
					Ifne ifne = new Ifne(loc);
					writer.Write(ifne.Encode());
				} else if (cmd == "iflt") {
					int loc = labels.FirstOrDefault(x => x.Value == entry.Value[1]).Key[0] - entry.Key;
					Iflt iflt = new Iflt(loc);
					writer.Write(iflt.Encode());
				} else if (cmd == "ifgt") {
					int loc = labels.FirstOrDefault(x => x.Value == entry.Value[1]).Key[0] - entry.Key;
					Ifgt ifgt = new Ifgt(loc);
					writer.Write(ifgt.Encode());
				} else if (cmd == "ifle") {
					int loc = labels.FirstOrDefault(x => x.Value == entry.Value[1]).Key[0] - entry.Key;
					Ifle ifle = new Ifle(loc);
					writer.Write(ifle.Encode());
				} else if (cmd == "ifge") {
					int loc = labels.FirstOrDefault(x => x.Value == entry.Value[1]).Key[0] - entry.Key;
					Ifge ifge = new Ifge(loc);
					writer.Write(ifge.Encode());
				} else if (cmd == "ifez") {
					int loc = labels.FirstOrDefault(x => x.Value == entry.Value[1]).Key[0] - entry.Key;
					Ifez ifez = new Ifez(loc);
					writer.Write(ifez.Encode());
				} else if (cmd == "ifnz") {
					int loc = labels.FirstOrDefault(x => x.Value == entry.Value[1]).Key[0] - entry.Key;
					Ifnz ifnz = new Ifnz(loc);
					writer.Write(ifnz.Encode());
				} else if (cmd == "ifmi") {
					int loc = labels.FirstOrDefault(x => x.Value == entry.Value[1]).Key[0] - entry.Key;
					Ifmi ifmi = new Ifmi(loc);
					writer.Write(ifmi.Encode());
				} else if (cmd == "ifpl") {
					int loc = labels.FirstOrDefault(x => x.Value == entry.Value[1]).Key[0] - entry.Key;
					Ifpl ifpl = new Ifpl(loc);
					writer.Write(ifpl.Encode());
				} else if (cmd == "dup") {
					Dup dup;
					if (entry.Value.Length == 2) {
						try {
							dup = new Dup(int.Parse(entry.Value[1]));
						} catch {
							dup = new Dup(int.Parse(entry.Value[1].Substring(2), System.Globalization.NumberStyles.HexNumber));
						}
					} else {
						dup = new Dup();
					}
					writer.Write(dup.Encode());
				} else if (cmd == "print") {
					Print print;
					if (entry.Value.Length == 2) {
						try {
							print = new Print(int.Parse(entry.Value[1]));
						} catch {
							print = new Print(int.Parse(entry.Value[1].Substring(2), System.Globalization.NumberStyles.HexNumber));
						}
					} else {
						print = new Print();
					}
					writer.Write(print.Encode());
				} else if (cmd == "printh") {
					Printh printh;
					if (entry.Value.Length == 2) {
						try {
							printh = new Printh(int.Parse(entry.Value[1]));
						} catch {
							printh = new Printh(int.Parse(entry.Value[1].Substring(2), System.Globalization.NumberStyles.HexNumber));
						}
					} else {
						printh = new Printh();
					}
					writer.Write(printh.Encode());
				} else if (cmd == "printb") {
					Printb printb;
					if (entry.Value.Length == 2) {
						try {
							printb = new Printb(int.Parse(entry.Value[1]));
						} catch {
							printb = new Printb(int.Parse(entry.Value[1].Substring(2), System.Globalization.NumberStyles.HexNumber));
						}
					} else {
						printb = new Printb();
					}
					writer.Write(printb.Encode());
				} else if (cmd == "printo") {
					Printo printo;
					if (entry.Value.Length == 2) {
						try {
							printo = new Printo(int.Parse(entry.Value[1]));
						} catch {
							printo = new Printo(int.Parse(entry.Value[1].Substring(2), System.Globalization.NumberStyles.HexNumber));
						}
					} else {
						printo = new Printo();
					}
					writer.Write(printo.Encode());
				} else if (cmd == "dump") {
					Dump dump = new Dump();
					writer.Write(dump.Encode());
				} else {
					Push push;
					if (entry.Value.Length == 1) {
						push = new Push();
					} else {
						try {
							push = new Push(int.Parse(entry.Value[1]));
						} catch {
							if (entry.Value[1].Substring(0, 2) == "0x") {
								push = new Push(int.Parse(entry.Value[1].Substring(2), System.Globalization.NumberStyles.HexNumber));
							} else {
								int loc = labels.FirstOrDefault(x => x.Value == entry.Value[1]).Key[0];
								push = new Push(loc);
							}
						}	
					}
					writer.Write(push.Encode());
				}
			}
		}
	}
}
